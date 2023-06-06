using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;

namespace RubrosAltaProvisoriaABM.Layouts.RubrosAltaProvisoriaABM
{
    public partial class ABMRubros : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rowID.Visible = false;
                CargarInicial();
            }
        }

        private void CargarInicial()
        {
            if (Request.QueryString["ID"] != null)
            {
                SPListItem Rubros = SPContext.Current.Web.Lists["BienesServiciosAP"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                CargarDatos(Rubros);
                rowID.Visible = true;
            }
        }

        private void CargarDatos(SPListItem Rubros)
        {
            txtID.Text = Rubros["ID"].ToString();
            txtRubro01.Text = Rubros["Title"] != null && !string.IsNullOrEmpty(Rubros["Title"].ToString()) ? Rubros["Title"].ToString() : String.Empty;
            txtRubro02.Text = Rubros["RAMA02"] != null && !string.IsNullOrEmpty(Rubros["RAMA02"].ToString()) ? Rubros["RAMA02"].ToString() : String.Empty;
            txtRubro03.Text = Rubros["RAMA03"] != null && !string.IsNullOrEmpty(Rubros["RAMA03"].ToString()) ? Rubros["RAMA03"].ToString() : String.Empty;
            txtRubro04.Text = Rubros["RAMA04"] != null && !string.IsNullOrEmpty(Rubros["RAMA04"].ToString()) ? Rubros["RAMA04"].ToString() : String.Empty;
            if (Rubros["Estado"] != null && !string.IsNullOrEmpty(Rubros["Estado"].ToString()))
            {
                cboEstado.SelectedValue = (Rubros["Estado"] != null && !string.IsNullOrEmpty(Rubros["Estado"].ToString()) ? Rubros["Estado"].ToString() : String.Empty);
            }
            else
            {
                cboEstado.SelectedValue = "Activo";
            }
        }

       
        protected void btnGuardar(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                SPListItem RegistroExistente = SPContext.Current.Web.Lists["BienesServiciosAP"].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                GuardarDatos(RegistroExistente);
                RegistroExistente.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeGuarda();", true);
            }
            else
            {
                SPListItem RegistroNuevo = SPContext.Current.Web.Lists["BienesServiciosAP"].AddItem();
                GuardarDatos(RegistroNuevo);
                RegistroNuevo.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeGuarda();", true);
            }
        }

        private void GuardarDatos(SPListItem Registro)
        {
            Registro["Title"] = (txtRubro01.Text != null && !string.IsNullOrEmpty(txtRubro01.Text) ? txtRubro01.Text : String.Empty);
            Registro["RAMA02"] = (txtRubro02.Text != null && !string.IsNullOrEmpty(txtRubro02.Text) ? txtRubro02.Text : String.Empty);
            Registro["RAMA03"] = (txtRubro03.Text != null && !string.IsNullOrEmpty(txtRubro03.Text) ? txtRubro03.Text : String.Empty);
            Registro["RAMA04"] = (txtRubro04.Text != null && !string.IsNullOrEmpty(txtRubro04.Text) ? txtRubro04.Text : String.Empty);
            Registro["Estado"] = cboEstado.SelectedValue;
        }
    }
}
