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
        //private string sSitio = "http://ibiza:2222/sites/formularios";
        private string sSitio = "https://portalrrhh.ieasa.com.ar";
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
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "grupoBtnGuardar();", true);
                btnGroupDatosPersonales.Visible = false;
                btnGroupID.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "OcultaDivs();", true);
            }
        }
        private void CargaPestania()
        {
            LtPestañaConID.Text = "<li class='nav-item bg-light'>" +
                                      "<a class='nav-link active' data-toggle='pill' id='pestaniaDatosPersonales' href='#datosPersonales' ClientIDMode='Static'>Datos Personales</a>" +
                                    "</li>" +
                                    "<li class='nav-item bg-light'>" +
                                      "<a class='nav-link' data-toggle='pill' id='pestaniaEstructura' href='#estructura'>Estructura</a>" +
                                    "</li>" +
                                    "<li class='nav-item bg-light' >" +
                                      "<a class='nav-link' data-toggle='pill' id='pestaniaGrupoFamiliar' href='#grupoFamiliar' ClientIDMode='Static'>Grupo Familiar</a>" +
                                    "</li>" +
                                    "<li class='nav-item bg-light'>" +
                                      "<a class='nav-link' data-toggle='pill' id='pestaniaEstudiosCursados' href='#estudiosCursados'>Estudios Cursados</a>" +
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
            txtNombreApellido.Text = (DatosPersonal["NombreApellido"] != null && !string.IsNullOrEmpty(DatosPersonal["NombreApellido"].ToString()) ? DatosPersonal["NombreApellido"].ToString() : String.Empty);
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
                switch(DatosPersonal["EstadoCivil"].ToString()){
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
                if (entro == false){
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
        }
        private void CargarAdjuntos(SPListItem AltaProveedor)
        {
            SPAttachmentCollection collAttachments = AltaProveedor.Attachments;
            if (collAttachments.Count > 0)
            {
                foreach (string fileName in collAttachments)
                {
                    ltAdjuntos.Text += "<tr class='table-dark text-dark'><td><a href='" + collAttachments.UrlPrefix + fileName + "' target='_blank' title='Click para ver el documento' class='adjuntoItem'> " + fileName + "</a></td></tr>";

                }

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
            else {
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

                    string fileName = "DNI - " + txtNombreApellido.Text+ " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "CUIL - "+ txtNombreApellido.Text + System.IO.Path.GetExtension(fuCuil.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "DNI - " + txtNombreConyuge.Text + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "CUIL - " + txtNombreConyuge.Text + System.IO.Path.GetExtension(fuCUILconyuge.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "DNI - " + txtNombreApellidoHijos1.Text + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "Partida Nacimiento - " + txtNombreApellidoHijos1.Text + System.IO.Path.GetExtension(fuPartidaNacimientoHijos1.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "CUIL - " + txtNombreApellidoHijos1.Text + System.IO.Path.GetExtension(fuCUILHijos1.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "DNI - " + txtNombreApellidoHijos2.Text + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "Partida Nacimiento - " + txtNombreApellidoHijos2.Text + System.IO.Path.GetExtension(fuPartidaNacimientoHijos2.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "CUIL - " + txtNombreApellidoHijos2.Text + System.IO.Path.GetExtension(fuCUILHijos2.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "DNI - " + txtNombreApellidoHijos3.Text + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
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
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "CUIL - " + txtNombreApellidoHijos3.Text + System.IO.Path.GetExtension(fuCUILHijos3.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "DNI - " + txtNombreApellidoHijos4.Text + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "Partida Nacimiento - " + txtNombreApellidoHijos4.Text + System.IO.Path.GetExtension(fuPartidaNacimientoHijos4.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "CUIL - " + txtNombreApellidoHijos4.Text + System.IO.Path.GetExtension(fuCUILHijos4.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "DNI - " + txtNombreApellidoHijos5.Text + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "Partida Nacimiento - " + txtNombreApellidoHijos5.Text + System.IO.Path.GetExtension(fuPartidaNacimientoHijos5.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "CUIL - " + txtNombreApellidoHijos5.Text + System.IO.Path.GetExtension(fuCUILHijos5.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "DNI - " + txtNombreApellidoHijos6.Text + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "Partida Nacimiento - " + txtNombreApellidoHijos6.Text + System.IO.Path.GetExtension(fuPartidaNacimientoHijos6.FileName);
                    attachments.Add(fileName, fileContents);
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

                    string fileName = "CUIL - " + txtNombreApellidoHijos6.Text + System.IO.Path.GetExtension(fuCUILHijos6.FileName);
                    attachments.Add(fileName, fileContents);
                }
            }
            //Estudios Cursados
            Registro["EstudiosPrimarios"] = (cboEstuidosPrimarios.SelectedItem.Text != null && !string.IsNullOrEmpty(cboEstuidosPrimarios.SelectedItem.Text) ? cboEstuidosPrimarios.SelectedItem.Text : String.Empty);
            string estudiosPrimarios = string.Empty;
            if(cboEstuidosPrimarios.SelectedValue == "CURSANDO"){
                estudiosPrimarios = "Constancia Alumno Regular";
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
                    attachments.Add(fileName, fileContents);
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
                estudiosSecundarios = "Constancia Alumno Regular";
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
                    attachments.Add(fileName, fileContents);
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
                estudiosTerciarios = "Constancia Alumno Regular";
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
                    attachments.Add(fileName, fileContents);
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
                estudiosUniversitarios = "Constancia Alumno Regular";
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
                    attachments.Add(fileName, fileContents);
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
    }
}
