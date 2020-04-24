using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using TO;

namespace BL
{
    public class ManejadorExpediente
    {
        public string CrearExpediente(BLExpediente expediente)
        {
            string confirmacion = "Error: Indefinido.";

            if (expediente != null)
            {
                DAOExpediente dao = new DAOExpediente();

                TOEncargado encargado = null;

                if (expediente.Encargado != null)
                {
                    encargado = new TOEncargado(
                                expediente.Encargado.Cedula, expediente.Encargado.Nombre, expediente.Encargado.PrimerApellido,
                                expediente.Encargado.SegundoApellido, expediente.Encargado.Telefono, expediente.Encargado.Correo,
                                expediente.Encargado.Parentesco, expediente.Encargado.CodigoDireccion,
                                expediente.Encargado.DireccionExacta);
                }

                TODestinatarioFactura destinatario = null;

                if (expediente.DestinatarioFactura != null)
                {
                    destinatario = new TODestinatarioFactura(
                                expediente.DestinatarioFactura.Cedula, expediente.DestinatarioFactura.Nombre,
                                expediente.DestinatarioFactura.PrimerApellido,
                                expediente.DestinatarioFactura.SegundoApellido, expediente.DestinatarioFactura.Telefono,
                                expediente.DestinatarioFactura.Correo,
                                expediente.DestinatarioFactura.CodigoDireccion,
                                expediente.DestinatarioFactura.DireccionExacta);
                }

                TOSolicitanteCita solicitante = null;

                if (expediente.SolicitanteCita != null)
                {
                    solicitante = new TOSolicitanteCita(expediente.SolicitanteCita.Cedula, expediente.SolicitanteCita.Correo, expediente.SolicitanteCita.Contrasenna,
                    expediente.SolicitanteCita.Telefono);
                }

                TOHistoriaClinica historiaClinica = null;

                if (expediente.HistoriaClinica != null)
                {

                    TODatosNacimiento datosNacimiento = null;

                    if (expediente.HistoriaClinica.DatosNacimiento != null)
                    {

                        datosNacimiento = new TODatosNacimiento(
                            expediente.HistoriaClinica.DatosNacimiento.TallaNacimiento,
                            expediente.HistoriaClinica.DatosNacimiento.PesoNacimiento,
                            expediente.HistoriaClinica.DatosNacimiento.PerimetroCefalico,
                            expediente.HistoriaClinica.DatosNacimiento.Apgar,
                            expediente.HistoriaClinica.DatosNacimiento.EdadGestacional,
                            expediente.HistoriaClinica.DatosNacimiento.ClasificacionUniversal);
                    }

                    historiaClinica = new TOHistoriaClinica(
                        expediente.HistoriaClinica.Perinatales, expediente.HistoriaClinica.Patologicos,
                        expediente.HistoriaClinica.Quirurgicos, expediente.HistoriaClinica.Traumaticos,
                        expediente.HistoriaClinica.HeredoFamiliares, expediente.HistoriaClinica.Alergias,
                        expediente.HistoriaClinica.Vacunas, datosNacimiento);
                }


                TOExpediente to = new TOExpediente(expediente.IDExpediente, expediente.Cedula, expediente.Nombre,
                    expediente.PrimerApellido, expediente.SegundoApellido, expediente.FechaNacimiento, expediente.Sexo,
                    expediente.UrlFoto, expediente.UrlExpedienteAntiguo, expediente.CodigoDireccion,
                    expediente.DireccionExacta, expediente.IDMedico, expediente.FechaCreacion,
                    historiaClinica, encargado, destinatario, solicitante);

                return dao.CrearExpediente(to);
            }
            else
            {
                confirmacion = "Error: No se pudo ingresar el expediente en el sistema";
            }
            return confirmacion;
        }

        public string CargarExpedientes(List<BLExpediente> expedientes, string idMedico)
        {
            List<TOExpediente> to = new List<TOExpediente>();
            DAOExpediente dao = new DAOExpediente();

            string confirmacion = "Error: Indefinido.";

            confirmacion = dao.CargarExpedientes(to, idMedico);

            if (!confirmacion.Contains("Error:"))
            {
                foreach (TOExpediente t in to)
                {
                    BLExpediente e = new BLExpediente();
                    e.IDExpediente = t.IDExpediente;
                    e.Cedula = t.Cedula;
                    e.Nombre = t.Nombre;
                    e.PrimerApellido = t.PrimerApellido;
                    e.SegundoApellido = t.SegundoApellido;
                    expedientes.Add(e);
                }
            }

            return confirmacion;

        }
    }
}
