using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace Sistema_Pediatrico
{
    public partial class administracion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["accion"] != null)
                {
                    if (Session["accion"].ToString().Equals("consultarCuenta"))
                    {
                        Response.Redirect("inicio.aspx");
                    }
                }

                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            ManejadorCuenta manejador = new ManejadorCuenta();
            List<BLCuenta> cuentas = new List<BLCuenta>();
            List<BLUsuario> usuarios = new List<BLUsuario>();
            string confirmacion = manejador.CargarUsuarios(cuentas, usuarios);

            if (!confirmacion.Contains("Error:"))
            {
                List<ItemGrid> itemsGrid = new List<ItemGrid>();

                foreach (BLCuenta c in  cuentas)
                {
                    foreach(BLUsuario u in usuarios)
                    {
                        if (c.IdCuenta.Equals(u.Cedula))
                        {
                            itemsGrid.Add(new ItemGrid(c,u));
                            break;
                        }
                    }
                }

                listaCuentas.DataBound += (object o, EventArgs ev) =>
                {
                    listaCuentas.HeaderRow.TableSection = TableRowSection.TableHeader;
                };

                listaCuentas.DataSource = null;
                listaCuentas.DataSource = itemsGrid;
                listaCuentas.DataBind();


            }
            else
            {
                MensajeAviso(confirmacion);
            }
        }

        private class ItemGrid {

            public string Cedula { get; set; }
            public string Nombre { get; set; }
            public string Correo { get; set; }
            public string Telefono { get; set; }
            public bool Estado { get; set; }
            public ItemGrid(BLCuenta cuenta, BLUsuario usuario)
            {
                this.Cedula = cuenta.IdCuenta;
                this.Nombre = usuario.Nombre + " " + usuario.PrimerApellido + " " + usuario.SegundoApellido;
                this.Correo = cuenta.Correo;
                this.Telefono = usuario.Telefono;
                this.Estado = Convertir(cuenta.Estado);
            }

            private bool Convertir(string estado)
            {
                if (estado.Equals("activa"))
                {
                    return true;
                }
                return false;
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
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Session["accion"] = "crearCuenta";
            Response.Redirect("cuenta_usuario.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }
    }
}