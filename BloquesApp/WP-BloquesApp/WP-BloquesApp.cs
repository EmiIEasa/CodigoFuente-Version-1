using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace BloquesApp.WP_BloquesApp
{
    [ToolboxItemAttribute(false)]
    public class WP_BloquesApp : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/BloquesApp/WP-BloquesApp/WP-BloquesAppUserControl.ascx";
       
        private string _sHtmlTitulo = string.Empty;

        [Personalizable(PersonalizationScope.Shared)]
        [WebBrowsable(true)]
        [Category("Configuración")]
        [WebDisplayName("Configurar Acceso")]
        [Description("")]
        public string GetSetHTMLApp
        {
            get
            {
                if (string.IsNullOrEmpty(_sHtmlTitulo))
                    _sHtmlTitulo = "<a class='mosaicos bordesDI conInfo tile tile-Designer col1 fila1'  target='_blank' href='#' id='uno'>"+
				                        "<img src='../../../../_layouts/15/BloquesApp/img/factura.png' class='icon'/>"+
				                        "<span class='nombre-icono'>CARGA TU FACTURA</span>"+
				                        "<section class='portafolio-text'>"+
					                        "<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit.</p>"+
				                        "</section>"+
			                        "</a>"+
                                    "<a class='mosaicos bordesSI conInfo tile col2 fila1'  target='_blank' href='#' id='dos'>"+
				                        "<img src='../../../../_layouts/15/BloquesApp/img/datos.png' class='icon'/>"+
				                        "<span class='nombre-icono'>ACTUALIZA TUS DATOS</span>"+
				                        "<section class='portafolio-text'>"+
					                        "<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit.</p>"+
				                        "</section>"+
			                        "</a>"+
                                    "<a class='mosaicos bordesDI conInfo tile tile-Designer col3 fila1'  target='_blank' href='#' id='tres'>"+
				                        "<img src='../../../../_layouts/15/BloquesApp/img/licitaciones.png' class='icon'/>"+
				                        "<span class='nombre-icono'>LICITACIONES</span>"+
				                        "<section class='portafolio-text'>"+
					                        "<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit.</p>"+
				                        "</section>"+
			                        "</a>";
                return _sHtmlTitulo;
            }
            set
            {
                _sHtmlTitulo = value;
            }
        }

        protected override void CreateChildControls()
        {
            WP_BloquesAppUserControl control = (WP_BloquesAppUserControl)Page.LoadControl(_ascxPath);
            control.HTMLTitulo = GetSetHTMLApp;
            
            Controls.Add(control);
        }
    }
}
