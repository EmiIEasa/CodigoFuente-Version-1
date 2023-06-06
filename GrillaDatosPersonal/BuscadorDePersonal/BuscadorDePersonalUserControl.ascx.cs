using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace GrillaDatosPersonal.BuscadorDePersonal
{
    public partial class BuscadorDePersonalUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                alertaCamposComp.Visible = true;
            }
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
                QuerySTR = "<View><Query><Where><Contains><FieldRef Name='"+cboCampos.SelectedValue+"' /><Value Type='Text'>"+txtValorB.Text+"</Value></Contains></Where></Query></View>";
                query.ViewXml = QuerySTR;
                SPListItemCollection ListaPersonal = SPContext.Current.Web.Lists["DatosPersonal"].GetItems(query);
                LtTablaPersonal.Text = "";
                if (ListaPersonal.Count == 0)
                {
                    alertaNoResultado.Visible = true;
                    pnlTabla.Visible = false;
                    txtValorB.Focus();
                }
                if (ListaPersonal.Count < 500)
                {
                    alertaMasCarac.Visible = false;
                    pnlTabla.Visible = true;
                    foreach (SPListItem Personal in ListaPersonal)
                    {
                        LtTablaPersonal.Text += "<tr>" +
                                                    "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/DatosPersonal/Registro.aspx?ID=" + Personal.ID.ToString() + "' class='alert-link'>" + (Personal["NombreApellido"] != null && !string.IsNullOrEmpty(Personal["NombreApellido"].ToString()) ? Personal["NombreApellido"].ToString() : String.Empty) + "</a></td>" +
                                                    "<td>" + (Personal["Legajo"] != null && !string.IsNullOrEmpty(Personal["Legajo"].ToString()) ? Personal["Legajo"].ToString() : String.Empty) + "</td>" +
                                                    "<td>" + (Personal["CUIL"] != null && !string.IsNullOrEmpty(Personal["CUIL"].ToString()) ? Personal["CUIL"].ToString() : String.Empty) + "</td>" +
                                                "</tr>";
                    }
                }
                else
                {
                    alertaMasCarac.Visible = true;
                    pnlTabla.Visible = false;
                    LtTablaPersonal.Text = "";
                    txtValorB.Focus();
                }
            }
            else
            {
                alertaMasCarac.Visible = false;
                alertaCampoVacio.Visible = true;
                pnlTabla.Visible = false;
                LtTablaPersonal.Text = "";
                txtValorB.Focus();
            }

        }
    }
}
