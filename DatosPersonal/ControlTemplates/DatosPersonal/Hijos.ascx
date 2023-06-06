<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Hijos.ascx.cs" Inherits="DatosPersonal.ControlTemplates.DatosPersonal.Hijos" %>




                    <div class="row my-1 divHijos">
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
                                    <asp:TextBox runat="server" ID="txtNacionalidadHijos1" CssClass="form-control txtNacionalidadHijos1"></asp:TextBox><span style="color: red; display:none;" class="spanCUITMAL" id="spanCUITMAL"><i class="fas fa-times-circle"></i></span><span style="color: green; display:none;" class="spanBIEN" id="spanBIEN"><i class="fas fa-check-circle"></i></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-sm-4  my-1">
                            <div class="form-inline">
                                <div class="input-group w-100">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">CUIL</span>
                                    </div>
                                    <asp:TextBox runat="server" ID="txtCuilHijos1" CssClass="form-control txtCuilHijos1" onfocusout="validateCUIT()" placeholder="xxxxxxxxxxx (Sin guiones)" onkeypress="return soloCUIT(event)" MaxLength="11"></asp:TextBox><span style="color: red; display:none;" class="spanCUITMAL" id="spanCUITMAL"><i class="fas fa-times-circle"></i></span><span style="color: green; display:none;" class="spanBIEN" id="spanBIEN"><i class="fas fa-check-circle"></i></span>
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