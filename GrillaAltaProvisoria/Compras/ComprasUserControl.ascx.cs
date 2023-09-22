using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

namespace GrillaAltaProvisoria.Compras
{
    public partial class ComprasUserControl : UserControl
    {
        public Compras WebPart { get; set; }
        public string sLimiteVista = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.WebPart = this.Parent as Compras;
                sLimiteVista = this.WebPart.PropiedadLimiteVista;
                CargarTabla();
            }
        }

        private void CargarTabla()
        {
            SPQuery query = new SPQuery();
            string QuerySTR = "<View>"+
                                "<Query><Where><Eq><FieldRef Name='Estado' /><Value Type='Text'>Pendiente</Value></Eq></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy></Query>"+
                            "<RowLimit>" + sLimiteVista + "</RowLimit></View>";
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
                                "<Query><Where><Eq><FieldRef Name='Estado' /><Value Type='Text'>Aprobado</Value></Eq></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy></Query>" +
                            "<RowLimit>" + sLimiteVista + "</RowLimit></View>";
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
            //string QuerySTRRe = "<View>" +
            //                    "<Query><Where><Eq><FieldRef Name='Estado' /><Value Type='Text'>Rechazado</Value></Eq></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy></Query>" +
            //                "<RowLimit>" + sLimiteVista + "</RowLimit></View>";
            string QuerySTRRe = "<View>" +
                                "<Query><Where><Eq><FieldRef Name='Estado' /><Value Type='Text'>Pre-inscripto</Value></Eq></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy></Query>" +
                            "<RowLimit>" + sLimiteVista + "</RowLimit></View>";
            queryRe.ViewXml = QuerySTRRe;
            SPListItemCollection ListaRechazados = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(queryRe);
            foreach (SPListItem Facturas in ListaRechazados)
            {
                //ltTablaRechazado.Text
                ltTablaPrInscriptos.Text+= "<tr>" +
                                            "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/ENARSA/AltaProvisoria.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></td>" +
                                            
                                            "<td>" + (Facturas["NombreFantasia"] != null && !string.IsNullOrEmpty(Facturas["NombreFantasia"].ToString()) ? Facturas["NombreFantasia"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["RazonSocial"] != null && !string.IsNullOrEmpty(Facturas["RazonSocial"].ToString()) ? Facturas["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["ActividadPrincipal"] != null && !string.IsNullOrEmpty(Facturas["ActividadPrincipal"].ToString()) ? Facturas["ActividadPrincipal"].ToString() : String.Empty) + "</td>" +
                                        "</tr>";
            }
            SPQuery querySubs = new SPQuery();
            string QuerySTRSubs = "<View>" +
                                "<Query><Where><Eq><FieldRef Name='Estado' /><Value Type='Text'>Subsanado</Value></Eq></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy></Query>" +
                            "<RowLimit>" + sLimiteVista + "</RowLimit></View>";
            querySubs.ViewXml = QuerySTRSubs;
            SPListItemCollection ListaSubsanado = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(querySubs);
            foreach (SPListItem Facturas in ListaSubsanado)
            {
                ltTablaSubsanado.Text += "<tr>" +
                                            "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/ENARSA/AltaProvisoria.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></td>" +
                                            
                                            "<td>" + (Facturas["NombreFantasia"] != null && !string.IsNullOrEmpty(Facturas["NombreFantasia"].ToString()) ? Facturas["NombreFantasia"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["RazonSocial"] != null && !string.IsNullOrEmpty(Facturas["RazonSocial"].ToString()) ? Facturas["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["ActividadPrincipal"] != null && !string.IsNullOrEmpty(Facturas["ActividadPrincipal"].ToString()) ? Facturas["ActividadPrincipal"].ToString() : String.Empty) + "</td>" +
                                        "</tr>";
            }
            SPQuery querySuspBloq = new SPQuery();
            string QuerySTRSuspBloq = "<View>" +
                                "<Query><Where><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>Suspendido/Bloqueado</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Inhabilitado</Value></Eq></Or></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy></Query>" +
                            "<RowLimit>" + sLimiteVista + "</RowLimit></View>";
            querySuspBloq.ViewXml = QuerySTRSuspBloq;
            SPListItemCollection ListaSuspBloq = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(querySuspBloq);
            foreach (SPListItem Facturas in ListaSuspBloq)
            {
                ltTablaSuspBloq.Text += "<tr>" +
                                            "<td>" + "<a href='" + SPContext.Current.Web.Url + "/_layouts/15/ENARSA/AltaProvisoria.aspx?ID=" + Facturas.ID.ToString() + "' class='alert-link'>" + Facturas.ID.ToString() + "</a></td>" +
                                            
                                            "<td>" + (Facturas["NombreFantasia"] != null && !string.IsNullOrEmpty(Facturas["NombreFantasia"].ToString()) ? Facturas["NombreFantasia"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["RazonSocial"] != null && !string.IsNullOrEmpty(Facturas["RazonSocial"].ToString()) ? Facturas["RazonSocial"].ToString() : String.Empty) + "</td>" +
                                            "<td>" + (Facturas["ActividadPrincipal"] != null && !string.IsNullOrEmpty(Facturas["ActividadPrincipal"].ToString()) ? Facturas["ActividadPrincipal"].ToString() : String.Empty) + "</td>" +
                                        "</tr>";
            }
        }

        
        //static DataTable GetTable()
        //{
            
        //    //
        //    // Here we create a DataTable with four columns.
        //    //
        //    DataTable table = new DataTable();
        //    table.Columns.Add("Razón Social", typeof(int));
        //    table.Columns.Add("Nombre de Fantasía", typeof(string));
        //    table.Columns.Add("Dirección", typeof(string));
        //    table.Columns.Add("Telefono", typeof(string));
        //    table.Columns.Add("Correo Ventas", typeof(string));

        //    //
        //    // Here we add five DataRows.
        //    //
        //    SPQuery query = new SPQuery();
        //    string QuerySTR = "<View>" +
        //                        "<Query><Where><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>Aprobado</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>Pendiente</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Rechazado</Value></Eq></Or></Or></Where></Query>" +
        //                    "</View>";
        //    query.ViewXml = QuerySTR;
        //    SPListItemCollection ListaAlta = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(query);
        //    foreach (SPListItem Facturas in ListaAlta)
        //    {
        //        table.Rows.Add(
        //            (Facturas["RazonSocial"] != null && !string.IsNullOrEmpty(Facturas["RazonSocial"].ToString()) ? Facturas["RazonSocial"].ToString() : String.Empty),
        //            (Facturas["NombreFantasia"] != null && !string.IsNullOrEmpty(Facturas["NombreFantasia"].ToString()) ? Facturas["NombreFantasia"].ToString() : String.Empty),
        //            (Facturas["Calle"] != null && !string.IsNullOrEmpty(Facturas["Calle"].ToString()) ? Facturas["Calle"].ToString() : String.Empty),
        //            (Facturas["Telefono"] != null && !string.IsNullOrEmpty(Facturas["Telefono"].ToString()) ? Facturas["Telefono"].ToString() : String.Empty),
        //            (Facturas["VentasCorreo"] != null && !string.IsNullOrEmpty(Facturas["VentasCorreo"].ToString()) ? Facturas["VentasCorreo"].ToString() : String.Empty));
        //    }
            

        //    return table;
        //}

        protected void ExportarExcel_ServerClick(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Razón Social", typeof(string));
            table.Columns.Add("Nombre de Fantasía", typeof(string));
            table.Columns.Add("País", typeof(string));
            table.Columns.Add("Provincia", typeof(string));
            table.Columns.Add("Ciudad", typeof(string));
            table.Columns.Add("Cod. Postal", typeof(string));
            table.Columns.Add("Calle", typeof(string));
            table.Columns.Add("Altura", typeof(string));
            table.Columns.Add("Depto", typeof(string));
            table.Columns.Add("Piso", typeof(string));
            table.Columns.Add("Telefono", typeof(string));
            table.Columns.Add("Correo Ventas", typeof(string));
            table.Columns.Add("Representante legal", typeof(string));
            table.Columns.Add("CUIT", typeof(string));
            table.Columns.Add("Rubros", typeof(string));
            table.Columns.Add("Estado", typeof(string));

            SPQuery query = new SPQuery();
            string QuerySTR = "<View>" +
                                "<Query><Where><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>Aprobado</Value></Eq><Or><Eq><FieldRef Name='Estado' /><Value Type='Text'>Pendiente</Value></Eq><Eq><FieldRef Name='Estado' /><Value Type='Text'>Rechazado</Value></Eq></Or></Or></Where></Query>" +
                            "</View>";
            query.ViewXml = QuerySTR;


            //            SPListItemCollection ListaAlta = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems(query);
            SPListItemCollection ListaAlta = SPContext.Current.Web.Lists["AltaProvisoria"].GetItems();

            foreach (SPListItem Facturas in ListaAlta)
            {
                string sRubro = string.Empty;
                //Literal ltRubros = new Literal();
                string QuerySTRID = "<View><Query><Where><Eq><FieldRef Name='IDRegistro' /><Value Type='Text'>" + Facturas.ID.ToString() + "</Value></Eq></Where></Query></View>";
                SPQuery queryID = new SPQuery();
                queryID.ViewXml = QuerySTRID;
                SPListItemCollection ListaAux = SPContext.Current.Web.Lists["AltaProvisoriaRubros"].GetItems(queryID);
                if (ListaAux.Count > 0)
                {
                    foreach (SPListItem item in ListaAux)
                    {
                       // sRubro += item["Title"].ToString()+" ; ";
                        table.Rows.Add(
                            (Facturas["NombreFantasia"] != null && !string.IsNullOrEmpty(Facturas["NombreFantasia"].ToString()) ? Facturas["NombreFantasia"].ToString() : String.Empty),
                            (Facturas["RazonSocial"] != null && !string.IsNullOrEmpty(Facturas["RazonSocial"].ToString()) ? Facturas["RazonSocial"].ToString() : String.Empty),
                            (Facturas["Pais"] != null && !string.IsNullOrEmpty(Facturas["Pais"].ToString()) ? Facturas["Pais"].ToString() : String.Empty),
                            (Facturas["Provincia"] != null && !string.IsNullOrEmpty(Facturas["Provincia"].ToString()) ? Facturas["Provincia"].ToString() : String.Empty) + (Facturas["ProvinciaArg"] != null && !string.IsNullOrEmpty(Facturas["ProvinciaArg"].ToString()) ? Facturas["ProvinciaArg"].ToString() : String.Empty),
                            (Facturas["Ciudad"] != null && !string.IsNullOrEmpty(Facturas["Ciudad"].ToString()) ? Facturas["Ciudad"].ToString() : String.Empty),
                            (Facturas["CodPostal"] != null && !string.IsNullOrEmpty(Facturas["CodPostal"].ToString()) ? Facturas["CodPostal"].ToString() : String.Empty),
                            (Facturas["Calle"] != null && !string.IsNullOrEmpty(Facturas["Calle"].ToString()) ? Facturas["Calle"].ToString() : String.Empty),
                            (Facturas["Altura"] != null && !string.IsNullOrEmpty(Facturas["Altura"].ToString()) ? Facturas["Altura"].ToString() : String.Empty),
                            (Facturas["Depto"] != null && !string.IsNullOrEmpty(Facturas["Depto"].ToString()) ? Facturas["Depto"].ToString() : String.Empty),
                            (Facturas["Piso"] != null && !string.IsNullOrEmpty(Facturas["Piso"].ToString()) ? Facturas["Piso"].ToString() : String.Empty),
                            (Facturas["Telefono"] != null && !string.IsNullOrEmpty(Facturas["Telefono"].ToString()) ? Facturas["Telefono"].ToString() : String.Empty),
                            (Facturas["VentasCorreo"] != null && !string.IsNullOrEmpty(Facturas["VentasCorreo"].ToString()) ? Facturas["VentasCorreo"].ToString() : String.Empty),
                            (Facturas["ApoderadoResp"] != null && !string.IsNullOrEmpty(Facturas["ApoderadoResp"].ToString()) ? Facturas["ApoderadoResp"].ToString() : String.Empty),
                            (Facturas["Cuit"] != null && !string.IsNullOrEmpty(Facturas["Cuit"].ToString()) ? Facturas["Cuit"].ToString() : String.Empty),
                            item["Title"].ToString(),
                            (Facturas["Estado"] != null && !string.IsNullOrEmpty(Facturas["Estado"].ToString()) ? Facturas["Estado"].ToString() : String.Empty)
                        );
                    }
                }
                //table.Rows.Add(
                //    (Facturas["RazonSocial"] != null && !string.IsNullOrEmpty(Facturas["RazonSocial"].ToString()) ? Facturas["RazonSocial"].ToString() : String.Empty),
                //    (Facturas["NombreFantasia"] != null && !string.IsNullOrEmpty(Facturas["NombreFantasia"].ToString()) ? Facturas["NombreFantasia"].ToString() : String.Empty),
                //    (Facturas["Pais"] != null && !string.IsNullOrEmpty(Facturas["Pais"].ToString()) ? Facturas["Pais"].ToString() : String.Empty),
                //    (Facturas["Provincia"] != null && !string.IsNullOrEmpty(Facturas["Provincia"].ToString()) ? Facturas["Provincia"].ToString() : String.Empty) + (Facturas["ProvinciaArg"] != null && !string.IsNullOrEmpty(Facturas["ProvinciaArg"].ToString()) ? Facturas["ProvinciaArg"].ToString() : String.Empty),
                //    (Facturas["Ciudad"] != null && !string.IsNullOrEmpty(Facturas["Ciudad"].ToString()) ? Facturas["Ciudad"].ToString() : String.Empty),
                //    (Facturas["CodPostal"] != null && !string.IsNullOrEmpty(Facturas["CodPostal"].ToString()) ? Facturas["CodPostal"].ToString() : String.Empty),
                //    (Facturas["Calle"] != null && !string.IsNullOrEmpty(Facturas["Calle"].ToString()) ? Facturas["Calle"].ToString() : String.Empty),
                //    (Facturas["Altura"] != null && !string.IsNullOrEmpty(Facturas["Altura"].ToString()) ? Facturas["Altura"].ToString() : String.Empty),
                //    (Facturas["Depto"] != null && !string.IsNullOrEmpty(Facturas["Depto"].ToString()) ? Facturas["Depto"].ToString() : String.Empty),
                //    (Facturas["Piso"] != null && !string.IsNullOrEmpty(Facturas["Piso"].ToString()) ? Facturas["Piso"].ToString() : String.Empty),
                //    (Facturas["Telefono"] != null && !string.IsNullOrEmpty(Facturas["Telefono"].ToString()) ? Facturas["Telefono"].ToString() : String.Empty),
                //    (Facturas["VentasCorreo"] != null && !string.IsNullOrEmpty(Facturas["VentasCorreo"].ToString()) ? Facturas["VentasCorreo"].ToString() : String.Empty),
                //    (Facturas["ApoderadoResp"] != null && !string.IsNullOrEmpty(Facturas["ApoderadoResp"].ToString()) ? Facturas["ApoderadoResp"].ToString() : String.Empty),
                //    (Facturas["Cuit"] != null && !string.IsNullOrEmpty(Facturas["Cuit"].ToString()) ? Facturas["Cuit"].ToString() : String.Empty),
                //    sRubro.ToString());
            }
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            HtmlForm form = new HtmlForm();
            GridView dg = new GridView();
            dg.EnableViewState = false;
            dg.DataSource = table;
            dg.DataBind();
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(dg);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment;filename=Proveedores.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

        
        
    }
}
