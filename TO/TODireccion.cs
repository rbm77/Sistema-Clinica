using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TODireccion
    {
        public string CodigoDireccion { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }

        public TODireccion()
        {

        }
        public TODireccion(string codigoDireccion, string provincia, string canton, string distrito)
        {
            this.CodigoDireccion = codigoDireccion;
            this.Provincia = provincia;
            this.Canton = canton;
            this.Distrito = distrito;
        }
    }
}
