using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOMedico
    {
        public string IdMedico { get; set; }
        public string CodigoMedico { get; set; }
        public string Especialidad { get; set; }
        public string DuracionCita { get; set; }

        public TOMedico()
        {

        }

        public TOMedico(string idMedico, string codigoMedico, string especialidad, string duracionCita)
        {
            this.IdMedico = idMedico;
            this.CodigoMedico = codigoMedico;
            this.Especialidad = especialidad;
            this.DuracionCita = duracionCita;
        }
    }
}
