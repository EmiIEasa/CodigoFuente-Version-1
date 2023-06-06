using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace BloquesApp.WP_BloquesApp
{
    public partial class WP_BloquesAppUserControl : UserControl
    {
        private string _sHtmlTitulo;
        public string HTMLTitulo { get { return _sHtmlTitulo; } set { _sHtmlTitulo = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            ltTitulo.Text = _sHtmlTitulo;
        }
    }
}
