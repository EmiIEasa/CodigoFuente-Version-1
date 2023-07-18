using System;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace GrillaSegFact.WP_Contabilidad
{ 
    public partial class WP_ContabilidadUserControl : UserControl
    {
        public WP_Contabilidad WebPart { get; set; }
        public string sLimiteVista = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.WebPart = this.Parent as WP_Contabilidad;
                sLimiteVista = this.WebPart.PropiedadLimiteVista;
                CargarTabla();
                txtHoraActualizacion.Text = DateTime.Now.ToString("hh:mm:ss tt");
                //  Refrescar();
            }
        }
        protected void GetTime(object sender, EventArgs e)
        {
            ltTablaContabilidadConOC.Text = "";
            ltTablaContabilidadSinOC.Text = "";
            this.WebPart = this.Parent as WP_Contabilidad;
            sLimiteVista = this.WebPart.PropiedadLimiteVista;
            CargarTabla();
            txtHoraActualizacion.Text = DateTime.Now.ToString("hh:mm:ss tt");
            // Refrescar();
        }

        private void Refrescar()
        {
            ltTablaContabilidadConOC.Text = "";
            ltTablaContabilidadSinOC.Text = "";
            this.WebPart = this.Parent as WP_Contabilidad;
            sLimiteVista = this.WebPart.PropiedadLimiteVista;
            CargarTabla();
            txtHoraActualizacion.Text = DateTime.Now.ToString("hh:mm:ss tt");
           

        }
        private void CargarTabla()
        {
            SPQuery query = new SPQuery();
            string QuerySTR = "<View><Query><Where><And><Eq><FieldRef Name='OC' /><Value Type='Text'>Con OC</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIZADO</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIDAD</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>TESORERIA</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>RECHAZADO</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>PENDIENTE DE RECEPCIÓN</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>RECIBIDO</Value></Eq></Or></Or></Or></Or></Or></And></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy></Query><RowLimit>" + sLimiteVista +"</RowLimit></View>";
            //string QuerySTR = "<View><Query><Where><And><Eq><FieldRef Name='OC' /><Value Type='Text'>Con OC</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIZADO</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIDAD</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>TESORERIA</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>RECIBIDO</Value></Eq></Or></Or></Or></And></Where></Query><RowLimit>500</RowLimit></View>";
            query.ViewXml = QuerySTR;
            SPListItemCollection ListaFact = SPContext.Current.Web.Lists["SegFact"].GetItems(query);
            foreach (SPListItem Facturas in ListaFact)
            {
                ltTablaContabilidadConOC.Text += "<tr>" +
                                                    "<td>" + "<div class='form-check'><label class='form-check-label' for='check1'><input type='checkbox' class='form-check-input' id='check1' name='option1' value='" + Facturas.ID.ToString() + "'><a href='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/registro.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></label></div></td>" +
                                                    "<td style='Background-Color:" + SetColorEstado((Facturas["Estado"] != null && !string.IsNullOrEmpty(Facturas["Estado"].ToString()) ? Facturas["Estado"].ToString() : String.Empty)) + "'>" + (Facturas["Estado"] != null && !string.IsNullOrEmpty(Facturas["Estado"].ToString()) ? Facturas["Estado"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["RazonSocial"] != null && !string.IsNullOrEmpty(Facturas["RazonSocial"].ToString()) ? Facturas["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["CUIT"] != null && !string.IsNullOrEmpty(Facturas["CUIT"].ToString()) ? Facturas["CUIT"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["FechaComprobante"] != null && !string.IsNullOrEmpty(Facturas["FechaComprobante"].ToString()) ? Facturas["FechaComprobante"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + DateTime.Parse(Facturas["Created"].ToString()).ToShortDateString() + "</td>" +
                                                    "<td>" + (Facturas["NumFact"] != null && !string.IsNullOrEmpty(Facturas["NumFact"].ToString()) ? Facturas["NumFact"].ToString() : String.Empty) + "</td>" +
                    //"<td>" + (Facturas["OC"] != null && !string.IsNullOrEmpty(Facturas["OC"].ToString()) ? Facturas["OC"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["NumOC"] != null && !string.IsNullOrEmpty(Facturas["NumOC"].ToString()) ? Facturas["NumOC"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["Monto"] != null && !string.IsNullOrEmpty(Facturas["Monto"].ToString()) ? Facturas["Monto"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["Sociedad"] != null && !string.IsNullOrEmpty(Facturas["Sociedad"].ToString()) ? Facturas["Sociedad"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["Observaciones"] != null && !string.IsNullOrEmpty(Facturas["Observaciones"].ToString()) ? Facturas["Observaciones"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["ObservacionesContabilidad"] != null && !string.IsNullOrEmpty(Facturas["ObservacionesContabilidad"].ToString()) ? Facturas["ObservacionesContabilidad"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["NumOrdenPago"] != null && !string.IsNullOrEmpty(Facturas["NumOrdenPago"].ToString()) ? Facturas["NumOrdenPago"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["NumContabilizado"] != null && !string.IsNullOrEmpty(Facturas["NumContabilizado"].ToString()) ? Facturas["NumContabilizado"].ToString() : String.Empty) + "</td>" +
                                             "</tr>";
            }
            SPQuery querySOC = new SPQuery();
            string QuerySTRSOC = "<View><Query><Where><And><Eq><FieldRef Name='OC' /><Value Type='Text'>Sin OC</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIZADO</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIDAD</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>TESORERIA</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>RECHAZADO</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>PENDIENTE DE RECEPCIÓN</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>RECIBIDO</Value></Eq></Or></Or></Or></Or></Or></And></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy></Query><RowLimit>" + sLimiteVista+"</RowLimit></View>";
            //string QuerySTRSOC = "<View><Query><Where><And><Eq><FieldRef Name='OC' /><Value Type='Text'>Sin OC</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIZADO</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIDAD</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>TESORERIA</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>RECIBIDO</Value></Eq></Or></Or></Or></And></Where></Query><RowLimit>500</RowLimit></View>";
            querySOC.ViewXml = QuerySTRSOC;
            SPListItemCollection ListaFactSOC = SPContext.Current.Web.Lists["SegFact"].GetItems(querySOC);
            foreach (SPListItem Facturas in ListaFactSOC)
            {
                ltTablaContabilidadSinOC.Text += "<tr>" +
                                                    "<td>" + "<div class='form-check'><label class='form-check-label' for='check1'><input type='checkbox' class='form-check-input' id='check1' name='option1' value='" + Facturas.ID.ToString() + "'><a href='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/registro.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></label></div></td>" +
                                                    "<td style='Background-Color:" + SetColorEstado((Facturas["Estado"] != null && !string.IsNullOrEmpty(Facturas["Estado"].ToString()) ? Facturas["Estado"].ToString() : String.Empty)) + "'>" + (Facturas["Estado"] != null && !string.IsNullOrEmpty(Facturas["Estado"].ToString()) ? Facturas["Estado"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["RazonSocial"] != null && !string.IsNullOrEmpty(Facturas["RazonSocial"].ToString()) ? Facturas["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["CUIT"] != null && !string.IsNullOrEmpty(Facturas["CUIT"].ToString()) ? Facturas["CUIT"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["FechaComprobante"] != null && !string.IsNullOrEmpty(Facturas["FechaComprobante"].ToString()) ? Facturas["FechaComprobante"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + DateTime.Parse(Facturas["Created"].ToString()).ToShortDateString() + "</td>" +
                                                    "<td>" + (Facturas["NumFact"] != null && !string.IsNullOrEmpty(Facturas["NumFact"].ToString()) ? Facturas["NumFact"].ToString() : String.Empty) + "</td>" +
                    //"<td>" + (Facturas["OC"] != null && !string.IsNullOrEmpty(Facturas["OC"].ToString()) ? Facturas["OC"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["NumOC"] != null && !string.IsNullOrEmpty(Facturas["NumOC"].ToString()) ? Facturas["NumOC"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["Monto"] != null && !string.IsNullOrEmpty(Facturas["Monto"].ToString()) ? Facturas["Monto"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["Sociedad"] != null && !string.IsNullOrEmpty(Facturas["Sociedad"].ToString()) ? Facturas["Sociedad"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["Observaciones"] != null && !string.IsNullOrEmpty(Facturas["Observaciones"].ToString()) ? Facturas["Observaciones"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["ObservacionesContabilidad"] != null && !string.IsNullOrEmpty(Facturas["ObservacionesContabilidad"].ToString()) ? Facturas["ObservacionesContabilidad"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["NumOrdenPago"] != null && !string.IsNullOrEmpty(Facturas["NumOrdenPago"].ToString()) ? Facturas["NumOrdenPago"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["NumContabilizado"] != null && !string.IsNullOrEmpty(Facturas["NumContabilizado"].ToString()) ? Facturas["NumContabilizado"].ToString() : String.Empty) + "</td>" +
                                             "</tr>";
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
                Refrescar();
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
        private void GuardarDatosHistorial(int iIdFormulario, string sEventoHistorial, string sEstado)
        {
            SPListItem Item = SPContext.Current.Web.Lists["SegFactHistorial"].Items.Add();
            Item["IdFormRelacionado"] = iIdFormulario;
            Item["Evento"] = sEventoHistorial;
            Item["Estado"] = sEstado;
            Item.Update();
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

     
    }
}
