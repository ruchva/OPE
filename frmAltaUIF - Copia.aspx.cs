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
using System.Runtime.InteropServices;
using System.Configuration;
using System.Collections;

public partial class UI_OPE_frmAltaUIF : System.Web.UI.Page
{
    public static string sEstadoProcesado = "N";
    protected void Page_Load(object sender, EventArgs e)
    {
        AddJavascript("../../JS/OPE/jsCargaArchivosOperaciones.js");
        if (!IsPostBack)
        {
            List<string> lstCabecera = new List<string>();
            lstCabecera = new List<string>();
            lstCabecera.Add("SUCURSAL: " + this.Context.Session["sesIdSucursal"].ToString() + "   REPORTE ARCHIVOS NANOCREDITOS:     " + "       FECHA: " + DateTime.Now.ToShortDateString().ToString());
            lstCabecera.Add("USUARIO: " + HttpContext.Current.User.Identity.Name.ToString() + "         DESDE FECHA: " + "-" + "    HASTA FECHA: " + "-" + "      HORA: " + DateTime.Now.ToShortTimeString().ToString());
            dtxtFechaDesde.Text = DateTime.Now.ToShortDateString();
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
        if (!IsPostBack)
        {
            this.Context.Session.Remove("lstReposicion");
            this.Context.Session.Remove("lstLimites");
            if (dcbTiposArchivo != null)
                mtdCargarGridVista(dcbTiposArchivo.SelectedItem.Value.ToString());
        }
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
        string sValor = string.Empty;
        string lsSelected = (string)dhddTipoArchivo["idSelected"];
        if (lsSelected == "RE")
        {
            e.CallbackData = mtdGuardarArchivo2(e.UploadedFile, lsSelected, sValor);
        }
    }
    private string mtdGuardarArchivo2(UploadedFile uploadedFile, string psSelected, string pFecha)
    {
        return "";
        //string lsRespuesta = "false~RE";
        //try
        //{
        //    if (!uploadedFile.IsValid)
        //        return string.Empty;
        //    byte[] luArray = uploadedFile.FileBytes.ToArray();
        //    string lsFileName = uploadedFile.PostedFile.FileName;

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
        //    var otrosactivos = double.MinValue;
        //    var codpdv = double.MinValue;
        //    for (int i = 2; i <= rw; i++)
        //    {
        //        if ((xlWorkSheet.Cells[i, 1] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 1] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        {
        //            nrodocumento = 0;
        //        }
        //        else
        //        {
        //            nrodocumento = (double)(xlWorkSheet.Cells[i, 1] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        }

        //        var extduplicada = (string)(xlWorkSheet.Cells[i, 2] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
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
        //        if ((xlWorkSheet.Cells[i, 11] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 11] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        {
        //            paisnacimiento = 0;
        //        }
        //        else
        //        { paisnacimiento = (double)(xlWorkSheet.Cells[i, 11] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        var sexo = (string)(xlWorkSheet.Cells[i, 12] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        var estadocivil = (string)(xlWorkSheet.Cells[i, 13] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        var nombrereferencia1 = (string)(xlWorkSheet.Cells[i, 14] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 15] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 15] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { telefonoreferencia1 = 0; }
        //        else { telefonoreferencia1 = (double)(xlWorkSheet.Cells[i, 15] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        if ((xlWorkSheet.Cells[i, 16] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull)
        //        {
        //            tiporeferenicia1 = 0;
        //        }
        //        else
        //        { tiporeferenicia1 = (double)(xlWorkSheet.Cells[i, 16] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        var nombrereferencia2 = (string)(xlWorkSheet.Cells[i, 17] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 18] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 18] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { telefonoreferencia2 = 0; }
        //        else { telefonoreferencia2 = (double)(xlWorkSheet.Cells[i, 18] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var telefonoreferencia2 = (double)(xlWorkSheet.Cells[i, 18] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 19] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 19] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { tiporeferenicia2 = 0; }
        //        else
        //        { tiporeferenicia2 = (double)(xlWorkSheet.Cells[i, 19] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var tiporeferenicia2 = (double)(xlWorkSheet.Cells[i, 19] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 20] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 20] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { departamentodd = 0; }
        //        else { departamentodd = (double)(xlWorkSheet.Cells[i, 20] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var departamentodd = (double)(xlWorkSheet.Cells[i, 20] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 21] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 21] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { ciudadd = 0; }
        //        else { ciudadd = (double)(xlWorkSheet.Cells[i, 21] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var ciudadd = (double)(xlWorkSheet.Cells[i, 21] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        var barrio = (string)(xlWorkSheet.Cells[i, 22] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 23] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 23] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { zona = 0; }
        //        else { zona = (double)(xlWorkSheet.Cells[i, 23] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var zona = (double)(xlWorkSheet.Cells[i, 23] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        var calle = (string)(xlWorkSheet.Cells[i, 24] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 25] as Microsoft.Office.Interop.Excel.Range).Value2 == null || (xlWorkSheet.Cells[i, 25] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { telefonofijo = 0; }
        //        else { telefonofijo = (double)(xlWorkSheet.Cells[i, 25] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var telefonofijo = (double)(xlWorkSheet.Cells[i, 25] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        //var telefonocelular = (double)(xlWorkSheet.Cells[i, 26] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 26] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 26] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { telefonocelular = 0; }
        //        else { telefonocelular = (double)(xlWorkSheet.Cells[i, 26] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        var actividadlaboral = (DateTime)(xlWorkSheet.Cells[i, 27] as Microsoft.Office.Interop.Excel.Range).Value;
        //        //var departametodt = (double)(xlWorkSheet.Cells[i, 28] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 28] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 28] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { departametodt = 0; }
        //        else { departametodt = (double)(xlWorkSheet.Cells[i, 28] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var ciudadt = (double)(xlWorkSheet.Cells[i, 29] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 29] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 29] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { ciudadt = 0; }
        //        else { ciudadt = (double)(xlWorkSheet.Cells[i, 29] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        var barriodt = (string)(xlWorkSheet.Cells[i, 30] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        //var zonadt = (double)(xlWorkSheet.Cells[i, 31] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 31] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 31] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { zonadt = 0; }
        //        else { zonadt = (double)(xlWorkSheet.Cells[i, 31] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        var calledt = (string)(xlWorkSheet.Cells[i, 32] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        //var montoingreso = (double)(xlWorkSheet.Cells[i, 33] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 33] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 33] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { montoingreso = 0; }
        //        else { montoingreso = (double)(xlWorkSheet.Cells[i, 31] as Microsoft.Office.Interop.Excel.Range).Value2; }
        //        //var montocompra = (double)(xlWorkSheet.Cells[i, 34] as Microsoft.Office.Interop.Excel.Range).Value2;
        //        if ((xlWorkSheet.Cells[i, 34] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 34] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { montocompra = 0; }
        //        else { montocompra = (double)(xlWorkSheet.Cells[i, 34] as Microsoft.Office.Interop.Excel.Range).Value2; }

        //        if ((xlWorkSheet.Cells[i, 35] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 35] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { otrosactivos = 0; }
        //        else { otrosactivos = (double)(xlWorkSheet.Cells[i, 35] as Microsoft.Office.Interop.Excel.Range).Value2; }

        //        if ((xlWorkSheet.Cells[i, 36] as Microsoft.Office.Interop.Excel.Range).Value2 is DBNull || (xlWorkSheet.Cells[i, 36] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
        //        { codpdv = 0; }
        //        else { codpdv = (double)(xlWorkSheet.Cells[i, 35] as Microsoft.Office.Interop.Excel.Range).Value2; }

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
        //    lNanoCredito.InsertarNanoCredito(eNanoCredito);
        //}
        //catch (Exception ex)
        //{
        //    // clsTextLogs.WriteError("ERROR", ex);
        //    return "false~" + "Ocurrió un problema al procesar la información.";
        //}
        //return lsRespuesta;
    }
    //private string mtdTraerStringIso(string pValor)
    //{
    //    Encoding eIso = Encoding.GetEncoding("ISO-8859-1");
    //    Encoding eUtf8 = Encoding.UTF8;
    //    byte[] utfBytes = eUtf8.GetBytes(pValor);
    //    byte[] isoBytes = Encoding.Convert(eUtf8, eIso, utfBytes);
    //    return eIso.GetString(isoBytes);
    //}


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
        //grilla
        clsNanoCreditoNeg objNanoCredito = new clsNanoCreditoNeg();
        rptCliente.DataSource = objNanoCredito.ConsultarNanoCredito(DateTime.Now);
        rptCliente.DataBind();
    }

