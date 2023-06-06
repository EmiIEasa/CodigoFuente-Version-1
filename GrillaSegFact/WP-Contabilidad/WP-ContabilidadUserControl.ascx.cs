using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace GrillaSegFact.WP_Contabilidad
{
    public partial class WP_ContabilidadUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                CargarTabla();
                txtHoraActualizacion.Text = DateTime.Now.ToString("hh:mm:ss tt");
            }
        }
        protected void GetTime(object sender, EventArgs e)
        {
            ltTablaContabilidadConOC.Text = "";
            ltTablaContabilidadSinOC.Text = "";
            txtHoraActualizacion.Text = DateTime.Now.ToString("hh:mm:ss tt");
            SPQuery query = new SPQuery();
            //string QuerySTR = "<View><Query><Where><And><Eq><FieldRef Name='OC' /><Value Type='Text'>Con OC</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIZADO</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIDAD</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>TESORERIA</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>RECIBIDO</Value></Eq></Or></Or></Or></And></Where></Query><RowLimit>500</RowLimit></View>";
            string QuerySTR = "<View><Query><Where><And><Eq><FieldRef Name='OC' /><Value Type='Text'>Con OC</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIZADO</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIDAD</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>TESORERIA</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>RECIBIDO</Value></Eq></Or></Or></Or></And></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy></Query><RowLimit>500</RowLimit></View>";
            query.ViewXml = QuerySTR;
            SPListItemCollection ListaFact = SPContext.Current.Web.Lists["SegFact"].GetItems(query);
            foreach (SPListItem Facturas in ListaFact)
            {
                ltTablaContabilidadConOC.Text += "<tr>" +
                                                    "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/registro.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></td>" +
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
            string QuerySTRSOC = "<View><Query><Where><And><Eq><FieldRef Name='OC' /><Value Type='Text'>Sin OC</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIZADO</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIDAD</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>TESORERIA</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>RECIBIDO</Value></Eq></Or></Or></Or></And></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy></Query><RowLimit>500</RowLimit></View>";
            //string QuerySTRSOC = "<View><Query><Where><And><Eq><FieldRef Name='OC' /><Value Type='Text'>Sin OC</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIZADO</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIDAD</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>TESORERIA</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>RECIBIDO</Value></Eq></Or></Or></Or></And></Where></Query><RowLimit>500</RowLimit></View>";
            querySOC.ViewXml = QuerySTRSOC;
            SPListItemCollection ListaFactSOC = SPContext.Current.Web.Lists["SegFact"].GetItems(querySOC);
            foreach (SPListItem Facturas in ListaFactSOC)
            {
                ltTablaContabilidadSinOC.Text += "<tr>" +
                                                    "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/registro.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></td>" +
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

        private void CargarTabla()
        {
            SPQuery query = new SPQuery();
            string QuerySTR = "<View><Query><Where><And><Eq><FieldRef Name='OC' /><Value Type='Text'>Con OC</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIZADO</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIDAD</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>TESORERIA</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>RECIBIDO</Value></Eq></Or></Or></Or></And></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy></Query><RowLimit>500</RowLimit></View>";
            //string QuerySTR = "<View><Query><Where><And><Eq><FieldRef Name='OC' /><Value Type='Text'>Con OC</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIZADO</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIDAD</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>TESORERIA</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>RECIBIDO</Value></Eq></Or></Or></Or></And></Where></Query><RowLimit>500</RowLimit></View>";
            query.ViewXml = QuerySTR;
            SPListItemCollection ListaFact = SPContext.Current.Web.Lists["SegFact"].GetItems(query);
            foreach (SPListItem Facturas in ListaFact)
            {
                ltTablaContabilidadConOC.Text += "<tr>" +
                                                    "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/registro.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></td>" +
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
            string QuerySTRSOC = "<View><Query><Where><And><Eq><FieldRef Name='OC' /><Value Type='Text'>Sin OC</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIZADO</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIDAD</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>TESORERIA</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>RECIBIDO</Value></Eq></Or></Or></Or></And></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy></Query><RowLimit>500</RowLimit></View>";
            //string QuerySTRSOC = "<View><Query><Where><And><Eq><FieldRef Name='OC' /><Value Type='Text'>Sin OC</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIZADO</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>CONTABILIDAD</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>TESORERIA</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>RECIBIDO</Value></Eq></Or></Or></Or></And></Where></Query><RowLimit>500</RowLimit></View>";
            querySOC.ViewXml = QuerySTRSOC;
            SPListItemCollection ListaFactSOC = SPContext.Current.Web.Lists["SegFact"].GetItems(querySOC);
            foreach (SPListItem Facturas in ListaFactSOC)
            {
                ltTablaContabilidadSinOC.Text += "<tr>" +
                                                    "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/registro.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></td>" +
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
                    sColor = "#FFDEAD";
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
