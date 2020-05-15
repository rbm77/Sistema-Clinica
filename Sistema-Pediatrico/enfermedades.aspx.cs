using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace Sistema_Pediatrico
{
    public partial class enfermedades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEnfermedades();
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

        protected void listaEnfermedades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "eliminar")
            {
                int indice = Convert.ToInt32(e.CommandArgument.ToString());
                string enfermedadSeleccionada = Formato((string) listaEnfermedades.DataKeys[indice]["Enfermedad"]);
                ManejadorConsultas manejador = new ManejadorConsultas();
                string confirmacion = manejador.EliminarEnfermedad(enfermedadSeleccionada);
                if (!confirmacion.Contains("Error:"))
                {
                    CargarEnfermedades();
                }
                else
                {
                    MensajeAviso(confirmacion);
                }
            }
        }

        private void CargarEnfermedades()
        {
            ManejadorConsultas manejador = new ManejadorConsultas();
            List<string> enfermedades = new List<string>();

            string confirmacion = manejador.CargarEnfermedades(enfermedades);

            if (!confirmacion.Contains("Error:"))
            {
                List<ItemGrid> itemsGrid = new List<ItemGrid>();

                foreach (string e in enfermedades)
                {
                    itemsGrid.Add(new ItemGrid(e));
                }


                listaEnfermedades.DataBound += (object o, EventArgs ev) =>
                {
                    if (listaEnfermedades.Rows.Count > 0)
                    {
                        listaEnfermedades.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                };

                listaEnfermedades.DataSource = null;
                listaEnfermedades.DataSource = itemsGrid;
                listaEnfermedades.DataBind();

            }
            else
            {
                MensajeAviso(confirmacion);
            }
        }
        private string Formato(string texto)
        {
            texto = texto.Replace("&#243;", "ó").Replace("&#233;", "é").Replace("&#225;", "á").Replace("&#237;", "í").Replace("&#250;", "ú").Replace("&#241;", "ñ");
            return texto;
        }
        private void MensajeAviso(string mensaje)
        {
            string color = "";

            if (mensaje.Contains("Error:"))
            {
                color = "danger";
            }
            else
            {
                color = "success";
            }
            mensajeConfirmacion.Text = "<div class=\"alert alert-" + color + " alert-dismissible fade show\" " +
            "role=\"alert\"> <strong></strong>" + mensaje + "<button" +
            " type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">" +
            " <span aria-hidden=\"true\">&times;</span> </button> </div>";

            mensajeConfirmacion.Visible = true;
        }
        private class ItemGrid
        {
            public string Enfermedad { get; set; }
            public ItemGrid(string enfermedad)
            {
                this.Enfermedad = enfermedad;
            }
        }

        protected void guardarEnfermedad_Click(object sender, EventArgs e)
        {
            ManejadorConsultas manejador = new ManejadorConsultas();

            string confirmacion = manejador.IngresarEnfermedad(enfermedadNueva.Text.Trim());

            enfermedadNueva.Text = "";

            if (confirmacion.Contains("Error:"))
            {
                MensajeAviso(confirmacion);
            }
            else
            {
                CargarEnfermedades();
            }
        }
    }
}