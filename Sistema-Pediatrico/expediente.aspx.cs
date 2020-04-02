using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

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
            string respuestaFoto = SubirFoto();

        }
    }
}