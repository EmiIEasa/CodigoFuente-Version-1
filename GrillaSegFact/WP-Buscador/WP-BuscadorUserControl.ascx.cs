using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace GrillaSegFact.WP_Buscador
{
    public partial class WP_BuscadorUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                QuerySTR = "<View><Query><Where><Contains><FieldRef Name='" + cboCampos.SelectedValue + "' /><Value Type='Text'>" + txtValorB.Text + "</Value></Contains></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy></Query></View>";
                query.ViewXml = QuerySTR;
                SPListItemCollection ListaReclamos = SPContext.Current.Web.Lists["SegFact"].GetItems(query);
                LtTablaFacturas.Text = "";
                if (ListaReclamos.Count == 0)
                {
                    alertaNoResultado.Visible = true;
                    pnlTabla.Visible = false;
                }
                if (ListaReclamos.Count < 500)
                {
                    alertaMasCarac.Visible = false;
                    pnlTabla.Visible = true;
                    foreach (SPListItem Reclamos in ListaReclamos)
                    {
                        LtTablaFacturas.Text += "<tr>" +
                                                    "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/SegFact/registro.aspx?ID=" + Reclamos.ID.ToString() + "' class='alert-link'>" + Reclamos.ID.ToString() + "</a></td>" +
                                                    "<td style='Background-Color:" + SetColorEstado((Reclamos["Estado"] != null && !string.IsNullOrEmpty(Reclamos["Estado"].ToString()) ? Reclamos["Estado"].ToString() : String.Empty)) + "'>" + (Reclamos["Estado"] != null && !string.IsNullOrEmpty(Reclamos["Estado"].ToString()) ? Reclamos["Estado"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Reclamos["RazonSocial"] != null && !string.IsNullOrEmpty(Reclamos["RazonSocial"].ToString()) ? Reclamos["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Reclamos["CUIT"] != null && !string.IsNullOrEmpty(Reclamos["CUIT"].ToString()) ? Reclamos["CUIT"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Reclamos["NumFact"] != null && !string.IsNullOrEmpty(Reclamos["NumFact"].ToString()) ? Reclamos["NumFact"].ToString() : String.Empty) + "</td>" +
                                                "</tr>";
                    }
                }
                else
                {
                    alertaMasCarac.Visible = true;
                    pnlTabla.Visible = false;
                    LtTablaFacturas.Text = "";
                    txtValorB.Focus();
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
    }
}
