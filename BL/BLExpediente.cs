using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLExpediente
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
        public BLDireccion Direccion { get; set; }
        public string DireccionExacta { get; set; }
        public string IDMedico { get; set; }
        public string FechaCreacion { get; set; }
        public BLHistoriaClinica HistoriaClinica { get; set; }
        public BLEncargado Encargado { get; set; }
        public BLDestinatarioFactura DestinatarioFactura { get; set; }
        public BLSolicitanteCita SolicitanteCita { get; set; }
        
        public BLExpediente()
        {

        }
        public BLExpediente(int idExpediente, string cedula, string nombre, string primerApellido, 
            string segundoApellido, string fechaNacimiento, string sexo, string urlFoto, string urlExpedienteAntiguo,
            BLDireccion direccion, string direccionExacta, string idMedico, string fechaCreacion, BLHistoriaClinica historiaClinica,
            BLEncargado encargado, BLDestinatarioFactura destinatarioFactura, BLSolicitanteCita solicitanteCita)
        {
            this.IDExpediente = idExpediente;
            this.Nombre = nombre;
            this.PrimerApellido = primerApellido;
            this.SegundoApellido = segundoApellido;
            this.FechaNacimiento = fechaNacimiento;
            this.Sexo = sexo;
            this.UrlFoto = urlFoto;
            this.Direccion = direccion;
            this.UrlExpedienteAntiguo = urlExpedienteAntiguo;
            this.DireccionExacta = direccionExacta;
            this.IDMedico = idMedico;
            this.FechaNacimiento = fechaCreacion;
            this.HistoriaClinica = historiaClinica;
            this.Encargado = encargado;
            this.DestinatarioFactura = destinatarioFactura;
            this.SolicitanteCita = solicitanteCita;
        }
    }
}
