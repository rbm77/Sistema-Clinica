using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace Sistema_Pediatrico
{
    public partial class consultas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["accion"] = "verConsultas";
                CargarConsultas();
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("consulta.aspx");
        }

        protected void listaConsultas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        private void CargarConsultas()
        {
            ManejadorConsultas manejador = new ManejadorConsultas();
            List<BLConsulta> consultas = new List<BLConsulta>();
            BLExpediente expediente = new BLExpediente();

            if (Session["idExpediente"] != null)
            {
                expediente.IDExpediente = long.Parse(Session["idExpediente"].ToString());

                string confirmacion = manejador.CargarConsultas(consultas, expediente);

                if (!confirmacion.Contains("Error:"))
                {
                    List<ItemGrid> itemsGrid = new List<ItemGrid>();

                    foreach (BLConsulta c in consultas)
                    {
                        itemsGrid.Add(new ItemGrid(c));
                    }


                    listaConsultas.DataBound += (object o, EventArgs ev) =>
                    {
                        if (listaConsultas.Rows.Count > 0)
                        {
                            listaConsultas.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    };

                    listaConsultas.DataSource = null;
                    listaConsultas.DataSource = itemsGrid;
                    listaConsultas.DataBind();

                    // AHORA SE CARGA EL ENCABEZADO

                    expedienteEncabezado.InnerText = expediente.IDExpediente + "";
                    cedulaEncabezado.InnerText = expediente.Cedula;
                    nombreEncabezado.InnerText = expediente.Nombre + " " + expediente.PrimerApellido + " " + expediente.SegundoApellido; 

                }
                else
                {
                    MensajeAviso(confirmacion);
                }
            }
        }

        private class ItemGrid
        {
            public string Fecha { get; set; }
            public string Hora { get; set; }
            public ItemGrid(BLConsulta consulta)
            {
                string[] contenido = consulta.Hora.Split('|');
                string hora = contenido[0];
                string md = contenido[1];

                this.Fecha = consulta.Fecha;
                this.Hora = hora + " " + md;
            }

            private string DarFormato(string fecha)
            {
                string[] divisiones = fecha.Split('/');
                string nuevoFormato = divisiones[2] + "/" + divisiones[1] + "/" + divisiones[0];
                return nuevoFormato;
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
    }
}