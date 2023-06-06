using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using iTextSharp;
using iTextSharp.text.pdf;

using System.IO;
using iTextSharp.text;
using System.Collections;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;

namespace FirmaDigital.Layouts.FirmaDigital
{
    public partial class Firmante : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtoFirmar_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["FIRMANTE"] != null && Request.QueryString["EMISOR"] != null && Request.QueryString["ID"] != null && Request.QueryString["LISTA"] != null)
            {
                string sHtml = string.Empty;
                SPList ListaFirma = SPContext.Current.Web.Lists[Request.QueryString["LISTA"].ToString()];
                SPView VistaFirma = ListaFirma.Views["FIRMA"];
                sHtml= " <div style='padding-bottom: 15px; width: 1000px; background-color: #D7D7D7; font-family: open sans, sans-serif; border-radius: .3rem!important; margin-right: auto; margin-left: auto;font-size: 1rem; font-weight: 400; line-height: 1.5; color: #212529;text-align: left;' id='formPrincipal'>" +
                                        "<table style='border: none; border-collapse: collapse;'>" +
                                            "<tbody>" +
                                              "<tr style='height: 60px; background-color: #0a4e9a; border: none; text-align: center!important;'>" +
                                                "<td style='width: 250px;'><img style='width: 150px; height: auto; vertical-align: middle;' src='https://fotos.subefotos.com/ce6aee543c833f2b67df7d2d906bf089o.png'></td>" +
                                                "<td style='width: 750px; color: white; font-family: Ubuntu, sans-serif; font-size: 42px; font-weight: 700;'>" + ListaFirma.Title + "</td>" +
                                              "</tr>" +
                                            "</tbody>" +
                                          "</table>";
                SPListItem ItemLista = SPContext.Current.Web.Lists[Request.QueryString["LISTA"].ToString()].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                foreach (string fieldName in VistaFirma.ViewFields)
                {
                    
                    sHtml += "<table style='padding-right: 15px; padding-left: 15px;'>" +
                                            "<tbody>" +
                                              "<tr>" +
                                                "<td style='border-top-right-radius: 0; border-bottom-right-radius: 0; align-items: center; padding: .375rem .75rem; margin-bottom: 0; font-size: 1rem; font-weight: 400; line-height: 1.5; color: #495057; text-align: center; white-space: nowrap; background-color: #e9ecef; border: 1px solid #ced4da; border-radius: .25rem;'>" + fieldName + "</td>" +
                                                "<td style='width: 100%; border-top-right-radius: 0; border-bottom-right-radius: 0; align-items: center; padding: .375rem .75rem; margin-bottom: 0; font-size: 1rem; font-weight: 400; line-height: 1.5; color: #495057; white-space: nowrap; background-color: #e9ecef; border: 1px solid #ced4da; border-radius: .25rem;'>" + (ItemLista[fieldName] != null && !string.IsNullOrEmpty(ItemLista[fieldName].ToString()) ? ItemLista[fieldName].ToString() : String.Empty) + "</td>" +
                                              "</tr>" +
                                            "</tbody>" +
                                          "</table><br>";
                }
                sHtml += "Documento firmado digitalmente por : " + Request.QueryString["FIRMANTE"].ToString() + "<br>";
                sHtml += "Entidad certificante: " + Request.QueryString["EMISOR"].ToString() +"<br>";
                sHtml += "Fecha:  " + DateTime.Now.ToShortDateString() + "<br>";
                sHtml += "</div>";






                var htmlContent = String.Format("<html>" +

                                                "<body>" +

                                                    sHtml +

                                                "</body>" +
                                            "</html>");
                byte[] pdfBytes = null;

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                    pdfBytes = htmlToPdf.GeneratePdf(htmlContent);
                });
                SPListItem ItemLista2 = SPContext.Current.Web.Lists[Request.QueryString["LISTA"].ToString()].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                List<string> fileNames = new List<string>();
                foreach (string fileName in ItemLista2.Attachments)
                {
                    fileNames.Add(fileName);
                }
                foreach (string fileName in fileNames)
                {
                    ItemLista2.Attachments.Delete(fileName);
                }
                ItemLista2.Update();
                ItemLista2.Attachments.Add(ItemLista2.ParentList.Title +"ConFirma.pdf", pdfBytes);
                ItemLista2.Update();


                tituloVentana.Visible = btnGrupoFirmar.Visible = false;
                divAbrirPdf.Visible = btnGroupPDF.Visible = true;
                ltrPDF.Text = "<a href='" + ItemLista2.Attachments.UrlPrefix + ItemLista2.ParentList.Title + "ConFirma.pdf" + "' class='btn btn-primary' role='button'>Abrir PDF</a> ";
              
            //    string filePath = ItemLista2.Attachments.UrlPrefix + +".pdf";
               
            }


           
        }
    }
}
