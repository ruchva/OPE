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
using System.Text.RegularExpressions;

public partial class UI_OPE_frmCargaArchivosOperaciones : System.Web.UI.Page
{
    public static string sEstadoProcesado = "N";
    protected void Page_Load(object sender, EventArgs e)
    {
        AddJavascript("../../JS/OPE/jsCargaArchivosOperaciones.js");
        if (!IsPostBack)
        {
            List<string> lstCabecera = new List<string>();
            wucExportarGrid2.userControlClick += new UserControlDelegate(UserControlDemo_userControlClick);
            wucExportarGrid2.dpveControlExportar = dgveGrid;
            wucExportarGrid2.sTitulo = dlblTitulo.Text;
            lstCabecera = new List<string>();
            lstCabecera.Add("SUCURSAL: " + this.Context.Session["sesIdSucursal"].ToString() + "   REPORTE ARCHIVOS RETENCIONES:     " + "       FECHA: " + DateTime.Now.ToShortDateString().ToString());
            lstCabecera.Add("USUARIO: " + HttpContext.Current.User.Identity.Name.ToString() + "         DESDE FECHA: " + "-" + "    HASTA FECHA: " + "-" + "      HORA: " + DateTime.Now.ToShortTimeString().ToString());
            wucExportarGrid2.lstCabecera = lstCabecera;
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (dcbTiposArchivo.SelectedItem.Value.ToString() == "RE")
        {
            mtdCargarGridRetenciones(dcbTiposArchivo.SelectedItem.Value.ToString());
        }
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

        string lsRespuesta = "false~RE";

        try
        {
            if (!uploadedFile.IsValid)
                return string.Empty;
            byte[] luArray = uploadedFile.FileBytes.ToArray();
            string lsFileName = uploadedFile.FileName;

            string lsUser = (string)this.Session["IdUsuario"];
            if (psSelected == "RE")
            {
                if (lsFileName.Substring(0, 4).Equals("SIN_") || lsFileName.Substring(0, 3).Equals("AN_"))
                {
                    //Encoding.GetEncoding("ISO-8859-1");
                    string sValor = Encoding.UTF8.GetString(luArray);
                    string[] sLines = sValor.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                    List<string> lsArchivo = sLines.ToList<string>();
                    List<clsAsf_RetencionSolicitud> lstRetenciones = new List<clsAsf_RetencionSolicitud>();
                    clsAsf_RetencionSolicitud objRetencion = new clsAsf_RetencionSolicitud();
                    clsOperacionesNeg objReten = new clsOperacionesNeg();

                    if (objReten.mtdExisteArchivo(lsFileName.Replace(".txt", "")).Count == 0)
                    {
                        foreach (string sFilas in lsArchivo)
                        {
                            if (sFilas != "")
                            {
                                string[] sCampos = sFilas.Split('|');
                                objRetencion = new clsAsf_RetencionSolicitud();
                                objRetencion.FECHA_CIRCULAR_ASFI = Convert.ToDateTime(mtdTraerStringIso(sCampos[0]).Replace("?", ""));
                                objRetencion.NUM_CIRCULAR_ASFI = sCampos[1];
                                objRetencion.MOTIVO = Convert.ToInt16(sCampos[2]);
                                objRetencion.NUM_CITE = sCampos[3];
                                objRetencion.NOMBRE_RESPUESTA_CARTA = sCampos[4];
                                objRetencion.DEMANDANTE = sCampos[5];
                                objRetencion.CARGO_AUTORIDAD = sCampos[6];
                                objRetencion.PRIMER_APELLIDO_DEMANDADO = sCampos[7];
                                objRetencion.SEGUNDO_APELLIDO_DEMANDADO = sCampos[8];
                                objRetencion.NOMBRE_DEMANDADO = sCampos[9];
                                objRetencion.EMPRESA_DEMANDADA = Regex.Replace(sCampos[10], @"\s+", " ").Trim();
                                if (objRetencion.EMPRESA_DEMANDADA.Length > 100)
                                {
                                    objRetencion.EMPRESA_DEMANDADA = objRetencion.EMPRESA_DEMANDADA.Substring(0,100);
                                }
                                objRetencion.NUMERO_DOC = string.IsNullOrEmpty(sCampos[11]) ? " " : sCampos[11];                               
                                objRetencion.EXTENSION = string.IsNullOrEmpty(sCampos[12]) ? " " : sCampos[12];
                                if (objRetencion.EXTENSION.Equals(" "))
                                    objRetencion.TIPO_DOC = "NIT";
                                else
                                    objRetencion.TIPO_DOC = "CI";
                                objRetencion.TIPO_PERSONA = Convert.ToInt16(sCampos[13]);
                                objRetencion.IMPORTE_SOLICITUD = Convert.ToDecimal(string.IsNullOrEmpty(sCampos[14]) ? "0" : sCampos[14]);
                                objRetencion.IMPORTE_SOLICITUD_UFV = Convert.ToDecimal(string.IsNullOrEmpty(sCampos[15]) ? "0" : sCampos[15]);
                                objRetencion.NUM_PROC_PIET = sCampos[16];
                                objRetencion.REFERENCIA_SOLICITANTE = Convert.ToInt16(sCampos[17]);
                                objRetencion.NUM_DOC_RESPALDO = sCampos[18];
                                objRetencion.NUM_DOC_ORIGEN = sCampos[19];
                                if ((objRetencion.IMPORTE_SOLICITUD > 0 && objRetencion.IMPORTE_SOLICITUD_UFV > 0) ||
                                    (objRetencion.IMPORTE_SOLICITUD > 0 && objRetencion.IMPORTE_SOLICITUD_UFV == 0))
                                {
                                    objRetencion.MONEDA_SOLICITUD = 0;
                                }                                    
                                else
                                {
                                    objRetencion.MONEDA_SOLICITUD = 9800;
                                    objRetencion.IMPORTE_SOLICITUD = objRetencion.IMPORTE_SOLICITUD_UFV;
                                }
                                                                   
                                objRetencion.ADICIONADO_POR = HttpContext.Current.User.Identity.Name.ToString();
                                objRetencion.FECHA_INGRESO = DateTime.Now;
                                objRetencion.TZ_LOCK = 0;
                                objRetencion.PROCESADO = sEstadoProcesado;
                                objRetencion.NOM_ARCHIVO = lsFileName.Replace(".txt", "");
                                lstRetenciones.Add(objRetencion);
                            }
                        }
                        bool lbResult = clsOperacionesNeg.mtdInsertar(lstRetenciones, "9999");
                        if (!lbResult)
                            lsRespuesta = "false~" + "Ocurrió un problema";
                        else
                            lsRespuesta = "true~RE";
                    }
                    else
                        lsRespuesta = "false~" + "El archivo ya se cargó anteriormente";
                }
                else
                    lsRespuesta = "false~" + "Formato del archivo incorrecto";
            }
        }
        catch (Exception ex)
        {
            // clsTextLogs.WriteError("ERROR", ex);
            return "false~" + "Ocurrió un problema al procesar la información.";
        }   
        return lsRespuesta;
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
        clsOperacionesNeg objOperaciones = new clsOperacionesNeg();
        clsAsf_RetencionSolicitud objRepo = new clsAsf_RetencionSolicitud();
        List<BG.SIA.ESTRUCTURA.clsAsf_RetencionSolicitud> lstReposiciones = objOperaciones.mtdTraerRetenciones();
        dgrvArchivosPrevios.DataSource = lstReposiciones;
        dgrvArchivosPrevios.KeyFieldName = "ID_RETENCIONES";
        dgrvArchivosPrevios.DataBind();
    }
   
    #endregion


    protected void cbAll_Init(object sender, EventArgs e)
    {
        ASPxCheckBox chk = sender as ASPxCheckBox;
        ASPxGridView grid = (chk.NamingContainer as GridViewHeaderTemplateContainer).Grid;
        chk.Checked = (grid.Selection.Count == grid.VisibleRowCount);
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
        if (dcbTiposArchivo.SelectedItem.Value.ToString() == "RE")
        {
            mtdCargarGridRetenciones(dcbTiposArchivo.SelectedItem.Value.ToString());
        }
    }
   
    #endregion
    protected void dgrvArchivosPrevios_BeforeGetCallbackResult(object sender, EventArgs e)
    {
        if (dgrvArchivosPrevios.VisibleRowCount > 0)
        {
           (dgrvArchivosPrevios.Columns["FECHA_INGRESO"] as GridViewDataColumn).GroupBy();
           dgrvArchivosPrevios.SortBy((dgrvArchivosPrevios.Columns["FECHA_INGRESO"] as GridViewDataColumn), DevExpress.Data.ColumnSortOrder.Descending);
        }
    }
}