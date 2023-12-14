<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaProvisoria.aspx.cs" Inherits="Ieasa.Layouts.Ieasa.AltaProvisoria" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link href="../../../../_layouts/15/ENARSA/css/4-3-1-bootstrap.min.css?version=4.0" rel="stylesheet" />
    <link href="../../../../_layouts/15/ENARSA/css/estilosIeasa.css?version=4.0" rel="stylesheet" />
    <link href="../../../../_layouts/15/ENARSA/css/tempusdominus-bootstrap-4.min.css?version=4.0" rel="stylesheet" />

    <script src="../../../../_layouts/15/ENARSA/js/3-4-1-jquery.min.js?version=4.0" type="text/javascript"></script>
    
    <script src="../../../../_layouts/15/ENARSA/js/fontAwesome.js?version=4.0" crossorigin="anonymous" type="text/javascript"></script>
    <script src="../../../../_layouts/15/ENARSA/js/1-14-7-popper.min.js?version=4.0" type="text/javascript"></script>
    <script src="../../../../_layouts/15/ENARSA/js/4-3-1-bootstrap.min.js?version=4.0" type="text/javascript"></script>
    <script src="../../../../_layouts/15/ENARSA/js/funcionesIeasa.js?version=4.0" type="text/javascript"></script>
    <script src="../../../../_layouts/15/ENARSA/js/jquery.MultiFile.js?version=4.0" type="text/javascript"></script>
    <script src="../../../../_layouts/15/ENARSA/js/moment-with-locales.js?version=4.0" type="text/javascript"></script>
    <script src="../../../../_layouts/15/ENARSA/js/tempusdominus-bootstrap-4.js?version=4.0" type="text/javascript"></script>

    <script src="../../../../_layouts/15/ENARSA/js/chosen.jquery.js?version=4.0" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    <style>
		a img{border: none;}
		ol li{list-style: decimal outside;}
		div#container{width: 780px;margin: 0 auto;padding: 1em 0;}
		div.side-by-side{width: 100%;margin-bottom: 1em;}
		div.side-by-side > div{float: left;width: 50%;}
		div.side-by-side > div > em{margin-bottom: 10px;display: block;}
		.clearfix:after{content: "\0020";display: block;height: 0;clear: both;overflow: hidden;visibility: hidden;}
		.chzn-container{
            width: 870px !important;
		}
        .chzn-drop {
            width: 869px !important;
        }
        div.chzn-search > input {
            width: 838px !important;
        }
        .nav-link{
            padding: 0.5rem .05rem !important;
        }
	</style>
	<link rel="stylesheet" href="../../../../_layouts/15/ENARSA/css/chosen.css?version=1.0" />
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="container contenedor rounded-lg" style="padding-bottom: 15px" id="formPrincipal">
        <div class="row text-center px-0 banner pb-2">
            <div class="col-3"><img width="150" class="img-fluid" src="../../../../_layouts/15/ENARSA/img/Logo.png" alt="Logo - ENARSA"></div>
            <div class="col-8 text-center align-middle" style="color: white; font-family: Ubuntu; font-size: 28px; font-weight: 700;">Formulario de Alta / Actualización de Datos</div>
        </div>
        <div class="row" >
            <div class="col-1 mt-1">
                <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="#D7D7D7">11/12/2023</asp:Label>
            </div>
            <div class="col-11 mt-1">
                <div class="form-inline float-right">
                    <div class="input-group w-100">
                        <div class="input-group-prepend">
                            <span class="input-group-text">Estado</span>
                        </div>

                        <asp:TextBox runat="server" ID="txtEstado" CssClass="form-control txtEstado" ReadOnly="true">Pendiente</asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <ul class="nav nav-pills nav-justified mt-1" runat="server" id="menuConID" visible="false">
            <asp:Literal ID="LtPestañaCompras" runat="server"></asp:Literal>
          </ul>
        
        <ul class="nav nav-pills nav-justified mt-3"  runat="server" id="menuSinID">
            <li class="nav-item bg-light">
              <a class="nav-link active" id="pestaniaDatosGrales">Datos Generales</a>
            </li>
            <li class="nav-item bg-light">
              <a class="nav-link" id="pestaniaRubros">Rubros</a>
            </li>
            <li class="nav-item bg-light" >
              <a class="nav-link" id="pestaniaDatosContacto" >Datos Contacto</a>
            </li>
            <li class="nav-item bg-light">
              <a class="nav-link" id="pestaniaDatosImpositivos">Datos Impositivos</a>
            </li>
            <li class="nav-item bg-light">
              <a class="nav-link" id="pestaniaAdjuntos">Adjuntos</a>
            </li>
        </ul>
        
          <!-- Tab panes -->
          <div class="tab-content  border border-light border-top-0">
              <!--Datos generales-->
            <div id="home" class="tab-pane active"><br>
                <div class="row" >
                    <div class="col-12 col-sm-5  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Razón Social</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtRazonSocial" CssClass="form-control txtRazonSocial"></asp:TextBox>
                            </div>
                        </div>
                    </div>
            
                    <div class="col-12 col-sm-7 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nombre de Fantasía</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNomFantasia" CssClass="form-control txtNomFantasia"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Personería</span>
                                   
                                </div>
                                <asp:DropDownList CssClass="form-control cboPersoneria" runat="server" ID="cboPersoneria">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-7 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Actividad principal</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtActPrinc" CssClass="form-control txtActPrinc"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-md-6 my-1 fuPersoneriaJur" style="display: none;">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Adjunto</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuPersoneriaJur" CssClass="fuPersoneriaJurAdj"  AllowMultiple="true"></asp:FileUpload>
                                <p class="text-secondary" id="alertDNI" style="display: none;">Adjunte DNI</p>
                                <p class="text-secondary" id="alertEstatuto" style="display: none;">Adjunte Estatuto/Contrato Social</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-6 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Sitio Web</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtSitioWeb" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                <div class="row">

                   
                    <div class="col-md-5 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Pais</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboPais" runat="server" ID="cboPais" AutoPostBack="true" OnSelectedIndexChanged="cboPais_SelectedIndexChanged">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-7 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Provincia</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtProvincia" CssClass="form-control txtProvincia" style="display: none;"></asp:TextBox>
                                <asp:DropDownList CssClass="form-control cboProvincia" runat="server" ID="cboProvincia" >
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                   <div class="col-md-6 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Ciudad</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCiudad" CssClass="form-control txtCiudad"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Cod. Postal</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCodPostal" CssClass="form-control txtCodPostal"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                            
                              </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row">
                    <div class="col-md-5 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Calle</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCalle" CssClass="form-control txtCalle"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Altura</span>
                                </div> 
                                <asp:TextBox runat="server" ID="txtAltura" CssClass="form-control txtAltura" onkeypress="return soloNumeros(event)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Depto</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtDepto" CssClass="form-control txtDepto"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Piso</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtPiso" CssClass="form-control txtPiso" onkeypress="return soloNumeros(event)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--Rubros-->
              <div id="rubros" class="tab-pane fade">
                <div class="row" >
                    <div class="col-12 my-1" runat="server" id="comboRubros">
                        <div class="input-group w-100">
                            <div class="input-group-prepend">
                                <span class="input-group-text" style="width: 94px !important">Rubro</span>
                            </div>
					        <asp:DropDownList runat="server" ID="cboRubro" class="chzn-select form-control">

					        </asp:DropDownList>
                            <p class="text-primary" style="width: 100%;">En caso de no encontrar su categoría, por favor indique "Otros" que el sector de proveedores los estará contactando para adecuar la categoría correcta.</p>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row my-1" style="margin-left: 15px !important" >
                                
                                <div class="col-10" runat="server" id="divListBox" style="overflow-x:auto; padding-left: 0 !important">
                                    <asp:ListBox ID="libServicio" AutoPostBack="true" runat="server"></asp:ListBox>
                                </div>
                                <div class="col-2"  runat="server" id="btnAQ">
                                    <button type="button" class="btn btn-outline-success" id="btoMasServicio" onserverclick="btoMasServicio_Click" runat="server"><i class="fas fa-plus"></i></button>
                                    <button type="button" class="btn btn-outline-danger" id="btoMenosServicio"  runat="server" onserverclick="btoMenosServicio_Click"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                        </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>
              <!--Datos de contacto-->
            <div id="menu1" class="tab-pane fade"><br>
                <div class="row justify-content-md-center">
                    <div class="col-10 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nombre apoderado o representante legal</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtApoderado" CssClass="form-control txtApoderado"></asp:TextBox>
                                
                            </div>
                        </div>
                    </div>
                    <div class="col-10 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Adjuntar Poder</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuApoderado" CssClass="fuApoderado" AllowMultiple="true"></asp:FileUpload>
                                <p class="text-primary" id="txtAdjApoderado"></p>
                            </div>
                        </div>
                    </div>
                    <div class="col-10 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Pesona de Contacto - Ventas</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtVentasContacto" CssClass="form-control txtVentasContacto"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-10 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Dirección de e-mail - Ventas</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtVentasCorreo" CssClass="form-control txtVentasCorreo"></asp:TextBox>
                                <p class="text-primary" style="width: 100%;">Ésta casilla de correo será usada como email principal para las comunicaciones e invitaciones, recomendamos usar una dirección genérica y no personal, así como mantenerla vigente mientras esté dado de alta.</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-10 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Pesona de Contacto - Administración</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtAdministracionContacto" CssClass="form-control txtAdministracionContacto"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-10 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Dirección de e-mail - Administración</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtAdministracionCorreo" CssClass="form-control txtAdministracionCorreo"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-5 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Teléfono</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control txtTelefono" onkeypress="return soloNumeros(event)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-5 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Fax</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtFax" CssClass="form-control txtFax" onkeypress="return soloNumeros(event)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-10 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">DDJJ del 202</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuDDJJ" CssClass="fuDDJJ" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-10 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Designación de autoridades</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuDesignacionAutoridades" CssClass="fuDesignacionAutoridades"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           <!--Datos Impositivos-->
            <div id="menu2" class="tab-pane fade"><br>
                <div class="row">
                    <div class="col-12 col-md-5 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Proveedor</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboProveedor" runat="server" ID="cboProveedor">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-md-4 my-1 divCIE" style="display: none;">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">CIE</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCIE" CssClass="form-control txtCIE" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-md-4 my-1 divNacional" style="display: none;">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">CUIT</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCuit" CssClass="form-control txtCuit" onfocusout="validateCUIT()" placeholder="xxxxxxxxxxx (Sin guiones)" onkeypress="return soloCUIT(event)" MaxLength="11" ></asp:TextBox><span style="color: red; display:none;" class="spanCUITMAL" id="spanCUITMAL"><i class="fas fa-times-circle"></i></span><span style="color: green; display:none;" class="spanBIEN" id="spanBIEN"><i class="fas fa-check-circle"></i></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-md-2 my-1 divNacional" style="display: none;">
                        <div class="form-check form-group mb-3">
                            <input type="checkbox" class="form-check-input chkPersFis" id="chkPersFis" runat="server" clientidmode="Static">
                            <label class="form-check-label" for="chkPersFis">Persona Física</label>
                          </div>
                    </div>
                    <div class="col-12 col-md-7 my-1 divNacional" style="display: none;">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Constancia de CUIT</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuCuit" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-md-10 my-1 divNacional" style="display: none;">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Condición Impositiva(Impuesto a las Ganancias)</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboConImpG" runat="server" ID="cboConImpG">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-md-10 my-1 divNacional" style="display: none;">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Condición Impositiva(IVA)</span>
                                </div>
                                <asp:DropDownList CssClass="form-control" runat="server" ID="cboCondImIVA" clientidmode="Static">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
               
                    <div class="col-12 my-1 divCertExencion" style="display: none;">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Constancia impositiva</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuExencionImpositiva" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-12 my-1 divNacional" style="display: none;">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Certificado de no retención - IVA - Ganancias - SUSS - IIBB</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuNoRetencionIVA" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 my-1 divNacional" style="display: none;">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Agente de retención IVA</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuRetencionIVA" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
               
                <hr />
                <div class="row divNacional" style="display: none;">
                    <div class="col-md-12 text-center align-middle text-white"><h4>INGRESOS BRUTOS</h4></div>
                </div>
                <div class="row divNacional" style="display: none;">
                    <div class="col-12 col-sm-6 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Tipo Ingresos Brutos</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboIngresosBrutos" runat="server" ID="cboIngresosBrutos">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nº Ingresos brutos</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtIngresosBrutos" CssClass="form-control txtIngresosBrutos" onkeypress="return soloNumeros(event)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-8 my-1 adjuntoConvenioMulti" style="display:none">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Convenio Multilateral</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuConvenioMultilateral" CssClass="fuConvenioMultilateral" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
               <%-- </div>
                <div class="row">--%>
                    
                    <div class="col-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Constancia Ingresos Brutos</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuIngresosBrutos" CssClass="fuIngresosBrutos" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    
                </div>
            </div>
          <!--Adjuntos-->
                <div id="menu3" class="tab-pane fade"><br>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                          <table class="table">
                            <tbody>
                    
                                <asp:CheckBoxList runat="server" ID="chkBoxList"></asp:CheckBoxList>
                                </br><button type="button" id="btnEliminar" onserverclick="btnEliminar_ServerClick" class="btn btn-danger btn-sm" runat="server" visible="false"><i class="far fa-trash-alt"></i>  Eliminar</button></br>
                                </br><asp:Label ID="lbMensaje" ForeColor="Red" runat="server" Text=""></asp:Label>
                        
                            </tbody>
                          </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
              <!--SAP -->
               <div id="sap" class="tab-pane fade"><br>
               
                <div class="row">

                        <div class="col-6 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">IDSap </span>
                                </div>
                                 <asp:TextBox runat="server" ID="lbIdSap" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                              
                            </div>
                        </div>
                    </div>  
                    
                    <div class="col-6 my-1">
                        <div class="form-inline ">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Estado SAP</span>
                                </div>

                                <asp:TextBox runat="server" ID="txtEstadoSAP" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>

                    <div class="row">
                           <div class="col-12 my-1">
                        <div class="form-inline ">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Resultado Web Service</span>
                                </div>
                                 <asp:TextBox runat="server" ID="lbSap" TextMode="MultiLine"  CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                
                            </div>
                        </div>
                    </div>
                    
                    </div>
                   <div class="row">
                         <div class="col-7 my-1" >
                              <div class="form-check form-group mb-3">
                            <input type="checkbox" class="form-check-input chkPersFis" id="chkSAP" runat="server"  clientidmode="Static"/>
                            <label class="form-check-label" for="chkPersFis">Pasar a SAP</label>
                          </div>
                        <%--<button type="button" id="idSapBto" class="btn btn-outline-success" onserverclick="btoPasarSap_ServerClick"  runat="server" >Pasar a SAP</button>--%>
                    </div>
                       </div>
              
                          
                             <div class="row">

                        <div class="col-6 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Adjuntos </span>
                                </div>
                                 
                              
                            </div>
                        </div>
                    </div> </div>
                             <table style="width:100%">
  <tr>
    <th>Nombre documento</th>
    <th>Estado</th>
    <th>Fecha</th>
      <th>Eliminado</th>
      
  </tr>
    <asp:Literal ID="ltAdjuntos" runat="server"></asp:Literal>
