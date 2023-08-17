<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ComprasUserControl.ascx.cs" Inherits="GrillaIeasa.Compras.ComprasUserControl" %>
<link rel="stylesheet" href="../../../../_layouts/15/GrillaIeasa/css/4-3-1-bootstrap.min.css?version=2.0">
<script src="../../../../_layouts/15/GrillaIeasa/js/fontAwesome.js?version=2.0"></script>
<script src="../../../../_layouts/15/GrillaIeasa/js/3-4-1-jquery.min.js?version=2.0"></script>
<script src="../../../../_layouts/15/GrillaIeasa/js/1-14-7-popper.min.js?version=2.0"></script>
<script src="../../../../_layouts/15/GrillaIeasa/js/4-3-1-bootstrap.min.js?version=2.0"></script>
<link rel="stylesheet" type="text/css" href="../../../../_layouts/15/GrillaIeasa/css/1-10-20-dataTables.bootstrap4.min.css?version=2.0">
<script type="text/javascript" language="javascript" src="../../../../_layouts/15/GrillaIeasa/js/1-10-20-jquery.dataTables.min.js?version=2.0"></script>
<script type="text/javascript" language="javascript" src="../../../../_layouts/15/GrillaIeasa/js/1-10-20-dataTables.bootstrap4.min.js?version=2.0"></script>

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
    <ul class="nav nav-pills nav-justified" role="tablist">
        <li class="nav-item bg-light">
            <a class="nav-link active" id="pestPendientee" data-toggle="pill" href="#pendiente"><i class="far fa-file"></i> Pendiente</a>
        </li>
        <li class="nav-item bg-light">
            <a class="nav-link" id="pestAprobado" data-toggle="pill" href="#aprobado"><i class="far fa-file"></i> Aprobado</a>
        </li>
        <li class="nav-item bg-light">
            <a class="nav-link" id="pestRechazado" data-toggle="pill" href="#rechazado"><i class="far fa-file"></i> Rechazado</a>
        </li>
        <li class="nav-item bg-light">
            <a class="nav-link" id="pestSubsanado" data-toggle="pill" href="#subsanado"><i class="far fa-file"></i> Subsanando</a>
        </li>
        <li class="nav-item bg-light">
            <a class="nav-link" id="pestSuspbloq" data-toggle="pill" href="#suspbloq"><i class="far fa-file"></i> Suspendidos/Bloqueados</a>
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
                        <th>Personería</th>
                        <th>Actividad Principal</th>
                        <th>Rama 1</th>
                        <th>Rama 2</th>
                        <th>Rama 3</th>
                        <th>Rama 4</th>
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
                        <th>Personería</th>
                        <th>Actividad Principal</th>
                        <th>Rama 1</th>
                        <th>Rama 2</th>
                        <th>Rama 3</th>
                        <th>Rama 4</th>
				    </tr>
			    </thead>
			    <tbody>
                    <asp:Literal ID="ltTablaAprobado" runat="server"></asp:Literal>
			    </tbody>
		    </table>
	    </div>
        <div id="rechazado" class="container-fluid tab-pane fade"><br>
	        <table id="example2" class="table table-striped table-bordered table-hover" style="width:100%">
			    <thead>
				    <tr>
					    <th>ID</th>
                        <th>Razón Social</th>
                        <th>Nombre Fantasía</th>
                        <th>Personería</th>
                        <th>Actividad Principal</th>
                        <th>Rama 1</th>
                        <th>Rama 2</th>
                        <th>Rama 3</th>
                        <th>Rama 4</th>
				    </tr>
			    </thead>
			    <tbody>
                    <asp:Literal ID="ltTablaRechazado" runat="server"></asp:Literal>
			    </tbody>
		    </table>
	    </div>
        <div id="subsanado" class="container-fluid tab-pane fade"><br>
	        <table id="example3" class="table table-striped table-bordered table-hover" style="width:100%">
			    <thead>
				    <tr>
					    <th>ID</th>
                        <th>Razón Social</th>
                        <th>Nombre Fantasía</th>
                        <th>Personería</th>
                        <th>Actividad Principal</th>
                        <th>Rama 1</th>
                        <th>Rama 2</th>
                        <th>Rama 3</th>
                        <th>Rama 4</th>
				    </tr>
			    </thead>
			    <tbody>
                    <asp:Literal ID="ltTablaSubsanados" runat="server"></asp:Literal>
			    </tbody>
		    </table>
	    </div>
        <div id="suspbloq" class="container-fluid tab-pane fade"><br>
	        <table id="example4" class="table table-striped table-bordered table-hover" style="width:100%">
			    <thead>
				    <tr>
					    <th>ID</th>
                        <th>Razón Social</th>
                        <th>Nombre Fantasía</th>
                        <th>Personería</th>
                        <th>Actividad Principal</th>
                        <th>Rama 1</th>
                        <th>Rama 2</th>
                        <th>Rama 3</th>
                        <th>Rama 4</th>
				    </tr>
			    </thead>
			    <tbody>
                    <asp:Literal ID="ltTablaSuspBloq" runat="server"></asp:Literal>
			    </tbody>
		    </table>
	    </div>
    </div>
</div>