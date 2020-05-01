using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace Sistema_Pediatrico
{
    public partial class expedientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarExpedientes();
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {

        }

        private void CargarExpedientes()
        {
            ManejadorExpediente manejador = new ManejadorExpediente();
            List<BLExpediente> expedientes = new List<BLExpediente>();


            if ((Session["id"] != null) && (Session["rol"] != null))
            {

                string idMedico = "";

                if (Session["codigoMedico"] != null)
                {
                    idMedico = Session["codigoMedico"].ToString();
                }

                string confirmacion = manejador.CargarExpedientes(expedientes, idMedico);

                if (!confirmacion.Contains("Error:"))
                {
                    List<ItemGrid> itemsGrid = new List<ItemGrid>();

                    foreach (BLExpediente e in expedientes)
                    {
                        itemsGrid.Add(new ItemGrid(e));
                    }


                    listaExpedientes.DataBound += (object o, EventArgs ev) =>
                    {
                        if (listaExpedientes.Rows.Count > 0)
                        {
                            listaExpedientes.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    };

                    listaExpedientes.DataSource = null;
                    listaExpedientes.DataSource = itemsGrid;
                    listaExpedientes.DataBind();

                }
                else
                {
                    MensajeAviso(confirmacion);
                }
            }

            
        }

        private class ItemGrid
        {
            public string IdExpediente { get; set; }
            public string Cedula { get; set; }
            public string Nombre { get; set; }
            public string PrimerApellido { get; set; }
            public string SegundoApellido { get; set; }
            public ItemGrid(BLExpediente expediente)
            {
                this.IdExpediente = expediente.IDExpediente + "";
                this.Cedula = expediente.Cedula;
                this.Nombre = expediente.Nombre;
                this.PrimerApellido = expediente.PrimerApellido;
                this.SegundoApellido = expediente.SegundoApellido;
            }
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

        protected void listaExpedientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "consultar")
            {
                int indice = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow filaSeleccionada = listaExpedientes.Rows[indice];
                TableCell id = filaSeleccionada.Cells[5];
                Session["accion"] = "consultarExpediente";
                Response.Redirect("expediente.aspx?id=" + id.Text);
            }
        }
    }
}