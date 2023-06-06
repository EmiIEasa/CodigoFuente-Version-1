<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormularioFirmaDigital.aspx.cs" Inherits="FirmaDigital.Layouts.FirmaDigital.FormularioFirmaDigital" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link href="../../../../_layouts/15/FirmaDigital/css/4-3-1-bootstrap.min.css" rel="stylesheet" />
    <script src="../../../../_layouts/15/FirmaDigital/js/3-4-1-jquery.min.js" type="text/javascript"></script>
    <script src="../../../../_layouts/15/FirmaDigital/js/1-14-7-popper.min.js" type="text/javascript"></script>
    <script src="../../../../_layouts/15/FirmaDigital/js/4-3-1-bootstrap.min.js" type="text/javascript"></script>
    <script src="../../../../_layouts/15/FirmaDigital/js/fontAwesome.js" crossorigin="anonymous" type="text/javascript"></script>
    <script>
        function MensajeFirmante() {
            $(".modal").css('display', 'block');
        }
        function cerrar() {
            //location.href = 'http://ibiza:2222/sites/formularios/Lists/ActaDeReunionAprobadores/AllItems.aspx';
            $(".modal").css('display', 'none');
        }
    </script>
   <script type="text/javascript">
       // Some needed constants
       CAPICOM_CURRENT_USER_STORE = 2;
       CAPICOM_STORE_OPEN_READ_ONLY = 0;
       CAPICOM_AUTHENTICATED_ATTRIBUTE_SIGNING_TIME = 0;
       CAPICOM_ENCODE_BASE64 = 0;
       function Authenticate() {
           try {
        

    // Open windows certificate store
    var store = new ActiveXObject("CAPICOM.Store");
    store.Open(CAPICOM_CURRENT_USER_STORE, "My", CAPICOM_STORE_OPEN_READ_ONLY);

    // Show personal certificates which are installed for this user
    var certificates = store.Certificates.Select("Firma Digital", "Por favor seleccione el certificado a utilizar.");

    // Proceed if any certificate is selected
    if (certificates.Count > 0) {
        var a;
   
        a = certificates.Item(1);
      
        if (a.IsValid() == true)
        {
           
            var LISTA = getParameterByName('LISTA');
            var ID = getParameterByName('ID');

         
         
            var options = {
                url: '/_layouts/FirmaDigital/Firmante.aspx?ID=' + ID + '&LISTA=' + LISTA + '&FIRMANTE=' + a.GetInfo(6) + "&EMISOR=" + a.GetInfo(1)+'&IsDlg=1',
                width: 500,
                height: 200,
                showClose:true,


            };
         
            SP.SOD.execute('sp.ui.dialog.js', 'SP.UI.ModalDialog.showModalDialog', options);
          

        }
        else
        {
           $(".modal").css('display', 'block');
            $("#guardar").text('Certificado no válido, por favor comuníquese con su emisor.') ;
        
        }
    


return true;
}
    return false;
}
    catch (e) {
        alert(e.description);
        return false;
    }
       }
       function getParameterByName(name) {
           url = window.location.href;
           name = name.replace(/[\[\]]/g, '\\$&');
           var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
               results = regex.exec(url);
           if (!results) return null;
           if (!results[2]) return '';
           return decodeURIComponent(results[2].replace(/\+/g, ' '));
       }
       
</script>
<style>
    table{
        border-collapse:inherit !important;
    }
</style>


</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
   

   <asp:Literal ID="LtFormulario" runat="server"></asp:Literal>
     <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <div class="container">
        <div style="padding: 10px 45px;">
           <button id="btnGenerarPDF" type="button" style="cursor: pointer; color: #fff; background-color: #dc3545; border-color: #dc3545; font-weight: 400; text-align: center; vertical-align: middle; border: 1px solid transparent; padding: .375rem .75rem;font-size: 1rem; line-height: 1.5; border-radius: .25rem; width:150px !important" runat="server" onserverclick="btnGenerarPDF_ServerClick"><i class="fas fa-file-pdf"></i> Generar PDF</button>
             <button id="btoFirmar" type="button" style="cursor: pointer; color: #fff; background-color: #dc3545; border-color: #dc3545; font-weight: 400; text-align: center; vertical-align: middle; border: 1px solid transparent; padding: .375rem .75rem;font-size: 1rem; line-height: 1.5; border-radius: .25rem;  width:150px !important" runat="server" onclick="Authenticate()" ><i class="fas fa-pen-alt"></i> FIRMAR</button>
        
             </div>
        <div class="modal" id="myModal">
             <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background:url('../FirmaDigital/img/Logo.png'); background-position-y: center; background-size:30%; background-color:#0a4e9a; background-position-x: 20px; background-repeat: no-repeat;">
                        <h4 class="modal-title" style="color: white; margin-top: 0px !important; margin-bottom: 0px !important; margin-left: 190px; font-size: 19px; font-weight: bold;">Firma Digital</h4>
                    </div>
                     <div class="modal-body" id="guardar" style="padding: 13px !important;">
                        Su PDF se ha generado correctamente.
                    </div>
                    <div class="modal-footer modalBtn" style="padding: 0.5rem !important">
                        <button type="button" class="btn btn-primary btn-block" id="modalBtn" onclick="cerrar()">Aceptar</button>
                    </div>
                 </div>
            </div>
            
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Firma Digital
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Firma Digital
</asp:Content>
