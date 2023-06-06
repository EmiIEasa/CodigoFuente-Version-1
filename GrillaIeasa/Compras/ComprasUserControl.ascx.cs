using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace GrillaIeasa.Compras
{
    public partial class ComprasUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                CargarTabla();
            }
        }

        private void CargarTabla()
        {
            SPQuery query = new SPQuery();
            string QuerySTR = "<View>"+
                                "<Query><Where><Eq><FieldRef Name='Estado' /><Value Type='Text'>Pendiente</Value></Eq></Where></Query>"+
                            "<RowLimit>250</RowLimit></View>";
            query.ViewXml = QuerySTR;
            SPListItemCollection ListaAlta = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(query);
            foreach (SPListItem Facturas in ListaAlta)
            {
                ltTablaPendiente.Text += "<tr>" +
                                            "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/Ieasa/AltaProvisoria.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></td>" +
                                            "<td>" + (Facturas["RazonSocial"] != null && !string.IsNullOrEmpty(Facturas["RazonSocial"].ToString()) ? Facturas["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["NombreFantasia"] != null && !string.IsNullOrEmpty(Facturas["NombreFantasia"].ToString()) ? Facturas["NombreFantasia"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["Personeria"] != null && !string.IsNullOrEmpty(Facturas["Personeria"].ToString()) ? Facturas["Personeria"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["ActividadPrincipal"] != null && !string.IsNullOrEmpty(Facturas["ActividadPrincipal"].ToString()) ? Facturas["ActividadPrincipal"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["Rama01"] != null && !string.IsNullOrEmpty(Facturas["Rama01"].ToString()) ? Facturas["Rama01"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["Rama02"] != null && !string.IsNullOrEmpty(Facturas["Rama02"].ToString()) ? Facturas["Rama02"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["Rama03"] != null && !string.IsNullOrEmpty(Facturas["Rama03"].ToString()) ? Facturas["Rama03"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["Rama04"] != null && !string.IsNullOrEmpty(Facturas["Rama04"].ToString()) ? Facturas["Rama04"].ToString() : String.Empty) + "</td>" +
                                        "</tr>";
            }
            SPQuery queryAp = new SPQuery();
            string QuerySTRAp = "<View>" +
                                "<Query><Where><Eq><FieldRef Name='Estado' /><Value Type='Text'>Aprobado</Value></Eq></Where></Query>" +
                            "<RowLimit>250</RowLimit></View>";
            queryAp.ViewXml = QuerySTRAp;
            SPListItemCollection ListaAprobados = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(query);
            foreach (SPListItem Facturas in ListaAprobados)
            {
                ltTablaAprobado.Text += "<tr>" +
                                            "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/Ieasa/AltaProvisoria.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></td>" +
                                            "<td>" + (Facturas["RazonSocial"] != null && !string.IsNullOrEmpty(Facturas["RazonSocial"].ToString()) ? Facturas["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["NombreFantasia"] != null && !string.IsNullOrEmpty(Facturas["NombreFantasia"].ToString()) ? Facturas["NombreFantasia"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["Personeria"] != null && !string.IsNullOrEmpty(Facturas["Personeria"].ToString()) ? Facturas["Personeria"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["ActividadPrincipal"] != null && !string.IsNullOrEmpty(Facturas["ActividadPrincipal"].ToString()) ? Facturas["ActividadPrincipal"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["Rama01"] != null && !string.IsNullOrEmpty(Facturas["Rama01"].ToString()) ? Facturas["Rama01"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["Rama02"] != null && !string.IsNullOrEmpty(Facturas["Rama02"].ToString()) ? Facturas["Rama02"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["Rama03"] != null && !string.IsNullOrEmpty(Facturas["Rama03"].ToString()) ? Facturas["Rama03"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["Rama04"] != null && !string.IsNullOrEmpty(Facturas["Rama04"].ToString()) ? Facturas["Rama04"].ToString() : String.Empty) + "</td>" +
                                        "</tr>";
            }
            SPQuery queryRe = new SPQuery();
            string QuerySTRRe = "<View>" +
                                "<Query><Where><Eq><FieldRef Name='Estado' /><Value Type='Text'>Aprobado</Value></Eq></Where></Query>" +
                            "<RowLimit>250</RowLimit></View>";
            queryRe.ViewXml = QuerySTRRe;
            SPListItemCollection ListaRechazados = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(query);
            foreach (SPListItem Facturas in ListaRechazados)
            {
                ltTablaRechazado.Text += "<tr>" +
                                            "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/Ieasa/AltaProvisoria.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></td>" +
                                            "<td>" + (Facturas["RazonSocial"] != null && !string.IsNullOrEmpty(Facturas["RazonSocial"].ToString()) ? Facturas["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["NombreFantasia"] != null && !string.IsNullOrEmpty(Facturas["NombreFantasia"].ToString()) ? Facturas["NombreFantasia"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["Personeria"] != null && !string.IsNullOrEmpty(Facturas["Personeria"].ToString()) ? Facturas["Personeria"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["ActividadPrincipal"] != null && !string.IsNullOrEmpty(Facturas["ActividadPrincipal"].ToString()) ? Facturas["ActividadPrincipal"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["Rama01"] != null && !string.IsNullOrEmpty(Facturas["Rama01"].ToString()) ? Facturas["Rama01"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["Rama02"] != null && !string.IsNullOrEmpty(Facturas["Rama02"].ToString()) ? Facturas["Rama02"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["Rama03"] != null && !string.IsNullOrEmpty(Facturas["Rama03"].ToString()) ? Facturas["Rama03"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["Rama04"] != null && !string.IsNullOrEmpty(Facturas["Rama04"].ToString()) ? Facturas["Rama04"].ToString() : String.Empty) + "</td>" +
                                        "</tr>";
            }
        }
    }
}
