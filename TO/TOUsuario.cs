using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOUsuario
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public int Telefono { get; set; }
        public string CodigoAsistente { get; set; }

        public TOUsuario()
        {

        }

        public TOUsuario(string cedula, string nombre, string primerApellido, string segundoApellido, int telefono, string codigoAsistente)
        {
            this.Cedula = cedula;
            this.Nombre = nombre;
            this.PrimerApellido = primerApellido;
            this.SegundoApellido = segundoApellido;
            this.Telefono = telefono;
            this.CodigoAsistente = codigoAsistente;
        }
    }
}
