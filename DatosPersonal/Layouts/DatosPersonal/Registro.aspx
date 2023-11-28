<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="DatosPersonal.Layouts.DatosPersonal.Registro" DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register Src="../../../_controltemplates/15/DatosPersonal/Hijos.ascx" TagName="ucHijos" TagPrefix="ucHijos" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link href="../../../../_layouts/15/DatosPersonal/css/4-3-1-bootstrap.min.css" rel="stylesheet" />
    <link href="../../../../_layouts/15/DatosPersonal/css/estilosDatosPersonal.css" rel="stylesheet" />
    <link href="../../../../_layouts/15/DatosPersonal/css/tempusdominus-bootstrap-4.min.css" rel="stylesheet" />

    <script src="../../../../_layouts/15/DatosPersonal/js/3-4-1-jquery.min.js" type="text/javascript"></script>
    
    <script src="../../../../_layouts/15/DatosPersonal/js/fontAwesome.js" crossorigin="anonymous" type="text/javascript"></script>
    <script src="../../../../_layouts/15/DatosPersonal/js/1-14-7-popper.min.js" type="text/javascript"></script>
    <script src="../../../../_layouts/15/DatosPersonal/js/4-3-1-bootstrap.min.js" type="text/javascript"></script>
    <script src="js/funcionesDatosPersonal.js" type="text/javascript" </script>
    <%--<script src="../../../../_layouts/15/DatosPersonal/js/funcionesDatosPersonal.js" type="text/javascript"></script>--%>
    <script src="../../../../_layouts/15/DatosPersonal/js/moment-with-locales.js" type="text/javascript"></script>
    <script src="../../../../_layouts/15/DatosPersonal/js/tempusdominus-bootstrap-4.js" type="text/javascript"></script>
  

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="container contenedor rounded-lg" style="padding-bottom: 15px" id="formPrincipal">
        <div class="row text-center px-0 banner">
            <div class="col-3">
                <img width="150" class="img-fluid" src="../../../../_layouts/15/DatosPersonal/img/Logo.png" alt="Logo - IEASA"></div>
            <div class="col-7 text-center align-middle" style="color: white; font-family: Ubuntu; font-size: 42px; font-weight: 700;">Información Personal</div>
        </div>
        <ul class="nav nav-pills nav-justified mt-1" runat="server" id="menuConID" visible="false">
            <asp:Literal ID="LtPestañaConID" runat="server"></asp:Literal>
        </ul>
        <ul class="nav nav-pills nav-justified mt-3" runat="server" id="menuSinID">
            <li class="nav-item bg-light">
                <a class="nav-link active" id="pestaniaPerfil" runat="server" clientidmode="Static">Perfil</a>
            </li>
            <li class="nav-item bg-light">
                <a class="nav-link" id="pestaniaDatosPersonales" runat="server" clientidmode="Static">Datos Personales</a>
            </li>
            <li class="nav-item bg-light">
                <a class="nav-link" id="pestaniaEstructura">Estructura</a>
            </li>
            <li class="nav-item bg-light">
                <a class="nav-link" id="pestaniaGrupoFamiliar" runat="server" clientidmode="Static">Grupo Familiar</a>
            </li>
            <li class="nav-item bg-light">
                <a class="nav-link" id="pestaniaEstudiosCursados">Estudios Cursados</a>
            </li>
            <%--<li class="nav-item bg-light">
              <a class="nav-link" href='#adjuntos' data-toggle='pill'>Adjuntos</a>
            </li>--%>
        </ul>
        <div class="tab-content  border border-light border-top-0">
            <div id="perfil" class="tab-pane active" runat="server" clientidmode="Static">
                <div class="row">
                    <div class="col-3 p-5">
                        <!-- Foto de perfil redonda rounded-circle -->
                        <%--<img src="https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png" alt="Foto de perfil" class="img-fluid rounded-circle">--%>
                        <asp:Image runat="server" ID="imgPerfil" ImageUrl="img/default-user-image.png" CssClass="img-fluid" Style="max-width: 153px; max-height: 153px;"></asp:Image>
                        <br />
                        <a class="text-secondary pointer" onclick="fotoPerfilVer()">Cambiar Imagen</a>
                    </div>
                    <div class="col-9">
                        <!-- Datos personales -->
                        <div class="row">
                            <div class="col-12 col-sm-9 my-1">
                                <div class="form-inline">
                                    <div class="input-group w-100">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">Nombre y Apellido</span>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtNombreApellido" CssClass="form-control txtNombreApellido"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 col-sm-3 my-1">
                                <div class="form-inline">
                                    <div class="input-group w-100">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">Legajo</span>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtLegajo" CssClass="form-control txtLegajo"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 my-1">
                                <div class="form-inline">
                                    <div class="input-group w-100">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">Posición</span>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtPosicion" CssClass="form-control txtPosicion"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 my-1">
                                <div class="form-inline">
                                    <div class="input-group w-100">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">Gerencia</span>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtGerencia" CssClass="form-control txtGerencia"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 my-1">
                                <div class="form-inline">
                                    <div class="input-group w-100">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">Piso</span>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtPisoPerfil" CssClass="form-control txtPisoPerfil"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--</div>
                        </div>--%>
                    </div>
                    <div class="col-md-12 my-1" id="FotoPerfil" style="display: none;">
                        <asp:FileUpload runat="server" ID="fuFotoPerfil" accept=".png,.jpg,.jpeg," CssClass="fuFotoPerfil"></asp:FileUpload>
                    </div>
                </div>
            </div>
            <div id="datosPersonales" class="tab-pane" runat="server" clientidmode="Static">
                <div class="row">
                    <div class="col-1 mt-1">
                        <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="#D7D7D7">15/09/2023</asp:Label>
                    </div>
                    <div class="col-11 mt-1">
                        <div class="form-inline float-right">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Sociedad</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboSociedad" runat="server" ID="cboSociedad" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Fecha de Nacimiento</span>
                                </div>
                                <SharePoint:DateTimeControl ID="dtFecha" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFecha" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-6 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Lugar de Nacimiento</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtLugarNacimiento" CssClass="form-control txtLugarNacimiento"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-3 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Género</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboGenero" runat="server" ID="cboGenero" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 col-sm-4 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Estado Civil</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboEstadoCivil" runat="server" ID="cboEstadoCivil" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Grupo Sangüineo</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtGrupoSanguineo" CssClass="form-control txtGrupoSanguineo" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-8  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">DNI (Adjuntar frente y dorso)</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuDNI" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-12 col-sm-4  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">CUIL</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCuil" CssClass="form-control txtCuil" ClientIDMode="Static" onfocusout="validateCUIT(0)" placeholder="xxxxxxxxxxx (Sin guiones)" onkeypress="return soloCUIT(event)" MaxLength="11"></asp:TextBox><span style="color: red; display: none;" class="spanCUITMAL0" id="spanCUITMAL"><i class="fas fa-times-circle"></i></span><span style="color: green; display: none;" class="spanBIEN0" id="spanBIEN"><i class="fas fa-check-circle"></i></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Constancia de CUIL</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuCuil"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" runat="server">
                    <div class="col-12 col-sm-3 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Alergias</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboAlergias" runat="server" ID="cboAlergias" ClientIDMode="Static">
                                    <asp:ListItem Text="-" Value="-"></asp:ListItem>
                                    <asp:ListItem Text="SI" Value="SI"></asp:ListItem>
                                    <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-9 my-1 divAlergiaNO" id="divEspecificar" clientidmode="Static" runat="server">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Especificar</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtEspecificar" CssClass="form-control txtEspecificar" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" runat="server">
                    <div class="col-12 col-sm-4 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Licencia de conducir</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboLicenciaConducir" runat="server" ID="cboLicenciaConducir" ClientIDMode="Static">
                                    <asp:ListItem Text="-" Value="-"></asp:ListItem>
                                    <asp:ListItem Text="SI" Value="SI"></asp:ListItem>
                                    <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4 my-1 divLicenciaNO" id="divCtegoria" clientidmode="Static" runat="server">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Categoría</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCategoria" CssClass="form-control txtCategoria" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4 my-1 divLicenciaNO" id="divVigencia" clientidmode="Static" runat="server">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Vigencia</span>
                                </div>
                                <SharePoint:DateTimeControl ID="dtVigenciaLicencia" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtVigenciaLicencia" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8  my-1 divLicenciaNO" id="divAdjunteLicencia" clientidmode="Static" runat="server">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Adjunte licencia</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuLicencia" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
                <hr>
                <div class="row">
                    <div class="col-md-12 text-center align-middle text-white">
                        <h3>Domicilio</h3>
                    </div>
                </div>
                <div class="row my-1">
                    <div class="col-md-6 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Provincia</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboProvincia" runat="server" ID="cboProvincia">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Ciudad</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCiudad" CssClass="form-control txtCiudad"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 my-1">
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
                                    <span class="input-group-text">Piso</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtPiso" CssClass="form-control txtPiso" onkeypress="return soloNumeros(event)"></asp:TextBox>
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
                </div>
                <div class="row">
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
                    <div class="col-md-4 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Teléfono Fijo</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control txtTelefono"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Celular Personal</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCelularPersonal" CssClass="form-control txtCelularPersonal"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Celular de la empresa</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCelularEmpresa" CssClass="form-control txtCelularEmpresa"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Compañía celular de la empresa</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCompaniaCelular" CssClass="form-control txtCompaniaCelular"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <hr>
                <div class="row">
                    <div class="col-md-12 text-center align-middle text-white">
                        <h3>Datos de Emergencia</h3>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nombre y Apellido</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNombreApellidoEmergencia" CssClass="form-control txtNombreApellidoEmergencia"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Vinculo</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtVinculoEmergencia" CssClass="form-control txtVinculoEmergencia"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Celular/Teléfono </span>
                                </div>
                                <asp:TextBox runat="server" ID="txtTelefonoEmergencia" CssClass="form-control txtContactoEmergencia"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="estructura" class="tab-pane fade">
                <div class="row">
                    <div class="col-md-7 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Puesto Actual</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtPuestoActual" CssClass="form-control txtPuestoActual"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text text-left">¿En qué otra área de la Empresa deseás desarrollarte?<br>
                                        ¿Qué te motiva al cambio?<br>
                                        ¿Con qué formación contás para desempeñarte en la misma?</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtPuestoNuevo" CssClass="form-control txtPuestoNuevo" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="grupoFamiliar" class="tab-pane fade" runat="server" clientidmode="Static">
                <div class="row divConyuge" runat="server">
                    <div class="col-md-12 text-center align-middle text-white">
                        <h3 id="txtConyugeConcubinato" runat="server" clientidmode="Static"></h3>
                    </div>
                </div>
                <div class="row my-1 divConyuge" runat="server">
                    <div class="col-12 col-sm-7 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nombre y Apellido</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNombreConyuge" CssClass="form-control txtNombreConyuge"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Fecha de Nacimiento</span>
                                </div>
                                <SharePoint:DateTimeControl ID="dtFechaNacimientoConyuge" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFechaNacimientoConyuge" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-12  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">DNI (Adjuntar frente y dorso)</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuDNIconyuge" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">CUIL</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCuilConyuge" CssClass="form-control txtCuil1" ClientIDMode="Static" onfocusout="validateCUIT(1)" placeholder="xxxxxxxxxxx (Sin guiones)" onkeypress="return soloCUIT(event)" MaxLength="11"></asp:TextBox>
                                <span style="color: red; display: none;" class="spanCUITMAL1" id="spanCUITMALConyuge">
                                    <i class="fas fa-times-circle"></i>
                                </span>
                                <span style="color: green; display: none;" class="spanBIEN1" id="spanBIENConyuge">
                                    <i class="fas fa-check-circle"></i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Constancia de CUIL</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuCUILconyuge"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" runat="server">
                    <div class="col-md-3 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Hijos</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboCantidadHijos" runat="server" ID="cboCantidadHijos" ClientIDMode="Static">
                                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <hr>
                <div class="row my-1 divHijos divHijosTitulo" id="divHijos" runat="server" clientidmode="Static">
                    <div class="col-md-12 text-center align-middle text-white">
                        <h3>Hijos/as</h3>
                    </div>
                </div>
                <div class="row my-1 divHijos divHijos1" id="divHijos1" runat="server" clientidmode="Static">
                    <div class="col-12 col-sm-7 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nombre y Apellido</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNombreApellidoHijos1" CssClass="form-control txtNombreApellidoHijos1"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Fecha de Nacimiento</span>
                                </div>
                                <SharePoint:DateTimeControl ID="dtFechaNacimientoHijos1" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFechaNacimientoHijos1" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">DNI (Adjuntar frente y dorso)</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuDNIHijos1" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Partida de Nacimiento</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuPartidaNacimientoHijos1"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nacionalidad</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNacionalidadHijos1" CssClass="form-control txtNacionalidadHijos1"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">CUIL</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCuilHijos1" CssClass="form-control txtCuil2" ClientIDMode="Static" onfocusout="validateCUIT(2)" placeholder="xxxxxxxxxxx (Sin guiones)" onkeypress="return soloCUIT(event)" MaxLength="11"></asp:TextBox>
                                <span style="color: red; display: none;" class="spanCUITMAL2" id="spanCUITMALHijos1">
                                    <i class="fas fa-times-circle"></i>
                                </span>
                                <span style="color: green; display: none;" class="spanBIEN2" id="spanBIENHijos1">
                                    <i class="fas fa-check-circle"></i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Constancia de CUIL</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuCUILHijos1"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
                <hr class="divHijos divHijos2" id="divhrHijos2" runat="server" clientidmode="Static" />
                <div class="row my-1 divHijos divHijos2" id="divHijos2" runat="server" clientidmode="Static">
                    <div class="col-12 col-sm-7 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nombre y Apellido</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNombreApellidoHijos2" CssClass="form-control txtNombreApellidoHijos2"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Fecha de Nacimiento</span>
                                </div>
                                <SharePoint:DateTimeControl ID="dtFechaNacimientoHijos2" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFechaNacimientoHijos2" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">DNI (Adjuntar frente y dorso)</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuDNIHijos2" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Partida de Nacimiento</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuPartidaNacimientoHijos2"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nacionalidad</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNacionalidadHijos2" CssClass="form-control txtNacionalidadHijos2"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">CUIL</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCuilHijos2" CssClass="form-control txtCuil3" onfocusout="validateCUIT(3)" ClientIDMode="Static" placeholder="xxxxxxxxxxx (Sin guiones)" onkeypress="return soloCUIT(event)" MaxLength="11"></asp:TextBox>
                                <span style="color: red; display: none;" class="spanCUITMAL3" id="spanCUITMALHijos2">
                                    <i class="fas fa-times-circle"></i>
                                </span>
                                <span style="color: green; display: none;" class="spanBIEN3" id="spanBIENHijos2">
                                    <i class="fas fa-check-circle"></i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Constancia de CUIL</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuCUILHijos2"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
                <hr class="divHijos divHijos3" id="divhrHijos3" runat="server" clientidmode="Static" />
                <div class="row my-1 divHijos divHijos3" id="divHijos3" runat="server" clientidmode="Static">
                    <div class="col-12 col-sm-7 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nombre y Apellido</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNombreApellidoHijos3" CssClass="form-control txtNombreApellidoHijos3"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Fecha de Nacimiento</span>
                                </div>
                                <SharePoint:DateTimeControl ID="dtFechaNacimientoHijos3" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFechaNacimientoHijos3" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">DNI (Adjuntar frente y dorso)</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuDNIHijos3" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Partida de Nacimiento</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuPartidaNacimientoHijos3"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nacionalidad</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNacionalidadHijos3" CssClass="form-control txtNacionalidadHijos3"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">CUIL</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCuilHijos3" CssClass="form-control txtCuil4" ClientIDMode="Static" onfocusout="validateCUIT(4)" placeholder="xxxxxxxxxxx (Sin guiones)" onkeypress="return soloCUIT(event)" MaxLength="11"></asp:TextBox>
                                <span style="color: red; display: none;" class="spanCUITMAL4" id="spanCUITMALHijos3">
                                    <i class="fas fa-times-circle"></i>
                                </span>
                                <span style="color: green; display: none;" class="spanBIEN4" id="spanBIENHijos3">
                                    <i class="fas fa-check-circle"></i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Constancia de CUIL</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuCUILHijos3"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
                <hr class="divHijos divHijos4" id="divhrHijos4" runat="server" clientidmode="Static" />
                <div class="row my-1 divHijos divHijos4" id="divHijos4" runat="server" clientidmode="Static">
                    <div class="col-12 col-sm-7 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nombre y Apellido</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNombreApellidoHijos4" CssClass="form-control txtNombreApellidoHijos4"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Fecha de Nacimiento</span>
                                </div>
                                <SharePoint:DateTimeControl ID="dtFechaNacimientoHijos4" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFechaNacimientoHijos4" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">DNI (Adjuntar frente y dorso)</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuDNIHijos4" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Partida de Nacimiento</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuPartidaNacimientoHijos4"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nacionalidad</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNacionalidadHijos4" CssClass="form-control txtNacionalidadHijos4"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">CUIL</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCuilHijos4" CssClass="form-control txtCuil5" ClientIDMode="Static" onfocusout="validateCUIT(5)" placeholder="xxxxxxxxxxx (Sin guiones)" onkeypress="return soloCUIT(event)" MaxLength="11"></asp:TextBox>
                                <span style="color: red; display: none;" class="spanCUITMAL5" id="spanCUITMALHijos4">
                                    <i class="fas fa-times-circle"></i>
                                </span>
                                <span style="color: green; display: none;" class="spanBIEN5" id="spanBIENHijos4">
                                    <i class="fas fa-check-circle"></i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Constancia de CUIL</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuCUILHijos4"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
                <hr class="divHijos divHijos5" id="divhrHijos5" runat="server" clientidmode="Static" />
                <div class="row my-1 divHijos divHijos5" id="divHijos5" runat="server" clientidmode="Static">
                    <div class="col-12 col-sm-7 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nombre y Apellido</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNombreApellidoHijos5" CssClass="form-control txtNombreApellidoHijos5"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Fecha de Nacimiento</span>
                                </div>
                                <SharePoint:DateTimeControl ID="dtFechaNacimientoHijos5" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFechaNacimientoHijos5" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">DNI (Adjuntar frente y dorso)</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuDNIHijos5" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Partida de Nacimiento</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuPartidaNacimientoHijos5"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nacionalidad</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNacionalidadHijos5" CssClass="form-control txtNacionalidadHijos5"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">CUIL</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCuilHijos5" CssClass="form-control txtCuil6" ClientIDMode="Static" onfocusout="validateCUIT(6)" placeholder="xxxxxxxxxxx (Sin guiones)" onkeypress="return soloCUIT(event)" MaxLength="11"></asp:TextBox>
                                <span style="color: red; display: none;" class="spanCUITMAL6" id="spanCUITMALHijos5">
                                    <i class="fas fa-times-circle"></i>
                                </span>
                                <span style="color: green; display: none;" class="spanBIEN6" id="spanBIENHijos5">
                                    <i class="fas fa-check-circle"></i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Constancia de CUIL</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuCUILHijos5"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
                <hr class="divHijos divHijos6" id="divhrHijos6" runat="server" clientidmode="Static" />
                <div class="row my-1 divHijos divHijos6" id="divHijos6" runat="server" clientidmode="Static">
                    <div class="col-12 col-sm-7 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nombre y Apellido</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNombreApellidoHijos6" CssClass="form-control txtNombreApellidoHijos6"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Fecha de Nacimiento</span>
                                </div>
                                <SharePoint:DateTimeControl ID="dtFechaNacimientoHijos6" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFechaNacimientoHijos6" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">DNI (Adjuntar frente y dorso)</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuDNIHijos6" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Partida de Nacimiento</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuPartidaNacimientoHijos6"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nacionalidad</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNacionalidadHijos6" CssClass="form-control txtNacionalidadHijos6"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">CUIL</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCuilHijos6" CssClass="form-control txtCuil7" ClientIDMode="Static" onfocusout="validateCUIT(7)" placeholder="xxxxxxxxxxx (Sin guiones)" onkeypress="return soloCUIT(event)" MaxLength="11"></asp:TextBox>
                                <span style="color: red; display: none;" class="spanCUITMAL7" id="spanCUITMALHijos6">
                                    <i class="fas fa-times-circle"></i>
                                </span>
                                <span style="color: green; display: none;" class="spanBIEN7" id="spanBIENHijos6">
                                    <i class="fas fa-check-circle"></i>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Constancia de CUIL</span>
                                </div>
                                <asp:FileUpload runat="server" ID="fuCUILHijos6"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="estudiosCursados" class="tab-pane fade">

                <div class="accordion" id="accordionPrim">
                    <div class="card">
                        <div class="card-header" data-toggle="collapse" href="#collapsePrim">
                            PRIMARIOS
                        </div>

                        <div id="collapsePrim" class="collapse" aria-labelledby="headingOne" data-parent="#accordionPrim">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12 col-sm-4 my-1">
                                        <div class="form-inline">
                                            <div class="input-group w-100">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Estado</span>
                                                </div>
                                                <asp:DropDownList CssClass="form-control cboEstuidosPrimarios" runat="server" ID="cboEstuidosPrimarios" ClientIDMode="Static">
                                                    <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                    <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                    <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    <asp:ListItem Text="INCOMPLETO" Value="INCOMPLETO"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-8 my-1" id="divAdjuntoPrimarios">
                                        <div class="form-inline">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Constancia</span>
                                                </div>
                                                <asp:FileUpload runat="server" ID="fuConstanciaAlumnoPrimarios"></asp:FileUpload>
                                                <p class="text-primary" id="txtPrimarios"></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-sm-4 my-1">
                                        <div class="form-inline">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Desde</span>
                                                </div>
                                                <SharePoint:DateTimeControl ID="dtDesdePrimarios" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdePrimarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-8  my-1">
                                        <div class="form-inline">
                                            <div class="input-group w-100">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Institución</span>
                                                </div>
                                                <asp:TextBox runat="server" ID="txtInstitucionPrimarios" CssClass="form-control txtInstitucionPrimarios"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-4 my-1">
                                        <div class="form-inline">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Hasta</span>
                                                </div>
                                                <SharePoint:DateTimeControl ID="dtHastaPrimarios" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaPrimarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 col-sm-8  my-1">
                                        <div class="form-inline">
                                            <div class="input-group w-100">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Título</span>
                                                </div>
                                                <asp:TextBox runat="server" ID="txtTituloPrimarios" CssClass="form-control txtTituloPrimarios"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="accordion" id="accordionSec">
                    <div class="card">
                        <div class="card-header" data-toggle="collapse" href="#collapseSec">
                            SECUNDARIOS
                        </div>

                        <div id="collapseSec" class="collapse" aria-labelledby="headingOne" data-parent="#accordionSec">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12 col-sm-4 my-1">
                                        <div class="form-inline">
                                            <div class="input-group w-100">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Estado</span>
                                                </div>
                                                <asp:DropDownList CssClass="form-control cboEstuidosSecundarios" runat="server" ID="cboEstuidosSecundarios" ClientIDMode="Static">
                                                    <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                    <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                    <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    <asp:ListItem Text="INCOMPLETO" Value="INCOMPLETO"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 col-sm-8 my-1" id="divAdjuntoSecundarios">
                                        <div class="form-inline">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Constancia</span>
                                                </div>
                                                <asp:FileUpload runat="server" ID="fuConstanciaAlumnoSecundarios"></asp:FileUpload>
                                                <p class="text-primary" id="txtSecundarios"></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-sm-4 my-1">
                                        <div class="form-inline">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Desde</span>
                                                </div>
                                                <SharePoint:DateTimeControl ID="dtDesdeSecundarios" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeSecundarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-8  my-1">
                                        <div class="form-inline">
                                            <div class="input-group w-100">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Institución</span>
                                                </div>
                                                <asp:TextBox runat="server" ID="txtInstitucionSecundarios" CssClass="form-control txtInstitucionSecundarios"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-4 my-1">
                                        <div class="form-inline">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Hasta</span>
                                                </div>
                                                <SharePoint:DateTimeControl ID="dtHastaSecundarios" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaSecundarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-8  my-1">
                                        <div class="form-inline">
                                            <div class="input-group w-100">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Título</span>
                                                </div>
                                                <asp:TextBox runat="server" ID="txtTituloSecundarios" CssClass="form-control txtTituloSecundarios"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="accordion" id="accordionTer">
                    <div class="card">
                        <div class="card-header" data-toggle="collapse" href="#collapseTer">
                            TERCIARIOS
                        </div>

                        <div id="collapseTer" class="collapse" aria-labelledby="headingOne" data-parent="#accordionTer">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12 col-sm-4 my-1">
                                        <div class="form-inline">
                                            <div class="input-group w-100">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Estado</span>
                                                </div>
                                                <asp:DropDownList CssClass="form-control cboEstuidosTerciarios" runat="server" ID="cboEstuidosTerciarios" ClientIDMode="Static">
                                                    <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                    <asp:ListItem Text="NO CORRESPONDE" Value="NO CORRESPONDE"></asp:ListItem>
                                                    <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                    <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    <asp:ListItem Text="INCOMPLETO" Value="INCOMPLETO"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlTerciario" runat="server">
                                    <div class="row" id="divAdjuntoTerciarios">
                                        <div class="col-12 col-sm-8 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaAlumnoTerciarios"></asp:FileUpload>
                                                    <p class="text-primary" id="txtTerciarios"></p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="rowTerciario">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeTerciarios" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeTerciarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionTerciarios" CssClass="form-control txtInstitucionTerciarios"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaTerciarios" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaTerciarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloTerciarios" CssClass="form-control txtTituloTerciarios"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="accordion" id="accordionUni">
                    <div class="card">
                        <div class="card-header" data-toggle="collapse" href="#collapseUni">
                            UNIVERSITARIOS
                        </div>

                        <div id="collapseUni" class="collapse" aria-labelledby="headingOne" data-parent="#accordionUni">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12 col-sm-4 my-1">
                                        <div class="form-inline">
                                            <div class="input-group w-100">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Estado</span>
                                                </div>
                                                <asp:DropDownList CssClass="form-control cboEstuidosUniversitarios" runat="server" ID="cboEstuidosUniversitarios" ClientIDMode="Static">
                                                    <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                    <asp:ListItem Text="NO CORRESPONDE" Value="NO CORRESPONDE"></asp:ListItem>
                                                    <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                    <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    <asp:ListItem Text="INCOMPLETO" Value="INCOMPLETO"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlEstudiosUniversitarios" runat="server">
                                    <div class="row" id="divAdjuntoUniversitarios">
                                        <div class="col-12 col-sm-8 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaAlumnoUniversitarios"></asp:FileUpload>
                                                    <p class="text-primary" id="txtUniversitarios"></p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="rowUniversitario">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeUniversitarios" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeUniversitarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionUniversitarios" CssClass="form-control txtInstitucionUniversitarios"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaUniversitarios" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaUniversitarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloUniversitarios" CssClass="form-control txtTituloUniversitarios"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="accordion" id="accordionCursos1">
                    <div class="card">
                        <div class="card-header" data-toggle="collapse" href="#collapseCursos1">
                            CURSOS
                        </div>
                        <div id="collapseCursos1" class="collapse" aria-labelledby="headingOne" data-parent="#accordionCursos1">
                            <div class="card-body">
                                <div id="divCurso1" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso1" runat="server" ID="cboCurso1" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso1"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso1" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso1" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso1" CssClass="form-control txtInstitucionCurso1"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso1" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso1" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso1" CssClass="form-control txtTituloCurso1"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso2" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso2" runat="server" ID="cboCurso2" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso2">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso2"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso2" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso2" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso2" CssClass="form-control txtInstitucionCurso2"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso2" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso2" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso2" CssClass="form-control txtTituloCurso2"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso3" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso3" runat="server" ID="cboCurso3" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso3">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso3"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso3" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso3" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso3" CssClass="form-control txtInstitucionCurso3"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso3" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso3" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso3" CssClass="form-control txtTituloCurso3"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso4" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso4" runat="server" ID="cboCurso4" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso4">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso4"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso4" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso4" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso4" CssClass="form-control txtInstitucionCurso4"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso4" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso4" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso4" CssClass="form-control txtTituloCurso4"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso5" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso5" runat="server" ID="cboCurso5" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso5">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso5"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso5" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso5" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso5" CssClass="form-control txtInstitucionCurso5"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso5" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso5" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso5" CssClass="form-control txtTituloCurso5"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="accordion" id="accordionCursos2">
                    <div class="card">
                        <div class="card-header" data-toggle="collapse" href="#collapseCursos2">
                            CURSOS
                        </div>

                        <div id="collapseCursos2" class="collapse" aria-labelledby="headingOne" data-parent="#accordionCursos2">
                            <div class="card-body">
                                <div id="divCurso6" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso6" runat="server" ID="cboCurso6" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso6">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso6"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso6" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso6" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso6" CssClass="form-control txtInstitucionCurso6"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso6" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso6" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso6" CssClass="form-control txtTituloCurso6"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso7" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso7" runat="server" ID="cboCurso7" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso7">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso7"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso7" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso7" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso7" CssClass="form-control txtInstitucionCurso7"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso7" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso7" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso7" CssClass="form-control txtTituloCurso7"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso8" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso8" runat="server" ID="cboCurso8" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso8">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso8"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso8" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso8" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso8" CssClass="form-control txtInstitucionCurso8"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso8" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso8" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso8" CssClass="form-control txtTituloCurso8"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso9" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso9" runat="server" ID="cboCurso9" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso9">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso9"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso9" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso9" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso9" CssClass="form-control txtInstitucionCurso9"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso9" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso9" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso9" CssClass="form-control txtTituloCurso9"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso10" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso10" runat="server" ID="cboCurso10" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso10">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso10"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso10" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso10" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso10" CssClass="form-control txtInstitucionCurso10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso10" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso10" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso10" CssClass="form-control txtTituloCurso10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="accordion" id="accordionCursos3">
                    <div class="card">
                        <div class="card-header" data-toggle="collapse" href="#collapseCursos3">
                            CURSOS
                        </div>

                        <div id="collapseCursos3" class="collapse" aria-labelledby="headingOne" data-parent="#accordionCursos3">
                            <div class="card-body">
                                <div id="divCurso11" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso11" runat="server" ID="cboCurso11" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso11">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso11"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso11" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso11" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso11" CssClass="form-control txtInstitucionCurso11"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso11" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso11" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso11" CssClass="form-control txtTituloCurso11"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso12" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso12" runat="server" ID="cboCurso12" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso12">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso12"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso12" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso12" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso12" CssClass="form-control txtInstitucionCurso12"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso12" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso12" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso12" CssClass="form-control txtTituloCurso12"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso13" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso13" runat="server" ID="cboCurso13" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso13">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso13"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso13" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso13" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso13" CssClass="form-control txtInstitucionCurso13"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso13" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso13" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso13" CssClass="form-control txtTituloCurso13"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso14" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso14" runat="server" ID="cboCurso14" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso14">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso14"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso14" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso14" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso14" CssClass="form-control txtInstitucionCurso14"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso14" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso14" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso14" CssClass="form-control txtTituloCurso14"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso15" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso15" runat="server" ID="cboCurso15" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso15">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso15"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso15" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso15" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso15" CssClass="form-control txtInstitucionCurso15"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso15" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso15" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso15" CssClass="form-control txtTituloCurso15"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="accordion" id="accordionCursos4">
                    <div class="card">
                        <div class="card-header" data-toggle="collapse" href="#collapseCursos4">
                            CURSOS
                        </div>
                        <div id="collapseCursos4" class="collapse" aria-labelledby="headingOne" data-parent="#accordionCursos4">
                            <div class="card-body">
                                <div id="divCurso16" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso16" runat="server" ID="cboCurso16" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso16">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso16"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso16" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso16" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso16" CssClass="form-control txtInstitucionCurso16"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso16" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso16" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso16" CssClass="form-control txtTituloCurso16"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso17" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso17" runat="server" ID="cboCurso17" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso17">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso17"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso17" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso17" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso17" CssClass="form-control txtInstitucionCurso17"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso17" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso17" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso17" CssClass="form-control txtTituloCurso17"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso18" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso18" runat="server" ID="cboCurso18" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso18">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso18"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso18" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso18" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso18" CssClass="form-control txtInstitucionCurso18"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso18" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso18" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso18" CssClass="form-control txtTituloCurso18"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso19" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso19" runat="server" ID="cboCurso19" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso19">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso19"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso19" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso19" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso19" CssClass="form-control txtInstitucionCurso19"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso19" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso19" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso19" CssClass="form-control txtTituloCurso19"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div id="divCurso20" runat="server" clientidmode="Static">
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Estado</span>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control cboCurso20" runat="server" ID="cboCurso20" ClientIDMode="Static">
                                                        <asp:ListItem Text="Seleccione" Value="Seleccione"></asp:ListItem>
                                                        <asp:ListItem Text="CURSANDO" Value="CURSANDO"></asp:ListItem>
                                                        <asp:ListItem Text="COMPLETO" Value="COMPLETO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8 my-1" id="divAdjuntoCurso20">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Constancia</span>
                                                    </div>
                                                    <asp:FileUpload runat="server" ID="fuConstanciaCurso20"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Desde</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtDesdeCurso20" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeCurso20" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Institución</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtInstitucionCurso20" CssClass="form-control txtInstitucionCurso20"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-4 my-1">
                                            <div class="form-inline">
                                                <div class="input-group">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Hasta</span>
                                                    </div>
                                                    <SharePoint:DateTimeControl ID="dtHastaCurso20" DatePickerFrameUrl="<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaCurso20" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat="server"></SharePoint:DateTimeControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-8  my-1">
                                            <div class="form-inline">
                                                <div class="input-group w-100">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">Título</span>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTituloCurso20" CssClass="form-control txtTituloCurso20"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
       <%-- </div>--%>
            <div id="adjuntos" class="tab-pane fade">
                <table class="table">
                    <tbody>
                        <asp:CheckBoxList runat="server" ID="chkBoxList"></asp:CheckBoxList>
                        <button type="button" id="btnEliminar" onserverclick="btnEliminar_ServerClick" class="btn btn-danger btn-sm" runat="server" visible="false">
                            <i class="far fa-trash-alt">
                            </i>
                            Eliminar
                        </button>
                        <asp:Label ID="lbMensaje" ForeColor="Red" runat="server" Text=""></asp:Label>
                        <div class="alert alert-warning" id="alertaArchivoAdjuntos" runat="server" clientidmode="Static" visible="false"></div>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="btn-group btn-block" id="btnGroupPerfil" runat="server" clientidmode="Static">
            <button onclick="if (!validaCamposPerfil()) return;" type="button" class="btn btn-primary" runat="server"><i class="fas fa-arrow-right" aria-hidden="true"></i>Siguiente</button>
            <button type="button" onclick="mensajeCerrar()" id="Button5" runat="server" class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>Cerrar</button>
        </div>
        <div class="btn-group btn-block" id="btnGroupDatosPersonales" style="display: none">
            <button type="button" id="btnVolver" onclick="volverAPerfil()" class="btn btn-volver"><i class="fas fa-arrow-left"></i>Volver</button>
            <button onclick="if (!validaCamposDatosPersonales()) return;" type="button" class="btn btn-primary" runat="server"><i class="fas fa-arrow-right" aria-hidden="true"></i>Siguiente</button>
            <button type="button" onclick="mensajeCerrar()" id="Button4" runat="server" class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>Cerrar</button>
        </div>
        <div class="btn-group btn-block" id="btnGroupEstructura" style="display: none">
            <button type="button" id="btnVolver1" onclick="volverADatosPersonales()" class="btn btn-volver"><i class="fas fa-arrow-left"></i>Volver</button>
            <button onclick="if (!validaCamposEstructura()) return;" type="button" class="btn btn-primary" runat="server"><i class="fas fa-arrow-right"></i>Siguiente</button>
            <button type="button" onclick="mensajeCerrar()" id="Button3" runat="server" class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>Cerrar</button>
        </div>
        <div class="btn-group btn-block" id="btnGroupGrupoFliar" style="display: none">
            <button type="button" id="btnVolver2" onclick="volverAEstructura()" class="btn btn-volver"><i class="fas fa-arrow-left"></i>Volver</button>
            <button onclick="if (!validaCamposGrupoFliar()) return;" type="button" class="btn btn-primary" runat="server"><i class="fas fa-arrow-right"></i>Siguiente</button>
            <button type="button" onclick="mensajeCerrar()" id="Button2" runat="server" class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>Cerrar</button>
        </div>
        <div class="btn-group btn-block" id="btnGroupEstudios" style="display: none">
            <button type="button" id="btnVolver3" onclick="volverAGrupoFliar()" class="btn btn-volver"><i class="fas fa-arrow-left"></i>Volver</button>
            <button onclick="if (!validaCamposEstudios()) return;" onserverclick="btnGuardar" type="button" class="btn btn-primary" runat="server"><i class="fas fa-download" aria-hidden="true"></i>Guardar</button>
            <button type="button" onclick="mensajeCerrar()" id="Button1" runat="server" class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>Cerrar</button>
        </div>
        <div class="btn-group btn-block" id="btnGroupID" runat="server" clientidmode="Static" visible="false">
            <button onserverclick="btnGuardar" id="btnGuardarID" type="button" class="btn btn-primary" runat="server"><i class="fas fa-download" aria-hidden="true"></i>Guardar</button>
            <button type="button" onclick="mensajeCerrar()" id="btnCerrar4" runat="server" class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>Cerrar</button>
        </div>
        <div class="alert alert-warning" id="alertaCamposObligatorios" style="display: none; margin-top: 5px;"></div>
        <div class="modal" id="myModal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background: url('../DatosPersonal/img/Logo.png'); background-position-y: center; background-size: 30%; background-color: rgb(58,133,227); background-position-x: 20px; background-repeat: no-repeat;">
                        <h4 class="modal-title" style="color: white; margin-top: 0px !important; margin-bottom: 0px !important; margin-left: 190px; font-size: 19px; font-weight: bold;">Datos Personal</h4>
                    </div>
                    <div class="modal-body" style="padding: 13px !important;">
                        <%-- La información se guardó correctamente.--%>
                    </div>
                    <div class="modal-footer modalBtnRRHH" style="padding: 0.5rem !important">
                        <button type="button" class="btn btn-primary btn-block" id="modalBtn" onclick="cerrarRRHH()">Aceptar</button>
                    </div>
                    <div class="modal-footer modalBtnUsuario" style="padding: 0.5rem !important">
                        <button type="button" class="btn btn-primary btn-block " id="modalBtnC" onclick="cerrarHome()">Aceptar</button>
                    </div>
                    <div class="modal-footer modalBtnCerrar" style="padding: 0.5rem !important">
                        <button type="button" onclick="cerrarModal()" style="width: 45%;" id="btnCerrarModal" runat="server" class="btn btn-secondary">Cancelar</button>
                        <button type="button" class="btn btn-primary" style="width: 45%;" id="modalBtnCerrar" onserverclick="btnCerrar" runat="server">Aceptar</button>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Datos del Personal
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Datos del Personal
</asp:Content>
