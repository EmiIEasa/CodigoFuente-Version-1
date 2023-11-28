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
using Microsoft.SharePoint.IdentityModel;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.Security;
using IEasa.Sharepoint.FormsBasedAuthentication;


namespace SegFact.Layouts1.SegFact
{
    public partial class Registro : LayoutsPageBase
    {
        private string sSitioAnonimo= "https://proveedores-an.energia-argentina.com.ar/";
        string strSeleccione = "Seleccione";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                CargarCombos();
                CargarInicial();
                CargaPestañas();
            }
        }

        private void CargaPestañas()
        {
            bool bPerteneceIeasa = false;
            if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("CONTABILIDAD")))
            {
                bPerteneceIeasa = true;
            }
            if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("TESORERIA")))
            {
                bPerteneceIeasa = true;
            }
            
            if (bPerteneceIeasa == true)
            {
                LtPestañas.Text = "<li class='nav-item bg-light'>"+
                                      "<a class='nav-link active' data-toggle='pill' href='#DatosGenerales'>Datos Generales</a>"+
                                    "</li>"+
                                    "<li class='nav-item bg-light'>"+
                                      "<a class='nav-link' data-toggle='pill' href='#Contabilidad'>Contabilidad</a>" +
                                    "</li>"+
                                    "<li class='nav-item bg-light'>"+
                                      "<a class='nav-link' data-toggle='pill' href='#Tesorería'>Tesorería</a>" +
                                    "</li>"+
                                    "<li class='nav-item bg-light'>"+
                                      "<a class='nav-link' data-toggle='pill' href='#Historial'>Historial</a>" +
                                    "</li>";
            }
            else {
                LtPestañas.Text = "<li class='nav-item bg-light'>" +
                                          "<a class='nav-link active' data-toggle='pill' href='#DatosGenerales'>Datos Generales</a>" +
                                        "</li>" +
                                        "<li class='nav-item bg-light'>" +
                                          "<a class='nav-link' data-toggle='pill' href='#Historial'>Historial</a>" +
                                        "</li>";
            }
        }

        private void CargarInicial()
        {
            if (Request.QueryString["ID"] != null)
            {
                if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("CONTABILIDAD")))
                {
                    divBTNContabilidad.Visible = true;
                }
                SPListItem SegFact = SPContext.Current.Web.Lists["SegFact"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                CargarDatos(SegFact);
                CargarDatosHist(SegFact.ID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "divBlock();", true);
            }
            else
            {
                txtCreado.Text = DateTime.Now.ToShortDateString();
                dtFecha.SelectedDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                txtRazonSocial.Text = SPContext.Current.Web.CurrentUser.Name.ToString();
                string s = string.Empty;
                try
                {
                    s = SPContext.Current.Web.CurrentUser.LoginName.ToString().Split('|')[2];
                }
                catch
                {
                    s = SPContext.Current.Web.CurrentUser.LoginName.ToString().Split('|')[1];
                }
                try
                {
                    MembershipUser userMembership = Utils.BaseMembershipProvider().GetUser(s, false);

                    txtCuit.Text = s;
                    txtEmail.Text = userMembership.Email;
                }
                catch
                {
                    txtCuit.Text = s;
                    txtEmail.Text = SPContext.Current.Web.CurrentUser.Email;
                }
            }
        }

        private void CargarDatosHist(int iIdFormulario)
        {
            string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='IdFormRelacionado' /><Value Type='Text'>" + iIdFormulario.ToString() + "</Value></Eq></Where></Query></View>";

            SPQuery query = new SPQuery();
            query.ViewXml = QuerySTR;
            SPListItemCollection ListaAux = SPContext.Current.Web.Lists["SegFactHistorial"].GetItems(query);
            foreach (SPListItem item in ListaAux)
            {
                SPFieldUserValue Usuario = new SPFieldUserValue(SPContext.Current.Web, item["Author"].ToString());
                ltTablaHistorial.Text += "<tr>"+
                                            "<td>" + item["Created"].ToString() + "</td>" +
                                            "<td>" + item["Evento"].ToString() + "</td>"+
                                            "<td>" + item["Estado"].ToString() + "</td>" +
                                            "<td>" + Usuario.User.Name + "</td>"+
                                        "</tr>";
            }
            
        }

        private void CargarDatos(SPListItem Registro)
        {
            HabilitarCampos(Registro);
            if (Request.QueryString["ID"] != null)
            {
                txtCreado.Text = DateTime.Parse(Registro["Created"].ToString()).ToShortDateString();
            }
            if (Registro["FechaComprobante"] != null)
            {
                dtFecha.SelectedDate = Convert.ToDateTime(Registro["FechaComprobante"] != null && !string.IsNullOrEmpty(Registro["FechaComprobante"].ToString()) ? Registro["FechaComprobante"].ToString() : String.Empty);
            }
            //Datos Generales
            txtEstado.Text = (Registro["Estado"] != null && !string.IsNullOrEmpty(Registro["Estado"].ToString()) ? Registro["Estado"].ToString() : String.Empty);
            txtRazonSocial.Text = (Registro["RazonSocial"] != null && !string.IsNullOrEmpty(Registro["RazonSocial"].ToString()) ? Registro["RazonSocial"].ToString() : String.Empty);
            txtCuit.Text = (Registro["CUIT"] != null && !string.IsNullOrEmpty(Registro["CUIT"].ToString()) ? Registro["CUIT"].ToString() : String.Empty);
            txtNroFact.Text = (Registro["NumFact"] != null && !string.IsNullOrEmpty(Registro["NumFact"].ToString()) ? Registro["NumFact"].ToString() : String.Empty);
            if (Registro["OC"] != null && !string.IsNullOrEmpty(Registro["OC"].ToString()))
            {
                cboOC.SelectedValue = (Registro["OC"] != null && !string.IsNullOrEmpty(Registro["OC"].ToString()) ? Registro["OC"].ToString() : String.Empty);
            }
            txtNumOC.Text = (Registro["NumOC"] != null && !string.IsNullOrEmpty(Registro["NumOC"].ToString()) ? Registro["NumOC"].ToString() : String.Empty);
         //   txtCertServ.Text = (Registro["CertServ"] != null && !string.IsNullOrEmpty(Registro["CertServ"].ToString()) ? Registro["CertServ"].ToString() : String.Empty);
         //   txtCentroCosto.Text = (Registro["CentroCosto"] != null && !string.IsNullOrEmpty(Registro["CentroCosto"].ToString()) ? Registro["CentroCosto"].ToString() : String.Empty);
            txtMonto.Text = (Registro["Monto"] != null && !string.IsNullOrEmpty(Registro["Monto"].ToString()) ? Registro["Monto"].ToString() : String.Empty);
            if (Registro["Sociedad"] != null && !string.IsNullOrEmpty(Registro["Sociedad"].ToString()))
            {
                cboSociedad.SelectedValue = (Registro["Sociedad"] != null && !string.IsNullOrEmpty(Registro["Sociedad"].ToString()) ? Registro["Sociedad"].ToString() : String.Empty);
            }
            txtEmail.Text = (Registro["Email"] != null && !string.IsNullOrEmpty(Registro["Email"].ToString()) ? Registro["Email"].ToString() : String.Empty);
            txtObservaciones.Text = (Registro["Observaciones"] != null && !string.IsNullOrEmpty(Registro["Observaciones"].ToString()) ? Registro["Observaciones"].ToString() : String.Empty);
            //Contabilidad
            txtObsContabilidad.Text = (Registro["ObservacionesContabilidad"] != null && !string.IsNullOrEmpty(Registro["ObservacionesContabilidad"].ToString()) ? Registro["ObservacionesContabilidad"].ToString() : String.Empty);
            //Tesoreria
            txtOrdenPago.Text = (Registro["NumOrdenPago"] != null && !string.IsNullOrEmpty(Registro["NumOrdenPago"].ToString()) ? Registro["NumOrdenPago"].ToString() : String.Empty);
            txtNumContabilidad.Text = (Registro["NumContabilizado"] != null && !string.IsNullOrEmpty(Registro["NumContabilizado"].ToString()) ? Registro["NumContabilizado"].ToString() : String.Empty);
            CargarAdjuntos(Registro);
            
            
        }

        private void HabilitarCampos(SPListItem Registro)
        {
            txtEstado.ReadOnly = true;
            dtFecha.Enabled = false;
            txtRazonSocial.ReadOnly = true;
            txtCuit.ReadOnly = true;
            txtNumOC.ReadOnly = true;
            txtNroFact.ReadOnly = true;
            cboOC.Enabled = false;
           // txtCertServ.ReadOnly = true;
         //   txtCentroCosto.ReadOnly = true;
            txtMonto.ReadOnly = true;
            cboSociedad.Enabled = false;
            txtEmail.ReadOnly = true;
            fuFactura.Visible = false;
            fuCertServ.Visible = false;
            txtObservaciones.ReadOnly = true;

            btnGuardar.Visible = false;
         
            string ID = Registro.ID.ToString();
            bool TesCont = false;
            SPQuery query = new SPQuery();
            string QuerySTR = "<View>" +
                "<Query><Where><And><Eq><FieldRef Name='IdFormRelacionado' /><Value Type='Text'>" + ID + "</Value></Eq><Eq><FieldRef Name='Evento' /><Value Type='Text'>Tesorería ha pasado el formulario a Contabilidad</Value></Eq></And></Where></Query>" +
                    "</View>";
            query.ViewXml = QuerySTR;
            SPListItemCollection Sociedad = SPContext.Current.Web.Lists["SegFactHistorial"].GetItems(query);
            if (Sociedad.Count > 0)
            {
                TesCont = true;
            }
            
            string Estado = Registro["Estado"].ToString();
            switch (Estado)
            {
                case "PENDIENTE DE RECEPCIÓN":
                    if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("CONTABILIDAD")))
                    {
                        btnIrTesoreria.Visible = false;
                        btnContabilizar.Visible = true;
                        btnContabilizar.Disabled = false;
                        btnRechazar.Disabled = false;
                        btnRecibido.Disabled = false;
                        if (TesCont == true)
                        {
                            btnRechazar.Visible = false;
                        }
                        txtObsContabilidad.ReadOnly = false;
                    }
                    break;
                case "RECIBIDO":
                    if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("CONTABILIDAD")))
                    {
                        btnIrTesoreria.Visible = false;
                        btnContabilizar.Visible = true;
                        btnContabilizar.Disabled = false;
                        btnRechazar.Disabled = false;
                        btnRecibido.Disabled = true;
                        btnRecibido.Visible = false;
                        if (TesCont == true)
                        {
                            btnRechazar.Visible = false;
                        }
                        txtObsContabilidad.ReadOnly = false;
                    }
                    break;
                case "TESORERIA":
                    if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("TESORERIA")))
                    {
                        btnPasarContabilidad.Disabled = false;
                        fuAdjunto.Enabled = true;
                        txtNumContabilidad.ReadOnly = false;
                        txtOrdenPago.ReadOnly = false;
                        btnRecibido.Visible = false;
                        btnRechazar.Visible = false;
                        btnIrTesoreria.Visible = false;
                        btnContabilizar.Visible = false;
                        //btnFinalizar.Disabled = false;
                    }
                    break;
                case "CONTABILIZADO":
                    if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("CONTABILIDAD")))
                    {
                        btnIrTesoreria.Disabled = false;
                        btnRecibido.Disabled = false;
                        //btnRecibido.Visible = true;
                        btnContabilizar.Disabled = true;
                        btnRechazar.Disabled = true;
                        btnRechazar.Visible = false;
                        btnContabilizar.Visible = false;
                        if (TesCont == true)
                        {
                            btnRechazar.Visible = false;
                        }
                        txtObsContabilidad.ReadOnly = false;
                    }
                    break;
                //case "RECHAZADO":
                //    SPFieldUserValue Usuario = new SPFieldUserValue(SPContext.Current.Web, Registro[SPBuiltInFieldId.Author].ToString());
                //    SPUser Creado = Usuario.User;
                //    if (SPContext.Current.Web.CurrentUser.ID.ToString().ToUpper() == Creado.ID.ToString().ToUpper())
                //    {
                //        btnRecibido.Visible = false;
                //        btnRechazar.Visible = false;
                //        btnIrTesoreria.Visible = false;
                //        btnContabilizar.Visible = false;
                //        txtNumOC.ReadOnly = false;
                //        txtNroFact.ReadOnly = false;
                //        cboOC.Enabled = true;
                //     //   txtCertServ.ReadOnly = false;
                //     //   txtCentroCosto.ReadOnly = false;
                //        txtMonto.ReadOnly = false;
                //        cboSociedad.Enabled = true;
                //        txtEmail.ReadOnly = false;
                //        fuFactura.Visible = true;
                //        fuCertServ.Visible = true;
                //        txtObservaciones.ReadOnly = false;
                //        btnGuardar.Visible = true;
                //    }
                //    break;
            }
        }
        private void CargarAdjuntos(SPListItem AltaProveedor)
        {
            SPAttachmentCollection collAttachments = AltaProveedor.Attachments;
            if (collAttachments.Count > 0)
            {
                foreach (string fileName in collAttachments)
                {
                    if (fileName.Contains("NFactura"))
                    {
                        ltFactura.Text += "<a href='" + collAttachments.UrlPrefix + fileName + "' target='_blank' title='Click para ver el documento' class='adjuntoItem'> " + fileName + "</a>";
                    }
                    if (fileName.Contains("CertifDeServ"))
                    {
                        ltCertServ.Text += "<p><a href='" + collAttachments.UrlPrefix + fileName + "' target='_blank' title='Click para ver el documento' class='adjuntoItem'> " + fileName + "</a></p>";
                    }
                     if (fileName.Contains("CertifFI"))
                    {
                        ltCertFI.Text += "<a href='" + collAttachments.UrlPrefix + fileName + "' target='_blank' title='Click para ver el documento' class='adjuntoItem'> " + fileName + "</a>";
                    }
                    if (fileName.Contains("NOrdenDePago"))
                    {
                        ltAdjuntos.Text += "<a href='" + collAttachments.UrlPrefix + fileName + "' target='_blank' title='Click para ver el documento' class='adjuntoItem'> " + fileName + "</a>";
                    }
                   
                }

            }
            
        }

        private void CargarCombos()
        {
            SPQuery query = new SPQuery();
            string QuerySTR = "<View>" +
                "<Query><Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>Sociedad</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Activo</Value></Eq></And></Where></Query>" +
                    "</View>";
            query.ViewXml = QuerySTR;
            SPListItemCollection Sociedad = SPContext.Current.Web.Lists["SegFact_Aux"].GetItems(query);
            if (Sociedad.Count > 0)
            {
                DataView view1 = new DataView(Sociedad.GetDataTable());
                DataTable distinctValues1 = view1.ToTable(true, "Valor");
                DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
                rFilaSeleccioneSolicitud["Valor"] = strSeleccione;
                distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
                cboSociedad.DataSource = distinctValues1;
                cboSociedad.DataValueField = "Valor";
                cboSociedad.DataTextField = "Valor";
                cboSociedad.DataBind();
            }
            
        }

        protected void btnGuardar_ServerClick(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                SPListItem RegistroExistente = SPContext.Current.Web.Lists["SegFact"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                if (RegistroExistente["Estado"].ToString() == "RECHAZADO")
                {
                    EnviarCorreoEditado(RegistroExistente);
                    GuardarDatosHistorialEditado(RegistroExistente.ID);
                }
                GuardarDatos(RegistroExistente);
                RegistroExistente.Update();
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeModificado();", true);
            }
            else
            {
                SPListItem RegistroNuevo = SPContext.Current.Web.Lists["SegFact"].AddItem();
                GuardarDatos(RegistroNuevo);
                RegistroNuevo.Update();
                GuardarDatosHistorial(RegistroNuevo.ID);
                EnviarCorreo(RegistroNuevo);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeGuarda(1);", true);
            }
        }

        private void GuardarDatosHistorial(int iIdFormulario)
        {
            SPListItem Item = null;
            string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='IdFormRelacionado' /><Value Type='Text'>" + iIdFormulario.ToString() + "</Value></Eq></Where></Query></View>";
            SPQuery query = new SPQuery();
            query.ViewXml = QuerySTR;
            SPListItemCollection ListaAux = SPContext.Current.Web.Lists["SegFactHistorial"].GetItems(query);
            if (ListaAux.Count > 0)
            {
                Item = ListaAux[0];
            }
            else
            {
                Item = SPContext.Current.Web.Lists["SegFactHistorial"].Items.Add();

            }
            if (Item != null)
            {
                Item["IdFormRelacionado"] = iIdFormulario;
                Item["Evento"] = "El usuario " + txtRazonSocial.Text + " ha generado un nuevo formulario";
                Item["Estado"] = "PENDIENTE DE RECEPCIÓN";
                Item.Update();
            }
        }

        private void GuardarDatos(SPListItem Registro)
        {
            
            //Datos Generales
            Registro["Estado"] = "PENDIENTE DE RECEPCIÓN";
            Registro["RazonSocial"] = (txtRazonSocial.Text != null && !string.IsNullOrEmpty(txtRazonSocial.Text) ? txtRazonSocial.Text : String.Empty);
            Registro["CUIT"] = (txtCuit.Text != null && !string.IsNullOrEmpty(txtCuit.Text) ? txtCuit.Text : String.Empty);
            Registro["NumFact"] = (txtNroFact.Text != null && !string.IsNullOrEmpty(txtNroFact.Text) ? txtNroFact.Text : String.Empty);
            if (dtFecha.IsDateEmpty == false)
            {
                Registro["FechaComprobante"] = dtFecha.SelectedDate.Date.ToShortDateString();
            }
            Registro["OC"] = (cboOC.SelectedItem.Text != null && !string.IsNullOrEmpty(cboOC.SelectedItem.Text) ? cboOC.SelectedItem.Text : String.Empty);
            Registro["NumOC"] = (txtNumOC.Text != null && !string.IsNullOrEmpty(txtNumOC.Text) ? txtNumOC.Text : String.Empty);
         //   Registro["CertServ"] = (txtCertServ.Text != null && !string.IsNullOrEmpty(txtCertServ.Text) ? txtCertServ.Text : String.Empty);
     //       Registro["CentroCosto"] = (txtCentroCosto.Text != null && !string.IsNullOrEmpty(txtCentroCosto.Text) ? txtCentroCosto.Text : String.Empty);
            Registro["Monto"] = (txtMonto.Text != null && !string.IsNullOrEmpty(txtMonto.Text) ? txtMonto.Text : String.Empty);
            Registro["Sociedad"] = (cboSociedad.SelectedItem.Text != null && !string.IsNullOrEmpty(cboSociedad.SelectedItem.Text) ? cboSociedad.SelectedItem.Text : String.Empty);
            Registro["Email"] = (txtEmail.Text != null && !string.IsNullOrEmpty(txtEmail.Text) ? txtEmail.Text : String.Empty);
            Registro["Observaciones"] = (txtObservaciones.Text != null && !string.IsNullOrEmpty(txtObservaciones.Text) ? txtObservaciones.Text : String.Empty);

            if (fuFactura.HasFiles)
            {
                List<string> fileNames = new List<string>();
                foreach (string fileName in Registro.Attachments)
                {
                    fileNames.Add(fileName);
                }
                foreach (string fileName in fileNames)
                {
                    if (fileName.Contains("NFactura"))
                    {
                        Registro.Attachments.Delete(fileName);
                    }
                }
                foreach (HttpPostedFile uploadedFile in fuFactura.PostedFiles)
                {
                    string  ext =  System.IO.Path.GetExtension (fuFactura.FileName);
                    Stream fs = fuFactura.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                 
                    Registro.Attachments.Add("NFactura"  + ext, fileContents);
                }
            }
            if (fuCertFI.HasFiles)
            {
                int cont = 0;
                foreach (HttpPostedFile uploadedFile in fuCertFI.PostedFiles)
                {
                    cont++;
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "CertifFI" + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
                }
                //List<string> fileNames = new List<string>();
                //foreach (string fileName in Registro.Attachments)
                //{
                //    fileNames.Add(fileName);
                //}
                //foreach (string fileName in fileNames)
                //{
                //    if (fileName.Contains("CertifFI"))
                //    {
                //        Registro.Attachments.Delete(fileName);
                //    }
                //}
                //foreach (HttpPostedFile uploadedFile in fuCertFI.PostedFiles)
                //{
                //    string ext = System.IO.Path.GetExtension(fuCertFI.FileName);
                //    Stream fs = fuCertFI.PostedFile.InputStream;
                //    byte[] fileContents = new byte[fs.Length];
                //    fs.Read(fileContents, 0, (int)fs.Length);
                //    fs.Close();
                //    Registro.Attachments.Add("CertifFI" + ext, fileContents);

                //}
            }
            if (fuCertServ.HasFiles)
            {
                int cont = 0;
                foreach (HttpPostedFile uploadedFile in fuCertServ.PostedFiles)
                {
                    cont++;
                    Stream fs = uploadedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    SPAttachmentCollection attachments = Registro.Attachments;

                    string fileName = "CertifDeServ" + " - " + cont + System.IO.Path.GetExtension(uploadedFile.FileName);
                    attachments.Add(fileName, fileContents);
                }
            }
        }
        private void GuardarDatosHistorialEditado(int iIdFormulario)
        {
            SPListItem Item = SPContext.Current.Web.Lists["SegFactHistorial"].Items.Add();
            Item["IdFormRelacionado"] = iIdFormulario;
            Item["Evento"] = "El usuario " + txtRazonSocial.Text + " ha modificado el formulario";
            Item["Estado"] = "PENDIENTE DE RECEPCIÓN";
            Item.Update();
        }

        protected void EnvioDeEmails(string CodigoEmail, SPListItem RegistroExistente)
        {
            switch (CodigoEmail)
            {
                case "CCPCOCMOD":
                    //CORREO A CUENTAS A PAGAR POR MODIFICACION - CON OC
                    string QuerySTRCFEOCMOD = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CCPCOCMOD</Value></Eq></Where></Query></View>";
                    SPQuery queryCFEOCMOD = new SPQuery();
                    queryCFEOCMOD.ViewXml = QuerySTRCFEOCMOD;
                    SPListItemCollection ListaABMCFEOCMOD = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCFEOCMOD);
                    string htmlCorreoCFEOCMOD = "";
                    string destinatarioCorreoCFEOCMOD = "";
                    foreach (SPListItem item in ListaABMCFEOCMOD)
                    {
                        htmlCorreoCFEOCMOD = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                        destinatarioCorreoCFEOCMOD = (item["UsuariosPara"] != null && !string.IsNullOrEmpty(item["UsuariosPara"].ToString()) ? item["UsuariosPara"].ToString() : String.Empty);
                    }
                    htmlCorreoCFEOCMOD = htmlCorreoCFEOCMOD.Replace("##CUIT##", RegistroExistente["CUIT"].ToString()).Replace("##NUMOC##", RegistroExistente["NumOC"].ToString()).Replace("##NUMFACT##", RegistroExistente["NumFact"].ToString()).Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/SE.PNG").Replace("##REGISTRO##", SPContext.Current.Web.Url + "/_layouts/15/SegFact/Registro.aspx?ID=" + RegistroExistente.ID);
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, destinatarioCorreoCFEOCMOD, RegistroExistente["CUIT"].ToString() + " - " + RegistroExistente["NumFact"].ToString(), htmlCorreoCFEOCMOD);
                    break;
                case "CCPSOCMOD":
                    //CORREO A CUENTAS A PAGAR POR MODIFICACION - SIN OC
                    string QuerySTRCCPSOCMOD = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CCPSOCMOD</Value></Eq></Where></Query></View>";
                    SPQuery queryCCPSOCMOD = new SPQuery();
                    queryCCPSOCMOD.ViewXml = QuerySTRCCPSOCMOD;
                    SPListItemCollection ListaABMCCPSOCMOD = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCCPSOCMOD);
                    string htmlCorreoCCPSOCMOD = "";
                    string destinatarioCorreoCCPSOCMOD = "";
                    foreach (SPListItem item in ListaABMCCPSOCMOD)
                    {
                        htmlCorreoCCPSOCMOD = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                        destinatarioCorreoCCPSOCMOD = (item["UsuariosPara"] != null && !string.IsNullOrEmpty(item["UsuariosPara"].ToString()) ? item["UsuariosPara"].ToString() : String.Empty);
                    }
                    htmlCorreoCCPSOCMOD = htmlCorreoCCPSOCMOD.Replace("##CUIT##", RegistroExistente["CUIT"].ToString()).Replace("##NUMFACT##", RegistroExistente["NumFact"].ToString()).Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/SE.PNG").Replace("##REGISTRO##", SPContext.Current.Web.Url + "/_layouts/15/SegFact/Registro.aspx?ID=" + RegistroExistente.ID);
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, destinatarioCorreoCCPSOCMOD, RegistroExistente["CUIT"].ToString() + " - " + RegistroExistente["NumFact"].ToString(), htmlCorreoCCPSOCMOD);
                    break;
                case "CPMFRMOD":
                    //CORREO A PROVEEDOR RECEPCION DE MODIFICACION FACTURA
                    string QuerySTRCPMFRMOD = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CPMFRMOD</Value></Eq></Where></Query></View>";
                    SPQuery queryCPMFRMOD = new SPQuery();
                    queryCPMFRMOD.ViewXml = QuerySTRCPMFRMOD;
                    SPListItemCollection ListaABMCPMFRMOD = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCPMFRMOD);
                    string htmlCorreoCPMFRMOD = "";
                    foreach (SPListItem item in ListaABMCPMFRMOD)
                    {
                        htmlCorreoCPMFRMOD = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                    }
                    htmlCorreoCPMFRMOD = htmlCorreoCPMFRMOD.Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/SE.PNG").Replace("##REGISTRO##", SPContext.Current.Web.Url + "/_layouts/15/SegFact/Registro.aspx?ID=" + RegistroExistente.ID);
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, RegistroExistente["Email"].ToString().Trim(), "Recepción " + RegistroExistente["NumFact"].ToString() + " - " + RegistroExistente["CUIT"].ToString(), htmlCorreoCPMFRMOD);
                    break;
                case "CCPCOC":
                    //CORREO A CUENTAS A PAGAR Nueva Factura- CON OC
                    string QuerySTRCFEOC = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CCPCOC</Value></Eq></Where></Query></View>";
                    SPQuery queryCFEOC = new SPQuery();
                    queryCFEOC.ViewXml = QuerySTRCFEOC;
                    SPListItemCollection ListaABMCFEOC = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCFEOC);
                    string htmlCorreoCFEOC = "";
                    string destinatarioCorreoCFEOC = "";
                    foreach (SPListItem item in ListaABMCFEOC)
                    {
                        htmlCorreoCFEOC = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                        destinatarioCorreoCFEOC = (item["UsuariosPara"] != null && !string.IsNullOrEmpty(item["UsuariosPara"].ToString()) ? item["UsuariosPara"].ToString() : String.Empty);
                    }
                    htmlCorreoCFEOC = htmlCorreoCFEOC.Replace("##CUIT##", RegistroExistente["CUIT"].ToString()).Replace("##NUMOC##", RegistroExistente["NumOC"].ToString()).Replace("##NUMFACT##", RegistroExistente["NumFact"].ToString()).Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/SE.PNG").Replace("##REGISTRO##", SPContext.Current.Web.Url + "/_layouts/15/SegFact/Registro.aspx?ID=" + RegistroExistente.ID);
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, destinatarioCorreoCFEOC, RegistroExistente["CUIT"].ToString() + " - " + RegistroExistente["NumFact"].ToString(), htmlCorreoCFEOC);
                    break;
                case "CCPSOC":
                    //CORREO A CUENTAS A PAGAR  Nueva Factura - SIN OC
                    string QuerySTRCCPSOC = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CCPSOC</Value></Eq></Where></Query></View>";
                    SPQuery queryCCPSOC = new SPQuery();
                    queryCCPSOC.ViewXml = QuerySTRCCPSOC;
                    SPListItemCollection ListaABMCCPSOC = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCCPSOC);
                    string htmlCorreoCCPSOC = "";
                    string destinatarioCorreoCCPSOC = "";
                    foreach (SPListItem item in ListaABMCCPSOC)
                    {
                        htmlCorreoCCPSOC = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                        destinatarioCorreoCCPSOC = (item["UsuariosPara"] != null && !string.IsNullOrEmpty(item["UsuariosPara"].ToString()) ? item["UsuariosPara"].ToString() : String.Empty);
                    }
                    htmlCorreoCCPSOC = htmlCorreoCCPSOC.Replace("##CUIT##", RegistroExistente["CUIT"].ToString()).Replace("##NUMFACT##", RegistroExistente["NumFact"].ToString()).Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/SE.PNG").Replace("##REGISTRO##", SPContext.Current.Web.Url + "/_layouts/15/SegFact/Registro.aspx?ID=" + RegistroExistente.ID);
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, destinatarioCorreoCCPSOC, RegistroExistente["CUIT"].ToString() + " - " + RegistroExistente["NumFact"].ToString(), htmlCorreoCCPSOC);
                    break;
                case "CPFR":
                    //CORREO A PROVEEDOR RECEPCION  Nueva Factura
                    string QuerySTRCPMFR = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CPFR</Value></Eq></Where></Query></View>";
                    SPQuery queryCPMFR = new SPQuery();
                    queryCPMFR.ViewXml = QuerySTRCPMFR;
                    SPListItemCollection ListaABMCPMFR = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCPMFR);
                    string htmlCorreoCPMFR = "";
                    foreach (SPListItem item in ListaABMCPMFR)
                    {
                        htmlCorreoCPMFR = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                    }
                    htmlCorreoCPMFR = htmlCorreoCPMFR.Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/SE.PNG").Replace("##REGISTRO##", SPContext.Current.Web.Url + "/_layouts/15/SegFact/Registro.aspx?ID=" + RegistroExistente.ID);
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, RegistroExistente["Email"].ToString().Trim(), "Recepción " + RegistroExistente["NumFact"].ToString() + " - " + RegistroExistente["CUIT"].ToString(), htmlCorreoCPMFR);
                    break;
                    case "CPFRC":
                    //CORREO A PROVEEDOR RECHAZO Factura
                    string QuerySTRCPFRC = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CPFRC</Value></Eq></Where></Query></View>";
                    SPQuery queryCPFRC = new SPQuery();
                    queryCPFRC.ViewXml = QuerySTRCPFRC;
                    SPListItemCollection ListaABMCPFRC = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCPFRC);
                    string htmlCorreoCPFRC = "";
                    foreach (SPListItem item in ListaABMCPFRC)
                    {
                        htmlCorreoCPFRC = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                    }
                    htmlCorreoCPFRC = htmlCorreoCPFRC.Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/SE.PNG").Replace("##REGISTRO##", SPContext.Current.Web.Url + "/_layouts/15/SegFact/Registro.aspx?ID=" + RegistroExistente.ID).Replace("##OBSERVACIONESCONTABILIDAD##", RegistroExistente["ObservacionesContabilidad"].ToString()).Replace("##IDREGISTRO##", RegistroExistente.ID.ToString());
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, RegistroExistente["Email"].ToString().Trim(), RegistroExistente["CUIT"].ToString() + " - " + RegistroExistente["NumFact"].ToString(), htmlCorreoCPFRC);
                    break;
                case "CTF":
                    //CORREO A TESORERIA
                    SPFieldUserValue Usuario = new SPFieldUserValue(SPContext.Current.Web, RegistroExistente["Author"].ToString());
                    string QuerySTRCTF = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CTF</Value></Eq></Where></Query></View>";
                    SPQuery queryCTF = new SPQuery();
                    queryCTF.ViewXml = QuerySTRCTF;
                    SPListItemCollection ListaABMCTF = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCTF);
                    string htmlCorreoCTF = "";
                    string destinatarioCorreoCTF = "";
                    foreach (SPListItem item in ListaABMCTF)
                    {
                        htmlCorreoCTF = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                        destinatarioCorreoCTF = (item["UsuariosPara"] != null && !string.IsNullOrEmpty(item["UsuariosPara"].ToString()) ? item["UsuariosPara"].ToString() : String.Empty);
                    }
                    htmlCorreoCTF = htmlCorreoCTF.Replace("##USUARIO##", Usuario.User.Name).Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/SE.PNG").Replace("##REGISTRO##", SPContext.Current.Web.Url + "/_layouts/15/SegFact/Registro.aspx?ID=" + RegistroExistente.ID).Replace("##IDREGISTRO##", RegistroExistente.ID.ToString());
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, destinatarioCorreoCTF, RegistroExistente["CUIT"].ToString() + " - " + RegistroExistente["NumFact"].ToString(), htmlCorreoCTF);
                    break;
                case "CCF":
                    //CORREO A CONTABILIDAD
                    SPFieldUserValue UsuarioCCF = new SPFieldUserValue(SPContext.Current.Web, RegistroExistente["Author"].ToString());
                    string QuerySTRCCF = "<View><Query><Where><Eq><FieldRef Name='Codigo'/><Value Type='Text'>CCF</Value></Eq></Where></Query></View>";
                    SPQuery queryCCF = new SPQuery();
                    queryCCF.ViewXml = QuerySTRCCF;
                    SPListItemCollection ListaABMCCF = SPContext.Current.Web.Lists["ABMMails"].GetItems(queryCCF);
                    string htmlCorreoCCF = "";
                    string destinatarioCorreoCCF = "";
                    foreach (SPListItem item in ListaABMCCF)
                    {
                        htmlCorreoCCF = (item["Html"] != null && !string.IsNullOrEmpty(item["Html"].ToString()) ? item["Html"].ToString() : String.Empty);
                        destinatarioCorreoCCF = (item["UsuariosPara"] != null && !string.IsNullOrEmpty(item["UsuariosPara"].ToString()) ? item["UsuariosPara"].ToString() : String.Empty);
                    }
                    htmlCorreoCCF = htmlCorreoCCF.Replace("##USUARIO##", UsuarioCCF.User.Name).Replace("##URLSITIO##", sSitioAnonimo + "/PublishingImages/SE.PNG").Replace("##REGISTRO##", SPContext.Current.Web.Url + "/_layouts/15/SegFact/Registro.aspx?ID=" + RegistroExistente.ID).Replace("##IDREGISTRO##", RegistroExistente.ID.ToString());
                    SPUtility.SendEmail(SPContext.Current.Web, false, false, destinatarioCorreoCCF, RegistroExistente["CUIT"].ToString() + " - " + RegistroExistente["NumFact"].ToString(), htmlCorreoCCF);
                    break;
            }
        }
        private void EnviarCorreoEditado(SPListItem Registro)
        {
            if (Registro["OC"].ToString() == "Con OC")
            {
                EnvioDeEmails("CCPCOCMOD", Registro);
            }
            else
            {
                EnvioDeEmails("CCPSOCMOD", Registro);
            }
            EnvioDeEmails("CPMFRMOD", Registro);
        }
        private void EnviarCorreo(SPListItem Registro)
        {
            if (Registro["OC"].ToString() == "Con OC")
            {
                EnvioDeEmails("CCPCOC", Registro);
            }
            if (Registro["OC"].ToString() == "Sin OC")
            {
                EnvioDeEmails("CCPSOC", Registro);
            }
            EnvioDeEmails("CPFR", Registro);
           
        }

        protected void btnRechazar_ServerClick(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                SPListItem RechazaContabilidad = SPContext.Current.Web.Lists["SegFact"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                GuardarDatosContabilidad(RechazaContabilidad);
                RechazaContabilidad.Update();
                CorreoRechazado(RechazaContabilidad);
                GuardarDatosHistorialContabilidad(RechazaContabilidad.ID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeGuarda(2);", true);
            }
        }

        private void CorreoRechazado(SPListItem RechazaContabilidad)
        {
            EnvioDeEmails("CPFRC", RechazaContabilidad);
           
        }

        private void GuardarDatosHistorialContabilidad(int iIdFormulario)
        {
            SPListItem Item = SPContext.Current.Web.Lists["SegFactHistorial"].Items.Add();
            Item["IdFormRelacionado"] = iIdFormulario;
            Item["Evento"] = "Contabilidad ha rechazado el formulario con el siguiente comentario: " + txtObsContabilidad.Text + "";
            Item["Estado"] = "RECHAZADO";
            Item.Update();
            
        }

        private void GuardarDatosContabilidad(SPListItem Registro)
        {
            Registro["Estado"] = "RECHAZADO";
            Registro["ObservacionesContabilidad"] = (txtObsContabilidad.Text != null && !string.IsNullOrEmpty(txtObsContabilidad.Text) ? txtObsContabilidad.Text : String.Empty);
        }

        protected void btnIrTesoreria_ServerClick(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                SPListItem pasarTesoreria = SPContext.Current.Web.Lists["SegFact"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                GuardarDatosTesoreria(pasarTesoreria);
                pasarTesoreria.Update();
                CorreoContabilidadTesoreria(pasarTesoreria);
                GuardarDatosHistorialTesoreria(pasarTesoreria.ID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeGuarda(2);", true);
            }
        }

        private void CorreoContabilidadTesoreria(SPListItem pasarTesoreria)
        {
            EnvioDeEmails("CTF", pasarTesoreria);
            //SPFieldUserValue Usuario = new SPFieldUserValue(SPContext.Current.Web, pasarTesoreria["Author"].ToString());
            //string sCorreo = "<table style='height: 160px;' border='0' width='606' cellspacing='0' cellpadding='0' align='center'><tbody>" +
            //                        "<tr>" +
            //                            "<td bgcolor='#0a4e9a'><img src='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/img/Captura.PNG' alt='Logo IEASA' width='606'></td>" +
            //                        "</tr>" +
            //                        "<tr>" +
            //                            "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
            //                                "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>" +
            //                                    "El formulario " + pasarTesoreria.ID + " del usuario " + Usuario.User.Name + " ha cambiado su estado a TESORERIA" +
            //                                    "<br><a href='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/Registro.aspx?ID=" + pasarTesoreria.ID + "'>Ir al registro " + pasarTesoreria.ID + "</a>" +
            //                                "</p>" +
            //                            "</td>" +
            //                        "</tr>" +
            //                        "<tr>" +
            //                            "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
            //                                "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Saludos</p>" +
            //                            "</td>" +
            //                        "</tr>" +
            //                    "</tbody></table>";
            //SPUtility.SendEmail(SPContext.Current.Web, false, false, "tesoreria@ieasa.com.ar", pasarTesoreria["CUIT"].ToString() + " - " + pasarTesoreria["NumFact"].ToString(), sCorreo);
        }

        private void GuardarDatosHistorialTesoreria(int iIdFormulario)
        {
            SPListItem Item = SPContext.Current.Web.Lists["SegFactHistorial"].Items.Add();
            Item["IdFormRelacionado"] = iIdFormulario;
            Item["Evento"] = "Contabilidad ha pasado el formulario a Tesorería";
            Item["Estado"] = "TESORERIA";
            Item.Update();
        }

        private void GuardarDatosTesoreria(SPListItem Registro)
        {
            Registro["Estado"] = "TESORERIA";
            Registro["ObservacionesContabilidad"] = (txtObsContabilidad.Text != null && !string.IsNullOrEmpty(txtObsContabilidad.Text) ? txtObsContabilidad.Text : String.Empty);
        }

        protected void btnPasarContabilidad_ServerClick(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                SPListItem pasarContabilidad = SPContext.Current.Web.Lists["SegFact"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                GuardarContabilidad(pasarContabilidad);
                GuardarHistorialContabilidad(pasarContabilidad.ID);
                pasarContabilidad.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeGuarda(3);", true);
            }
        }

        private void CorreoTesoreriaContabilidad(SPListItem pasarContabilidad)
        {
            EnvioDeEmails("CCF", pasarContabilidad);
            //SPFieldUserValue Usuario = new SPFieldUserValue(SPContext.Current.Web, pasarContabilidad["Author"].ToString());
            //string sCorreo = "<table style='height: 160px;' border='0' width='606' cellspacing='0' cellpadding='0' align='center'><tbody>" +
            //                        "<tr>" +
            //                            "<td bgcolor='#0a4e9a'><img src='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/img/Captura.PNG' alt='Logo IEASA' width='606'></td>" +
            //                        "</tr>" +
            //                        "<tr>" +
            //                            "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
            //                                "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>" +
            //                                    "El formulario " + pasarContabilidad.ID + " del usuario " + Usuario.User.Name + " ha cambiado su estado a CONTABILIDAD" +
            //                                    "<br><a href='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/Registro.aspx?ID=" + pasarContabilidad.ID + "'>Ir al registro " + pasarContabilidad.ID + "</a>" +
            //                                "</p>" +
            //                            "</td>" +
            //                        "</tr>" +
            //                        "<tr>" +
            //                            "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
            //                                "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Saludos</p>" +
            //                            "</td>" +
            //                        "</tr>" +
            //                    "</tbody></table>";

            //SPUtility.SendEmail(SPContext.Current.Web, false, false, "cuentasapagar@ieasa.com.ar", pasarContabilidad["CUIT"].ToString() + " - " + pasarContabilidad["NumFact"].ToString(), sCorreo);
            //SPUtility.SendEmail(SPContext.Current.Web, false, false, "cgperez@nacionservicios.com.ar", pasarContabilidad["CUIT"].ToString() + " - " + pasarContabilidad["NumFact"].ToString(), sCorreo);
        }

        private void GuardarHistorialContabilidad(int iIdFormulario)
        {
            SPListItem Item = SPContext.Current.Web.Lists["SegFactHistorial"].Items.Add();
            SPListItem pasarContabilidad = SPContext.Current.Web.Lists["SegFact"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
            Item["IdFormRelacionado"] = iIdFormulario;
            HttpPostedFile hpf = fuAdjunto.PostedFile;
            if (txtOrdenPago.Text != "" && txtNumContabilidad.Text != "" && hpf.FileName != null)
            {
                Item["Evento"] = "El formulario fue FINALIZADO";
                Item["Estado"] = "FINALIZADO";
            }
            else
            {
                Item["Evento"] = "Tesorería ha pasado el formulario a Contabilidad";
                Item["Estado"] = "PENDIENTE DE RECEPCIÓN";
                CorreoTesoreriaContabilidad(pasarContabilidad);
            }
            Item.Update();
        }

        private void GuardarContabilidad(SPListItem Registro)
        {
            HttpPostedFile hpf = fuAdjunto.PostedFile;
            if (txtOrdenPago.Text != "" && txtNumContabilidad.Text != "")
            {
                Registro["Estado"] = "FINALIZADO";
            }
            else
            {
                Registro["Estado"] = "PENDIENTE DE RECEPCIÓN";
            }
            Registro["NumOrdenPago"] = (txtOrdenPago.Text != null && !string.IsNullOrEmpty(txtOrdenPago.Text) ? txtOrdenPago.Text : String.Empty);
            Registro["NumContabilizado"] = (txtNumContabilidad.Text != null && !string.IsNullOrEmpty(txtNumContabilidad.Text) ? txtNumContabilidad.Text : String.Empty);
            
            if (fuAdjunto.HasFiles)
            {
                List<string> fileNames = new List<string>();
                foreach (string fileName in Registro.Attachments)
                {
                    fileNames.Add(fileName);
                }
                foreach (string fileName in fileNames)
                {
                    if (fileName.Contains("NOrdenDePago"))
                    {
                        Registro.Attachments.Delete(fileName);
                    }
                }
                foreach (HttpPostedFile uploadedFile in fuAdjunto.PostedFiles)
                {
                    string ext = System.IO.Path.GetExtension(fuAdjunto.FileName);
                    Stream fs = fuAdjunto.PostedFile.InputStream;
                    byte[] fileContents = new byte[fs.Length];
                    fs.Read(fileContents, 0, (int)fs.Length);
                    fs.Close();
                    Registro.Attachments.Add("NOrdenDePago" + ext, fileContents);
                }
            }
        }

        protected void btnContabilizar_ServerClick(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                SPListItem pasarContabilizar = SPContext.Current.Web.Lists["SegFact"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                GuardarContabilizar(pasarContabilizar);
                pasarContabilizar.Update();
                GuardarHistorialContabilizar(pasarContabilizar.ID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeGuarda(2);", true);
            }
        }

        private void GuardarHistorialContabilizar(int iIdFormulario)
        {
            SPListItem Item = SPContext.Current.Web.Lists["SegFactHistorial"].Items.Add();
            Item["IdFormRelacionado"] = iIdFormulario;
            Item["Evento"] = "El formulario fue CONTABILIZADO";
            Item["Estado"] = "CONTABILIZADO";
            Item.Update();
        }

        private void GuardarContabilizar(SPListItem Registro)
        {
            Registro["Estado"] = "CONTABILIZADO";
            Registro["ObservacionesContabilidad"] = (txtObsContabilidad.Text != null && !string.IsNullOrEmpty(txtObsContabilidad.Text) ? txtObsContabilidad.Text : String.Empty);
        
        }

        protected void btnRecibido_ServerClick(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                SPListItem recibido = SPContext.Current.Web.Lists["SegFact"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                GuardarRecibido(recibido);
                recibido.Update();
                GuardarHistorialRecibido(recibido.ID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeGuarda(2);", true);
            }
        }

        private void GuardarHistorialRecibido(int iIdFormulario)
        {
            SPListItem Item = SPContext.Current.Web.Lists["SegFactHistorial"].Items.Add();
            Item["IdFormRelacionado"] = iIdFormulario;
            Item["Evento"] = "El formulario fue RECIBIDO";
            Item["Estado"] = "RECIBIDO";
            Item.Update();
        }

        private void GuardarRecibido(SPListItem Registro)
        {
            Registro["Estado"] = "RECIBIDO";
        }
        protected void btnCerrar_ServerClick(object sender, EventArgs e)
        { 
            if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("CONTABILIDAD")))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "cerrarContabilidad();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "cerrarDatosGenerales();", true);
            }
        }
        
    }
}
