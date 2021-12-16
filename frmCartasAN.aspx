<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmCartasAN.aspx.cs" Inherits="UI_OPE_frmCartasAN" %>
                                                                                                      
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxDocking" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGlobalEvents" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxUploadControl" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/UI/WUC/wucDialogos.ascx" TagPrefix="uc1" TagName="wucDialogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <script type="text/javascript">
        function OnClick(s, e) {
            e.processOnServer = ASPxClientEdit.ValidateGroup("submit", true);
        }
    </script>
    <% if (DesignMode){ %>
        <script src="../../JS/ASPxScriptIntelliSense.js" type="text/javascript"></script>
    <% } %>
    <table style="border:0;width:100%">
    <tr>
        <td>
            <div> 
                <div>
                    <dx:aspxlabel ID="dlblTitulo"  runat="server"  Font-Size="130%"  Font-Bold="True" 
                            ForeColor="#337447" Font-Underline="True" Text="EXCEL DE VERIFICACIONES" >
                    </dx:aspxlabel>
                </div>
                <dx:ASPxUploadControl ID="ASPxUploadControl1" runat="server" ShowUploadButton="True" Width="50%"
                                      OnFilesUploadComplete ="filesUploadComplete" ShowProgressPanel="True">
                    <ValidationSettings AllowedFileExtensions=".xlsx"></ValidationSettings>
                    <ClientSideEvents FilesUploadComplete="function(s, e) {clientGridView1.Refresh();}" />
                </dx:ASPxUploadControl>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="clientGridView1"  runat="server" Width="100%" >
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="SIREFO" Caption="SIREFO" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="FECHA_CITE" Caption="FECHA" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="PIET" Caption="NRO. PIET" />
                    <dx:GridViewDataTextColumn FieldName="TIPO" Caption="TOPO DOC." Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="NUMERO" Caption="NUM. DOC." Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="NOMBRE" Caption="NOMBRE / RAZON SOCIAL" />
                    <dx:GridViewDataTextColumn FieldName="CUENTA" Caption="NRO. CUENTA" />
                    <dx:GridViewDataTextColumn FieldName="MONTO" Caption="MONTO" />
                    <dx:GridViewDataTextColumn FieldName="MONEDA" Caption="MONEDA" Visible="false" />
                    <dx:GridViewDataColumn VisibleIndex="5">
                        <DataItemTemplate>
                            <dx:ASPxButton ID="ASPxButton1" runat="server" OnClick="generaCartaRetencion_Click" 
                                AutoPostBack="False" RenderMode="Link" Text="Carta Retención" Visible='<%# Eval("CUENTA").ToString() != "" && Eval("MONTO").ToString() != "0,00" ? true : false%>' 
                                CommandArgument=' <%# Eval("SIREFO") +"|"+ 
                                                      Eval("FECHA_CITE") +"|"+ 
                                                      Eval("PIET") +"|"+ 
                                                      Eval("TIPO") +"|"+ 
                                                      Eval("NUMERO") +"|"+ 
                                                      Eval("NOMBRE") +"|"+ 
                                                      Eval("CUENTA") +"|"+ 
                                                      Eval("MONTO") +"|"+
                                                      Eval("MONEDA") %> ' >
                                <Image IconID="actions_download_16x16" />
                                <ClientSideEvents Click="" />   
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="ASPxButton2" runat="server" OnClick="generaCartaBloqueo_Click" 
                                AutoPostBack="False" RenderMode="Link" Text="Carta Bloqueo" Visible='<%# Eval("CUENTA").ToString() != "" && Eval("MONTO").ToString() == "0,00" ? true : false%>' 
                                CommandArgument=' <%# Eval("SIREFO") +"|"+ 
                                                      Eval("FECHA_CITE") +"|"+ 
                                                      Eval("PIET") +"|"+ 
                                                      Eval("TIPO") +"|"+ 
                                                      Eval("NUMERO") +"|"+ 
                                                      Eval("NOMBRE") +"|"+ 
                                                      Eval("CUENTA") +"|"+ 
                                                      Eval("MONTO") +"|"+
                                                      Eval("MONEDA") %> ' >
                                <Image IconID="actions_download_16x16" />
                                <ClientSideEvents Click="" />   
                            </dx:ASPxButton>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                </Columns>
                <SettingsDataSecurity AllowEdit="False" AllowInsert="False" AllowDelete="False"></SettingsDataSecurity>
            </dx:ASPxGridView>
        </td>
    </tr>
    </table>

    <rsweb:ReportViewer ID="rptVisor" runat="server" AsyncRendering="false" Width="900" ZoomMode="PageWidth"></rsweb:ReportViewer>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <uc1:wucDialogo runat="server" ID="wucDialogo" />
</asp:Content>

