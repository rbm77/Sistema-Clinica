using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace Sistema_Pediatrico
{
    public partial class consultas_dia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["accion"] = "verConsultasDia";
                CargarConsultasDia();
            }
        }
        private void CargarConsultasDia()
        {
            ManejadorConsultas manejador = new ManejadorConsultas();
            List<BLExpediente> expedientes = new List<BLExpediente>();
            List<BLConsulta> consultas = new List<BLConsulta>();

            if ((Session["id"] != null) && (Session["rol"] != null))
            {

                string idMedico = "";

                if (Session["codigoMedico"] != null)
                {
                    idMedico = Session["codigoMedico"].ToString();
                }

                string fechaActual = DateTime.Today.ToString("dd/MM/yyyy");

                string confirmacion = manejador.CargarConsultasDia(consultas, expedientes, idMedico, fechaActual);

                if (!confirmacion.Contains("Error:"))
                {
                    List<ItemGrid> itemsGrid = new List<ItemGrid>();

                    for(int i = 0; i < expedientes.Count; i++)
                    {
                        itemsGrid.Add(new ItemGrid(consultas[i], expedientes[i]));
                    }

                    listaConsultasDia.DataBound += (object o, EventArgs ev) =>
                    {
                        if (listaConsultasDia.Rows.Count > 0)
                        {
                            listaConsultasDia.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    };

                    listaConsultasDia.DataSource = null;
                    listaConsultasDia.DataSource = itemsGrid;
                    listaConsultasDia.DataBind();

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
            public string Hora { get; set; }
            public ItemGrid(BLConsulta consulta, BLExpediente expediente)
            {
                this.IdExpediente = expediente.IDExpediente + "";
                this.Cedula = expediente.Cedula;
                this.Nombre = expediente.Nombre;
                this.PrimerApellido = expediente.PrimerApellido;
                this.SegundoApellido = expediente.SegundoApellido;
                string[] contenido = consulta.Hora.Split('|');
                string hora = contenido[0];
                string md = contenido[1];
                this.Hora = hora + " " + md;
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
        protected void listaConsultasDia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "consultar")
            {
                int indice = Convert.ToInt32(e.CommandArgument.ToString());
                string id = (string)listaConsultasDia.DataKeys[indice]["IdExpediente"];
                string fechaActual = DateTime.Today.ToString("dd/MM/yyyy");
                Session["accion"] = "consultarConsulta";
                Session["fechaConsulta"] = fechaActual;
                Session["idExpediente"] = id;
                BLExpediente expediente = new BLExpediente();

                GridViewRow fila = listaConsultasDia.Rows[indice];
                TableCell cedula = fila.Cells[0];
                TableCell nombre = fila.Cells[1];
                TableCell primerApellido = fila.Cells[2];
                TableCell segundoApellido = fila.Cells[3];

                expediente.IDExpediente = long.Parse(id);
                expediente.Cedula = cedula.Text;
                expediente.Nombre = nombre.Text;
                expediente.PrimerApellido = primerApellido.Text;
                expediente.SegundoApellido = segundoApellido.Text;

                Session["expediente"] = expediente;
                Response.Redirect("consulta.aspx?accion=dia");

            }
        }
    }
}