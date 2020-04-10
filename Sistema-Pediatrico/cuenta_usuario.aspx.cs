using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace Sistema_Pediatrico
{
    public partial class cuenta_usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                this.inputCodigoAsistente.Attributes["disabled"] = "disabled";
                this.inputCodigoMedico.Attributes["disabled"] = "disabled";
                this.inputEspecialidad.Attributes["disabled"] = "disabled";
                CargarCodigosMedicos(new List<string>());
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            mensajeConfirmacion.Visible = false;
            string cedula = inputCedula.Text.Trim();
            string nombre = inputNombre.Text.Trim();
            string primerApellido = inputPrimerApellido.Text.Trim();
            string segundoApellido = inputSegundoApellido.Text.Trim();
            string telefono = inputTelefono.Text.Trim();

            string correo = inputCorreo.Text.Trim();
            string contrasenna = inputContrasenna.Text.Trim();
            string confimar = inputConfirmar.Text.Trim();

            string confirmacion = "Error: Puede que algunos datos se encuentren vacíos o con un formato incorrecto.";

            if (contrasenna.Equals(confimar))
            {
                string rol = inputRol.Value.Trim();
                string codigoAsistente = inputCodigoAsistente.SelectedValue.Trim();
                string codigoMedico = inputCodigoMedico.Text.Trim();
                string especialidad = inputEspecialidad.Text.Trim();


                ManejadorCuenta manejador = new ManejadorCuenta();
                BLCuenta cuenta = new BLCuenta(cedula, correo, contrasenna, rol, "activa");
                BLUsuario usuario = new BLUsuario(cedula, nombre, primerApellido, segundoApellido, telefono, codigoAsistente);
                BLMedico medico = new BLMedico(cedula, codigoMedico, especialidad, "");

                confirmacion = manejador.CrearCuenta(cuenta, usuario, medico);

                if (!confirmacion.Contains("Error:"))
                {
                    if(rol.Equals("medico"))
                    {
                        CargarCodigosMedicos(new List<string>());
                    }
                    LimpiarDatos();
                }
            }
            MensajeAviso(confirmacion);
            inputRol.Value = "nulo";
            inputCodigoAsistente.SelectedValue = "nulo";
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            mensajeConfirmacion.Visible = false;

        }

        private void MensajeAviso(string mensaje)
        {
            string color = "";

            if(mensaje.Contains("Error:"))
            {
                color = "danger"; 
            } else
            {
                color = "success";
            }
            mensajeConfirmacion.Text = "<div class=\"alert alert-" + color + " alert-dismissible fade show\" " +
            "role=\"alert\"> <strong></strong>" + mensaje + "<button" +
            " type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">" +
            " <span aria-hidden=\"true\">&times;</span> </button> </div>";

            mensajeConfirmacion.Visible = true;
        }

        private void LimpiarDatos()
        {
            inputCedula.Text = "";
            inputNombre.Text = "";
            inputPrimerApellido.Text = "";
            inputSegundoApellido.Text = "";
            inputTelefono.Text = "";
            inputCorreo.Text = "";
            inputContrasenna.Text = "";
            inputConfirmar.Text = "";
            inputRol.Value = "nulo";
            inputCodigoMedico.Text = "";
            inputEspecialidad.Text = "";
        }

        private void CargarCodigosMedicos(List<string> codigos)
        {
            ManejadorCuenta manejador = new ManejadorCuenta();
            string confirmacion = manejador.CargarCodigosMedicos(codigos);

            inputCodigoAsistente.Items.Clear();
            inputCodigoAsistente.Items.Add(new ListItem("Seleccionar...", "nulo"));
            inputCodigoAsistente.SelectedValue = "nulo";

            if (codigos.Count > 0)
            {
                foreach(string i in codigos) {
                    ListItem item = new ListItem(i, i);
                    inputCodigoAsistente.Items.Add(item);
                }
            }

            if(confirmacion.Contains("Error:"))
            {
                MensajeAviso(confirmacion);
            }
        }
    }
}