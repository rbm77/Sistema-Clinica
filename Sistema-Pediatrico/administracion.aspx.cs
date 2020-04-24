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

                if (Request.QueryString["accion"] != null)
                {
                    if (Request.QueryString["accion"].Equals("regresar"))
                    {
                        if (Session["accion"] != null)
                        {
                            if (Session["accion"].ToString().Equals("consultarCuenta"))
                            {
                                Response.Redirect("inicio.aspx");
                            }
                        }
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

        private string Formato(string texto)
        {
            texto = texto.Replace("&#243;", "ó").Replace("&#233;", "é").Replace("&#225;", "á").Replace("&#237;", "í").Replace("&#250;", "ú").Replace("&#241;", "ñ");
            return texto;
        }
        private void ActualizarEstados()
        {
            List<BLCuenta> cuentas = new List<BLCuenta>();
            CheckBox temp;
            string estado = "";

            foreach (GridViewRow fila in listaCuentas.Rows)
            {
                temp = (CheckBox)fila.Cells[4].FindControl("estado");

                if (temp.Checked)
                {
                    estado = "activa";                }
                else
                {
                    estado = "inactiva";
                }

                BLCuenta nueva = new BLCuenta();
                nueva.IdCuenta = Formato(fila.Cells[1].Text);
                nueva.Estado = estado;
                cuentas.Add(nueva);
            }

            ManejadorCuenta manejador = new ManejadorCuenta();

            string confirmacion = manejador.ActualizarEstados(cuentas);

            if (confirmacion.Contains("Error:"))
            {
                CargarUsuarios();
            }

            MensajeAviso(confirmacion);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ActualizarEstados();
        }
    }
}