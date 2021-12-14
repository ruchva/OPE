<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmCargaNanoCreditos.aspx.cs" Inherits="UI_OPE_frmCargaNanoCreditos" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGlobalEvents" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>

<%@ Register Src="~/UI/WUC/wucDialogos.ascx" TagPrefix="uc1" TagName="wucDialogo" %>
<%@ Register Src="~/UI/WUC/wucExportarGrid.ascx" TagPrefix="uc1" TagName="wucExportarGrid" %>

<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../CSS/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            height: 135px;
        }
    </style>
    <script type="text/javascript">
        function setearruta(obj)
        {            
            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");
            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))  // If Internet Explorer, return version number
            {
                $('#rutaarchivo').css('display', 'none');
            }
            else  // If another browser, return 0
            {
                $('#rutaarchivo').css('display', '');
                $('#rutaarchivo').val(obj.value);
            }           
        }
        function mensajerespuesta(valor)
        {            
            if (valor == 'True') {
                $.msgBox('Se cargo el archivo con existo.');
            }
            else
            {
                $.msgBox('Problemas al cargar el archivo.');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <% if (DesignMode){ %>
    <script src="../../JS/ASPxScriptIntelliSense.js" type="text/javascript"></script>
    <% } %> 
    <script type="text/javascript">
    </script>
    <fieldset class="search" style="height: 70%">
        <table style="border: 0; width: 100%">
            <tr>
                <td style="height: 37px">
                    <div>
                        <dx:ASPxLabel ID="dlblTitulo" runat="server" Font-Size="130%" Font-Bold="True"
                            ForeColor="#337447"  Font-Underline="True" Text="Registro de Archivo PDV">
                        </dx:ASPxLabel>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel ID="dlblPaso21" runat="server" Font-Size="100%" Font-Bold="True"
                        ForeColor="#337447"  Text="PASO 1.-" >
                    </dx:ASPxLabel>
                    <dx:ASPxLabel ID="dlblPaso21Descripcion" runat="server" Font-Size="100%" Font-Bold="False" ForeColor="#337447" Text="Seleccione el tipo e archivo a procesar:">
                    </dx:ASPxLabel>
                    <dx:ASPxCallbackPanel ID="dcbpCombo" ClientInstanceName="dcbpComboInst" runat="server" Width="100%" OnCallback="dcbpCombo_Callback" >
                        <PanelCollection>
                        <dx:PanelContent runat="server" >
                    <dx:ASPxComboBox runat="server" ID="dcbTiposArchivo" ClientInstanceName="dcbTiposArchivoInst" SelectedIndex="0" 
                        Width="30%"  >
                        <Items>
                            <dx:ListEditItem Selected="true" Value="0" Text="SELECCIONE" />
                            <dx:ListEditItem Value="1" Text="NANO CREDITOS" />                           
                        </Items>
                       <%-- <ClientSideEvents Init="mtdDcbTiposArchivoInst_Init" />
                        <ClientSideEvents SelectedIndexChanged="mtdDcbTiposArchivoInst_SelectedIndexChanged" />--%>
                    </dx:ASPxComboBox>
                    </dx:PanelContent>
                    </PanelCollection>
                        <ClientSideEvents EndCallback="mtdDcbpCombo_EndCallback" />
                    </dx:ASPxCallbackPanel>
                    <%-- <ClientSideEvents Init="mtdDcbTiposArchivoInst_Init" />
                        <ClientSideEvents SelectedIndexChanged="mtdDcbTiposArchivoInst_SelectedIndexChanged" />--%>
                </td>
            </tr>
            <tr>
                <td>
                     <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Size="100%" Font-Bold="True"
                        ForeColor="#337447"  Text="PASO 2.-">
                    </dx:ASPxLabel>
                    <dx:ASPxLabel ID="dlblSeleccion" runat="server" Font-Size="100%" Font-Bold="False"
                        ForeColor="#337447" Text="Seleccione el archivo: Nano Creditos">
                    </dx:ASPxLabel>
                    <input type="text" id="rutaarchivo" style="display:none"/>
                    
                    <label><asp:FileUpload ID="FileUpload1" runat="server" onchange="setearruta(this);"/>Examinar
                    </label>                  
                     <%-- <ClientSideEvents Init="mtdDcbTiposArchivoInst_Init" />
                        <ClientSideEvents SelectedIndexChanged="mtdDcbTiposArchivoInst_SelectedIndexChanged" />--%>
                </td>
            </tr>
            <tr>
                <td><asp:Button ID="btnUpload" Text="Subir archivo" BackColor="#337447" ForeColor="White" runat="server" OnClick="UploadFile" Font-Size="Small" Font-Bold="true" /><br />
                    <dx:ASPxButton runat="server" ID="dbtnSubir" Width="50%" AutoPostBack="false" Text="Registrar Archivo PDV" ClientInstanceName="dbtnSubir" Font-Size="Small" Font-Bold="true" Visible="false">
                        <ClientSideEvents Click="mtdDbtnSubir_click" />
                    </dx:ASPxButton>
                </td>
            </tr>
            <tr style="display:none">
                    <td >
                        <div id="Mensage001">
                            <dx:ASPxLabel ID="ddlPaso2" runat="server" Font-Size="100%" Font-Bold="True"
                        ForeColor="#337447" meta:resourcekey="ddlPaso2Resource1">
                            </dx:ASPxLabel>
                                <dx:ASPxLabel ID="ddlPaso2Texto" runat="server" Font-Size="100%" Font-Bold="False" 
                        ForeColor="#337447" meta:resourcekey="ddlPaso2TextoResource1">
                            </dx:ASPxLabel>
                        </div>
                    
                    <dx:ASPxCallbackPanel ID="dcbpGrid" ClientInstanceName="dcbpGridInst" runat="server" Width="100%" OnCallback="dcbpGrid_Callback" >
                        
                        <PanelCollection>
                        <dx:PanelContent runat="server">
                    <div style="overflow: hidden;">
                    <dx:ASPxGridView runat="server" ID="dgrvArchivosPrevios" ClientInstanceName="dgrvArchivosPreviosInst" OnBeforeGetCallbackResult="dgrvArchivosPrevios_BeforeGetCallbackResult"
                        Settings-UseFixedTableLayout="true" Styles-Cell-Wrap="False" 
                         AutoGenerateColumns="true" Width="100%" Font-Size="75%" Visible="true" OnLoad="dgrvArchivosPrevios_Load">
                        <SettingsBehavior ProcessSelectionChangedOnServer="true" AllowGroup="true" />
                        <Settings ShowGroupPanel="True" ShowFilterRow="True" />
                        <Columns>
                            <dx:GridViewDataDateColumn Caption="FECHA INGRESO" FieldName="FECHA_INGRESO" Width="10%" ShowInCustomizationForm="True" VisibleIndex="1" GroupIndex="0">
                                </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="RETENCION" FieldName="ID_RETENCIONES" Width="4%"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="NUM CIRC ASFI" FieldName="NUM_CIRCULAR_ASFI" Width="10%"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="NUM CITE" FieldName="NUM_CITE" Width="18%"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="NUM PROC PIET" FieldName="NUM_PROC_PIET" Width="10%"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="NOMBRE PROCESO" FieldName="NOMBRE_PROCESO" Width="10%"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="NOMBRE DEMANDADO" FieldName="NOMBRE_DEMANDADO" Width="10%"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="1° APELLIDO" FieldName="PRIMER_APELLIDO_DEMANDADO" Width="10%"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="2° APELLIDO" FieldName="SEGUNDO_APELLIDO_DEMANDADO" Width="10%"></dx:GridViewDataTextColumn>
                        </Columns>

                        <SettingsPager PageSize="7">
                        </SettingsPager>
                        <Settings ShowVerticalScrollBar="true"  />
                        <SettingsLoadingPanel Delay="0" Mode="Disabled" />

                        <Styles>
                        <Cell Wrap="False"></Cell>
                        </Styles>                       
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="dgveGrid" runat="server" GridViewID="dgrvArchivosPrevios"></dx:ASPxGridViewExporter>

                                        <%--<fieldset>
                                            <legend style="color:#000000">Opciones</legend>--%>
                                                <uc1:wucExportarGrid runat="server" ID="wucExportarGrid2" Visible="false" />
                                        <%--</fieldset>    --%>
                    </div>                         
                        </dx:PanelContent>
                        </PanelCollection>
                        <ClientSideEvents EndCallback="function(s, e) {
                             MostrarMensaje(s.cp_close);
                        }" />
                        </dx:ASPxCallbackPanel> 
                   </td>
            </tr> 
        </table>
        <dx:ASPxPopupControl ID="pcPopup" runat="server" ClientInstanceName="popup" PopupHorizontalAlign="OutsideLeft" PopupVerticalAlign="Middle" 
                            EncodeHtml="False" Modal="True" CloseAction="CloseButton">
                        </dx:ASPxPopupControl>
        <dx:ASPxProgressBar ID="dbarProgress" runat="server">

        </dx:ASPxProgressBar>
    </fieldset>    
    <%--<dx:ASPxHiddenField ID="dhddTipoArchivo" ClientInstanceName="dhddTipoArchivoInst" runat="server"></dx:ASPxHiddenField>--%>
        <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" ClientInstanceName="popupControl" Height="83px" Modal="True" CloseAction="CloseButton" Width="207px" AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                     <dx:ASPxLabel ID="txtMessage" runat="server" Font-Size="100%" Font-Bold="True"
                        ForeColor="#337447">
                    </dx:ASPxLabel>
                                <br/><br/>
                    <table style="border:none">
                        <tr>                            
                            <td>
                                <dx:ASPxButton ID="btnCancel" runat="server" AutoPostBack="False" ClientInstanceName="btnCancel"
                                    Text="Aceptar" Width="120px">
                                    <ClientSideEvents Click="function(s, e) {
	popupControl.Hide();
}" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <uc1:wucDialogo runat="server" ID="wucDialogo" />
</asp:Content>
