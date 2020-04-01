using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLHistoriaClinica
    { 
        public string Perinatales { get; set; }
        public string Patologicos { get; set; }
        public string Quirurgicos { get; set; }
        public string Traumaticos { get; set; }
        public string HeredoFamiliares { get; set; }
        public string Alergias { get; set; }
        public string Vacunas { get; set; }
        public BLDatosNacimiento DatosNacimiento { get; set; }

        public BLHistoriaClinica()
        {

        }
        public BLHistoriaClinica(string perinatales, string patologicos, string quirurgicos, 
            string traumaticos, string heredoFamiliares, string alergias, string vacunas, BLDatosNacimiento datosNacimiento)
        {
            this.Perinatales = perinatales;
            this.Patologicos = patologicos;
            this.Quirurgicos = quirurgicos;
            this.Traumaticos = traumaticos;
            this.HeredoFamiliares = heredoFamiliares;
            this.Alergias = alergias;
            this.Vacunas = vacunas;
            this.DatosNacimiento = datosNacimiento;
        }

    }
}
