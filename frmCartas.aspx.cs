using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using BG.SIA.NEGOCIO;
using Microsoft.Reporting.WebForms;
using System.Data;

public partial class UI_COT_frmCartas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtxtFechaDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
            dtxtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    protected void dgrvRetenciones_Load(object sender, EventArgs e)
    {
        mtdCargarGrid();
    }
    void mtdCargarGrid()
    {
        try
        {
            if (dtxtFechaDesde.Value != null && dtxtFechaHasta.Value != null)
            {
                clsOperacionesNeg objNegocio = new clsOperacionesNeg();
                string sCite = string.Empty;
                if (dtxtCite.Text != null)
                    sCite = dtxtCite.Text.ToString();
                dgrvRetenciones.DataSource = objNegocio.mtdTraerRetenciones(Convert.ToDateTime(dtxtFechaDesde.Value.ToString()), Convert.ToDateTime(dtxtFechaHasta.Value.ToString()), sCite);
                dgrvRetenciones.DataBind();
            }
        }
        catch (Exception error)
        {
            throw (error);
        }
    }
    protected void dbtnDocDownloadAction_Click(object sender, EventArgs e)
    {
        try
        {
            if (dcbTipo.SelectedItem.Value.ToString() == "NC")
            {
                string sIdRetencion = dhddVariable["pIdRetenciones"].ToString();
                clsOperacionesNeg objOpe = new clsOperacionesNeg();
                System.Data.DataTable dtReturn = objOpe.mtdTraerRetenciones(sIdRetencion);
                mtdGenerarWord(dtReturn);
                //HACER QUE ACEPTE FORMATO NO VALIDO
                //clsWebUtil.mtdCrearArchivoYDescargar(Response, clsGarantiasNeg.mtdRetornarNombreArchivo(Convert.ToDateTime(sFecha).Year.ToString(), Convert.ToDateTime(sFecha).ToString("dd/MM/yyyy").Substring(3, 2), Convert.ToDateTime(sFecha).Day.ToString(), sTipo), sTipo, clsGarantiasNeg.sSiglaEntidadFinanciera, lstContenido);
            }
        }
        catch (Exception error)
        {

        }
    }
    private void mtdGenerarWord(System.Data.DataTable pTabla)
    {
        var app = new Application();
        string sCiteNombreDocx = pTabla.Rows[0][0].ToString();
        string sTipo = pTabla.Rows[0][1].ToString();
        string sFechaCarta = pTabla.Rows[0][2].ToString();
        string sFechaCarta_Espanol = pTabla.Rows[0][3].ToString();
        string sCite = pTabla.Rows[0][4].ToString();
        string sNombreRespuestaCarta = pTabla.Rows[0][5].ToString();
        string sCargoAutoridad = pTabla.Rows[0][6].ToString();
        string sDepartamento = pTabla.Rows[0][7].ToString();
        string sServicio = pTabla.Rows[0][8].ToString();        
        string sMotivo = pTabla.Rows[0][9].ToString();
        string sNumeroCircularAsfi = pTabla.Rows[0][10].ToString();
        string sFechaCircularAsfi = pTabla.Rows[0][11].ToString();
        string sFechaCircularAsfiEspanol = pTabla.Rows[0][12].ToString();  
        string sNombreProceso = pTabla.Rows[0][13].ToString();
        string sCodigoConcatenado = pTabla.Rows[0][17].ToString();        
        string sDemandante = pTabla.Rows[0][18].ToString();                      
        string sDemandado = pTabla.Rows[0][19].ToString();
        string sNombre = pTabla.Rows[0][20].ToString();
        string sNumeroDocumento = pTabla.Rows[0][21].ToString();
        string sUsuario = pTabla.Rows[0][22].ToString();
        string sDireccion = pTabla.Rows[0][25].ToString();
        string sJuzgado = pTabla.Rows[0][26].ToString();
                                
        try
        {
            //This code creates a document based on the specified template.
            var doc = app.Documents.Add(HttpContext.Current.Server.MapPath("~") + @"UPLOAD\DOC\Mod1.dotx", Visible: false);
            doc.Activate();

            //do this for each keyword you want to replace.
            var sel = app.Selection;
            //FECHA CARTA
            sel.Find.Text = "[fecha_carta]";
            sel.Find.Replacement.Text = sFechaCarta_Espanol;
            sel.Find.Wrap = WdFindWrap.wdFindContinue;
            sel.Find.Forward = true;
            sel.Find.Format = false;
            sel.Find.MatchCase = false;
            sel.Find.MatchWholeWord = false;
            sel.Find.Execute(Replace: WdReplace.wdReplaceAll);
            //CITE
            sel.Find.Text = "[cite]";
            sel.Find.Replacement.Text = sCite;
            sel.Find.Wrap = WdFindWrap.wdFindContinue;
            sel.Find.Forward = true;
            sel.Find.Format = false;
            sel.Find.MatchCase = false;
            sel.Find.MatchWholeWord = false;
            sel.Find.Execute(Replace: WdReplace.wdReplaceAll);
            //NOMBRE //CARGO //JUZGADO //SERVICIO //CIUDAD
            if (!string.IsNullOrEmpty(sCargoAutoridad))
                sNombreRespuestaCarta = sNombreRespuestaCarta + "\n\r" + sCargoAutoridad;
            if (!string.IsNullOrEmpty(sJuzgado))
                sNombreRespuestaCarta = sNombreRespuestaCarta + "\n\r" + sJuzgado;
            sServicio = (sTipo == "M" ? sDireccion : sServicio);
            if (!string.IsNullOrEmpty(sServicio))
                sNombreRespuestaCarta = sNombreRespuestaCarta + "\n\r" + sServicio;
            if (!string.IsNullOrEmpty(sDepartamento))
                sNombreRespuestaCarta = sNombreRespuestaCarta + "\n\r" + sDepartamento;
            sel.Find.Text = "[nombre_cargo_juzagado_servicio_ciudad]";
            sel.Find.Replacement.Text = sNombreRespuestaCarta;
            sel.Find.Wrap = WdFindWrap.wdFindContinue;
            sel.Find.Forward = true;
            sel.Find.Format = false;
            sel.Find.MatchCase = false;
            sel.Find.MatchWholeWord = false;
            sel.Find.Execute(Replace: WdReplace.wdReplaceAll);
            //MOTIVO
            sel.Find.Text = "[MOTIVO]";
            sel.Find.Replacement.Text = sMotivo;
            sel.Find.Wrap = WdFindWrap.wdFindContinue;
            sel.Find.Forward = true;
            sel.Find.Format = false;
            sel.Find.MatchCase = false;
            sel.Find.MatchWholeWord = false;
            sel.Find.Execute(Replace: WdReplace.wdReplaceAll);
            //NUMERO CIRCULAR
            sel.Find.Text = "[circular]";
            sel.Find.Replacement.Text = sNumeroCircularAsfi;
            sel.Find.Wrap = WdFindWrap.wdFindContinue;
            sel.Find.Forward = true;
            sel.Find.Format = false;
            sel.Find.MatchCase = false;
            sel.Find.MatchWholeWord = false;
            sel.Find.Execute(Replace: WdReplace.wdReplaceAll);
            //FECHA CIRCULAR
            sel.Find.Text = "[fecha_circular]";
            sel.Find.Replacement.Text = Convert.ToDateTime(sFechaCircularAsfi).ToString("dd MMMM", new CultureInfo("ES"));
            sel.Find.Wrap = WdFindWrap.wdFindContinue;
            sel.Find.Forward = true;
            sel.Find.Format = false;
            sel.Find.MatchCase = false;
            sel.Find.MatchWholeWord = false;
            sel.Find.Execute(Replace: WdReplace.wdReplaceAll);
            //NUMERO PROCESO
            sel.Find.Text = "[proceso]";
            sel.Find.Replacement.Text = (sTipo == "M" ? sNombreProceso : "de Ejecución Tributaria");
            sel.Find.Wrap = WdFindWrap.wdFindContinue;
            sel.Find.Forward = true;
            sel.Find.Format = false;
            sel.Find.MatchCase = false;
            sel.Find.MatchWholeWord = false;
            sel.Find.Execute(Replace: WdReplace.wdReplaceAll);
            //CODIGO CONCATENADO
            sel.Find.Text = "[proceso_concatenado]";
            sel.Find.Replacement.Text = sCodigoConcatenado;
            sel.Find.Wrap = WdFindWrap.wdFindContinue;
            sel.Find.Forward = true;
            sel.Find.Format = false;
            sel.Find.MatchCase = false;
            sel.Find.MatchWholeWord = false;
            sel.Find.Execute(Replace: WdReplace.wdReplaceAll);
            //DEMANDANTE
            sel.Find.Text = "[demandante]";
            sel.Find.Replacement.Text = sDemandante;
            sel.Find.Wrap = WdFindWrap.wdFindContinue;
            sel.Find.Forward = true;
            sel.Find.Format = false;
            sel.Find.MatchCase = false;
            sel.Find.MatchWholeWord = false;
            sel.Find.Execute(Replace: WdReplace.wdReplaceAll);
            //NOMBRE DEMANDADO
            sel.Find.Text = "[demandado]";
            sel.Find.Replacement.Text = (sTipo == "M" ? sDemandado : "del/los contribuyente(s) mencionado(s) línea(s) abajo");
            sel.Find.Wrap = WdFindWrap.wdFindContinue;
            sel.Find.Forward = true;
            sel.Find.Format = false;
            sel.Find.MatchCase = false;
            sel.Find.MatchWholeWord = false;
            sel.Find.Execute(Replace: WdReplace.wdReplaceAll);
            //NOMBRE PERSONA
            string sNombre_NumDoc = "";
            foreach (DataRow row in pTabla.Rows) {
                sNombre_NumDoc = sNombre_NumDoc + "        " + "- " + row[20] + "\t" + row[21] + "\n\r";
            }
            sel.Find.Text = "[nombre_demandado]";
            sel.Find.Replacement.Text = sNombre_NumDoc;
            sel.Find.Wrap = WdFindWrap.wdFindContinue;
            sel.Find.Forward = true;
            sel.Find.Format = false;
            sel.Find.MatchCase = false;
            sel.Find.MatchWholeWord = false;
            sel.Find.Execute(Replace: WdReplace.wdReplaceAll);

            //USUARIO PIE PAGINA
            foreach (Microsoft.Office.Interop.Word.Section wordSection in sel.Sections)
            {
                foreach (Microsoft.Office.Interop.Word.HeaderFooter wordFooter in wordSection.Footers)
                {
                    Microsoft.Office.Interop.Word.Range docRange = wordFooter.Range;

                    docRange.Find.ClearFormatting();
                    docRange.Find.Text = "[usuario]";
                    docRange.Find.Replacement.ClearFormatting();
                    docRange.Find.Replacement.Text = sUsuario;

                    object replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    docRange.Find.Execute(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                              ref replaceAll, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
            }

            //************************************************
            doc.SaveAs(HttpContext.Current.Server.MapPath("~") + @"TMP\" + sCiteNombreDocx + ".docx");
            this.Context.Session["sesCite"] = sCiteNombreDocx;
            //doc.SaveAs(@"F:\Docs\foo.docx");
            app.Quit();
            doc.Close();
            //Response.Redirect(HttpContext.Current.Server.MapPath("~") + @"UPLOAD\DOC\Mod1.docx");
        }
        finally
        {
            //SUPER IMPORTANT!
            //If you don't do this, each time you run the code 
            //the winword.exe process will keep running on background (for ever!),
            //at 10MB a piece, you may end up with a huge memory leak.
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition",
                               "attachment; filename=" + this.Context.Session["sesCite"].ToString() + ".docx;");
            response.TransmitFile(HttpContext.Current.Server.MapPath("~") + @"TMP\" + this.Context.Session["sesCite"].ToString() + ".docx");
            //response.Flush();
            //response.Close();
            response.End();
            app.Quit();
            //Response.Redirect(HttpContext.Current.Server.MapPath("~") + @"UPLOAD\DOC\Mod1.docx");
            //sys.Runtime.InteropServices.Marshal.FinalReleaseComObject(app);
            //
        }
    }
    protected void dbtnImprimirTodos_Click(object sender, EventArgs e)
    {
        rptVisor.Visible = true;
        dvGrid.Visible = false;
        mtdCargarReporte();
    }
    void mtdCargarReporte()
    {
        try
        {
            if (dtxtFechaDesde.Value != null && dtxtFechaHasta.Value != null)
            {
                clsOperacionesNeg objNegocio = new clsOperacionesNeg();
                string sCite = string.Empty;
                if (dtxtCite.Text != null)
                    sCite = dtxtCite.Text.ToString();
                System.Data.DataTable dtResultado = objNegocio.mtdTraerRetenciones(Convert.ToDateTime(dtxtFechaDesde.Value.ToString()), Convert.ToDateTime(dtxtFechaHasta.Value.ToString()), sCite);
                if (dtResultado.Rows.Count > 0)
                {
                    foreach (DataRow rFila in dtResultado.Rows)
                    {
                        rFila["CONCATENADO1"] = rFila["CONCATENADO1"].ToString().Replace("Ejecuci¿n", "Ejecución");
                        rFila["CONCATENADO2"] = rFila["CONCATENADO2"].ToString().Replace("l¿neas", "líneas");
                    }
                    rptVisor.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                    //rptVisor.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    rptVisor.LocalReport.ReportPath = Server.MapPath("rdCartas.rdlc");
                    ReportDataSource datasource = new ReportDataSource("DataSet1", dtResultado);

                    rptVisor.LocalReport.DataSources.Clear();
                    rptVisor.LocalReport.DataSources.Add(datasource);
                    rptVisor.Visible = true;
                    rptVisor.LocalReport.Refresh();
                }
                else
                {
                    rptVisor.LocalReport.DataSources.Clear();
                    rptVisor.DataBind();
                    rptVisor.LocalReport.Refresh();
                }

            }
            else
            {
                rptVisor.LocalReport.DataSources.Clear();
                rptVisor.DataBind();
                rptVisor.LocalReport.Refresh();
            }
        }
        catch (Exception error)
        {
            throw (error);
        }
    }
    protected void dbtnBuscar_Click(object sender, EventArgs e)
    {
        mtdCargarGrid();
        rptVisor.Visible = false;
        dvGrid.Visible = true;
    }
}