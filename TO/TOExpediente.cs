using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO
{
    public class TOExpediente
    {
        public int IDExpediente { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string UrlFoto { get; set; }
        public string UrlExpedienteAntiguo { get; set; }
        public string CodigoDireccion { get; set; }
        public string DireccionExacta { get; set; }
        public string IDMedico { get; set; }
        public string FechaCreacion { get; set; }
        public TOHistoriaClinica HistoriaClinica { get; set; }
        public TOEncargado Encargado { get; set; }
        public TODestinatarioFactura DestinatarioFactura { get; set; }
        public TOSolicitanteCita SolicitanteCita { get; set; }

        public TOExpediente()
        {

        }
        public TOExpediente(int idExpediente, string cedula, string nombre, string primerApellido,
            string segundoApellido, string fechaNacimiento, string sexo, string urlFoto, string urlExpedienteAntiguo,
            string codigoDireccion, string direccionExacta, string idMedico, string fechaCreacion, TOHistoriaClinica historiaClinica,
            TOEncargado encargado, TODestinatarioFactura destinatarioFactura, TOSolicitanteCita solicitanteCita)
        {
            this.IDExpediente = idExpediente;
            this.Nombre = nombre;
            this.PrimerApellido = primerApellido;
            this.SegundoApellido = segundoApellido;
            this.FechaNacimiento = fechaNacimiento;
            this.Sexo = sexo;
            this.UrlFoto = urlFoto;
            this.UrlExpedienteAntiguo = urlExpedienteAntiguo;
            this.DireccionExacta = direccionExacta;
            this.IDMedico = idMedico;
            this.FechaNacimiento = fechaCreacion;
            this.CodigoDireccion = codigoDireccion;
            this.HistoriaClinica = historiaClinica;
            this.Encargado = encargado;
            this.DestinatarioFactura = destinatarioFactura;
            this.SolicitanteCita = solicitanteCita;
        }

    }
}
