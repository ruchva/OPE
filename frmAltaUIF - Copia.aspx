<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmAltaUIF - Copia.aspx.cs" Inherits="UI_OPE_frmAltaUIF" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGlobalEvents" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx"%>
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
        .auto-style2 {
            width: 173px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <% if (DesignMode){ %>
    <script src="../../JS/ASPxScriptIntelliSense.js" type="text/javascript"></script>
<% } %> 
    <script type="text/javascript">
    </script>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/JS/OPE/jsCargaPDV.js?"+DateTime.Now.Ticks) %>"></script>
    <script type="text/javascript">        
        function SetUniqueRadioButton(nameregex, rid) {
            re = new RegExp(nameregex);
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.type == 'radio') {
                    if (re.test(elm.name)) {
                        elm.checked = false;
                    }
                }
            }
            rid.checked = true;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('<%= btnProcesar.ClientID %>').click(function () {
             $.blockUI({ message: 'Just a momeqweqwent...' });

             setTimeout(function () {
                 $.unblockUI({
                     onUnblock: function () { alert('onUnblock'); }
                 });
             }, 2000);
         });
     });
    </script>
    <fieldset class="search" style="height: 70%">
            <dx:ASPxLabel ID="dlblTitulo" runat="server" Font-Size="130%" Font-Bold="True"
                            ForeColor="#337447"  Font-Underline="True" Text="Validación de PDV con TRACE">
                        </dx:ASPxLabel> 
            <table>            
            <tr>
            <td>
                <dx:ASPxLabel runat="server" ID="dlblFechaDesde" Text="Fecha:"></dx:ASPxLabel>
            </td>
            <td>            
            <dx:ASPxDateEdit ID="dtxtFechaDesde" runat="server" ClientInstanceName="dtxtFechaDesdeInst">
                            <%--<ValidationSettings>
                                <RequiredField IsRequired="true" ErrorText="*" />
                            </ValidationSettings>
                            <ClientSideEvents Init="" />
                            <ClientSideEvents DateChanged="function(s, e) { var diff = CheckDifference(); if ( diff < 0)  { dtxtFechaHastaInst.SetText(''); } }" />--%>
                        </dx:ASPxDateEdit>               
            </td>
            <td>
                <asp:Button runat="server" ID="btnBuscar" Text="Buscar Archivo PDV" BackColor="#337447" ForeColor="White" OnClick="btnBuscar_Click" />                   
            </td>
            </tr>            
            </table>
            <asp:Repeater id="rptCliente" runat="server" onitemdatabound="rptCliente_ItemDataBound">
            <HeaderTemplate>
                <table style="margin-top:5px;">
                    <tr style="background-color:#337447">
                        <td>Codigo</td>                   
                        <td>Fecha Proceso</td>                   
                        <td>NombreArchivo</td>                    
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="text-align:left;color:black">
                        <asp:RadioButton ID="rdbSeleccionar" GroupName="rbGrupo" runat="server"/>           
                    <asp:Label ID="lblCodigoPersona" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CODCABNANOCREDITO")%>'></asp:Label></td>
                    <td style="color:black"><asp:Label ID="lblFechaProceso" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FECHAPROCESO")%>'></asp:Label></td>                                                    
                    <td style="color:black"><asp:Label ID="lblNombreArchivo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NOMBREARCHIVO")%>'></asp:Label></td>                                                                                                                                              
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr>
                    <td style="text-align:left;color:black">
                        <asp:RadioButton ID="rdbSeleccionar" GroupName="rbGrupo" runat="server"/>           
                    <asp:Label ID="lblCodigoPersona" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CODCABNANOCREDITO")%>'></asp:Label></td>
                    <td style="color:black"><asp:Label ID="lblFechaProceso" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FECHAPROCESO")%>'></asp:Label></td>                                                    
                    <td style="color:black"><asp:Label ID="lblNombreArchivo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NOMBREARCHIVO")%>'></asp:Label></td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Button runat="server" Text="Validar Archivo PDV" BackColor="#337447" ForeColor="White" OnClick="Unnamed1_Click" />         
        <table style="border: 0; width: 100%;visibility:hidden">
            <tr>
                <td style="height: 37px">
                    <div>
                        
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
                        Width="30%">
                        <Items>
                            <dx:ListEditItem Selected="true" Value="0" Text="SELECCIONE" />
                            <dx:ListEditItem Value="RE" Text="NANO CREDITOS" />                           
                        </Items>
                        <ClientSideEvents Init="mtdDcbTiposArchivoInst_Init" />
                        <ClientSideEvents SelectedIndexChanged="mtdDcbTiposArchivoInst_SelectedIndexChanged" />
                    </dx:ASPxComboBox>
                    </dx:PanelContent>
                    </PanelCollection>
                        <ClientSideEvents EndCallback="mtdDcbpCombo_EndCallback" />
                    </dx:ASPxCallbackPanel>
                    <dx:ASPxHiddenField ID="dhddTipoArchivo" ClientInstanceName="dhddTipoArchivoInst" runat="server"></dx:ASPxHiddenField>
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
                    <dx:ASPxUploadControl ID="dupcArchivo" ClientInstanceName="dupcArchivoInst" runat="server" Width="50%" 
                        ShowProgressPanel="true" OnFileUploadComplete="dupcArchivo_FileUploadComplete" FileUploadMode="OnPageLoad" 
                        meta:resourcekey="dupcArchivoResource1" ShowUploadButton="false" Height="30px"  >
                        <ClientSideEvents TextChanged="mtdDupcArchivoInst_TextChanged"  />               
                        <ClientSideEvents FileUploadComplete="mtdDupcArchivoInst_FileUploadComplete" />
                        <ClientSideEvents Init="mtdDupcArchivoInst_Init" />
                        <ButtonStyle Font-Size="Medium" Font-Bold="true"></ButtonStyle>
                    </dx:ASPxUploadControl> 
                    <dx:ASPxButton runat="server" ID="dbtnSubir" Width="50%" AutoPostBack="false" Text="Subir archivo" ClientInstanceName="dbtnSubir" Font-Size="Small" Font-Bold="true">
                        <ClientSideEvents Click="mtdDbtnSubir_click" />
                    </dx:ASPxButton>
                </td>
            </tr>            
            <tr>
                    <td >                               
                        <div id="Mensage001">
                            <dx:ASPxLabel ID="ddlPaso2" runat="server" Font-Size="100%" Font-Bold="True"
                        ForeColor="#337447" meta:resourcekey="ddlPaso2Resource1">
                            </dx:ASPxLabel>
                                <dx:ASPxLabel ID="ddlPaso2Texto" runat="server" Font-Size="100%" Font-Bold="False" 
                        ForeColor="#337447" meta:resourcekey="ddlPaso2TextoResource1">
                            </dx:ASPxLabel>
                        </div>
                    
                    <dx:ASPxCallbackPanel ID="dcbpGrid" ClientInstanceName="dcbpGridInst" runat="server" Width="100%">                        
                        <PanelCollection>
                        <dx:PanelContent runat="server">
                    
                    <div style="overflow: hidden;">
                    <%--<%--<dx:ASPxGridView runat="server" ID="dgrvArchivosPrevios" ClientInstanceName="dgrvArchivosPreviosInst" OnBeforeGetCallbackResult="dgrvArchivosPrevios_BeforeGetCallbackResult"
                        Settings-UseFixedTableLayout="true" Styles-Cell-Wrap="False" 
                         AutoGenerateColumns="true" Width="100%" Font-Size="75%" Visible="true" OnLoad="dgrvArchivosPrevios_Load">
                        <SettingsBehavior ProcessSelectionChangedOnServer="false" AllowGroup="false" />
                        <Settings ShowGroupPanel="false" ShowFilterRow="false" />
                        <Columns>
                            <dx:GridViewDataCheckColumn Caption="" FieldName="Radio" Width="20%">
                                <%--<DataItemTemplate>
                                    <input id="Radio1" type="radio" name="myradio"/>    
                                </DataItemTemplate>--%>
                                <%--<DataItemTemplate>
                                        <a href="javascript:void(0);" onclick="mtdEliminarMensaje( &#039;<%#Eval("CODCABNANOCREDITO")%> &#039;)">
                                            <img src="../../IMG/ICONS/icoDelete.png" />
                                </DataItemTemplate>
                            </dx:GridViewDataCheckColumn>                            
                            <dx:GridViewDataTextColumn Caption="CODIGO" FieldName="CODCABNANOCREDITO" Width="20%"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="FECHA PROCESO" FieldName="FECHAPROCESO" Width="30%"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="NOMBRE ARCHIVO" FieldName="NOMBREARCHIVO" Width="30%"></dx:GridViewDataTextColumn>                            
                        </Columns>
                        <SettingsPager PageSize="7">
                        </SettingsPager>
                        <Settings ShowVerticalScrollBar="true"  />
                        <SettingsLoadingPanel Delay="0" Mode="Disabled" />
                        <Styles>
                        <Cell Wrap="False"></Cell>
                        </Styles>                       
                    </dx:ASPxGridView>--%>
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
            <tr>
                <td>                    
                    <asp:Button ID="btnProcesar" runat="server" Text="Validar Archivo PDV" OnClick="Unnamed1_Click" />
                </td>
            </tr>
        </table>
        <dx:ASPxProgressBar ID="dbarProgress" runat="server">

        </dx:ASPxProgressBar>
    </fieldset>
    
    <dx:ASPxCallback ID="dcllRefrescarCarga" ClientInstanceName="dcllRefrescarCargaInst" OnCallback="dcllRefrescarCarga_Callback" runat="server">
        <ClientSideEvents CallbackComplete="dcllRefrescarCarga_CallbackComplete" />
    </dx:ASPxCallback>
            <dx:ASPxGlobalEvents ID="ge" runat="server">
        <ClientSideEvents BeginCallback="OnControlsInitialized" EndCallback="OnEndCallBack"  />
   </dx:ASPxGlobalEvents>
   <dx:ASPxHiddenField ID="hdCODCLI" ClientInstanceName="hdCODCLIInst" runat="server"></dx:ASPxHiddenField>
   <dx:ASPxPopupControl ID="dppcRegistrar" HeaderText="Registrar Mensaje" Width="400px" Height="200px" 
        ClientInstanceName="dppcRegistrarInst" runat="server" ShowFooter="true" AllowDragging="true"
        PopupVerticalAlign="WindowCenter" Modal="true" PopupHorizontalAlign="WindowCenter">
        <ClientSideEvents Shown="mtdValidarTipoCamp" />
        <ContentCollection>
            <dx:PopupControlContentControl>                
                <dx:ASPxCallback ID="dcllRegistro" ClientInstanceName="dcllRegistroInst" runat="server"
                    OnCallback="dcllRegistro_Callback">
                    <ClientSideEvents CallbackComplete="mtdDcllRegistro_CallbackComplete" />
                </dx:ASPxCallback>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <FooterTemplate>
            <div>
                <table>
                    <tr>
                        <td><input type="text" id="hdt" value="0"  runat="server" />
                            <asp:Label ID="lblhdt" runat="server" Text=""></asp:Label>
                            <dx:ASPxLabel ID="dlblNroCampCaptionA" CssClass="text_caption" Text="Nro. Campaña: " runat="server"></dx:ASPxLabel>
                            <dx:ASPxHiddenField ID="dhddDatos" ClientInstanceName="dhddDatosInst" runat="server"></dx:ASPxHiddenField>
                            <dx:ASPxButton ID="dbtnRegistrarAceptar" Text="Aceptar"
                                ClientInstanceName="dbtnRegistrarAceptarInst" AutoPostBack="false" runat="server" OnClick="Unnamed1_Click">
                                <ClientSideEvents Click="mtdDbtnRegistrarAceptarMsg_click" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="dbtnRegistrarCancelar" Text="Cancelar"
                                ClientInstanceName="dbtnRegistrarCancelarInst" AutoPostBack="false" runat="server">
                                <ClientSideEvents Click="function() { dppcRegistrarInst.Hide(); }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </FooterTemplate>
    </dx:ASPxPopupControl>

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
