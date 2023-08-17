using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;

namespace GrillaAltaProvisoria.BuscadorRubros
{
    public partial class BuscadorRubrosUserControl : UserControl
    {
        string strSeleccione = "Seleccione";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombo();
            }
        }

        private void CargarCombo()
        {
            //SPListItemCollection Rubro01 = SPContext.Current.Web.Lists["AltaProvisoriaRubros"].GetItems();
            //if (Rubro01.Count > 0)
            //{
            //    int contador = Rubro01.Count + 1;
            //    DataView view1 = new DataView(Rubro01.GetDataTable());
            //    DataTable distinctValues1 = view1.ToTable(true, "Title");
            //    DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
            //    rFilaSeleccioneSolicitud["Title"] = strSeleccione;
            //    distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
                
            //    cboRama1.DataSource = distinctValues1;
            //    cboRama1.DataValueField = "Title";
            //    cboRama1.DataTextField = "Title";
            //    cboRama1.DataBind();
            //}
            SPQuery queryRama01 = new SPQuery();
            string QuerySTRRama01 = "<View>" +
                "<Query><Where><And><IsNotNull><FieldRef Name='Title' /></IsNotNull><Neq><FieldRef Name='Estado' /><Value Type='Text'>No Activo</Value></Neq></And></Where></Query>" +

                    "</View>";
            queryRama01.ViewXml = QuerySTRRama01;
            SPListItemCollection Rama01 = SPContext.Current.Web.Lists["BienesServiciosAP"].GetItems(queryRama01);
            cboRama1.Items.Add(new ListItem(strSeleccione));
            foreach (SPListItem item in Rama01)
            {
                cboRama1.Items.Add(new ListItem((item["Title"] != null && !string.IsNullOrEmpty(item["Title"].ToString()) ? item["Title"].ToString() : String.Empty) + " - " + (item["RAMA02"] != null && !string.IsNullOrEmpty(item["RAMA02"].ToString()) ? item["RAMA02"].ToString() : String.Empty) + " - " + (item["RAMA03"] != null && !string.IsNullOrEmpty(item["RAMA03"].ToString()) ? item["RAMA03"].ToString() : String.Empty) + " - " + (item["RAMA04"] != null && !string.IsNullOrEmpty(item["RAMA04"].ToString()) ? item["RAMA04"].ToString() : String.Empty)));
            }
        }

        //protected void cboRama1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    pnlTabla.Visible = false;
        //    if (cboRama1.SelectedValue != "Seleccione")
        //    {
        //        if (cboRama1.SelectedValue != "Todos")
        //        {
        //            divRama2.Visible = true;
        //            SPQuery query = new SPQuery();
        //            string QuerySTR = "<View>" +
        //                                "<Query><Where><Eq><FieldRef Name='Rama01' /><Value Type='Text'>" + cboRama1.SelectedValue + "</Value></Eq></Where></Query>" +
        //                            "</View>";
        //            query.ViewXml = QuerySTR;
        //            SPListItemCollection Pais = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(query);
        //            if (Pais.Count > 0)
        //            {
        //                int contador = Pais.Count + 1;
        //                DataView view1 = new DataView(Pais.GetDataTable());
        //                DataTable distinctValues1 = view1.ToTable(true, "Rama02");
        //                DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
        //                rFilaSeleccioneSolicitud["Rama02"] = strSeleccione;
        //                distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
        //                DataRow rFilaAgregar = distinctValues1.NewRow();
        //                rFilaAgregar["Rama02"] = "Todos";
        //                distinctValues1.Rows.InsertAt(rFilaAgregar, contador);
        //                cboRama2.DataSource = distinctValues1;
        //                cboRama2.DataValueField = "Rama02";
        //                cboRama2.DataTextField = "Rama02";
        //                cboRama2.DataBind();
        //            }
        //        }
        //        else
        //        {
        //            divRama2.Visible = false;
        //            divRama3.Visible = false;
        //            divRama4.Visible = false;
        //            btnBuscar.Disabled = false;
        //        }
        //    }

        //}

        //protected void cboRama2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    pnlTabla.Visible = false;
        //    if (cboRama2.SelectedValue != "Seleccione")
        //    {
        //        if (cboRama2.SelectedValue != "Todos")
        //        {
        //            divRama3.Visible = true;
        //            SPQuery query = new SPQuery();
        //            string QuerySTR = "<View>" +
        //                                "<Query><Where><And><Eq><FieldRef Name='Rama01' /><Value Type='Text'>" + cboRama1.SelectedValue + "</Value></Eq><Eq><FieldRef Name='Rama02' /><Value Type='Text'>" + cboRama2.SelectedValue + "</Value></Eq></And></Where></Query>" +
        //                            "</View>";
        //            query.ViewXml = QuerySTR;
        //            SPListItemCollection Pais = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(query);
        //            if (Pais.Count > 0)
        //            {
        //                int contador = Pais.Count + 1;
        //                DataView view1 = new DataView(Pais.GetDataTable());
        //                DataTable distinctValues1 = view1.ToTable(true, "Rama03");
        //                DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
        //                rFilaSeleccioneSolicitud["Rama03"] = strSeleccione;
        //                distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
        //                DataRow rFilaAgregar = distinctValues1.NewRow();
        //                rFilaAgregar["Rama03"] = "Todos";
        //                distinctValues1.Rows.InsertAt(rFilaAgregar, contador);
        //                cboRama3.DataSource = distinctValues1;
        //                cboRama3.DataValueField = "Rama03";
        //                cboRama3.DataTextField = "Rama03";
        //                cboRama3.DataBind();
        //            }
        //        }
        //        else
        //        {
        //            divRama3.Visible = false;
        //            divRama4.Visible = false;
        //            btnBuscar.Disabled = false;
        //        }
        //    }
        //}

        //protected void cboRama3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    pnlTabla.Visible = false;
        //    if (cboRama3.SelectedValue != "Seleccione")
        //    {
        //        if (cboRama3.SelectedValue != "Todos")
        //        {
        //            divRama4.Visible = true;
        //            SPQuery query = new SPQuery();
        //            string QuerySTR = "<View>" +
        //                                "<Query><Where><And><Eq><FieldRef Name='Rama01' /><Value Type='Text'>" + cboRama1.SelectedValue + "</Value></Eq><And><Eq><FieldRef Name='Rama02' /><Value Type='Text'>" + cboRama2.SelectedValue + "</Value></Eq><Eq><FieldRef Name='Rama03' /><Value Type='Text'>" + cboRama3.SelectedValue + "</Value></Eq></And></And></Where></Query>" +
        //                            "</View>";
        //            query.ViewXml = QuerySTR;
        //            SPListItemCollection Pais = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(query);
        //            if (Pais.Count > 0)
        //            {
        //                int contador = Pais.Count + 1;
        //                DataView view1 = new DataView(Pais.GetDataTable());
        //                DataTable distinctValues1 = view1.ToTable(true, "Rama04");
        //                DataRow rFilaSeleccioneSolicitud = distinctValues1.NewRow();
        //                rFilaSeleccioneSolicitud["Rama04"] = strSeleccione;
        //                distinctValues1.Rows.InsertAt(rFilaSeleccioneSolicitud, 0);
        //                DataRow rFilaAgregar = distinctValues1.NewRow();
        //                rFilaAgregar["Rama04"] = "Todos";
        //                distinctValues1.Rows.InsertAt(rFilaAgregar, contador);
        //                cboRama4.DataSource = distinctValues1;
        //                cboRama4.DataValueField = "Rama04";
        //                cboRama4.DataTextField = "Rama04";
        //                cboRama4.DataBind();
        //            }
        //        }
        //        else
        //        {
        //            divRama4.Visible = false;
        //            btnBuscar.Disabled = false;
        //        }
        //    }
        //}
        //protected void cboRama4_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    pnlTabla.Visible = false;
        //    if (cboRama4.SelectedValue != "Seleccione")
        //    {
        //        btnBuscar.Disabled = false;
        //    }
        //}
        protected void buscarRegistro(object sender, EventArgs e)
        {
            LtTablaRama.Text = "";
            alertaMasCarac.Visible = false;
            if (cboRama1.SelectedValue != "Seleccione")
            {
                {
                    string QuerySTR = string.Empty;
                    SPQuery query = new SPQuery();
                    QuerySTR = "<View>" +
                                    "<Query><Where><Eq><FieldRef Name='Title' /><Value Type='Text'>"+cboRama1.SelectedValue+"</Value></Eq></Where></Query>"+
                                "</View>";
                    query.ViewXml = QuerySTR;
                    SPListItemCollection ListaRubro = SPContext.Current.Web.Lists["AltaProvisoriaRubros"].GetItems(query);
                    if (ListaRubro.Count > 0)
                    {
                        //if (ListaRubro.Count < 500)
                        //{
                        alertaMasCarac.Visible = false;
                        pnlTabla.Visible = true;
                        foreach (SPListItem Rubro in ListaRubro)
                        {
                            SPListItem Item = SPContext.Current.Web.Lists["AltaProvisoria"].GetItemById(int.Parse(Rubro["IDRegistro"].ToString()));
                            if (Item != null)
                            {
                                LtTablaRama.Text += "<tr>" +
                                                        "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/ENARSA/altaprovisoria.aspx?ID=" + Rubro["IDRegistro"].ToString() + "' class='alert-link' target='_blank'> " + Rubro["IDRegistro"].ToString() + "</a></td>" +
                                                        "<td>" + (Item["Title"] != null && !string.IsNullOrEmpty(Item["Title"].ToString()) ? Item["Title"].ToString() : String.Empty) + "</td>" +
                                                        "<td style='Background-Color:" + SetColorEstado((Item["Estado"] != null && !string.IsNullOrEmpty(Item["Estado"].ToString()) ? Item["Estado"].ToString() : String.Empty)) + "'>" + (Item["Estado"] != null && !string.IsNullOrEmpty(Item["Estado"].ToString()) ? Item["Estado"].ToString() : String.Empty) + "</td>" +
                                                        "<td>" + (Item["RazonSocial"] != null && !string.IsNullOrEmpty(Item["RazonSocial"].ToString()) ? Item["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                                        "<td>" + (Item["NombreFantasia"] != null && !string.IsNullOrEmpty(Item["NombreFantasia"].ToString()) ? Item["NombreFantasia"].ToString() : String.Empty) + "</td>" +
                                                        "<td>" + (Item["ActividadPrincipal"] != null && !string.IsNullOrEmpty(Item["ActividadPrincipal"].ToString()) ? Item["ActividadPrincipal"].ToString() : String.Empty) + "</td>" +
                                                    "</tr>";
                            }

                        }
                        //}
                    }
                    else
                    {
                        alertaMasCarac.Visible = true;
                    }
                }
            }
        }
        private string SetColorEstado(string sEstado)
        {
            string sColor = "#FFDEAD";

            switch (sEstado)
            {
                case "Aprobado":
                    sColor = "#98FB98";
                    break;
                case "Pendiente":
                    sColor = "#FFDEAD";
                    break;
                case "Rechazado":
                    sColor = "#CD5C5C";
                    break;
            }
            return sColor;
        }

        protected void cboRama1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlTabla.Visible = false;
            if (cboRama1.SelectedValue != "Seleccione")
            {
                btnBuscar.Disabled = false;
            }
        }

        

        
    }
}
