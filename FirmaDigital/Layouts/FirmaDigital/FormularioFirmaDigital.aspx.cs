using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.X509;
using SysX509 = System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Crypto;
using FirmarPDF;
using iTextSharpSign;
using System.Collections.Generic;
using FirmarPDF;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.UI;

namespace FirmaDigital.Layouts.FirmaDigital
{
    public partial class FormularioFirmaDigital : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btoFirmar.Visible = false;

            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null && Request.QueryString["LISTA"] != null)
                {


                    SPList ListaFirma = SPContext.Current.Web.Lists[Request.QueryString["LISTA"].ToString()];
                SPView VistaFirma = ListaFirma.Views["FIRMA"];
                LtFormulario.Text = " <div style='padding-bottom: 15px; width: 1000px; background-color: #D7D7D7; font-family: open sans, sans-serif; border-radius: .3rem!important; margin-right: auto; margin-left: auto;font-size: 1rem; font-weight: 400; line-height: 1.5; color: #212529;text-align: left;' id='formPrincipal'>" +
                                        "<table style='border: none; border-collapse: collapse;'>" +
                                            "<tbody>"+
                                              "<tr style='height: 60px; background-color: #0a4e9a; border: none; text-align: center!important;'>"+
                                                "<td style='width: 250px;'><img style='width: 150px; height: auto; vertical-align: middle;' src='https://fotos.subefotos.com/ce6aee543c833f2b67df7d2d906bf089o.png'></td>"+
                                                "<td style='width: 750px; color: white; font-family: Ubuntu, sans-serif; font-size: 42px; font-weight: 700;'>"+ ListaFirma.Title + "</td>"+
                                              "</tr>"+
                                            "</tbody>"+
                                          "</table>";
                SPListItem ItemLista = SPContext.Current.Web.Lists[Request.QueryString["LISTA"].ToString()].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
                foreach (string fieldName in VistaFirma.ViewFields)
                {
                    Guid Id = new Guid();  
                    LtFormulario.Text += "<table style='padding-right: 15px; padding-left: 15px;'>" +
                                            "<tbody>" +
                                              "<tr>" +
                                                "<td style='border-top-right-radius: 0; border-bottom-right-radius: 0; align-items: center; padding: .375rem .75rem; margin-bottom: 0; font-size: 1rem; font-weight: 400; line-height: 1.5; color: #495057; text-align: center; white-space: nowrap; background-color: #e9ecef; border: 1px solid #ced4da; border-radius: .25rem;'>" + fieldName + "</td>" +
                                                "<td style='width: 100%; border-top-right-radius: 0; border-bottom-right-radius: 0; align-items: center; padding: .375rem .75rem; margin-bottom: 0; font-size: 1rem; font-weight: 400; line-height: 1.5; color: #495057; white-space: nowrap; background-color: #e9ecef; border: 1px solid #ced4da; border-radius: .25rem;'>" + (ItemLista[fieldName] != null && !string.IsNullOrEmpty(ItemLista[fieldName].ToString()) ? ItemLista[fieldName].ToString() : String.Empty) + "</td>" +
                                              "</tr>" +
                                            "</tbody>" +
                                          "</table>";
                }
                LtFormulario.Text += "</div>";
                }
            }
          
        }
    
     
     
     

        protected void btnGenerarPDF_ServerClick(object sender, EventArgs e)
        {
            var htmlContent = String.Format("<html>" +
  
                                                  "<body>" +

                                                      LtFormulario.Text +

                                                  "</body>" +
                                              "</html>");
            byte[] pdfBytes = null;

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {

                var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                pdfBytes = htmlToPdf.GeneratePdf(htmlContent);
            });
            SPListItem ItemLista = SPContext.Current.Web.Lists[Request.QueryString["LISTA"].ToString()].GetItemById(int.Parse(Request.QueryString["ID"].ToString()));
            List<string> fileNames = new List<string>();
            foreach (string fileName in ItemLista.Attachments)
            {
                fileNames.Add(fileName);
            }
            foreach (string fileName in fileNames)
            {
                ItemLista.Attachments.Delete(fileName);
            }
            ItemLista.Update();
            ItemLista.Attachments.Add(ItemLista.ParentList.Title + "SinFirma" + ".pdf", pdfBytes);
              ItemLista.Update();
              ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "MensajeFirmante();", true);
              btoFirmar.Visible = true;
              btnGenerarPDF.Visible = false;
              
        }
    }
}
