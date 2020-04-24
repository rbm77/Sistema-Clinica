using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOSolicitanteCita
    {
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public string Contrasenna { get; set; }
        public string Telefono { get; set; }

        public TOSolicitanteCita()
        {

        }

        public TOSolicitanteCita(string cedula, string correo, string contrasenna, string telefono)
        {
            this.Cedula = cedula;
            this.Correo = correo;
            this.Contrasenna = contrasenna;
            this.Telefono = telefono;
        }
    }
}
