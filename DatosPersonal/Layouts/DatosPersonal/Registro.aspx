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
    <script src="../../../../_layouts/15/DatosPersonal/js/funcionesDatosPersonal.js" type="text/javascript"></script>
    <script src="../../../../_layouts/15/DatosPersonal/js/moment-with-locales.js" type="text/javascript"></script>
    <script src="../../../../_layouts/15/DatosPersonal/js/tempusdominus-bootstrap-4.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="container contenedor rounded-lg" style="padding-bottom: 15px" id="formPrincipal">
        <div class="row text-center px-0 banner">
            <div class="col-3"><img width="150" class="img-fluid" src="../../../../_layouts/15/DatosPersonal/img/Logo.png" alt="Logo - IEASA"></div>
            <div class="col-7 text-center align-middle" style="color: white; font-family: Ubuntu; font-size: 42px; font-weight: 700;">Información Personal</div>
        </div>
        <ul class="nav nav-pills nav-justified mt-1" runat="server" id="menuConID" visible="false">
            <asp:Literal ID="LtPestañaConID" runat="server"></asp:Literal>
          </ul>
        <ul class="nav nav-pills nav-justified mt-3"  runat="server" id="menuSinID">
            <li class="nav-item bg-light">
              <a class="nav-link active" id="pestaniaPerfil" runat="server" ClientIDMode="Static">Perfil</a>
            </li>
            <li class="nav-item bg-light">
              <a class="nav-link" id="pestaniaDatosPersonales" runat="server" ClientIDMode="Static">Datos Personales</a>
            </li>
            <li class="nav-item bg-light">
              <a class="nav-link" id="pestaniaEstructura">Estructura</a>
            </li>
            <li class="nav-item bg-light" >
              <a class="nav-link" id="pestaniaGrupoFamiliar"  runat="server" ClientIDMode="Static">Grupo Familiar</a>
            </li>
            <li class="nav-item bg-light">
              <a class="nav-link" id="pestaniaEstudiosCursados" >Estudios Cursados</a>
            </li>
            <%--<li class="nav-item bg-light">
              <a class="nav-link" href='#adjuntos' data-toggle='pill'>Adjuntos</a>
            </li>--%>
        </ul>
        <div class="tab-content  border border-light border-top-0">
            <div id="perfil" class="tab-pane active" runat="server" ClientIDMode="Static">
                Perfil
            </div>
            <div id="datosPersonales" class="tab-pane active" runat="server" ClientIDMode="Static">
                
                <div class="row" >
                    <div class="col-1 mt-1">
                        <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="#D7D7D7">30/12/2021</asp:Label>
                    </div>
                    <div class="col-11 mt-1">
                        <div class="form-inline float-right">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Sociedad</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboSociedad" runat="server" ID="cboSociedad" clientidmode="Static">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-7 my-1">
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
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Fecha de Nacimiento</span>
                                </div>
                                <SharePoint:DateTimeControl ID="dtFecha" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFecha" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
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
                                <asp:DropDownList CssClass="form-control cboGenero" runat="server" ID="cboGenero" clientidmode="Static">
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
                                <asp:DropDownList CssClass="form-control cboEstadoCivil" runat="server" ID="cboEstadoCivil" clientidmode="Static">
                                </asp:DropDownList>
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
                                <asp:FileUpload runat="server" id="fuDNI" AllowMultiple="true"></asp:FileUpload>
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
                                <asp:TextBox runat="server" ID="txtCuil" CssClass="form-control txtCuil" ClientIDMode="Static" onfocusout="validateCUIT(0)" placeholder="xxxxxxxxxxx (Sin guiones)" onkeypress="return soloCUIT(event)" MaxLength="11"></asp:TextBox><span style="color: red; display:none;" class="spanCUITMAL0" id="spanCUITMAL"><i class="fas fa-times-circle"></i></span><span style="color: green; display:none;" class="spanBIEN0" id="spanBIEN"><i class="fas fa-check-circle"></i></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Constancia de CUIL</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuCuil"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
                <hr>
                <div class="row">
                    <div class="col-md-12 text-center align-middle text-white"><h3>Domicilio</h3></div>
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
                                <span class="input-group-text text-left">¿En que otra área de la Empresa deseás desarrollarte?<br>¿Que te motiva al cambio?<br>¿Con qué formación contás para desempeñarte en la misma?</span>
                            </div>
                            <asp:TextBox runat="server" ID="txtPuestoNuevo" CssClass="form-control txtPuestoNuevo" TextMode="MultiLine" ></asp:TextBox>
                        </div>
                    </div>
                </div>
                </div>
            </div>
            <div id="grupoFamiliar" class="tab-pane fade" runat="server" ClientIDMode="Static"> 
                    <div class="row divConyuge" runat="server">
                        <div class="col-md-12 text-center align-middle text-white"><h3 id="txtConyugeConcubinato"  runat="server" ClientIDMode="Static"></h3></div>
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
                                    <SharePoint:DateTimeControl ID="dtFechaNacimientoConyuge" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFechaNacimientoConyuge" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
                                </div>
                            </div>
                        </div>
                        <div class="col-12  my-1">
                            <div class="form-inline">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">DNI (Adjuntar frente y dorso)</span>
                                    </div>
                                    <asp:FileUpload runat="server" id="fuDNIconyuge" AllowMultiple="true"></asp:FileUpload>
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
                                    <span style="color: red; display:none;" class="spanCUITMAL1" id="spanCUITMALConyuge">
                                    <i class="fas fa-times-circle"></i>
                                </span>
                                <span style="color: green; display:none;" class="spanBIEN1" id="spanBIENConyuge">
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
                                    <asp:FileUpload runat="server" id="fuCUILconyuge"></asp:FileUpload>
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
                <div class="row my-1 divHijos divHijosTitulo" id="divHijos" runat="server" ClientIDMode="Static">
                    <div class="col-md-12 text-center align-middle text-white"><h3>Hijos/as</h3></div>
                </div>
                <div class="row my-1 divHijos divHijos1" id="divHijos1" runat="server" ClientIDMode="Static">
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
                                <SharePoint:DateTimeControl ID="dtFechaNacimientoHijos1" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFechaNacimientoHijos1" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">DNI (Adjuntar frente y dorso)</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuDNIHijos1" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Partida de Nacimiento</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuPartidaNacimientoHijos1"></asp:FileUpload>
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
                                <span style="color: red; display:none;" class="spanCUITMAL2" id="spanCUITMALHijos1">
                                    <i class="fas fa-times-circle"></i>
                                </span>
                                <span style="color: green; display:none;" class="spanBIEN2" id="spanBIENHijos1">
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
                                <asp:FileUpload runat="server" id="fuCUILHijos1"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
                <hr class="divHijos divHijos2" id="divhrHijos2" runat="server" ClientIDMode="Static" />
                <div class="row my-1 divHijos divHijos2" id="divHijos2" runat="server" ClientIDMode="Static">
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
                                <SharePoint:DateTimeControl ID="dtFechaNacimientoHijos2" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFechaNacimientoHijos2" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">DNI (Adjuntar frente y dorso)</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuDNIHijos2" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Partida de Nacimiento</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuPartidaNacimientoHijos2"></asp:FileUpload>
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
                                <span style="color: red; display:none;" class="spanCUITMAL3" id="spanCUITMALHijos2">
                                    <i class="fas fa-times-circle"></i>
                                </span>
                                <span style="color: green; display:none;" class="spanBIEN3" id="spanBIENHijos2">
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
                                <asp:FileUpload runat="server" id="fuCUILHijos2"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
                <hr class="divHijos divHijos3" id="divhrHijos3" runat="server" ClientIDMode="Static" />
                <div class="row my-1 divHijos divHijos3" id="divHijos3" runat="server" ClientIDMode="Static">
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
                                <SharePoint:DateTimeControl ID="dtFechaNacimientoHijos3" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFechaNacimientoHijos3" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">DNI (Adjuntar frente y dorso)</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuDNIHijos3" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Partida de Nacimiento</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuPartidaNacimientoHijos3"></asp:FileUpload>
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
                                <span style="color: red; display:none;" class="spanCUITMAL4" id="spanCUITMALHijos3">
                                    <i class="fas fa-times-circle"></i>
                                </span>
                                <span style="color: green; display:none;" class="spanBIEN4" id="spanBIENHijos3">
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
                                <asp:FileUpload runat="server" id="fuCUILHijos3"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
                <hr class="divHijos divHijos4" id="divhrHijos4" runat="server" ClientIDMode="Static" />
                <div class="row my-1 divHijos divHijos4" id="divHijos4" runat="server" ClientIDMode="Static">
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
                                <SharePoint:DateTimeControl ID="dtFechaNacimientoHijos4" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFechaNacimientoHijos4" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">DNI (Adjuntar frente y dorso)</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuDNIHijos4" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Partida de Nacimiento</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuPartidaNacimientoHijos4"></asp:FileUpload>
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
                                <span style="color: red; display:none;" class="spanCUITMAL5" id="spanCUITMALHijos4">
                                    <i class="fas fa-times-circle"></i>
                                </span>
                                <span style="color: green; display:none;" class="spanBIEN5" id="spanBIENHijos4">
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
                                <asp:FileUpload runat="server" id="fuCUILHijos4"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
                <hr class="divHijos divHijos5" id="divhrHijos5" runat="server" ClientIDMode="Static" />
                <div class="row my-1 divHijos divHijos5" id="divHijos5" runat="server" ClientIDMode="Static">
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
                                <SharePoint:DateTimeControl ID="dtFechaNacimientoHijos5" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFechaNacimientoHijos5" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">DNI (Adjuntar frente y dorso)</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuDNIHijos5" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Partida de Nacimiento</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuPartidaNacimientoHijos5"></asp:FileUpload>
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
                                <span style="color: red; display:none;" class="spanCUITMAL6" id="spanCUITMALHijos5">
                                    <i class="fas fa-times-circle"></i>
                                </span>
                                <span style="color: green; display:none;" class="spanBIEN6" id="spanBIENHijos5">
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
                                <asp:FileUpload runat="server" id="fuCUILHijos5"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
                <hr class="divHijos divHijos6" id="divhrHijos6" runat="server" ClientIDMode="Static" />
                <div class="row my-1 divHijos divHijos6" id="divHijos6" runat="server" ClientIDMode="Static">
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
                                <SharePoint:DateTimeControl ID="dtFechaNacimientoHijos6" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFechaNacimientoHijos6" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">DNI (Adjuntar frente y dorso)</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuDNIHijos6" AllowMultiple="true"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Partida de Nacimiento</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuPartidaNacimientoHijos6"></asp:FileUpload>
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
                                <span style="color: red; display:none;" class="spanCUITMAL7" id="spanCUITMALHijos6">
                                    <i class="fas fa-times-circle"></i>
                                </span>
                                <span style="color: green; display:none;" class="spanBIEN7" id="spanBIENHijos6">
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
                                <asp:FileUpload runat="server" id="fuCUILHijos6"></asp:FileUpload>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="estudiosCursados" class="tab-pane fade">
                <div class="row">
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Primarios</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboEstuidosPrimarios" runat="server" ID="cboEstuidosPrimarios" clientidmode="Static">
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
                                <asp:FileUpload runat="server" id="fuConstanciaAlumnoPrimarios"></asp:FileUpload>
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
                                <SharePoint:DateTimeControl ID="dtDesdePrimarios" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdePrimarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
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
                                <SharePoint:DateTimeControl ID="dtHastaPrimarios" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaPrimarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
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
                <hr />
                <div class="row">
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Secundarios</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboEstuidosSecundarios" runat="server" ID="cboEstuidosSecundarios" clientidmode="Static">
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
                                <asp:FileUpload runat="server" id="fuConstanciaAlumnoSecundarios"></asp:FileUpload>
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
                                <SharePoint:DateTimeControl ID="dtDesdeSecundarios" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeSecundarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
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
                                <SharePoint:DateTimeControl ID="dtHastaSecundarios" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaSecundarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
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
                <hr />
                <div class="row">
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Terciarios</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboEstuidosTerciarios" runat="server" ID="cboEstuidosTerciarios" clientidmode="Static">
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
                                    <asp:FileUpload runat="server" id="fuConstanciaAlumnoTerciarios"></asp:FileUpload>
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
                                    <SharePoint:DateTimeControl ID="dtDesdeTerciarios" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeTerciarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
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
                                    <SharePoint:DateTimeControl ID="dtHastaTerciarios" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaTerciarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
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
                <hr />
                <div class="row">
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Universitarios</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboEstuidosUniversitarios" runat="server" ID="cboEstuidosUniversitarios" clientidmode="Static">
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
                                    <asp:FileUpload runat="server" id="fuConstanciaAlumnoUniversitarios"></asp:FileUpload>
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
                                    <SharePoint:DateTimeControl ID="dtDesdeUniversitarios" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtDesdeUniversitarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
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
                                    <SharePoint:DateTimeControl ID="dtHastaUniversitarios" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtHastaUniversitarios" CalendarImageUrl="../DatosPersonal/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
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
            <div id="adjuntos" class="tab-pane fade">
                <table class="table">
                    <tbody>
                        <asp:Literal ID="ltAdjuntos" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="btn-group btn-block" id="btnGroupDatosPersonales" runat="server" ClientIDMode="Static">
            <button onclick="if (!validaCamposDatosPersonales()) return;" type="button" class="btn btn-primary" runat="server"><i class="fas fa-arrow-right" aria-hidden="true"></i>  Siguiente</button>
            <button type="button" id="btnCerrar1" runat="server" onserverclick="btnCerrar"  class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>  Cerrar</button>
        </div>
        <div class="btn-group btn-block" id="btnGroupEstructura" style="display:none">
            <button type="button" id="btnVolver1" onclick="volverADatosPersonales()" class="btn btn-volver"><i class="fas fa-arrow-left"></i>  Volver</button>
            <button onclick="if (!validaCamposEstructura()) return;"  type="button" class="btn btn-primary" runat="server"><i class="fas fa-arrow-right"></i>  Siguiente</button>
            <button type="button" id="btnCerrar2" runat="server" onserverclick="btnCerrar"  class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>  Cerrar</button>
        </div>
        <div class="btn-group btn-block" id="btnGroupGrupoFliar" style="display:none">
            <button type="button" id="btnVolver2" onclick="volverAEstructura()" class="btn btn-volver"><i class="fas fa-arrow-left"></i>  Volver</button>
            <button onclick="if (!validaCamposGrupoFliar()) return;"  type="button" class="btn btn-primary" runat="server"><i class="fas fa-arrow-right"></i>  Siguiente</button>
            <button type="button" id="btnCerrar3" runat="server" onserverclick="btnCerrar"  class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>  Cerrar</button>
        </div>
        <div class="btn-group btn-block" id="btnGroupEstudios" style="display:none">
            <button type="button" id="btnVolver3" onclick="volverAGrupoFliar()" class="btn btn-volver"><i class="fas fa-arrow-left"></i>  Volver</button>
            <button onclick="if (!validaCamposEstudios()) return;" onserverclick="btnGuardar" type="button" class="btn btn-primary" runat="server"><i class="fas fa-download" aria-hidden="true"></i>  Guardar</button>
            <button type="button" id="Button1" runat="server" onserverclick="btnCerrar"  class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>  Cerrar</button>
        </div>
        <div class="btn-group btn-block" id="btnGroupID"  runat="server" ClientIDMode="Static" visible="false">
            <button onserverclick="btnGuardar" id="btnGuardarID" type="button" class="btn btn-primary" runat="server"><i class="fas fa-download" aria-hidden="true"></i>  Guardar</button>
            <button type="button" id="btnCerrar4" runat="server" onserverclick="btnCerrar" class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>  Cerrar</button>
        </div>
         <div class="alert alert-warning" id="alertaCamposObligatorios" style="display:none;margin-top: 5px;"></div>
        <div class="modal" id="myModal">
             <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background:url('../DatosPersonal/img/Logo.png'); background-position-y: center; background-size:30%; background-color:rgb(58,133,227); background-position-x: 20px; background-repeat: no-repeat;">
                        <h4 class="modal-title" style="color: white; margin-top: 0px !important; margin-bottom: 0px !important; margin-left: 190px; font-size: 19px; font-weight: bold;">Datos Personal</h4>
                    </div>
                    <div class="modal-body" style="padding: 13px !important;">
                        La información se guardó correctamente.
                    </div>
                    <div class="modal-footer modalBtnRRHH" style="padding: 0.5rem !important">
                        <button type="button" class="btn btn-primary btn-block" id="modalBtn" onclick="cerrarRRHH()">Aceptar</button>
                    </div>
                    <div class="modal-footer modalBtnUsuario" style="padding: 0.5rem !important">
                        <button type="button" class="btn btn-primary btn-block " id="modalBtnC" onclick="cerrarHome()">Aceptar</button>
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
