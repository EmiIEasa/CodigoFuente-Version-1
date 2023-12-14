<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreadoPorMiUserControl.ascx.cs" Inherits="GrillaAltaProvisoria.CreadoPorMi.CreadoPorMiUserControl" %>
<link rel="stylesheet" href="../../../../_layouts/15/GrillaAltaProvisoria/css/4-3-1-bootstrap.min.css">
<script src="../../../../_layouts/15/GrillaAltaProvisoria/js/fontAwesome.js"></script>
<script src="../../../../_layouts/15/GrillaAltaProvisoria/js/3-4-1-jquery.min.js"></script>
<script src="../../../../_layouts/15/GrillaAltaProvisoria/js/1-14-7-popper.min.js"></script>
<script src="../../../../_layouts/15/GrillaAltaProvisoria/js/4-3-1-bootstrap.min.js"></script>
<link rel="stylesheet" type="text/css" href="../../../../_layouts/15/GrillaAltaProvisoria/css/1-10-20-dataTables.bootstrap4.min.css">
<script type="text/javascript" language="javascript" src="../../../../_layouts/15/GrillaAltaProvisoria/js/1-10-20-jquery.dataTables.min.js"></script>
<script type="text/javascript" language="javascript" src="../../../../_layouts/15/GrillaAltaProvisoria/js/1-10-20-dataTables.bootstrap4.min.js"></script>

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
		    window.open("../../_layouts/15/ENARSA/AltaProvisoria.aspx", "_self")
		}
		
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
	</style>
<div class="container-fluid" style="margin-top: 25px;">
    <div id="divNuevoForm" runat="server" ClientIDMode="Static">
        <button type="button" class="btn btn-success" onclick="abreFormReq()"><i class="fas fa-plus"></i> Nuevo Formulario de Alta Provisoria</button>
        <br>
        <br>
    </div>
    <!-- Pestañas -->
    <ul class="nav nav-pills nav-justified" role="tablist">
        <li class="nav-item bg-light">
            <a class="nav-link active" id="pestPendientee" data-toggle="pill" href="#pendiente"><i class="fas fa-clock"></i> Pendientes</a>
        </li>
        <li class="nav-item bg-light">
            <a class="nav-link" id="pestAprobado" data-toggle="pill" href="#aprobado"><i class="fas fa-check"></i> Aprobados</a>
        </li>
        <li class="nav-item bg-light">
            <a class="nav-link" id="pestRechazado" data-toggle="pill" href="#rechazado"><i class="far fa-times-circle"></i> Rechazados</a>
        </li>
    </ul>
  <!-- Tabs de pestañas -->
    <div class="tab-content">
        <div id="pendiente" class="container-fluid tab-pane active"><br>
             <table id="example" class="table table-striped table-bordered table-hover" style="width:100%">
				<thead>
					<tr>
						<th>ID</th>
                          <th>Razón Social</th>
                        <th>Nombre Fantasía</th>
                      
                        
                        <th>Actividad Principal</th>
					</tr>
				</thead>
				<tbody>
                    <asp:Literal ID="ltTablaPendiente" runat="server"></asp:Literal>
				</tbody>
			</table>
	    </div>
        <div id="aprobado" class="container-fluid tab-pane fade"><br>
	        <table id="example1" class="table table-striped table-bordered table-hover" style="width:100%">
			    <thead>
				    <tr>
					    <th>ID</th>
                        <th>Razón Social</th>
                        <th>Nombre Fantasía</th>
                        
                        <th>Actividad Principal</th>
				    </tr>
			    </thead>
			    <tbody>
                    <asp:Literal ID="ltTablaAprobado" runat="server"></asp:Literal>
			    </tbody>
		    </table>
	    </div><div id="rechazado" class="container-fluid tab-pane fade"><br>
	        <table id="example2" class="table table-striped table-bordered table-hover" style="width:100%">
			    <thead>
				    <tr>
					    <th>ID</th>
                            <th>Razón Social</th>
                        <th>Nombre Fantasía</th>

                    
                        <th>Actividad Principal</th>
				    </tr>
			    </thead>
			    <tbody>
                    <asp:Literal ID="ltTablaRechazado" runat="server"></asp:Literal>
			    </tbody>
		    </table>
	    </div>
    </div>
</div>