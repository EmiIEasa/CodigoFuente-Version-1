<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WP-ContabilidadUserControl.ascx.cs" Inherits="GrillaSegFact.WP_Contabilidad.WP_ContabilidadUserControl" %>
<link rel="stylesheet" href="../../../../_layouts/15/GrillaSegFact/css/4-3-1-bootstrap.min.css">
<script src="../../../../_layouts/15/GrillaSegFact/js/fontAwesome.js"></script>
<script src="../../../../_layouts/15/GrillaSegFact/js/3-4-1-jquery.min.js"></script>
<script src="../../../../_layouts/15/GrillaSegFact/js/1-14-7-popper.min.js"></script>
<script src="../../../../_layouts/15/GrillaSegFact/js/4-3-1-bootstrap.min.js"></script>
<link rel="stylesheet" type="text/css" href="../../../../_layouts/15/GrillaSegFact/css/1-10-20-dataTables.bootstrap4.min.css">
<script type="text/javascript" language="javascript" src="../../../../_layouts/15/GrillaSegFact/js/1-10-20-jquery.dataTables.min.js"></script>
<script type="text/javascript" language="javascript" src="../../../../_layouts/15/GrillaSegFact/js/1-10-20-dataTables.bootstrap4.min.js"></script>

<script type="text/javascript" class="init">
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
		window.open("../../_layouts/15/SegFact/registro.aspx", "_self")
	}
	$(function () {
		bindDataTable();
		Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
	});
	function bindDataTable() {
		$('.table').DataTable(
		{
			"language": idioma_espanol,
			"lengthMenu": [[5, 10, 15, 20, 25, 50, -1], [5, 10, 15, 20, 25, 50, "Todos"]],
			"iDisplayLength": 15,
			"order": [[0, "desc"]],
			"orderCellsTop": true,
			"initComplete": function () {
			    this.api()
                    .columns([1])
                    .every(function () {
                        var column = this;
                        var select = $(
                        '<select class="form-control" style="font-size: 11px !important; padding: .375rem .175rem !important;"><option value=""></option></select>'
                        )
                        // .appendTo( $(column.footer()).empty() )
                        .appendTo($(column.header()))
                        .on("change", function () {
                            var val = $.fn.dataTable.util.escapeRegex($(this).val());
                            column.search(val ? "^" + val + "$" : "", true, false).draw();
                        });

                        column
                        .data()
                        .unique()
                        .sort()
                        .each(function (d, j) {
                            select.append('<option value="' + d + '">' + d + "</option>");
                        });
                    });
			}
		});
    }
    function validaCampos() {
        $("#alertaBigData").css('display', 'none');
        var textoError = '';
        var retorna = true;
        var cboEstado = $(".cboEstado").val();
        if (cboEstado == '-') {
            $("#alertaBigData").css('display', 'block');
            $("#alertaBigData").html("<p>Seleccione Estado nuevo</p>");
        } 
        else {
            $("#alertaBigData").css('display', 'none');
            $("#lblComentarioRechazo").css('display', 'none');
            $(".txtRechazo").html("");
            $(".modalG").css('display', 'block');
        }
    }
    function validaComentarioRechazo() {
        var txtRechazo = $(".txtRechazo").val();
        var cboEstado = $(".cboEstado").val();
        if (cboEstado == 'RECHAZADO' && txtRechazo == '') {
            $("#lblComentarioRechazo").css('display', 'block');
            $("#lblComentarioRechazo").html("Complete motivo de rechazo");
            return false;
        }
        else {
            return true;
        }
    }
    function cerrarModal() {
        $(".modalG").css('display', 'none');
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
    <asp:UpdatePanel ID="UpdatePanel1" updatemode="Always" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="HiddenField1" runat="server"  />
     <%--   <asp:TextBox ID="TextBox1"  runat="server"></asp:TextBox>
        <asp:Label ID="Label1" runat="server"   Text="Label"></asp:Label>
        <asp:Label ID="Label2" runat="server"   Text="Label"></asp:Label>--%>
        <div class="row">
            <div class="col-4 my-1">
                <div class="form-inline">
                    <div class="input-group w-100">
                        <div class="input-group-prepend">
                            <span class="input-group-text">Reemplazar por</span>
                        </div>
                        <asp:DropDownList CssClass="form-control cboEstado" runat="server" ID="cboEstado">
                            <asp:ListItem Text="-" Value="-"></asp:ListItem>
                            <asp:ListItem Text="CONTABILIZADO" Value="CONTABILIZADO"></asp:ListItem>
                            <asp:ListItem Text="PENDIENTE DE RECEPCIÓN" Value="PENDIENTE DE RECEPCIÓN"></asp:ListItem>
                            <asp:ListItem Text="RECIBIDO" Value="RECIBIDO"></asp:ListItem>
                            <asp:ListItem Text="RECHAZADO" Value="RECHAZADO"></asp:ListItem>
                            <%--<asp:ListItem Text="TESORERIA" Value="TESORERIA"></asp:ListItem>--%>
                            <%--<asp:ListItem Text="FINALIZADO" Value="FINALIZADO"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-4 col-sm-2">
                <%--<asp:Button  ID="btnCambiarEstado" CssClass="btn btn-primary verDiasSeleccionados" OnClick="btnCambiarEstado_ServerClick" runat="server" Text="Cambiar estado" />--%>
                <button id="btnCambiarEstado" onclick="validaCampos()" type="button" class="btn btn-primary verDiasSeleccionados"><i class="fas fa-download" aria-hidden="true"></i>  Cambiar estado</button>
            </div>
            <div class="alert alert-warning" id="alertaBigData" style="display:none;margin-top: 5px;"></div>
            <div class="modal modalG" id="myModal">
                 <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="background:url('../../../_layouts/15/GrillaSegFact/img/Logo.png'); background-position-y: center; background-size:25%; background-color:#0a4e9a; background-position-x: 20px; background-repeat: no-repeat;">
                            <h4 class="modal-title" style="color: white; margin-top: 0px !important; margin-bottom: 0px !important; margin-left: 190px; font-size: 19px; font-weight: bold;">Seguimiento de Facturas</h4>
                        </div>
                         <div class="modal-body" id="guardarG" style="padding: 13px !important;">
                             <h6><label id="lblConfirma">¿Confirma que desea modificar los registros seleccionados?</label></h6>
                             
                             <asp:TextBox runat="server" ID="txtRechazo" CssClass="form-control txtRechazo" TextMode="MultiLine"></asp:TextBox>
                             
                            <h6><div class="alert alert-warning" id="lblComentarioRechazo" style="display:none;margin-top: 5px;"></div>
                                <h6></h6>
                                <%--<asp:Label runat="server" Text="¿Confirma que desea modificar los registros seleccionados?"></asp:Label>--%>
                                <h6></h6>
                                <h6></h6>
                                <h6></h6>
                             </h6>

                        </div>
                        <div class="modal-footer modalBtnT btn-group btn-block" style="padding: 0.5rem !important">
                            <button type="button" class="btn btn-secondary " id="modalCerrar" onclick="cerrarModal()">Cancelar</button>
                            <button type="button" class="btn btn-primary " id="modalGuardar" onclick="if (!validaComentarioRechazo()) return;" onserverclick="BtnCambiarEstado_ServerClick" runat="server">Confirmar</button>
                        </div>
                     </div>
                </div>
            </div>
        </div>
        <asp:Timer ID="Timer1" runat="server" OnTick="GetTime" Interval="60000"></asp:Timer>
            <div class="row">
                <div class="col-4">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text">Última actualización</span>
                        </div>
                        <asp:TextBox runat="server" ID="txtHoraActualizacion" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        <div class="input-group-append">
                            <span class="input-group-text"><i class="fa fa-clock-o" aria-hidden="true"></i></span>
                        </div>
                    </div>
                </div>
            </div>
            <ul class="nav nav-pills nav-justified" role="tablist">
                <li class="nav-item bg-light">
                    <a class="nav-link active" id="pestReque" data-toggle="pill" href="#home"><i class="far fa-file"></i> Con Orden de Compra</a>
                </li>
                <li class="nav-item bg-light">
                    <a class="nav-link" id="pestPenMin" data-toggle="pill" href="#menu1"><span class="fa-stack"><i class="far fa-file fa-stack-1x"></i><i class="fas fa-ban fa-stack-2x"></i></span> Sin Orden de Compra</a>
                </li>
            </ul>
            <div class="tab-content">
                <div id="home" class="container-fluid tab-pane active"><br>
                    <table id="example" class="table table-striped table-bordered table-hover" style="width:100%">
	                    <thead>
		                    <tr>
			                    <th>ID</th>
                                <th style="width:115px">Estado</th>
                                <th>Razón Social</th>
                                <th>CUIT</th>
                                <th>Fecha de comprobante</th>
                                <th>Fecha de recepción</th>
                                <th>N° Factura</th>
                                <th>N° OC</th>
                                <th>Monto</th>
                                <th>Sociedad</th>
                                <th>Observaciones</th>
                                <th>Observaciones Contabilidad</th>
                                <th>N° OP</th>
                                <th>N° Contabilizado</th>
		                    </tr>
	                    </thead>
	                    <tbody>
                            <asp:Literal ID="ltTablaContabilidadConOC" runat="server"></asp:Literal>
	                    </tbody>
                    </table>
                </div>
                <div id="menu1" class="container-fluid tab-pane fade"><br>
                    <table id="example" class="table table-striped table-bordered table-hover" style="width:100%">
	                    <thead>
		                    <tr>
			                    <th>ID</th>
                                <th style="width:115px">Estado</th>
                                <th>Razón Social</th>
                                <th>CUIT</th>
                                <th>Fecha de comprobante</th>
                                <th>Fecha de recepción</th>
                                <th>N° Factura</th>
                                <th>N° OC</th>
                                <th>Monto</th>
                                <th>Sociedad</th>
                                <th>Observaciones</th>
                                <th>Observaciones Contabilidad</th>
                                <th>N° OP</th>
                                <th>N° Contabilizado</th>
		                    </tr>
	                    </thead>
	                    <tbody>
                            <asp:Literal ID="ltTablaContabilidadSinOC" runat="server"></asp:Literal>
            	        </tbody>
                    </table>
                </div>
            </div>
       </ContentTemplate>
      
    </asp:UpdatePanel>  
    <script>
    $(document).ready(function() {
        $('input[type=checkbox]').on('change', function (e) {
            var diasSeleccionados = new Array();
                $('input[type=checkbox]:checked').each(function () {
                             diasSeleccionados.push($(this).val());
                         });
            <%--document.getElementById("<%=TextBox1.ClientID%>").value = diasSeleccionados;--%>
            document.getElementById("<%=HiddenField1.ClientID%>").value = diasSeleccionados;
        });
    });
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function() {
        $('input[type=checkbox]').on('change', function (e) {
            var diasSeleccionados = new Array();
            $('input[type=checkbox]:checked').each(function () {
                diasSeleccionados.push($(this).val());
            });
            <%--document.getElementById("<%=TextBox1.ClientID%>").value = diasSeleccionados;--%>
            document.getElementById("<%=HiddenField1.ClientID%>").value = diasSeleccionados;
        });
    });
    </script>
    
</div>
