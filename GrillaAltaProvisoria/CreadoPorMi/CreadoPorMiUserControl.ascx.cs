using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace GrillaAltaProvisoria.CreadoPorMi
{
    public partial class CreadoPorMiUserControl : UserControl
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
            
            string QuerySTRUs = "<View><Query><Where><Eq><FieldRef Name='Author' /><Value Type='User'>" + SPContext.Current.Web.CurrentUser.Name.ToString()+ "</Value></Eq></Where></Query></View>";
            SPQuery queryUs = new SPQuery();
            queryUs.ViewXml = QuerySTRUs;
            SPListItemCollection ListaAux = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(queryUs);
            if (ListaAux.Count > 0)
            {
                divNuevoForm.Visible = false;
            }
            else
            {
                divNuevoForm.Visible = true;
            }
            SPQuery query = new SPQuery();
            string QuerySTR = "<View>" +
                                "<Query><Where><And><Eq><FieldRef Name='Estado' /><Value Type='Text'>Pendiente</Value></Eq><Eq><FieldRef Name='Author' /><Value Type='User'>" + SPContext.Current.Web.CurrentUser.Name.ToString() + "</Value></Eq></And></Where></Query>" +
                            "<RowLimit>250</RowLimit></View>";
            query.ViewXml = QuerySTR;
            SPListItemCollection ListaAlta = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(query);
            foreach (SPListItem Facturas in ListaAlta)
            {
                ltTablaPendiente.Text += "<tr>" +
                                            "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/ENARSA/AltaProvisoria.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></td>" +
                                              "<td>" + (Facturas["NombreFantasia"] != null && !string.IsNullOrEmpty(Facturas["NombreFantasia"].ToString()) ? Facturas["NombreFantasia"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["RazonSocial"] != null && !string.IsNullOrEmpty(Facturas["RazonSocial"].ToString()) ? Facturas["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                         
                                            "<td>" + (Facturas["ActividadPrincipal"] != null && !string.IsNullOrEmpty(Facturas["ActividadPrincipal"].ToString()) ? Facturas["ActividadPrincipal"].ToString() : String.Empty) + "</td>" +
                                        "</tr>";
            }
            SPQuery queryAp = new SPQuery();
            string QuerySTRAp = "<View>" +
                                "<Query><Where><And><Eq><FieldRef Name='Estado' /><Value Type='Text'>Aprobado</Value></Eq><Eq><FieldRef Name='Author' /><Value Type='User'>" + SPContext.Current.Web.CurrentUser.Name.ToString() + "</Value></Eq></And></Where></Query>" +
                            "<RowLimit>250</RowLimit></View>";
            queryAp.ViewXml = QuerySTRAp;
            SPListItemCollection ListaAprobados = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(queryAp);
            foreach (SPListItem Facturas in ListaAprobados)
            {
                ltTablaAprobado.Text += "<tr>" +
                                            "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/ENARSA/AltaProvisoria.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></td>" +
                                             "<td>" + (Facturas["NombreFantasia"] != null && !string.IsNullOrEmpty(Facturas["NombreFantasia"].ToString()) ? Facturas["NombreFantasia"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["RazonSocial"] != null && !string.IsNullOrEmpty(Facturas["RazonSocial"].ToString()) ? Facturas["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                           
                                            "<td>" + (Facturas["ActividadPrincipal"] != null && !string.IsNullOrEmpty(Facturas["ActividadPrincipal"].ToString()) ? Facturas["ActividadPrincipal"].ToString() : String.Empty) + "</td>" +
                                        "</tr>";
            }
            SPQuery queryRe = new SPQuery();
            string QuerySTRRe = "<View>" +
                                "<Query><Where><And><Eq><FieldRef Name='Estado' /><Value Type='Text'>Rechazado</Value></Eq><Eq><FieldRef Name='Author' /><Value Type='User'>" + SPContext.Current.Web.CurrentUser.Name.ToString() + "</Value></Eq></And></Where></Query>" +
                            "<RowLimit>250</RowLimit></View>";
            queryRe.ViewXml = QuerySTRRe;
            SPListItemCollection ListaRechazados = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(queryRe);
            foreach (SPListItem Facturas in ListaRechazados)
            {
                ltTablaRechazado.Text += "<tr>" +
                                            "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/ENARSA/AltaProvisoria.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></td>" +
                                             "<td>" + (Facturas["NombreFantasia"] != null && !string.IsNullOrEmpty(Facturas["NombreFantasia"].ToString()) ? Facturas["NombreFantasia"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["RazonSocial"] != null && !string.IsNullOrEmpty(Facturas["RazonSocial"].ToString()) ? Facturas["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                          
                                            "<td>" + (Facturas["ActividadPrincipal"] != null && !string.IsNullOrEmpty(Facturas["ActividadPrincipal"].ToString()) ? Facturas["ActividadPrincipal"].ToString() : String.Empty) + "</td>" +
                                        "</tr>";
            }
        }
    }
}