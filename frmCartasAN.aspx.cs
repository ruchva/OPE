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
using System.Data;
using System.Drawing;

using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxUploadControl;
using DevExpress.Web.ASPxEditors;
using OfficeOpenXml;
using Microsoft.Reporting.WebForms;

public partial class UI_OPE_frmCartasAN : System.Web.UI.Page
{
    /// <summary>
    /// mapeo direccion MapPath de contexto estatico
    /// </summary>
    //private static string CURRENT_DIR = System.Web.HttpContext.Current.Server.MapPath("~");
    //private static string PATH_TEMPLATE = CURRENT_DIR + @"UI\OPE\template.doc";
    //private static string PATH_NEW_DOC = CURRENT_DIR + @"UI\OPE\";
    private static string CURRENT_DIR = @"E:\websiafiles\templates\";
    private static string PATH_TEMPLATE = CURRENT_DIR + "template.doc";
    private static string PATH_NEW_DOC = CURRENT_DIR;
    /// <summary>
    /// instancia libreria para Word
    /// </summary>
    private static Application objWord = new Application();
    /// <summary>
    /// main method
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            ASPxGridView1.DataSource = Session["push"];
            ASPxGridView1.DataBind();
        }
        else
        {
            ASPxGridView1.DataSource = null;
            Session["push"] = null;
            ASPxGridView1.DataBind();
        }

    }
    /// <summary>
    /// genera carta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void generarCarta_Click(object sender, EventArgs e)
    {
        string[] argumentos = ((ASPxButton)sender).CommandArgument.Split(new char[] { '|' });        
        string fileName = "AN-" + DateTime.Now.ToString("yyyymmddhhmmssmss") + ".doc";
        string path = getNuevoDocumento(fileName);  
        abreDocumento(path, argumentos);
        Response.ContentType = "Application/msword";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
        Response.TransmitFile(path);
        Response.Flush();
        FileInfo newFile = new FileInfo(path);
        newFile.Delete();
        Response.End();        
    }    
    /// <summary>
    /// abre nuevo doc y lo copia en la raiz
    /// </summary>
    /// <returns></returns>
    static string getNuevoDocumento(string fileName)
    {
        try
        {
            string pathFile = @PATH_NEW_DOC + fileName;
            File.Copy(@PATH_TEMPLATE, pathFile, true);            
            return pathFile;
        }
        catch (UnauthorizedAccessException e)
        {
            throw e;
        }
        catch (DirectoryNotFoundException e)
        {
            throw e;
        }

    }
    /// <summary>
    /// edita el documento
    /// </summary>
    /// <param name="path"></param>
    static void abreDocumento(string path, string[] args)
    {
        try
        {
            /// cada array es una fila de parametros
            DateTime fechaActual = DateTime.Now;
            string dia  = fechaActual.ToString("dd");
            string mes  = fechaActual.ToString("MMMM");
            string anio = fechaActual.ToString("yyyy");
            string FECHA_CARTA = dia + " de " + mes + " de " + anio;
            ///validaciones
            string SIREFO      = args[0];
            string FECHA_CITE  = args[1];
            string PIET        = args[2];
            string INSTITUCION = "Gerencia Regional de la Aduana Nacional";///constante
            string CLIENTE     = args[5];
            string CUENTA      = args[6];
            string MONTO       = args[7];
            var culture = new System.Globalization.CultureInfo("es-ES");///Spanish
            decimal monto_decimal = decimal.Parse(MONTO.Replace(",","."), culture);            
            string MONTO_LITERAL  = monto_decimal.NumeroALetras();
            string MONEDA_L    = "";
            string MONEDA_C    = "";
            if (args[8] == "1")
            {
                MONEDA_L = "Bolivianos";
                MONEDA_C = "Bs";
            }
            else
            {
                MONEDA_L = "Dólares";
                MONEDA_C = "Sus";
            }
            /// instanciamos documento
            Application objWord = new Application();
            Document documento = objWord.Documents.Open(path);
            documento.Activate();
            FindAndReplace(objWord, "[FECHA_CARTA]", FECHA_CARTA);
            FindAndReplace(objWord, "[PIET]", PIET);
            FindAndReplace(objWord, "[SIREFO]", SIREFO);
            FindAndReplace(objWord, "[FECHA_CITE]", FECHA_CITE);
            FindAndReplace(objWord, "[INSTITUCION]", INSTITUCION);
            FindAndReplace(objWord, "[CLIENTE]", CLIENTE);
            FindAndReplace(objWord, "[MONEDA_L]", MONEDA_L);
            FindAndReplace(objWord, "[MONEDA_C]", MONEDA_C);
            FindAndReplace(objWord, "[MONTO]", MONTO);
            FindAndReplace(objWord, "[MONTO_LITERAL]", MONTO_LITERAL);
            ///parametro MANEJO - TIPO_USO de la cuenta - campo C1686 de SALDOS
            clsOperacionesNeg objNegocio = new clsOperacionesNeg();
            System.Data.DataTable dtReturn = objNegocio.mtdCuentaTipoUso(CUENTA);
            string tipo_uso = "No Determinado";
            if (dtReturn != null)
            {
                tipo_uso = dtReturn.Rows[0]["DESC_TIPO_USO"].ToString();
            }            
            ///por el momento: una cuenta --> una carta
            EditTable(1, ref documento, args, tipo_uso);
            documento.Save();
            documento.Close();
        }
        catch (FileNotFoundException e)
        {
            throw e;
        }
    }
    /// <summary>
    /// edita la tabla dentro del documento
    /// </summary>
    /// <param name="rows"></param>
    /// <param name="value"></param>
    /// <param name="doc"></param>
    static void EditTable(int rows, ref Document doc, string[] args, string tipo_uso)
    {
        try
        {
            string TIPO      = args[3];
            string DOCUMENTO = args[4];
            string CLIENTE   = args[5];
            string CUENTA    = args[6];
            string MONTO     = args[7];
            string MANEJO    = tipo_uso;
            string MONEDA_N = "";
            string MONEDA_C = "";
            if (args[8] == "1")
            {
                MONEDA_N = "Nacional";
                MONEDA_C = "Bs";
            }
            else
            {
                MONEDA_N = "Extranjera";
                MONEDA_C = "Sus";
            }
            ///
            foreach (Section s in doc.Sections)
            {
                Tables tables = s.Range.Tables;
                int count = 0;
                foreach (Microsoft.Office.Interop.Word.Table table in tables)
                {
                    if (count > 0) 
                        break;
                    ///
                    table.Rows.Add();
                    for (var i = 1; i <= rows; i++)
                    {
                        table.Cell(i + 1, 1).Range.Text = CLIENTE + " " + TIPO + " " + DOCUMENTO;
                        table.Cell(i + 1, 2).Range.Text = CUENTA;
                        table.Cell(i + 1, 3).Range.Text = MANEJO;
                        table.Cell(i + 1, 4).Range.Text = MONEDA_N;
                        table.Cell(i + 1, 5).Range.Text = MONEDA_C + " " + MONTO;
                        table.Cell(i + 1, 1).Range.Bold = 0;
                        table.Cell(i + 1, 2).Range.Bold = 0;
                        table.Cell(i + 1, 3).Range.Bold = 0;
                        table.Cell(i + 1, 4).Range.Bold = 0;
                        table.Cell(i + 1, 5).Range.Bold = 0;
                    }
                    table.Borders.Enable = 1;
                    
                    count++;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    /// <summary>
    /// SAMPLE DE LA LIBRERIA Microsoft.Office.Interop.Word
    /// </summary>
    /// <param name="WordApp"></param>
    /// <param name="findText"></param>
    /// <param name="replaceWithText"></param>
    private static void FindAndReplace(Microsoft.Office.Interop.Word.Application WordApp, object findText, object replaceWithText)
    {
        object matchCase = true;
        object matchWholeWord = true;
        object matchWildCards = false;
        object matchSoundsLike = false;
        object nmatchAllWordForms = false;
        object forward = true;
        object format = false;
        object matchKashida = false;
        object matchDiacritics = false;
        object matchAlefHamza = false;
        object matchControl = false;
        object read_only = false;
        object visible = true;
        object replace = 2;
        object wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
        object replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
        WordApp.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord, ref matchWildCards, ref matchSoundsLike,
        ref nmatchAllWordForms, ref forward,
        ref wrap, ref format, ref replaceWithText,
        ref replaceAll, ref matchKashida,
        ref matchDiacritics, ref matchAlefHamza,
        ref matchControl);
    }
    /// <summary>
    /// CAPTURA EL EXCEL
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void filesUploadComplete(object sender, DevExpress.Web.ASPxUploadControl.FilesUploadCompleteEventArgs e)
    {
        ASPxUploadControl uploadControl = sender as ASPxUploadControl;
        if (uploadControl.UploadedFiles != null && uploadControl.UploadedFiles.Length > 0)
        {
            for (int i = 0; i < uploadControl.UploadedFiles.Length; i++)
            {
                UploadedFile file = uploadControl.UploadedFiles[i];
                if (file.FileName != "")
                {
                    System.Data.DataTable dataTable = obtenerDatosExcel(file);
                    var data = procesarDatos(dataTable);
                    Session["push"] = data;
                }
            }
        }
    }
    /// <summary>
    /// RECORRE EL EXCEL
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    protected System.Data.DataTable obtenerDatosExcel(UploadedFile file)
    {
        var tabla = new System.Data.DataTable();
        using (var excel = new ExcelPackage(file.PostedFile.InputStream))
        {
            var ws = excel.Workbook.Worksheets.First();
            var hasHeader = true;
            // add DataColumns to DataTable
            int i = 0;
            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
            {
                i++;
                tabla.Columns.Add(hasHeader ? "Columna" + i
                    : String.Format("Column {0}", firstRowCell.Start.Column));
            }
            // add DataRows to DataTable
            int startRow = hasHeader ? 2 : 1;
            for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                DataRow row = tabla.NewRow();
                foreach (var cell in wsRow)
                    row[cell.Start.Column - 1] = cell.Text.ToString();
                tabla.Rows.Add(row);
            }
        }
        return tabla;
    }
    /// <summary>
    /// ACOMODA DATOS DEL EXCEL EN LA GRILLA
    /// </summary>
    /// <param name="dataTable"></param>
    /// <returns></returns>
    public List<dynamic> procesarDatos(System.Data.DataTable dataTable)
    {
        string vSIREFO;    
        string vFECHA_CITE;
        string vPIET;      
        string vTIPO;      
        string vNUMERO;    
        string vNOMBRE;    
        string vCUENTA;    
        string vMONTO;     
        string vMONEDA;    
        List<dynamic> data = new List<dynamic>();
        foreach (DataRow item in dataTable.Rows)
        {
            var row = item.ItemArray;
            vSIREFO     = row[1].ToString().Trim(); 
            vFECHA_CITE = row[2].ToString().Trim(); 
            vPIET       = row[3].ToString().Trim(); 
            vTIPO       = row[4].ToString().Trim(); 
            vNUMERO     = row[5].ToString().Trim(); 
            vNOMBRE     = row[6].ToString().Trim(); 
            vCUENTA     = row[10].ToString().Trim();
            vMONTO      = row[11].ToString().Trim();
            vMONEDA     = row[12].ToString().Trim();
            data.Add(new { 
                SIREFO     = vSIREFO, 
                FECHA_CITE = vFECHA_CITE, 
                PIET       = vPIET, 
                TIPO       = vTIPO, 
                NUMERO     = vNUMERO, 
                NOMBRE     = vNOMBRE, 
                CUENTA     = vCUENTA, 
                MONTO      = vMONTO, 
                MONEDA     = vMONEDA
            });
        }
        return data;
    }
}
/// <summary>
/// Convertir un numero a literal
/// Para dinero se utiliza el tipo Decimal
/// </summary>
public static class Conversores
{
    /// <summary>
    /// parte decimal
    /// </summary>
    /// <param name="numberAsString"></param>
    /// <returns></returns>
    public static string NumeroALetras(this decimal numberAsString)
    {
        string dec;            
           
        var entero = Convert.ToInt64(Math.Truncate(numberAsString));
        var decimales = Convert.ToInt32(Math.Round((numberAsString - entero) * 100, 2));
        if (decimales > 0 && decimales < 10)
        {
            dec = " 0"+decimales.ToString() + "/100";
        }
        else
        {
            dec = " " + decimales.ToString() + "/100";
        }
        var res = NumeroALetras(Convert.ToDouble(entero)) + dec;
        return res;
    }
    /// <summary>
    /// parte entera
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static string NumeroALetras(double value)
    {
        string num2Text; value = Math.Truncate(value);
        if (value == 0) num2Text = "cero";
        else if (value == 1) num2Text = "uno";
        else if (value == 2) num2Text = "dos";
        else if (value == 3) num2Text = "tres";
        else if (value == 4) num2Text = "cuatro";
        else if (value == 5) num2Text = "cinco";
        else if (value == 6) num2Text = "seis";
        else if (value == 7) num2Text = "siete";
        else if (value == 8) num2Text = "ocho";
        else if (value == 9) num2Text = "nueve";
        else if (value == 10) num2Text = "diez";
        else if (value == 11) num2Text = "once";
        else if (value == 12) num2Text = "doce";
        else if (value == 13) num2Text = "trece";
        else if (value == 14) num2Text = "catorce";
        else if (value == 15) num2Text = "quince";
        else if (value < 20) num2Text = "dieci" + NumeroALetras(value - 10);
        else if (value == 20) num2Text = "veinte";
        else if (value < 30) num2Text = "veinti" + NumeroALetras(value - 20);
        else if (value == 30) num2Text = "treinta";
        else if (value == 40) num2Text = "cuarenta";
        else if (value == 50) num2Text = "cincuenta";
        else if (value == 60) num2Text = "sesenta";
        else if (value == 70) num2Text = "setenta";
        else if (value == 80) num2Text = "ochenta";
        else if (value == 90) num2Text = "noventa";
        else if (value < 100) num2Text = NumeroALetras(Math.Truncate(value / 10) * 10) + " y " + NumeroALetras(value % 10);
        else if (value == 100) num2Text = "cien";
        else if (value < 200) num2Text = "ciento " + NumeroALetras(value - 100);
        else if ((value == 200) || (value == 300) 
                                || (value == 400) 
                                || (value == 600) 
                                || (value == 800)) num2Text = NumeroALetras(Math.Truncate(value / 100)) + "cientos";
        else if (value == 500) num2Text = "quinientos";
        else if (value == 700) num2Text = "setecientos";
        else if (value == 900) num2Text = "novecientos";
        else if (value < 1000) num2Text = NumeroALetras(Math.Truncate(value / 100) * 100) + " " + NumeroALetras(value % 100);
        else if (value == 1000) num2Text = "un mil";
        else if (value < 2000) num2Text = "mil " + NumeroALetras(value % 1000);
        else if (value < 1000000)
        {
            num2Text = NumeroALetras(Math.Truncate(value / 1000)) + " mil";
            if ((value % 1000) > 0)
            {
                num2Text = num2Text + " " + NumeroALetras(value % 1000);
            }
        }
        else if (value == 1000000)
        {
            num2Text = "un millon";
        }
        else if (value < 2000000)
        {
            num2Text = "un millon " + NumeroALetras(value % 1000000);
        }
        else if (value < 1000000000000)
        {
            num2Text = NumeroALetras(Math.Truncate(value / 1000000)) + " millones ";
            if ((value - Math.Truncate(value / 1000000) * 1000000) > 0)
            {
                num2Text = num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000) * 1000000);
            }
        }
        else if (value == 1000000000000) num2Text = "un billon";
        else if (value < 2000000000000) num2Text = "un billon " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
        else
        {
            num2Text = NumeroALetras(Math.Truncate(value / 1000000000000)) + " billones";
            if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0)
            {
                num2Text = num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
        }
        return num2Text;
    }
}