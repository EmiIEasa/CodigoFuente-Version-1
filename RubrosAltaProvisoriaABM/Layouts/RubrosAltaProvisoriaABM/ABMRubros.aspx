<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ABMRubros.aspx.cs" Inherits="RubrosAltaProvisoriaABM.Layouts.RubrosAltaProvisoriaABM.ABMRubros" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link href="../../../../_layouts/15/RubrosAltaProvisoriaABM/css/4-3-1-bootstrap.min.css" rel="stylesheet" />
    <link href="../../../../_layouts/15/RubrosAltaProvisoriaABM/css/estilosABMRubros.css" rel="stylesheet" />
    <link href="../../../../_layouts/15/RubrosAltaProvisoriaABM/css/tempusdominus-bootstrap-4.min.css" rel="stylesheet" />
    
    <script src="../../../../_layouts/15/RubrosAltaProvisoriaABM/js/fontAwesome.js" crossorigin="anonymous" type="text/javascript"></script>
    <script src="../../../../_layouts/15/RubrosAltaProvisoriaABM/js/1-14-7-popper.min.js" type="text/javascript"></script>
    <script src="../../../../_layouts/15/RubrosAltaProvisoriaABM/js/3-4-1-jquery.min.js" type="text/javascript"></script>
    <script src="../../../../_layouts/15/RubrosAltaProvisoriaABM/js/4-3-1-bootstrap.min.js" type="text/javascript"></script>
    <script src="../../../../_layouts/15/RubrosAltaProvisoriaABM/js/funcionesABMRubros.js" type="text/javascript"></script>
    <script src="../../../../_layouts/15/RubrosAltaProvisoriaABM/js/jquery.MultiFile.js" type="text/javascript"></script>
    <script src="../../../../_layouts/15/RubrosAltaProvisoriaABM/js/moment-with-locales.js" type="text/javascript"></script>
    <script src="../../../../_layouts/15/RubrosAltaProvisoriaABM/js/tempusdominus-bootstrap-4.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="container contenedor rounded-lg" style="padding-bottom: 15px" id="formPrincipal">
        <div class="row text-center px-0 banner">
            <div class="col-3"><img width="150" class="img-fluid" src="../../../../_layouts/15/RubrosAltaProvisoriaABM/img/Logo.png" alt="Logo - IEASA"></div>
            <div class="col-7 text-center align-middle" style="color: white; font-family: Ubuntu; font-size: 42px; font-weight: 700;">ABM Rubros</div>
        </div>
        <div class="row" runat="server" id="rowID">
            <div class="col-sm-2 offset-sm-9 mt-1">
                <div class="form-inline float-right">
                    <div class="input-group w-100">
                        <div class="input-group-prepend">
                            <span class="input-group-text">ID</span>
                        </div>
                        <asp:TextBox runat="server" ID="txtID" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="row justify-content-center" >
            <div class="col-10 mt-1">
                <div class="form-inline">
                    <div class="input-group w-100">
                        <div class="input-group-prepend">
                            <span class="input-group-text">Rama 01</span>
                        </div>
                        <asp:TextBox runat="server" ID="txtRubro01" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-10 mt-1">
                <div class="form-inline">
                    <div class="input-group w-100">
                        <div class="input-group-prepend">
                            <span class="input-group-text">Rama 02</span>
                        </div>
                        <asp:TextBox runat="server" ID="txtRubro02" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-10 mt-1">
                <div class="form-inline">
                    <div class="input-group w-100">
                        <div class="input-group-prepend">
                            <span class="input-group-text">Rama 03</span>
                        </div>
                        <asp:TextBox runat="server" ID="txtRubro03" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-10 mt-1">
                <div class="form-inline">
                    <div class="input-group w-100">
                        <div class="input-group-prepend">
                            <span class="input-group-text">Rama 04</span>
                        </div>
                        <asp:TextBox runat="server" ID="txtRubro04" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-10 mt-1">
                <div class="form-inline">
                    <div class="input-group w-100">
                        <div class="input-group-prepend">
                            <span class="input-group-text">Estado</span>
                        </div>
                        <asp:DropDownList CssClass="form-control" runat="server" ID="cboEstado">
                            <asp:ListItem Text="Activo" Value="Activo"></asp:ListItem>
                            <asp:ListItem Text="No Activo" Value="No Activo"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="btn-group btn-block mt-3" id="btnGroupID">
            <button onserverclick="btnGuardar" id="btnGuardarID" type="button" class="btn btn-primary" runat="server"><i class="fas fa-download" aria-hidden="true"></i>  Guardar</button>
            <button type="button" id="btnCerrar4"  class="btn btn-secondary" onclick="cerrar()"><i class="far fa-window-close" aria-hidden="true"></i>  Cerrar</button>
        </div>
        <div class="modal" id="myModal">
             <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background:url('../RubrosAltaProvisoriaABM/img/Logo.png'); background-position-y: center; background-size:30%; background-color:#0a4e9a; background-position-x: 20px; background-repeat: no-repeat;">
                        <h4 class="modal-title" style="color: white; margin-top: 0px !important; margin-bottom: 0px !important; margin-left: 190px; font-size: 19px; font-weight: bold;">ABM Rubros</h4>
                    </div>
                     <div class="modal-body" id="guardar" style="padding: 13px !important;">
                        La información se guardó correctamente.
                    </div>
                    <div class="modal-footer" style="padding: 0.5rem !important">
                        <button type="button" class="btn btn-primary btn-block" id="modalBtnAN" onclick="cerrar()">Aceptar</button>
                    </div>

                 </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
ABM Rubros
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
ABM Rubros
</asp:Content>
