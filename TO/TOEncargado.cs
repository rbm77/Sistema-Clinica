using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOEncargado
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Parentesco { get; set; }
        public string CodigoDireccion { get; set; }
        public string DireccionExacta { get; set; }

        public TOEncargado()
        {

        }

        public TOEncargado(string cedula, string nombre, string primerApellido, string segundoApellido,
            string telefono, string correo, string parentesco, string codigoDireccion, string direccionExacta)
        {
            this.Cedula = cedula;
            this.Nombre = nombre;
            this.PrimerApellido = primerApellido;
            this.SegundoApellido = segundoApellido;
            this.Telefono = telefono;
            this.Correo = correo;
            this.Parentesco = parentesco;
            this.CodigoDireccion = codigoDireccion;
            this.DireccionExacta = direccionExacta;
        }
    }
}
