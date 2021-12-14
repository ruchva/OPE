using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BG.SIA.ESTRUCTURA;
using BG.SIA.NEGOCIO;
using BG.SIA.ESTRUCTURA.TC;
using BG.SIA.UTILITARIO;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxUploadControl;
using DevExpress.Web.ImageControls.Internal;
using System.Web.UI.HtmlControls;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using OfficeOpenXml;


public partial class UI_OPE_frmCargaNanoCreditos : System.Web.UI.Page
{
    public static string sEstadoProcesado = "N";
    protected void Page_Load(object sender, EventArgs e)
    {
        AddJavascript("../../JS/OPE/jsCargaArchivosOperaciones.js");
        if (!IsPostBack)
        {
            List<string> lstCabecera = new List<string>();
            //wucExportarGrid2.userControlClick += new UserControlDelegate(UserControlDemo_userControlClick);
            //wucExportarGrid2.dpveControlExportar = dgveGrid;
            //wucExportarGrid2.sTitulo = dlblTitulo.Text;
            lstCabecera = new List<string>();
            lstCabecera.Add("SUCURSAL: " + this.Context.Session["sesIdSucursal"].ToString() + "   REPORTE ARCHIVOS NANOCREDITOS:     " + "       FECHA: " + DateTime.Now.ToShortDateString().ToString());
            lstCabecera.Add("USUARIO: " + HttpContext.Current.User.Identity.Name.ToString() + "         DESDE FECHA: " + "-" + "    HASTA FECHA: " + "-" + "      HORA: " + DateTime.Now.ToShortTimeString().ToString());
            //wucExportarGrid2.lstCabecera = lstCabecera;
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        //if (dcbTiposArchivo.SelectedItem.Value.ToString() == "RE")
        //{
        //    mtdCargarGridRetenciones(dcbTiposArchivo.SelectedItem.Value.ToString());
        //}
    }
    #region AUXILIARES

    #endregion
    public void AddJavascript(string javascriptUrl)
    {
        HtmlGenericControl script = new HtmlGenericControl("script");
        script.Attributes.Add("type", "text/javascript");
        javascriptUrl += "?v" + Assembly.GetExecutingAssembly().GetName().Version + DateTime.Now.Year.ToString() +
           DateTime.Now.Month.ToString() + DateTime.Now.Ticks.ToString();
        script.Attributes.Add("src", ResolveUrl(javascriptUrl));
        Page.Header.Controls.Add(script);
    }

    void UserControlDemo_userControlClick(ControlClienteEventHandler valor)
    {
        if (valor.STitulo != "") { }
    }

    public override void Dispose()
    {
        base.Dispose();
    }
    protected void dcllRefrescarCarga_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {

    }
    protected void dgrvGcie_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {

    }
    protected void dupcArchivo_FileUploadComplete(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
    {
    }

    private string mtdGuardarArchivo2(UploadedFile uploadedFile, string psSelected, string pFecha)
    {
        return "";
        //string lsRespuesta = "false~RE";

        //try
        //{
        //    string folderPath = Server.MapPath("~/UPLOAD/XLS");
        //    //Check whether Directory (Folder) exists.
        //    if (!Directory.Exists(folderPath))
        //    {
        //        //If Directory (Folder) does not exists. Create it.
        //        Directory.CreateDirectory(folderPath);
        //    }
        //    //Save the File to the Directory (Folder).
        //    FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));
        //    //Display the success message.
        //    //lblMessage.Text = Path.GetFileName(FileUpload1.FileName) + " ha sido cargado.";                           
        //    if (!uploadedFile.IsValid)
        //        return string.Empty;
        //    byte[] luArray = uploadedFile.FileBytes.ToArray();
        //    //string lsFileName = uploadedFile.PostedFile.FileName;
        //    string lsFileName = Path.GetFileName(FileUpload1.FileName);
        //    Microsoft.Office.Interop.Excel.Application xlApp;
        //    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        //    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        //    Microsoft.Office.Interop.Excel.Range range;

        //    int rw = 0;
        //    int cl = 0;



        //    xlApp = new Microsoft.Office.Interop.Excel.Application();
        //    xlWorkBook = xlApp.Workbooks.Open(lsFileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

        //    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

        //    range = xlWorkSheet.UsedRange;
        //    rw = range.Rows.Count;
        //    cl = range.Columns.Count;


        //    clsNanoCreditoDetalle eNanoCreditoDetalle = new clsNanoCreditoDetalle();
        //    List<clsNanoCreditoDetalle> lstCreditoDetalle = new List<clsNanoCreditoDetalle>();
        //    string lsUser = (string)this.Session["IdUsuario"];
        //    var nrodocumento = double.MinValue;
        //    var lugaremision = double.MinValue;
        //    var paisnacimiento = double.MinValue;
        //    var telefonoreferencia1 = double.MinValue;
        //    var tiporeferenicia1 = double.MinValue;
        //    var telefonoreferencia2 = double.MinValue;
        //    var tiporeferenicia2 = double.MinValue;
        //    var departamentodd = double.MinValue;
        //    var ciudadd = double.MinValue;
        //    var zona = double.MinValue;
        //    var telefonofijo = double.MinValue;
        //    var telefonocelular = double.MinValue;
        //    var ciudadt = double.MinValue;
        //    var zonadt = double.MinValue;
        //    var montoingreso = double.MinValue;
        //    var montocompra = double.MinValue;
        //    var departametodt = double.MinValue;
        //    var otrasactividades = double.MinValue;
        //    var codigopdv = double.MinValue;
        //    for (int i = 2; i <= rw; i++)
        //    {
        //        if ((xlWorkSheet.Cells[i, 1] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        {
        //            nrodocumento = 0;
        //        }
        //        else
        //        {
        //            nrodocumento = (double)(xlWorkSheet.Cells[i, 1] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        }

        //        var extduplicada = (string)(xlWorkSheet.Cells[i, 2] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        {
        //            lugaremision = 0;
        //        }
        //        else
        //        {
        //            lugaremision = (double)(xlWorkSheet.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        }
        //        var fechaemision = (DateTime)(xlWorkSheet.Cells[i, 4] as Microsoft.Office.Interop.Excel.Range).Value;
        //        var fechavecimiento = (DateTime)(xlWorkSheet.Cells[i, 5] as Microsoft.Office.Interop.Excel.Range).Value;
        //        var apellidopaterno = (string)(xlWorkSheet.Cells[i, 6] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        var apellidomaterno = (string)(xlWorkSheet.Cells[i, 7] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        var primernombre = (string)(xlWorkSheet.Cells[i, 8] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        var segundonombre = (string)(xlWorkSheet.Cells[i, 9] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        var fechanacimiento = (DateTime)(xlWorkSheet.Cells[i, 10] as Microsoft.Office.Interop.Excel.Range).Value;
        //        if ((xlWorkSheet.Cells[i, 11] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        {
        //            paisnacimiento = 0;
        //        }
        //        else
        //        { paisnacimiento = (double)(xlWorkSheet.Cells[i, 11] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        var sexo = (string)(xlWorkSheet.Cells[i, 12] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        var estadocivil = (string)(xlWorkSheet.Cells[i, 13] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        var nombrereferencia1 = (string)(xlWorkSheet.Cells[i, 14] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 15] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        { telefonoreferencia1 = 0; }
        //        else { telefonoreferencia1 = (double)(xlWorkSheet.Cells[i, 15] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        if ((xlWorkSheet.Cells[i, 16] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        {
        //            tiporeferenicia1 = 0;
        //        }
        //        else
        //        { tiporeferenicia1 = (double)(xlWorkSheet.Cells[i, 16] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        var nombrereferencia2 = (string)(xlWorkSheet.Cells[i, 17] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 18] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        { telefonoreferencia2 = 0; }
        //        else { telefonoreferencia2 = (double)(xlWorkSheet.Cells[i, 18] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var telefonoreferencia2 = (double)(xlWorkSheet.Cells[i, 18] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 19] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        { tiporeferenicia2 = 0; }
        //        else
        //        { tiporeferenicia2 = (double)(xlWorkSheet.Cells[i, 19] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var tiporeferenicia2 = (double)(xlWorkSheet.Cells[i, 19] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 20] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        { departamentodd = 0; }
        //        else { departamentodd = (double)(xlWorkSheet.Cells[i, 20] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var departamentodd = (double)(xlWorkSheet.Cells[i, 20] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 21] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        { ciudadd = 0; }
        //        else { ciudadd = (double)(xlWorkSheet.Cells[i, 21] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var ciudadd = (double)(xlWorkSheet.Cells[i, 21] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        var barrio = (string)(xlWorkSheet.Cells[i, 22] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 23] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        { zona = 0; }
        //        else { zona = (double)(xlWorkSheet.Cells[i, 23] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var zona = (double)(xlWorkSheet.Cells[i, 23] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        var calle = (string)(xlWorkSheet.Cells[i, 24] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 25] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { telefonofijo = 0; }
        //        else { telefonofijo = (double)(xlWorkSheet.Cells[i, 25] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var telefonofijo = (double)(xlWorkSheet.Cells[i, 25] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        //var telefonocelular = (double)(xlWorkSheet.Cells[i, 26] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 26] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        { telefonocelular = 0; }
        //        else { telefonocelular = (double)(xlWorkSheet.Cells[i, 26] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        var actividadlaboral = (DateTime)(xlWorkSheet.Cells[i, 27] as Microsoft.Office.Interop.Excel.Range).Value;
        //        //var departametodt = (double)(xlWorkSheet.Cells[i, 28] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 28] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        { departametodt = 0; }
        //        else { departametodt = (double)(xlWorkSheet.Cells[i, 28] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var ciudadt = (double)(xlWorkSheet.Cells[i, 29] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 29] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        { ciudadt = 0; }
        //        else { ciudadt = (double)(xlWorkSheet.Cells[i, 29] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        var barriodt = (string)(xlWorkSheet.Cells[i, 30] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        //var zonadt = (double)(xlWorkSheet.Cells[i, 31] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 31] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        { zonadt = 0; }
        //        else { zonadt = (double)(xlWorkSheet.Cells[i, 31] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        var calledt = (string)(xlWorkSheet.Cells[i, 32] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        //var montoingreso = (double)(xlWorkSheet.Cells[i, 33] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 33] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        { montoingreso = 0; }
        //        else { montoingreso = (double)(xlWorkSheet.Cells[i, 31] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var montocompra = (double)(xlWorkSheet.Cells[i, 34] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 34] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        { montocompra = 0; }
        //        else { montocompra = (double)(xlWorkSheet.Cells[i, 34] as Microsoft.Office.Interop.Excel.Range).Value2; }

        //        if ((xlWorkSheet.Cells[i, 35] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        { otrasactividades = 0; }
        //        else { otrasactividades = (double)(xlWorkSheet.Cells[i, 35] as Microsoft.Office.Interop.Excel.Range).Value2; }

        //        if ((xlWorkSheet.Cells[i, 36] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        { codigopdv = 0; }
        //        else { codigopdv = (double)(xlWorkSheet.Cells[i, 36] as Microsoft.Office.Interop.Excel.Range).Value2; }

        //        eNanoCreditoDetalle = new clsNanoCreditoDetalle();
        //        eNanoCreditoDetalle.PIntDpNroDocumento = Convert.ToInt32(nrodocumento);
        //        eNanoCreditoDetalle.PStrDpExtDuplicada = extduplicada;
        //        eNanoCreditoDetalle.PIntDpLugarEmision = Convert.ToInt32(lugaremision);
        //        eNanoCreditoDetalle.PDtDpFechaEmision = Convert.ToDateTime(fechaemision);
        //        eNanoCreditoDetalle.PDtDpFechaVencimiento = Convert.ToDateTime(fechavecimiento);
        //        eNanoCreditoDetalle.PStrDpApellidoPaterno = apellidopaterno;
        //        eNanoCreditoDetalle.PStrDpApellidoMaterno = apellidomaterno;
        //        eNanoCreditoDetalle.PStrDpPrimerNombre = primernombre;
        //        eNanoCreditoDetalle.PStrDpSegundoNombre = segundonombre;
        //        eNanoCreditoDetalle.PDtDpFechaNacimiento = fechanacimiento;
        //        eNanoCreditoDetalle.PPaisNacimiento = Convert.ToInt32(paisnacimiento);
        //        eNanoCreditoDetalle.PStrDpSexo = sexo;
        //        eNanoCreditoDetalle.PStrDpEstadoCivil = estadocivil;
        //        eNanoCreditoDetalle.PStrNombreReferenciaPer1 = nombrereferencia1;
        //        eNanoCreditoDetalle.PStrTelefonoReferencia1 = telefonoreferencia1;
        //        eNanoCreditoDetalle.PStrTipoRelacionReferencia1 = tiporeferenicia1;
        //        eNanoCreditoDetalle.PStrNombreReferenciaPer2 = nombrereferencia2;
        //        eNanoCreditoDetalle.PStrTelefonoReferenciaPer2 = telefonoreferencia2;
        //        eNanoCreditoDetalle.PStrTipoRelacionReferencia2 = tiporeferenicia2;
        //        eNanoCreditoDetalle.PIntDDDepartamento = Convert.ToInt32(departamentodd);
        //        eNanoCreditoDetalle.PIntDDCiudad = Convert.ToInt32(ciudadd);
        //        eNanoCreditoDetalle.PStrDDBarrio = barrio;
        //        eNanoCreditoDetalle.PIntDDZona = Convert.ToInt32(zona);
        //        eNanoCreditoDetalle.PStrDDCalle = calle;
        //        eNanoCreditoDetalle.PlngDDTelefonoFijo = Convert.ToInt64(telefonofijo);
        //        eNanoCreditoDetalle.PlngDDTelefonoCelular = Convert.ToInt64(telefonocelular);
        //        eNanoCreditoDetalle.PDtDLFechaInicioLaboral = actividadlaboral;
        //        eNanoCreditoDetalle.PIntDTDepartamento = Convert.ToInt32(departametodt);
        //        eNanoCreditoDetalle.PIntDTCiudad = Convert.ToInt32(ciudadt);
        //        eNanoCreditoDetalle.PStrDTBarrio = barriodt;
        //        eNanoCreditoDetalle.PIntDTZona = Convert.ToInt32(zonadt);
        //        eNanoCreditoDetalle.PStrDTCalle = calledt;
        //        eNanoCreditoDetalle.PLngMontoIngreso = Convert.ToInt64(montoingreso);
        //        eNanoCreditoDetalle.PLngEngresoCompraMercaderia = Convert.ToInt64(montocompra);
        //        eNanoCreditoDetalle.PUsuarioCreacion = lsUser;
        //        eNanoCreditoDetalle.PCodigoIPDV = Convert.ToInt64(codigopdv);
        //        eNanoCreditoDetalle.POtraActividad = Convert.ToInt32(otrasactividades);
        //        lstCreditoDetalle.Add(eNanoCreditoDetalle);
        //    }

        //    xlWorkBook.Close(true, null, null);
        //    xlApp.Quit();

        //    Marshal.ReleaseComObject(xlWorkSheet);
        //    Marshal.ReleaseComObject(xlWorkBook);
        //    Marshal.ReleaseComObject(xlApp);
        //    ///***********************************************************************                              
        //    clsNanoCredito eNanoCredito = new clsNanoCredito();
        //    clsNanoCreditoNeg lNanoCredito = new clsNanoCreditoNeg();
        //    eNanoCredito.PDtFechaProceso = DateTime.Now;
        //    eNanoCredito.PIntNroFilas = rw - 1;
        //    eNanoCredito.PStrEstado = "1";
        //    eNanoCredito.PStrNombreArchivo = uploadedFile.FileName;
        //    eNanoCredito.PStrUsuarioCreacion = lsUser;
        //    eNanoCredito.LstCreditoDetalle = lstCreditoDetalle;
        //    //lNanoCredito.InsertarNanoCredito(eNanoCredito);
        //}
        //catch (Exception ex)
        //{
        //    // clsTextLogs.WriteError("ERROR", ex);
        //    return "false~" + "Ocurrió un problema al procesar la información.";
        //}
        //return lsRespuesta;
    }
    private string mtdTraerStringIso(string pValor)
    {
        Encoding eIso = Encoding.GetEncoding("ISO-8859-1");
        Encoding eUtf8 = Encoding.UTF8;
        byte[] utfBytes = eUtf8.GetBytes(pValor);
        byte[] isoBytes = Encoding.Convert(eUtf8, eIso, utfBytes);
        return eIso.GetString(isoBytes);
    }


    #region "GRID_CARGAR"
    private void mtdCargarGridVista(string pTipo)
    {
        try
        {
            //throw new Exception();
            if (dcbTiposArchivo.SelectedItem.Value.ToString() == "RE")
                mtdCargarGridRetenciones(pTipo);
        }
        catch (Exception ERR)
        {
            throw new Exception("mtdCargarGridVista-->" + ERR.ToString());
        }
    }
    private void mtdCargarGridRetenciones(string pFecha)
    {
    }

    #endregion
    protected void cbAll_Init(object sender, EventArgs e)
    {
    }
    protected void dcbpGrid_Callback(object sender, CallbackEventArgsBase e)
    {
    }
    protected void dcbTiposArchivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        mtdCargarGridVista(dcbTiposArchivo.SelectedItem.Value.ToString());
    }
    protected void dcbpCombo_Callback(object sender, CallbackEventArgsBase e)
    {
        if (this.Context.Session["sExtension"] != null)
            dcbTiposArchivo.SelectedIndex = dcbTiposArchivo.Items.IndexOf(dcbTiposArchivo.Items.FindByText(this.Context.Session["sExtension"].ToString()));
        if (e.Parameter.ToString().Equals("Mensaje"))
        {
            dcbpCombo.JSProperties["cp_close"] = this.Context.Session["GuardadoExitoso"].ToString();
        }
    }

    #region "GRIDS_LOAD"

    protected void dgrvArchivosPrevios_Load(object sender, EventArgs e)
    {
    }
    #endregion
    protected void dgrvArchivosPrevios_BeforeGetCallbackResult(object sender, EventArgs e)
    {
    }
    public double validar(object row)
    {
        try
        {
            return Convert.ToDouble(row);
        }
        catch
        {
            return 0;
        }
    }
    public double validarNroDocumento(object row)
    {
        try
        {
            return Convert.ToDouble(row);
        }
        catch
        {
            throw;
        }
    }
    protected void UploadFile(object sender, EventArgs e)
    {
        Int32 filas = 0;
        try
        {
            if (Convert.ToInt32(dcbTiposArchivo.SelectedItem.Value) > 0)
            {
                if (FileUpload1.FileName != "")
                {
                    clsNanoCreditoDetalle eNanoCreditoDetalle = new clsNanoCreditoDetalle();
                    List<clsNanoCreditoDetalle> lstCreditoDetalle = new List<clsNanoCreditoDetalle>();
                    string lsUser = (string)this.Session["IdUsuario"];
                    var nrodocumento = double.MinValue;
                    var lugaremision = double.MinValue;
                    var paisnacimiento = double.MinValue;
                    var telefonoreferencia1 = double.MinValue;
                    var tiporeferenicia1 = double.MinValue;
                    var telefonoreferencia2 = double.MinValue;
                    var tiporeferenicia2 = double.MinValue;
                    var departamentodd = double.MinValue;
                    var ciudadd = double.MinValue;
                    var zona = double.MinValue;
                    var telefonofijo = double.MinValue;
                    var telefonocelular = double.MinValue;
                    var ciudadt = double.MinValue;
                    var zonadt = double.MinValue;
                    var montoingreso = double.MinValue;
                    var montocompra = double.MinValue;
                    var departametodt = double.MinValue;
                    var otrasactividades = double.MinValue;
                    var codigopdv = double.MinValue;


                    var tbl = new System.Data.DataTable();

                    if (FileUpload1.HasFile && Path.GetExtension(FileUpload1.FileName) == ".xlsx")
                    {
                        using (var excel = new ExcelPackage(FileUpload1.PostedFile.InputStream))
                        {

                            var ws = excel.Workbook.Worksheets.First();
                            var hasHeader = true;  // adjust accordingly
                            // add DataColumns to DataTable
                            int i = 0;
                            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                            {
                                i++;
                                tbl.Columns.Add(hasHeader ? "Columna" + i
                                    : String.Format("Column {0}", firstRowCell.Start.Column));
                            }
                            // add DataRows to DataTable
                            int startRow = hasHeader ? 2 : 1;
                            for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                            {
                                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                                DataRow row = tbl.NewRow();
                                foreach (var cell in wsRow)
                                    row[cell.Start.Column - 1] = cell.Text;
                                tbl.Rows.Add(row);
                            }
                        }
                    }

                    bool vacio = false;
                    foreach (DataRow row in tbl.Rows)
                    {
                        if (row[0].ToString() != string.Empty && row[5].ToString() != string.Empty && row[35].ToString() != string.Empty && row[7].ToString() != string.Empty)
                        {

                            //if (row[0].ToString() != string.Empty )
                            //{
                            filas++;
                            //if (validar(row[0]) == 0)
                            //{
                            //    filas = 0;
                            //    txtMessage.Text = "Error en la carga del archivo - Nro Documento no valido - Fila - " + filas;
                            //    ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
                            //    ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
                            //    ASPxPopupControl1.ShowOnPageLoad = true;
                            //    break;
                            //}
                            nrodocumento = validarNroDocumento(row[0]);
                            var extduplicada = Convert.ToString(row[1]);
                            lugaremision = validar(row[2]);
                            var fechaemision = Convert.ToDateTime(row[3]);
                            var fechavecimiento = Convert.ToDateTime(row[4]);
                            var apellidopaterno = Convert.ToString(row[5]);
                            var apellidomaterno = Convert.ToString(row[6]);
                            var primernombre = Convert.ToString(row[7]);
                            var segundonombre = Convert.ToString(row[8]);
                            var fechanacimiento = Convert.ToDateTime(row[9]);
                            paisnacimiento = validar(row[10]);
                            var sexo = Convert.ToString(row[11]);
                            var estadocivil = Convert.ToString(row[12]);
                            var nombrereferencia1 = Convert.ToString(row[13]);
                            telefonoreferencia1 = validar(row[14]);
                            tiporeferenicia1 = validar(row[15]);
                            var nombrereferencia2 = Convert.ToString(row[16]);
                            telefonoreferencia2 = validar(row[17]);
                            tiporeferenicia2 = validar(row[18]);
                            departamentodd = validar(row[19]);
                            ciudadd = validar(row[20]);
                            var barrio = Convert.ToString(row[21]);
                            zona = Convert.ToDouble(row[22]);
                            var calle = Convert.ToString(row[23]);
                            telefonofijo = validar(row[24]);
                            telefonocelular = validar(row[25]);
                            var actividadlaboral = Convert.ToDateTime(row[26]);
                            departametodt = validar(row[27]);
                            ciudadt = validar(row[28]);
                            var barriodt = Convert.ToString(row[29]);
                            zonadt = validar(row[30]);
                            var calledt = Convert.ToString(row[31]);
                            montoingreso = validar(row[32]);
                            montocompra = validar(row[33]);
                            otrasactividades = validar(row[34]);
                            codigopdv = validarNroDocumento(row[35]);
                            var apellidocasada = Convert.ToString(row[36]);
                            var usapellido = Convert.ToString(row[37]);
                            var nombreconyugue = Convert.ToString(row[38]);
                            var ideagente = validar(Convert.ToInt32(row[39]));

                            eNanoCreditoDetalle = new clsNanoCreditoDetalle();
                            eNanoCreditoDetalle.PIntDpNroDocumento = Convert.ToInt32(nrodocumento);
                            eNanoCreditoDetalle.PStrDpExtDuplicada = extduplicada;
                            eNanoCreditoDetalle.PIntDpLugarEmision = Convert.ToInt32(lugaremision);
                            eNanoCreditoDetalle.PDtDpFechaEmision = Convert.ToDateTime(fechaemision);
                            eNanoCreditoDetalle.PDtDpFechaVencimiento = Convert.ToDateTime(fechavecimiento);
                            eNanoCreditoDetalle.PStrDpApellidoPaterno = apellidopaterno;
                            eNanoCreditoDetalle.PStrDpApellidoMaterno = apellidomaterno;
                            eNanoCreditoDetalle.PStrDpPrimerNombre = primernombre;
                            eNanoCreditoDetalle.PStrDpSegundoNombre = segundonombre;
                            eNanoCreditoDetalle.PDtDpFechaNacimiento = fechanacimiento;
                            eNanoCreditoDetalle.PPaisNacimiento = Convert.ToInt32(paisnacimiento);
                            eNanoCreditoDetalle.PStrDpSexo = sexo;
                            eNanoCreditoDetalle.PStrDpEstadoCivil = estadocivil;
                            eNanoCreditoDetalle.PStrNombreReferenciaPer1 = nombrereferencia1;
                            eNanoCreditoDetalle.PStrTelefonoReferencia1 = telefonoreferencia1;
                            eNanoCreditoDetalle.PStrTipoRelacionReferencia1 = tiporeferenicia1;
                            eNanoCreditoDetalle.PStrNombreReferenciaPer2 = nombrereferencia2;
                            eNanoCreditoDetalle.PStrTelefonoReferenciaPer2 = telefonoreferencia2;
                            eNanoCreditoDetalle.PStrTipoRelacionReferencia2 = tiporeferenicia2;
                            eNanoCreditoDetalle.PIntDDDepartamento = Convert.ToInt32(departamentodd);                           
                            eNanoCreditoDetalle.PIntDDCiudad = Convert.ToInt32(ciudadd);
                            eNanoCreditoDetalle.PStrDDBarrio = barrio;
                            eNanoCreditoDetalle.PIntDDZona = Convert.ToInt32(zona);
                            eNanoCreditoDetalle.PStrDDCalle = calle;
                            eNanoCreditoDetalle.PlngDDTelefonoFijo = Convert.ToInt64(telefonofijo);
                            eNanoCreditoDetalle.PlngDDTelefonoCelular = Convert.ToInt64(telefonocelular);
                            eNanoCreditoDetalle.PDtDLFechaInicioLaboral = actividadlaboral;
                            eNanoCreditoDetalle.PIntDTDepartamento = Convert.ToInt32(departametodt);
                            eNanoCreditoDetalle.PIntDTCiudad = Convert.ToInt32(ciudadt);
                            //validar el departamento con la tabla sucursales 
                            //if (EsDptoValido(eNanoCreditoDetalle.PIntDTDepartamento) == false)
                            //{
                            //    txtMessage.Text = "Código de departamento Inexistente   - Verifique el Departamento  - Fila - " + filas;
                            //    ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
                            //    ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
                            //    ASPxPopupControl1.ShowOnPageLoad = true;
                            //    return;
                            //}

                            //validar el departamento con la tabla sucursales 
                            if (EsDptoValido(eNanoCreditoDetalle.PIntDDDepartamento) == false)
                            {
                                txtMessage.Text = " Código de departamento Inexistente. Verifique Fila:  " + filas + " - Departamento :" + Convert.ToString(eNanoCreditoDetalle.PIntDDDepartamento);
                                ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
                                ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
                                ASPxPopupControl1.ShowOnPageLoad = true;
                                return;
                            }

                            eNanoCreditoDetalle.PStrDTBarrio = barriodt;
                            eNanoCreditoDetalle.PIntDTZona = Convert.ToInt32(zonadt);
                            eNanoCreditoDetalle.PStrDTCalle = calledt;
                            eNanoCreditoDetalle.PLngMontoIngreso = Convert.ToInt64(montoingreso);
                            eNanoCreditoDetalle.PLngEngresoCompraMercaderia = Convert.ToInt64(montocompra);
                            eNanoCreditoDetalle.PUsuarioCreacion = lsUser;
                            eNanoCreditoDetalle.PCodigoIPDV = Convert.ToInt64(codigopdv);
                            eNanoCreditoDetalle.POtraActividad = Convert.ToInt32(otrasactividades);
                            eNanoCreditoDetalle.PApellidoCasada = apellidocasada;
                            eNanoCreditoDetalle.PUsaApellido = usapellido;
                            eNanoCreditoDetalle.PNombreConyugue = nombreconyugue;
                            eNanoCreditoDetalle.PIdeAgente = Convert.ToInt32(ideagente);
                            lstCreditoDetalle.Add(eNanoCreditoDetalle);
                            //}
                        }
                        else
                        {
                            if (row[0].ToString() != string.Empty || row[35].ToString() != string.Empty || row[7].ToString() != string.Empty || row[5].ToString() != string.Empty)
                            {
                                filas++;
                                vacio = true;
                                txtMessage.Text = "Error en la carga del archivo - Información Obligatoria Incompleta - Fila - " + filas;
                                ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
                                ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
                                ASPxPopupControl1.ShowOnPageLoad = true;
                            }
                        }
                    }
                    ///***********************************************************************   
                    if (vacio == false)
                    {
                        clsNanoCredito eNanoCredito = new clsNanoCredito();
                        clsNanoCreditoNeg lNanoCredito = new clsNanoCreditoNeg();
                        try
                        {
                            if (filas > 0)
                            {
                                eNanoCredito.PDtFechaProceso = Convert.ToDateTime(FileUpload1.FileName.Substring(0, 10).Replace('_', '/'));

                                //eNanoCredito.PDtFechaProceso = DateTime.Now;
                                eNanoCredito.PIntNroFilas = filas;
                                eNanoCredito.PStrEstado = "1";
                                eNanoCredito.PStrNombreArchivo = FileUpload1.FileName;
                                eNanoCredito.PStrUsuarioCreacion = lsUser;
                                eNanoCredito.LstCreditoDetalle = lstCreditoDetalle;
                                string msg_error = string.Empty;
                                if (lNanoCredito.InsertarNanoCredito(eNanoCredito, ref msg_error))
                                {
                                    //filas = 0;
                                    txtMessage.Text = "Carga de Archivo Correcto";
                                    ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
                                    ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
                                    ASPxPopupControl1.ShowOnPageLoad = true;
                                }
                                else
                                {
                                    //filas = 0;
                                    txtMessage.Text = "Error en la carga del archivo - " + msg_error;
                                    ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
                                    ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
                                    ASPxPopupControl1.ShowOnPageLoad = true;
                                }
                            }
                            else
                            {
                                if (vacio)
                                {
                                    txtMessage.Text = "Error en la carga del archivo - Nro Documento no valido - Fila - " + filas;
                                    ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
                                    ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
                                    ASPxPopupControl1.ShowOnPageLoad = true;
                                    vacio = false;
                                }
                                else
                                {
                                    //filas = 0;
                                    txtMessage.Text = "No Existen filas en el archivo";
                                    ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
                                    ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
                                    ASPxPopupControl1.ShowOnPageLoad = true;
                                }
                            }
                        }
                        catch
                        {
                            txtMessage.Text = "Error en la carga del archivo";
                            ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
                            ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
                            ASPxPopupControl1.ShowOnPageLoad = true;
                        }
                    }
                }
                else
                {
                    txtMessage.Text = "Debe seleccionar el archivo";
                    ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
                    ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
                    ASPxPopupControl1.ShowOnPageLoad = true;
                }
            }
            else
            {
                txtMessage.Text = "Debe seleccionar el tipo de archivo";
                ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
                ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
                ASPxPopupControl1.ShowOnPageLoad = true;
            }

        }
        catch (Exception ex)
        {
            //txtMessage.Text = "Error en la carga del archivo";
            txtMessage.Text = "Error en la carga del archivo - Fila - " + filas+ ex.Message;
            ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
            ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
            ASPxPopupControl1.ShowOnPageLoad = true;
        }
    }
    protected void FileUpload1_Unload(object sender, EventArgs e)
    {

    }
    protected void btnShowPopup_Click(object sender, EventArgs e)
    {
        //txtPopup.Text = "Mauricio Molina";
        ASPxPopupControl1.ShowOnPageLoad = true;
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // TODO: your code is here to process the popup window's data at the server
        //txtMain.Text = txtPopup.Text;
        ASPxPopupControl1.ShowOnPageLoad = false;
    }

    bool EsDptoValido(int pIntCiuId )
    {
        bool LBolEsValido = false;
        string lStrSql ="";
        int lintCntReg =0;
        try
        {
            lStrSql = "SELECT  count(1) FROM GANADERO.SUCURSALES WHERE CODDEPARTAMENTO =" + Convert.ToString(pIntCiuId); 
            System.Data.DataTable dtRetorno = clsUtilNeg.mtdEjecutarConsulta(lStrSql);
            if( dtRetorno.Rows.Count ==0  )
            {
                LBolEsValido=false;
                return LBolEsValido;
            }

           
            if (dtRetorno.Rows.Count > 0)
            {
                 
                foreach (DataRow row in dtRetorno.Rows)
                {
                      
                    lintCntReg=  Convert.ToInt32(row[0].ToString());  
                    if (lintCntReg >0)  
                    {
                        LBolEsValido=true;
                    }

                }

            }
                
        }
        catch (Exception ex)
        {
            LBolEsValido = false;
        }
       

       return LBolEsValido;
     
    }
}