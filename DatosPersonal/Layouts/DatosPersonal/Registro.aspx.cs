using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.SharePoint.Utilities;
using System.Collections.Generic;
using System.Web.Security;
using System.Text;
using Microsoft.SharePoint.IdentityModel;
using System.Web.UI.WebControls;

namespace DatosPersonal.Layouts.DatosPersonal
{
    public partial class Registro : LayoutsPageBase
    {
        private string sSitio = "https://portalrrhhdevtest.energia-argentina.com.ar";
      //  private string sSitio = "https://portalrrhh.ieasa.com.ar";
        string strSeleccione = "Seleccione";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();
                CargarInicial();
            }
        }

        private void CargarInicial()
        {
            if (Request.QueryString["ID"] != null)
            {
                SPListItem DatosPersonal = SPContext.Current.Web.Lists["DatosPersonal"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                CargarDatos(DatosPersonal);
                CargaPestania();
                menuConID.Visible = true;

                menuSinID.Visible = false;
                btnGroupPerfil.Visible = false;
                btnGroupID.Visible = true;
            }
           
        }
        private void CargaPestania()
        {
            LtPestañaConID.Text = "<li class='nav-item bg-light'>" +
                                      "<a class='nav-link active' data-toggle='pill' id='pestaniaPerfil' href='#perfil' ClientIDMode='Static'>Perfil</a>" +
                                    "</li>" +
                                    "<li class='nav-item bg-light'>" +
                                      "<a class='nav-link' data-toggle='pill' id='pestaniaDatosPersonales' href='#datosPersonales' ClientIDMode='Static'>Datos Personales</a>" +
                                    "</li>" +
                                    "<li class='nav-item bg-light'>" +
                                      "<a class='nav-link' data-toggle='pill' id='pestaniaEstructura' href='#estructura'>Estructura</a>" +
                                    "</li>" +
                                    "<li class='nav-item bg-light' >" +
                                      "<a class='nav-link' data-toggle='pill' id='pestaniaGrupoFamiliar' href='#grupoFamiliar' ClientIDMode='Static'>Grupo Familiar</a>" +
                                    "</li>" +
                                    "<li class='nav-item bg-light'>" +
                                      "<a class='nav-link' data-toggle='pill'style='padding: 0.5rem 0 !important;' id='pestaniaEstudiosCursados' href='#estudiosCursados'>Estudios Cursados</a>" +
                                    "</li>" +
                                    "<li class='nav-item bg-light'>" +
                                      "<a class='nav-link' data-toggle='pill' href='#adjuntos'>Adjuntos</a>" +
                                    "</li>";
        }
       

        private string reverseString(string sValor)
        {
            char[] miArray = sValor.ToCharArray();
            Array.Reverse(miArray);
            return new string(miArray);
        }
        private void CargarDatos(SPListItem DatosPersonal)
        {
            CargarAdjuntos(DatosPersonal);
            if (DatosPersonal["Sociedad"] != null && !string.IsNullOrEmpty(DatosPersonal["Sociedad"].ToString()))
            {
                cboSociedad.SelectedValue = (DatosPersonal["Sociedad"] != null && !string.IsNullOrEmpty(DatosPersonal["Sociedad"].ToString()) ? DatosPersonal["Sociedad"].ToString() : String.Empty);
            }
            txtPosicion.Text = (DatosPersonal["Posicion"] != null && !string.IsNullOrEmpty(DatosPersonal["Posicion"].ToString()) ? DatosPersonal["Posicion"].ToString() : String.Empty);
            txtGerencia.Text = (DatosPersonal["Gerencia"] != null && !string.IsNullOrEmpty(DatosPersonal["Gerencia"].ToString()) ? DatosPersonal["Gerencia"].ToString() : String.Empty);
            txtPisoPerfil.Text = (DatosPersonal["PisoPerfil"] != null && !string.IsNullOrEmpty(DatosPersonal["PisoPerfil"].ToString()) ? DatosPersonal["PisoPerfil"].ToString() : String.Empty);
            txtNombreApellido.Text = (DatosPersonal["NombreApellido"] != null && !string.IsNullOrEmpty(DatosPersonal["NombreApellido"].ToString()) ? DatosPersonal["NombreApellido"].ToString() : String.Empty);

            txtNombreApellidoEmergencia.Text = (DatosPersonal["DatosEmergenciaNombreApellido"] != null && !string.IsNullOrEmpty(DatosPersonal["DatosEmergenciaNombreApellido"].ToString()) ? DatosPersonal["DatosEmergenciaNombreApellido"].ToString() : String.Empty);
            txtVinculoEmergencia.Text = (DatosPersonal["DatosEmergenciaVinculo"] != null && !string.IsNullOrEmpty(DatosPersonal["DatosEmergenciaVinculo"].ToString()) ? DatosPersonal["DatosEmergenciaVinculo"].ToString() : String.Empty);
            txtTelefonoEmergencia.Text = (DatosPersonal["DatosEmergenciaTelefono"] != null && !string.IsNullOrEmpty(DatosPersonal["DatosEmergenciaTelefono"].ToString()) ? DatosPersonal["DatosEmergenciaTelefono"].ToString() : String.Empty);

          

            if (DatosPersonal["Alergico"] != null && !string.IsNullOrEmpty(DatosPersonal["Alergico"].ToString()))
            {
                cboAlergias.SelectedValue = (DatosPersonal["Alergico"] != null && !string.IsNullOrEmpty(DatosPersonal["Alergico"].ToString()) ? DatosPersonal["Alergico"].ToString() : String.Empty);
                if (DatosPersonal["Alergico"].ToString() == "SI")
                {
                    divEspecificar.Style.Add("display", "block");
                }
                else
                {
                    divEspecificar.Style.Add("display", "none");
                }
            }
            txtEspecificar.Text = (DatosPersonal["EspecificarAlergia"] != null && !string.IsNullOrEmpty(DatosPersonal["EspecificarAlergia"].ToString()) ? DatosPersonal["EspecificarAlergia"].ToString() : String.Empty);
            if (DatosPersonal["LicenciaConducir"] != null && !string.IsNullOrEmpty(DatosPersonal["LicenciaConducir"].ToString()))
            {
                cboLicenciaConducir.SelectedValue = (DatosPersonal["LicenciaConducir"] != null && !string.IsNullOrEmpty(DatosPersonal["LicenciaConducir"].ToString()) ? DatosPersonal["LicenciaConducir"].ToString() : String.Empty);
                if (DatosPersonal["LicenciaConducir"].ToString() == "SI")
                {
                    divCtegoria.Style.Add("display", "block");
                    divVigencia.Style.Add("display", "block");
                    divAdjunteLicencia.Style.Add("display", "block");
                }
                else
                {
                    divCtegoria.Style.Add("display", "none");
                    divVigencia.Style.Add("display", "none");
                    divAdjunteLicencia.Style.Add("display", "none");
                }
            }
            txtCategoria.Text = (DatosPersonal["Categoria"] != null && !string.IsNullOrEmpty(DatosPersonal["Categoria"].ToString()) ? DatosPersonal["Categoria"].ToString() : String.Empty);
            if (DatosPersonal["VigenciaLicencia"] != null && !string.IsNullOrEmpty(DatosPersonal["VigenciaLicencia"].ToString()))
                { 
            dtVigenciaLicencia.SelectedDate = Convert.ToDateTime(DatosPersonal["VigenciaLicencia"].ToString());
            }
            txtLegajo.Text = (DatosPersonal["Legajo"] != null && !string.IsNullOrEmpty(DatosPersonal["Legajo"].ToString()) ? DatosPersonal["Legajo"].ToString() : String.Empty);
            if (DatosPersonal["FechaNacimiento"] != null)
            {
                dtFecha.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaNacimiento"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaNacimiento"].ToString()) ? DatosPersonal["FechaNacimiento"].ToString() : String.Empty);
            }
            txtLugarNacimiento.Text = (DatosPersonal["LugarNacimiento"] != null && !string.IsNullOrEmpty(DatosPersonal["LugarNacimiento"].ToString()) ? DatosPersonal["LugarNacimiento"].ToString() : String.Empty);
            if (DatosPersonal["Genero"] != null && !string.IsNullOrEmpty(DatosPersonal["Genero"].ToString()))
            {
                cboGenero.SelectedValue = (DatosPersonal["Genero"] != null && !string.IsNullOrEmpty(DatosPersonal["Genero"].ToString()) ? DatosPersonal["Genero"].ToString() : String.Empty);
            }
            if (DatosPersonal["EstadoCivil"] != null && !string.IsNullOrEmpty(DatosPersonal["EstadoCivil"].ToString()))
            {
                bool entro = false;
                switch (DatosPersonal["EstadoCivil"].ToString())
                {
                    case "solter":
                        cboEstadoCivil.SelectedValue = "Soltero/a";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "OcultaDivConyuge();", true);
                        entro = true;
                        break;
                    case "casado":
                        cboEstadoCivil.SelectedValue = "Casado/a";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MuestraDivConyuge();", true);
                        entro = true;
                        break;
                    case "Concu.":
                        cboEstadoCivil.SelectedValue = "Concubinato";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MuestraDivConyuge();", true);
                        entro = true;
                        break;
                    case "divorc":
                        cboEstadoCivil.SelectedValue = "Divorciado/a";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "OcultaDivConyuge();", true);
                        entro = true;
                        break;
                    case "viudo":
                        cboEstadoCivil.SelectedValue = "Viudo/a";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "OcultaDivConyuge();", true);
                        entro = true;
                        break;
                }
                if (entro == false)
                {
                    cboEstadoCivil.SelectedValue = (DatosPersonal["EstadoCivil"] != null && !string.IsNullOrEmpty(DatosPersonal["EstadoCivil"].ToString()) ? DatosPersonal["EstadoCivil"].ToString() : String.Empty);
                }


                if (DatosPersonal["EstadoCivil"].ToString() == "Casado/a" || DatosPersonal["EstadoCivil"].ToString() == "casado")
                {
                    txtConyugeConcubinato.InnerText = "Cónyuge";
                }
                else if (DatosPersonal["EstadoCivil"].ToString() == "Concubinato" || DatosPersonal["EstadoCivil"].ToString() == "Concu.")
                {
                    txtConyugeConcubinato.InnerText = "Conviviente";
                }
            }
            if (DatosPersonal["CantidadHijos"] != null && !string.IsNullOrEmpty(DatosPersonal["CantidadHijos"].ToString()))
            {
                switch (DatosPersonal["CantidadHijos"].ToString())
                {
                    case "0":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MuestraDivHijos(0);", true);
                        break;
                    case "1":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MuestraDivHijos(1);", true);
                        break;
                    case "2":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MuestraDivHijos(2);", true);
                        break;
                    case "3":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MuestraDivHijos(3);", true);
                        break;
                    case "4":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MuestraDivHijos(4);", true);
                        break;
                    case "5":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MuestraDivHijos(5);", true);
                        break;
                    case "6":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "OcultaDivConyuge(6);", true);
                        break;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MuestraDivHijos(0);", true);
            }

            txtCuil.Text = (DatosPersonal["CUIL"] != null && !string.IsNullOrEmpty(DatosPersonal["CUIL"].ToString()) ? DatosPersonal["CUIL"].ToString() : String.Empty);
            if (DatosPersonal["Provincia"] != null && !string.IsNullOrEmpty(DatosPersonal["Provincia"].ToString()))
            {
                cboProvincia.SelectedValue = (DatosPersonal["Provincia"] != null && !string.IsNullOrEmpty(DatosPersonal["Provincia"].ToString()) ? DatosPersonal["Provincia"].ToString() : String.Empty);
            }
            txtCiudad.Text = (DatosPersonal["Ciudad"] != null && !string.IsNullOrEmpty(DatosPersonal["Ciudad"].ToString()) ? DatosPersonal["Ciudad"].ToString() : String.Empty);
            txtCalle.Text = (DatosPersonal["Calle"] != null && !string.IsNullOrEmpty(DatosPersonal["Calle"].ToString()) ? DatosPersonal["Calle"].ToString() : String.Empty);
            txtAltura.Text = (DatosPersonal["Altura"] != null && !string.IsNullOrEmpty(DatosPersonal["Altura"].ToString()) ? DatosPersonal["Altura"].ToString() : String.Empty);
            txtPiso.Text = (DatosPersonal["Piso"] != null && !string.IsNullOrEmpty(DatosPersonal["Piso"].ToString()) ? DatosPersonal["Piso"].ToString() : String.Empty);
            txtDepto.Text = (DatosPersonal["Depto"] != null && !string.IsNullOrEmpty(DatosPersonal["Depto"].ToString()) ? DatosPersonal["Depto"].ToString() : String.Empty);
            txtCodPostal.Text = (DatosPersonal["CodPostal"] != null && !string.IsNullOrEmpty(DatosPersonal["CodPostal"].ToString()) ? DatosPersonal["CodPostal"].ToString() : String.Empty);
            txtTelefono.Text = (DatosPersonal["Telefono"] != null && !string.IsNullOrEmpty(DatosPersonal["Telefono"].ToString()) ? DatosPersonal["Telefono"].ToString() : String.Empty);
            txtCelularPersonal.Text = (DatosPersonal["CelularPersonal"] != null && !string.IsNullOrEmpty(DatosPersonal["CelularPersonal"].ToString()) ? DatosPersonal["CelularPersonal"].ToString() : String.Empty);
            txtCelularEmpresa.Text = (DatosPersonal["CelularEmpresa"] != null && !string.IsNullOrEmpty(DatosPersonal["CelularEmpresa"].ToString()) ? DatosPersonal["CelularEmpresa"].ToString() : String.Empty);
            txtCompaniaCelular.Text = (DatosPersonal["CompaniaCelularEmpresa"] != null && !string.IsNullOrEmpty(DatosPersonal["CompaniaCelularEmpresa"].ToString()) ? DatosPersonal["CompaniaCelularEmpresa"].ToString() : String.Empty);
            //Estructura
            txtPuestoActual.Text = (DatosPersonal["PuestoActual"] != null && !string.IsNullOrEmpty(DatosPersonal["PuestoActual"].ToString()) ? DatosPersonal["PuestoActual"].ToString() : String.Empty);
            txtPuestoNuevo.Text = (DatosPersonal["PuestoNuevo"] != null && !string.IsNullOrEmpty(DatosPersonal["PuestoNuevo"].ToString()) ? DatosPersonal["PuestoNuevo"].ToString() : String.Empty);
            //Grupo Fliar
            txtNombreConyuge.Text = (DatosPersonal["NombreConyuge"] != null && !string.IsNullOrEmpty(DatosPersonal["NombreConyuge"].ToString()) ? DatosPersonal["NombreConyuge"].ToString() : String.Empty);
            if (DatosPersonal["FechaNacimientoConyuge"] != null)
            {
                dtFechaNacimientoConyuge.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaNacimientoConyuge"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaNacimientoConyuge"].ToString()) ? DatosPersonal["FechaNacimientoConyuge"].ToString() : String.Empty);
            }
            txtCuilConyuge.Text = (DatosPersonal["CUILconyuge"] != null && !string.IsNullOrEmpty(DatosPersonal["CUILconyuge"].ToString()) ? DatosPersonal["CUILconyuge"].ToString() : String.Empty);
            if (DatosPersonal["CantidadHijos"] != null && !string.IsNullOrEmpty(DatosPersonal["CantidadHijos"].ToString()))
            {
                cboCantidadHijos.SelectedValue = (DatosPersonal["CantidadHijos"] != null && !string.IsNullOrEmpty(DatosPersonal["CantidadHijos"].ToString()) ? DatosPersonal["CantidadHijos"].ToString() : String.Empty);
            }
            txtNombreApellidoHijos1.Text = (DatosPersonal["NombreApellidoHijo1"] != null && !string.IsNullOrEmpty(DatosPersonal["NombreApellidoHijo1"].ToString()) ? DatosPersonal["NombreApellidoHijo1"].ToString() : String.Empty);
            if (DatosPersonal["FechaNacimientoHijo1"] != null)
            {
                dtFechaNacimientoHijos1.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaNacimientoHijo1"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaNacimientoHijo1"].ToString()) ? DatosPersonal["FechaNacimientoHijo1"].ToString() : String.Empty);
            }
            txtNacionalidadHijos1.Text = (DatosPersonal["NacionalidadHijo1"] != null && !string.IsNullOrEmpty(DatosPersonal["NacionalidadHijo1"].ToString()) ? DatosPersonal["NacionalidadHijo1"].ToString() : String.Empty);
            txtCuilHijos1.Text = (DatosPersonal["CUILHijo1"] != null && !string.IsNullOrEmpty(DatosPersonal["CUILHijo1"].ToString()) ? DatosPersonal["CUILHijo1"].ToString() : String.Empty);

            txtNombreApellidoHijos2.Text = (DatosPersonal["NombreApellidoHijo2"] != null && !string.IsNullOrEmpty(DatosPersonal["NombreApellidoHijo2"].ToString()) ? DatosPersonal["NombreApellidoHijo2"].ToString() : String.Empty);
            if (DatosPersonal["FechaNacimientoHijo2"] != null)
            {
                dtFechaNacimientoHijos2.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaNacimientoHijo2"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaNacimientoHijo2"].ToString()) ? DatosPersonal["FechaNacimientoHijo2"].ToString() : String.Empty);
            }
            txtNacionalidadHijos2.Text = (DatosPersonal["NacionalidadHijo2"] != null && !string.IsNullOrEmpty(DatosPersonal["NacionalidadHijo2"].ToString()) ? DatosPersonal["NacionalidadHijo2"].ToString() : String.Empty);
            txtCuilHijos2.Text = (DatosPersonal["CUILHijo2"] != null && !string.IsNullOrEmpty(DatosPersonal["CUILHijo2"].ToString()) ? DatosPersonal["CUILHijo2"].ToString() : String.Empty);

            txtNombreApellidoHijos3.Text = (DatosPersonal["NombreApellidoHijo3"] != null && !string.IsNullOrEmpty(DatosPersonal["NombreApellidoHijo3"].ToString()) ? DatosPersonal["NombreApellidoHijo3"].ToString() : String.Empty);
            if (DatosPersonal["FechaNacimientoHijo3"] != null)
            {
                dtFechaNacimientoHijos3.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaNacimientoHijo3"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaNacimientoHijo3"].ToString()) ? DatosPersonal["FechaNacimientoHijo3"].ToString() : String.Empty);
            }
            txtNacionalidadHijos3.Text = (DatosPersonal["NacionalidadHijo3"] != null && !string.IsNullOrEmpty(DatosPersonal["NacionalidadHijo3"].ToString()) ? DatosPersonal["NacionalidadHijo3"].ToString() : String.Empty);
            txtCuilHijos3.Text = (DatosPersonal["CUILHijo3"] != null && !string.IsNullOrEmpty(DatosPersonal["CUILHijo3"].ToString()) ? DatosPersonal["CUILHijo3"].ToString() : String.Empty);

            txtNombreApellidoHijos4.Text = (DatosPersonal["NombreApellidoHijo4"] != null && !string.IsNullOrEmpty(DatosPersonal["NombreApellidoHijo4"].ToString()) ? DatosPersonal["NombreApellidoHijo4"].ToString() : String.Empty);
            if (DatosPersonal["FechaNacimientoHijo4"] != null)
            {
                dtFechaNacimientoHijos4.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaNacimientoHijo4"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaNacimientoHijo4"].ToString()) ? DatosPersonal["FechaNacimientoHijo4"].ToString() : String.Empty);
            }
            txtNacionalidadHijos4.Text = (DatosPersonal["NacionalidadHijo4"] != null && !string.IsNullOrEmpty(DatosPersonal["NacionalidadHijo4"].ToString()) ? DatosPersonal["NacionalidadHijo4"].ToString() : String.Empty);
            txtCuilHijos4.Text = (DatosPersonal["CUILHijo4"] != null && !string.IsNullOrEmpty(DatosPersonal["CUILHijo4"].ToString()) ? DatosPersonal["CUILHijo4"].ToString() : String.Empty);

            txtNombreApellidoHijos5.Text = (DatosPersonal["NombreApellidoHijo5"] != null && !string.IsNullOrEmpty(DatosPersonal["NombreApellidoHijo5"].ToString()) ? DatosPersonal["NombreApellidoHijo5"].ToString() : String.Empty);
            if (DatosPersonal["FechaNacimientoHijo5"] != null)
            {
                dtFechaNacimientoHijos5.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaNacimientoHijo5"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaNacimientoHijo5"].ToString()) ? DatosPersonal["FechaNacimientoHijo5"].ToString() : String.Empty);
            }
            txtNacionalidadHijos5.Text = (DatosPersonal["NacionalidadHijo5"] != null && !string.IsNullOrEmpty(DatosPersonal["NacionalidadHijo5"].ToString()) ? DatosPersonal["NacionalidadHijo5"].ToString() : String.Empty);
            txtCuilHijos5.Text = (DatosPersonal["CUILHijo5"] != null && !string.IsNullOrEmpty(DatosPersonal["CUILHijo5"].ToString()) ? DatosPersonal["CUILHijo5"].ToString() : String.Empty);

            txtNombreApellidoHijos6.Text = (DatosPersonal["NombreApellidoHijo6"] != null && !string.IsNullOrEmpty(DatosPersonal["NombreApellidoHijo6"].ToString()) ? DatosPersonal["NombreApellidoHijo6"].ToString() : String.Empty);
            if (DatosPersonal["FechaNacimientoHijo6"] != null)
            {
                dtFechaNacimientoHijos6.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaNacimientoHijo6"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaNacimientoHijo6"].ToString()) ? DatosPersonal["FechaNacimientoHijo6"].ToString() : String.Empty);
            }
            txtNacionalidadHijos6.Text = (DatosPersonal["NacionalidadHijo6"] != null && !string.IsNullOrEmpty(DatosPersonal["NacionalidadHijo6"].ToString()) ? DatosPersonal["NacionalidadHijo6"].ToString() : String.Empty);
            txtCuilHijos6.Text = (DatosPersonal["CUILHijo6"] != null && !string.IsNullOrEmpty(DatosPersonal["CUILHijo6"].ToString()) ? DatosPersonal["CUILHijo6"].ToString() : String.Empty);
            //Estuidos Cursados
            if (DatosPersonal["EstudiosPrimarios"] != null && !string.IsNullOrEmpty(DatosPersonal["EstudiosPrimarios"].ToString()))
            {
                cboEstuidosPrimarios.SelectedValue = (DatosPersonal["EstudiosPrimarios"] != null && !string.IsNullOrEmpty(DatosPersonal["EstudiosPrimarios"].ToString()) ? DatosPersonal["EstudiosPrimarios"].ToString() : String.Empty);
            }
            if (DatosPersonal["FechaDesdePrimarios"] != null)
            {
                dtDesdePrimarios.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdePrimarios"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdePrimarios"].ToString()) ? DatosPersonal["FechaDesdePrimarios"].ToString() : String.Empty);
            }
            if (DatosPersonal["FechaHastaPrimarios"] != null)
            {
                dtHastaPrimarios.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaPrimarios"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaPrimarios"].ToString()) ? DatosPersonal["FechaHastaPrimarios"].ToString() : String.Empty);
            }
            txtInstitucionPrimarios.Text = (DatosPersonal["InstitucionPrimarios"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionPrimarios"].ToString()) ? DatosPersonal["InstitucionPrimarios"].ToString() : String.Empty);
            txtTituloPrimarios.Text = (DatosPersonal["TituloPrimarios"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloPrimarios"].ToString()) ? DatosPersonal["TituloPrimarios"].ToString() : String.Empty);

            if (DatosPersonal["EstudiosSecundarios"] != null && !string.IsNullOrEmpty(DatosPersonal["EstudiosSecundarios"].ToString()))
            {
                cboEstuidosSecundarios.SelectedValue = (DatosPersonal["EstudiosSecundarios"] != null && !string.IsNullOrEmpty(DatosPersonal["EstudiosSecundarios"].ToString()) ? DatosPersonal["EstudiosSecundarios"].ToString() : String.Empty);
            }
            if (DatosPersonal["FechaDesdeSecundarios"] != null)
            {
                dtDesdeSecundarios.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeSecundarios"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeSecundarios"].ToString()) ? DatosPersonal["FechaDesdeSecundarios"].ToString() : String.Empty);
            }
            if (DatosPersonal["FechaHastaSecundarios"] != null)
            {
                dtHastaSecundarios.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaSecundarios"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaSecundarios"].ToString()) ? DatosPersonal["FechaHastaSecundarios"].ToString() : String.Empty);
            }
            txtInstitucionSecundarios.Text = (DatosPersonal["InstitucionSecundarios"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionSecundarios"].ToString()) ? DatosPersonal["InstitucionSecundarios"].ToString() : String.Empty);
            txtTituloSecundarios.Text = (DatosPersonal["TituloSecundarios"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloSecundarios"].ToString()) ? DatosPersonal["TituloSecundarios"].ToString() : String.Empty);

            if (DatosPersonal["EstudiosTerciarios"] != null && !string.IsNullOrEmpty(DatosPersonal["EstudiosTerciarios"].ToString()))
            {
                cboEstuidosTerciarios.SelectedValue = (DatosPersonal["EstudiosTerciarios"] != null && !string.IsNullOrEmpty(DatosPersonal["EstudiosTerciarios"].ToString()) ? DatosPersonal["EstudiosTerciarios"].ToString() : String.Empty);
                if (DatosPersonal["EstudiosTerciarios"].ToString() == "NO CORRESPONDE")
                {
                    pnlTerciario.Visible = false;
                }
            }
            if (DatosPersonal["FechaDesdeTerciarios"] != null)
            {
                dtDesdeTerciarios.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeTerciarios"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeTerciarios"].ToString()) ? DatosPersonal["FechaDesdeTerciarios"].ToString() : String.Empty);
            }
            if (DatosPersonal["FechaHastaTerciarios"] != null)
            {
                dtHastaTerciarios.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaTerciarios"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaTerciarios"].ToString()) ? DatosPersonal["FechaHastaTerciarios"].ToString() : String.Empty);
            }
            txtInstitucionTerciarios.Text = (DatosPersonal["InstitucionTerciarios"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionTerciarios"].ToString()) ? DatosPersonal["InstitucionTerciarios"].ToString() : String.Empty);
            txtTituloTerciarios.Text = (DatosPersonal["TituloTerciarios"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloTerciarios"].ToString()) ? DatosPersonal["TituloTerciarios"].ToString() : String.Empty);

            if (DatosPersonal["EstudiosUniversitarios"] != null && !string.IsNullOrEmpty(DatosPersonal["EstudiosUniversitarios"].ToString()))
            {
                cboEstuidosUniversitarios.SelectedValue = (DatosPersonal["EstudiosUniversitarios"] != null && !string.IsNullOrEmpty(DatosPersonal["EstudiosUniversitarios"].ToString()) ? DatosPersonal["EstudiosUniversitarios"].ToString() : String.Empty);
                if (DatosPersonal["EstudiosUniversitarios"].ToString() == "NO CORRESPONDE")
                {
                    pnlEstudiosUniversitarios.Visible = false;
                }
            }
            if (DatosPersonal["FechaDesdeUniversitarios"] != null)
            {
                dtDesdeUniversitarios.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeUniversitarios"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeUniversitarios"].ToString()) ? DatosPersonal["FechaDesdeUniversitarios"].ToString() : String.Empty);
            }
            if (DatosPersonal["FechaHastaUniversitarios"] != null)
            {
                dtHastaUniversitarios.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaUniversitarios"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaUniversitarios"].ToString()) ? DatosPersonal["FechaHastaUniversitarios"].ToString() : String.Empty);
            }
            txtInstitucionUniversitarios.Text = (DatosPersonal["InstitucionUniversitarios"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionUniversitarios"].ToString()) ? DatosPersonal["InstitucionUniversitarios"].ToString() : String.Empty);
            txtTituloUniversitarios.Text = (DatosPersonal["TituloUniversitarios"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloUniversitarios"].ToString()) ? DatosPersonal["TituloUniversitarios"].ToString() : String.Empty);

            if (DatosPersonal["Curso1"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso1"].ToString()))
            {
                divCurso1.Style.Add("display", "block");
                cboCurso1.SelectedValue = (DatosPersonal["Curso1"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso1"].ToString()) ? DatosPersonal["Curso1"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso1"] != null)
                {
                    dtDesdeCurso1.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso1"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso1"].ToString()) ? DatosPersonal["FechaDesdeCurso1"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso1"] != null)
                {
                    dtHastaCurso1.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso1"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso1"].ToString()) ? DatosPersonal["FechaHastaCurso1"].ToString() : String.Empty);
                }
                txtInstitucionCurso1.Text = (DatosPersonal["InstitucionCurso1"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso1"].ToString()) ? DatosPersonal["InstitucionCurso1"].ToString() : String.Empty);
                txtTituloCurso1.Text = (DatosPersonal["TituloCurso1"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso1"].ToString()) ? DatosPersonal["TituloCurso1"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso2"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso2"].ToString()))
            {
                divCurso2.Style.Add("display", "block");
                cboCurso2.SelectedValue = (DatosPersonal["Curso2"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso2"].ToString()) ? DatosPersonal["Curso2"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso2"] != null)
                {
                    dtDesdeCurso2.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso2"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso2"].ToString()) ? DatosPersonal["FechaDesdeCurso2"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso2"] != null)
                {
                    dtHastaCurso2.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso2"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso2"].ToString()) ? DatosPersonal["FechaHastaCurso2"].ToString() : String.Empty);
                }
                txtInstitucionCurso2.Text = (DatosPersonal["InstitucionCurso2"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso2"].ToString()) ? DatosPersonal["InstitucionCurso2"].ToString() : String.Empty);
                txtTituloCurso2.Text = (DatosPersonal["TituloCurso2"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso2"].ToString()) ? DatosPersonal["TituloCurso2"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso3"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso3"].ToString()))
            {
                divCurso3.Style.Add("display", "block");
                cboCurso3.SelectedValue = (DatosPersonal["Curso3"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso3"].ToString()) ? DatosPersonal["Curso3"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso3"] != null)
                {
                    dtDesdeCurso3.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso3"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso3"].ToString()) ? DatosPersonal["FechaDesdeCurso3"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso3"] != null)
                {
                    dtHastaCurso3.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso3"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso3"].ToString()) ? DatosPersonal["FechaHastaCurso3"].ToString() : String.Empty);
                }
                txtInstitucionCurso3.Text = (DatosPersonal["InstitucionCurso3"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso3"].ToString()) ? DatosPersonal["InstitucionCurso3"].ToString() : String.Empty);
                txtTituloCurso3.Text = (DatosPersonal["TituloCurso3"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso3"].ToString()) ? DatosPersonal["TituloCurso3"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso4"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso4"].ToString()))
            {
                divCurso4.Style.Add("display", "block");
                cboCurso4.SelectedValue = (DatosPersonal["Curso4"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso4"].ToString()) ? DatosPersonal["Curso4"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso4"] != null)
                {
                    dtDesdeCurso4.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso4"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso4"].ToString()) ? DatosPersonal["FechaDesdeCurso4"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso4"] != null)
                {
                    dtHastaCurso4.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso4"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso4"].ToString()) ? DatosPersonal["FechaHastaCurso4"].ToString() : String.Empty);
                }
                txtInstitucionCurso4.Text = (DatosPersonal["InstitucionCurso4"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso4"].ToString()) ? DatosPersonal["InstitucionCurso4"].ToString() : String.Empty);
                txtTituloCurso4.Text = (DatosPersonal["TituloCurso4"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso4"].ToString()) ? DatosPersonal["TituloCurso4"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso5"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso5"].ToString()))
            {
                divCurso5.Style.Add("display", "block");
                cboCurso5.SelectedValue = (DatosPersonal["Curso5"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso5"].ToString()) ? DatosPersonal["Curso5"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso5"] != null)
                {
                    dtDesdeCurso5.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso5"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso5"].ToString()) ? DatosPersonal["FechaDesdeCurso5"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso5"] != null)
                {
                    dtHastaCurso5.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso5"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso5"].ToString()) ? DatosPersonal["FechaHastaCurso5"].ToString() : String.Empty);
                }
                txtInstitucionCurso5.Text = (DatosPersonal["InstitucionCurso5"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso5"].ToString()) ? DatosPersonal["InstitucionCurso5"].ToString() : String.Empty);
                txtTituloCurso5.Text = (DatosPersonal["TituloCurso5"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso5"].ToString()) ? DatosPersonal["TituloCurso5"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso6"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso6"].ToString()))
            {
                divCurso6.Style.Add("display", "block");
                cboCurso6.SelectedValue = (DatosPersonal["Curso6"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso6"].ToString()) ? DatosPersonal["Curso6"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso6"] != null)
                {
                    dtDesdeCurso6.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso6"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso6"].ToString()) ? DatosPersonal["FechaDesdeCurso6"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso6"] != null)
                {
                    dtHastaCurso6.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso6"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso6"].ToString()) ? DatosPersonal["FechaHastaCurso6"].ToString() : String.Empty);
                }
                txtInstitucionCurso6.Text = (DatosPersonal["InstitucionCurso6"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso6"].ToString()) ? DatosPersonal["InstitucionCurso6"].ToString() : String.Empty);
                txtTituloCurso6.Text = (DatosPersonal["TituloCurso6"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso6"].ToString()) ? DatosPersonal["TituloCurso6"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso7"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso7"].ToString()))
            {
                divCurso7.Style.Add("display", "block");
                cboCurso7.SelectedValue = (DatosPersonal["Curso7"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso7"].ToString()) ? DatosPersonal["Curso7"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso7"] != null)
                {
                    dtDesdeCurso7.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso7"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso7"].ToString()) ? DatosPersonal["FechaDesdeCurso7"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso7"] != null)
                {
                    dtHastaCurso7.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso7"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso7"].ToString()) ? DatosPersonal["FechaHastaCurso7"].ToString() : String.Empty);
                }
                txtInstitucionCurso7.Text = (DatosPersonal["InstitucionCurso7"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso7"].ToString()) ? DatosPersonal["InstitucionCurso7"].ToString() : String.Empty);
                txtTituloCurso7.Text = (DatosPersonal["TituloCurso7"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso7"].ToString()) ? DatosPersonal["TituloCurso7"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso8"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso8"].ToString()))
            {
                divCurso8.Style.Add("display", "block");
                cboCurso8.SelectedValue = (DatosPersonal["Curso8"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso8"].ToString()) ? DatosPersonal["Curso8"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso8"] != null)
                {
                    dtDesdeCurso8.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso8"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso8"].ToString()) ? DatosPersonal["FechaDesdeCurso8"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso8"] != null)
                {
                    dtHastaCurso8.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso8"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso8"].ToString()) ? DatosPersonal["FechaHastaCurso8"].ToString() : String.Empty);
                }
                txtInstitucionCurso8.Text = (DatosPersonal["InstitucionCurso8"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso8"].ToString()) ? DatosPersonal["InstitucionCurso8"].ToString() : String.Empty);
                txtTituloCurso8.Text = (DatosPersonal["TituloCurso8"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso8"].ToString()) ? DatosPersonal["TituloCurso8"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso9"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso9"].ToString()))
            {
                divCurso9.Style.Add("display", "block");
                cboCurso9.SelectedValue = (DatosPersonal["Curso9"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso9"].ToString()) ? DatosPersonal["Curso9"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso9"] != null)
                {
                    dtDesdeCurso9.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso9"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso9"].ToString()) ? DatosPersonal["FechaDesdeCurso9"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso9"] != null)
                {
                    dtHastaCurso9.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso9"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso9"].ToString()) ? DatosPersonal["FechaHastaCurso9"].ToString() : String.Empty);
                }
                txtInstitucionCurso9.Text = (DatosPersonal["InstitucionCurso9"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso9"].ToString()) ? DatosPersonal["InstitucionCurso9"].ToString() : String.Empty);
                txtTituloCurso9.Text = (DatosPersonal["TituloCurso9"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso9"].ToString()) ? DatosPersonal["TituloCurso9"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso10"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso10"].ToString()))
            {
                divCurso10.Style.Add("display", "block");
                cboCurso10.SelectedValue = (DatosPersonal["Curso10"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso10"].ToString()) ? DatosPersonal["Curso10"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso10"] != null)
                {
                    dtDesdeCurso10.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso10"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso10"].ToString()) ? DatosPersonal["FechaDesdeCurso10"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso10"] != null)
                {
                    dtHastaCurso10.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso10"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso10"].ToString()) ? DatosPersonal["FechaHastaCurso10"].ToString() : String.Empty);
                }
                txtInstitucionCurso10.Text = (DatosPersonal["InstitucionCurso10"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso10"].ToString()) ? DatosPersonal["InstitucionCurso10"].ToString() : String.Empty);
                txtTituloCurso10.Text = (DatosPersonal["TituloCurso10"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso10"].ToString()) ? DatosPersonal["TituloCurso10"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso11"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso11"].ToString()))
            {
                divCurso11.Style.Add("display", "block");
                cboCurso11.SelectedValue = (DatosPersonal["Curso11"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso11"].ToString()) ? DatosPersonal["Curso11"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso11"] != null)
                {
                    dtDesdeCurso11.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso11"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso11"].ToString()) ? DatosPersonal["FechaDesdeCurso11"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso11"] != null)
                {
                    dtHastaCurso11.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso11"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso11"].ToString()) ? DatosPersonal["FechaHastaCurso11"].ToString() : String.Empty);
                }
                txtInstitucionCurso11.Text = (DatosPersonal["InstitucionCurso11"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso11"].ToString()) ? DatosPersonal["InstitucionCurso11"].ToString() : String.Empty);
                txtTituloCurso11.Text = (DatosPersonal["TituloCurso11"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso11"].ToString()) ? DatosPersonal["TituloCurso11"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso12"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso12"].ToString()))
            {
                divCurso12.Style.Add("display", "block");
                cboCurso12.SelectedValue = (DatosPersonal["Curso12"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso12"].ToString()) ? DatosPersonal["Curso12"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso12"] != null)
                {
                    dtDesdeCurso12.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso12"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso12"].ToString()) ? DatosPersonal["FechaDesdeCurso12"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso12"] != null)
                {
                    dtHastaCurso12.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso12"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso12"].ToString()) ? DatosPersonal["FechaHastaCurso12"].ToString() : String.Empty);
                }
                txtInstitucionCurso12.Text = (DatosPersonal["InstitucionCurso12"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso12"].ToString()) ? DatosPersonal["InstitucionCurso12"].ToString() : String.Empty);
                txtTituloCurso12.Text = (DatosPersonal["TituloCurso12"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso12"].ToString()) ? DatosPersonal["TituloCurso12"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso13"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso13"].ToString()))
            {
                divCurso13.Style.Add("display", "block");
                cboCurso13.SelectedValue = (DatosPersonal["Curso13"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso13"].ToString()) ? DatosPersonal["Curso13"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso13"] != null)
                {
                    dtDesdeCurso13.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso13"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso13"].ToString()) ? DatosPersonal["FechaDesdeCurso13"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso13"] != null)
                {
                    dtHastaCurso13.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso13"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso13"].ToString()) ? DatosPersonal["FechaHastaCurso13"].ToString() : String.Empty);
                }
                txtInstitucionCurso13.Text = (DatosPersonal["InstitucionCurso13"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso13"].ToString()) ? DatosPersonal["InstitucionCurso13"].ToString() : String.Empty);
                txtTituloCurso13.Text = (DatosPersonal["TituloCurso13"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso13"].ToString()) ? DatosPersonal["TituloCurso13"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso14"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso14"].ToString()))
            {
                divCurso14.Style.Add("display", "block");
                cboCurso14.SelectedValue = (DatosPersonal["Curso14"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso14"].ToString()) ? DatosPersonal["Curso14"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso14"] != null)
                {
                    dtDesdeCurso14.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso14"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso14"].ToString()) ? DatosPersonal["FechaDesdeCurso14"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso14"] != null)
                {
                    dtHastaCurso14.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso14"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso14"].ToString()) ? DatosPersonal["FechaHastaCurso14"].ToString() : String.Empty);
                }
                txtInstitucionCurso14.Text = (DatosPersonal["InstitucionCurso14"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso14"].ToString()) ? DatosPersonal["InstitucionCurso14"].ToString() : String.Empty);
                txtTituloCurso14.Text = (DatosPersonal["TituloCurso14"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso14"].ToString()) ? DatosPersonal["TituloCurso14"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso15"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso15"].ToString()))
            {
                divCurso15.Style.Add("display", "block");
                cboCurso15.SelectedValue = (DatosPersonal["Curso15"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso15"].ToString()) ? DatosPersonal["Curso15"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso15"] != null)
                {
                    dtDesdeCurso15.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso15"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso15"].ToString()) ? DatosPersonal["FechaDesdeCurso15"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso15"] != null)
                {
                    dtHastaCurso15.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso15"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso15"].ToString()) ? DatosPersonal["FechaHastaCurso15"].ToString() : String.Empty);
                }
                txtInstitucionCurso15.Text = (DatosPersonal["InstitucionCurso15"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso15"].ToString()) ? DatosPersonal["InstitucionCurso15"].ToString() : String.Empty);
                txtTituloCurso15.Text = (DatosPersonal["TituloCurso15"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso15"].ToString()) ? DatosPersonal["TituloCurso15"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso16"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso16"].ToString()))
            {
                divCurso16.Style.Add("display", "block");
                cboCurso16.SelectedValue = (DatosPersonal["Curso16"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso16"].ToString()) ? DatosPersonal["Curso16"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso16"] != null)
                {
                    dtDesdeCurso16.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso16"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso16"].ToString()) ? DatosPersonal["FechaDesdeCurso16"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso16"] != null)
                {
                    dtHastaCurso16.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso16"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso16"].ToString()) ? DatosPersonal["FechaHastaCurso16"].ToString() : String.Empty);
                }
                txtInstitucionCurso16.Text = (DatosPersonal["InstitucionCurso16"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso16"].ToString()) ? DatosPersonal["InstitucionCurso16"].ToString() : String.Empty);
                txtTituloCurso16.Text = (DatosPersonal["TituloCurso16"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso16"].ToString()) ? DatosPersonal["TituloCurso16"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso17"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso17"].ToString()))
            {
                divCurso17.Style.Add("display", "block");
                cboCurso17.SelectedValue = (DatosPersonal["Curso17"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso17"].ToString()) ? DatosPersonal["Curso17"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso17"] != null)
                {
                    dtDesdeCurso17.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso17"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso17"].ToString()) ? DatosPersonal["FechaDesdeCurso17"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso17"] != null)
                {
                    dtHastaCurso17.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso17"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso17"].ToString()) ? DatosPersonal["FechaHastaCurso17"].ToString() : String.Empty);
                }
                txtInstitucionCurso17.Text = (DatosPersonal["InstitucionCurso17"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso17"].ToString()) ? DatosPersonal["InstitucionCurso17"].ToString() : String.Empty);
                txtTituloCurso17.Text = (DatosPersonal["TituloCurso17"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso17"].ToString()) ? DatosPersonal["TituloCurso17"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso18"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso18"].ToString()))
            {
                divCurso18.Style.Add("display", "block");
                cboCurso18.SelectedValue = (DatosPersonal["Curso18"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso18"].ToString()) ? DatosPersonal["Curso18"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso18"] != null)
                {
                    dtDesdeCurso18.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso18"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso18"].ToString()) ? DatosPersonal["FechaDesdeCurso18"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso18"] != null)
                {
                    dtHastaCurso18.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso18"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso18"].ToString()) ? DatosPersonal["FechaHastaCurso18"].ToString() : String.Empty);
                }
                txtInstitucionCurso18.Text = (DatosPersonal["InstitucionCurso18"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso18"].ToString()) ? DatosPersonal["InstitucionCurso18"].ToString() : String.Empty);
                txtTituloCurso18.Text = (DatosPersonal["TituloCurso18"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso18"].ToString()) ? DatosPersonal["TituloCurso18"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso19"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso19"].ToString()))
            {
                divCurso19.Style.Add("display", "block");
                cboCurso19.SelectedValue = (DatosPersonal["Curso19"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso19"].ToString()) ? DatosPersonal["Curso19"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso19"] != null)
                {
                    dtDesdeCurso19.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso19"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso19"].ToString()) ? DatosPersonal["FechaDesdeCurso19"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso19"] != null)
                {
                    dtHastaCurso19.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso19"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso19"].ToString()) ? DatosPersonal["FechaHastaCurso19"].ToString() : String.Empty);
                }
                txtInstitucionCurso19.Text = (DatosPersonal["InstitucionCurso19"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso19"].ToString()) ? DatosPersonal["InstitucionCurso19"].ToString() : String.Empty);
                txtTituloCurso19.Text = (DatosPersonal["TituloCurso19"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso19"].ToString()) ? DatosPersonal["TituloCurso19"].ToString() : String.Empty);
            }
            if (DatosPersonal["Curso20"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso20"].ToString()))
            {
                divCurso20.Style.Add("display", "block");
                cboCurso20.SelectedValue = (DatosPersonal["Curso20"] != null && !string.IsNullOrEmpty(DatosPersonal["Curso20"].ToString()) ? DatosPersonal["Curso20"].ToString() : String.Empty);
                if (DatosPersonal["FechaDesdeCurso20"] != null)
                {
                    dtDesdeCurso20.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaDesdeCurso20"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaDesdeCurso20"].ToString()) ? DatosPersonal["FechaDesdeCurso20"].ToString() : String.Empty);
                }
                if (DatosPersonal["FechaHastaCurso20"] != null)
                {
                    dtHastaCurso20.SelectedDate = Convert.ToDateTime(DatosPersonal["FechaHastaCurso20"] != null && !string.IsNullOrEmpty(DatosPersonal["FechaHastaCurso20"].ToString()) ? DatosPersonal["FechaHastaCurso20"].ToString() : String.Empty);
                }
                txtInstitucionCurso20.Text = (DatosPersonal["InstitucionCurso20"] != null && !string.IsNullOrEmpty(DatosPersonal["InstitucionCurso20"].ToString()) ? DatosPersonal["InstitucionCurso20"].ToString() : String.Empty);
                txtTituloCurso20.Text = (DatosPersonal["TituloCurso20"] != null && !string.IsNullOrEmpty(DatosPersonal["TituloCurso20"].ToString()) ? DatosPersonal["TituloCurso20"].ToString() : String.Empty);
            }
        }
        private void CargarAdjuntos(SPListItem AltaProveedor)
        {
            SPAttachmentCollection collAttachments = AltaProveedor.Attachments;
            chkBoxList.Items.Clear();
            // Variable para rastrear si se encontró algún adjunto que no sea "FotoPerfil"
            bool tieneAdjuntosNoFotoPerfil = false;
            foreach (string nombreArchivo in collAttachments)
            {
                if (nombreArchivo.Contains("FotoPerfil"))
                {
                    imgPerfil.ImageUrl = collAttachments.UrlPrefix + nombreArchivo;
                }
                else
                {
                    tieneAdjuntosNoFotoPerfil = true;
                    ListItem itemChk = new ListItem();
                    itemChk.Value = nombreArchivo;
                    itemChk.Text = "<a class='vinculoIMGS' href='" + collAttachments.UrlPrefix + nombreArchivo + "' target='_blank' title='Click para ver la imagen'> " + nombreArchivo.ToString().Split('-')[0] + "</a></br>";
                    chkBoxList.Items.Add(itemChk);
                }
            }
            // Mostrar el botón "Eliminar" solo si hay adjuntos, excepto "FotoPerfil"
            if (tieneAdjuntosNoFotoPerfil == true)
            {
                if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("RRHH")))
                {
                    btnEliminar.Visible = true;
                }
                else
                {
                    btnEliminar.Visible = false;
                }
            }
            // btnEliminar.Visible = tieneAdjuntosNoFotoPerfil;
            // Mostrar el mensaje de advertencia si no hay adjuntos
            if (!tieneAdjuntosNoFotoPerfil)
            {
                alertaArchivoAdjuntos.Visible = true; // Mostrar el div de advertencia
                alertaArchivoAdjuntos.InnerText = "Aún no hay archivos adjuntos.";
            }
            else
            {
                alertaArchivoAdjuntos.Visible = false; // Ocultar el div de advertencia
            }
        }
        private void CargarCombos()
        {
            using (SPSite site = new SPSite(sSitio))
            {
                SPQuery query = new SPQuery();
                string QuerySTR = "<View>" +
                    "<Query><Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>Sociedad</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Activo</Value></Eq></And></Where></Query>" +
                        "</View>";
                query.ViewXml = QuerySTR;
                SPListItemCollection Pais = site.OpenWeb().Lists["DatosPersonalAux"].GetItems(query);
                if (Pais.Count > 0)
                {
                    DataView view1 = new DataView(Pais.GetDataTable());
                    DataTable distinctValues1 = view1.ToTable(true, "Valor");
                    DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
                    rFilaSeleccioneSolicitud["Valor"] = strSeleccione;
                    distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
                    cboSociedad.DataSource = distinctValues1;
                    cboSociedad.DataValueField = "Valor";
                    cboSociedad.DataTextField = "Valor";
                    cboSociedad.DataBind();
                }
                SPQuery queryGenero = new SPQuery();
                string QuerySTRGenero = "<View>" +
                    "<Query><Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>Genero</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Activo</Value></Eq></And></Where></Query>" +
                        "</View>";
                queryGenero.ViewXml = QuerySTRGenero;
                SPListItemCollection Genero = site.OpenWeb().Lists["DatosPersonalAux"].GetItems(queryGenero);
                if (Genero.Count > 0)
                {
                    DataView view1 = new DataView(Genero.GetDataTable());
                    DataTable distinctValues1 = view1.ToTable(true, "Valor");
                    DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
                    rFilaSeleccioneSolicitud["Valor"] = strSeleccione;
                    distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
                    cboGenero.DataSource = distinctValues1;
                    cboGenero.DataValueField = "Valor";
                    cboGenero.DataTextField = "Valor";
                    cboGenero.DataBind();
                }
                SPQuery queryEstadoCivil = new SPQuery();
                string QuerySTREstadoCivil = "<View>" +
                    "<Query><Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>EstadoCivil</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Activo</Value></Eq></And></Where></Query>" +
                        "</View>";
                queryEstadoCivil.ViewXml = QuerySTREstadoCivil;
                SPListItemCollection EstadoCivil = site.OpenWeb().Lists["DatosPersonalAux"].GetItems(queryEstadoCivil);
                if (EstadoCivil.Count > 0)
                {
                    DataView view1 = new DataView(EstadoCivil.GetDataTable());
                    DataTable distinctValues1 = view1.ToTable(true, "Valor");
                    DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
                    rFilaSeleccioneSolicitud["Valor"] = strSeleccione;
                    distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
                    cboEstadoCivil.DataSource = distinctValues1;
                    cboEstadoCivil.DataValueField = "Valor";
                    cboEstadoCivil.DataTextField = "Valor";
                    cboEstadoCivil.DataBind();
                }
                SPQuery queryProv = new SPQuery();
                string QuerySTRProv = "<View>" +
                    "<Query><Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>Provincia</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Activo</Value></Eq></And></Where><OrderBy><FieldRef Name='Valor' Ascending='True' /></OrderBy></Query>" +
                        "</View>";
                queryProv.ViewXml = QuerySTRProv;
                SPListItemCollection Provincia = site.OpenWeb().Lists["DatosPersonalAux"].GetItems(queryProv);
                if (Provincia.Count > 0)
                {
                    DataView view1 = new DataView(Provincia.GetDataTable());
                    DataTable distinctValues1 = view1.ToTable(true, "Valor");
                    DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
                    rFilaSeleccioneSolicitud["Valor"] = strSeleccione;
                    distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
                    cboProvincia.DataSource = distinctValues1;
                    cboProvincia.DataValueField = "Valor";
                    cboProvincia.DataTextField = "Valor";
                    cboProvincia.DataBind();
                }
            }
        }
        protected void btnGuardar(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                SPListItem RegistroExistente = SPContext.Current.Web.Lists["DatosPersonal"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                GuardarDatos(RegistroExistente);
                RegistroExistente.Update();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeGuarda();", true);
                if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("RRHH")))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeModificado(1);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeModificado(2);", true);
                }
            }
            else
            {
                //using (SPSite site = new SPSite(sSitio))
                //{
                //    using (SPWeb web = site.OpenWeb())
                //    {
                SPListItem RegistroNuevo = SPContext.Current.Web.Lists["DatosPersonal"].AddItem();
                GuardarDatos(RegistroNuevo);
                RegistroNuevo.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeGuarda();", true);
                //    }
                //}
            }
        }
        private void GuardarDatos(SPListItem Registro)
        {
            if (fuFotoPerfil.HasFile)
            {
                List<string> fileNames = new List<string>();
                foreach (string fileName in Registro.Attachments)
                {
                    fileNames.Add(fileName);
                }
                foreach (string fileName in fileNames)
                {
                    if (fileName.Contains("FotoPerfil"))
                    {
                        Registro.Attachments.Delete(fileName);
                    }
                }
                Stream fileStream = fuFotoPerfil.PostedFile.InputStream;
                byte[] fileBytes = new byte[fileStream.Length];
                fileStream.Read(fileBytes, 0, (int)fileStream.Length);
                fileStream.Close();
               
                string fileName1 = "FotoPerfil" + System.IO.Path.GetExtension(fuFotoPerfil.FileName) + "-" + Guid.NewGuid().ToString();
                Registro.Attachments.Add( fileName1, fileBytes);
            }
            Registro["Posicion"] = (txtPosicion.Text != null && !string.IsNullOrEmpty(txtPosicion.Text) ? txtPosicion.Text : String.Empty);
            Registro["Gerencia"] = (txtGerencia.Text != null && !string.IsNullOrEmpty(txtGerencia.Text) ? txtGerencia.Text : String.Empty);
            Registro["PisoPerfil"] = (txtPisoPerfil.Text != null && !string.IsNullOrEmpty(txtPisoPerfil.Text) ? txtPisoPerfil.Text : String.Empty);
            Registro["Sociedad"] = (cboSociedad.SelectedItem.Text != null && !string.IsNullOrEmpty(cboSociedad.SelectedItem.Text) ? cboSociedad.SelectedItem.Text : String.Empty);
            Registro["NombreApellido"] = (txtNombreApellido.Text != null && !string.IsNullOrEmpty(txtNombreApellido.Text) ? txtNombreApellido.Text : String.Empty);
            Registro["Legajo"] = (txtLegajo.Text != null && !string.IsNullOrEmpty(txtLegajo.Text) ? txtLegajo.Text : String.Empty);
            if (dtFecha.IsDateEmpty == false)
            {
                Registro["FechaNacimiento"] = dtFecha.SelectedDate.Date.ToShortDateString();
            }
            Registro["LugarNacimiento"] = (txtLugarNacimiento.Text != null && !string.IsNullOrEmpty(txtLugarNacimiento.Text) ? txtLugarNacimiento.Text : String.Empty);
            Registro["Genero"] = (cboGenero.SelectedItem.Text != null && !string.IsNullOrEmpty(cboGenero.SelectedItem.Text) ? cboGenero.SelectedItem.Text : String.Empty);
            Registro["EstadoCivil"] = (cboEstadoCivil.SelectedItem.Text != null && !string.IsNullOrEmpty(cboEstadoCivil.SelectedItem.Text) ? cboEstadoCivil.SelectedItem.Text : String.Empty);
            if (fuDNI.HasFiles)
            {
                int cont = 0;
                foreach (HttpPostedFile uploadedFile in fuDNI.PostedFiles)
                {
                    cont++;
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "DNI " + txtNombreApellido.Text + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (fuCuil.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuCuil.PostedFiles)
                {
                    Stream fs = fuCuil.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "CUIL  " + txtNombreApellido.Text + System.IO.Path.GetExtension(fuCuil.FileName);
                    attachments.Add( fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }

            Registro["CUIL"] = (txtCuil.Text != null && !string.IsNullOrEmpty(txtCuil.Text) ? txtCuil.Text : String.Empty);
            Registro["Provincia"] = (cboProvincia.SelectedItem.Text != null && !string.IsNullOrEmpty(cboProvincia.SelectedItem.Text) ? cboProvincia.SelectedItem.Text : String.Empty);
            Registro["Ciudad"] = (txtCiudad.Text != null && !string.IsNullOrEmpty(txtCiudad.Text) ? txtCiudad.Text : String.Empty);
            Registro["Calle"] = (txtCalle.Text != null && !string.IsNullOrEmpty(txtCalle.Text) ? txtCalle.Text : String.Empty);
            Registro["Altura"] = (txtAltura.Text != null && !string.IsNullOrEmpty(txtAltura.Text) ? txtAltura.Text : String.Empty);
            Registro["Piso"] = (txtPiso.Text != null && !string.IsNullOrEmpty(txtPiso.Text) ? txtPiso.Text : String.Empty);
            Registro["Depto"] = (txtDepto.Text != null && !string.IsNullOrEmpty(txtDepto.Text) ? txtDepto.Text : String.Empty);
            Registro["CodPostal"] = (txtCodPostal.Text != null && !string.IsNullOrEmpty(txtCodPostal.Text) ? txtCodPostal.Text : String.Empty);
            Registro["Telefono"] = (txtTelefono.Text != null && !string.IsNullOrEmpty(txtTelefono.Text) ? txtTelefono.Text : String.Empty);
            Registro["CelularPersonal"] = (txtCelularPersonal.Text != null && !string.IsNullOrEmpty(txtCelularPersonal.Text) ? txtCelularPersonal.Text : String.Empty);
            Registro["CelularEmpresa"] = (txtCelularEmpresa.Text != null && !string.IsNullOrEmpty(txtCelularEmpresa.Text) ? txtCelularEmpresa.Text : String.Empty);
            Registro["CompaniaCelularEmpresa"] = (txtCompaniaCelular.Text != null && !string.IsNullOrEmpty(txtCompaniaCelular.Text) ? txtCompaniaCelular.Text : String.Empty);
            Registro["LicenciaConducir"] = (cboLicenciaConducir.SelectedItem.Text != null && !string.IsNullOrEmpty(cboLicenciaConducir.SelectedItem.Text) ? cboLicenciaConducir.SelectedItem.Text : String.Empty);
            Registro["Alergico"] = (cboAlergias.SelectedItem.Text != null && !string.IsNullOrEmpty(cboAlergias.SelectedItem.Text) ? cboAlergias.SelectedItem.Text : String.Empty);
            if (dtVigenciaLicencia.IsDateEmpty == false)
            {
                Registro["VigenciaLicencia"] = dtVigenciaLicencia.SelectedDate.Date.ToShortDateString();
            }
            Registro["EspecificarAlergia"] = (txtEspecificar.Text != null && !string.IsNullOrEmpty(txtEspecificar.Text) ? txtEspecificar.Text : String.Empty);
            Registro["Categoria"] = (txtCategoria.Text != null && !string.IsNullOrEmpty(txtCategoria.Text) ? txtCategoria.Text : String.Empty);
            if (fuLicencia.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuLicencia.PostedFiles)
                {
                    Stream fs = fuLicencia.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Licencia Conducir" + System.IO.Path.GetExtension(fuLicencia.FileName);
                    attachments.Add( fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            Registro["DatosEmergenciaNombreApellido"] = (txtNombreApellidoEmergencia.Text != null && !string.IsNullOrEmpty(txtNombreApellidoEmergencia.Text) ? txtNombreApellidoEmergencia.Text : String.Empty);
            Registro["DatosEmergenciaVinculo"] = (txtVinculoEmergencia.Text != null && !string.IsNullOrEmpty(txtVinculoEmergencia.Text) ? txtVinculoEmergencia.Text : String.Empty);
            Registro["DatosEmergenciaTelefono"] = (txtTelefonoEmergencia.Text != null && !string.IsNullOrEmpty(txtTelefonoEmergencia.Text) ? txtTelefonoEmergencia.Text : String.Empty);
            //Estructura
            Registro["PuestoActual"] = (txtPuestoActual.Text != null && !string.IsNullOrEmpty(txtPuestoActual.Text) ? txtPuestoActual.Text : String.Empty);
            Registro["PuestoNuevo"] = (txtPuestoNuevo.Text != null && !string.IsNullOrEmpty(txtPuestoNuevo.Text) ? txtPuestoNuevo.Text : String.Empty);
            //Grupo Fliar
            Registro["NombreConyuge"] = (txtNombreConyuge.Text != null && !string.IsNullOrEmpty(txtNombreConyuge.Text) ? txtNombreConyuge.Text : String.Empty);
            if (dtFechaNacimientoConyuge.IsDateEmpty == false)
            {
                Registro["FechaNacimientoConyuge"] = dtFechaNacimientoConyuge.SelectedDate.Date.ToShortDateString();
            }
            if (fuDNIconyuge.HasFiles)
            {
                int cont = 0;
                foreach (HttpPostedFile uploadedFile in fuDNIconyuge.PostedFiles)
                {
                    cont++;
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "DNI  " + txtNombreConyuge.Text + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add( fileName + Guid.NewGuid().ToString(), fileContents);
                }
            }
            Registro["CUILconyuge"] = (txtCuilConyuge.Text != null && !string.IsNullOrEmpty(txtCuilConyuge.Text) ? txtCuilConyuge.Text : String.Empty);
            if (fuCUILconyuge.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuCUILconyuge.PostedFiles)
                {
                    Stream fs = fuCUILconyuge.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "CUIL  " + txtNombreConyuge.Text + System.IO.Path.GetExtension(fuCUILconyuge.FileName);
                    attachments.Add( fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            Registro["CantidadHijos"] = (cboCantidadHijos.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCantidadHijos.SelectedItem.Text) ? cboCantidadHijos.SelectedItem.Text : String.Empty);
            Registro["NombreApellidoHijo1"] = (txtNombreApellidoHijos1.Text != null && !string.IsNullOrEmpty(txtNombreApellidoHijos1.Text) ? txtNombreApellidoHijos1.Text : String.Empty);
            if (dtFechaNacimientoHijos1.IsDateEmpty == false)
            {
                Registro["FechaNacimientoHijo1"] = dtFechaNacimientoHijos1.SelectedDate.Date.ToShortDateString();
            }
            if (fuDNIHijos1.HasFiles)
            {
                int cont = 0;
                foreach (HttpPostedFile uploadedFile in fuDNIHijos1.PostedFiles)
                {
                    cont++;
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "DNI  " + txtNombreApellidoHijos1.Text + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (fuPartidaNacimientoHijos1.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuPartidaNacimientoHijos1.PostedFiles)
                {
                    Stream fs = fuPartidaNacimientoHijos1.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Partida Nacimiento  " + txtNombreApellidoHijos1.Text + System.IO.Path.GetExtension(fuPartidaNacimientoHijos1.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            Registro["NacionalidadHijo1"] = (txtNacionalidadHijos1.Text != null && !string.IsNullOrEmpty(txtNacionalidadHijos1.Text) ? txtNacionalidadHijos1.Text : String.Empty);
            Registro["CUILHijo1"] = (txtCuilHijos1.Text != null && !string.IsNullOrEmpty(txtCuilHijos1.Text) ? txtCuilHijos1.Text : String.Empty);
            if (fuCUILHijos1.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuCUILHijos1.PostedFiles)
                {
                    Stream fs = fuCUILHijos1.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "CUIL  " + txtNombreApellidoHijos1.Text + System.IO.Path.GetExtension(fuCUILHijos1.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }

            Registro["NombreApellidoHijo2"] = (txtNombreApellidoHijos2.Text != null && !string.IsNullOrEmpty(txtNombreApellidoHijos2.Text) ? txtNombreApellidoHijos2.Text : String.Empty);
            if (dtFechaNacimientoHijos2.IsDateEmpty == false)
            {
                Registro["FechaNacimientoHijo2"] = dtFechaNacimientoHijos2.SelectedDate.Date.ToShortDateString();
            }
            if (fuDNIHijos2.HasFiles)
            {
                int cont = 0;
                foreach (HttpPostedFile uploadedFile in fuDNIHijos2.PostedFiles)
                {
                    cont++;
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "DNI  " + txtNombreApellidoHijos2.Text + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (fuPartidaNacimientoHijos2.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuPartidaNacimientoHijos2.PostedFiles)
                {
                    Stream fs = fuPartidaNacimientoHijos2.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Partida Nacimiento  " + txtNombreApellidoHijos2.Text + System.IO.Path.GetExtension(fuPartidaNacimientoHijos2.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            Registro["NacionalidadHijo2"] = (txtNacionalidadHijos2.Text != null && !string.IsNullOrEmpty(txtNacionalidadHijos2.Text) ? txtNacionalidadHijos2.Text : String.Empty);
            Registro["CUILHijo2"] = (txtCuilHijos2.Text != null && !string.IsNullOrEmpty(txtCuilHijos2.Text) ? txtCuilHijos2.Text : String.Empty);
            if (fuCUILHijos2.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuCUILHijos2.PostedFiles)
                {
                    Stream fs = fuCUILHijos2.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "CUIL  " + txtNombreApellidoHijos2.Text + System.IO.Path.GetExtension(fuCUILHijos2.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }

            Registro["NombreApellidoHijo3"] = (txtNombreApellidoHijos3.Text != null && !string.IsNullOrEmpty(txtNombreApellidoHijos3.Text) ? txtNombreApellidoHijos3.Text : String.Empty);
            if (dtFechaNacimientoHijos3.IsDateEmpty == false)
            {
                Registro["FechaNacimientoHijo3"] = dtFechaNacimientoHijos3.SelectedDate.Date.ToShortDateString();
            }
            if (fuDNIHijos3.HasFiles)
            {
                int cont = 0;
                foreach (HttpPostedFile uploadedFile in fuDNIHijos3.PostedFiles)
                {
                    cont++;
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "DNI  " + txtNombreApellidoHijos3.Text + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (fuPartidaNacimientoHijos3.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuPartidaNacimientoHijos3.PostedFiles)
                {
                    Stream fs = fuPartidaNacimientoHijos3.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Partida Nacimiento - " + txtNombreApellidoHijos3.Text + System.IO.Path.GetExtension(fuPartidaNacimientoHijos3.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            Registro["NacionalidadHijo3"] = (txtNacionalidadHijos3.Text != null && !string.IsNullOrEmpty(txtNacionalidadHijos3.Text) ? txtNacionalidadHijos3.Text : String.Empty);
            Registro["CUILHijo3"] = (txtCuilHijos3.Text != null && !string.IsNullOrEmpty(txtCuilHijos3.Text) ? txtCuilHijos3.Text : String.Empty);
            if (fuCUILHijos3.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuCUILHijos3.PostedFiles)
                {
                    Stream fs = fuCUILHijos3.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "CUIL  " + txtNombreApellidoHijos3.Text + System.IO.Path.GetExtension(fuCUILHijos3.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }

            Registro["NombreApellidoHijo4"] = (txtNombreApellidoHijos4.Text != null && !string.IsNullOrEmpty(txtNombreApellidoHijos4.Text) ? txtNombreApellidoHijos4.Text : String.Empty);
            if (dtFechaNacimientoHijos4.IsDateEmpty == false)
            {
                Registro["FechaNacimientoHijo4"] = dtFechaNacimientoHijos4.SelectedDate.Date.ToShortDateString();
            }
            if (fuDNIHijos4.HasFiles)
            {
                int cont = 0;
                foreach (HttpPostedFile uploadedFile in fuDNIHijos4.PostedFiles)
                {
                    cont++;
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "DNI  " + txtNombreApellidoHijos4.Text + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (fuPartidaNacimientoHijos4.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuPartidaNacimientoHijos4.PostedFiles)
                {
                    Stream fs = fuPartidaNacimientoHijos4.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Partida Nacimiento  " + txtNombreApellidoHijos4.Text + System.IO.Path.GetExtension(fuPartidaNacimientoHijos4.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            Registro["NacionalidadHijo4"] = (txtNacionalidadHijos4.Text != null && !string.IsNullOrEmpty(txtNacionalidadHijos4.Text) ? txtNacionalidadHijos4.Text : String.Empty);
            Registro["CUILHijo4"] = (txtCuilHijos4.Text != null && !string.IsNullOrEmpty(txtCuilHijos4.Text) ? txtCuilHijos4.Text : String.Empty);
            if (fuCUILHijos4.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuCUILHijos4.PostedFiles)
                {
                    Stream fs = fuCUILHijos4.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "CUIL  " + txtNombreApellidoHijos4.Text + System.IO.Path.GetExtension(fuCUILHijos4.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }

            Registro["NombreApellidoHijo5"] = (txtNombreApellidoHijos5.Text != null && !string.IsNullOrEmpty(txtNombreApellidoHijos5.Text) ? txtNombreApellidoHijos5.Text : String.Empty);
            if (dtFechaNacimientoHijos5.IsDateEmpty == false)
            {
                Registro["FechaNacimientoHijo5"] = dtFechaNacimientoHijos5.SelectedDate.Date.ToShortDateString();
            }
            if (fuDNIHijos5.HasFiles)
            {
                int cont = 0;
                foreach (HttpPostedFile uploadedFile in fuDNIHijos5.PostedFiles)
                {
                    cont++;
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "DNI  " + txtNombreApellidoHijos5.Text + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (fuPartidaNacimientoHijos5.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuPartidaNacimientoHijos5.PostedFiles)
                {
                    Stream fs = fuPartidaNacimientoHijos5.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Partida Nacimiento  " + txtNombreApellidoHijos5.Text + System.IO.Path.GetExtension(fuPartidaNacimientoHijos5.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            Registro["NacionalidadHijo5"] = (txtNacionalidadHijos5.Text != null && !string.IsNullOrEmpty(txtNacionalidadHijos5.Text) ? txtNacionalidadHijos5.Text : String.Empty);
            Registro["CUILHijo5"] = (txtCuilHijos5.Text != null && !string.IsNullOrEmpty(txtCuilHijos5.Text) ? txtCuilHijos5.Text : String.Empty);
            if (fuCUILHijos5.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuCUILHijos5.PostedFiles)
                {
                    Stream fs = fuCUILHijos5.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "CUIL  " + txtNombreApellidoHijos5.Text + System.IO.Path.GetExtension(fuCUILHijos5.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }

            Registro["NombreApellidoHijo6"] = (txtNombreApellidoHijos6.Text != null && !string.IsNullOrEmpty(txtNombreApellidoHijos6.Text) ? txtNombreApellidoHijos6.Text : String.Empty);
            if (dtFechaNacimientoHijos6.IsDateEmpty == false)
            {
                Registro["FechaNacimientoHijo6"] = dtFechaNacimientoHijos6.SelectedDate.Date.ToShortDateString();
            }
            if (fuDNIHijos6.HasFiles)
            {
                int cont = 0;
                foreach (HttpPostedFile uploadedFile in fuDNIHijos6.PostedFiles)
                {
                    cont++;
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "DNI  " + txtNombreApellidoHijos6.Text + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (fuPartidaNacimientoHijos6.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuPartidaNacimientoHijos6.PostedFiles)
                {
                    Stream fs = fuPartidaNacimientoHijos6.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Partida Nacimiento  " + txtNombreApellidoHijos6.Text + System.IO.Path.GetExtension(fuPartidaNacimientoHijos6.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            Registro["NacionalidadHijo6"] = (txtNacionalidadHijos6.Text != null && !string.IsNullOrEmpty(txtNacionalidadHijos6.Text) ? txtNacionalidadHijos6.Text : String.Empty);
            Registro["CUILHijo6"] = (txtCuilHijos6.Text != null && !string.IsNullOrEmpty(txtCuilHijos6.Text) ? txtCuilHijos6.Text : String.Empty);
            if (fuCUILHijos6.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuCUILHijos6.PostedFiles)
                {
                    Stream fs = fuCUILHijos6.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "CUIL  " + txtNombreApellidoHijos6.Text + System.IO.Path.GetExtension(fuCUILHijos6.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            //Estudios Cursados
            Registro["EstudiosPrimarios"] = (cboEstuidosPrimarios.SelectedItem.Text != null && !string.IsNullOrEmpty(cboEstuidosPrimarios.SelectedItem.Text) ? cboEstuidosPrimarios.SelectedItem.Text : String.Empty);
            string estudiosPrimarios = string.Empty;
            if (cboEstuidosPrimarios.SelectedValue == "CURSANDO")
            {
                estudiosPrimarios = "Constancia Alumno Regular Primario";
            }
            else if (cboEstuidosPrimarios.SelectedValue == "COMPLETO")
            {
                estudiosPrimarios = "Constancia Estudios Primarios Completos";
            }
            if (fuConstanciaAlumnoPrimarios.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaAlumnoPrimarios.PostedFiles)
                {
                    Stream fs = fuConstanciaAlumnoPrimarios.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = estudiosPrimarios + System.IO.Path.GetExtension(fuConstanciaAlumnoPrimarios.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdePrimarios.IsDateEmpty == false)
            {
                Registro["FechaDesdePrimarios"] = dtDesdePrimarios.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaPrimarios.IsDateEmpty == false)
            {
                Registro["FechaHastaPrimarios"] = dtHastaPrimarios.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloPrimarios"] = (txtTituloPrimarios.Text != null && !string.IsNullOrEmpty(txtTituloPrimarios.Text) ? txtTituloPrimarios.Text : String.Empty);
            Registro["InstitucionPrimarios"] = (txtInstitucionPrimarios.Text != null && !string.IsNullOrEmpty(txtInstitucionPrimarios.Text) ? txtInstitucionPrimarios.Text : String.Empty);

            Registro["EstudiosSecundarios"] = (cboEstuidosSecundarios.SelectedItem.Text != null && !string.IsNullOrEmpty(cboEstuidosSecundarios.SelectedItem.Text) ? cboEstuidosSecundarios.SelectedItem.Text : String.Empty);
            string estudiosSecundarios = string.Empty;
            if (cboEstuidosSecundarios.SelectedValue == "CURSANDO")
            {
                estudiosSecundarios = "Constancia Alumno Regular Secundario";
            }
            else if (cboEstuidosSecundarios.SelectedValue == "COMPLETO")
            {
                estudiosSecundarios = "Constancia Estudios Secundarios Completos";
            }
            if (fuConstanciaAlumnoSecundarios.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaAlumnoSecundarios.PostedFiles)
                {
                    Stream fs = fuConstanciaAlumnoSecundarios.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = estudiosSecundarios + System.IO.Path.GetExtension(fuConstanciaAlumnoSecundarios.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeSecundarios.IsDateEmpty == false)
            {
                Registro["FechaDesdeSecundarios"] = dtDesdeSecundarios.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaSecundarios.IsDateEmpty == false)
            {
                Registro["FechaHastaSecundarios"] = dtHastaSecundarios.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloSecundarios"] = (txtTituloSecundarios.Text != null && !string.IsNullOrEmpty(txtTituloSecundarios.Text) ? txtTituloSecundarios.Text : String.Empty);
            Registro["InstitucionSecundarios"] = (txtInstitucionSecundarios.Text != null && !string.IsNullOrEmpty(txtInstitucionSecundarios.Text) ? txtInstitucionSecundarios.Text : String.Empty);

            Registro["EstudiosTerciarios"] = (cboEstuidosTerciarios.SelectedItem.Text != null && !string.IsNullOrEmpty(cboEstuidosTerciarios.SelectedItem.Text) ? cboEstuidosTerciarios.SelectedItem.Text : String.Empty);
            string estudiosTerciarios = string.Empty;
            if (cboEstuidosTerciarios.SelectedValue == "CURSANDO")
            {
                estudiosTerciarios = "Constancia Alumno Regular Terciario";
            }
            else if (cboEstuidosTerciarios.SelectedValue == "COMPLETO")
            {
                estudiosTerciarios = "Constancia Estudios Terciarios Completos";
            }
            if (fuConstanciaAlumnoTerciarios.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaAlumnoTerciarios.PostedFiles)
                {
                    Stream fs = fuConstanciaAlumnoTerciarios.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = estudiosTerciarios + System.IO.Path.GetExtension(fuConstanciaAlumnoTerciarios.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeTerciarios.IsDateEmpty == false)
            {
                Registro["FechaDesdeTerciarios"] = dtDesdeTerciarios.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaTerciarios.IsDateEmpty == false)
            {
                Registro["FechaHastaTerciarios"] = dtHastaTerciarios.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloTerciarios"] = (txtTituloTerciarios.Text != null && !string.IsNullOrEmpty(txtTituloTerciarios.Text) ? txtTituloTerciarios.Text : String.Empty);
            Registro["InstitucionTerciarios"] = (txtInstitucionTerciarios.Text != null && !string.IsNullOrEmpty(txtInstitucionTerciarios.Text) ? txtInstitucionTerciarios.Text : String.Empty);

            Registro["EstudiosUniversitarios"] = (cboEstuidosUniversitarios.SelectedItem.Text != null && !string.IsNullOrEmpty(cboEstuidosUniversitarios.SelectedItem.Text) ? cboEstuidosUniversitarios.SelectedItem.Text : String.Empty);
            string estudiosUniversitarios = string.Empty;
            if (cboEstuidosUniversitarios.SelectedValue == "CURSANDO")
            {
                estudiosUniversitarios = "Constancia Alumno Regular Universitario";
            }
            else if (cboEstuidosUniversitarios.SelectedValue == "COMPLETO")
            {
                estudiosUniversitarios = "Constancia Estudios Universitarios Completos";
            }
            if (fuConstanciaAlumnoUniversitarios.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaAlumnoUniversitarios.PostedFiles)
                {
                    Stream fs = fuConstanciaAlumnoUniversitarios.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = estudiosUniversitarios + System.IO.Path.GetExtension(fuConstanciaAlumnoUniversitarios.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeUniversitarios.IsDateEmpty == false)
            {
                Registro["FechaDesdeUniversitarios"] = dtDesdeUniversitarios.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaUniversitarios.IsDateEmpty == false)
            {
                Registro["FechaHastaUniversitarios"] = dtHastaUniversitarios.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloUniversitarios"] = (txtTituloUniversitarios.Text != null && !string.IsNullOrEmpty(txtTituloUniversitarios.Text) ? txtTituloUniversitarios.Text : String.Empty);
            Registro["InstitucionUniversitarios"] = (txtInstitucionUniversitarios.Text != null && !string.IsNullOrEmpty(txtInstitucionUniversitarios.Text) ? txtInstitucionUniversitarios.Text : String.Empty);

            Registro["Curso1"] = (cboCurso1.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso1.SelectedItem.Text) ? cboCurso1.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso1.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso1.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso1.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso "+ txtTituloCurso1.Text + System.IO.Path.GetExtension(fuConstanciaCurso1.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso1.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso1"] = dtDesdeCurso1.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso1.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso1"] = dtHastaCurso1.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso1"] = (txtTituloCurso1.Text != null && !string.IsNullOrEmpty(txtTituloCurso1.Text) ? txtTituloCurso1.Text : String.Empty);
            Registro["InstitucionCurso1"] = (txtInstitucionCurso1.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso1.Text) ? txtInstitucionCurso1.Text : String.Empty);

            Registro["Curso2"] = (cboCurso2.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso2.SelectedItem.Text) ? cboCurso2.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso2.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso2.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso2.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso2.Text + System.IO.Path.GetExtension(fuConstanciaCurso2.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso2.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso2"] = dtDesdeCurso2.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso2.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso2"] = dtHastaCurso2.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso2"] = (txtTituloCurso2.Text != null && !string.IsNullOrEmpty(txtTituloCurso2.Text) ? txtTituloCurso2.Text : String.Empty);
            Registro["InstitucionCurso2"] = (txtInstitucionCurso2.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso2.Text) ? txtInstitucionCurso2.Text : String.Empty);
            Registro["Curso3"] = (cboCurso3.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso3.SelectedItem.Text) ? cboCurso3.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso3.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso3.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso3.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso3.Text + System.IO.Path.GetExtension(fuConstanciaCurso3.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso3.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso3"] = dtDesdeCurso3.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso3.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso3"] = dtHastaCurso3.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso3"] = (txtTituloCurso3.Text != null && !string.IsNullOrEmpty(txtTituloCurso3.Text) ? txtTituloCurso3.Text : String.Empty);
            Registro["InstitucionCurso3"] = (txtInstitucionCurso3.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso3.Text) ? txtInstitucionCurso3.Text : String.Empty);
            Registro["Curso4"] = (cboCurso4.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso4.SelectedItem.Text) ? cboCurso4.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso4.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso4.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso4.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso4.Text + System.IO.Path.GetExtension(fuConstanciaCurso4.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso4.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso4"] = dtDesdeCurso4.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso4.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso4"] = dtHastaCurso4.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso4"] = (txtTituloCurso4.Text != null && !string.IsNullOrEmpty(txtTituloCurso4.Text) ? txtTituloCurso4.Text : String.Empty);
            Registro["InstitucionCurso4"] = (txtInstitucionCurso4.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso4.Text) ? txtInstitucionCurso4.Text : String.Empty);
            Registro["Curso5"] = (cboCurso5.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso5.SelectedItem.Text) ? cboCurso5.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso5.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso5.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso5.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso5.Text + System.IO.Path.GetExtension(fuConstanciaCurso5.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso5.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso5"] = dtDesdeCurso5.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso5.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso5"] = dtHastaCurso5.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso5"] = (txtTituloCurso5.Text != null && !string.IsNullOrEmpty(txtTituloCurso5.Text) ? txtTituloCurso5.Text : String.Empty);
            Registro["InstitucionCurso5"] = (txtInstitucionCurso5.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso5.Text) ? txtInstitucionCurso5.Text : String.Empty);
            Registro["Curso6"] = (cboCurso6.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso6.SelectedItem.Text) ? cboCurso6.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso6.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso6.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso6.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso6.Text + System.IO.Path.GetExtension(fuConstanciaCurso6.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso6.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso6"] = dtDesdeCurso6.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso6.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso6"] = dtHastaCurso6.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso6"] = (txtTituloCurso6.Text != null && !string.IsNullOrEmpty(txtTituloCurso6.Text) ? txtTituloCurso6.Text : String.Empty);
            Registro["InstitucionCurso6"] = (txtInstitucionCurso6.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso6.Text) ? txtInstitucionCurso6.Text : String.Empty);

            Registro["Curso7"] = (cboCurso7.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso7.SelectedItem.Text) ? cboCurso7.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso7.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso7.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso7.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso7.Text + System.IO.Path.GetExtension(fuConstanciaCurso7.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso7.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso7"] = dtDesdeCurso7.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso7.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso7"] = dtHastaCurso7.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso7"] = (txtTituloCurso7.Text != null && !string.IsNullOrEmpty(txtTituloCurso7.Text) ? txtTituloCurso7.Text : String.Empty);
            Registro["InstitucionCurso7"] = (txtInstitucionCurso7.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso7.Text) ? txtInstitucionCurso7.Text : String.Empty);

            Registro["Curso8"] = (cboCurso8.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso8.SelectedItem.Text) ? cboCurso8.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso8.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso8.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso8.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso8.Text + System.IO.Path.GetExtension(fuConstanciaCurso8.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso8.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso8"] = dtDesdeCurso8.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso8.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso8"] = dtHastaCurso8.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso8"] = (txtTituloCurso8.Text != null && !string.IsNullOrEmpty(txtTituloCurso8.Text) ? txtTituloCurso8.Text : String.Empty);
            Registro["InstitucionCurso8"] = (txtInstitucionCurso8.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso8.Text) ? txtInstitucionCurso8.Text : String.Empty);

            Registro["Curso9"] = (cboCurso9.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso9.SelectedItem.Text) ? cboCurso9.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso9.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso9.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso9.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso9.Text + System.IO.Path.GetExtension(fuConstanciaCurso9.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso9.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso9"] = dtDesdeCurso9.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso9.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso9"] = dtHastaCurso9.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso9"] = (txtTituloCurso9.Text != null && !string.IsNullOrEmpty(txtTituloCurso9.Text) ? txtTituloCurso9.Text : String.Empty);
            Registro["InstitucionCurso9"] = (txtInstitucionCurso9.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso9.Text) ? txtInstitucionCurso9.Text : String.Empty);

            Registro["Curso10"] = (cboCurso10.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso10.SelectedItem.Text) ? cboCurso10.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso10.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso10.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso10.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso10.Text + System.IO.Path.GetExtension(fuConstanciaCurso10.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso10.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso10"] = dtDesdeCurso10.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso10.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso10"] = dtHastaCurso10.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso10"] = (txtTituloCurso10.Text != null && !string.IsNullOrEmpty(txtTituloCurso10.Text) ? txtTituloCurso10.Text : String.Empty);
            Registro["InstitucionCurso10"] = (txtInstitucionCurso10.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso10.Text) ? txtInstitucionCurso10.Text : String.Empty);

            Registro["Curso11"] = (cboCurso11.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso11.SelectedItem.Text) ? cboCurso11.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso11.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso11.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso11.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso11.Text + System.IO.Path.GetExtension(fuConstanciaCurso11.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso11.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso11"] = dtDesdeCurso11.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso11.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso11"] = dtHastaCurso11.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso11"] = (txtTituloCurso11.Text != null && !string.IsNullOrEmpty(txtTituloCurso11.Text) ? txtTituloCurso11.Text : String.Empty);
            Registro["InstitucionCurso11"] = (txtInstitucionCurso11.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso11.Text) ? txtInstitucionCurso11.Text : String.Empty);

            Registro["Curso12"] = (cboCurso12.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso12.SelectedItem.Text) ? cboCurso12.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso12.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso12.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso12.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso12.Text + System.IO.Path.GetExtension(fuConstanciaCurso12.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso12.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso12"] = dtDesdeCurso12.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso12.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso12"] = dtHastaCurso12.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso12"] = (txtTituloCurso12.Text != null && !string.IsNullOrEmpty(txtTituloCurso12.Text) ? txtTituloCurso12.Text : String.Empty);
            Registro["InstitucionCurso12"] = (txtInstitucionCurso12.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso12.Text) ? txtInstitucionCurso12.Text : String.Empty);

            Registro["Curso13"] = (cboCurso13.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso13.SelectedItem.Text) ? cboCurso13.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso13.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso13.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso13.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso13.Text + System.IO.Path.GetExtension(fuConstanciaCurso13.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso13.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso13"] = dtDesdeCurso13.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso13.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso13"] = dtHastaCurso13.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso13"] = (txtTituloCurso13.Text != null && !string.IsNullOrEmpty(txtTituloCurso13.Text) ? txtTituloCurso13.Text : String.Empty);
            Registro["InstitucionCurso13"] = (txtInstitucionCurso13.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso13.Text) ? txtInstitucionCurso13.Text : String.Empty);

            Registro["Curso14"] = (cboCurso14.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso14.SelectedItem.Text) ? cboCurso14.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso14.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso14.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso14.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso14.Text + System.IO.Path.GetExtension(fuConstanciaCurso14.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso14.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso14"] = dtDesdeCurso14.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso14.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso14"] = dtHastaCurso14.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso14"] = (txtTituloCurso14.Text != null && !string.IsNullOrEmpty(txtTituloCurso14.Text) ? txtTituloCurso14.Text : String.Empty);
            Registro["InstitucionCurso14"] = (txtInstitucionCurso14.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso14.Text) ? txtInstitucionCurso14.Text : String.Empty);

            Registro["Curso15"] = (cboCurso15.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso15.SelectedItem.Text) ? cboCurso15.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso15.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso15.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso15.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso15.Text + System.IO.Path.GetExtension(fuConstanciaCurso15.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso15.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso15"] = dtDesdeCurso15.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso15.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso15"] = dtHastaCurso15.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso15"] = (txtTituloCurso15.Text != null && !string.IsNullOrEmpty(txtTituloCurso15.Text) ? txtTituloCurso15.Text : String.Empty);
            Registro["InstitucionCurso15"] = (txtInstitucionCurso15.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso15.Text) ? txtInstitucionCurso15.Text : String.Empty);

            Registro["Curso16"] = (cboCurso16.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso16.SelectedItem.Text) ? cboCurso16.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso16.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso16.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso16.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso16.Text + System.IO.Path.GetExtension(fuConstanciaCurso16.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso16.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso16"] = dtDesdeCurso16.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso16.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso16"] = dtHastaCurso16.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso16"] = (txtTituloCurso16.Text != null && !string.IsNullOrEmpty(txtTituloCurso16.Text) ? txtTituloCurso16.Text : String.Empty);
            Registro["InstitucionCurso16"] = (txtInstitucionCurso16.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso16.Text) ? txtInstitucionCurso16.Text : String.Empty);

            Registro["Curso17"] = (cboCurso17.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso17.SelectedItem.Text) ? cboCurso17.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso17.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso17.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso17.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso17.Text + System.IO.Path.GetExtension(fuConstanciaCurso17.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso17.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso17"] = dtDesdeCurso17.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso17.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso17"] = dtHastaCurso17.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso17"] = (txtTituloCurso17.Text != null && !string.IsNullOrEmpty(txtTituloCurso17.Text) ? txtTituloCurso17.Text : String.Empty);
            Registro["InstitucionCurso17"] = (txtInstitucionCurso17.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso17.Text) ? txtInstitucionCurso17.Text : String.Empty);

            Registro["Curso18"] = (cboCurso18.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso18.SelectedItem.Text) ? cboCurso18.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso18.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso18.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso18.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso18.Text + System.IO.Path.GetExtension(fuConstanciaCurso18.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso18.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso18"] = dtDesdeCurso18.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso18.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso18"] = dtHastaCurso18.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso18"] = (txtTituloCurso18.Text != null && !string.IsNullOrEmpty(txtTituloCurso18.Text) ? txtTituloCurso18.Text : String.Empty);
            Registro["InstitucionCurso18"] = (txtInstitucionCurso18.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso18.Text) ? txtInstitucionCurso18.Text : String.Empty);

            Registro["Curso19"] = (cboCurso19.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso19.SelectedItem.Text) ? cboCurso19.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso19.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso19.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso19.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso19.Text + System.IO.Path.GetExtension(fuConstanciaCurso19.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso19.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso19"] = dtDesdeCurso19.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso19.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso19"] = dtHastaCurso19.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso19"] = (txtTituloCurso19.Text != null && !string.IsNullOrEmpty(txtTituloCurso19.Text) ? txtTituloCurso19.Text : String.Empty);
            Registro["InstitucionCurso19"] = (txtInstitucionCurso19.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso19.Text) ? txtInstitucionCurso19.Text : String.Empty);

            Registro["Curso20"] = (cboCurso20.SelectedItem.Text != null && !string.IsNullOrEmpty(cboCurso20.SelectedItem.Text) ? cboCurso20.SelectedItem.Text : String.Empty);
            if (fuConstanciaCurso20.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConstanciaCurso20.PostedFiles)
                {
                    Stream fs = fuConstanciaCurso20.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia Curso " + txtTituloCurso20.Text + System.IO.Path.GetExtension(fuConstanciaCurso20.FileName);
                    attachments.Add(fileName + "-" + Guid.NewGuid().ToString(), fileContents);
                }
            }
            if (dtDesdeCurso20.IsDateEmpty == false)
            {
                Registro["FechaDesdeCurso20"] = dtDesdeCurso20.SelectedDate.Date.ToShortDateString();
            }
            if (dtHastaCurso20.IsDateEmpty == false)
            {
                Registro["FechaHastaCurso20"] = dtHastaCurso20.SelectedDate.Date.ToShortDateString();
            }
            Registro["TituloCurso20"] = (txtTituloCurso20.Text != null && !string.IsNullOrEmpty(txtTituloCurso20.Text) ? txtTituloCurso20.Text : String.Empty);
            Registro["InstitucionCurso20"] = (txtInstitucionCurso20.Text != null && !string.IsNullOrEmpty(txtInstitucionCurso20.Text) ? txtInstitucionCurso20.Text : String.Empty);

        }
        protected void btnCerrar(object sender, EventArgs e)
        {
            if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("RRHH")))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "cerrarRRHH();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "cerrarHome();", true);
            }
        }

        protected void btnEliminar_ServerClick(object sender, EventArgs e)
        {
            lbMensaje.Text = "";
            SPListItem Proceso = SPContext.Current.Web.Lists["DatosPersonal"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
            bool bEntro = false;
            foreach (ListItem x in chkBoxList.Items)
            {
                if (x.Selected == true)
                {
                    Proceso.Attachments.Delete(x.Value);
                    Proceso.Update();
                    lbMensaje.Text = "";
                    bEntro = true;
                }
            }
            if (bEntro == true)
            {
                CargarDatos(Proceso);
            }
            else
            {
                lbMensaje.Text = "Debe seleccionar al menos un elemento";
            }
        }
    }
}
