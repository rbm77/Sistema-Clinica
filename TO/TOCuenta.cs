using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOCuenta
    {
        public string IdCuenta { get; set; }
        public string Correo { get; set; }
        public string Contrasenna { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }

        public TOCuenta()
        {

        }

        public TOCuenta(string idCuenta, string correo, string contrasenna, string rol, string estado)
        {
            this.IdCuenta = idCuenta;
            this.Correo = correo;
            this.Contrasenna = contrasenna;
            this.Rol = rol;
            this.Estado = estado;
        }

    }
}
