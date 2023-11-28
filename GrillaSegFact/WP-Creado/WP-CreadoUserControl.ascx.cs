using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace GrillaSegFact.WP_Creado
{
    public partial class WP_CreadoUserControl : UserControl
    {
        public WP_Creado WebPart { get; set; }
        public string sLimiteVista = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.WebPart = this.Parent as WP_Creado;
                sLimiteVista = this.WebPart.PropiedadLimiteVista;
                CargarTabla();
            }
        }

        private void CargarTabla()
        {
            SPQuery query = new SPQuery();
            string QuerySTR = "<View><Query><Where><And><Eq><FieldRef Name='Author' /><Value Type='User'>" + SPContext.Current.Web.CurrentUser.Name.ToString() + "</Value></Eq><Eq><FieldRef Name='OC' /><Value Type='Text'>Con OC</Value></Eq></And></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy></Query><RowLimit>"+ sLimiteVista + "</RowLimit></View>";
            query.ViewXml = QuerySTR;
            SPListItemCollection ListaFact = SPContext.Current.Web.Lists["SegFact"].GetItems(query);
            foreach (SPListItem Facturas in ListaFact)
            {
                ltTablaCreadoPorMiCO.Text += "<tr>" +
                                                    "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/registro.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></td>" +
                                                    "<td style='Background-Color:" + SetColorEstado((Facturas["Estado"] != null && !string.IsNullOrEmpty(Facturas["Estado"].ToString()) ? Facturas["Estado"].ToString() : String.Empty)) + "'>" + (Facturas["Estado"] != null && !string.IsNullOrEmpty(Facturas["Estado"].ToString()) ? Facturas["Estado"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["RazonSocial"] != null && !string.IsNullOrEmpty(Facturas["RazonSocial"].ToString()) ? Facturas["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["CUIT"] != null && !string.IsNullOrEmpty(Facturas["CUIT"].ToString()) ? Facturas["CUIT"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["NumFact"] != null && !string.IsNullOrEmpty(Facturas["NumFact"].ToString()) ? Facturas["NumFact"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["NumOC"] != null && !string.IsNullOrEmpty(Facturas["NumOC"].ToString()) ? Facturas["NumOC"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["ObservacionesContabilidad"] != null && !string.IsNullOrEmpty(Facturas["ObservacionesContabilidad"].ToString()) ? Facturas["ObservacionesContabilidad"].ToString() : String.Empty) + "</td>" +
                                                    //"<td>" + (Facturas["CertServ"] != null && !string.IsNullOrEmpty(Facturas["CertServ"].ToString()) ? Facturas["CertServ"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["Monto"] != null && !string.IsNullOrEmpty(Facturas["Monto"].ToString()) ? Facturas["Monto"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Facturas["Sociedad"] != null && !string.IsNullOrEmpty(Facturas["Sociedad"].ToString()) ? Facturas["Sociedad"].ToString() : String.Empty) + "</td>" +
                                             "</tr>";
            }
            SPQuery querySO = new SPQuery();
            string QuerySTRSO = "<View><Query><Where><And><Eq><FieldRef Name='Author' /><Value Type='User'>" + SPContext.Current.Web.CurrentUser.Name.ToString() + "</Value></Eq><Eq><FieldRef Name='OC' /><Value Type='Text'>Sin OC</Value></Eq></And></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy></Query><RowLimit>" + sLimiteVista + "</RowLimit></View>";
            querySO.ViewXml = QuerySTRSO;
            SPListItemCollection ListaFactSO = SPContext.Current.Web.Lists["SegFact"].GetItems(querySO);
            foreach (SPListItem FactSO in ListaFactSO)
            {
                ltTablaCreadoPorMiSO.Text += "<tr>" +
                                                    "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/registro.aspx?ID=" + FactSO.ID.ToString() + "' class='alert-link'>" + FactSO.ID.ToString() + "</a></td>" +
                                                    "<td style='Background-Color:" + SetColorEstado((FactSO["Estado"] != null && !string.IsNullOrEmpty(FactSO["Estado"].ToString()) ? FactSO["Estado"].ToString() : String.Empty)) + "'>" + (FactSO["Estado"] != null && !string.IsNullOrEmpty(FactSO["Estado"].ToString()) ? FactSO["Estado"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (FactSO["RazonSocial"] != null && !string.IsNullOrEmpty(FactSO["RazonSocial"].ToString()) ? FactSO["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (FactSO["CUIT"] != null && !string.IsNullOrEmpty(FactSO["CUIT"].ToString()) ? FactSO["CUIT"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (FactSO["NumFact"] != null && !string.IsNullOrEmpty(FactSO["NumFact"].ToString()) ? FactSO["NumFact"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (FactSO["Monto"] != null && !string.IsNullOrEmpty(FactSO["Monto"].ToString()) ? FactSO["Monto"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (FactSO["Sociedad"] != null && !string.IsNullOrEmpty(FactSO["Sociedad"].ToString()) ? FactSO["Sociedad"].ToString() : String.Empty) + "</td>" +
                                                      "<td>" + (FactSO["ObservacionesContabilidad"] != null && !string.IsNullOrEmpty(FactSO["ObservacionesContabilidad"].ToString()) ? FactSO["ObservacionesContabilidad"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (FactSO["Email"] != null && !string.IsNullOrEmpty(FactSO["Email"].ToString()) ? FactSO["Email"].ToString() : String.Empty) + "</td>" +
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
