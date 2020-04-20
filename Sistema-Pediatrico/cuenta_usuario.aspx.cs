﻿using System;
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

            if (!IsPostBack)
            {
                CargarCodigosMedicos(new List<string>());

                // Se verifica si la accion es crear una nueva cuenta

                if (Session["accion"] != null)
                {
                    string accion = Session["accion"].ToString();

                    if (accion.Equals("crearCuenta"))
                    {
                        inputCodigoAsistente.Attributes.Add("disabled", "disabled");
                        inputCodigoMedico.Attributes.Add("disabled", "disabled");
                        inputEspecialidad.Attributes.Add("disabled", "disabled");
                    }
                    else
                    {
                        Consultar();
                    }
                }
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

                // AQUI DE DIVIDE EL PROCESO DEPENDIENDO DE SI LA ACCION ES CREAR O ACTUALIZAR

                

                if(Session["accion"] != null)
                {
                    string accion = Session["accion"].ToString();

                    if (accion.Equals("crearCuenta"))
                    {
                        confirmacion = manejador.CrearCuenta(cuenta, usuario, medico);
                        if (!confirmacion.Contains("Error:"))
                        {
                            if (rol.Equals("medico"))
                            {
                                CargarCodigosMedicos(new List<string>());
                            }
                            LimpiarDatos();
                        }
                    }
                    else
                    {
                        // AQUI SE HACE EL ACTUALIZAR
                        // SI ES CORRECTO LOS DATOS NO SE LIMPIAN
                        // SI ES INCORRECTO SE LLAMA A UN CONSULTAR PARA QUE LOS DATOS ESTEN EN SU ESTADO ACTUAL
                        // MOSTRAR EL MENSAJE DE CONFIRMACION EN CUALQUIERA DE LOS DOS CASOS

                    // RECORDAR ACTUALIZAR EL NOMBRE DE LA SESION EN CASO DE QUE LO HAYA CAMBIADO EN LOS DATOS PERSONALES
                    }
                }
            }

            MensajeAviso(confirmacion);

            string rolTemp = inputRol.Value.Trim();

            if (!rolTemp.Equals("nulo"))
            {
                if (rolTemp.Equals("medico"))
                {
                    inputCodigoAsistente.Attributes.Add("disabled", "disabled");
                    inputCodigoMedico.Attributes.Remove("disabled");
                    inputEspecialidad.Attributes.Remove("disabled");

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
            inputCodigoAsistente.SelectedValue = "nulo";
            inputCodigoAsistente.Attributes.Add("disabled", "disabled");
            inputCodigoMedico.Attributes.Add("disabled", "disabled");
            inputEspecialidad.Attributes.Add("disabled", "disabled");
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

        private void Consultar()
        {
            if (Session["id"] != null && Session["rol"] != null)
            {

                BLCuenta cuenta = new BLCuenta();
                cuenta.IdCuenta = Session["id"].ToString();

                BLUsuario usuario = new BLUsuario();
                BLMedico medico = new BLMedico();

                ManejadorCuenta manejador = new ManejadorCuenta();
                string confirmacion = manejador.CargarUsuario(cuenta, usuario, medico);


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
                    }
                    else if (rol.Equals("asistente"))
                    {
                        inputCodigoMedico.Attributes.Add("disabled", "disabled");
                        inputEspecialidad.Attributes.Add("disabled", "disabled");
                        inputCodigoAsistente.SelectedValue = usuario.CodigoAsistente;
                    }
                    else if (rol.Equals("administrador"))
                    {
                        inputCodigoAsistente.Attributes.Add("disabled", "disabled");
                        inputCodigoMedico.Attributes.Add("disabled", "disabled");
                        inputEspecialidad.Attributes.Add("disabled", "disabled");
                    }
                    inputCedula.Attributes.Add("disabled", "disabled");
                    inputRol.Attributes.Add("disabled", "disabled");
                }
                else
                {
                    MensajeAviso(confirmacion);

                    contenedorDatos.Visible = false;

                }
            }
               
        }


    }
}