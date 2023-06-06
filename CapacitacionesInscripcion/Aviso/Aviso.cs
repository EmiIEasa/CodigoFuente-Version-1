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
using System.Collections.Specialized;
using Microsoft.SharePoint.Utilities;

namespace CapacitacionesInscripcion.Aviso
{
    public sealed partial class Aviso : SequentialWorkflowActivity
    {
        public Aviso()
        {
            InitializeComponent();
        }

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();

        private void onWorkflowActivated1_Invoked(object sender, ExternalDataEventArgs e)
        {   string LinkArchivo=string.Empty;
            string sUsuarios=string.Empty;
            bool bImagen = false;
            string sImagenCompletaAltoAncho = string.Empty;
            SPFieldUserValueCollection objUserFieldValueCol = new SPFieldUserValueCollection(workflowProperties.Web, workflowProperties.Item["ParticipantsPicker"].ToString());
             if (workflowProperties.Item.Attachments.Count > 0)
             {
                 bImagen = true;
                LinkArchivo = workflowProperties.Item.Attachments.UrlPrefix + workflowProperties.Item.Attachments[0];
            }
               
            for (int i = 0; i < objUserFieldValueCol.Count; i++)
            {
                SPFieldUserValue singleUser = objUserFieldValueCol[i];



                sUsuarios += singleUser.User.Email+";";

            }
            if (bImagen == true)
            {
                sImagenCompletaAltoAncho = "<br><br><img src='" + LinkArchivo + "' alt='Imagen Capacitacion' width='600px' height='800px'/>";
            }
             string sCorreo = "<table style='height: 160px;' border='0' width='606' cellspacing='0' cellpadding='0' align='center'><tbody>" +
                              "<tr>" +
                                "<td colspan='2' bgcolor='#0a4e9a' style='font-family: Ubuntu, sans-serif; font-size: 23px; font-weight: bold; line-height: 28px; color:#FFF; text-align: center;'>CAPACITACIONES IEASA</td>" +
                              "</tr>" +
                              "<tr>" +
                                    "<td valign='top' bgcolor='#f7f7f7' style='padding:0px 15px' colspan='2'>" +
                                        "<p style='font-family: Open Sans, sans-serif; font-size: 13px; font-weight: bold; line-height: 28px;'>" +
                                            "</br>Hola, se los invita a participar de la capacitación: " + workflowProperties.Item["Title"].ToString() +
                                            "</br> La misma se llevara a cabo en: <a role='button' href='"+ workflowProperties.Item["Location"].ToString() +"' id='btn7' style='font-family: Open Sans, sans-serif; color: rgb(0,114,197); font-size: 13px; font-weight: bold; line-height: 50px; padding:10px; text-decoration: none;'>" +
                                                "Link Capacitación" +
                                              "</a>" + 
                                            "</br>Hora Inicio: " + DateTime.Parse(workflowProperties.Item["EventDate"].ToString()) + "" +
                                            "</br>Hora Fin: " + DateTime.Parse(workflowProperties.Item["EndDate"].ToString()) + "" +

                                             sImagenCompletaAltoAncho + 

                                            "</br><a role='button' href='https://capacitaciones.ieasa.com.ar/Lists/Inscribirse/AllItems.aspx' style='font-family: Open Sans, sans-serif; color: rgb(109,129,255); font-size: 13px; font-weight: bold; line-height: 50px;  padding:10px; text-decoration: none' id='btn6'>" +
                                                    "Para inscribirse ingrese aquí" +
                                            "</a>" +" y clickear en '+Nuevo'" +
                                            "</br>Esperamos su participación" +
                                            "</br>Muchas gracias" +
                                             
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
            headers.Add("to", sUsuarios);
            headers.Add("subject", "Capacitaciones");
            headers.Add("content-type", "text/html");


            SPUtility.SendEmail(workflowProperties.Web, headers, sCorreo);
                
        }
    }
}
