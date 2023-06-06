using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;
using Microsoft.SharePoint.Utilities;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;


namespace AvisoCapacitaciones.CapacitacionesWF
{
    public sealed partial class CapacitacionesWF : SequentialWorkflowActivity
    {
        public CapacitacionesWF()
        {
            InitializeComponent();
        }

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();

        private void onWorkflowActivated1_Invoked(object sender, ExternalDataEventArgs e)
        {
            string inscripto = string.Empty;
            string titulo = string.Empty;
            string inscriptoCorreo = string.Empty;
            string fechaDesde = string.Empty;
            string fechaHasta = string.Empty;
            string ubicacion = string.Empty;
            string id = string.Empty;
            string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='Title' /><Value Type='Text'>Capacitación Prueba</Value></Eq></Where></Query></View>";
            SPQuery query = new SPQuery();
            query.ViewXml = QuerySTR;
            SPListItemCollection ListaAux = SPContext.Current.Web.Lists["Capacitaciones_x0020__x0020_CalendarioList"].GetItems(query);
            if (ListaAux.Count > 0)
            {
                foreach (SPListItem item in ListaAux)
                {
                    SPFieldUserValue UserValueRV = new SPFieldUserValue(SPContext.Current.Web, item["Author"].ToString());
                    SPUser usuarioCreador = UserValueRV.User;

                    inscripto = usuarioCreador.Name.ToString();
                    titulo= item["Author"].ToString();
                    inscriptoCorreo = usuarioCreador.Email.ToString(); ;
                    fechaDesde = item["EventDate"].ToString();
                    fechaHasta = item["EndDate"].ToString();
                    ubicacion = item["Location"].ToString();
                    id = item["ID"].ToString();
                }
            }
            
            string sUrlSitio = workflowProperties.WebUrl.ToString();
            string sCorreo = "<table style='height: 160px;' border='0' width='606' cellspacing='0' cellpadding='0' align='center'><tbody>"+
                              "<tr>"+
                                "<td colspan='2' bgcolor='#0a4e9a'>CAPACITACIONES IEASA</td>" +
                              "</tr>"+
                              "<tr>"+
                                    "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>"+
                                        "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>"+
                                            "Estimado/a: " + inscripto + "" +
                                            "</br>Le enviamos la información sobre la capacitacion a la cual se ha inscripto"+
                                            "</br> Título: " + titulo + "" +
                                            "</br>Hora Inicio: " + fechaDesde + ""+
                                            "</br>Hora Fin: " + fechaHasta + ""+
                                        "</p>" +
                                    "</td>" +
                              "</tr>" +
                                "<tr bgcolor='#f7f7f7' style='text-align: center'>"+
                                  "<td style='padding: 0 150px;'>"+
                                    "<table>"+
                                      "<tr>"+
                                        "<a role='button' href='" + ubicacion + "' style='text-decoration: none; font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;' id='btn6'>" +
                                            "<td style='width: 450px; height:60px; background-color: rgb(109,129,255); cursor: pointer;'>" +
                                                "<img src='https://img.icons8.com/windows/32/000000/microsoft-teams-2019.png' style='padding:0 15px 0 20%'/>Unirse a la Reunión" +
                                            "</td>"+
                                        "</a>"+
                                    "</tr>"+
                                    "<tr>"+
                                        "<a role='button' href='" + sUrlSitio + "/_vti_bin/owssvr.dll?CS=109&Cmd=Display&List=%71632745-198b-4cd2-b248-7a77d5e425c3%7D&CacheControl=1&ID=" + id + "&Using=event.ics' id='btn7' style='text-decoration: none; font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>" +
                                          "<td style='width: 450px; height:60px; background-color: rgb(0,114,197); cursor: pointer;'>" +
                                            "<img src='https://img.icons8.com/ios/50/000000/microsoft-outlook-2019.png' style='padding:0 15px 0 20%'/>Agendar en OutLook" +
                                          "</td>"+
                                          "</a>"+
                                      "</tr>"+
                                    "</table>"+
                                  "</td>"+
                                "</tr>"+
                                       
                              "<tr>"+
                                  "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>"+
                                      "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Saludos</p>"+
                                    "</td>"+
                              "</tr>"+
                        "</tbody></table>";

            StringDictionary headers = new StringDictionary();
            headers.Add("to", "inscriptoCorreo");
            headers.Add("subject", "Inscripción Capacitaciones");
            headers.Add("content-type", "text/html");
           
            SPUtility.SendEmail(workflowProperties.Web, headers, sCorreo);
                        


        }
    }
}
