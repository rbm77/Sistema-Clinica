using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BL;

namespace Sistema_Pediatrico
{
    public partial class cuenta_usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // Se obtiene el valor del parámetro accion

                if (Request.QueryString["accion"] != null)
                {
                    if (Request.QueryString["accion"].Equals("consultar"))
                    {
                        Session["accion"] = "consultarCuenta";
                    }
                }
          
                // Se carga el dropdownlist con los códigos de los médicos

                CargarCodigosMedicos(new List<string>());

                // Se verifica si la accion es crear una nueva cuenta o consultar una existente

                if (Session["accion"] != null)
                {
                    string accion = Session["accion"].ToString();

                    if (accion.Equals("crearCuenta"))
                    {
                        inputCodigoAsistente.Attributes.Add("disabled", "disabled");
                        inputCodigoMedico.Attributes.Add("disabled", "disabled");
                        inputEspecialidad.Attributes.Add("disabled", "disabled");
                    }
                    else if (accion.Equals("consultarCuenta"))
                    {
                        Consultar();
                    }
                }
            }
        }

        // Se guarda o actualiza una cuenta, dependiendo de la accion del usuario
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            mensajeConfirmacion.Visible = false;

            // Se obtienen los valores de entrada

            string cedula = inputCedula.Text;
            string nombre = inputNombre.Text.Trim();
            string primerApellido = inputPrimerApellido.Text.Trim();
            string segundoApellido = inputSegundoApellido.Text.Trim();
            string telefono = inputTelefono.Text.Trim();
            string correo = inputCorreo.Text.Trim();
            string contrasenna = inputContrasenna.Text.Trim();
            string confimar = inputConfirmar.Text.Trim();

            // Se inicializa el valor de la variable confirmacion, por defecto contiene el mensaje de error

            string confirmacion = "Error: Puede que algunos datos se encuentren vacíos o con un formato incorrecto.";

            // Si la contraseña y su confirmacion coinciden, se procede a obtener el resto de los
            // valores de entrada y continuar con el flujo de la aplicacion

            if (contrasenna.Equals(confimar))
            {
                string rol = inputRol.Value.Trim();
                string codigoAsistente = inputCodigoAsistente.SelectedValue.Trim();
                string codigoMedico = inputCodigoMedico.Text.Trim();
                string especialidad = inputEspecialidad.Text.Trim();

                // Se encapsulan los datos en los objetos correspondientes

                ManejadorCuenta manejador = new ManejadorCuenta();
                BLCuenta cuenta = new BLCuenta(cedula, correo, contrasenna, rol, "activa");
                BLUsuario usuario = new BLUsuario(cedula, nombre, primerApellido, segundoApellido, telefono, codigoAsistente);
                BLMedico medico = new BLMedico(cedula, codigoMedico, especialidad, "");

                // Aqui se divide el flujo dependiendo si la accion es crear o actualizar

                if (Session["accion"] != null)
                {

                    string accion = Session["accion"].ToString();

                    if (accion.Equals("crearCuenta"))
                    {
                        // Se envían los objetos hacia el manejador y se recibe la respuesta de confirmacion

                        confirmacion = manejador.CrearCuenta(cuenta, usuario, medico);

                        // Si la respuesta es exitosa y la cuenta recién ingresada tenía el rol de médico,
                        // se procede a cargar nuevamente la lista de códigos de médicos y se limpian las
                        // entradas de datos

                        if (!confirmacion.Contains("Error:"))
                        {
                            if (rol.Equals("medico"))
                            {
                                CargarCodigosMedicos(new List<string>());
                            }
                            LimpiarDatos();
                        }
                    }
                    else if(accion.Equals("consultarCuenta"))
                    {
                        // Se envían los objetos hacia el manejador y se recibe la respuesta de confirmacion

                        confirmacion = manejador.ActualizarCuenta(cuenta, usuario, medico);

                        // Si la respuesta es existosa se procede a actualizar el objeto de sesión que contiene
                        // el nombre del usuario
                        // Si la respuesta contiene un error, se procede a cargar nuevamente los datos 
                        // correctos de la base de datos

                        if (!confirmacion.Contains("Error:"))
                        {
                            Session["nombre"] = usuario.Nombre + " " + usuario.PrimerApellido[0] + " " + usuario.SegundoApellido[0];
                        }
                        else
                        {
                            Consultar();
                        }
                    }
                }
            }
            else
            {
                //  Si la contraseña no coincide con su confirmación

                string accion = Session["accion"].ToString(); 
                if (accion.Equals("consultarCuenta"))
                {
                    // Se procede a cargar nuevamente los datos correctos de la base de datos
                    Consultar();
                }
            }

            // Se muestra en pantalla la respuesta a la accion realizada, ya sea exitosa o no

            MensajeAviso(confirmacion);

            // Se habilitan/deshabilitan las entradas dependiendo del rol seleccionado para la cuenta

            string rolTemp = inputRol.Value.Trim();

            if (!rolTemp.Equals("nulo"))
            {
                if (rolTemp.Equals("medico"))
                {
                    inputCodigoAsistente.Attributes.Add("disabled", "disabled");
                    inputEspecialidad.Attributes.Remove("disabled");
                    inputCodigoMedico.Attributes.Remove("disabled");
                }
                else if (rolTemp.Equals("asistente"))
                {
                    inputCodigoMedico.Attributes.Add("disabled", "disabled");
                    inputEspecialidad.Attributes.Add("disabled", "disabled");
                    inputCodigoAsistente.Attributes.Remove("disabled");
                }
                else if (rolTemp.Equals("administrador"))
                {
                    inputCodigoAsistente.Attributes.Add("disabled", "disabled");
                    inputCodigoMedico.Attributes.Add("disabled", "disabled");
                    inputEspecialidad.Attributes.Add("disabled", "disabled");
                }
            }
            else
            {
                inputCodigoAsistente.Attributes.Add("disabled", "disabled");
                inputCodigoMedico.Attributes.Add("disabled", "disabled");
                inputEspecialidad.Attributes.Add("disabled", "disabled");
            }
        }

        // Se muestra un mensaje en pantalla con la respuesta a la accion realizada por el usuario
        private void MensajeAviso(string mensaje)
        {
            // Dependiendo de si la acción fue exitosa o no, se muestra un color u otro

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

        // Se limpian los datos de entrada
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
            inputCodigoAsistente.SelectedValue = "nulo";
            inputCodigoAsistente.Attributes.Add("disabled", "disabled");
            inputCodigoMedico.Attributes.Add("disabled", "disabled");
            inputEspecialidad.Attributes.Add("disabled", "disabled");
        }

        // Se carga el dropdownlist con los códigos de los médicos
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

            // Si la acción contiene un error, se muestra el mensaje en pantalla

            if(confirmacion.Contains("Error:"))
            {
                MensajeAviso(confirmacion);
            }
        }

        // Se cargan todos los datos de entrada con los valores almacenados de la cuenta existente en base de datos
        private void Consultar()
        {
            if (Session["id"] != null && Session["rol"] != null)
            {

                BLCuenta cuenta = new BLCuenta();
                cuenta.IdCuenta = Session["id"].ToString();

                BLUsuario usuario = new BLUsuario();
                BLMedico medico = new BLMedico();

                // Se cargan los objetos enviados como parámetros con los datos respectivos

                ManejadorCuenta manejador = new ManejadorCuenta();
                string confirmacion = manejador.CargarUsuario(cuenta, usuario, medico);

                // Si la carga de datos fue correcta, se procede a asignar los valores a los componentes
                // de la página

                if (!confirmacion.Contains("Error:"))
                {
                    inputCedula.Text = usuario.Cedula;
                    inputNombre.Text = usuario.Nombre;
                    inputPrimerApellido.Text = usuario.PrimerApellido;
                    inputSegundoApellido.Text = usuario.SegundoApellido;
                    inputTelefono.Text = usuario.Telefono;
                    inputCorreo.Text = cuenta.Correo;
                    inputContrasenna.Text = cuenta.Contrasenna;
                    inputConfirmar.Text = cuenta.Contrasenna;
                    string rol = cuenta.Rol;
                    inputRol.Value = rol;

                    if (rol.Equals("medico"))
                    {
                        inputCodigoAsistente.Attributes.Add("disabled", "disabled");
                        inputCodigoMedico.Text = medico.CodigoMedico;
                        inputEspecialidad.Text = medico.Especialidad;
                        inputCodigoMedico.ReadOnly = true;
                    }
                    else if (rol.Equals("asistente"))
                    {
                        inputCodigoMedico.ReadOnly = true;
                        inputEspecialidad.ReadOnly = true;
                        inputCodigoAsistente.SelectedValue = usuario.CodigoAsistente;
                    }
                    else if (rol.Equals("administrador"))
                    {
                        inputCodigoAsistente.Attributes.Add("disabled", "disabled");
                        inputCodigoMedico.ReadOnly = true;
                        inputEspecialidad.ReadOnly = true;
                    }

                    inputCedula.ReadOnly = true;
                    inputRol.Attributes.Add("disabled", "disabled");
                }
                else
                {
                    // Si la respuesta contiene un error, se muestra el mensaje en pantalla 

                    MensajeAviso(confirmacion);
                    contenedorDatos.Visible = false;
                }
            }    
        }
    }
}