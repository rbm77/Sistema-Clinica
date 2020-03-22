using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_Pediatrico
{
    public partial class cuenta_usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.inputCodigoAsistente.Attributes["disabled"] = "disabled";
            this.inputCodigoMedico.Attributes["disabled"] = "disabled";
            this.inputEspecialidad.Attributes["disabled"] = "disabled";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {

        }
    }
}