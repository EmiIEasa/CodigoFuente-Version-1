using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace GrillaDatosPersonal.MisDatos
{
    public partial class MisDatosUserControl : UserControl
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
            string QuerySTR = "<View>" +
                                "<Query><Where><Eq><FieldRef Name='Author' /><Value Type='User'>" + SPContext.Current.Web.CurrentUser.Name.ToString() + "</Value></Eq></Where></Query>" +
                            "<RowLimit>250</RowLimit></View>";
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
