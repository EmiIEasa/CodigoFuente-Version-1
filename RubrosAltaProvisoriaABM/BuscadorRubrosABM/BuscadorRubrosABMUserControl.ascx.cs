using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace RubrosAltaProvisoriaABM.BuscadorRubrosABM
{
    public partial class BuscadorRubrosABMUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void buscarRegistro(object sender, EventArgs e)
        {
            LtTablaRama.Text = "";
            if (txtBuscadorRubro.Text != "")
            {
                alertCampoVacio.Visible = false;
                string QuerySTR = string.Empty;
                SPQuery query = new SPQuery();
                QuerySTR = "<View>" +
                                "<Query><Where><Or><Contains><FieldRef Name='Title' /><Value Type='Text'>" + txtBuscadorRubro.Text + "</Value></Contains><Or><Contains><FieldRef Name='RAMA02' /><Value Type='Text'>" + txtBuscadorRubro.Text + "</Value></Contains><Or><Contains><FieldRef Name='RAMA03' /><Value Type='Note'>" + txtBuscadorRubro.Text + "</Value></Contains><Contains><FieldRef Name='RAMA04' /><Value Type='Text'>" + txtBuscadorRubro.Text + "</Value></Contains></Or></Or></Or></Where></Query>" +
                            "</View>";
                query.ViewXml = QuerySTR;
                SPListItemCollection ListaRubro = SPContext.Current.Web.Lists["BienesServiciosAP"].GetItems(query);
                if (ListaRubro.Count < 500)
                {
                    string Estado = string.Empty;
                    alertaMasCarac.Visible = false;
                    pnlTabla.Visible = true;
                    foreach (SPListItem Rubro in ListaRubro)
                    {
                        if (Rubro["Estado"] != null && !string.IsNullOrEmpty(Rubro["Estado"].ToString()))
                        {
                            Estado = (Rubro["Estado"] != null && !string.IsNullOrEmpty(Rubro["Estado"].ToString()) ? Rubro["Estado"].ToString() : String.Empty);
                        }
                        else
                        {
                            Estado = "Activo";
                        }
                            LtTablaRama.Text += "<tr>" +
                                                    "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/RubrosAltaProvisoriaABM/ABMRubros.aspx?ID=" + Rubro["ID"].ToString() + "' class='alert-link' target='_blank'> " + Rubro["ID"].ToString() + "</a></td>" +
                                                    "<td>" + (Rubro["Title"] != null && !string.IsNullOrEmpty(Rubro["Title"].ToString()) ? Rubro["Title"].ToString() : String.Empty) + " - " + (Rubro["RAMA02"] != null && !string.IsNullOrEmpty(Rubro["RAMA02"].ToString()) ? Rubro["RAMA02"].ToString() : String.Empty) + " - " + (Rubro["RAMA03"] != null && !string.IsNullOrEmpty(Rubro["RAMA03"].ToString()) ? Rubro["RAMA03"].ToString() : String.Empty) + " - " + (Rubro["RAMA04"] != null && !string.IsNullOrEmpty(Rubro["RAMA04"].ToString()) ? Rubro["RAMA04"].ToString() : String.Empty) + "</td>" +
                                                    "<td style='Background-Color:" + SetColorEstado(Estado) + "'>" + Estado + "</td>" +
                                                "</tr>";
                    

                    }
                }
                else
                {
                     alertaMasCarac.Visible = true;
                }
            }
            else
            {
                alertCampoVacio.Visible = true;
            }
        }
        private string SetColorEstado(string sEstado)
        {
            string sColor = "#98FB98";

            switch (sEstado)
            {
                case "Activo":
                    sColor = "#98FB98";
                    break;
                case "No Activo":
                    sColor = "#CD5C5C";
                    break;
            }
            return sColor;
        }
    }
}
