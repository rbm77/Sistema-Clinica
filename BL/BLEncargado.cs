using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLEncargado
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }
        public string Parentesco { get; set; }

        public BLDireccion Direccion { get; set; }

        public string DireccionExacta { get; set; }

        public BLEncargado()
        {

        }

        public BLEncargado(string cedula, string nombre, string primerApellido, string segundoApellido, 
            int telefono, string correo, string parentesco, BLDireccion direccion, string direccionExacta)
        {
            this.Cedula = cedula;
            this.Nombre = nombre;
            this.PrimerApellido = primerApellido;
            this.SegundoApellido = segundoApellido;
            this.Telefono = telefono;
            this.Correo = correo;
            this.Parentesco = parentesco;
            this.Direccion = direccion;
            this.DireccionExacta = direccionExacta;
        }
    }
}