    #endregion


    protected void cbAll_Init(object sender, EventArgs e)
    {
        ASPxCheckBox chk = sender as ASPxCheckBox;
        ASPxGridView grid = (chk.NamingContainer as GridViewHeaderTemplateContainer).Grid;
        chk.Checked = (grid.Selection.Count == grid.VisibleRowCount);
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
    protected void dcllRegistro_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
    }
    protected void rptCliente_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            return;
        RadioButton rdo = (RadioButton)e.Item.FindControl("rdbSeleccionar");
        string script = "SetUniqueRadioButton('rptCliente.*rbGrupo',this)";
        rdo.Attributes.Add("onclick", script);
    }
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        if (rptCliente.Items.Count > 0)
        {
            clsNanoCreditoNeg objNanoCredito = new clsNanoCreditoNeg();
            Int32 CODCABNANOCREDITO = 0;
            DateTime FECHAPROCESO;
            string NOMBREARCHIVO;
            foreach (RepeaterItem item in this.rptCliente.Items)
            {
                RadioButton rd = (RadioButton)item.FindControl("rdbSeleccionar");
                if (rd.Checked)
                {
                    Label lbl = (Label)item.FindControl("lblCodigoPersona");
                    CODCABNANOCREDITO = Convert.ToInt32(lbl.Text);

                    Label lblCodMoneda = (Label)item.FindControl("lblFechaProceso");
                    FECHAPROCESO = Convert.ToDateTime(lblCodMoneda.Text);

                    Label lblNroCuenta = (Label)item.FindControl("lblNombreArchivo");
                    NOMBREARCHIVO = lblNroCuenta.Text;
                }
            }
            if (CODCABNANOCREDITO > 0)
            {
                string pusuario = (string)this.Session["IdUsuario"];
                ArrayList _lstRespuesta = objNanoCredito.RealizarPagoNanoCreditoUIF(CODCABNANOCREDITO, pusuario);
                rptCliente.DataSource = objNanoCredito.ConsultarNanoCreditoUIF(Convert.ToDateTime(dtxtFechaDesde.Text));
                rptCliente.DataBind();
                if (_lstRespuesta[0].ToString() == "2")
                {
                    txtMessage.Text = "Proceso Concluido con Observaciones. Favor revisar reporte de estado";
                    ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
                    ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
                    ASPxPopupControl1.ShowOnPageLoad = true;
                }
                else
                {
                    if (_lstRespuesta[0].ToString() == "1")
                    {
                        txtMessage.Text = "Proceso Concluido Correctamente";
                        ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
                        ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
                        ASPxPopupControl1.ShowOnPageLoad = true;
                    }
                    else
                    {
                        txtMessage.Text = "Error en el proceso";
                        ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
                        ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
                        ASPxPopupControl1.ShowOnPageLoad = true;
                    }
                }
            }
            else
            {
                txtMessage.Text = "Debe seleccionar un registro para procesar";
                ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
                ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
                ASPxPopupControl1.ShowOnPageLoad = true;
            }
        }
        else
        {
            txtMessage.Text = "No hay datos para procesar";
            ASPxPopupControl1.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#337447");
            ASPxPopupControl1.HeaderText = "Mensaje de Confirmación";
            ASPxPopupControl1.ShowOnPageLoad = true;
        }
    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        clsNanoCreditoNeg objNanoCredito = new clsNanoCreditoNeg();
        rptCliente.DataSource = objNanoCredito.ConsultarNanoCreditoUIF(Convert.ToDateTime(dtxtFechaDesde.Text));
        rptCliente.DataBind();
    }
}