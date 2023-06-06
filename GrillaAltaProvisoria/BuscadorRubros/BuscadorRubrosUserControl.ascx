<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuscadorRubrosUserControl.ascx.cs" Inherits="GrillaAltaProvisoria.BuscadorRubros.BuscadorRubrosUserControl" %>
<link rel="stylesheet" href="../../../../_layouts/15/GrillaAltaProvisoria/css/4-3-1-bootstrap.min.css">
<script src="../../../../_layouts/15/GrillaAltaProvisoria/js/fontAwesome.js"></script>
<script src="../../../../_layouts/15/GrillaAltaProvisoria/js/3-4-1-jquery.min.js"></script>
<script src="../../../../_layouts/15/GrillaAltaProvisoria/js/1-14-7-popper.min.js"></script>
<script src="../../../../_layouts/15/GrillaAltaProvisoria/js/4-3-1-bootstrap.min.js"></script>
<link rel="stylesheet" type="text/css" href="../../../../_layouts/15/GrillaAltaProvisoria/css/1-10-20-dataTables.bootstrap4.min.css">
<script type="text/javascript" language="javascript" src="../../../../_layouts/15/GrillaAltaProvisoria/js/1-10-20-jquery.dataTables.min.js"></script>
<script type="text/javascript" language="javascript" src="../../../../_layouts/15/GrillaAltaProvisoria/js/1-10-20-dataTables.bootstrap4.min.js"></script>

<script src="../../../../_layouts/15/GrillaAltaProvisoria/js/chosen.jquery.js" type="text/javascript"></script>
<%--    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>--%>

<script type="text/javascript" class="init">
    $(document).ready(function () {
		    $('.table').DataTable(
			{
				"language": idioma_espanol,
				"lengthMenu": [[5, 10, 15, 20, 25, 50, -1], [5, 10, 15, 20, 25, 50, "Todos"]],
				"iDisplayLength": 15,
				"order": [[0, "desc"]],
				    
			});
		});
		
		var idioma_espanol = {
		    "sProcessing": "Procesando...",
		    "sLengthMenu": "Mostrar _MENU_ registros",
		    "sZeroRecords": "No se encontraron resultados",
		    "sEmptyTable": "Ningún dato disponible en esta tabla",
		    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
		    "sInfoEmpty": "",
		    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
		    "sInfoPostFix": "",
		    "sSearch": "Buscar:",
		    "sUrl": "",
		    "sInfoThousands": ",",
		    "sLoadingRecords": "Cargando...",
		    "oPaginate": {
		        "sFirst": "Primero",
		        "sLast": "Último",
		        "sNext": "Siguiente",
		        "sPrevious": "Anterior"
		    },
		    "oAria": {
		        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
		        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
		    },
		    "buttons": {
		        "copy": "Copiar",
		        "colvis": "Visibilidad"
		    }
		}
		function abreFormReq() {
		    //location.replace('../../_layouts/15/ReclamosPPT/registro.aspx');
		    window.open("../../_layouts/15/Ieasa/AltaProvisoria.aspx", "_self")
		}
		jQuery(document).ready(function () {

		    jQuery.getScript("//harvesthq.github.io/chosen/chosen.jquery.js")
                .done(function (script, textStatus) {
                    jQuery(".chosen1").chosen();

                })
                .fail(function (jqxhr, settings, exception) {
                    alert("Error");
                });

		});
</script>
<style>
       #pageContentTitle{
            display:none !important;
        }
#contentBox {
    margin-right: 20px;
    margin-left: 20px !important;
}
     .tooltip-inner{
         background: red;
     }
     table {
         font-size: 11px;
         font-family: Open Sans, sans-serif;
     }
     
     .tab-content,.nav-link.active{
         color: #000 !important;
      background-color:#EEEEEE !important;

  }
     .nav-link {
    height: 54px !important;
}
	    .nav-pills .nav-link.active, .nav-pills .show > .nav-link {
	        color: #000 !important;
	    }
        .pnlTabla{
            margin-top:25px;
        }
    
        
    </style>
        <link rel="stylesheet" href="../../../../_layouts/15/GrillaAltaProvisoria/css/chosen.css" />
	
<div class="container">
    <div class="row py-2">
        <div class="col-md-12 mb-3">
            <div class="alert alert-secondary">
                Buscador de Registros
            </div>
        </div>
        <div class="col-12">
            <div class="form-inline">
                <div class="input-group w-100">
                    <div class="input-group-prepend">
                        <span class="input-group-text">Rubro</span>
                    </div>
                    <asp:DropDownList CssClass="form-control chosen1" runat="server" ID="cboRama1" OnSelectedIndexChanged="cboRama1_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        
    </div>
    <button type="button" runat="server" class="btn btn-primary btnBuscar" onserverclick="buscarRegistro" disabled="disabled" id="btnBuscar"><i class="fas fa-search"></i>  BUSCAR</button>
    <div class="alert alert-warning pt-2 mt-2" runat="server" id="alertaMasCarac" visible="false">
        <!--El resultado de la búsqueda excede los 500 registros, por favor haga una búsqueda más detallada.-->
        No existe registro con el rubro seleccionado.
    </div>
    
       <br />
        <asp:Panel ID="pnlTabla" runat="server" Visible="false" CssClass="pnlTabla">
            <table id="example" class="table table-striped table-bordered table-hover" style="width:100%; margin-top: 25px;">
				<thead>
					<tr>
						<th>ID</th>
                        <th>Rubro</th>
                        <th>Estado</th>
                        <th>Nombre Fantasía</th>
                        <th>Razón Social</th>
                        
                        <th>Actividad Principal</th>
					</tr>
				</thead>
				<tbody>
                    <asp:Literal ID="LtTablaRama" runat="server"></asp:Literal>
				</tbody>
			</table>
        </asp:Panel>
    
</div>