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
namespace Ieasa.Layouts.Ieasa
{
    public partial class AltaProvisoria : UnsecuredLayoutsPageBase
    {
        //private string sSitio = "http://ibiza:2222/sites/formularios";
        private string sSitio = "https://proveedores-desa.ieasa.com.ar";
        //private string sSitio = "https://portalproveedores.ieasa.com.ar";
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
                menuConID.Visible = true;
                menuSinID.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "divBlock();", true);
                string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='NombreUsuario' /><Value Type='Text'>" + AltaProv["NombreFantasia"].ToString() + "</Value></Eq></Where></Query></View>";
                SPQuery query = new SPQuery();
                query.ViewXml = QuerySTR;
                SPListItemCollection ListaAux = SPContext.Current.Web.Lists["AltaProvisoriaUsuarioBloqueado"].GetItems(query);
                if(ListaAux.Count > 0){
                    if (ListaAux[0]["UsuarioBloqueado"].ToString()=="SI")
                    BloqueaCampos();
                }
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
            LtPestañaCompras.Text = "<li class='nav-item bg-light'>"+
                                      "<a class='nav-link active' data-toggle='pill' id='pestaniaDatosGrales' href='#home'>Datos Generales</a>"+
                                    "</li>"+
                                    "<li class='nav-item bg-light'>" +
                                      "<a class='nav-link' data-toggle='pill' id='pestaniaRubros' href='#rubros'>Rubros</a>" +
                                    "</li>" +
                                    "<li class='nav-item bg-light' >"+
                                      "<a class='nav-link' data-toggle='pill' id='pestaniaDatosContacto' href='#menu1'>Datos Contacto</a>"+
                                    "</li>"+
                                    "<li class='nav-item bg-light'>"+
                                      "<a class='nav-link' data-toggle='pill' id='pestaniaDatosImpositivos' href='#menu2'>Datos Impositivos</a>"+
                                    "</li>"+
                                    
