using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLConsulta
    {
        public int IDExpediente { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string PadecimientoActual { get; set; }
        public string Analisis { get; set; }
        public string ImpresionDiagnostica { get; set; }
        public string Plan { get; set; }
        public string MMFrecuencia { get; set; }
        public string MMReferidoA { get; set; }

        public BLConsulta()
        {

        }
        public BLConsulta(int idExpediente, string fecha, string hora, string padecimientoActual,
            string analisis, string impresionDiagnostica, string plan, string mmFrecuencia, string mmReferidoA)
        {
            this.IDExpediente = idExpediente;
            this.Fecha = fecha;
            this.Hora = hora;
            this.PadecimientoActual = padecimientoActual;
            this.Analisis = analisis;
            this.ImpresionDiagnostica = impresionDiagnostica;
            this.Plan = plan;
            this.MMFrecuencia = mmFrecuencia;
            this.MMReferidoA = mmReferidoA;
        }
    }
}
