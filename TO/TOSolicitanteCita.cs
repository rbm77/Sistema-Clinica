using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOSolicitanteCita
    {
        public string Correo { get; set; }
        public string Contrasenna { get; set; }
        public string Telefono { get; set; }
        public string Estado { get; set; }

        public TOSolicitanteCita()
        {

        }

        public TOSolicitanteCita(string correo, string contrasenna, string telefono, string estado)
        {
            this.Correo = correo;
            this.Contrasenna = contrasenna;
            this.Telefono = telefono;
            this.Estado = estado;
        }
    }
}
