<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MisDatosUserControl.ascx.cs" Inherits="GrillaDatosPersonal.MisDatos.MisDatosUserControl" %>
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

<div class="container-fluid grillas">
    <table id="example" class="table table-striped table-bordered table-hover" style="width:100%; margin-top: 25px;">   
		<thead style="background-color:white;">
			<tr>
				<th>Nombre y Apellido</th>
                <th>Legajo</th>
                <th>CUIL</th>
			</tr>
		</thead>
		<tbody>
            <asp:Literal ID="ltTablaMisDatos" runat="server"></asp:Literal>
		</tbody>
	</table>
</div>