                                    "<li class='nav-item bg-light'>"+
                                      "<a class='nav-link' data-toggle='pill' href='#menu3'>Adjuntos</a>"+
                                    "</li>"+
                                    "<li class='nav-item bg-light'>" +
                                        "<a class='nav-link' data-toggle='pill' href='#sap'>SAP</a>" +
                                    "</li>"+
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
                if(ListaAux.Count > 0){
                    cboUsuarioBloqueado.SelectedValue = ListaAux[0]["UsuarioBloqueado"].ToString();
                }
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
            if (AltaProv["Pais"].ToString() == "Argentina")
            {
                cboProvincia.SelectedValue = (AltaProv["ProvinciaArg"] != null && !string.IsNullOrEmpty(AltaProv["ProvinciaArg"].ToString()) ? AltaProv["ProvinciaArg"].ToString() : String.Empty);
            }
            else
            {
                txtProvincia.Text = (AltaProv["Provincia"] != null && !string.IsNullOrEmpty(AltaProv["Provincia"].ToString()) ? AltaProv["Provincia"].ToString() : String.Empty);
            }
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
            if (AltaProv["SAP"] != null && !string.IsNullOrEmpty(AltaProv["SAP"].ToString()))
            {
                if (AltaProv["SAP"].ToString() == "SI")
                {
                    chkSAP.Checked = true;
                }
                else
                {
                    chkSAP.Checked = false;
                }
            }
            if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("COMPRAS")))
            {
                btnEliminar.Visible = true;
            }
            else
            {
                btnEliminar.Visible = false;
            }
            if (AltaProv["Estado"].ToString() == "Aprobado" || AltaProv["Estado"].ToString() == "Rechazado")
            {
             
                txtObsCompras.ReadOnly = true;
                btnAprobarCompras.Disabled = true;
                btnRechazarCompras.Disabled = true;
            }
           
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
           
            if (sopcion != string.Empty &&  Combo.Items.FindByText(sopcion) ==null)
            {
             DataTable dtDatos= (DataTable)Combo.DataSource;
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
            string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='IDRegistro' /><Value Type='Text'>" + iIdFormulario.ToString() + "</Value></Eq></Where></Query></View>";
            SPQuery query = new SPQuery();
            query.ViewXml = QuerySTR;
            SPListItemCollection ListaAux = SPContext.Current.Web.Lists["AltaProvisoriaRubros"].GetItems(query);
            if (ListaAux.Count > 0)
            {
                foreach (SPListItem item in ListaAux)
                {
                    libServicio.Items.Add(item["Title"].ToString());
                }
            }
        }
        private void CargarSociedad(int iIdFormulario)
        {
            string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='IDRegistro' /><Value Type='Text'>" + iIdFormulario.ToString() + "</Value></Eq></Where></Query></View>";
            SPQuery query = new SPQuery();
            query.ViewXml = QuerySTR;
            SPListItemCollection ListaAux = SPContext.Current.Web.Lists["AltaProvisoriaSociedad"].GetItems(query);
            if (ListaAux.Count > 0)
            {
                foreach (SPListItem item in ListaAux)
                {
                    listSociedad.Items.Add(item["Title"].ToString());
                }
            }
        }
        private void BloqueaCampos()
        {
            if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("COMPRAS")))
            {
               
            }
            else
            {
              
                btnGuardarID.Visible = false;
                //Bloqueo Pestaña 1
                txtRazonSocial.ReadOnly = true;
                txtNomFantasia.ReadOnly = true;
                cboPersoneria.Enabled = false;
                txtActPrinc.ReadOnly = true;
                txtSitioWeb.ReadOnly = true;
                comboRubros.Style.Add("display", "none");
                btnAQ.Style.Add("display", "none");
                divListBox.Attributes.Add("class", "col-12");
                cboPais.Enabled = false;
                txtProvincia.ReadOnly = true;
                cboProvincia.Enabled = false;
                txtCiudad.ReadOnly = true;
                txtCodPostal.ReadOnly = true;
                txtCalle.ReadOnly = true;
                txtAltura.ReadOnly = true;
                txtDepto.ReadOnly = true;
                txtPiso.ReadOnly = true;

                //Bloqueo Pestaña 2
                txtApoderado.ReadOnly = true;
                txtVentasContacto.ReadOnly = true;
                txtVentasCorreo.ReadOnly = true;
                txtAdministracionContacto.ReadOnly = true;
                txtAdministracionCorreo.ReadOnly = true;
                txtTelefono.ReadOnly = true;
                txtFax.ReadOnly = true;

                //Bloqueo Pestaña 3
                txtCuit.ReadOnly = true;
                cboConImpG.Enabled = false;
                cboCondImIVA.Enabled = false;
                cboIngresosBrutos.Enabled = false;
                txtIngresosBrutos.ReadOnly = true;
            }
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
                delegate()
                {
                    using (SPSite site = new SPSite(sSitio))
                    {

                        
                        SPQuery query = new SPQuery();
                        string QuerySTR = "<View>" +
                            "<Query><Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>Pais</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Activo</Value></Eq></And></Where></Query>" +
                                "</View>";
                        query.ViewXml = QuerySTR;
                        SPListItemCollection Pais = site.OpenWeb().Lists["AltaProvisoria_Aux"].GetItems(query);
                        if (Pais.Count > 0)
                        {
                            DataView view1 = new DataView(Pais.GetDataTable());
                            DataTable distinctValues1 = view1.ToTable(true, "Valor");
                            DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
                            rFilaSeleccioneSolicitud["Valor"] = strSeleccione;
                            distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
                            cboPais.DataSource = distinctValues1;
                            cboPais.DataValueField = "Valor";
                            cboPais.DataTextField = "Valor";
                            cboPais.DataBind();
                        }

                        SPQuery queryProv = new SPQuery();
                        string QuerySTRProv = "<View>" +
                            "<Query><Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>Provincia</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Activo</Value></Eq></And></Where><OrderBy><FieldRef Name='Valor' Ascending='True' /></OrderBy></Query>"+
                                "</View>";
                        queryProv.ViewXml = QuerySTRProv;
                        SPListItemCollection Provincia = site.OpenWeb().Lists["AltaProvisoria_Aux"].GetItems(queryProv);
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
                        SPQuery querySociedad = new SPQuery();
                        string QuerySTRSociedad = "<View>" +
                            "<Query><Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>Sociedad</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Activo</Value></Eq></And></Where></Query>" +
                                "</View>";
                        querySociedad.ViewXml = QuerySTRSociedad;
                        SPListItemCollection Sociedad = site.OpenWeb().Lists["AltaProvisoria_Aux"].GetItems(querySociedad);
                        if (Sociedad.Count > 0)
                        {
                            cboSociedad.Items.Add(new ListItem(strSeleccione));
                            foreach (SPListItem item in Sociedad)
                            {
                                cboSociedad.Items.Add(item["Valor"].ToString());
                            }
                        }
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
            libServicio.Items.Add(cboRubro.SelectedValue);
                libServicio.DataBind();
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
        protected void btoMasSociedad_Click(object sender, EventArgs e)
        {
            listSociedad.Items.Add(cboSociedad.SelectedValue);
            listSociedad.DataBind();
        }
        protected void btoMenosSociedad_Click(object sender, EventArgs e)
        {
            listSociedad.Items.Remove(listSociedad.SelectedItem);
            listSociedad.DataBind();
        }
        protected void btnValidoRubros(object sender, EventArgs e)
        {
            if(libServicio.Items.Count < 1){
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "validaCamposRubros(1);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "validaCamposRubros(0);", true);
            }
                
        }

        private int validarEmpresaRegistrada()
        {
            string CorreoVentas = txtVentasCorreo.Text;
            string QuerySTR = "<View><Query><Where><Or><Contains><FieldRef Name='VentasCorreo' /><Value Type='Text'>" + CorreoVentas.Split('@')[1] + "</Value></Contains><Or><Eq><FieldRef Name='RazonSocial' /><Value Type='Text'>" + txtNomFantasia.Text + "</Value></Eq><Or><Eq><FieldRef Name='SitioWeb' /><Value Type='Text'>" + txtSitioWeb.Text + "</Value></Eq><And><Eq><FieldRef Name='Calle' /><Value Type='Text'>" + txtCalle.Text + "</Value></Eq><Eq><FieldRef Name='Altura' /><Value Type='Text'>" + txtAltura.Text + "</Value></Eq></And></Or></Or></Or></Where></Query></View>";
            SPQuery query = new SPQuery();
            query.ViewXml = QuerySTR;

            string QuerySTRNacional = "<View><Query><Where><Eq><FieldRef Name='Cuit' /><Value Type='Text'>"+ txtCuit.Text +"</Value></Eq></Where></Query></View>";
            SPQuery queryNacional = new SPQuery();
            queryNacional.ViewXml = QuerySTRNacional;
            

            SPListItemCollection ListaAux = null;
            if (cboProveedor.SelectedValue.ToString() == "Extranjero")
            {
                if (Request.QueryString["AN"] == null)
                {
                    ListaAux = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(query);
                }
                else
                {
                    SPSecurity.RunWithElevatedPrivileges(
                    delegate()
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
                    delegate()
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
                                SPListItem RegistroNuevo = web.Lists["AltaProvisoria"].AddItem();
                                GuardarDatos(RegistroNuevo);
                                RegistroNuevo.Update();
                                string sCorreo = "<table style='height: 160px;' border='0' width='606' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                                                    "<tr>" +
                                                    "<td bgcolor='#0a4e9a'><img src='" + SPContext.Current.Web.Url + "/_layouts/15/Ieasa/img/Captura.PNG' alt='Logo IEASA' width='606'></td>" +
                                                    "</tr>" +
                                                    "<tr>" +
                                                        "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                                            "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>" +
                                                            "Estimado/a: " + txtRazonSocial.Text + "" +

                                                            "<br>Su información fue recibida y será analizada" +

                                                            "</p>" +
                                                    "</td>" +
                                                    "</tr>" +
                                                    "<tr>" +
                                                        "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                                            "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Saludos</p>" +
                                                        "</td>" +
                                                    "</tr>" +
                                                "</tbody></table>";
                                SPUtility.SendEmail(SPContext.Current.Web, false, false, txtVentasCorreo.Text, "Alta Portal Proveedores", sCorreo);

                                string sCorreoRegistro = "<table style='height: 160px;' border='0' width='606' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                                                                "<tr>" +
                                                                "<td bgcolor='#0a4e9a'><img src='" + SPContext.Current.Web.Url + "/_layouts/15/Ieasa/img/Captura.PNG' alt='Logo IEASA' width='606'></td>" +
                                                                "</tr>" +
                                                                "<tr>" +
                                                                    "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                                                        "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>" +
                                                                        "La empresa: " + txtNomFantasia.Text + " ha efectuado su inscripción en el portal de proveedores de IEASA el día " + DateTime.Today.ToShortDateString() + ", la información se encuentra disponible para su análisis." +
                                                                        "</p>" +
                                                                "</td>" +
                                                                "</tr>" +
                                                                "<tr>" +
                                                                    "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                                                        "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Saludos</p>" +
                                                                    "</td>" +
                                                                "</tr>" +
                                                            "</tbody></table>";
                                SPUtility.SendEmail(SPContext.Current.Web, false, false, "registroproveedores@ieasa.com.ar; lflores@ieasa.com.ar; dbruno@ieasa.com.ar", "Alta Portal Proveedores", sCorreoRegistro);
                                //                            SPUtility.SendEmail(SPContext.Current.Web, false, false, "cgperez@nacionservicios.com.ar; gus.rnr@gmail.com", "Alta Portal Proveedores", sCorreoRegistro);

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
                    }
                    else
                    {

                        SPSecurity.RunWithElevatedPrivileges(
                        delegate()
                        {
                            using (SPSite site = new SPSite(sSitio))
                            {
                                using (SPWeb web = site.OpenWeb())
                                {
                                    site.AllowUnsafeUpdates = true;
                                    web.AllowUnsafeUpdates = true;

                                    //SPListItem RegistroNuevo = SPContext.Current.Web.Lists["AltaProvisoria"].AddItem(); //(Ibiza)
                                    SPListItem RegistroNuevo = web.Lists["AltaProvisoria"].AddItem();
                                    GuardarDatos(RegistroNuevo);
                                    RegistroNuevo.Update();
                                    string sCorreo = "<table style='height: 160px;' border='0' width='606' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                                                        "<tr>" +
                                                        "<td bgcolor='#0a4e9a'><img src='" + SPContext.Current.Web.Url + "/_layouts/15/Ieasa/img/Captura.PNG' alt='Logo IEASA' width='606'></td>" +
                                                        "</tr>" +
                                                        "<tr>" +
                                                            "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                                                "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>" +
                                                                "Estimado/a: " + txtRazonSocial.Text + "" +

                                                                "<br>Su información fue recibida y será analizada" +

                                                                "</p>" +
                                                        "</td>" +
                                                        "</tr>" +
                                                        "<tr>" +
                                                            "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                                                "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Saludos</p>" +
                                                            "</td>" +
                                                        "</tr>" +
                                                    "</tbody></table>";
                                    SPUtility.SendEmail(SPContext.Current.Web, false, false, txtVentasCorreo.Text, "Alta Portal Proveedores", sCorreo);

                                    string sCorreoRegistro = "<table style='height: 160px;' border='0' width='606' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                                                                    "<tr>" +
                                                                    "<td bgcolor='#0a4e9a'><img src='" + SPContext.Current.Web.Url + "/_layouts/15/Ieasa/img/Captura.PNG' alt='Logo IEASA' width='606'></td>" +
                                                                    "</tr>" +
                                                                    "<tr>" +
                                                                        "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                                                            "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>" +
                                                                            "La empresa: " + txtNomFantasia.Text + " ha efectuado su inscripción en el portal de proveedores de IEASA el día " + DateTime.Today.ToShortDateString() + ", la información se encuentra disponible para su análisis." +
                                                                            "</p>" +
                                                                    "</td>" +
                                                                    "</tr>" +
                                                                    "<tr>" +
                                                                        "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                                                            "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Saludos</p>" +
                                                                        "</td>" +
                                                                    "</tr>" +
                                                                "</tbody></table>";
                                    SPUtility.SendEmail(SPContext.Current.Web, false, false, "registroproveedores@ieasa.com.ar; lflores@ieasa.com.ar; dbruno@ieasa.com.ar", "Alta Portal Proveedores", sCorreoRegistro);
                                    //SPUtility.SendEmail(SPContext.Current.Web, false, false, "cgperez@nacionservicios.com.ar; gus.rnr@gmail.com", "Alta Portal Proveedores", sCorreoRegistro);

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
                delegate()
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
            if (listSociedad.Items.Count > 0)
            {
                for (int i = 0; i < listSociedad.Items.Count; i++)
                {
                    SPListItem Rubro = SPContext.Current.Web.Lists["AltaProvisoriaSociedad"].AddItem();
                    Rubro["Title"] = listSociedad.Items[i].ToString();
                    Rubro["IDRegistro"] = p;
                    Rubro.Update();
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
                    CorreoComprasModificacion(Request.QueryString["ID"]);
                }
            }
            else
            {
                Registro["Estado"] = "Pendiente";
            }
            //Datos Generales
            Registro["NombreFantasia"] = (txtRazonSocial.Text != null && !string.IsNullOrEmpty(txtRazonSocial.Text) ? txtRazonSocial.Text : String.Empty);
            Registro["RazonSocial"] = (txtNomFantasia.Text != null && !string.IsNullOrEmpty(txtNomFantasia.Text) ? txtNomFantasia.Text : String.Empty);
            Registro["Personeria"] = cboPersoneria.SelectedItem.Text;
            Registro["ActividadPrincipal"] = (txtActPrinc.Text != null && !string.IsNullOrEmpty(txtActPrinc.Text) ? txtActPrinc.Text : String.Empty);
            Registro["SitioWeb"] = (txtSitioWeb.Text != null && !string.IsNullOrEmpty(txtSitioWeb.Text) ? txtSitioWeb.Text : String.Empty);
            Registro["Pais"] = cboPais.SelectedItem.Text;
            if (cboPais.SelectedValue == "Argentina")
            {
                Registro["ProvinciaArg"] = cboProvincia.SelectedItem.Text;
            }
            else
            {
                Registro["Provincia"] = (txtProvincia.Text != null && !string.IsNullOrEmpty(txtProvincia.Text) ? txtProvincia.Text : String.Empty);
            }
            Registro["Ciudad"] = (txtCiudad.Text != null && !string.IsNullOrEmpty(txtCiudad.Text) ? txtCiudad.Text : String.Empty);
            Registro["CodPostal"] = (txtCodPostal.Text != null && !string.IsNullOrEmpty(txtCodPostal.Text) ? txtCodPostal.Text : String.Empty);
            Registro["Calle"] = (txtCalle.Text != null && !string.IsNullOrEmpty(txtCalle.Text) ? txtCalle.Text : String.Empty);
            Registro["Altura"] = (txtAltura.Text != null && !string.IsNullOrEmpty(txtAltura.Text) ? txtAltura.Text : String.Empty);
            Registro["Depto"] = (txtDepto.Text != null && !string.IsNullOrEmpty(txtDepto.Text) ? txtDepto.Text : String.Empty);
            Registro["Piso"] = (txtPiso.Text != null && !string.IsNullOrEmpty(txtPiso.Text) ? txtPiso.Text : String.Empty);
          
            if (fuPersoneriaJur.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fuPersoneriaJur.PostedFiles)
                {
                    Stream fs = fuPersoneriaJur.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "Personería Jurídica" + System.IO.Path.GetExtension(fuPersoneriaJur.FileName);
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
            if(chkPersFis.Checked==true){
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

                    string fileName = "Certificado Exención Impositiva" + " - " + g.ToString() + System.IO.Path.GetExtension(uploadedFile.FileName);
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
                Registro["SAP"] = "SI";
            }
            else
            {
                Registro["SAP"] = "NO";
            }
            
        }
        private void CorreoComprasModificacion(string p)
        {
            string sCorreoComprasModificacion = "<table style='height: 160px;' border='0' width='606' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                                                            "<tr>" +
                                                            "<td bgcolor='#0a4e9a'><img src='" + SPContext.Current.Web.Url + "/_layouts/15/Ieasa/img/Captura.PNG' alt='Logo IEASA' width='606'></td>" +
                                                            "</tr>" +
                                                            "<tr>" +
                                                                "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                                                    "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>" +
                                                                    "El proveedor " + txtNomFantasia.Text + " ha efectuado una modificación en su legajo, ingresar al legajo del proveedor y aceptar las modificaciones efectuadas." +
                                                                    "<br><br><a href='" + SPContext.Current.Web.Url + "/_layouts/15/Ieasa/AltaProvisoria.aspx?ID=" + p + "'>Ir al legajo</a>" +
                                                                    "</p>" +
                                                            "</td>" +
                                                            "</tr>" +
                                                            "<tr>" +
                                                                "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                                                    "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Saludos</p>" +
                                                                "</td>" +
                                                            "</tr>" +
                                                        "</tbody></table>";
            SPUtility.SendEmail(SPContext.Current.Web, false, false, "registroproveedores@ieasa.com.ar", "Alta Portal Proveedores", sCorreoComprasModificacion);
                            
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
            SPSecurity.RunWithElevatedPrivileges(delegate()
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
                SPSecurity.RunWithElevatedPrivileges(delegate()
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

                            if(RegistroExistente["Proveedor"].ToString() == "Extranjero")
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
                            group.AddUser(Utils.EncodeUsername(user.UserName),user.Email,RegistroExistente["NombreFantasia"].ToString(),"");
                            group.Update();
                            string sCorreo = "<table style='height: 160px;' border='0' width='606' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                          "<tr>" +
                            "<td bgcolor='#0a4e9a'><img src='" + SPContext.Current.Web.Url + "/_layouts/15/Ieasa/img/Captura.PNG' alt='Logo IEASA' width='606'></td>" +
                          "</tr>" +
                          "<tr>" +
                                "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                    "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>" +
                                    "Estimado/a:" +
                                    "<br>Le confirmamos que ha sido dado de alta como proveedor de IEASA, para visualizar y/o modificar su información institucional lo podrá efectuar accediendo al siguiente link con el nombre de Usuario y Contraseña indicados a continuación:" +

                                    "<br><a href='" + SPContext.Current.Web.Url + "'>Ir al sitio </a>" +
                                    "<br>Usuario: " + ((RegistroExistente["Cuit"]!=null) ? RegistroExistente["Cuit"].ToString() : sCodigoExtranjero) +
                                   "<br>Password: " + sPasword +
                                   "<br>En caso de comentarios sobre vuestro perfil, podrá contactarnos en la dirección: registroproveedores@ieasa.com.ar" +
                                    "</p>" +
                            "</td>" +
                          "</tr>" +
                          "<tr>" +
                              "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                  "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Saludos</p>" +
                                  "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Proveedores - IEASA</p>" +
                                "</td>" +
                          "</tr>" +
                    "</tbody></table>";
                            SPUtility.SendEmail(SPContext.Current.Web, false, false, RegistroExistente["VentasCorreo"].ToString().Trim(), "Alta Portal Proveedores", sCorreo);
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
                            MembershipUser userMembership =    Utils.BaseMembershipProvider().GetUser((RegistroExistente["Cuit"] != null ? SPContext.Current.Web.EnsureUser(RegistroExistente["Cuit"].ToString()) : SPContext.Current.Web.EnsureUser(RegistroExistente["CodigoExtranjero"].ToString())), false);
                            userMembership.Email = RegistroExistente["VentasCorreo"].ToString().Trim();
                            Utils.BaseMembershipProvider().UpdateUser(userMembership);
                           // user.Email = RegistroExistente["VentasCorreo"].ToString().Trim();
                            Usuario = ((RegistroExistente["Cuit"]!= null) ? SPContext.Current.Web.EnsureUser(RegistroExistente["Cuit"].ToString()) : SPContext.Current.Web.EnsureUser(RegistroExistente["CodigoExtranjero"].ToString()));
                            Usuario.Email = RegistroExistente["VentasCorreo"].ToString().Trim();
                            Usuario.Update();
                        }
                        catch (Exception EX)
                        {
                            txtActPrinc.Text = EX.ToString();
                        }
                    }
                    EnviarCorreoAprobado(RegistroExistente);
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

                            UsuarioCreador =  SPContext.Current.Web.EnsureUser(RegistroExistente["Cuit"].ToString());


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
        }
        private void EnviarCorreoAprobado(SPListItem RegistroExistente)
        {
            string sCorreoCC = "<table style='height: 160px;' border='0' width='606' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                                    "<tr>" +
                                        "<td bgcolor='#0a4e9a'><img src='" + SPContext.Current.Web.Url + "/_layouts/15/Ieasa/img/Logo.png' alt='Logo IEASA' width='106'></td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                            "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>" +
                                                "La solicitud fue aprobada" +
                                                "<br><br><a href='" + SPContext.Current.Web.Url + "/_layouts/15/Ieasa/AltaProvisoria.aspx?ID=" + RegistroExistente.ID + "'>Ir al registro " + RegistroExistente.ID + "</a>" +
                                            "</p>" +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                            "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Saludos</p>" +
                                        "</td>" +
                                    "</tr>" +
                                "</tbody></table>";


            SPUtility.SendEmail(SPContext.Current.Web, false, false, RegistroExistente["VentasCorreo"].ToString(), "Registro aprobado ", sCorreoCC);
       
        }
        protected void btnRechazarCompras_ServerClick(object sender, EventArgs e)
        {
            SPListItem RegistroExistente = SPContext.Current.Web.Lists["AltaProvisoria"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
            RegistroExistente["ObservacionesCompras"] = (txtObsCompras.Text != null && !string.IsNullOrEmpty(txtObsCompras.Text) ? txtObsCompras.Text : String.Empty);
            RegistroExistente["Estado"] = "Rechazado";
            RegistroExistente.Update();
            EnviarCorreoRechazado(RegistroExistente);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeModificado(2);", true);
        }
        private void EnviarCorreoRechazado(SPListItem RegistroExistente)
            .replace(''
        {
            string sCorreoCC = "<table style='height: 160px;' border='0' width='606' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                                    "<tr>" +
                                        "<td bgcolor='#0a4e9a'><img src='" + SPContext.Current.Web.Url + "/_layouts/15/Ieasa/img/Logo.png' alt='Logo IEASA' width='106'></td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                            "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>" +
                                                "Estimado/a:" +
                                                "<br>Le informamos que no hemos procesado su solicitud de alta como proveedor en nuestra empresa debido a errores formales en la presentación de la información" +
                                                "<br>Si considera que el siguient mensaje es un error, podrá contactarnos en la dirección: registroproveedores@ieasa.com.ar" +
                                                "<br>Saludos" +
                                            "</p>" +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                            "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Saludos</p>" +
                                            "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Proveedores - IEASA</p>" +
                                        "</td>" +
                                    "</tr>" +
                                "</tbody></table>";
            SPUtility.SendEmail(SPContext.Current.Web, false, false, RegistroExistente["VentasCorreo"].ToString(), "Registro rechazado ", sCorreoCC);
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
                RegistroExistente["Estado"] = "Bloqueado";
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
                
            }
            if (cboUsuarioBloqueado.SelectedValue == "NO")
            {
                SPListItem RegistroExistente = SPContext.Current.Web.Lists["AltaProvisoria"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                RegistroExistente["Estado"] = "Aprobado";
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeCompra('" + sMensajeModal + "');", true);
        }
    }
}
