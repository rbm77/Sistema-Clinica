using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOReferencia
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
        public string IdCuenta { get; set; }
        public long IdExpediente { get; set; }

        public TOReferencia()
        {
        }
        public TOReferencia(string idCuenta, long idExpediente)
        {
            this.IdCuenta = idCuenta;
            this.IdExpediente = idExpediente;
        }
        public TOReferencia(string nombreClinica, string nombreMedico, string codigoMedico, string telefonoMedico,
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
