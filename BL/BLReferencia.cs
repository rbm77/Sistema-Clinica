using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLReferencia
    {
        public string NombreClinica { get; set; }
        public string NombreMedico { get; set; }
        public string CodigoMedico { get; set; }
        public string TelefonoMedico { get; set; }
        public string CorreoMedico { get; set; }
        public string FechaReferencia { get; set; }
        public string CedulaPaciente { get; set; }
        public string NombrePaciente { get; set; }
        public string SexoPaciente { get; set; }
        public string EdadPaciente { get; set; }
        public string Analisis { get; set; }
        public string ImpresionDiagnóstica { get; set; }
        public string Especialidad { get; set; }
        public string Motivo { get; set; }

        public BLReferencia()
        {
        }
        public BLReferencia(string nombreClinica, string nombreMedico, string codigoMedico, string telefonoMedico,
            string correoMedico, string fechaReferencia, string cedulaPaciente, string nombrePaciente, string sexoPaciente,
            string edadPaciente, string analisis, string impresionDiagnostica, string especialidad, string motivo)
        {
            this.NombreClinica = nombreClinica;
            this.NombreMedico = nombreMedico;
            this.CodigoMedico = codigoMedico;
            this.TelefonoMedico = telefonoMedico;
            this.CorreoMedico = correoMedico;
            this.FechaReferencia = fechaReferencia;
            this.CedulaPaciente = cedulaPaciente;
            this.NombrePaciente = nombrePaciente;
            this.SexoPaciente = sexoPaciente;
            this.EdadPaciente = edadPaciente;
            this.Analisis = analisis;
            this.ImpresionDiagnóstica = impresionDiagnostica;
            this.Especialidad = especialidad;
            this.Motivo = motivo;
        }
    }
}
