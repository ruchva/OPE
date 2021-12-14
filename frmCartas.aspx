<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmCartas.aspx.cs" Inherits="UI_COT_frmCartas" %>

<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DateInitial(s, e) {  
            var today = new Date();
            //dtxtFechaDesdeInst.SetDate(today);
            //dtxtFechaHastaInst.SetDate(today);
        }
        function EnviarClick(psb) {
            dhddVariableInst.Set('pIdRetenciones', psb);
            dbtnDocDownloadAction.DoClick();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <dx:ASPxLabel ID="dlblTitulo" runat="server" Font-Size="130%" Font-Bold="True"
        ForeColor="#337447" Font-Underline="True" Text="GENERACI&Oacute;N DE CARTAS DE RETENCIONES">
    </dx:ASPxLabel>
    <br />
    <fieldset>
        <legend style="color: #000000">Filtros</legend>
        <table style="border: 0; width: 100%">
            <colgroup>
                <col style="width: 20%" />
                <col style="width: 30%" />
                <col style="width: 20%" />
                <col style="width: 40%" />
            </colgroup>
            <tr>
                <td>
                    <dx:ASPxLabel runat="server" ID="dlblFechaDesde" Text="Fecha desde:"></dx:ASPxLabel>
                </td>

                <td>
                    <dx:ASPxDateEdit ID="dtxtFechaDesde" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" runat="server" ClientInstanceName="dtxtFechaDesdeInst">
                        <ValidationSettings>
                            <RequiredField IsRequired="true" ErrorText="*" />
                        </ValidationSettings>
                        <ClientSideEvents Init="DateInitial" />
                    </dx:ASPxDateEdit>
                </td>
                <td>
                    <dx:ASPxLabel runat="server" ID="ASPxLabel1" Text="Fecha hasta:"></dx:ASPxLabel>
                </td>

                <td>
                    <dx:ASPxDateEdit ID="dtxtFechaHasta" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" runat="server" ClientInstanceName="dtxtFechaHastaInst">
                        <ValidationSettings>
                            <RequiredField IsRequired="true" ErrorText="*" />
                        </ValidationSettings>
                        <ClientSideEvents LostFocus="function(s,e) { dgrvLibroBancarizacionInst.Refresh(); dcbPeriodoInst.PerformCallback(); }" />
                    </dx:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel runat="server" ID="dlblExpandir" Text="C&oacute;digo de CITE:"></dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxTextBox runat="server" ID="dtxtCite" ClientInstanceName="dtxtCiteInst">
                        <ClientSideEvents TextChanged="function(s,e) { dgrvLibroBancarizacionInst.Refresh(); dcbPeriodoInst.PerformCallback(); }" />
                        <MaskSettings Mask="999999" />
                    </dx:ASPxTextBox>
                </td>
                <td>
                    <dx:ASPxLabel runat="server" ID="ASPxLabel2" Text="Tipo documento:"></dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox runat="server" ID="dcbTipo" ClientInstanceName="dcbTipoInst">
                        <Items>
                            <dx:ListEditItem Selected="true" Text="No clientes" Value="NC" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
        </table>
        </fieldset>
    <div style="padding: 10px 10px 10px 10px">
    <dx:ASPxButton ID="dbtnBuscar" ClientInstanceName="dbtnBuscarInst" runat="server" Text="BUSCAR" Width="350" AutoPostBack="true" OnClick="dbtnBuscar_Click">
                    </dx:ASPxButton>
        <dx:ASPxButton ID="dbtnImprimirTodos" ClientInstanceName="dbtnImprimirTodosInst" OnClick="dbtnImprimirTodos_Click" runat="server" Text="TODOS" Width="350" AutoPostBack="true">
                    </dx:ASPxButton>
    </div>
    <div style="width: 900px; height: 300px; overflow: scroll" id="dvGrid" runat="server">
     <dx:ASPxGridView ID="dgrvRetenciones" ClientInstanceName="dgrvRetencionesInst" runat="server" OnLoad="dgrvRetenciones_Load"
            Font-Size="8px" Font-Bold="False" SettingsLoadingPanel-Mode="Default" SettingsBehavior-AllowSelectByRowClick="true"
            Settings-UseFixedTableLayout="true" Styles-Cell-Wrap="False" AutoGenerateColumns="False" 
            EnableRowsCache="true"
            SettingsBehavior-ProcessFocusedRowChangedOnServer="false" Width="950px" KeyFieldName="ID_RETENCIONES">
            <SettingsEditing Mode="PopupEditForm">
            </SettingsEditing>
            <Settings ShowFilterBar="Visible" ShowFilterRow="True" ShowFilterRowMenu="false" ShowFilterRowMenuLikeItem="false" ShowHeaderFilterButton="false" />
            <SettingsBehavior AllowSelectByRowClick="True"></SettingsBehavior>

            <SettingsPager PageSize="7">
            </SettingsPager>
            <SettingsLoadingPanel Mode="Disabled" />

            <Columns>
                <dx:GridViewDataColumn Caption="Cite" FieldName="ID_RETENCIONES" Width="6%" VisibleIndex="0"></dx:GridViewDataColumn>
                <dx:GridViewDataDateColumn Caption="Fecha" FieldName="FECHA_CIRCULAR_ASFI" Width="5%" VisibleIndex="1"></dx:GridViewDataDateColumn>
                <dx:GridViewDataColumn Caption="Motivo" FieldName="MOTIVO" Width="20%" VisibleIndex="2"></dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn  VisibleIndex="3" Width="15%" Caption="#" >
                                <DataItemTemplate>
                                    <a href="javascript:void(0);" onclick='EnviarClick("<%#Eval("ID_RETENCIONES").ToString()%>")'>
                                            Descargar...</a>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
            </Columns>

            <SettingsPopup>
                <EditForm HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
            </SettingsPopup>

            <Styles>
                <Cell Wrap="False"></Cell>
            </Styles>
           
        </dx:ASPxGridView>
        </div>

    <dx:ASPxButton ID="dbtnDocDownloadAction" ClientInstanceName="dbtnDocDownloadAction" runat="server"
                     ClientVisible="false" OnClick="dbtnDocDownloadAction_Click" >
                    </dx:ASPxButton>
    <dx:ASPxHiddenField ID="dhddVariable" ClientInstanceName="dhddVariableInst" runat="server"></dx:ASPxHiddenField>



    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <rsweb:ReportViewer ID="rptVisor" Visible="True" runat="server" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="900px" ZoomMode="PageWidth">
                </rsweb:ReportViewer>
        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

