using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BL;
using Newtonsoft;
using Newtonsoft.Json;

namespace Sistema_Pediatrico
{
    public partial class expediente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Este es para la accion de crear, se necesita algo diferente para la accion de actualizar

            if (!IsPostBack)
            {

                if (Request.QueryString["accion"] != null)
                {
                    if (Request.QueryString["accion"].Equals("crear"))
                    {
                        Session["accion"] = "crearExpediente";
                    }
                }

                if (Session["accion"] != null)
                {
                    string accion = Session["accion"].ToString();

                    if (accion.Equals("crearExpediente"))
                    {
                        esDestinatario.Checked = true;
                        esSolicitante.Checked = true;
                        normalPerinatal.Checked = true;
                        descripcionPerinatal.Disabled = true;
                        negativoPatologico.Checked = true;
                        descripcionPatologico.Disabled = true;
                        negativoQuirurgico.Checked = true;
                        descripcionQuirurgico.Disabled = true;
                        negativoTraumatico.Checked = true;
                        descripcionTraumatico.Disabled = true;
                        negativoHeredoFamiliar.Checked = true;
                        descripcionHeredoFamiliar.Disabled = true;
                        negativoAlergias.Checked = true;
                        descripcionAlergia.Disabled = true;
                        aldiaVacunas.Checked = true;
                        descripcionVacuna.Disabled = true;
                    }
                    else if (accion.Equals("consultarExpediente"))
                    {
                        if (Request.QueryString["id"] != null)
                        {
                            Session["idExpediente"] = Request.QueryString["id"];
                            CargarExpediente(Session["idExpediente"].ToString());
                        }
                    }
                }
            }
        }

        //private string SubirFoto()
        //{
        //    try
        //    {
                

        //        if (inputFotoPaciente.HasFile)
        //        {
        //            string extension = Path.GetExtension(inputFotoPaciente.FileName);

        //            if (extension.Equals(".jpg") || extension.Equals(".jpeg") || extension.Equals(".png")
        //               || extension.Equals(".JPG") || extension.Equals(".JPEG") || extension.Equals(".PNG"))
        //            {
        //                string ruta = Server.MapPath("~/Fotos_Pacientes/" + inputFotoPaciente.FileName);

        //                if (!File.Exists(ruta))
        //                {
        //                    inputFotoPaciente.SaveAs(ruta);
        //                }
        //                else
        //                {
        //                    return "Error: El archivo de la foto seleccionada ya existe. Por favor intente cambiar el nombre del archivo.";
        //                }
        //            }
        //            else
        //            {
        //                return "Error: La extensión del archivo de la foto no es permitida.";
        //            }
        //        }
        //        else
        //        {
        //            return "Error: No se ha seleccionado una foto.";
        //        }
        //        return "La foto se almacenó exitosamente.";
        //    }
        //    catch (Exception)
        //    {
        //        return "Error: La foto no se pudo almacenar.";
        //    }
        //}

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            BLExpediente expediente = ValidarDatos();

            string confirmacion = "";

            if (expediente != null)
            {

                ManejadorExpediente manejador = new ManejadorExpediente();

                confirmacion = manejador.CrearExpediente(expediente);

                MensajeAviso(confirmacion);

            }
            else
            {
                confirmacion = "Error: Puede que algunos datos se encuentren vacíos o con un formato incorrecto.";
                MensajeAviso(confirmacion);
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

        private BLExpediente ValidarDatos()
        {
            BLEncargado encargado = null;
            BLDestinatarioFactura destinatario = null;
            BLSolicitanteCita solicitante = null;
            BLDatosNacimiento datosNacimiento = null;
            BLHistoriaClinica historiaClinica = null;
            BLExpediente expediente = new BLExpediente();


            // Datos de PACIENTE

            string cedulaP = cedulaPaciente.Text.Trim();
            string nombreP = nombrePaciente.Text.Trim();
            string primerApellidoP = primerApellidoPaciente.Text.Trim();
            string segundoApellidoP = segundoApellidoPaciente.Text.Trim();
            string fechaNacimientoP = fechaNacimientoPaciente.Text.Trim();
            string sexoP = sexoPaciente.Text.Trim();
            string urlExpedienteAntiguoP = urlExpedienteAntiguoPaciente.Value.Trim();
            string provinciaP = provinciaPValue.Value.Trim();
            string cantonP = cantonPValue.Value.Trim();
            string distritoP = distritoPValue.Value.Trim();
            string direccionExactaP = direccionExactaPaciente.Value.Trim();

            string idMedico = "";

            if (Session["codigoMedico"] != null)
            {
                idMedico = Session["codigoMedico"].ToString();
            }

            string fechaCreacion = fechaActual.Text.Trim();

            // Comienza a validar

            if (nombreP.Equals("") || primerApellidoP.Equals("") || fechaNacimientoP.Equals("") || sexoP.Equals("nulo") ||
                provinciaP.Equals("nulo") || cantonP.Equals("nulo") || distritoP.Equals("nulo") || 
                fechaCreacion.Equals("") || idMedico.Equals(""))
            {
                return null;
            }
            else
            {
                // Asigna primeros datos

                expediente.Cedula = cedulaP;
                expediente.Nombre = nombreP;
                expediente.PrimerApellido = primerApellidoP;
                expediente.SegundoApellido = segundoApellidoP;
                expediente.FechaNacimiento = fechaNacimientoP;
                expediente.Sexo = sexoP;
                expediente.UrlExpedienteAntiguo = urlExpedienteAntiguoP;
                expediente.CodigoDireccion = provinciaP + "-" + cantonP + "-" + distritoP;
                expediente.DireccionExacta = direccionExactaP;
                expediente.IDMedico = idMedico;
                expediente.FechaCreacion = fechaCreacion;
                expediente.Encargado = null;
                expediente.DestinatarioFactura = null;
                expediente.SolicitanteCita = null;
            }

            // Datos de ENCARGADO

            string cedulaE = cedulaEncargado.Text.Trim();
            string nombreE = nombreEncargado.Text.Trim();
            string primerApellidoE = primerApellidoEncargado.Text.Trim();
            string segundoApellidoE = segundoApellidoEncargado.Text.Trim();
            string telefonoE = inputTelefonoEncargado.Text.Trim();
            string correoE = inputCorreoEncargado.Text.Trim();
            string parentescoE = parentesco.Text.Trim();
            string provinciaE = provinciaEValue.Value.Trim();
            string cantonE = cantonEValue.Value.Trim();
            string distritoE = distritoEValue.Value.Trim();
            string direccionExactaE = direccionExactaEncargado.Value.Trim();

            if (!nombreE.Equals("") || !primerApellidoE.Equals("") || !segundoApellidoE.Equals("") || !telefonoE.Equals("") ||
                !correoE.Equals("") || !parentescoE.Equals("") || !direccionExactaE.Equals("") || !cedulaE.Equals(""))
            {
                if (cedulaE.Equals(""))
                {
                    return null;
                }
                else
                {
                    string codigoDireccion = provinciaE + "-" + cantonE + "-" + distritoE;

                    encargado = new BLEncargado(cedulaE, nombreE, primerApellidoE, segundoApellidoE, telefonoE, correoE, parentescoE,
                        codigoDireccion, direccionExactaE);
                    expediente.Encargado = encargado;
                }
            }

            // Datos de DESTINATARIO

            if (esDestinatario.Checked == false)
            {
                string cedulaD = cedulaDestinatario.Text.Trim();
                string nombreD = nombreDestinatario.Text.Trim();
                string primerApellidoD = primerApellidoDestinatario.Text.Trim();
                string segundoApellidoD = segundoApellidoDestinatario.Text.Trim();
                string telefonoD = telefonoDestinatario.Text.Trim();
                string correoD = correoDestinatario.Text.Trim();
                string provinciaD = provinciaDValue.Value.Trim();
                string cantonD = cantonDValue.Value.Trim();
                string distritoD = distritoDValue.Value.Trim();
                string direccionExactaD = direccionExactaDestinatario.Value.Trim();


                    if (cedulaD.Equals(""))
                    {
                        return null;
                    }
                    else
                    {
                        string codigoDireccion = provinciaD + "-" + cantonD + "-" + distritoD;

                        destinatario = new BLDestinatarioFactura(cedulaD, nombreD, primerApellidoD, segundoApellidoD, telefonoD,
                            correoD, codigoDireccion, direccionExactaD);

                        expediente.DestinatarioFactura = destinatario;
                    }
            }

            // Datos de SOLICITANTE

            if (esSolicitante.Checked == false)
            {
                string cedulaS = cedulaSolicitante.Text.Trim();

                string correoS = correoSolicitante.Text.Trim();

                string telefonoS = telefonoSolicitante.Text.Trim();
                
                if (correoS.Equals("") || cedulaS.Equals(""))
                {
                    return null;
                }
                else
                {

                    string contrasenna = "NINGUNA"; // SE DEBE AUTOGENERAR UNA CONTRASENNA Y ASIGNARLA AL SOLICITANTE. ADEMAS ENVIARLA POR CORREO
                    solicitante = new BLSolicitanteCita(cedulaS, correoS, contrasenna, telefonoS);
                    expediente.SolicitanteCita = solicitante;
                }
            }
            else
            {
                if (expediente.Encargado != null)
                {
                    if (expediente.Encargado.Correo.Equals("") || expediente.Encargado.Cedula.Equals(""))
                    {
                        return null;
                    }
                }
            }



            // Datos de DATOS DE NACIMIENTO

            string tempTalla = tallaNacimiento.Text.Trim();
            string tempPeso = pesoNacimiento.Text.Trim();
            string tempPerimCefalico = perimetroCefalico.Text.Trim();
            string tempEdadGest = edadGestacional.Text.Trim();
            string tempApgar = apgar.Text.Trim();


            double talla = 0.0;
            double peso = 0.0;
            double perimCefalico = 0.0;
            double edadGest = 0.0;
            int apg = 0;
            string clasifUniversal = clasificacionUniversal.Text.Trim();

            try
            {
                if (!tempTalla.Equals(""))
                {
                    talla = double.Parse(tempTalla);
                }
                if (!tempPeso.Equals(""))
                {
                    peso = double.Parse(tempPeso);
                }
                if (!tempPerimCefalico.Equals(""))
                {
                    perimCefalico = double.Parse(tempPerimCefalico);
                }
                if (!tempEdadGest.Equals(""))
                {
                    edadGest = double.Parse(tempEdadGest);
                }
                if (!tempApgar.Equals(""))
                {
                    apg = int.Parse(tempApgar);
                }
            }
            catch (Exception)
            {
                return null;
            }

            if (talla != 0.0 || peso != 0.0 || perimCefalico != 0.0 || edadGest != 0.0 || !clasifUniversal.Equals("") || apg != 0)
            {
                datosNacimiento = new BLDatosNacimiento(talla, peso, perimCefalico, apg, edadGest, clasifUniversal);
            }


            // Datos de HISTORIA CLINICA

            string perinatales = descripcionPerinatal.Value.Trim();
            string patologicos = descripcionPatologico.Value.Trim();
            string quirurgicos = descripcionQuirurgico.Value.Trim();
            string traumaticos = descripcionTraumatico.Value.Trim();
            string heredoFamiliares = descripcionHeredoFamiliar.Value.Trim();
            string alergias = descripcionAlergia.Value.Trim();
            string vacunas = descripcionVacuna.Value.Trim();

            if ((anormalPerinatal.Checked && perinatales.Equals("")) ||
                (positivoPatologico.Checked && patologicos.Equals("")) ||
                (positivoQuirurgico.Checked && quirurgicos.Equals("")) ||
                (positivoTraumatico.Checked && traumaticos.Equals("")) ||
                (positivoHeredoFamiliar.Checked && heredoFamiliares.Equals("")) ||
                (positivoAlergias.Checked && alergias.Equals("")) ||
                (pendientesVacunas.Checked && vacunas.Equals("")))
            {
                return null;
            }

            if (normalPerinatal.Checked)
            {
                perinatales = "";
            }
            if (negativoPatologico.Checked)
            {
                patologicos = "";
            }
            if (negativoQuirurgico.Checked)
            {
                quirurgicos = "";
            }
            if (negativoTraumatico.Checked)
            {
                traumaticos = "";
            }
            if (negativoHeredoFamiliar.Checked)
            {
                heredoFamiliares = "";
            }
            if (negativoAlergias.Checked)
            {
                alergias = "";
            }
            if (aldiaVacunas.Checked)
            {
                vacunas = "";
            }

            historiaClinica = new BLHistoriaClinica(perinatales, patologicos, quirurgicos, traumaticos,
                heredoFamiliares, alergias, vacunas, datosNacimiento);

            expediente.HistoriaClinica = historiaClinica;

            return expediente;
        }
        private void CargarExpediente(string idExpediente)
        {

            BLExpediente expediente = new BLExpediente();

            expediente.IDExpediente =  long.Parse(idExpediente);

            ManejadorExpediente manejador = new ManejadorExpediente();
            string confirmacion = manejador.CargarExpediente(expediente);

            if (!confirmacion.Contains("Error:"))
            {
                // Datos de PACIENTE

                //cedulaPaciente.Text = expediente.Cedula;
                //nombrePaciente.Text = expediente.Nombre;
                //primerApellidoPaciente.Text = expediente.PrimerApellido;
                //segundoApellidoPaciente.Text = expediente.SegundoApellido;
                //fechaNacimientoPaciente.Text = expediente.FechaNacimiento;
                //sexoPaciente.Text = expediente.Sexo;
                //urlExpedienteAntiguoPaciente.Value = expediente.UrlExpedienteAntiguo;
                //provinciaPValue.Value;
                //cantonPValue.Value.Trim();
                //distritoPValue.Value.Trim();
                //direccionExactaPaciente.Value.Trim();

                // DEBO CARGAR LAS DIRECCIONES Y SELECCIONAR LA CORRESPONDIENTE, DE IGUAL MANERA CON EL SEXO.


                CargarDirecciones(expediente.CodigoDireccion, "", "");



            }
            else
            {
                MensajeAviso(confirmacion);
                contenedorDatos.Visible = false;
            }

        }

        private class DatoDireccion {

            public string Clave { get; set; }
            public string Valor { get; set; }

            public DatoDireccion()
            {

            }
            public DatoDireccion(string clave, string valor)
            {
                this.Clave = clave;
                this.Valor = valor;
            }

        }


        private void CargarDirecciones(string codDireccionPaciente, string codDireccionEncargado, string codDireccionDestinatario)
        {



            // CARGANDO DIRECCION DE PACIENTE

            if (!codDireccionPaciente.Equals(""))
            {
                string[] direccion = codDireccionPaciente.Split('-');
                string provincia = direccion[0];
                string canton = direccion[1];
                string distrito = direccion[2];

                string path = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);

                path = path + "/Recursos/" + provincia + "_cantones.json";

                string json = File.ReadAllText(path).Replace("\"", "'");

                JsonTextReader reader = new JsonTextReader(new StringReader(json));

                List<string> lista = new List<string>();

                if (reader.HasLineInfo())
                {
                    while (reader.Read())
                    {
                        if (reader.Value != null)
                        {
                            lista.Add(reader.Value + "");
                        }
                    }
                }

                List<DatoDireccion> fuente = new List<DatoDireccion>();

                for(int i = 0; i < lista.Count - 1; i = i+2)
                {
                    fuente.Add(new DatoDireccion(lista[i], lista[i + 1]));
                }

            }


            



        }

    }
}