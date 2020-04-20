using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace Sistema_Pediatrico
{
    public partial class inicio_sesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void botonLogin_Click(object sender, EventArgs e)
        {
            string cedula = inputCedula.Text.Trim();
            string contrasenna = inputContrasenna.Text.Trim();

            if (!cedula.Equals("") && !contrasenna.Equals(""))
            {
                ManejadorCuenta manejador = new ManejadorCuenta();
                BLCuenta cuenta = new BLCuenta(cedula, "", contrasenna, "", "");
                BLUsuario usuario = new BLUsuario();

                string confirmacion = manejador.IniciarSesion(cuenta, usuario);

                if (confirmacion.Contains("Error:"))
                {
                    MensajeAviso(confirmacion);
                    inputCedula.Text = "";
                    inputContrasenna.Text = "";
                }
                else
                {
                    // SE CREA LA SESION Y SE REDIRECCIONA

                    mensajeConfirmacion.Visible = false;


                    Session["id"] = cuenta.IdCuenta;
                    Session["rol"] = cuenta.Rol;
                    Session["nombre"] = usuario.Nombre + " " + usuario.PrimerApellido + " " + usuario.SegundoApellido;
                    Session["accion"] = "iniciar";

                    Response.Redirect("inicio.aspx");
                }
            }
            else
            {
                MensajeAviso("Error: Los datos son requeridos.");
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