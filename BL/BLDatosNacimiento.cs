using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLDatosNacimiento
    {
        public double TallaNacimiento { get; set; }
        public double PesoNacimiento { get; set; }
        public double PerimetroCefalico { get; set; }
        public int Apgar { get; set; }
        public double EdadGestacional { get; set; }
        public string ClasificacionUniversal { get; set; }

        public BLDatosNacimiento()
        {

        }
        public BLDatosNacimiento(double tallaNacimiento, double pesoNacimiento, double perimetroCefalico, 
            int apgar, double edadGestacional, string clasificacionUniversal)
        {
            this.TallaNacimiento = tallaNacimiento;
            this.PesoNacimiento = pesoNacimiento;
            this.PerimetroCefalico = perimetroCefalico;
            this.Apgar = apgar;
            this.EdadGestacional = edadGestacional;
            this.ClasificacionUniversal = clasificacionUniversal;
        }

    }
}
