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
using System.Text;
using System.IO;
using Microsoft.SharePoint.Administration;
using System.Net;

namespace CapacitacionesInscripcion.Workflow1
{
    public sealed partial class Workflow1 : SequentialWorkflowActivity
    {
        public Workflow1()
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
            string Descripcion = string.Empty;
            string LinkTeams = string.Empty;
            string LinkImagen= string.Empty;
            string LinkArchivo = string.Empty;
            bool bImagen = false;
            string QuerySTR = "<View><Query><Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + workflowProperties.Item["Capacitaci_x00f3_n"].ToString().Split('#')[1].ToString() + "</Value></Eq></Where></Query></View>";
            SPQuery query = new SPQuery();
            query.ViewXml = QuerySTR;
            SPListItemCollection ListaAux = workflowProperties.Web.Lists["Capacitaciones - Calendario"].GetItems(query);
            if (ListaAux.Count > 0)
            {
               
                foreach (SPListItem item in ListaAux)
                {
                    SPFieldUserValue UserValueRV = new SPFieldUserValue(workflowProperties.Web, workflowProperties.Item["Author"].ToString());
                    SPUser usuarioCreador = UserValueRV.User;

                    inscripto = usuarioCreador.Name.ToString();
                    titulo = item["Title"].ToString();
                    inscriptoCorreo = usuarioCreador.Email.ToString();
                    fechaDesde =  DateTime.Parse(item["EventDate"].ToString()).AddHours(3).ToString();
                    fechaHasta = DateTime.Parse(item["EndDate"].ToString()).AddHours(3).ToString();
                    ubicacion = item["Location"].ToString();
                    id = item["ID"].ToString();
                    Descripcion = item["Description"].ToString();
                    LinkTeams = item["Location"].ToString();
                 
                    if (item.Attachments.Count > 0)
                    {
                        bImagen = true;
                         LinkImagen = item.Attachments.UrlPrefix + item.Attachments[0];
                    }
                }
            }
          
          

         

            StringBuilder str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR");
            str.AppendLine("PRODID:-//Microsoft Corporation//Sharepoint MIMEDIR//EN");
            str.AppendLine("VERSION:2.0");
            str.AppendLine("METHOD:PUBLISH");
            str.AppendLine("BEGIN:VEVENT");
            str.AppendLine(string.Format("DTSTART:{0}",fechaDesde.Split('/')[2].Substring(0,4)+fechaDesde.Split('/')[1] + fechaDesde.Split('/')[0] +"T" + fechaDesde.Split('/')[2].Remove(0,4).Replace(":","").Trim() +"Z"));
           // str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
            str.AppendLine(string.Format("DTEND:{0}", fechaHasta.Split('/')[2].Substring(0, 4) + fechaDesde.Split('/')[1] + fechaHasta.Split('/')[0] + "T" + fechaHasta.Split('/')[2].Remove(0, 4).Replace(":", "").Trim() + "Z"));
            str.AppendLine("LOCATION: " + ubicacion);
            str.AppendLine(string.Format("UID:{0}", "Sharepoint:14"));
        //    str.AppendLine(string.Format("DESCRIPTION:{0}", Descripcion));
            string sImagenCompleta = string.Empty;
            string sImagenCompletaAltoAncho = string.Empty;
            if (bImagen == true)
            {
                sImagenCompleta = "<img src='" + LinkImagen + "' alt='Imagen Capacitacion'/>";
                sImagenCompletaAltoAncho = "</br></br><img src='" + LinkImagen + "' alt='Imagen Capacitacion' width='600px' height='800px'/>"; 
            }
            
            str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", Descripcion + " <br>"+ sImagenCompleta +"<br><a href='" + ubicacion + "'>Link de la capacitación</a>"));
            str.AppendLine(string.Format("SUMMARY:{0}", titulo));
         
            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("CLASS:PUBLIC");
            str.AppendLine("ACTION:DISPLAY");
         
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");
           
            byte[] byteArray = Encoding.UTF8.GetBytes(str.ToString());
       
          
              workflowProperties.Web.AllowUnsafeUpdates=true;
            workflowProperties.Item.Attachments.Add("AgendaOutlook.ics", byteArray);
            workflowProperties.Item.Update();
            workflowProperties.Web.AllowUnsafeUpdates = false;
            string sUrlSitio = workflowProperties.WebUrl.ToString();
            if (workflowProperties.Item.Attachments.Count > 0)
            {
                LinkArchivo = workflowProperties.Item.Attachments.UrlPrefix + workflowProperties.Item.Attachments[0];
            }
            string sCorreo = "<table style='height: 160px;' border='0' width='606' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                              "<tr>" +
                                "<td colspan='2' bgcolor='#0a4e9a' style='font-family: Ubuntu, sans-serif; font-size: 23px; font-weight: bold; line-height: 28px; color:#FFF; text-align: center;'>CAPACITACIONES IEASA</td>" +
                              "</tr>" +
                              "<tr>" +
                                    "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                        "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>" +
                                            "Estimado/a: " + inscripto + "" +
                                            "</br>Le enviamos la información sobre la capacitación a la cual se ha inscripto." +
                                            "</br> Título: " + titulo + "" +
                                            "</br>Hora Inicio: " + DateTime.Parse(fechaDesde).AddHours(-3).ToString() + "" +
                                            "</br>Hora Fin: " + DateTime.Parse(fechaHasta).AddHours(-3).ToString() + "" +
                                               sImagenCompletaAltoAncho   + 
                                            "</br><a role='button' href='" + ubicacion + "' style='font-family: Open Sans, sans-serif; color: rgb(109,129,255); font-size: 13px; font-weight: bold; line-height: 50px;  padding:10px; text-decoration: none' id='btn6'>" +
                                                    "Unirse a la Reunión" +
                                            "</a>" +
                                            "</br><a role='button' href='"+ LinkArchivo +"' id='btn7' style='font-family: Open Sans, sans-serif; color: rgb(0,114,197); font-size: 13px; font-weight: bold; line-height: 50px; padding:10px; text-decoration: none;'>" +
                                                "Agendar en Outlook" +
                                              "</a>" +
                                        "</p>" +
                                    "</td>" +
                              "</tr>" +

                              "<tr>" +
                                  "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                      "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>Saludos</p>" +
                                    "</td>" +
                              "</tr>" +
                        "</tbody></table>";
            StringDictionary headers = new StringDictionary();
            headers.Add("to", inscriptoCorreo);
            headers.Add("subject", "Inscripción Capacitaciones");
            headers.Add("content-type", "text/html");


            SPUtility.SendEmail(workflowProperties.Web, headers, sCorreo);
          
          
                        


        }
    }
}
