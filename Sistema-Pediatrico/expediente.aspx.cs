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
            catch(Exception)
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
            // Datos de PACIENTE

            string cedulaP = cedulaPaciente.Text.Trim();
            string nombreP = nombrePaciente.Text.Trim();
            string primerApellidoP = primerApellidoPaciente.Text.Trim();
            string segundoApellidoP = segundoApellidoPaciente.Text.Trim();
            string fechaNacimientoP = fechaNacimientoPaciente.Text.Trim();
            string sexoP = sexoPaciente.Text.Trim();
            string urlFotoP = inputFotoPaciente.FileName.Trim();
            string urlExpedienteAntiguoP = urlExpedienteAntiguoPaciente.Value.Trim();
            string provinciaP = inputProvinciaPaciente.Value.Trim();
            string cantonP = inputCantonPaciente.Value.Trim();
            string distritoP = inputDistritoPaciente.Value.Trim();
            string direccionExactaP = direccionExactaPaciente.Value.Trim();
            string idMedico = "AUSENTE"; // SE DEBE INSERTAR EL ID DEL MEDICO QUE CREO EL EXPEDIENTE. SE OBTIENE DE LA SESION DE USUARIO.
            string fechaCreacion = fechaActual.Text.Trim();
            
            // Comienza a validar

            if(nombreP.Equals("") || primerApellidoP.Equals("") || fechaNacimientoP.Equals("") || sexoP.Equals("nulo") ||
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
                telefonoE = int.Parse(inputTelefonoEncargado.Text.Trim());
            } catch(Exception)
            {
                return null;
            }
            string correoE = inputCorreoEncargado.Text.Trim();
            string parentescoE = parentesco.Text.Trim();
            string provinciaE = inputProvinciaEncargado.Value.Trim();
            string cantonE = inputCantonEncargado.Value.Trim();

            // Nota: Validar estos datos de modo que hay que estar seguro que no hay datos de los subobjetos para dejarlos como nulos





            return null;


        }

    }
}