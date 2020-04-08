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
            BLDireccion direccionPaciente = null;
            BLDireccion direccionEncargado = null;
            BLDireccion direccionDestinatario = null;
            BLEncargado encargado = null;
            BLDestinatarioFactura destinatario = null;
            BLSolicitanteCita solicitante = null;
            BLDatosNacimiento datosNacimiento = null;
            BLHistoriaClinica historiaClinica = null;
            BLExpediente expediente = null;


            // Datos de PACIENTE

            string cedulaP = cedulaPaciente.Text.Trim();
            string nombreP = nombrePaciente.Text.Trim();
            string primerApellidoP = primerApellidoPaciente.Text.Trim();
            string segundoApellidoP = segundoApellidoPaciente.Text.Trim();
            string fechaNacimientoP = fechaNacimientoPaciente.Text.Trim();
            string sexoP = sexoPaciente.Text.Trim();
            string urlFotoP = inputFotoPaciente.FileName.Trim();
            string urlExpedienteAntiguoP = urlExpedienteAntiguoPaciente.Value.Trim();
            string provinciaP = inputProvinciaPaciente.Text;
            string cantonP = inputCantonPaciente.Text;
            string distritoP = inputDistritoPaciente.Text;
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
            string provinciaE = inputProvinciaEncargado.Value.Trim();
            string cantonE = inputCantonEncargado.Value.Trim();
            string distritoE = inputDistritoEncargado.Value.Trim();
            string direccionExactaE = direccionExactaEncargado.Value.Trim();

            if (!nombreE.Equals("") || !primerApellidoE.Equals("") || !segundoApellidoE.Equals("") || telefonoE != 0 ||
                !correoE.Equals("") || !parentescoE.Equals("") || !direccionExactaE.Equals(""))
            {
                if (cedulaE.Equals("") || provinciaE.Equals("") || cantonE.Equals("") || distritoE.Equals(""))
                {
                    return null;
                } else
                {
                    string cod = provinciaE + "-" + cantonE + "-" + distritoE;
                    direccionEncargado = new BLDireccion(cod, provinciaE, cantonE, distritoE);
                    encargado = new BLEncargado(cedulaE, nombreE, primerApellidoE, segundoApellidoE, telefonoE, correoE, parentescoE,
                        direccionEncargado, direccionExactaE);
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
                string provinciaD = inputProvinciaDestinatario.Value.Trim();
                string cantonD = inputCantonDestinatario.Value.Trim();
                string distritoD = inputDistritoDestinatario.Value.Trim();
                string direccionExactaD = direccionExactaDestinatario.Value.Trim();

                if (!nombreD.Equals("") || !primerApellidoD.Equals("") || !segundoApellidoD.Equals("") || telefonoD != 0 ||
                    !correoD.Equals("") || !direccionExactaD.Equals(""))
                {
                    if (cedulaD.Equals("") || provinciaD.Equals("") || cantonD.Equals("") || distritoD.Equals(""))
                    {
                        return null;
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
            }






            return null;


        }

    }
}