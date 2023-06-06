<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Firmante.aspx.cs" Inherits="FirmaDigital.Layouts.FirmaDigital.Firmante" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link href="../../../../_layouts/15/FirmaDigital/css/4-3-1-bootstrap.min.css" rel="stylesheet" />
    <script src="../../../../_layouts/15/FirmaDigital/js/3-4-1-jquery.min.js" type="text/javascript"></script>
    <script src="../../../../_layouts/15/FirmaDigital/js/1-14-7-popper.min.js" type="text/javascript"></script>
    <script src="../../../../_layouts/15/FirmaDigital/js/4-3-1-bootstrap.min.js" type="text/javascript"></script>
    <script>
        
        function MensajeFirmante() {
            $(".modal").css('display', 'block');
        }
        function Cerrar() {
          
            var closeModalDialog = function () {
                SP.SOD.executeFunc('sp.js', 'SP.ClientContext', function () {
                    SP.UI.ModalDialog.commonModalDialogClose(1, 1);
                });
            };
        }
    </script>
    <style>
       body div.ms.dlgBorder div.ms-dlgTitle {
        display:none !important; 
        }


        </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="container mt-4">
        <div id="tituloVentana" runat="server" clientidmode="Static">
            ¿Usted esta seguro que desea firmar el documento seleccionado?
        </div>
        <div class="btn-group btn-block mt-3" id="btnGrupoFirmar"  runat="server" clientidmode="Static">
            <button onserverclick="BtoFirmar_Click" id="BtoFirmar" type="button" class="btn btn-primary" runat="server"><i class="fas fa-download" aria-hidden="true"></i>  SI</button>
       
        </div> 
        <div id="divAbrirPdf" runat="server" clientidmode="Static" visible="false">
            Descargar PDF generado
        </div>
        <div class="btn-group btn-block mt-3" id="btnGroupPDF"  runat="server" clientidmode="Static" visible="false">
            <asp:Literal ID="ltrPDF" runat="server"></asp:Literal>
          
        </div>
     
    </div>
   
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
IEASA Firma Digital
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
IEASA Firma Digital
</asp:Content>
