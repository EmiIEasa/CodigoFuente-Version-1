using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Linq;
namespace GrillaDatosPersonal.DatosDelPersonal
{
    public partial class DatosDelPersonalUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (SPContext.Current.Web.CurrentUser.Groups.Cast<SPGroup>().Any(g => g.Name.Equals("RRHH")))
                {
                    CargarTabla();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "RedirectHome();", true);
                }
               
            }
        }
        private void CargarTabla()
        {
            SPQuery query = new SPQuery();
            string QuerySTR = "<View>" +
                                "<Query><Where><IsNotNull><FieldRef Name='Author' /></IsNotNull></Where></Query>" +
                            "<RowLimit>500</RowLimit></View>";
            query.ViewXml = QuerySTR;
            SPListItemCollection ListaDatosPersonal = SPContext.Current.Web.Lists["DatosPersonal"].GetItems(query);
            foreach (SPListItem MisDatos in ListaDatosPersonal)
            {
                ltTablaMisDatos.Text += "<tr>" +
                                            "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/DatosPersonal/Registro.aspx?ID=" + MisDatos.ID.ToString() + "' class='alert-link'>" + (MisDatos["NombreApellido"] != null && !string.IsNullOrEmpty(MisDatos["NombreApellido"].ToString()) ? MisDatos["NombreApellido"].ToString() : String.Empty) + "</a></td>" +
                                            "<td>" + (MisDatos["Legajo"] != null && !string.IsNullOrEmpty(MisDatos["Legajo"].ToString()) ? MisDatos["Legajo"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (MisDatos["CUIL"] != null && !string.IsNullOrEmpty(MisDatos["CUIL"].ToString()) ? MisDatos["CUIL"].ToString() : String.Empty) + "</td>" +
                                        "</tr>";
            }
        }
    }
}
