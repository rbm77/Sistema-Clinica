using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLCuenta
    {
        public string IdCuenta { get; set; }
        public string Correo { get; set; }
        public string Contrasenna { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }

        public BLCuenta()
        {

        }

        public BLCuenta(string idCuenta, string correo, string contrasenna, string rol, string estado)
        {
            this.IdCuenta = idCuenta;
            this.Correo = correo;
            this.Contrasenna = contrasenna;
            this.Rol = rol;
            this.Estado = estado;
        }

    }
}
