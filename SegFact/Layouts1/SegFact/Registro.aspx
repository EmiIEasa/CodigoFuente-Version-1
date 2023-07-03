<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="SegFact.Layouts1.SegFact.Registro" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link href="../../../_layouts/15/SegFact/css/4-3-1-bootstrap.min.css?version=1.0" rel="stylesheet" />
    <link href="../../../_layouts/15/SegFact/css/estilosSegFact.css?version=1.0" rel="stylesheet" />
    <link href="../../../_layouts/15/SegFact/css/tempusdominus-bootstrap-4.min.css?version=1.0" rel="stylesheet" />
    
    <script src="../../../_layouts/15/SegFact/js/fontAwesome.js?version=1.0" crossorigin="anonymous" type="text/javascript"></script>
    <script src="../../../_layouts/15/SegFact/js/1-14-7-popper.min.js?version=1.0" type="text/javascript"></script>
    <script src="../../../_layouts/15/SegFact/js/3-4-1-jquery.min.js?version=1.0" type="text/javascript"></script>
    <script src="../../../_layouts/15/SegFact/js/4-3-1-bootstrap.min.js?version=1.0" type="text/javascript"></script>
    <script src="../../../_layouts/15/SegFact/js/funcionesSegFact.js?version=1.0" type="text/javascript"></script>
    <script src="../../../_layouts/15/SegFact/js/moment-with-locales.js?version=1.0" type="text/javascript"></script>
    <script src="../../../_layouts/15/SegFact/js/tempusdominus-bootstrap-4.js?version=1.0" type="text/javascript"></script>
    <script src="../../../_layouts/15/SegFact/js/jquery.MultiFile.js?version=1.0" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="container contenedor rounded-lg" style="padding-bottom: 15px" id="formPrincipal">
        <div class="row text-center px-0 banner">
            
            <div class="col-3"><img width="150" class="img-fluid" src="../../../_layouts/15/SegFact/img/Logo.png" alt="Logo - IEASA"></div>
            <div class="col-7 text-center align-middle" style="color: white; font-family: Ubuntu; font-size: 42px; font-weight: 700;">Seguimiento de Facturas</div>
        </div>
        <div class="row justify-content-end">
                    <div class="col-3 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Creado</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCreado" CssClass="form-control " ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
        <ul class="nav nav-pills nav-justified mt-3">
            <asp:Literal ID="LtPestañas" runat="server"></asp:Literal>
        </ul>
          <!-- Tab panes -->
          <div class="tab-content  border border-light border-top-0">
            <div id="DatosGenerales" class="tab-pane active mx-1"><br>
                <div class="row" id="divBTNContabilidad" runat="server" visible="false">
                    <div class="col-12 col-sm-3 my-1">
                        <button id="btnRecibido"  disabled="disabled" type="button" class="btn btn-success btn-block" runat="server" onserverclick="btnRecibido_ServerClick"><i class="fa fa-check"></i> Recibido</button>
                    </div>
                    <div class="col-12 col-sm-3 my-1">
                        <button id="btnContabilizar" disabled="disabled" type="button" class="btn btn-info btn-block" runat="server" onserverclick="btnContabilizar_ServerClick"><i class="fas fa-calculator"></i> Contabilizar</button>
                    </div>
                </div>
                
                <div class="row justify-content-end">
                    <div class="col-4 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Estado</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtEstado" CssClass="form-control txtEstado" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-8 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Razón Social</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtRazonSocial" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 col-sm-4 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">CUIT</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCuit" CssClass="form-control" onkeypress="return soloNumeros(event)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nº Factura</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNroFact" CssClass="form-control txtNroFact"></asp:TextBox>
                                <asp:FileUpload runat="server" id="fuFactura" ClientIDMode="Static"></asp:FileUpload><br />
                                
                            </div>
                        </div>
                        <asp:Literal ID="ltFactura" runat="server"></asp:Literal>
                    </div>
                    <div class="col-12 col-sm-5 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Fecha de comprobante</span>
                                </div>
                                <SharePoint:DateTimeControl ID="dtFecha" DatePickerFrameUrl = "<% $SPUrl:~SiteCollection/_layouts/iframe.aspx %>" CssClassTextBox="claseFecha dtFecha" CalendarImageUrl="../SegFact/img/calendarB4.png" LocaleId="11274" DateOnly="true" runat=server></SharePoint:DateTimeControl>
                            </div>
                        </div>
                    </div>
                </div>
                <hr>
                <div class="row">
                    <div class="col-4 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Comprobante a cargar</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboOC" runat="server" ID="cboOC">
                                    <asp:ListItem Text="-" Value="-"></asp:ListItem>
                                    <asp:ListItem Text="Con OC" Value="Con OC"></asp:ListItem>
                                    <asp:ListItem Text="Sin OC" Value="Sin OC"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-5 my-1 pnlCOC" style="display: none">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nº OC</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNumOC" CssClass="form-control txtNumOC" onkeypress="return soloNumeros(event)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <%--<div class="col-12 col-sm-6 my-1 pnlCOC" style="display: none">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Centro de Costo</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtCentroCosto" CssClass="form-control txtCentroCosto" onkeypress="return soloNumeros(event)"></asp:TextBox>
                            </div>
                        </div>
                    </div>--%>
                    <div class="col-12 col-sm-7 my-1 pnlCOC" style="display: none">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Certif. de Serv.</span>
                                </div>
                              <%--  <asp:TextBox runat="server" ID="txtCertServ" CssClass="form-control txtCertServ" onkeypress="return soloNumeros(event)"></asp:TextBox>--%>
                                <asp:FileUpload runat="server" id="fuCertServ"  ClientIDMode="Static"  AllowMultiple="true"></asp:FileUpload><br />
                                <%--<asp:Literal ID="ltCertServ" runat="server"></asp:Literal>--%>
                                <%--<asp:CheckBoxList runat="server" ID="chkBoxList"></asp:CheckBoxList>
                                <button type="button" id="btnEliminar" onserverclick="btnEliminar_ServerClick" class="btn btn-outline-danger btn-sm" runat="server" visible="false"><i class="far fa-trash-alt"></i>  Eliminar</button>
                                <asp:Label ID="lbMensaje" ForeColor="Red" runat="server" Text=""></asp:Label>--%>
                            </div>
                        </div>
                        <asp:Literal ID="ltCertServ" runat="server"></asp:Literal>
                    </div>
                    
                    <div class="col-12 col-sm-5 my-1 pnlSCOC" style="display: none">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Monto</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtMonto" CssClass="form-control txtMonto" onkeypress="return validaDecimal(event,this)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-7 my-1 pnlSCOC" style="display: none">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Sociedad</span>
                                </div>
                                <asp:DropDownList CssClass="form-control cboSociedad" runat="server" ID="cboSociedad">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-5 my-1 pnlSCOC" style="display: none">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Email</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control txtEmail"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-7 my-1 pnlSOC" style="display: none">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Anexos al comprobante</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuCertFI"  ClientIDMode="Static"></asp:FileUpload><br />
                                
                            </div>
                        </div>
                        <asp:Literal ID="ltCertFI" runat="server"></asp:Literal>
                    </div>
                   
                    <div class="col-12 my-1 pnlSCOC" style="display: none">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Observaciones</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtObservaciones" CssClass="form-control txtRazonSocial" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="Contabilidad" class="tab-pane fade mx-1"><br>
                <div class="row">
                    <div class="col-12 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Observaciones</span>
                                </div>
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtObsContabilidad" CssClass="form-control txtObsContabilidad" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-3 my-1">
                        <button id="btnIrTesoreria"  disabled="disabled" type="button" class="btn btn-primary btn-block" runat="server" onserverclick="btnIrTesoreria_ServerClick"><i class="fas fa-arrow-right"></i> Pasar a Tesoreria</button>
                    </div>
                    <%--<div class="col-12 col-sm-3 my-1">
                        <button id="btnRecibido"  disabled="disabled" type="button" class="btn btn-success btn-block" runat="server" onserverclick="btnRecibido_ServerClick"><i class="fa fa-check"></i> Recibido</button>
                    </div>
                    <div class="col-12 col-sm-3 my-1">
                        <button id="btnContabilizar" disabled="disabled" type="button" class="btn btn-info btn-block" runat="server" onserverclick="btnContabilizar_ServerClick"><i class="fas fa-calculator"></i> Contabilizar</button>
                    </div>--%>
                    <div class="col-12 col-sm-3 my-1">
                        <button id="btnRechazar" disabled="disabled" type="button" class="btn btn-danger btn-block" runat="server"  onclick="if (!validaCampos()) return;" onserverclick="btnRechazar_ServerClick"><i class="fas fa-times-circle"></i> Rechazar</button>
                    </div>
                </div>
            </div>
            <div id="Tesorería" class="tab-pane fade mx-1"><br>
                <div class="row">
                    <div class="col-12 col-sm-6 my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Nº Orden de Pago</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtOrdenPago" ReadOnly="true" CssClass="form-control txtOrdenPago" onkeypress="return soloNumeros(event)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-6  my-1">
                        <div class="form-inline">
                            <div class="input-group w-100">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">N° Contabilizado/ N° Registro SAP</span>
                                </div>
                                <asp:TextBox runat="server" ID="txtNumContabilidad" ReadOnly="true" CssClass="form-control txtNumContabilidad" onkeypress="return soloNumeros(event)"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-7  my-1">
                        <div class="form-inline">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Adjunto</span>
                                </div>
                                <asp:FileUpload runat="server" id="fuAdjunto" Enabled="False"></asp:FileUpload><br />
                                <asp:Literal ID="ltAdjuntos" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                   <div class="col-12 col-sm-3 offset-3 my-1">
                        <button id="btnPasarContabilidad" type="button" disabled="disabled" class="btn btn-primary btn-block" runat="server" onserverclick="btnPasarContabilidad_ServerClick"><i class="fas fa-arrow-right" aria-hidden="true"></i>  Pasar a Contabilidad</button>
                    </div>
                    <%--<div class="col-12 col-sm-3 my-1">
                       <button id="btnFinalizar" type="button" disabled="disabled" class="btn btn-primary btn-block" runat="server" onclick="if (!validaCampos()) return;" onserverclick="btnFinalizar_ServerClick"><i class="fas fa-arrow-right" aria-hidden="true"></i>  Finalizar</button>
                    </div>--%>
                </div>
            </div>
            <div id="Historial" class="tab-pane fade mx-1"><br>
              <div class="table-responsive pt-2">
                    <table id="example1" class="table table-striped table-bordered table-hover table-sm bg-light">
					    <thead>
						    <tr>
							    <th>Fecha y Hora</th>
                                <th>Evento</th>
                                <th>Estado</th>
                                <th>Creado Por</th>
						    </tr>
					    </thead>
					    <tbody>
                            <asp:Literal ID="ltTablaHistorial" runat="server"></asp:Literal>
					    </tbody>
				    </table>
                </div>
            </div>
          </div>
          <hr>
        
        <div class="btn-group btn-block">
            <button id="btnGuardar" onclick="if (!validaCampos()) return;" onserverclick="btnGuardar_ServerClick" type="button" class="btn btn-primary" runat="server"><i class="fas fa-download" aria-hidden="true"></i>  Guardar</button>
            <button type="button" id="btnCerrar" onclick="cerrarDatosGenerales()" class="btn btn-secondary"><i class="far fa-window-close" aria-hidden="true"></i>  Cerrar</button>
        </div>
        <div class="alert alert-warning" id="alertaBigData" style="display:none;margin-top: 5px;"></div>
        <div class="modal modalG" id="myModal">
             <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background:url('../../../_layouts/15/SegFact/img/Logo.png'); background-position-y: center; background-size:25%; background-color:#0a4e9a; background-position-x: 20px; background-repeat: no-repeat;">
                        <h4 class="modal-title" style="color: white; margin-top: 0px !important; margin-bottom: 0px !important; margin-left: 190px; font-size: 19px; font-weight: bold;">Seguimiento de Facturas</h4>
                    </div>
                     <div class="modal-body" id="guardarG" style="padding: 13px !important;">
                        La información se guardó correctamente.
                    </div>
                   
                    <div class="modal-footer modalBtnG" style="padding: 0.5rem !important">
                        <button type="button" class="btn btn-primary btn-block" id="modalBtnG" onclick="cerrarDatosGenerales()">Aceptar</button>
                    </div>
                    <div class="modal-footer modalBtnC" style="padding: 0.5rem !important">
                        <button type="button" class="btn btn-primary btn-block " id="modalBtnC" onclick="cerrarContabilidad()">Aceptar</button>
                    </div>
                    <div class="modal-footer modalBtnT" style="padding: 0.5rem !important">
                        <button type="button" class="btn btn-primary btn-block " id="modalBtnT" onclick="cerrarTesoreria()">Aceptar</button>
                    </div>
                 </div>
            </div>
        </div>
    <%--    <div class="modal modalT" id="myModalT">
             <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background:url('../../../_layouts/15/SegFact/img/Logo.png'); background-position-y: center; background-size:25%; background-color:#0a4e9a; background-position-x: 20px; background-repeat: no-repeat;">
                        <h4 class="modal-title" style="color: white; margin-top: 0px !important; margin-bottom: 0px !important; margin-left: 190px; font-size: 19px; font-weight: bold;">Seguimiento de Facturas</h4>
                    </div>
                     <div class="modal-body" id="guardarT" style="padding: 13px !important;">
                        La información se guardó correctamente.
                    </div>
                    <div class="modal-footer" style="padding: 0.5rem !important">
                        <button type="button" class="btn btn-primary btn-block modalBtnT" id="modalBtnT" onclick="cerrarTesoreria()">Aceptar</button>
                    </div>
                 </div>
            </div>
        </div>--%>
     <%--   <div class="modal modalC" id="myModalC">
             <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background:url('../../../_layouts/15/SegFact/img/Logo.png'); background-position-y: center; background-size:25%; background-color:#0a4e9a; background-position-x: 20px; background-repeat: no-repeat;">
                        <h4 class="modal-title" style="color: white; margin-top: 0px !important; margin-bottom: 0px !important; margin-left: 190px; font-size: 19px; font-weight: bold;">Seguimiento de Facturas</h4>
                    </div>
                     <div class="modal-body" id="guardarC" style="padding: 13px !important;">
                        La información se guardó correctamente.
                    </div>
                    <div class="modal-footer" style="padding: 0.5rem !important">
                        <button type="button" class="btn btn-primary btn-block modalBtnC" id="modalBtnC" onclick="cerrarContabilidad()">Aceptar</button>
                    </div>
                 </div>
            </div>
        </div>--%>
 </div> 
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">

</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >

</asp:Content>