</table> 
            </div>

         <!--Compras-->   
              <div id="compras" class="tab-pane fade"><br>
                <div class="row">
                    <div class="col-12 col-sm-6 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Usuario inhabilitado</span>
                                </div>
                                <asp:DropDownList CssClass="form-control" runat="server" ID="cboUsuarioBloqueado" OnSelectedIndexChanged="cboUsuarioBloqueado_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                    <asp:ListItem Text="SI" Value="SI"></asp:ListItem>
                                    <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Observaciones</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtObsCompras" CssClass="form-control txtObsCompras" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-3 my-1">
                        <button id="btnAprobarCompras" type="button" class="btn btn-success btn-block" style="height: 62px;" runat="server" onserverclick="btnAprobarCompras_ServerClick" AutoPostBack = "true"><i class="fas fa-check"></i> Aprobar</button>
                    </div>
                     <div class="col-12 col-sm-3 my-1">
                        <button id="btnSubsanarCompras" onclick="if (!validaCamposCompras()) return;" type="button" class="btn btn-warning btn-block" style="height: 62px;" runat="server" onserverclick="btnSubsanandoCompras_ServerClick"><i class="fas fa-wrench"></i> Subsanar</button>
                    </div>
                    <div class="col-12 col-sm-3 my-1">
                        <%--<button id="btnRechazarCompras" onclick="if (!validaCamposCompras()) return;" type="button" class="btn btn-info btn-block" style="height: 62px;" runat="server" onserverclick="btnRechazarCompras_ServerClick"><i class="fas fa-times-circle"></i> Rechazar</button>--%>
                        <button id="btnPreInscribiCompras" onclick="if (!validaCamposCompras()) return;" type="button" class="btn btn-info btn-block" style="height: 62px;" runat="server" onserverclick="btnPreInscribiCompras_ServerClick"><i class="fas fa-times-circle"></i> Pre-Inscribir</button>
                    </div>
                   
                    <div class="col-12 col-sm-3 my-1">
                        <button id="btnSuspenderCompras" onclick="if (!validaCamposCompras()) return;" type="button" class="btn btn-danger btn-block" style="height: 62px;" runat="server" onserverclick="btnSuspenderCompras_ServerClick"><i class="fas fa-lock"></i> Suspendido-Bloqueado</button>
                    </div>
                    <br><br>
                      <asp:Literal ID="LtUsuario" runat="server"></asp:Literal>
                    <div class="alert alert-warning" id="alertaCompras" style="display:none;margin-top: 5px;" runat="server"><asp:Literal ID="ltAlerta" runat="server"></asp:Literal></div>
                  </div>
            </div>
             

          </div>
          <hr>

        <div class="btn-group btn-block" id="btnGroupGrales">
            <button onclick="if (!validaCamposGenerales()) return;" type="button" class="btn btn-primary" runat="server"><i class="fas fa-arrow-right"></i>  Siguiente</button>
            <button type="button" id="btnCerrar1" runat="server" onserverclick="btnCerrar_ServerClick" class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>  Cerrar</button>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="btn-group btn-block" id="btnGroupRubros" style="display:none"  ClientIDMode="Static" runat="server">
                    <button type="button" id="btnVolver" onclick="volverGrales()" class="btn btn-info"><i class="fas fa-arrow-left"></i>  Volver</button>
                    <button onserverclick="btnValidoRubros" type="button" class="btn btn-primary" runat="server"><i class="fas fa-arrow-right"></i>  Siguiente</button>
                    <button type="button" id="btnCerrar" runat="server" onserverclick="btnCerrar_ServerClick"  class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>  Cerrar</button>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="btn-group btn-block" id="btnGroupContacto" style="display:none">
            <button type="button" id="btnVolver1" onclick="volverRubros()" class="btn btn-info"><i class="fas fa-arrow-left"></i>  Volver</button>
            <button onclick="if (!validaCamposContacto()) return;"  type="button" class="btn btn-primary" runat="server"><i class="fas fa-arrow-right"></i>  Siguiente</button>
            <button type="button" id="btnCerrar2" runat="server" onserverclick="btnCerrar_ServerClick"  class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>  Cerrar</button>
        </div>

        <div class="btn-group btn-block" id="btnGroupImpositivo" style="display:none">
            <button type="button" id="btnVolver2" onclick="volverContacto()" class="btn btn-info"><i class="fas fa-arrow-left"></i>  Volver</button>
            <button onclick="if (!validaCamposImpositivos()) return;" onserverclick="btnGuardar" type="button" class="btn btn-primary" runat="server"><i class="fas fa-download" aria-hidden="true"></i>  Guardar</button>
            <button type="button" id="btnCerrar3" runat="server" onserverclick="btnCerrar_ServerClick"  class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>  Cerrar</button>
        </div>

        <div class="btn-group btn-block" id="btnGroupID" style="display:none">
            <button onserverclick="btnGuardar" id="btnGuardarID" type="button" class="btn btn-primary" runat="server"><i class="fas fa-download" aria-hidden="true"></i>  Guardar</button>
            <button type="button" id="btnCerrar4" runat="server" onserverclick="btnCerrar_ServerClick"  class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>  Cerrar</button>
        </div>
        
        <div class="alert alert-warning" id="alertaBigData" style="display:none;margin-top: 5px;"></div>
        <div class="modal" id="myModal">
             <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background:url('../ENARSA/img/Logo.png'); background-position-y: center; background-size:30%; background-color:#0a4e9a; background-position-x: 20px; background-repeat: no-repeat;">
                        <h4 class="modal-title" style="color: white; margin-top: 0px !important; margin-bottom: 0px !important; margin-left: 190px; font-size: 19px; font-weight: bold;">Alta Proveedores</h4>
                    </div>
                     <div class="modal-body" id="guardar" style="padding: 13px !important;">
                        La información se guardó correctamente.
                    </div>
                    <div class="modal-body" id="anonimo" style="padding: 13px !important; display:none">
                        Sus datos han sido cargados exitosamente, en los próximos días efectuaremos la revisión de la documental adjunta, se aprobará o rechazará y recibirá sus credenciales de acceso para actualizar sus datos.
                    </div>
                    <div class="modal-body" id="existeEmpresa" style="padding: 13px !important; display:none">
                        Ya se ha registrado una solicitud de Alta para una persona perteneciente a su empresa.
                    </div>
                    <div class="modal-footer modalBtn" style="padding: 0.5rem !important">
                        <button type="button" class="btn btn-primary btn-block" id="modalBtn" onclick="cerrarCompras()">Aceptar</button>
                    </div>
                    
                    <div class="modal-footer modalBtnC" style="padding: 0.5rem !important">
                        <button type="button" class="btn btn-primary btn-block " id="modalBtnC" onclick="cerrarCompras()">Aceptar</button>
                    </div>
                    <div class="modal-footer modalBtnAN" style="padding: 0.5rem !important">
                        <button type="button" class="btn btn-primary btn-block" id="modalBtnAN" onclick="cerrarAnonimo()">Aceptar</button>
                    </div>
                    <div class="modal-footer modalBtnDatos" style="padding: 0.5rem !important">
                        <button type="button" class="btn btn-primary btn-block" id="modalBtnDatos" onclick="cerrarIrDatos()">Aceptar</button>
                    </div>
                 </div>
            </div>
            
        </div>
      
        
 </div> 

</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Alta Provisoria
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Alta Provisoria
</asp:Content>
