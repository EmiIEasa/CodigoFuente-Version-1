<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuscadorDePersonalUserControl.ascx.cs" Inherits="GrillaDatosPersonal.BuscadorDePersonal.BuscadorDePersonalUserControl" %>
<link rel="stylesheet" href="../../../../_layouts/15/GrillaDatosPersonal/css/4-3-1-bootstrap.min.css">
<link rel="stylesheet" href="../../../../_layouts/15/GrillaDatosPersonal/css/estilosGrillaDatosPersonal.css">
<script src="../../../../_layouts/15/GrillaDatosPersonal/js/fontAwesome.js"></script>
<script src="../../../../_layouts/15/GrillaDatosPersonal/js/funcionesGrillaDatosPersonal.js"></script>
<script src="../../../../_layouts/15/GrillaDatosPersonal/js/3-4-1-jquery.min.js"></script>
<script src="../../../../_layouts/15/GrillaDatosPersonal/js/1-14-7-popper.min.js"></script>
<script src="../../../../_layouts/15/GrillaDatosPersonal/js/4-3-1-bootstrap.min.js"></script>
<link rel="stylesheet" type="text/css" href="../../../../_layouts/15/GrillaDatosPersonal/css/1-10-20-dataTables.bootstrap4.min.css">
<script type="text/javascript" language="javascript" src="../../../../_layouts/15/GrillaDatosPersonal/js/1-10-20-jquery.dataTables.min.js"></script>
<script type="text/javascript" language="javascript" src="../../../../_layouts/15/GrillaDatosPersonal/js/1-10-20-dataTables.bootstrap4.min.js"></script>

<div class="container grillas">
    <asp:UpdatePanel ID="UpdatePanel9" runat="server"><ContentTemplate>
    
        <div class="row py-2">
        <div class="col-md-4">
            <div class="form-inline">
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text">Seleccione Campo</span>
                    </div>
                    <asp:DropDownList CssClass="form-control" runat="server" ID="cboCampos">
                        <asp:ListItem Text="-" Value="-"></asp:ListItem>
                        <asp:ListItem Text="Apellido o Nombre" Value="NombreApellido"></asp:ListItem>
                        <asp:ListItem Text="Legajo" Value="Legajo"></asp:ListItem>
                        <asp:ListItem Text="CUIL" Value="CUIL"></asp:ListItem>
                        <asp:ListItem Text="Sociedad" Value="Sociedad"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-md-8">
            <div class="form-inline">
                <div class="input-group" style="width: 80%!important;">
                    <div class="input-group-prepend">
                        <span class="input-group-text">Ingrese Valor Buscado</span>
                    </div>
                    <asp:TextBox runat="server" ID="txtValorB" CssClass="form-control" ></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <button type="button" runat="server" class="btn btn-primary" onserverclick="buscarRegistro"><i class="fas fa-search"></i>  BUSCAR</button>
    <div class="alert alert-warning pt-2 mt-2" runat="server" id="alertaMasCarac" visible="false">
        El resultado de la búsqueda excede los 500 registros, por favor haga una búsqueda más detallada.
    </div>
    <div class="alert alert-warning pt-2 mt-2" runat="server" id="alertaCampoVacio" visible="false">
        Debe completar el <strong>Valor a buscar</strong> para realizar la búsqueda.
      </div>
    <div class="alert alert-primary pt-2 mt-2" runat="server" id="alertaNoResultado" visible="false">
        <strong>No</strong> se encontraron resultados para la búsqueda deseada.
      </div>
    <div class="alert alert-info pt-2 mt-2" runat="server" id="alertaCamposComp" visible="false">
        1- Seleccione <strong>Campo</strong>
        2- Ingrese <strong>Valor a buscar</strong>
        3- Presione <strong>BUSCAR</strong>
    </div>
       <br />
        <asp:Panel ID="pnlTabla" runat="server" Visible="false" CssClass="pnlTabla" ClientIDMode="Static">
            <table id="example" class="table table-striped table-bordered table-hover" style="width:100%; margin-top: 25px;">
				<thead>
					<tr>
						<th>Nombre y Apellido</th>
                        <th>Legajo</th>
                        <th>CUIL</th>
					</tr>
				</thead>
				<tbody>
                    <asp:Literal ID="LtTablaPersonal" runat="server"></asp:Literal>
				</tbody>
			</table>
        </asp:Panel>
    </ContentTemplate></asp:UpdatePanel>
</div>