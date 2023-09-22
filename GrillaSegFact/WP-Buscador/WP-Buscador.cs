using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace GrillaSegFact.WP_Buscador
{
    [ToolboxItemAttribute(false)]
    public class WP_Buscador : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/GrillaSegFact/WP-Buscador/WP-BuscadorUserControl.ascx";
        const string const_PropiedadLimiteVista = "100";
        private string _PropiedadLimiteVistaBuscador = const_PropiedadLimiteVista;

        [Category("Propiedades vista"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Limite de la vista"),
        WebDescription("Ingrese un valor numérico entero"),
        DefaultValue(const_PropiedadLimiteVista)]
        public string PropiedadLimiteVistaBuscador
        {
            get { return _PropiedadLimiteVistaBuscador; }
            set
            {
                _PropiedadLimiteVistaBuscador = value;
            }
        }
        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
