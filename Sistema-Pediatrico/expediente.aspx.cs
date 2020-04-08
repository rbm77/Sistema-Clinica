using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BL;

namespace Sistema_Pediatrico
{
    public partial class expediente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string SubirFoto()
        {
            try
            {
                string extension = Path.GetExtension(inputFotoPaciente.FileName);

                if (inputFotoPaciente.HasFile)
                {
                    if (extension.Equals(".jpg") || extension.Equals(".jpeg") || extension.Equals(".png")
                       || extension.Equals(".JPG") || extension.Equals(".JPEG") || extension.Equals(".PNG"))
                    {
                        string ruta = Server.MapPath("~/Fotos_Pacientes/" + inputFotoPaciente.FileName);

                        if (!File.Exists(ruta))
                        {
                            inputFotoPaciente.SaveAs(ruta);
                        }
                        else
                        {
                            return "Error: El archivo seleccionado ya existe. Por favor intente cambiar el nombre del archivo.";
                        }
                    }
                    else
                    {
                        return "Error: La extensión del archivo no es permitida.";
                    }
                }
                else
                {
                    return "Error: No se ha seleccionado una foto.";
                }
                return "La foto se almacenó exitosamente.";
            }
            catch (Exception)
            {
                return "Error: La foto no se pudo almacenar.";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidarDatos();
            //  string respuestaFoto = SubirFoto();

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
            string urlFotoP = inputFotoPaciente.FileName.Trim();
            string urlExpedienteAntiguoP = urlExpedienteAntiguoPaciente.Value.Trim();
            string provinciaP = provinciaPValue.Value.Trim();
            string cantonP = cantonPValue.Value.Trim();
            string distritoP = distritoPValue.Value.Trim();
            string direccionExactaP = direccionExactaPaciente.Value.Trim();
            string idMedico = "AUSENTE"; // SE DEBE INSERTAR EL ID DEL MEDICO QUE CREO EL EXPEDIENTE. SE OBTIENE DE LA SESION DE USUARIO.
            string fechaCreacion = fechaActual.Text.Trim();

            // Comienza a validar

            if (nombreP.Equals("") || primerApellidoP.Equals("") || fechaNacimientoP.Equals("") || sexoP.Equals("nulo") ||
                provinciaP.Equals("nulo") || cantonP.Equals("nulo") || distritoP.Equals("nulo") || direccionExactaP.Equals("") ||
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
                expediente.UrlFoto = urlFotoP;
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
            int telefonoE = 0;
            try
            {
                string temp = inputTelefonoEncargado.Text.Trim();
                if (!temp.Equals(""))
                {
                    telefonoE = int.Parse(temp);
                }
            }
            catch (Exception)
            {
                return null;
            }
            string correoE = inputCorreoEncargado.Text.Trim();
            string parentescoE = parentesco.Text.Trim();
            string provinciaE = provinciaEValue.Value.Trim();
            string cantonE = cantonEValue.Value.Trim();
            string distritoE = distritoEValue.Value.Trim();
            string direccionExactaE = direccionExactaEncargado.Value.Trim();

            if (!nombreE.Equals("") || !primerApellidoE.Equals("") || !segundoApellidoE.Equals("") || telefonoE != 0 ||
                !correoE.Equals("") || !parentescoE.Equals("") || !direccionExactaE.Equals(""))
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
                int telefonoD = 0;
                try
                {
                    string temp = telefonoDestinatario.Text.Trim();
                    if (!temp.Equals(""))
                    {
                        telefonoD = int.Parse(temp);
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                string correoD = correoDestinatario.Text.Trim();
                string provinciaD = provinciaDValue.Value.Trim();
                string cantonD = cantonDValue.Value.Trim();
                string distritoD = distritoDValue.Value.Trim();
                string direccionExactaD = direccionExactaDestinatario.Value.Trim();

                if (!nombreD.Equals("") || !primerApellidoD.Equals("") || !segundoApellidoD.Equals("") || telefonoD != 0 ||
                    !correoD.Equals("") || !direccionExactaD.Equals(""))
                {
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
            }

            // Datos de SOLICITANTE

            if (esSolicitante.Checked == false)
            {
                int telefonoS = 0;
                try
                {
                    string temp = telefonoSolicitante.Text.Trim();
                    if (!temp.Equals(""))
                    {
                        telefonoS = int.Parse(temp);
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                string correoS = correoSolicitante.Text.Trim();

                if (correoS.Equals(""))
                {
                    return null;
                }
                else
                {

                    string contrasenna = "NINGUNA"; // SE DEBE AUTOGENERAR UNA CONTRASENNA Y ASIGNARLA AL SOLICITANTE. ADEMAS ENVIARLA POR CORREO
                    solicitante = new BLSolicitanteCita(correoS, contrasenna, telefonoS, "activa");
                    expediente.SolicitanteCita = solicitante;
                }
            }
            else
            {
                if (expediente.Encargado != null)
                {
                    if (expediente.Encargado.Correo.Equals(""))
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
            int perimCefalico = 0;
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
                    perimCefalico = int.Parse(tempPerimCefalico);
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

            if (talla != 0.0 || peso != 0.0 || perimCefalico != 0 || edadGest != 0.0 || !clasifUniversal.Equals("") || apg != 0)
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
            if(negativoPatologico.Checked)
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

    }
}