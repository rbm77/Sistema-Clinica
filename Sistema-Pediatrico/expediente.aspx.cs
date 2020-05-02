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
                CargarProvincias();

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
                        fechaActual.Text = DateTime.Today.ToString("dd/MM/yyyy");
                        Titulo.InnerText = "Nuevo Expediente";
                        Consul_Exam.Visible = false;
                    }
                    else if (accion.Equals("consultarExpediente"))
                    {
                        if (Request.QueryString["id"] != null)
                        {
                            if (!Request.QueryString["id"].Equals(""))
                            {
                                Session["idExpediente"] = Request.QueryString["id"];
                            }
                        }
                        if (Session["idExpediente"] != null)
                        {
                            CargarExpediente(Session["idExpediente"].ToString());
                            Titulo.InnerText = "Expediente # " + Session["idExpediente"].ToString();
                            Consul_Exam.Visible = true;
                        }
                    }
                }
            }
            else
            {
                if (anormalPerinatal.Checked)
                {
                    descripcionPerinatal.Disabled = false;
                }
                if (positivoPatologico.Checked)
                {
                    descripcionPatologico.Disabled = false;
                }
                if (positivoQuirurgico.Checked)
                {
                    descripcionQuirurgico.Disabled = false;
                }
                if (positivoTraumatico.Checked)
                {
                    descripcionTraumatico.Disabled = false;
                }
                if (positivoHeredoFamiliar.Checked)
                {
                    descripcionHeredoFamiliar.Disabled = false;
                }
                if (positivoAlergias.Checked)
                {
                    descripcionHeredoFamiliar.Disabled = false;
                }
                if (pendientesVacunas.Checked)
                {
                    descripcionVacuna.Disabled = false;
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

                if (confirmacion.Contains("Error:"))
                {
                    EstablecerNulos();
                    MensajeAviso(confirmacion);
                }
                else
                {
                    string id = confirmacion.Split('*')[1];
                    Session["accion"] = "consultarExpediente";
                    Response.Redirect("expediente.aspx?id=" + id);
                }
            }
            else
            {
                EstablecerNulos();
                confirmacion = "Error: Puede que algunos datos se encuentren vacíos o con un formato incorrecto.";
                MensajeAviso(confirmacion);
            }

        }

        private void EstablecerNulos()
        {
            inputProvinciaPaciente.SelectedValue = "nulo";
            inputProvinciaEncargado.SelectedValue = "nulo";
            inputProvinciaDestinatario.SelectedValue = "nulo";

            provinciaPValue.Value = "nulo";
            provinciaEValue.Value = "nulo";
            provinciaDValue.Value = "nulo";
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
                    if (!provinciaE.Equals("nulo") || !cantonE.Equals("nulo") || !distritoE.Equals("nulo"))
                    {
                        if (provinciaE.Equals("nulo") || cantonE.Equals("nulo") || distritoE.Equals("nulo"))
                        {
                            return null;
                        }
                    }
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
                    if (!provinciaD.Equals("nulo") || !cantonD.Equals("nulo") || !distritoD.Equals("nulo"))
                    {
                        if (provinciaD.Equals("nulo") || cantonD.Equals("nulo") || distritoD.Equals("nulo"))
                        {
                            return null;
                        }
                    }
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

            expediente.IDExpediente = long.Parse(idExpediente);

            ManejadorExpediente manejador = new ManejadorExpediente();
            string confirmacion = manejador.CargarExpediente(expediente);

            if (!confirmacion.Contains("Error:"))
            {
                // Datos de PACIENTE

                cedulaPaciente.Text = expediente.Cedula;
                nombrePaciente.Text = expediente.Nombre;
                primerApellidoPaciente.Text = expediente.PrimerApellido;
                segundoApellidoPaciente.Text = expediente.SegundoApellido;
                fechaNacimientoPaciente.Text = expediente.FechaNacimiento;
                sexoPaciente.SelectedValue = expediente.Sexo;
                urlExpedienteAntiguoPaciente.Value = expediente.UrlExpedienteAntiguo;
                direccionExactaPaciente.Value = expediente.DireccionExacta;
                fechaActual.Text = expediente.FechaCreacion;
                fechaActual.ReadOnly = true;


                // Datos de ENCARGADO

                if (expediente.Encargado != null)
                {
                    if (expediente.Encargado.Cedula != null)
                    {
                        if (!expediente.Encargado.Cedula.Equals(""))
                        {
                            cedulaEncargado.Text = expediente.Encargado.Cedula;
                            nombreEncargado.Text = expediente.Encargado.Nombre;
                            primerApellidoEncargado.Text = expediente.Encargado.PrimerApellido;
                            segundoApellidoEncargado.Text = expediente.Encargado.SegundoApellido;
                            inputTelefonoEncargado.Text = expediente.Encargado.Telefono;
                            inputCorreoEncargado.Text = expediente.Encargado.Correo;
                            parentesco.Text = expediente.Encargado.Parentesco;
                            direccionExactaEncargado.Value = expediente.Encargado.DireccionExacta;
                        }
                    }
                }

                // Datos de DESTINATARIO

                if (expediente.Encargado != null && expediente.DestinatarioFactura != null)
                {
                    if (!expediente.DestinatarioFactura.Cedula.Equals("") && 
                        !expediente.DestinatarioFactura.Cedula.Equals(expediente.Encargado.Cedula))
                    {
                        esDestinatario.Checked = false;

                        cedulaDestinatario.Text = expediente.DestinatarioFactura.Cedula;
                        nombreDestinatario.Text = expediente.DestinatarioFactura.Nombre;
                        primerApellidoDestinatario.Text = expediente.DestinatarioFactura.PrimerApellido;
                        segundoApellidoDestinatario.Text = expediente.DestinatarioFactura.SegundoApellido;
                        telefonoDestinatario.Text = expediente.DestinatarioFactura.Telefono;
                        correoDestinatario.Text = expediente.DestinatarioFactura.Correo;
                        direccionExactaDestinatario.Value = expediente.DestinatarioFactura.DireccionExacta;
                    }
                    else
                    {
                        esDestinatario.Checked = true;
                    }
                }

                // Datos de SOLICITANTE

                if (expediente.Encargado != null && expediente.SolicitanteCita != null)
                {
                    if (!expediente.SolicitanteCita.Cedula.Equals("") &&
                        !expediente.SolicitanteCita.Cedula.Equals(expediente.Encargado.Cedula))
                    {
                        esSolicitante.Checked = false;
                        cedulaSolicitante.Text = expediente.SolicitanteCita.Cedula;
                        correoSolicitante.Text = expediente.SolicitanteCita.Correo;
                        telefonoSolicitante.Text = expediente.SolicitanteCita.Telefono;
                    }
                    else
                    {
                        esSolicitante.Checked = true;
                    }
                }

                
                if (expediente.HistoriaClinica != null)
                {
                    // DATOS DE NACIMIENTO

                    if (expediente.HistoriaClinica.DatosNacimiento != null)
                    {
                        tallaNacimiento.Text = Form0(expediente.HistoriaClinica.DatosNacimiento.TallaNacimiento + "");
                        pesoNacimiento.Text = Form0(expediente.HistoriaClinica.DatosNacimiento.PesoNacimiento + "");
                        perimetroCefalico.Text = Form0(expediente.HistoriaClinica.DatosNacimiento.PerimetroCefalico + "");
                        edadGestacional.Text = Form0(expediente.HistoriaClinica.DatosNacimiento.EdadGestacional + "");
                        apgar.Text = Form0(expediente.HistoriaClinica.DatosNacimiento.Apgar + "");
                        clasificacionUniversal.Text = expediente.HistoriaClinica.DatosNacimiento.ClasificacionUniversal;
                    }

                    // DATOS DE HISTORIA CLINICA

                    descripcionPerinatal.Value = expediente.HistoriaClinica.Perinatales;
                    descripcionPatologico.Value = expediente.HistoriaClinica.Patologicos;
                    descripcionQuirurgico.Value = expediente.HistoriaClinica.Quirurgicos;
                    descripcionTraumatico.Value = expediente.HistoriaClinica.Traumaticos;
                    descripcionHeredoFamiliar.Value = expediente.HistoriaClinica.HeredoFamiliares;
                    descripcionAlergia.Value = expediente.HistoriaClinica.Alergias;
                    descripcionVacuna.Value = expediente.HistoriaClinica.Vacunas;

                    if (expediente.HistoriaClinica.Perinatales.Equals(""))
                    {
                        normalPerinatal.Checked = true;
                        descripcionPerinatal.Disabled = true;
                    }
                    else
                    {
                        anormalPerinatal.Checked = true;
                    }

                    if (expediente.HistoriaClinica.Patologicos.Equals(""))
                    {
                        negativoPatologico.Checked = true;
                        descripcionPatologico.Disabled = true;
                    }
                    else
                    {
                        positivoPatologico.Checked = true;
                    }

                    if (expediente.HistoriaClinica.Quirurgicos.Equals(""))
                    {
                        negativoQuirurgico.Checked = true;
                        descripcionQuirurgico.Disabled = true;
                    }
                    else
                    {
                        positivoQuirurgico.Checked = true;
                    }

                    if (expediente.HistoriaClinica.Traumaticos.Equals(""))
                    {
                        negativoTraumatico.Checked = true;
                        descripcionTraumatico.Disabled = true;
                    }
                    else
                    {
                        positivoTraumatico.Checked = true;
                    }

                    if (expediente.HistoriaClinica.HeredoFamiliares.Equals(""))
                    {
                        negativoHeredoFamiliar.Checked = true;
                        descripcionHeredoFamiliar.Disabled = true;
                    }
                    else
                    {
                        positivoHeredoFamiliar.Checked = true;
                    }

                    if (expediente.HistoriaClinica.Alergias.Equals(""))
                    {
                        negativoAlergias.Checked = true;
                        descripcionAlergia.Disabled = true;
                    }
                    else
                    {
                        positivoAlergias.Checked = true;
                    }

                    if (expediente.HistoriaClinica.Vacunas.Equals(""))
                    {
                        aldiaVacunas.Checked = true;
                        descripcionVacuna.Disabled = true;
                    }
                    else
                    {
                        pendientesVacunas.Checked = true;
                    }
                }

                // CARGO TODAS LAS DIRECCIONES

                if (!expediente.CodigoDireccion.Equals("") && !expediente.CodigoDireccion.Equals("nulo-nulo-nulo"))
                {
                    string direccionP = expediente.CodigoDireccion;
                    string direccionE = "";
                    string direccionD = "";

                    if (expediente.Encargado != null)
                    {
                        if (expediente.Encargado.CodigoDireccion != null)
                        {
                            if (!expediente.Encargado.CodigoDireccion.Equals("") &&
                                !expediente.Encargado.CodigoDireccion.Equals("nulo-nulo-nulo"))
                            {
                                direccionE = expediente.Encargado.CodigoDireccion;
                            }
                        }
                    }

                    if (expediente.DestinatarioFactura != null)
                    {
                        if (expediente.DestinatarioFactura.CodigoDireccion != null)
                        {
                            if (!expediente.DestinatarioFactura.CodigoDireccion.Equals("") &&
                                !expediente.DestinatarioFactura.CodigoDireccion.Equals("nulo-nulo-nulo"))
                            {
                                direccionD = expediente.DestinatarioFactura.CodigoDireccion;
                            }
                        }
                    }
                    CargarDirecciones(direccionP, direccionE, direccionD);
                }
            }
            else
            {
                MensajeAviso(confirmacion);
                contenedorDatos.Visible = false;
            }

        }

        private string Form0(string entrada)
        {
            if (entrada.Equals("0"))
            {
                return "";
            }
            return entrada;
        }
        private class DatoDireccion
        {

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

                inputProvinciaPaciente.SelectedValue = provincia;
                provinciaPValue.Value = provincia;

                //

                String path = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory) + "/Recursos/" + provincia + "_cantones.json";

                List<DatoDireccion> fuente = GenerarFuente(path);

                // SE CARGA EL SELECT

                inputCantonPaciente.DataSource = null;
                inputCantonPaciente.DataTextField = "Valor";
                inputCantonPaciente.DataValueField = "Clave";
                inputCantonPaciente.DataSource = fuente;
                inputCantonPaciente.DataBind();

                inputCantonPaciente.SelectedValue = canton;
                cantonPValue.Value = canton;

                //

                path = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory) + "/Recursos/" + provincia + "_" + canton + "_distritos.json";

                fuente = GenerarFuente(path);


                // SE CARGA EL SELECT

                inputDistritoPaciente.DataSource = null;
                inputDistritoPaciente.DataTextField = "Valor";
                inputDistritoPaciente.DataValueField = "Clave";
                inputDistritoPaciente.DataSource = fuente;
                inputDistritoPaciente.DataBind();

                inputDistritoPaciente.SelectedValue = distrito;
                distritoPValue.Value = distrito;

                //
            }

            // CARGANDO DIRECCION DE ENCARGADO

            if (!codDireccionEncargado.Equals(""))
            {
                string[] direccion = codDireccionEncargado.Split('-');
                string provincia = direccion[0];
                string canton = direccion[1];
                string distrito = direccion[2];

                inputProvinciaEncargado.SelectedValue = provincia;
                provinciaEValue.Value = provincia;

                //

                String path = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory) + "/Recursos/" + provincia + "_cantones.json";

                List<DatoDireccion> fuente = GenerarFuente(path);

                // SE CARGA EL SELECT

                inputCantonEncargado.DataSource = null;
                inputCantonEncargado.DataTextField = "Valor";
                inputCantonEncargado.DataValueField = "Clave";
                inputCantonEncargado.DataSource = fuente;
                inputCantonEncargado.DataBind();

                inputCantonEncargado.SelectedValue = canton;
                cantonEValue.Value = canton;

                //

                path = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory) + "/Recursos/" + provincia + "_" + canton + "_distritos.json";

                fuente = GenerarFuente(path);


                // SE CARGA EL SELECT

                inputDistritoEncargado.DataSource = null;
                inputDistritoEncargado.DataTextField = "Valor";
                inputDistritoEncargado.DataValueField = "Clave";
                inputDistritoEncargado.DataSource = fuente;
                inputDistritoEncargado.DataBind();

                inputDistritoEncargado.SelectedValue = distrito;
                distritoEValue.Value = distrito;

                //
            }


            // CARGANDO DIRECCION DE DESTINATARIO

            if (!codDireccionDestinatario.Equals(""))
            {
                string[] direccion = codDireccionDestinatario.Split('-');
                string provincia = direccion[0];
                string canton = direccion[1];
                string distrito = direccion[2];

                inputProvinciaDestinatario.SelectedValue = provincia;
                provinciaDValue.Value = provincia;

                //

                String path = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory) + "/Recursos/" + provincia + "_cantones.json";

                List<DatoDireccion> fuente = GenerarFuente(path);

                // SE CARGA EL SELECT

                inputCantonDestinatario.DataSource = null;
                inputCantonDestinatario.DataTextField = "Valor";
                inputCantonDestinatario.DataValueField = "Clave";
                inputCantonDestinatario.DataSource = fuente;
                inputCantonDestinatario.DataBind();

                inputCantonDestinatario.SelectedValue = canton;
                cantonDValue.Value = canton;

                //

                path = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory) + "/Recursos/" + provincia + "_" + canton + "_distritos.json";

                fuente = GenerarFuente(path);


                // SE CARGA EL SELECT

                inputDistritoDestinatario.DataSource = null;
                inputDistritoDestinatario.DataTextField = "Valor";
                inputDistritoDestinatario.DataValueField = "Clave";
                inputDistritoDestinatario.DataSource = fuente;
                inputDistritoDestinatario.DataBind();

                inputDistritoDestinatario.SelectedValue = distrito;
                distritoDValue.Value = distrito;

                //
            }
        }

        private List<DatoDireccion> GenerarFuente(string path)
        {
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

            reader.Close();

            List<DatoDireccion> fuente = new List<DatoDireccion>();

            fuente.Add(new DatoDireccion("nulo","Seleccionar..."));

            for (int i = 0; i < lista.Count - 1; i = i + 2)
            {
                fuente.Add(new DatoDireccion(lista[i], lista[i + 1]));
            }
            return fuente;
        }

        private void CargarProvincias()
        {

            string path = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory) + "/Recursos/provincias.json";

            List<DatoDireccion> fuente = GenerarFuente(path);


            // SE CARGA EL SELECT

            inputProvinciaPaciente.DataSource = null;
            inputProvinciaPaciente.DataTextField = "Valor";
            inputProvinciaPaciente.DataValueField = "Clave";
            inputProvinciaPaciente.DataSource = fuente;
            inputProvinciaPaciente.DataBind();

            inputProvinciaPaciente.SelectedValue = "nulo";

            provinciaPValue.Value = "nulo";
            cantonPValue.Value = "nulo";
            distritoPValue.Value = "nulo";

            //

            // SE CARGA EL SELECT

            inputProvinciaEncargado.DataSource = null;
            inputProvinciaEncargado.DataTextField = "Valor";
            inputProvinciaEncargado.DataValueField = "Clave";
            inputProvinciaEncargado.DataSource = fuente;
            inputProvinciaEncargado.DataBind();

            inputProvinciaEncargado.SelectedValue = "nulo";

            provinciaEValue.Value = "nulo";
            cantonEValue.Value = "nulo";
            distritoEValue.Value = "nulo";

            //

            // SE CARGA EL SELECT

            inputProvinciaDestinatario.DataSource = null;
            inputProvinciaDestinatario.DataTextField = "Valor";
            inputProvinciaDestinatario.DataValueField = "Clave";
            inputProvinciaDestinatario.DataSource = fuente;
            inputProvinciaDestinatario.DataBind();

            inputProvinciaDestinatario.SelectedValue = "nulo";

            provinciaDValue.Value = "nulo";
            cantonDValue.Value = "nulo";
            distritoDValue.Value = "nulo";

            //
        }

    }
}