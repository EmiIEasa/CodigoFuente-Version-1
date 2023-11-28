using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace GrillaSegFact.WP_Buscador
{
    public partial class WP_BuscadorUserControl : UserControl
    {
        public WP_Buscador WebPart { get; set; }
        public string sLimiteVistaBuscador = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
         this.WebPart = this.Parent as WP_Buscador;
            sLimiteVistaBuscador = this.WebPart.PropiedadLimiteVistaBuscador;
            if (!IsPostBack)
            {

              
                alertaCamposComp.Visible = true;
               
            }
        }
        private string SetColorEstado(string sEstado)
        {
            string sColor = "#FFDEAD";

            switch (sEstado)
            {
                case "FINALIZADO":
                    sColor = "#98FB98";
                    break;
                case "TESORERIA":
                    sColor = "#AFEEEE";
                    break;
                case "CONTABILIDAD":
                    sColor = "#FFFF00";
                    break;
                case "PENDIENTE DE RECEPCIÓN":
                    sColor = "#FFFF00";
                    break;
                case "CONTABILIZADO":
                    sColor = "#F4A460";
                    break;
                case "RECHAZADO":
                    sColor = "#CD5C5C";
                    break;
                case "RECIBIDO":
                    sColor = "#A5EEA0";
                    break;
            }
            return sColor;
        }
        protected void buscarRegistro(object sender, EventArgs e)
        {
            pnlTabla.Visible = false;
            alertaCamposComp.Visible = false;
            alertaCampoVacio.Visible = false;
            alertaNoResultado.Visible = false;
            if (txtValorB.Text != null && !string.IsNullOrEmpty(txtValorB.Text))
            {
                string QuerySTR = string.Empty;
                SPQuery query = new SPQuery();
                QuerySTR = "<View><Query><Where><Contains><FieldRef Name='" + cboCampos.SelectedValue + "' /><Value Type='Text'>" + txtValorB.Text + "</Value></Contains></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy></Query>" +
                            "<RowLimit>" + sLimiteVistaBuscador + "</RowLimit></View>";
                query.ViewXml = QuerySTR;
                SPListItemCollection ListaReclamos = SPContext.Current.Web.Lists["SegFact"].GetItems(query);
                LtTablaFacturas.Text = "";
                if (ListaReclamos.Count == 0)
                {
                    alertaNoResultado.Visible = true;
                    pnlTabla.Visible = false;
                }
                alertaMasCarac.Visible = false;
                pnlTabla.Visible = true;
                foreach (SPListItem Reclamos in ListaReclamos)
                {
                    LtTablaFacturas.Text += "<tr>" +
                                                "<td>" + "<div class='form-check'><label class='form-check-label' for='check1'><input type='checkbox' class='form-check-input checkbox-class' id='check1' name='option1' value='" + Reclamos.ID.ToString() + "' data-id='" + Reclamos.ID.ToString() + "'>"+"<a href='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/registro.aspx?ID=" + Reclamos.ID.ToString() + "' class='alert-link'>" + Reclamos.ID.ToString() + "</a></td>" +
                                                "<td style='Background-Color:" + SetColorEstado((Reclamos["Estado"] != null && !string.IsNullOrEmpty(Reclamos["Estado"].ToString()) ? Reclamos["Estado"].ToString() : String.Empty)) + "'>" + (Reclamos["Estado"] != null && !string.IsNullOrEmpty(Reclamos["Estado"].ToString()) ? Reclamos["Estado"].ToString() : String.Empty) + "</td>" +
                                                "<td>" + (Reclamos["RazonSocial"] != null && !string.IsNullOrEmpty(Reclamos["RazonSocial"].ToString()) ? Reclamos["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                                "<td>" + (Reclamos["CUIT"] != null && !string.IsNullOrEmpty(Reclamos["CUIT"].ToString()) ? Reclamos["CUIT"].ToString() : String.Empty) + "</td>" +
                                                "<td>" + (Reclamos["NumFact"] != null && !string.IsNullOrEmpty(Reclamos["NumFact"].ToString()) ? Reclamos["NumFact"].ToString() : String.Empty) + "</td>" +
                                                "<td>" + DateTime.Parse(Reclamos["Created"].ToString()).ToShortDateString() + "</td>" +
                                            "</tr>";
                }
               
            }
            else
            {
                alertaMasCarac.Visible = false;
                alertaCampoVacio.Visible = true;
                pnlTabla.Visible = false;
                LtTablaFacturas.Text = "";
                txtValorB.Focus();
            }

        }

        protected void BtnCambiarEstado_ServerClick(object sender, EventArgs e)
        {

            int cantRegSel = HiddenField1.Value.Split(',').Length;
            for (int i = 0; i < cantRegSel; i++)
            {
                var id = HiddenField1.Value.Split(',')[i];
                SPListItem RegistroExistente = SPContext.Current.Web.Lists["SegFact"].GetItemById(int.Parse(id));
                string sEstado = cboEstado.SelectedItem.Text;
                string sEventoHistorial = string.Empty;
                RegistroExistente["Estado"] = cboEstado.SelectedItem.Text;
                RegistroExistente["ObservacionesContabilidad"] = (txtRechazo.Text != null && !string.IsNullOrEmpty(txtRechazo.Text) ? txtRechazo.Text : String.Empty);
                RegistroExistente.Update();
             
                switch (sEstado)
                {
                    case "PENDIENTE DE RECEPCIÓN":
                        sEventoHistorial = "Se ha pasado el formulario a Pendiente de recepción";
                          CorreoTesoreriaContabilidad(RegistroExistente);
                        break;
                    case "CONTABILIZADO":
                        sEventoHistorial = "El formulario fue CONTABILIZADO";
                        break;
                    case "RECHAZADO":
                        CorreoRechazado(RegistroExistente);
                        sEventoHistorial = "Se ha rechazado el formulario con el siguiente comentario: " + txtRechazo.Text + "";
                        break;
                    case "RECIBIDO":
                        sEventoHistorial = "El formulario fue RECIBIDO";
                        break;
                }
                GuardarDatosHistorial(RegistroExistente.ID, sEventoHistorial, sEstado);
            }
            Refrescar();
        }
        private void GuardarDatosHistorial(int iIdFormulario, string sEventoHistorial, string sEstado)
        {
            SPListItem Item = SPContext.Current.Web.Lists["SegFactHistorial"].Items.Add();
            Item["IdFormRelacionado"] = iIdFormulario;
            Item["Evento"] = sEventoHistorial;
            Item["Estado"] = sEstado;
            Item.Update();
        }
        private void CorreoTesoreriaContabilidad(SPListItem pasarContabilidad)
        {
            SPFieldUserValue Usuario = new SPFieldUserValue(SPContext.Current.Web, pasarContabilidad["Author"].ToString());
            string sCorreo = "<table style='height: 160px;' border='0' width='606' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                                    "<tr>" +
                                        "<td bgcolor='#0a4e9a'><img src='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/img/Captura.PNG' alt='Logo IEASA' width='606'></td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                            "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>" +
                                                "El formulario " + pasarContabilidad.ID + " del usuario " + Usuario.User.Name + " ha cambiado su estado a PENDIENTE DE RECEPCIÓN" +
                                                "<br><a href='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/Registro.aspx?ID=" + pasarContabilidad.ID + "'>Ir al registro " + pasarContabilidad.ID + "</a>" +
                                            "</p>" +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                            "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Saludos</p>" +
                                        "</td>" +
                                    "</tr>" +
                                "</tbody></table>";

            SPUtility.SendEmail(SPContext.Current.Web, false, false, "cuentasapagar@ieasa.com.ar", pasarContabilidad["CUIT"].ToString() + " - " + pasarContabilidad["NumFact"].ToString(), sCorreo);
            //SPUtility.SendEmail(SPContext.Current.Web, false, false, "gus.rnr@gmail.com", pasarContabilidad["CUIT"].ToString() + " - " + pasarContabilidad["NumFact"].ToString(), sCorreo);
        }
        private void CorreoRechazado(SPListItem RechazaContabilidad)
        {
            string sCorreo = "<table style='height: 160px;' border='0' width='606' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                                  "<tr>" +
                                    "<td bgcolor='#0a4e9a'><img src='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/img/Captura.PNG' alt='Logo IEASA' width='606'></td>" +
                                  "</tr>" +
                                  "<tr>" +
                                        "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                            "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>" +
                                            "El Formulario " + RechazaContabilidad.ID + " fue rechazado." +
                                            "<br>Motivo:" +
                                                "<br>" + RechazaContabilidad["ObservacionesContabilidad"].ToString() + "." +
                                            "<br><br><a href='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/Registro.aspx?ID=" + RechazaContabilidad.ID + "'>Ir al registro " + RechazaContabilidad.ID + "</a>" +
                                            "</p>" +
                                    "</td>" +
                                  "</tr>" +
                                  "<tr>" +
                                      "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                          "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Saludos</p>" +
                                        "</td>" +
                                  "</tr>" +
                            "</tbody></table>";


            SPUtility.SendEmail(SPContext.Current.Web, false, false, RechazaContabilidad["Email"].ToString().Trim(), RechazaContabilidad["CUIT"].ToString() + " - " + RechazaContabilidad["NumFact"].ToString(), sCorreo);
        }
        private void Refrescar()
        {
            pnlTabla.Visible = false;
            LtTablaFacturas.Text = "";
            txtValorB.Focus();
            this.WebPart = this.Parent as WP_Buscador;
            sLimiteVistaBuscador = this.WebPart.PropiedadLimiteVistaBuscador;
            alertaCamposComp.Visible = true;

        }
    }

}
