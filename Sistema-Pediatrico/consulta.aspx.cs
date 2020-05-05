using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_Pediatrico
{
    public partial class consulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
                Titulo.InnerText = "Nueva Consulta Médica";
            }
        }
    }
}