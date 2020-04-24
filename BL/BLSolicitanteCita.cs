using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLSolicitanteCita
    {
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public string Contrasenna { get; set; }
        public string Telefono { get; set; }

        public BLSolicitanteCita()
        {

        }

        public BLSolicitanteCita(string cedula, string correo, string contrasenna, string telefono)
        {
            this.Cedula = cedula;
            this.Correo = correo;
            this.Contrasenna = contrasenna;
            this.Telefono = telefono;
        }
    }
}
