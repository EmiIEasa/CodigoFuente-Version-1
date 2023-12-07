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
using IEasa.Sharepoint.FormsBasedAuthentication;
using System.Web.Security;
using System.Text;
using Microsoft.SharePoint.IdentityModel;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace Ieasa.Layouts.Ieasa
{
    public partial class AltaProvisoria : UnsecuredLayoutsPageBase
    {
        private string sSitioAnonimo = "https://proveedores-an.energia-argentina.com.ar/";
     private string sSitio = "https://proveedores-desa.energia-argentina.com.ar";
        //private string sSitio = "https://portalproveedores.energia-argentina.com.ar";
       // private string sSitio = " https://espd-01.energia-argentina.com.ar";
       
        string strSeleccione = "Seleccione";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CargarCombos();
                CargarInicial();

            }
        }
        protected override bool AllowAnonymousAccess
        {
            get { return true; }
        }
        private void CargarInicial()
        {
            if (Request.QueryString["ID"] != null)
            {
                SPListItem AltaProv = SPContext.Current.Web.Lists["AltaProvisoria"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                CargarDatos(AltaProv);
                CargaPestania();
                CargasAdjuntosSap(Request.QueryString["ID"].ToString());
                menuConID.Visible = true;
                menuSinID.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "divBlock();", true);
                string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='NombreUsuario' /><Value Type='Text'>" + AltaProv["NombreFantasia"].ToString() + "</Value></Eq></Where></Query></View>";
                SPQuery query = new SPQuery();
                query.ViewXml = QuerySTR;
                SPListItemCollection ListaAux = SPContext.Current.Web.Lists["AltaProvisoriaUsuarioBloqueado"].GetItems(query);
                if (ListaAux.Count > 0) {
                    if (ListaAux[0]["UsuarioBloqueado"] != null && ListaAux[0]["UsuarioBloqueado"].ToString() == "SI")
                    {
                        BloqueaCampos(true);
                    }
                    else
                    {
                        BloqueaCampos(false);
                    }
                }
            }
        }

        private void CargasAdjuntosSap(string Id)
        {
            string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='IdRegistro' /><Value Type='Text'>" + Id + "</Value></Eq></Where></Query></View>";
            SPQuery query = new SPQuery();
            query.ViewXml = QuerySTR;
            string sHtml = string.Empty;
            SPListItemCollection ListaAux = SPContext.Current.Web.Lists["AltaProvisoriaAdjuntosSAP"].GetItems(query);
            if (ListaAux.Count > 0)
            {

                foreach (SPListItem item in ListaAux)
                {
                    sHtml += "<tr>";
                    sHtml += "<td>" + item["Title"] + "</td>" + "<td>" + item["Estado"] + "</td>" + "<td>" + item["Created"] + "</td>" + "<td>" + item["Eliminado"] + "</td>";
                    sHtml += "</tr>";

                }

                ltAdjuntos.Text = sHtml;
            }
        }

        private void CargaPestania()
        {
            bool bPerteneceIeasa = false;
            if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("COMPRAS")))
            {
                bPerteneceIeasa = true;
            }
            if (bPerteneceIeasa == true)
            {
                LtPestañaCompras.Text = "<li class='nav-item bg-light'>" +
                                            "<a class='nav-link active' data-toggle='pill' id='pestaniaDatosGrales' href='#home'>Datos Generales</a>" +
                                        "</li>" +
                                        "<li class='nav-item bg-light'>" +
                                            "<a class='nav-link' data-toggle='pill' id='pestaniaRubros' href='#rubros'>Rubros</a>" +
                                        "</li>" +
                                        "<li class='nav-item bg-light' >" +
                                            "<a class='nav-link' data-toggle='pill' id='pestaniaDatosContacto' href='#menu1'>Datos Contacto</a>" +
                                        "</li>" +
                                        "<li class='nav-item bg-light'>" +
                                            "<a class='nav-link' data-toggle='pill' id='pestaniaDatosImpositivos' href='#menu2'>Datos Impositivos</a>" +
                                        "</li>" +

                                        "<li class='nav-item bg-light'>" +
                                            "<a class='nav-link' data-toggle='pill' href='#menu3'>Adjuntos</a>" +
                                        "</li>" +
                                        "<li class='nav-item bg-light'>" +
                                            "<a class='nav-link' data-toggle='pill' href='#sap'>SAP</a>" +
                                        "</li>" +
                                        "<li class='nav-item bg-light'>" +
                                            "<a class='nav-link' data-toggle='pill' href='#compras'>Compras</a>" +
                                        "</li>";
            }
            else
            {
                LtPestañaCompras.Text = "<li class='nav-item bg-light'>" +
                                        "<a class='nav-link active' data-toggle='pill' id='pestaniaDatosGrales' href='#home'>Datos Generales</a>" +
                                    "</li>" +
                                    "<li class='nav-item bg-light'>" +
                                        "<a class='nav-link' data-toggle='pill' id='pestaniaRubros' href='#rubros'>Rubros</a>" +
                                    "</li>" +
                                    "<li class='nav-item bg-light' >" +
                                        "<a class='nav-link' data-toggle='pill' id='pestaniaDatosContacto' href='#menu1'>Datos Contacto</a>" +
                                    "</li>" +
                                    "<li class='nav-item bg-light'>" +
                                        "<a class='nav-link' data-toggle='pill' id='pestaniaDatosImpositivos' href='#menu2'>Datos Impositivos</a>" +
                                    "</li>" +

                                    "<li class='nav-item bg-light'>" +
                                        "<a class='nav-link' data-toggle='pill' href='#menu3'>Adjuntos</a>" +
                                    "</li>";

            }


        }
        private void CargarDatos(SPListItem AltaProv)
        {
            if (AltaProv["NombreFantasia"] != null)
            {
                string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='NombreUsuario' /><Value Type='Text'>" + AltaProv["NombreFantasia"].ToString() + "</Value></Eq></Where></Query></View>";
                SPQuery query = new SPQuery();
                query.ViewXml = QuerySTR;
                SPListItemCollection ListaAux = SPContext.Current.Web.Lists["AltaProvisoriaUsuarioBloqueado"].GetItems(query);
                if (ListaAux.Count > 0) {
                    cboUsuarioBloqueado.SelectedValue = ListaAux[0]["UsuarioBloqueado"].ToString();
                }
            }
            if (AltaProv["PasarSAP"] != null && !string.IsNullOrEmpty(AltaProv["PasarSAP"].ToString()) && AltaProv["PasarSAP"].ToString() == "SI")

            {
                chkSAP.Checked = true;
            }
            else
            {
                chkSAP.Checked = false;
            }
            txtEstado.Text = (AltaProv["Estado"] != null && !string.IsNullOrEmpty(AltaProv["Estado"].ToString()) ? AltaProv["Estado"].ToString() : String.Empty);
            txtNomFantasia.Text = (AltaProv["RazonSocial"] != null && !string.IsNullOrEmpty(AltaProv["RazonSocial"].ToString()) ? AltaProv["RazonSocial"].ToString() : String.Empty);
            txtRazonSocial.Text = (AltaProv["NombreFantasia"] != null && !string.IsNullOrEmpty(AltaProv["NombreFantasia"].ToString()) ? AltaProv["NombreFantasia"].ToString() : String.Empty);
            if (AltaProv["Personeria"] != null && !string.IsNullOrEmpty(AltaProv["Personeria"].ToString()))
            {
                cboPersoneria.SelectedValue = (AltaProv["Personeria"] != null && !string.IsNullOrEmpty(AltaProv["Personeria"].ToString()) ? AltaProv["Personeria"].ToString() : String.Empty);
            }
            txtActPrinc.Text = (AltaProv["ActividadPrincipal"] != null && !string.IsNullOrEmpty(AltaProv["ActividadPrincipal"].ToString()) ? AltaProv["ActividadPrincipal"].ToString() : String.Empty);
            txtSitioWeb.Text = (AltaProv["SitioWeb"] != null && !string.IsNullOrEmpty(AltaProv["SitioWeb"].ToString()) ? AltaProv["SitioWeb"].ToString() : String.Empty);
            if (AltaProv["Rama01"] != null && !string.IsNullOrEmpty(AltaProv["Rama01"].ToString()))
            {
                cboRubro.SelectedValue = (AltaProv["Rama01"] != null && !string.IsNullOrEmpty(AltaProv["Rama01"].ToString()) ? AltaProv["Rama01"].ToString() : String.Empty);
            }
            if (AltaProv["Pais"] != null && !string.IsNullOrEmpty(AltaProv["Pais"].ToString()))
            {
                cboPais.SelectedValue = (AltaProv["Pais"] != null && !string.IsNullOrEmpty(AltaProv["Pais"].ToString()) ? AltaProv["Pais"].ToString() : String.Empty);
            }
            CargarProvincias();
            cboProvincia.SelectedValue = (AltaProv["Provincia"] != null && !string.IsNullOrEmpty(AltaProv["Provincia"].ToString()) ? AltaProv["Provincia"].ToString() : String.Empty);


            txtCiudad.Text = (AltaProv["Ciudad"] != null && !string.IsNullOrEmpty(AltaProv["Ciudad"].ToString()) ? AltaProv["Ciudad"].ToString() : String.Empty);
            txtCodPostal.Text = (AltaProv["CodPostal"] != null && !string.IsNullOrEmpty(AltaProv["CodPostal"].ToString()) ? AltaProv["CodPostal"].ToString() : String.Empty);
            txtCalle.Text = (AltaProv["Calle"] != null && !string.IsNullOrEmpty(AltaProv["Calle"].ToString()) ? AltaProv["Calle"].ToString() : String.Empty);
            txtAltura.Text = (AltaProv["Altura"] != null && !string.IsNullOrEmpty(AltaProv["Altura"].ToString()) ? AltaProv["Altura"].ToString() : String.Empty);
            txtDepto.Text = (AltaProv["Depto"] != null && !string.IsNullOrEmpty(AltaProv["Depto"].ToString()) ? AltaProv["Depto"].ToString() : String.Empty);
            txtPiso.Text = (AltaProv["Piso"] != null && !string.IsNullOrEmpty(AltaProv["Piso"].ToString()) ? AltaProv["Piso"].ToString() : String.Empty);

            txtApoderado.Text = (AltaProv["ApoderadoResp"] != null && !string.IsNullOrEmpty(AltaProv["ApoderadoResp"].ToString()) ? AltaProv["ApoderadoResp"].ToString() : String.Empty);

            txtVentasContacto.Text = (AltaProv["VentasContacto"] != null && !string.IsNullOrEmpty(AltaProv["VentasContacto"].ToString()) ? AltaProv["VentasContacto"].ToString() : String.Empty);
            txtVentasCorreo.Text = (AltaProv["VentasCorreo"] != null && !string.IsNullOrEmpty(AltaProv["VentasCorreo"].ToString()) ? AltaProv["VentasCorreo"].ToString() : String.Empty);
            txtAdministracionContacto.Text = (AltaProv["AdministracionContacto"] != null && !string.IsNullOrEmpty(AltaProv["AdministracionContacto"].ToString()) ? AltaProv["AdministracionContacto"].ToString() : String.Empty);
            txtAdministracionCorreo.Text = (AltaProv["AdministracionCorreo"] != null && !string.IsNullOrEmpty(AltaProv["AdministracionCorreo"].ToString()) ? AltaProv["AdministracionCorreo"].ToString() : String.Empty);
            txtTelefono.Text = (AltaProv["Telefono"] != null && !string.IsNullOrEmpty(AltaProv["Telefono"].ToString()) ? AltaProv["Telefono"].ToString() : String.Empty);
            txtFax.Text = (AltaProv["Fax"] != null && !string.IsNullOrEmpty(AltaProv["Fax"].ToString()) ? AltaProv["Fax"].ToString() : String.Empty);
            txtCuit.Text = (AltaProv["Cuit"] != null && !string.IsNullOrEmpty(AltaProv["Cuit"].ToString()) ? AltaProv["Cuit"].ToString() : String.Empty);
            txtCIE.Text = (AltaProv["CodigoExtranjero"] != null && !string.IsNullOrEmpty(AltaProv["CodigoExtranjero"].ToString()) ? AltaProv["CodigoExtranjero"].ToString() : String.Empty);
            if (AltaProv["PersonaFisica"] != null && !string.IsNullOrEmpty(AltaProv["PersonaFisica"].ToString()))
            {
                if (AltaProv["PersonaFisica"].ToString() == "SI")
                {
                    chkPersFis.Checked = true;
                }
                else
                {
                    chkPersFis.Checked = false;
                }
            }

            ValidaAgregaOpcionCombo(AltaProv["Proveedor"] != null && !string.IsNullOrEmpty(AltaProv["Proveedor"].ToString()) ? AltaProv["Proveedor"].ToString() : String.Empty, "cboProveedor");
            cboProveedor.SelectedValue = (AltaProv["Proveedor"] != null && !string.IsNullOrEmpty(AltaProv["Proveedor"].ToString()) ? AltaProv["Proveedor"].ToString() : String.Empty);
            ValidaAgregaOpcionCombo(AltaProv["CondImpIG"] != null && !string.IsNullOrEmpty(AltaProv["CondImpIG"].ToString()) ? AltaProv["CondImpIG"].ToString() : String.Empty, "cboConImpG");
            cboConImpG.SelectedValue = (AltaProv["CondImpIG"] != null && !string.IsNullOrEmpty(AltaProv["CondImpIG"].ToString()) ? AltaProv["CondImpIG"].ToString() : String.Empty);
            ValidaAgregaOpcionCombo(AltaProv["CondImpIVA"] != null && !string.IsNullOrEmpty(AltaProv["CondImpIVA"].ToString()) ? AltaProv["CondImpIVA"].ToString() : String.Empty, "cboCondImIVA");
            cboCondImIVA.SelectedValue = (AltaProv["CondImpIVA"] != null && !string.IsNullOrEmpty(AltaProv["CondImpIVA"].ToString()) ? AltaProv["CondImpIVA"].ToString() : String.Empty);

            ValidaAgregaOpcionCombo(AltaProv["IngresosBrutos"] != null && !string.IsNullOrEmpty(AltaProv["IngresosBrutos"].ToString()) ? AltaProv["IngresosBrutos"].ToString() : String.Empty, "cboIngresosBrutos");
            cboIngresosBrutos.SelectedValue = (AltaProv["IngresosBrutos"] != null && !string.IsNullOrEmpty(AltaProv["IngresosBrutos"].ToString()) ? AltaProv["IngresosBrutos"].ToString() : String.Empty);
            txtIngresosBrutos.Text = (AltaProv["NumeroIngresosBrutos"] != null && !string.IsNullOrEmpty(AltaProv["NumeroIngresosBrutos"].ToString()) ? AltaProv["NumeroIngresosBrutos"].ToString() : String.Empty);
            txtObsCompras.Text = (AltaProv["ObservacionesCompras"] != null && !string.IsNullOrEmpty(AltaProv["ObservacionesCompras"].ToString()) ? AltaProv["ObservacionesCompras"].ToString() : String.Empty);
            CargarAdjuntos(AltaProv);

            //if (AltaProv["SAP"] != null && !string.IsNullOrEmpty(AltaProv["SAP"].ToString()))
            //{
            //    if (AltaProv["SAP"].ToString() == "SI")
            //    {
            //        chkSAP.Checked = true;
            //    }
            //    else
            //    {
            //        chkSAP.Checked = false;
            //    }
            //}
            txtEstadoSAP.Text = (AltaProv["EstadoSAP"] != null && !string.IsNullOrEmpty(AltaProv["EstadoSAP"].ToString()) ? AltaProv["EstadoSAP"].ToString() : String.Empty);
            lbIdSap.Text = (AltaProv["IdSap"] != null && !string.IsNullOrEmpty(AltaProv["IdSap"].ToString()) ? AltaProv["IdSap"].ToString() : String.Empty);
            lbSap.Text = (AltaProv["SAP"] != null && !string.IsNullOrEmpty(AltaProv["SAP"].ToString()) ? AltaProv["SAP"].ToString() : String.Empty);


            if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("COMPRAS")))
            {
                btnEliminar.Visible = true;
            }
            else
            {
                btnEliminar.Visible = false;
            }


            //if (AltaProv["Estado"].ToString() == "Aprobado" || AltaProv["Estado"].ToString() == "Rechazado" || AltaProv["Estado"].ToString() == "Subsanado" || AltaProv["Estado"].ToString() == "Suspendido/Bloqueado")
            //{

            //    txtObsCompras.ReadOnly = true;
            //    btnAprobarCompras.Disabled = true;
            //    btnRechazarCompras.Disabled = true;
            //    btnSubsanarCompras.Disabled = true;
            //    btnSuspenderCompras.Disabled = true;
            //}
            CargarRubros(AltaProv.ID);
            CargarSociedad(AltaProv.ID);
        }
        public static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
                return root;
            foreach (Control ctrl in root.Controls)
            {
                Control FoundCtl = FindControlRecursive(ctrl, id);
                if (FoundCtl != null)
                    return FoundCtl;
            }
            return null;
        }
        private void ValidaAgregaOpcionCombo(string sopcion, string sNombreCombo)
        {
            DropDownList Combo = (DropDownList)FindControlRecursive(this, sNombreCombo);

            if (sopcion != string.Empty && Combo.Items.FindByText(sopcion) == null)
            {
                DataTable dtDatos = (DataTable)Combo.DataSource;
                DataRow rFilaSeleccioneSolicitud = dtDatos.NewRow();
                rFilaSeleccioneSolicitud["Valor"] = sopcion;
                dtDatos.Rows.InsertAt(rFilaSeleccioneSolicitud, 1000);
                Combo.DataSource = dtDatos;
                Combo.DataValueField = "Valor";
                Combo.DataTextField = "Valor";
                Combo.DataBind();

            }

        }
        private void CargarRubros(int iIdFormulario)
        {
            for (int i = 0; i < libServicio.Items.Count; i++)
            {
                libServicio.Items.RemoveAt(i);
            }


            string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='IDRegistro' /><Value Type='Text'>" + iIdFormulario.ToString() + "</Value></Eq></Where></Query></View>";
            SPQuery query = new SPQuery();
            query.ViewXml = QuerySTR;
            SPListItemCollection ListaAux = SPContext.Current.Web.Lists["AltaProvisoriaRubros"].GetItems(query);
            bool bEncontro = false;
            if (ListaAux.Count > 0)
            {
                foreach (SPListItem item in ListaAux)
                {
                    bEncontro = false;
                    for (int i = 0; i < libServicio.Items.Count; i++)
                    {

                        if (libServicio.Items[i].Text.ToString() == item["Title"].ToString())
                        {
                            bEncontro = true;
                            break;

                        }
                    }
                    if (bEncontro == false)
                    {
                        libServicio.Items.Add(item["Title"].ToString());
                    }
                }
            }
        }
        private void CargarSociedad(int iIdFormulario)
        {
            ////    string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='IDRegistro' /><Value Type='Text'>" + iIdFormulario.ToString() + "</Value></Eq></Where></Query></View>";
            ////    SPQuery query = new SPQuery();
            ////    query.ViewXml = QuerySTR;
            ////    SPListItemCollection ListaAux = SPContext.Current.Web.Lists["AltaProvisoriaSociedad"].GetItems(query);
            ////    if (ListaAux.Count > 0)
            ////    {
            ////        foreach (SPListItem item in ListaAux)
            ////        {
            ////            listSociedad.Items.Add(item["Title"].ToString());
            ////        }
            ////    }
        }
        private void BloqueaCampos(bool respuesta)
        {
            bool valorBool = respuesta;
            string sDisplay;
            string sCol;
            if (respuesta == true)
            {
                sDisplay = "none";
                sCol = "col-12";
                chkPersFis.Attributes.Add("disabled", "disabled");

                btnAprobarCompras.Attributes.Add("disabled", "disabled");
                btnSubsanarCompras.Attributes.Add("disabled", "disabled");
                btnPreInscribiCompras.Attributes.Add("disabled", "disabled");
                btnSuspenderCompras.Attributes.Add("disabled", "disabled");
            }
            else
            {
                sDisplay = "block";
                sCol = "col-10";
                if (chkPersFis.Attributes["disabled"] != null)
                {
                    chkPersFis.Attributes.Remove("disabled");
                }

                if (btnAprobarCompras.Attributes["disabled"] != null)
                {
                    btnAprobarCompras.Attributes.Remove("disabled");
                }
                if (btnSubsanarCompras.Attributes["disabled"] != null)
                {
                    btnSubsanarCompras.Attributes.Remove("disabled");
                }
                if (btnPreInscribiCompras.Attributes["disabled"] != null)
                {
                    btnPreInscribiCompras.Attributes.Remove("disabled");
                }
                if (btnSuspenderCompras.Attributes["disabled"] != null)
                {
                    btnSuspenderCompras.Attributes.Remove("disabled");
                }
            }

            if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("COMPRAS")))
            {
                cboUsuarioBloqueado.Enabled = true;
            }
            else
            {
                cboUsuarioBloqueado.Enabled = false;
            }
            btnGuardarID.Visible = !valorBool;
            //Bloqueo Pestaña -Datos generales
            txtRazonSocial.ReadOnly = valorBool;
            txtNomFantasia.ReadOnly = valorBool;
            cboPersoneria.Enabled = !valorBool;
            txtActPrinc.ReadOnly = valorBool;
            txtSitioWeb.ReadOnly = valorBool;
            comboRubros.Style.Add("display", sDisplay);
            btnAQ.Style.Add("display", sDisplay);
            divListBox.Attributes.Add("class", sCol);
            cboPais.Enabled = !valorBool;
            // txtProvincia.ReadOnly = valorBool;
            cboProvincia.Enabled = !valorBool;
            txtCiudad.ReadOnly = valorBool;
            txtCodPostal.ReadOnly = valorBool;
            txtCalle.ReadOnly = valorBool;
            txtAltura.ReadOnly = valorBool;
            txtDepto.ReadOnly = valorBool;
            txtPiso.ReadOnly = valorBool;

            //Bloqueo Pestaña Datos contacto
            txtApoderado.ReadOnly = valorBool;
            fuApoderado.Enabled = !valorBool;
            fuDDJJ.Enabled = !valorBool;
            fuDesignacionAutoridades.Enabled = !valorBool;
            txtVentasContacto.ReadOnly = valorBool;
            txtVentasCorreo.ReadOnly = valorBool;
            txtAdministracionContacto.ReadOnly = valorBool;
            txtAdministracionCorreo.ReadOnly = valorBool;
            txtTelefono.ReadOnly = valorBool;
            txtFax.ReadOnly = valorBool;

            //Bloqueo Pestaña Datos impositivos
            cboProveedor.Enabled = !valorBool;
            fuCuit.Enabled = !valorBool;
            fuExencionImpositiva.Enabled = !valorBool;
            fuNoRetencionIVA.Enabled = !valorBool;
            fuRetencionIVA.Enabled = !valorBool;
            fuConvenioMultilateral.Enabled = !valorBool;
            fuIngresosBrutos.Enabled = !valorBool;
            txtCuit.ReadOnly = valorBool;
            cboConImpG.Enabled = !valorBool;
            cboCondImIVA.Enabled = !valorBool;
            cboIngresosBrutos.Enabled = !valorBool;
            txtIngresosBrutos.ReadOnly = valorBool;

            //Bloqueo Pestaña Adjuntos
            chkBoxList.Enabled = !valorBool;
            txtObsCompras.ReadOnly = valorBool;

            //  }
        }
        private void CargarAdjuntos(SPListItem AltaProveedor)
        {
            SPAttachmentCollection collAttachments = AltaProveedor.Attachments;
            chkBoxList.Items.Clear();
            if (collAttachments.Count > 0)
            {
                btnEliminar.Visible = true;
                foreach (string nombreArchivo in collAttachments)
                {
                    ListItem itemChk = new ListItem();
                    itemChk.Value = nombreArchivo;
                    itemChk.Text = "<a class='vinculoIMGS' href='" + collAttachments.UrlPrefix + nombreArchivo + "' target='_blank' title='Click para ver la imagen'> " + nombreArchivo.ToString().Split('-')[0] + "</a></br>";
                    chkBoxList.Items.Add(itemChk);
                }
            }
            else
            {
                btnEliminar.Visible = false;
            }
        }
        private void CargarCombos()
        {
            SPSecurity.RunWithElevatedPrivileges(
                delegate ()
                {
                    using (SPSite site = new SPSite(sSitio))
                    {




                        SPListItemCollection Pais = site.OpenWeb().Lists["PaisesProvinciasSAP"].GetItems();
                        if (Pais.Count > 0)
                        {
                            DataView view1 = new DataView(Pais.GetDataTable());
                            DataTable distinctValues1 = view1.ToTable(true, "DENOMINACION_x0020_PAIS");
                            distinctValues1.DefaultView.Sort = "DENOMINACION_x0020_PAIS ASC";
                            distinctValues1 = distinctValues1.DefaultView.ToTable();

                            DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
                            rFilaSeleccioneSolicitud["DENOMINACION_x0020_PAIS"] = strSeleccione;
                            distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);



                            cboPais.DataSource = distinctValues1;
                            cboPais.DataValueField = "DENOMINACION_x0020_PAIS";
                            cboPais.DataTextField = "DENOMINACION_x0020_PAIS";
                            cboPais.DataBind();
                        }


                        // SPListItemCollection Provincia = site.OpenWeb().Lists["AltaProvisoria_Aux"].GetItems();
                        //if (Pais.Count > 0)
                        //{
                        //    DataView view1 = new DataView(Pais.GetDataTable());
                        //    DataTable distinctValues1 = view1.ToTable(true, "DENOMINACION_x0020_REGION");
                        //    distinctValues1.DefaultView.Sort = "DENOMINACION_x0020_REGION ASC";
                        //    DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
                        //    rFilaSeleccioneSolicitud["DENOMINACION_x0020_REGION"] = strSeleccione;
                        //    distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
                        //    cboProvincia.DataSource = distinctValues1;
                        //    cboProvincia.DataValueField = "DENOMINACION_x0020_REGION";
                        //    cboProvincia.DataTextField = "DENOMINACION_x0020_REGION";
                        //    cboProvincia.DataBind();
                        //}

                        SPQuery queryPers = new SPQuery();
                        string QuerySTRPers = "<View>" +
                            "<Query><Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>Personeria</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Activo</Value></Eq></And></Where></Query>" +
                                "</View>";
                        queryPers.ViewXml = QuerySTRPers;
                        SPListItemCollection Personeria = site.OpenWeb().Lists["AltaProvisoria_Aux"].GetItems(queryPers);
                        if (Personeria.Count > 0)
                        {
                            DataView view1 = new DataView(Personeria.GetDataTable());
                            DataTable distinctValues1 = view1.ToTable(true, "Valor");
                            DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
                            rFilaSeleccioneSolicitud["Valor"] = strSeleccione;
                            distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
                            cboPersoneria.DataSource = distinctValues1;
                            cboPersoneria.DataValueField = "Valor";
                            cboPersoneria.DataTextField = "Valor";
                            cboPersoneria.DataBind();
                        }
                        SPQuery queryCondImpG = new SPQuery();
                        string QuerySTRConImpG = "<View>" +
                            "<Query><Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>CondImpG</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Activo</Value></Eq></And></Where></Query>" +
                                "</View>";
                        queryCondImpG.ViewXml = QuerySTRConImpG;
                        SPListItemCollection ConImpG = site.OpenWeb().Lists["AltaProvisoria_Aux"].GetItems(queryCondImpG);
                        if (ConImpG.Count > 0)
                        {
                            DataView view1 = new DataView(ConImpG.GetDataTable());
                            DataTable distinctValues1 = view1.ToTable(true, "Valor");
                            DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
                            rFilaSeleccioneSolicitud["Valor"] = strSeleccione;
                            distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
                            cboConImpG.DataSource = distinctValues1;
                            cboConImpG.DataValueField = "Valor";
                            cboConImpG.DataTextField = "Valor";
                            cboConImpG.DataBind();

                        }
                        //SPQuery querySociedad = new SPQuery();
                        //string QuerySTRSociedad = "<View>" +
                        //    "<Query><Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>Sociedad</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Activo</Value></Eq></And></Where></Query>" +
                        //        "</View>";
                        //querySociedad.ViewXml = QuerySTRSociedad;
                        //SPListItemCollection Sociedad = site.OpenWeb().Lists["AltaProvisoria_Aux"].GetItems(querySociedad);
                        //if (Sociedad.Count > 0)
                        //{
                        //    cboSociedad.Items.Add(new ListItem(strSeleccione));
                        //    foreach (SPListItem item in Sociedad)
                        //    {
                        //        cboSociedad.Items.Add(item["Valor"].ToString());
                        //    }
                        //}
                        SPQuery queryCondImpIVA = new SPQuery();
                        string QuerySTRConImpIVA = "<View>" +
                            "<Query><Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>CondImpIVA</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Activo</Value></Eq></And></Where></Query>" +
                                "</View>";
                        queryCondImpIVA.ViewXml = QuerySTRConImpIVA;
                        SPListItemCollection ConImpIVA = site.OpenWeb().Lists["AltaProvisoria_Aux"].GetItems(queryCondImpIVA);
                        if (ConImpIVA.Count > 0)
                        {
                            DataView view1 = new DataView(ConImpIVA.GetDataTable());
                            DataTable distinctValues1 = view1.ToTable(true, "Valor");
                            DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
                            rFilaSeleccioneSolicitud["Valor"] = strSeleccione;
                            distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
                            cboCondImIVA.DataSource = distinctValues1;
                            cboCondImIVA.DataValueField = "Valor";
                            cboCondImIVA.DataTextField = "Valor";
                            cboCondImIVA.DataBind();
                        }
                        SPQuery queryIngBrut = new SPQuery();
                        string QuerySTRIngBrut = "<View>" +
                            "<Query><Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>IngresosBrutos</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Activo</Value></Eq></And></Where></Query>" +
                                "</View>";
                        queryIngBrut.ViewXml = QuerySTRIngBrut;
                        SPListItemCollection IngBrut = site.OpenWeb().Lists["AltaProvisoria_Aux"].GetItems(queryIngBrut);
                        if (IngBrut.Count > 0)
                        {
                            DataView view1 = new DataView(IngBrut.GetDataTable());
                            DataTable distinctValues1 = view1.ToTable(true, "Valor");


                            DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
                            rFilaSeleccioneSolicitud["Valor"] = strSeleccione;
                            distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
                            cboIngresosBrutos.DataSource = distinctValues1;
                            cboIngresosBrutos.DataValueField = "Valor";
                            cboIngresosBrutos.DataTextField = "Valor";
                            cboIngresosBrutos.DataBind();
                        }
                        SPQuery queryProveedor = new SPQuery();
                        string QuerySTRProveedor = "<View>" +
                            "<Query><Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>Proveedor</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Activo</Value></Eq></And></Where></Query>" +
                                "</View>";
                        queryProveedor.ViewXml = QuerySTRProveedor;
                        SPListItemCollection Provee = site.OpenWeb().Lists["AltaProvisoria_Aux"].GetItems(queryProveedor);
                        if (Provee.Count > 0)
                        {
                            DataView view1 = new DataView(Provee.GetDataTable());
                            DataTable distinctValues1 = view1.ToTable(true, "Valor");
                            DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
                            rFilaSeleccioneSolicitud["Valor"] = strSeleccione;
                            distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
                            cboProveedor.DataSource = distinctValues1;
                            cboProveedor.DataValueField = "Valor";
                            cboProveedor.DataTextField = "Valor";
                            cboProveedor.DataBind();
                        }
                        SPQuery queryRama01 = new SPQuery();
                        string QuerySTRRama01 = "<View>" +
                            "<Query><Where><And><IsNotNull><FieldRef Name='Title' /></IsNotNull><Neq><FieldRef Name='Estado' /><Value Type='Text'>No Activo</Value></Neq></And></Where></Query>" +

                                "</View>";
                        queryRama01.ViewXml = QuerySTRRama01;
                        SPListItemCollection Rama01 = site.OpenWeb().Lists["BienesServiciosAP"].GetItems(queryRama01);
                        cboRubro.Items.Add(new ListItem(strSeleccione));
                        foreach (SPListItem item in Rama01)
                        {
                            cboRubro.Items.Add(new ListItem((item["Title"] != null && !string.IsNullOrEmpty(item["Title"].ToString()) ? item["Title"].ToString() : String.Empty) + " - " + (item["RAMA02"] != null && !string.IsNullOrEmpty(item["RAMA02"].ToString()) ? item["RAMA02"].ToString() : String.Empty) + " - " + (item["RAMA03"] != null && !string.IsNullOrEmpty(item["RAMA03"].ToString()) ? item["RAMA03"].ToString() : String.Empty) + " - " + (item["RAMA04"] != null && !string.IsNullOrEmpty(item["RAMA04"].ToString()) ? item["RAMA04"].ToString() : String.Empty)));
                        }
                    }
                });
        }
        protected void btoMasServicio_Click(object sender, EventArgs e)
        {
            bool rubroExite = false;
            for (int i = 0; i < libServicio.Items.Count; i++)
            {
                if (cboRubro.SelectedValue.ToString() == libServicio.Items[i].ToString())
                {
                    rubroExite = true;
                }
            }
            if (rubroExite == false)
            {
                libServicio.Items.Add(cboRubro.SelectedValue);
                libServicio.DataBind();
            }
            if (Request.QueryString["ID"] == null)
            {
                btnGroupRubros.Style.Add("display", "inline-flex");
            }
        }
        protected void btoMenosServicio_Click(object sender, EventArgs e)
        {
            libServicio.Items.Remove(libServicio.SelectedItem);
            libServicio.DataBind();
            if (Request.QueryString["ID"] == null)
            {
                btnGroupRubros.Style.Add("display", "inline-flex");
            }
        }
        //protected void btoMasSociedad_Click(object sender, EventArgs e)
        //{
        //    listSociedad.Items.Add(cboSociedad.SelectedValue);
        //    listSociedad.DataBind();
        //}
        //protected void btoMenosSociedad_Click(object sender, EventArgs e)
        //{
        //    listSociedad.Items.Remove(listSociedad.SelectedItem);
        //    listSociedad.DataBind();
        //}
        protected void btnValidoRubros(object sender, EventArgs e)
        {
            if (libServicio.Items.Count < 1) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "validaCamposRubros(1);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "validaCamposRubros(0);", true);
            }

        }
        private int validarEmpresaRegistrada()

        {
            SPListItemCollection ListaAux = null;


            string CorreoVentas = txtVentasCorreo.Text;
            string QuerySTR = "<View><Query><Where><Or><Contains><FieldRef Name='VentasCorreo' /><Value Type='Text'>" + CorreoVentas.Split('@')[1] + "</Value></Contains><Or><Eq><FieldRef Name='RazonSocial' /><Value Type='Text'>" + txtNomFantasia.Text + "</Value></Eq><Or><Eq><FieldRef Name='SitioWeb' /><Value Type='Text'>" + txtSitioWeb.Text + "</Value></Eq><And><Eq><FieldRef Name='Calle' /><Value Type='Text'>" + txtCalle.Text + "</Value></Eq><Eq><FieldRef Name='Altura' /><Value Type='Text'>" + txtAltura.Text + "</Value></Eq></And></Or></Or></Or></Where></Query></View>";
            SPQuery query = new SPQuery();
            query.ViewXml = QuerySTR;

            string QuerySTRNacional = "<View><Query><Where><Eq><FieldRef Name='Cuit' /><Value Type='Text'>" + txtCuit.Text + "</Value></Eq></Where></Query></View>";
            SPQuery queryNacional = new SPQuery();
            queryNacional.ViewXml = QuerySTRNacional;


            if (cboProveedor.SelectedValue.ToString() == "Extranjero")
            {
                if (Request.QueryString["AN"] == null)
                {
                    ListaAux = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(query);
                }
                else
                {

                    SPSecurity.RunWithElevatedPrivileges(
                    delegate ()
                    {
                        using (SPSite site = new SPSite(sSitio))
                        {

                            using (SPWeb web = site.OpenWeb())
                            {

                                ListaAux = web.Lists["AltaProvisoria"].GetItems(query);

                            }
                        }
                    });
                }
            }

            if (cboProveedor.SelectedValue.ToString() == "Nacional")
            {
                if (Request.QueryString["AN"] == null)
                {
                    ListaAux = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(queryNacional);
                }
                else
                {
                    SPSecurity.RunWithElevatedPrivileges(
                    delegate ()
                    {
                        using (SPSite site = new SPSite(sSitio))
                        {
                            using (SPWeb web = site.OpenWeb())
                            {

                                ListaAux = web.Lists["AltaProvisoria"].GetItems(queryNacional);
                            }
                        }
                    });
                }

            }


            if (ListaAux.Count > 0)
            {

                return ListaAux.Count;
            }
            else
            {
                return 0;
            }



        }
        protected void EnvioDeEmails(string Autenticacion, string CodigoEmail)
        {
            if (Autenticacion != "ANONIMO")
            {
                switch (CodigoEmail)
                {
                    case "UAPMAP":
                        //PRIMER MAIL NO ANONIMO A EL PROVEEDOR 
                        string QuerySTRUAPMAP = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>UAPMAP</Value></Eq></Where></Query></View>";
                        SPQuery queryUAPMAP = new SPQuery();
                        queryUAPMAP.ViewXml = QuerySTRUAPMAP;
                        SPListItemCollection ListaABMUAPMAP = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryUAPMAP);
                        string htmlCorreoUAPMAP = "";
                        foreach (SPListItem item in ListaABMUAPMAP)
                        {
                            htmlCorreoUAPMAP = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                        }
                        htmlCorreoUAPMAP = htmlCorreoUAPMAP.Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/BannerAltaProvisoria.PNG").Replace("##RAZONSOCIAL##", txtRazonSocial.Text);
                        SPUtility.SendEmail(SPContext.Current.Web, false, false, txtVentasCorreo.Text, "Alta Portal Proveedores", htmlCorreoUAPMAP);
                        break;
                    case "CNRP":
                        //SEGUNDO MAIL NO ANONIMO A CONTABILIDAD
                        string QuerySTRCNRP = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CNRP</Value></Eq></Where></Query></View>";
                        SPQuery queryCNRP = new SPQuery();
                        queryCNRP.ViewXml = QuerySTRCNRP;
                        SPListItemCollection ListaABMCNRP = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCNRP);
                        string htmlCorreoCNRP = "";
                        string destinatarioCorreoCNRP = "";
                        foreach (SPListItem item in ListaABMCNRP)
                        {
                            htmlCorreoCNRP = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                            destinatarioCorreoCNRP = (item["UsuariosPara"] != null && !string.IsNullOrEmpty(item["UsuariosPara"].ToString()) ? item["UsuariosPara"].ToString() : String.Empty);
                        }
                        htmlCorreoCNRP = htmlCorreoCNRP.Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/BannerAltaProvisoria.PNG").Replace("##RAZONSOCIAL##", txtRazonSocial.Text).Replace("##DIAHORAACTUAL##", DateTime.Today.ToShortDateString());
                        SPUtility.SendEmail(SPContext.Current.Web, false, false, destinatarioCorreoCNRP, "Alta Portal Proveedores", htmlCorreoCNRP);
                        break;
                    case "CCM":
                        //CORREO A CONTABILIDAD POR MODIFICACION
                        string QuerySTRCCM = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CCM</Value></Eq></Where></Query></View>";
                        SPQuery queryCCM = new SPQuery();
                        queryCCM.ViewXml = QuerySTRCCM;
                        SPListItemCollection ListaABMCCM = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCCM);
                        string htmlCorreoCCM = "";
                        string destinatarioCorreoCCM = "";
                        foreach (SPListItem item in ListaABMCCM)
                        {
                            htmlCorreoCCM = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                            destinatarioCorreoCCM = (item["UsuariosPara"] != null && !string.IsNullOrEmpty(item["UsuariosPara"].ToString()) ? item["UsuariosPara"].ToString() : String.Empty);
                        }
                        htmlCorreoCNRP = htmlCorreoCCM.Replace("##NOMBREFANTASIA##", txtNomFantasia.Text).Replace("##REGISTRO##", SPContext.Current.Web.Url + "/_layouts/15/ENARSA/AltaProvisoria.aspx?ID=" + ID).Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/BannerAltaProvisoria.PNG");
                        SPUtility.SendEmail(SPContext.Current.Web, false, false, destinatarioCorreoCCM, "Alta Portal Proveedores", htmlCorreoCCM);
                        break;


                }
            }
            else
            {
                switch (CodigoEmail)
                {
                    case "UAPMAP":
                        SPSecurity.RunWithElevatedPrivileges(
                        delegate ()
                        {
                            using (SPSite site = new SPSite(sSitio))
                            {
                                string QuerySTRUAPMAP = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>UAPMAP</Value></Eq></Where></Query></View>";
                                SPQuery queryUAPMAP = new SPQuery();
                                queryUAPMAP.ViewXml = QuerySTRUAPMAP;
                                SPListItemCollection ListaABMUAPMAP = site.OpenWeb().Lists["ABMMails"].GetItems(queryUAPMAP);
                                string htmlCorreoUAPMAP = "";
                                foreach (SPListItem item in ListaABMUAPMAP)
                                {
                                    htmlCorreoUAPMAP = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                                }
                                htmlCorreoUAPMAP = htmlCorreoUAPMAP.Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/BannerAltaProvisoria.PNG").Replace("##RAZONSOCIAL##", txtRazonSocial.Text);
                                SPUtility.SendEmail(site.OpenWeb(), false, false, txtVentasCorreo.Text, "Alta Portal Proveedores", htmlCorreoUAPMAP);
                            }
                        });
                        break;
                    case "CNRP":
                        SPSecurity.RunWithElevatedPrivileges(
                        delegate ()
                        {
                            using (SPSite site = new SPSite(sSitio))
                            {
                                string QuerySTRCNRP = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CNRP</Value></Eq></Where></Query></View>";
                                SPQuery queryCNRP = new SPQuery();
                                queryCNRP.ViewXml = QuerySTRCNRP;
                                SPListItemCollection ListaABMCNRP = site.OpenWeb().Lists["ABMMails"].GetItems(queryCNRP);
                                string htmlCorreoCNRP = "";
                                string destinatarioCorreoCNRP = "";
                                foreach (SPListItem item in ListaABMCNRP)
                                {
                                    htmlCorreoCNRP = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                                    destinatarioCorreoCNRP = (item["UsuariosPara"] != null && !string.IsNullOrEmpty(item["UsuariosPara"].ToString()) ? item["UsuariosPara"].ToString() : String.Empty);
                                }
                                htmlCorreoCNRP = htmlCorreoCNRP.Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/BannerAltaProvisoria.PNG").Replace("##RAZONSOCIAL##", txtRazonSocial.Text).Replace("##DIAHORAACTUAL##", DateTime.Today.ToShortDateString());
                                SPUtility.SendEmail(site.OpenWeb(), false, false, destinatarioCorreoCNRP, "Alta Portal Proveedores", htmlCorreoCNRP);

                            }
                        });
                        break;
                }
            }




        }
        protected void EnvioDeEmails(string Autenticacion, string CodigoEmail, string ID, SPListItem RegistroExistente)
        {
            switch (CodigoEmail)
            {
                case "CCM":
                    //CORREO A CONTABILIDAD POR MODIFICACION
                    string QuerySTRCCM = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CCM</Value></Eq></Where></Query></View>";
                    SPQuery queryCCM = new SPQuery();
                    queryCCM.ViewXml = QuerySTRCCM;
                    SPListItemCollection ListaABMCCM = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCCM);
                    string htmlCorreoCCM = "";
                    string destinatarioCorreoCCM = "";
                    foreach (SPListItem item in ListaABMCCM)
                    {
                        htmlCorreoCCM = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                        destinatarioCorreoCCM = (item["UsuariosPara"] != null && !string.IsNullOrEmpty(item["UsuariosPara"].ToString()) ? item["UsuariosPara"].ToString() : String.Empty);
                    }
                    htmlCorreoCCM = htmlCorreoCCM.Replace("##NOMBREFANTASIA##", txtNomFantasia.Text).Replace("##REGISTRO##", SPContext.Current.Web.Url + "/_layouts/15/ENARSA/AltaProvisoria.aspx?ID=" + ID).Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/BannerAltaProvisoria.PNG");
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, destinatarioCorreoCCM, "Alta Portal Proveedores", htmlCorreoCCM);
                    break;
                case "CPSA":
                    //CORREO A PROVEEDOR REGISTRO APROBADO
                    string QuerySTRCPSA = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CPSA</Value></Eq></Where></Query></View>";
                    SPQuery queryCPSA = new SPQuery();
                    queryCPSA.ViewXml = QuerySTRCPSA;
                    SPListItemCollection ListaABMCPSA = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCPSA);
                    string htmlCorreoCPSA = "";

                    foreach (SPListItem item in ListaABMCPSA)
                    {
                        htmlCorreoCPSA = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                    }
                    htmlCorreoCPSA = htmlCorreoCPSA.Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/BannerAltaProvisoria.PNG").Replace("##REGISTRO##", SPContext.Current.Web.Url + "/info/MISDATOS.ASPX");
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, RegistroExistente["VentasCorreo"].ToString(), "Alta Portal Proveedores", htmlCorreoCPSA);
                    break;
                case "CPSMA":
                    //CORREO A PROVEEDOR REGISTRO MODIFICADO APROBADO
                    string QuerySTRCPMSA = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CPSMA</Value></Eq></Where></Query></View>";
                    SPQuery queryCPSMA = new SPQuery();
                    queryCPSMA.ViewXml = QuerySTRCPMSA;
                    SPListItemCollection ListaABMCPSMA = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCPSMA);
                    string htmlCorreoCPSMA = "";

                    foreach (SPListItem item in ListaABMCPSMA)
                    {
                        htmlCorreoCPSMA = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                    }
                    htmlCorreoCPSMA = htmlCorreoCPSMA.Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/BannerAltaProvisoria.PNG").Replace("##REGISTRO##", SPContext.Current.Web.Url + "/info/MISDATOS.ASPX");
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, RegistroExistente["VentasCorreo"].ToString(), "Alta Portal Proveedores", htmlCorreoCPSMA);
                    break;

                case "CPSR":
                    //CORREO A PROVEEDOR REGISTRO RECHAZADO
                    string QuerySTRCPSR = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CPSR</Value></Eq></Where></Query></View>";
                    SPQuery queryCPSR = new SPQuery();
                    queryCPSR.ViewXml = QuerySTRCPSR;
                    SPListItemCollection ListaABMCPSR = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCPSR);
                    string htmlCorreoCPSR = "";
                    foreach (SPListItem item in ListaABMCPSR)
                    {
                        htmlCorreoCPSR = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                    }
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, RegistroExistente["VentasCorreo"].ToString(), "Alta Portal Proveedores", htmlCorreoCPSR.Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/BannerAltaProvisoria.PNG"));
                    break;
                case "CPSS":
                    //CORREO A PROVEEDOR REGISTRO SUBSANADO
                    string QuerySTRCPSS = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CPSS</Value></Eq></Where></Query></View>";
                    SPQuery queryCPSS = new SPQuery();
                    queryCPSS.ViewXml = QuerySTRCPSS;
                    SPListItemCollection ListaABMCPSS = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCPSS);
                    string htmlCorreoCPSS = "";
                    foreach (SPListItem item in ListaABMCPSS)
                    {
                        htmlCorreoCPSS = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                    }
                    htmlCorreoCPSS = htmlCorreoCPSS.Replace("##OBSERVACIONES##", RegistroExistente["ObservacionesCompras"].ToString()).Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/BannerAltaProvisoria.PNG");

                    SPUtility.SendEmail(SPContext.Current.Web, false, false, RegistroExistente["VentasCorreo"].ToString(), "Alta Portal Proveedores", htmlCorreoCPSS.Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/BannerAltaProvisoria.PNG"));
                    break;
                case "CPSSB":
                    //CORREO A PROVEEDOR REGISTRO SUSPENDIDO
                    string QuerySTRCPSSB = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CPSSB</Value></Eq></Where></Query></View>";
                    SPQuery queryCPSSB = new SPQuery();
                    queryCPSSB.ViewXml = QuerySTRCPSSB;
                    SPListItemCollection ListaABMCPSSB = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCPSSB);
                    string htmlCorreoCPSSB = "";
                    foreach (SPListItem item in ListaABMCPSSB)
                    {
                        htmlCorreoCPSSB = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                    }
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, RegistroExistente["VentasCorreo"].ToString(), "Alta Portal Proveedores", htmlCorreoCPSSB.Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/BannerAltaProvisoria.PNG"));
                    break;
                case "CPSPI":
                    //CORREO A PROVEEDOR PRE INSCRIPTO
                    string QuerySTRCPSPI = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CPSPI</Value></Eq></Where></Query></View>";
                    SPQuery queryCPSPI = new SPQuery();
                    queryCPSPI.ViewXml = QuerySTRCPSPI;
                    SPListItemCollection ListaABMCPSPI = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCPSPI);
                    string htmlCorreoCPSPI = "";
                    foreach (SPListItem item in ListaABMCPSPI)
                    {
                        htmlCorreoCPSPI = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                    }
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, RegistroExistente["VentasCorreo"].ToString(), "Alta Portal Proveedores", htmlCorreoCPSPI.Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/BannerAltaProvisoria.PNG"));
                    break;
            }
        }
        protected void EnvioDeEmails(string Autenticacion, string CodigoEmail, string ID, SPListItem RegistroExistente, string Usuario, string Password)
        {
            switch (CodigoEmail)
            {
                case "CPUC":
                    //CORREO A CONTABILIDAD POR MODIFICACION
                    string QuerySTRCPUC = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CPUC</Value></Eq></Where></Query></View>";
                    SPQuery queryCPUC = new SPQuery();
                    queryCPUC.ViewXml = QuerySTRCPUC;
                    SPListItemCollection ListaABMCCM = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCPUC);
                    string htmlCorreoCPUC = "";

                    foreach (SPListItem item in ListaABMCCM)
                    {
                        htmlCorreoCPUC = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);

                    }
                    htmlCorreoCPUC = htmlCorreoCPUC.Replace("##URL##", sSitio).Replace("##PASWORD##", Password).Replace("##CUIT##", Usuario).Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/BannerAltaProvisoria.PNG");
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, RegistroExistente["VentasCorreo"].ToString().Trim(), "Alta Portal Proveedores", htmlCorreoCPUC);
                    break;
            }
        }
        protected void btnGuardar(object sender, EventArgs e)
        {
            if (validarEmpresaRegistrada() != 0 && Request.QueryString["ID"] == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeExisteEmpresa();", true);
            }
            else
            {
                if (Request.QueryString["ID"] != null)
                {
                    SPListItem RegistroExistente = SPContext.Current.Web.Lists["AltaProvisoria"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                    GuardarDatos(RegistroExistente);
                    RegistroExistente.Update();
                    GuardarRubro(RegistroExistente.ID);

                    GuardarSociedad(RegistroExistente.ID);
                    if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("COMPRAS")))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeModificado(1);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeModificado(4);", true);
                    }
                }
                else
                {
                    if (Request.QueryString["AN"] == null)
                    {
                        using (SPSite site = new SPSite(sSitio))
                        {
                            using (SPWeb web = site.OpenWeb())
                            {
                                site.AllowUnsafeUpdates = true;
                                web.AllowUnsafeUpdates = true;
                                // SE CREA EL REGISTRO PARA EL PROVEEDOR
                                SPListItem RegistroNuevo = web.Lists["AltaProvisoria"].AddItem();
                                GuardarDatos(RegistroNuevo);
                                RegistroNuevo.Update();
                                //PRIMER MAIL NO ANONIMO A EL PROVEEDOR 
                                EnvioDeEmails("LOGIN", "UAPMAP");
                                //SEGUNDO MAIL NO ANONIMO A CONTABILIDAD
                                EnvioDeEmails("LOGIN", "CNRP");

                                //SE GUARDA EL RUBRO
                                GuardarRubro(RegistroNuevo.ID);
                                site.AllowUnsafeUpdates = false;
                                web.AllowUnsafeUpdates = false;
                                if (Request.QueryString["AN"] != null)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeGuardaAN();", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeGuarda();", true);
                                }
                            }
                        }
                        if (Request.QueryString["AN"] != null)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeGuardaAN();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeGuarda();", true);
                        }
                    }
                    else
                    {
                        SPSecurity.RunWithElevatedPrivileges(
                        delegate ()
                        {
                            using (SPSite site = new SPSite(sSitio))
                            {
                                using (SPWeb web = site.OpenWeb())
                                {
                                    site.AllowUnsafeUpdates = true;
                                    web.AllowUnsafeUpdates = true;

                                    // SE CREA EL REGISTRO PARA EL PROVEEDOR
                                    SPListItem RegistroNuevo = web.Lists["AltaProvisoria"].AddItem();
                                    GuardarDatos(RegistroNuevo);
                                    RegistroNuevo.Update();
                                    //PRIMER MAIL  ANONIMO A EL PROVEEDOR 
                                    EnvioDeEmails("ANONIMO", "UAPMAP");
                                    //SEGUNDO MAIL  ANONIMO A CONTABILIDAD
                                    EnvioDeEmails("ANONIMO", "CNRP");
                                    //SE GUARDA EL RUBRO
                                    GuardarRubro(RegistroNuevo.ID);
                                    site.AllowUnsafeUpdates = false;
                                    web.AllowUnsafeUpdates = false;
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeGuardaAN();", true);
                                }
                            }
                        });
                    }
                }
            }
        }
        private void GuardarRubro(int p)
        {
            if (Request.QueryString["ID"] != null)
            {
                string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='IDRegistro' /><Value Type='Text'>" + p + "</Value></Eq></Where></Query></View>";
                SPQuery query = new SPQuery();
                query.ViewXml = QuerySTR;
                SPListItemCollection ListaAux = SPContext.Current.Web.Lists["AltaProvisoriaRubros"].GetItems(query);
                if (ListaAux.Count > 0)
                {
                    foreach (SPListItem item in ListaAux)
                    {
                        SPListItem item2 = SPContext.Current.Web.Lists["AltaProvisoriaRubros"].GetItemById(item.ID);
                        item2.Delete();
                    }
                }
            }
            if (libServicio.Items.Count > 0)
            {
                SPSecurity.RunWithElevatedPrivileges(
                delegate ()
                {
                    using (SPSite site = new SPSite(sSitio))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            site.AllowUnsafeUpdates = true;
                            web.AllowUnsafeUpdates = true;
                            for (int i = 0; i < libServicio.Items.Count; i++)
                            {
                                SPListItem Rubro = web.Lists["AltaProvisoriaRubros"].AddItem();
                                Rubro["Title"] = libServicio.Items[i].ToString();
                                Rubro["IDRegistro"] = p;
                                Rubro.Update();
                            }
                            site.AllowUnsafeUpdates = false;
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                });
            }

        }
        private void GuardarSociedad(int p)
        {
            if (Request.QueryString["ID"] != null)
            {
                string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='IDRegistro' /><Value Type='Text'>" + p + "</Value></Eq></Where></Query></View>";
                SPQuery query = new SPQuery();
                query.ViewXml = QuerySTR;
                SPListItemCollection ListaAux = SPContext.Current.Web.Lists["AltaProvisoriaSociedad"].GetItems(query);
                if (ListaAux.Count > 0)
                {
                    foreach (SPListItem item in ListaAux)
                    {
                        SPListItem item2 = SPContext.Current.Web.Lists["AltaProvisoriaSociedad"].GetItemById(item.ID);
                        item2.Delete();
                    }
                }
            }


        }
        private void GuardarDatos(SPListItem Registro)
        {
            if (Request.QueryString["ID"] != null)
            {
                if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("COMPRAS")))
                {
                }
                else
                {
                    Registro["Estado"] = "Pendiente";
                    Registro["RegistroModificado"] = "SI";
                    Registro["EstadoSAP"] = "MODIFICADO";
                    CorreoComprasModificacion(Request.QueryString["ID"]);
                }
            }
            else
            {

                Registro["Estado"] = "Pendiente";
                Registro["EstadoSAP"] = "NUEVO";
                Registro["RegistroModificado"] = "NO";
            }
            //Datos Generales
            Registro["NombreFantasia"] = (txtRazonSocial.Text != null && !string.IsNullOrEmpty(txtRazonSocial.Text) ? txtRazonSocial.Text : String.Empty);
            Registro["RazonSocial"] = (txtNomFantasia.Text != null && !string.IsNullOrEmpty(txtNomFantasia.Text) ? txtNomFantasia.Text : String.Empty);
            Registro["Personeria"] = cboPersoneria.SelectedItem.Text;
            Registro["ActividadPrincipal"] = (txtActPrinc.Text != null && !string.IsNullOrEmpty(txtActPrinc.Text) ? txtActPrinc.Text : String.Empty);
            Registro["SitioWeb"] = (txtSitioWeb.Text != null && !string.IsNullOrEmpty(txtSitioWeb.Text) ? txtSitioWeb.Text : String.Empty);
            Registro["Pais"] = cboPais.SelectedItem.Text;
            //   if (cboPais.SelectedValue == "Argentina")
            //  {
            Registro["ProvinciaArg"] = cboProvincia.SelectedItem.Text;
            //}
            //else
            //{
            Registro["Provincia"] = cboProvincia.SelectedItem.Text;// (txtProvincia.Text != null && !string.IsNullOrEmpty(txtProvincia.Text) ? txtProvincia.Text : String.Empty);
                                                                   // }
            Registro["Ciudad"] = (txtCiudad.Text != null && !string.IsNullOrEmpty(txtCiudad.Text) ? txtCiudad.Text : String.Empty);
            Registro["CodPostal"] = (txtCodPostal.Text != null && !string.IsNullOrEmpty(txtCodPostal.Text) ? txtCodPostal.Text : String.Empty);
            Registro["Calle"] = (txtCalle.Text != null && !string.IsNullOrEmpty(txtCalle.Text) ? txtCalle.Text : String.Empty);
            Registro["Altura"] = (txtAltura.Text != null && !string.IsNullOrEmpty(txtAltura.Text) ? txtAltura.Text : String.Empty);
            Registro["Depto"] = (txtDepto.Text != null && !string.IsNullOrEmpty(txtDepto.Text) ? txtDepto.Text : String.Empty);
            Registro["Piso"] = (txtPiso.Text != null && !string.IsNullOrEmpty(txtPiso.Text) ? txtPiso.Text : String.Empty);

            //if (fuPersoneriaJur.HasFiles)
            //{
            //    foreach (HttpPostedFile uploadedFile in fuPersoneriaJur.PostedFiles)
            //    {
            //        Stream fs = fuPersoneriaJur.PostedFile.InputStream;
            //        byte[] fileContents = new byte[fs.Length];
            //        fs.Read(fileContents, 0, (int)fs.Length);
            //        fs.Close();
            //        SPAttachmentCollection attachments = Registro.Attachments;

            //        string fileName = "Personería Jurídica" + System.IO.Path.GetExtension(fuPersoneriaJur.FileName);
            //        attachments.Add(fileName, fileContents);
            //    }
            //}
            if (fuPersoneriaJur.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuPersoneriaJur.PostedFiles)
                {
                    Guid g = Guid.NewGuid();
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Personería Jurídica" + " - " + g.ToString() + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
                }
            }
            //Fin Datos Generales
            //Datos Contacto
            Registro["ApoderadoResp"] = (txtApoderado.Text != null && !string.IsNullOrEmpty(txtApoderado.Text) ? txtApoderado.Text : String.Empty);

            if (fuApoderado.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuApoderado.PostedFiles)
                {
                    Guid g = Guid.NewGuid();
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Nombre Apoderado o Representante Legal" + " - " + g.ToString() + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
                }
            }

            Registro["VentasContacto"] = (txtVentasContacto.Text != null && !string.IsNullOrEmpty(txtVentasContacto.Text) ? txtVentasContacto.Text : String.Empty);
            Registro["VentasCorreo"] = (txtVentasCorreo.Text != null && !string.IsNullOrEmpty(txtVentasCorreo.Text) ? txtVentasCorreo.Text : String.Empty);
            Registro["AdministracionContacto"] = (txtAdministracionContacto.Text != null && !string.IsNullOrEmpty(txtAdministracionContacto.Text) ? txtAdministracionContacto.Text : String.Empty);
            Registro["AdministracionCorreo"] = (txtAdministracionCorreo.Text != null && !string.IsNullOrEmpty(txtAdministracionCorreo.Text) ? txtAdministracionCorreo.Text : String.Empty);
            Registro["Telefono"] = (txtTelefono.Text != null && !string.IsNullOrEmpty(txtTelefono.Text) ? txtTelefono.Text : String.Empty);
            Registro["Fax"] = (txtFax.Text != null && !string.IsNullOrEmpty(txtFax.Text) ? txtFax.Text : String.Empty);
            if (fuDDJJ.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuDDJJ.PostedFiles)
                {
                    Guid g = Guid.NewGuid();
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "DDJJ del 202" + " - " + g.ToString() + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
                }
            }
            if (fuDesignacionAutoridades.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuDesignacionAutoridades.PostedFiles)
                {
                    Guid g = Guid.NewGuid();
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Designación de autoridades" + " - " + g.ToString() + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
                }
            }
            //Fin Datos Contacto
            //Datos Impositivos
            Registro["Cuit"] = (txtCuit.Text != null && !string.IsNullOrEmpty(txtCuit.Text) ? (txtCuit.Text).Replace("-", "") : String.Empty);
            if (fuCuit.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuCuit.PostedFiles)
                {
                    Guid g = Guid.NewGuid();
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "CUIT" + " - " + g.ToString() + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
                }
            }
            if (chkPersFis.Checked == true) {
                Registro["PersonaFisica"] = "SI";
            }
            else
            {
                Registro["PersonaFisica"] = "NO";
            }


            Registro["Proveedor"] = cboProveedor.SelectedItem.Text;
            Registro["CondImpIG"] = cboConImpG.SelectedItem.Text;

            if (fuExencionImpositiva.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuExencionImpositiva.PostedFiles)
                {
                    Guid g = Guid.NewGuid();
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Constancia impositiva" + " - " + g.ToString() + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
                }
            }
            if (fuRetencionIVA.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuRetencionIVA.PostedFiles)
                {
                    Guid g = Guid.NewGuid();
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Agente de Retención IVA" + " - " + g.ToString() + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
                }
            }
            if (fuNoRetencionIVA.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuNoRetencionIVA.PostedFiles)
                {
                    Guid g = Guid.NewGuid();
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Certificado No Retención - IVA - Ganancias - SUSS - IIBB" + " - " + g.ToString() + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
                }
            }

            if (fuConvenioMultilateral.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuConvenioMultilateral.PostedFiles)
                {
                    Guid g = Guid.NewGuid();
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Convenio Multilateral" + " - " + g.ToString() + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
                }
            }


            Registro["CondImpIVA"] = cboCondImIVA.SelectedItem.Text;

            Registro["IngresosBrutos"] = cboIngresosBrutos.SelectedItem.Text;
            if (fuIngresosBrutos.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuIngresosBrutos.PostedFiles)
                {
                    Guid g = Guid.NewGuid();
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Ingresos Brutos" + " - " + g.ToString() + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
                }
            }
            Registro["NumeroIngresosBrutos"] = (txtIngresosBrutos.Text != null && !string.IsNullOrEmpty(txtIngresosBrutos.Text) ? txtIngresosBrutos.Text : String.Empty);
            //Fin Datos Impositivos
            //SAP//
            if (chkSAP.Checked == true)
            {
                Registro["PasarSAP"] = "SI";

            }
            else
            {
                Registro["PasarSAP"] = "NO";
            }

        }
        private void CorreoComprasModificacion(string p)
        {
            SPListItem RegistroExistente = SPContext.Current.Web.Lists["AltaProvisoria"].AddItem();
            EnvioDeEmails("LOGIN", "CCM", p, RegistroExistente);
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public string RandomPassword(int size = 0)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        protected string SetErrorMessage(MembershipCreateStatus status)
        {
            string sError = string.Empty;
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return sError = LocalizedString.GetGlobalString("FBAPackWebPages", "DuplicateUserName");
                    break;

                case MembershipCreateStatus.DuplicateEmail:
                    return sError = LocalizedString.GetGlobalString("FBAPackWebPages", "DuplicateEmail");
                    break;

                case MembershipCreateStatus.InvalidPassword:
                    string message = "";
                    if (string.IsNullOrEmpty(Utils.BaseMembershipProvider().PasswordStrengthRegularExpression))
                    {
                        message = string.Format(LocalizedString.GetGlobalString("FBAPackWebPages", "InvalidPasswordChars"), Utils.BaseMembershipProvider().MinRequiredPasswordLength, Utils.BaseMembershipProvider().MinRequiredNonAlphanumericCharacters);
                    }
                    else
                    {
                        message = string.Format(LocalizedString.GetGlobalString("FBAPackWebPages", "InvalidPasswordCharsRegex"), Utils.BaseMembershipProvider().MinRequiredPasswordLength, Utils.BaseMembershipProvider().MinRequiredNonAlphanumericCharacters, Utils.BaseMembershipProvider().PasswordStrengthRegularExpression);
                    }
                    return message;
                    break;

                case MembershipCreateStatus.InvalidEmail:
                    return sError = LocalizedString.GetGlobalString("FBAPackWebPages", "InvalidEmail");
                    break;

                case MembershipCreateStatus.InvalidAnswer:
                    return sError = LocalizedString.GetGlobalString("FBAPackWebPages", "InvalidAnswer");
                    break;

                case MembershipCreateStatus.InvalidQuestion:
                    return sError = LocalizedString.GetGlobalString("FBAPackWebPages", "InvalidQuestion");
                    break;

                case MembershipCreateStatus.InvalidUserName:
                    return sError = LocalizedString.GetGlobalString("FBAPackWebPages", "InvalidUserName");
                    break;

                case MembershipCreateStatus.ProviderError:
                    return sError = LocalizedString.GetGlobalString("FBAPackWebPages", "ProviderError");
                    break;

                case MembershipCreateStatus.UserRejected:
                    return sError = LocalizedString.GetGlobalString("FBAPackWebPages", "UserRejected");
                    break;

                default:
                    return sError = LocalizedString.GetGlobalString("FBAPackWebPages", "UnknownError");
                    break;
            }
        }
        private void AddUserToSite(string login, string email, string fullname)
        {
            this.Web.AllUsers.Add(
                login,
                email,
                fullname,
                "");
        }
        protected void btnAprobarCompras_ServerClick(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPListItem RegistroExistente = SPContext.Current.Web.Lists["AltaProvisoria"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                bool _showRoles = (new MembershipSettings(SPContext.Current.Web)).EnableRoles;
                MembershipUser user = null;
                if (RegistroExistente["Cuit"] != null && !string.IsNullOrEmpty(RegistroExistente["Cuit"].ToString()))
                {
                    user = Utils.BaseMembershipProvider().GetUser(RegistroExistente["Cuit"].ToString(), false);
                }
                if (RegistroExistente["CodigoExtranjero"] != null && !string.IsNullOrEmpty(RegistroExistente["CodigoExtranjero"].ToString()))
                {
                    user = Utils.BaseMembershipProvider().GetUser(RegistroExistente["CodigoExtranjero"].ToString(), false);
                }

                //SI EL CUIT NO ESTA VACIO, EL PROVEEDOR ES NACIOAL
                SPUser Usuario = null;
                string sMensaje = string.Empty;
                bool UsuarioCreado = false;
                string sMensajeModal = string.Empty;
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    if (user == null)
                    {
                        try
                        {
                            // get site reference             
                            string provider = Utils.GetMembershipProvider(this.Site);
                            // create FBA database user
                            MembershipCreateStatus createStatus;
                            string sPasword = RandomPassword() + "+";
                            string sCodigoExtranjero = string.Empty;

                            if (RegistroExistente["Proveedor"].ToString() == "Extranjero")
                            {
                                int iNuevoCodigo = 0;
                                SPListItem ProvExtranjeroNuevo = SPContext.Current.Web.Lists["AltaProvisoriaProveedorExtranjero"].AddItem();
                                string QuerySTR = "<View><Query><OrderBy><FieldRef Name='NumCodExtranjero' Ascending='False' /></OrderBy></Query></View>";
                                SPQuery query = new SPQuery();
                                query.ViewXml = QuerySTR;
                                SPListItemCollection ListaAux = SPContext.Current.Web.Lists["AltaProvisoriaProveedorExtranjero"].GetItems(query);
                                ProvExtranjeroNuevo["NumCodExtranjero"] = int.Parse(ListaAux[0]["NumCodExtranjero"].ToString()) + 1;

                                if (ListaAux[0]["NumCodExtranjero"].ToString().Length == 3)
                                {
                                    iNuevoCodigo = int.Parse(ListaAux[0]["NumCodExtranjero"].ToString()) + 1;
                                    sCodigoExtranjero = "EXT" + iNuevoCodigo.ToString();
                                }
                                else if (ListaAux[0]["NumCodExtranjero"].ToString().Length == 2)
                                {
                                    iNuevoCodigo = int.Parse(ListaAux[0]["NumCodExtranjero"].ToString()) + 1;
                                    sCodigoExtranjero = "EXT0" + iNuevoCodigo.ToString();
                                }
                                else
                                {
                                    iNuevoCodigo = int.Parse(ListaAux[0]["NumCodExtranjero"].ToString()) + 1;
                                    sCodigoExtranjero = "EXT00" + iNuevoCodigo.ToString();
                                }
                                RegistroExistente["CodigoExtranjero"] = sCodigoExtranjero;
                                RegistroExistente.Update();
                                ProvExtranjeroNuevo.Update();
                            }
                            if (RegistroExistente["Cuit"] != null && !string.IsNullOrEmpty(RegistroExistente["Cuit"].ToString()))
                            {

                                user = Utils.BaseMembershipProvider().CreateUser(RegistroExistente["Cuit"].ToString(), sPasword, RegistroExistente["VentasCorreo"].ToString(), null, null, true, null, out createStatus);

                            }
                            else
                            {
                                user = Utils.BaseMembershipProvider().CreateUser(sCodigoExtranjero, sPasword, RegistroExistente["VentasCorreo"].ToString(), null, null, true, null, out createStatus);
                            }
                            if (createStatus != MembershipCreateStatus.Success)
                            {
                                sMensaje = SetErrorMessage(createStatus);
                            }
                            // add user to SharePoint whether a role was selected or not
                            AddUserToSite(Utils.EncodeUsername(user.UserName), user.Email, RegistroExistente["NombreFantasia"].ToString());
                            // add user to group
                            SPGroup group = this.Web.SiteGroups["EXTERNOS"];
                            group.AddUser(Utils.EncodeUsername(user.UserName), user.Email, RegistroExistente["NombreFantasia"].ToString(), "");
                            group.Update();
                            EnvioDeEmails("LOGIN", "CPUC", "0", RegistroExistente, ((RegistroExistente["Cuit"] != null) ? RegistroExistente["Cuit"].ToString() : sCodigoExtranjero), sPasword);
                            //      
                            UsuarioCreado = true;
                        }
                        catch (Exception EX)
                        {
                            sMensajeModal = "El registro no fue aprobado debido a que el usuario no se pudo crear: " + sMensaje;
                        }
                    }
                    else
                    {
                        try
                        {
                            MembershipUser userMembership = Utils.BaseMembershipProvider().GetUser((RegistroExistente["Cuit"] != null ? SPContext.Current.Web.EnsureUser(RegistroExistente["Cuit"].ToString()) : SPContext.Current.Web.EnsureUser(RegistroExistente["CodigoExtranjero"].ToString())), false);
                            userMembership.Email = RegistroExistente["VentasCorreo"].ToString().Trim();
                            Utils.BaseMembershipProvider().UpdateUser(userMembership);
                            // user.Email = RegistroExistente["VentasCorreo"].ToString().Trim();
                            Usuario = ((RegistroExistente["Cuit"] != null) ? SPContext.Current.Web.EnsureUser(RegistroExistente["Cuit"].ToString()) : SPContext.Current.Web.EnsureUser(RegistroExistente["CodigoExtranjero"].ToString()));
                            Usuario.Email = RegistroExistente["VentasCorreo"].ToString().Trim();
                            Usuario.Update();
                        }
                        catch (Exception EX) { }
                    }
                    if (RegistroExistente["RegistroModificado"] != null && RegistroExistente["RegistroModificado"].ToString() == "SI")
                    {
                        EnvioDeEmails("LOGIN", "CPSMA", "0", RegistroExistente);
                    }
                    else
                    {
                        EnvioDeEmails("LOGIN", "CPSA", "0", RegistroExistente);
                    }
                    RegistroExistente["ObservacionesCompras"] = (txtObsCompras.Text != null && !string.IsNullOrEmpty(txtObsCompras.Text) ? txtObsCompras.Text : String.Empty);
                    RegistroExistente["Estado"] = "Aprobado";
                    if (UsuarioCreado == true)
                    {
                        RegistroExistente["Author"] = SPContext.Current.Web.EnsureUser(user.UserName);
                        sMensajeModal = "El registro se aprobo correctamente, el usuario fue creado y fue notificado via email el proveedor.";
                    }
                    else
                    {
                        SPUser UsuarioCreador = null;
                        if (RegistroExistente["Cuit"] != null && !string.IsNullOrEmpty(RegistroExistente["Cuit"].ToString()))
                        {
                            UsuarioCreador = SPContext.Current.Web.EnsureUser(RegistroExistente["Cuit"].ToString());
                        }
                        if (RegistroExistente["CodigoExtranjero"] != null && !string.IsNullOrEmpty(RegistroExistente["CodigoExtranjero"].ToString()))
                        {
                            UsuarioCreador = SPContext.Current.Web.EnsureUser(RegistroExistente["CodigoExtranjero"].ToString());
                        }
                        sMensajeModal = "El registro se aprobo correctamente.";
                    }
                    RegistroExistente.Update();
                });
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeCompra('" + sMensajeModal + "');", true);
            });
            if (chkSAP.Checked == true)
            {
                PasarSAP();
            }
        }

        protected void btnRechazarCompras_ServerClick(object sender, EventArgs e)
        {
            SPListItem RegistroExistente = SPContext.Current.Web.Lists["AltaProvisoria"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
            RegistroExistente["ObservacionesCompras"] = (txtObsCompras.Text != null && !string.IsNullOrEmpty(txtObsCompras.Text) ? txtObsCompras.Text : String.Empty);
            RegistroExistente["Estado"] = "Rechazado";
            RegistroExistente.Update();

            EnvioDeEmails("LOGIN", "CPSR", "0", RegistroExistente);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeModificado(2);", true);
        }
        protected void btnPreInscribiCompras_ServerClick(object sender, EventArgs e)
        {
            SPListItem RegistroExistente = SPContext.Current.Web.Lists["AltaProvisoria"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
            RegistroExistente["ObservacionesCompras"] = (txtObsCompras.Text != null && !string.IsNullOrEmpty(txtObsCompras.Text) ? txtObsCompras.Text : String.Empty);
            RegistroExistente["Estado"] = "Pre-inscripto";
            RegistroExistente.Update();

            EnvioDeEmails("LOGIN", "CPSPI", "0", RegistroExistente);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeModificado(2);", true);
        }

        protected void btnSubsanandoCompras_ServerClick(object sender, EventArgs e)
        {
            SPListItem RegistroExistente = SPContext.Current.Web.Lists["AltaProvisoria"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
            RegistroExistente["ObservacionesCompras"] = (txtObsCompras.Text != null && !string.IsNullOrEmpty(txtObsCompras.Text) ? txtObsCompras.Text : String.Empty);
            RegistroExistente["Estado"] = "Subsanado";
            RegistroExistente.Update();

            EnvioDeEmails("LOGIN", "CPSS", "0", RegistroExistente);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeModificado(2);", true);
        }
        protected void btnSuspenderCompras_ServerClick(object sender, EventArgs e)
        {
            SPListItem RegistroExistente = SPContext.Current.Web.Lists["AltaProvisoria"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
            RegistroExistente["ObservacionesCompras"] = (txtObsCompras.Text != null && !string.IsNullOrEmpty(txtObsCompras.Text) ? txtObsCompras.Text : String.Empty);
            RegistroExistente["Estado"] = "Suspendido/Bloqueado";
            RegistroExistente.Update();

            EnvioDeEmails("LOGIN", "CPSSB", "0", RegistroExistente);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeModificado(2);", true);
        }
        public partial class RespuestaWebService
        { }
        private void PasarSAP()

        {

            string Usuario = SPContext.Current.Web.CurrentUser.ToString().Split('\\')[1].ToString(); //usuario que realizo la operacion;
            string Status = ""; //status => [NUEVO|MODIFICADO|BLOQUEADO]
            string IdSAP = "";// si el campo EstadoSAP es Nuevo, enviar vacío. Si es una modificación, ya existe en SAP, enviar el valor idSAP 
            string idProveedorSP = ""; //idProvedorSap => ( Vacío si es nacionales/ CIE si es extranjeros )
            string GrupoCuenta = ""; //GrupoCuenta => [YB01(si es nacional) | YBIV(si es extranjero)]
            string tratamiento = ""; //tratamiento => VACIO
            string nombre = ""; //Nombre => Razon Social
            string nombre2 = ""; //nombre2 => Nombre fantasia
            string conceptoB12 = ""; //conceptoB12 => Actividad principal
            string conceptoB2 = ""; //conceptoB2 =>VACIO
            string plantaEdificio = ""; //plantaEdificio => Piso
            string nPiso = ""; //nPiso => Depto
            string calle = ""; //calle => Calle
            string numero = ""; // numero => Altura
            string codigoPostal = "";//codigoPostal => Cod. Postal
            string poblacion = ""; //Poblacion => Ciudad 
            string pais = "";// Pais =>enviar 2 letras de la tabla Pais
            string region = "";//region => enviar 2/3 letras de la tabla Region
            string idioma = ""; //idioma => ES 

            string tel1 = ""; //tel1=>telefono
            string tel2 = "";//tel2 => VACIO
            string direccion = "";// direccion => Dirección de e-mail – Ventas, 2 
            string direccion2 = ""; //direccion => Dirección de e -mail - Administración
            string nIdentFis1 = "";//nIdentFis1 =>Cuit, si es Nacionales/ Vacío  para extranjeros ,
            string tipoNIF = "";//tipoNIF => 80 si es nacional/83 si es extranjero
            string claseImpuesto = ""; //claseImpuesto => Condición Impositiva(IVA) enviar primeros 2 números de la lista desplegable o de la tabla Clase de Impuesto
            string personaFisica = "";//personaFisica => Persona Física. Si es true, enviar una X. Si es False, enviar Vacío 
            string claveBanco = "";  //claveBanco => ?? 
            string nCuentaBancaria = ""; //nCuentaBancaria =??
            string pais2 = "";//pais=>??
            string CuentaAsociada = "";// CuentaAsociada=>2110010
            string GrupoTesoreria = "";// GrupoTesoreria => A1 si es nacionales / A2 si es extranjero, - 
            string codigoActividad = "";// codigoActividad => Actividad principal,
            string claseDistrib = "";//claseDistrib => Tipo ingresos brtuos
            string nExtension = ""; //nExtension => ??
            string motivoExencion = "";//motivoExencion =>?? 
            string verifFraDupl = "X"; //verifFraDupl => x
            string viasdePago = ""; //viasdePago => ??
            string notaInterior = ""; //notaInterior =>??
            string paisRetencion = "";//paisRetencion => ???
            string monedaPedido = "";// monedadPedido =>ARS si es nacional/USD si es Extranjero
            string grupoEsqProveedor = "";// grupoEsqProveedor => 03  ,
            string verifFacBaseEM = "X";//  - verifFacBaseEM =>True
            string telefonoProveedor = "";//telefonoProveedor => telefono
            string vendedorProveedor = ""; //vendedorProveedor => Pesona de Contacto -Ventas, 
            string pedidoAutoPermitido = "";//pedidoAutoPermitido => VACIO
            string facturaRelativaServicio = "X";//-facturaRelativaServicio => X


            SPListItem RegistroExistente = SPContext.Current.Web.Lists["AltaProvisoria"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));

            if (RegistroExistente != null)
            {
                // REGISTRO NUEVO APROBADO
                if (RegistroExistente["Estado"].ToString() == "Aprobado" && (RegistroExistente["IdSap"] == null || string.IsNullOrEmpty(RegistroExistente["IdSap"].ToString())))
                {
                    Status = "NUEVO";
                    IdSAP = "";
                    if (RegistroExistente["Cuit"] != null && !string.IsNullOrEmpty(RegistroExistente["Cuit"].ToString()))
                    {
                        idProveedorSP = "";
                    }
                    else
                    {
                        idProveedorSP = RegistroExistente["CodigoExtranjero"].ToString();
                    }
                }
                //REGISTRO MODIFICADO Aprobado
                if (RegistroExistente["Estado"].ToString() == "Aprobado" && RegistroExistente["IdSap"] != null && RegistroExistente["EstadoSAP"].ToString() == "MODIFICADO")
                {
                    Status = "MODIFICADO";
                    IdSAP = RegistroExistente["IdSap"].ToString();
                    idProveedorSP = RegistroExistente["IdSap"].ToString();
                }
                //REGISTRO INHABILITADO 
                if (RegistroExistente["Estado"].ToString() == "Inhabilitado" && RegistroExistente["IdSap"] != null && RegistroExistente["EstadoSAP"].ToString() == "BLOQUEADO")
                {
                    Status = "BLOQUEADO";
                    IdSAP = RegistroExistente["IdSap"].ToString();
                    idProveedorSP = RegistroExistente["IdSap"].ToString();
                }
                //REGISTRO HABILITADO 
                if (RegistroExistente["Estado"].ToString() == "Aprobado" && RegistroExistente["IdSap"] != null && RegistroExistente["EstadoSAP"].ToString() == "DESBLOQUEADO")
                {
                    Status = "MODIFICADO";
                    IdSAP = RegistroExistente["IdSap"].ToString();
                    idProveedorSP = RegistroExistente["IdSap"].ToString();
                }
             
                if (RegistroExistente["Cuit"] != null && !string.IsNullOrEmpty(RegistroExistente["Cuit"].ToString()))
                {
               
                    GrupoCuenta = "YB01";
                    nIdentFis1 = RegistroExistente["Cuit"].ToString();
                    tipoNIF = "80";
                    GrupoTesoreria = "A1";
                    monedaPedido = "ARS";
                    claseImpuesto = RegistroExistente["CondImpIVA"].ToString().Substring(0, 2);
                    claseDistrib = RegistroExistente["IngresosBrutos"].ToString().Substring(0, 2);


                }
                else
                {

                    GrupoCuenta = "YBIV";
                    nIdentFis1 = "";
                    tipoNIF = "83";
                    GrupoTesoreria = "A2";
                    monedaPedido = "USD";
                    claseImpuesto = "08";
                    claseDistrib = "";

                }


                string QuerySTRRegion = "<View><Query><Where><Eq><FieldRef Name='DENOMINACION_x0020_REGION' /><Value Type='Text'>" + RegistroExistente["Provincia"].ToString() + "</Value></Eq></Where></Query></View>";
                SPQuery queryRegion = new SPQuery();
                queryRegion.ViewXml = QuerySTRRegion;
                SPListItemCollection ListaRegionessSap = SPContext.Current.Web.Lists["PaisesProvinciasSAP"].GetItems(queryRegion);

                if (ListaRegionessSap != null && ListaRegionessSap.Count > 0)
                {

                    region = (ListaRegionessSap[0]["CODIGO_x0020_REGION"] != null && !string.IsNullOrEmpty(ListaRegionessSap[0]["CODIGO_x0020_REGION"].ToString()) ? ListaRegionessSap[0]["CODIGO_x0020_REGION"].ToString() : "");
                }
               
                tratamiento = "";
                nombre = (RegistroExistente["RazonSocial"] != null && !string.IsNullOrEmpty(RegistroExistente["RazonSocial"].ToString()) ? RegistroExistente["RazonSocial"].ToString() : "");
                nombre2 = (RegistroExistente["NombreFantasia"] != null && !string.IsNullOrEmpty(RegistroExistente["NombreFantasia"].ToString()) ? RegistroExistente["NombreFantasia"].ToString() : "");
                conceptoB12 = (RegistroExistente["ActividadPrincipal"] != null && !string.IsNullOrEmpty(RegistroExistente["ActividadPrincipal"].ToString()) ? RegistroExistente["ActividadPrincipal"].ToString() : "");
                conceptoB2 = "";
                plantaEdificio = (RegistroExistente["Piso"] != null && !string.IsNullOrEmpty(RegistroExistente["Piso"].ToString()) ? RegistroExistente["Piso"].ToString() : "");
                nPiso = (RegistroExistente["Depto"] != null && !string.IsNullOrEmpty(RegistroExistente["Depto"].ToString()) ? RegistroExistente["Depto"].ToString() : "");
                calle = (RegistroExistente["Calle"] != null && !string.IsNullOrEmpty(RegistroExistente["Calle"].ToString()) ? RegistroExistente["Calle"].ToString() : "");
                numero = (RegistroExistente["Altura"] != null && !string.IsNullOrEmpty(RegistroExistente["Altura"].ToString()) ? RegistroExistente["Altura"].ToString() : "");
                codigoPostal = (RegistroExistente["CodPostal"] != null && !string.IsNullOrEmpty(RegistroExistente["CodPostal"].ToString()) ? RegistroExistente["CodPostal"].ToString() : "");
                poblacion = (RegistroExistente["Ciudad"] != null && !string.IsNullOrEmpty(RegistroExistente["Ciudad"].ToString()) ? RegistroExistente["Ciudad"].ToString() : "");

                string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='DENOMINACION_x0020_PAIS' /><Value Type='Text'>" + RegistroExistente["Pais"].ToString() + "</Value></Eq></Where></Query></View>";
                SPQuery query = new SPQuery();
                query.ViewXml = QuerySTR;
                SPListItemCollection ListaPaisesSap = SPContext.Current.Web.Lists["PaisesProvinciasSAP"].GetItems(query);
              
                if (ListaPaisesSap != null && ListaPaisesSap.Count > 0)
                {
                    pais = (ListaPaisesSap[0]["Title"] != null && !string.IsNullOrEmpty(ListaPaisesSap[0]["Title"].ToString()) ? ListaPaisesSap[0]["Title"].ToString() : "");
                }

                poblacion = (RegistroExistente["Ciudad"] != null && !string.IsNullOrEmpty(RegistroExistente["Ciudad"].ToString()) ? RegistroExistente["Ciudad"].ToString() : "");

                idioma = "ES";

              
                tel1 = (RegistroExistente["Telefono"] != null && !string.IsNullOrEmpty(RegistroExistente["Telefono"].ToString()) ? RegistroExistente["Telefono"].ToString() : "");
                tel2 = "";
                direccion = (RegistroExistente["VentasCorreo"] != null && !string.IsNullOrEmpty(RegistroExistente["VentasCorreo"].ToString()) ? RegistroExistente["VentasCorreo"].ToString() : "");
                direccion2 = (RegistroExistente["AdministracionCorreo"] != null && !string.IsNullOrEmpty(RegistroExistente["AdministracionCorreo"].ToString()) ? RegistroExistente["AdministracionCorreo"].ToString() : "");




                if (RegistroExistente["PersonaFisica"].ToString() == "SI")
                {
                    personaFisica = "X";
                }
                else
                {
                    personaFisica = "";
                }
                   ;
                claveBanco = "";
                nCuentaBancaria = "";
                pais2 = "";
                CuentaAsociada = "2110010";


                codigoActividad = "01";// VER ESTE,

                nExtension = "";
                motivoExencion = "";
                verifFraDupl = "X";
                viasdePago = "";
                notaInterior = "";
                paisRetencion = "";
                grupoEsqProveedor = "03";

                verifFacBaseEM = "X";
                telefonoProveedor = (RegistroExistente["Telefono"] != null && !string.IsNullOrEmpty(RegistroExistente["Telefono"].ToString()) ? RegistroExistente["Telefono"].ToString() : "");
                vendedorProveedor = (RegistroExistente["VentasContacto"] != null && !string.IsNullOrEmpty(RegistroExistente["VentasContacto"].ToString()) ? RegistroExistente["VentasContacto"].ToString() : "");
                pedidoAutoPermitido = "";
                facturaRelativaServicio = "X";








            }




            try
            {
                // string url = "http://192.168.11.105:50000/RESTAdapter/ActualizacionProveedores"; //DEV
                  string url = "http://192.168.11.115:50000/RESTAdapter/ActualizacionProveedores";//QAS
               // string url = "http://esap-5p:50000/RESTAdapter/ActualizacionProveedores";
                HttpMessageHandler handler = new HttpClientHandler();

                var httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url),
                    Timeout = new TimeSpan(0, 2, 0)
                };

                httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");

                //This is the key section you were missing    
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("portal_proveedores:Ieasa2022");//Ieasa.2023
                string val = System.Convert.ToBase64String(plainTextBytes);
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + val);
                string json2 = string.Empty;

                json2 = "{\"root\":{\"status\":\"" + Status + "\",\"username\":\"" + Usuario + "\",\"idProveedorSP\":\"" + idProveedorSP + "\",\"idSAP\":\"" + IdSAP + "\"," +
               "\"dataGeneral\":{\"GrupoCuenta\":\"" + GrupoCuenta + "\",\"tratamiento\":\"" + tratamiento + "\"," +
               "\"nombre\":\"" + nombre + "\",\"nombre2\":\"" + nombre2 + "\",\"conceptoB12\":\"" + conceptoB12 + "\",\"conceptoB2\":\"" + conceptoB2 + "\"," +
               "\"plantaEdificio\":\"" + plantaEdificio + "\",\"direccion\":{\"nPiso\":\"" + nPiso + "\",\"calle\":\"" + calle + "\"," +
               "\"numero\":\"" + numero + "\",\"codigoPostal\":\"" + codigoPostal + "\",\"poblacion\":\"" + poblacion + "\",\"pais\":\"" + pais + "\"," +
               "\"region\":\"" + region + "\"},\"idioma\":\"" + idioma + "\",\"contacto\":{\"tel1\":\"" + tel1 + "\",\"tel2\":\"" + tel2 + "\"," +
               "\"emails\":[{\"direccion\":\"" + direccion + "\"},{\"direccion\":\"" + direccion2 + "\"}]}," +
               "\"control\":{\"nIdentFis1\":\"" + nIdentFis1 + "\",\"tipoNIF\":\"" + tipoNIF + "\",\"claseImpuesto\":\"" + claseImpuesto + "\"," +
               "\"personaFisica\":\"" + personaFisica + "\"},\"Pagos\":{\"pais\":\"" + pais2 + "\",\"claveBanco\":\"" + claveBanco + "\"," +
               "\"nCuentaBancaria\":\"" + nCuentaBancaria + "\"}},\"Sociedad\":{\"gestionCuenta\":{\"CuentaAsociada\":\"" + CuentaAsociada + "\"," +
               "\"GrupoTesoreria\":\"" + GrupoTesoreria + "\",\"codigoActividad\":\"" + codigoActividad + "\",\"claseDistrib\":\"" + claseDistrib + "\"," +
               "\"nExtension\":\"" + nExtension + "\",\"motivoExencion\":\"" + motivoExencion + "\"}," +
               "\"pagosContabilidad\":{\"verifFraDupl\":\"" + verifFraDupl + "\",\"viasdePago\":\"" + viasdePago + "\"}," +
               "\"notaInterior\":\"" + notaInterior + "\",\"paisRetencion\":\"" + paisRetencion + "\"}," +
               "\"organizacionCompras\":{\"monedaPedido\":\"" + monedaPedido + "\",\"grupoEsqProveedor\":\"" + grupoEsqProveedor + "\"," +
               "\"verifFacBaseEM\":\"" + verifFacBaseEM + "\",\"telefonoProveedor\":\"" + telefonoProveedor + "\",\"vendedorProveedor\":\"" + vendedorProveedor + "\"," +
               "\"pedidoAutoPermitido\":\"" + pedidoAutoPermitido + "\",\"facturaRelativaServicio\":\"" + facturaRelativaServicio + "\"}}}";



                using (var response = httpClient.PostAsync(url, new StringContent(json2, System.Text.Encoding.UTF8, "application/json")).Result)
                {


                    lbSap.Text = response.Content.ReadAsStringAsync().Result;
                    if (response.Content.ReadAsStringAsync().Result.Contains("success"))
                    {
                        txtEstadoSAP.Text = "ACTUALIZADO";
                        if (RegistroExistente["Estado"].ToString() == "Aprobado" && (RegistroExistente["IdSap"] == null || string.IsNullOrEmpty(RegistroExistente["IdSap"].ToString())))
                        {
                            lbIdSap.Text = response.Content.ReadAsStringAsync().Result.ToString().Split(':')[1].Replace('"', ' ').Replace('}', ' ').Trim().Trim();
                        }
                        RegistroExistente["IdSap"] = lbIdSap.Text;
                        RegistroExistente["EstadoSAP"] = "ACTUALIZADO";
                        RegistroExistente["SAP"] = lbSap.Text;

                        RegistroExistente.Update();

                        SPAttachmentCollection collAttachments = RegistroExistente.Attachments;

                        string sTipoArchivo = string.Empty;

                        if (collAttachments.Count > 0)
                        {
                           //  string url2 = "http://192.168.11.105:50000/RESTAdapter/ActualizacionDocumentacionProveedores"; //dev
                            string url2 = "http://192.168.11.115:50000/RESTAdapter/ActualizacionDocumentacionProveedores";// QAS
                          //  string url2 = "http://esap-5p:50000/RESTAdapter/ActualizacionDocumentacionProveedores";
                            HttpMessageHandler handler2 = new HttpClientHandler();

                            var httpClient2 = new HttpClient(handler2)
                            {
                                BaseAddress = new Uri(url2),
                                Timeout = new TimeSpan(0, 2, 0)
                            };
                            httpClient2.DefaultRequestHeaders.Add("ContentType", "application/json");

                            //This is the key section you were missing    
                            var plainTextBytes2 = System.Text.Encoding.UTF8.GetBytes("PORTAL_PROVEEDORES:Ieasa2022");
                            string val2 = System.Convert.ToBase64String(plainTextBytes);
                            httpClient2.DefaultRequestHeaders.Add("Authorization", "Basic " + val);




                            foreach (string nombreArchivo in collAttachments)
                            {


                                string QuerySTRdOCS = "<View><Query><Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + nombreArchivo + "</Value></Eq></Where></Query></View>";
                                SPQuery querydOCS = new SPQuery();
                                querydOCS.ViewXml = QuerySTRdOCS;
                                SPListItemCollection Adjunto = SPContext.Current.Web.Lists["AltaProvisoriaAdjuntosSAP"].GetItems(querydOCS);
                                if (Adjunto == null || Adjunto.Count == 0)
                                {
                                    var attachmentFile = SPContext.Current.Web.GetFileByUrl(collAttachments.UrlPrefix + nombreArchivo);

                                    byte[] Bytes = attachmentFile.OpenBinary(SPOpenBinaryOptions.None);
                                    var Archivo = Convert.ToBase64String(Bytes);




                                    if (nombreArchivo.ToUpper().Contains("DDJJ DEL 202"))
                                    {
                                        sTipoArchivo = "Z_ACREED01";
                                    }
                                    else
                                    {
                                        sTipoArchivo = "Z_ACREED02";
                                    }






                                    //    string json2 = "\"root\":{\"GrupoCuenta\":\"YB01\",\"idProveedorSP\":\"0000035989\"\"nIdentFis1\":\"23325235349\",\"attachment\":{\"tipo\":\"Z_ACREED01\",\"filename\":\"PRUEBA.pdf\",\"extension\":\"PDF\",\"mimeType\":\"application/pdf\",\"stream\":\"" +
                                    //     ""+Archivo +"\"}}";

                                    string json3 = "{\"root\":{\"GrupoCuenta\":\"" + GrupoCuenta + "\",\"username\":\"" + Usuario + "\",\"idSAP\":\"" + lbIdSap.Text + "\",\"idProveedorSP\":\"" + lbIdSap.Text + "\",\"nIdentFis1\":\"" + nIdentFis1 + "\",\"attachment\":{\"tipo\":\"" + sTipoArchivo + "\",\"filename\":\"" + nombreArchivo + "\",\"extension\":\"PDF\",\"mimeType\":\"application/pdf\",\"stream\":\"" + Archivo + "\"}}}";

                                    try
                                    {
                                        using (var response2 = httpClient2.PostAsync(url2, new StringContent(json3, System.Text.Encoding.UTF8, "application/json")).Result)
                                        {
                                            lbSap.Text += "Procesamiento de adjuntos: " + "Nombre adjunto -" + nombreArchivo + "-" + response2.Content.ReadAsStringAsync().Result;
                                            SPListItem RegistroNuevoSap = SPContext.Current.Web.Lists["AltaProvisoriaAdjuntosSAP"].AddItem();
                                            RegistroNuevoSap["Title"] = nombreArchivo;
                                            RegistroNuevoSap["IdRegistro"] = RegistroExistente.ID;
                                            RegistroNuevoSap["Estado"] = "Enviado a SAP";
                                            RegistroNuevoSap["Eliminado"] = "NO";
                                            RegistroNuevoSap["MensajeSAp"] = response2.Content.ReadAsStringAsync().Result;
                                            RegistroNuevoSap.Update();



                                        }
                                    }
                                    catch (Exception er)
                                    {
                                        SPListItem RegistroNuevoSap = SPContext.Current.Web.Lists["AltaProvisoriaAdjuntosSAP"].AddItem();
                                        RegistroNuevoSap["Title"] = nombreArchivo;
                                        RegistroNuevoSap["IdRegistro"] = RegistroExistente.ID;
                                        RegistroNuevoSap["Estado"] = "NO enviado";
                                        RegistroNuevoSap["Eliminado"] = "NO";
                                        RegistroNuevoSap["MensajeSAp"] = er.Message;
                                        RegistroNuevoSap.Update();

                                    }
                                }
                            }

                        }
                    }
                    else
                    {

                        // REGISTRO NUEVO APROBADO
                        if (RegistroExistente["Estado"].ToString() == "Aprobado" && (RegistroExistente["IdSap"] == null || string.IsNullOrEmpty(RegistroExistente["IdSap"].ToString())))
                        {
                            txtEstadoSAP.Text = "NUEVO";
                            RegistroExistente["IdSap"] = "";
                            RegistroExistente["EstadoSAP"] = "NUEVO";
                            RegistroExistente["SAP"] = lbSap.Text;
                            lbIdSap.Text = "";
                            RegistroExistente.Update();
                        }
                        //REGISTRO MODIFICADO Aprobado
                        if (RegistroExistente["Estado"].ToString() == "Aprobado" && RegistroExistente["IdSap"] != null && RegistroExistente["EstadoSAP"].ToString() == "MODIFICADO")
                        {
                            txtEstadoSAP.Text = "MODIFICADO";

                            RegistroExistente["EstadoSAP"] = "MODIFICADO";
                            RegistroExistente["SAP"] = lbSap.Text;

                            RegistroExistente.Update();
                        }

                    }
                }





            }
            catch (Exception ex)
            {
                lbSap.Text += "error" + ex.Message.ToString();
            }
        }

        protected void btnCerrar_ServerClick(object sender, EventArgs e)
        {
            if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("COMPRAS")))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "cerrarCompras();", true);
            }
            else
            {
                if (Request.QueryString["ID"] != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "cerrarHome();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "cerrarAnonimo();", true);
                }
            }
        }
        protected void btnEliminar_ServerClick(object sender, EventArgs e)
        {
            lbMensaje.Text = "";
            SPListItem Proceso = SPContext.Current.Web.Lists["AltaProvisoria"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
            bool bEntro = false;
            foreach (ListItem x in chkBoxList.Items)
            {
                if (x.Selected == true)
                {
                    Proceso.Attachments.Delete(x.Value);
                    Proceso.Update();
                    lbMensaje.Text = "";
                    bEntro = true;

                    string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + x.Value + "</Value></Eq></Where></Query></View>";
                    SPQuery query = new SPQuery();
                    query.ViewXml = QuerySTR;
                    string sHtml = string.Empty;
                    SPListItemCollection ListaAux = SPContext.Current.Web.Lists["AltaProvisoriaAdjuntosSAP"].GetItems(query);
                    if (ListaAux.Count > 0)
                    {
                        SPListItem adjunto = ListaAux[0];
                        adjunto["Eliminado"] = "SI";
                        adjunto.Update();


                    }
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
        protected void cboUsuarioBloqueado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sMensajeModal = string.Empty;
            string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='NombreUsuario' /><Value Type='Text'>" + txtRazonSocial.Text + "</Value></Eq></Where></Query></View>";
            SPQuery query = new SPQuery();
            query.ViewXml = QuerySTR;
            SPListItemCollection ListaAux = SPContext.Current.Web.Lists["AltaProvisoriaUsuarioBloqueado"].GetItems(query);
            if (cboUsuarioBloqueado.SelectedValue == "SI")
            {
                SPListItem RegistroExistente = SPContext.Current.Web.Lists["AltaProvisoria"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                RegistroExistente["Estado"] = "Inhabilitado";
                RegistroExistente["EstadoSAP"] = "BLOQUEADO";
                RegistroExistente.Update();
                if (ListaAux.Count > 0)
                {
                    SPListItem itemUsuario = ListaAux[0];
                    itemUsuario["UsuarioBloqueado"] = "SI";
                    itemUsuario.Update();
                }
                else
                {
                    SPListItem UsuarioBloqueado = SPContext.Current.Web.Lists["AltaProvisoriaUsuarioBloqueado"].AddItem();
                    UsuarioBloqueado["Title"] = "Usuario bloqueado";
                    UsuarioBloqueado["UsuarioBloqueado"] = "SI";
                    UsuarioBloqueado["NombreUsuario"] = txtRazonSocial.Text;
                    UsuarioBloqueado.Update();
                }
                sMensajeModal = "Usuario bloqueado correctamente";
                if (chkSAP.Checked == true)
                {
                    PasarSAP();
                }
            }
            if (cboUsuarioBloqueado.SelectedValue == "NO")
            {
                SPListItem RegistroExistente = SPContext.Current.Web.Lists["AltaProvisoria"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                RegistroExistente["Estado"] = "Aprobado";

                RegistroExistente["EstadoSAP"] = "DESBLOQUEADO";
                RegistroExistente.Update();

                if (ListaAux.Count > 0)
                {
                    SPListItem itemUsuario = ListaAux[0];
                    itemUsuario["UsuarioBloqueado"] = "NO";
                    itemUsuario.Update();
                }
                else
                {
                    SPListItem UsuarioBloqueado = SPContext.Current.Web.Lists["AltaProvisoriaUsuarioBloqueado"].AddItem();
                    UsuarioBloqueado["Title"] = "Usuario bloqueado";
                    UsuarioBloqueado["UsuarioBloqueado"] = "NO";
                    UsuarioBloqueado["NombreUsuario"] = txtRazonSocial.Text;
                    UsuarioBloqueado.Update();
                }
                sMensajeModal = "Usuario desbloqueado correctamente";
            }
            CargarInicial();
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeCompra('" + sMensajeModal + "');", true);
        }
        protected void CargarProvincias()
        {

            SPQuery queryProvincias = new SPQuery();
            string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='DENOMINACION_x0020_PAIS' /><Value Type='Text'>" + cboPais.SelectedItem.Text + "</Value></Eq></Where><OrderBy><FieldRef Name='DENOMINACION_x0020_REGION' Ascending='True' /></OrderBy></Query></View>";

            queryProvincias.ViewXml = QuerySTR;

            SPListItemCollection PaisesProvinciasSAP = SPContext.Current.Web.Lists["PaisesProvinciasSAP"].GetItems(queryProvincias);
            if (PaisesProvinciasSAP.Count > 0)
            {
                DataView view1 = new DataView(PaisesProvinciasSAP.GetDataTable());
                DataTable distinctValues1 = view1.ToTable(true, "DENOMINACION_x0020_REGION");
                DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
                rFilaSeleccioneSolicitud["DENOMINACION_x0020_REGION"] = strSeleccione;
                distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
                cboProvincia.DataSource = distinctValues1;
                cboProvincia.DataValueField = "DENOMINACION_x0020_REGION";
                cboProvincia.DataTextField = "DENOMINACION_x0020_REGION";
                cboProvincia.DataBind();
            }
        }
        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProvincias();
        }
    }
}
