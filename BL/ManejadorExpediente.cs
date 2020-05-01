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
                    expediente.UrlExpedienteAntiguo, expediente.CodigoDireccion,
                    expediente.DireccionExacta, expediente.IDMedico, expediente.FechaCreacion,
                    historiaClinica, encargado, destinatario, solicitante);

                return dao.CrearExpediente(to);
            }
            else
            {
                confirmacion = "Error: No se pudo ingresar el to en el sistema";
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

        public string CargarExpediente(BLExpediente expediente)
        {

            TOEncargado toencargado = new TOEncargado();
            TODestinatarioFactura todestinatario = new TODestinatarioFactura();
            TOSolicitanteCita tosolicitante = new TOSolicitanteCita();
            TODatosNacimiento todatosNacimiento = new TODatosNacimiento();
            TOHistoriaClinica tohistoriaClinica = new TOHistoriaClinica();

            TOExpediente to = new TOExpediente();

            DAOExpediente dao = new DAOExpediente();

            string confirmacion = "Error: Indefinido.";

            to.IDExpediente = expediente.IDExpediente;
            to.Encargado = toencargado;
            to.DestinatarioFactura = todestinatario;
            to.SolicitanteCita = tosolicitante;
            to.HistoriaClinica = tohistoriaClinica;
            to.HistoriaClinica.DatosNacimiento = todatosNacimiento;

            confirmacion = dao.CargarExpediente(to);

            if (!confirmacion.Contains("Error:"))
            {
                BLEncargado encargado = null;

                if (to.Encargado != null)
                {
                    encargado = new BLEncargado(
                                to.Encargado.Cedula, to.Encargado.Nombre, to.Encargado.PrimerApellido,
                                to.Encargado.SegundoApellido, to.Encargado.Telefono, to.Encargado.Correo,
                                to.Encargado.Parentesco, to.Encargado.CodigoDireccion,
                                to.Encargado.DireccionExacta);
                }

                BLDestinatarioFactura destinatario = null;

                if (to.DestinatarioFactura != null)
                {
                    destinatario = new BLDestinatarioFactura(
                                to.DestinatarioFactura.Cedula, to.DestinatarioFactura.Nombre,
                                to.DestinatarioFactura.PrimerApellido,
                                to.DestinatarioFactura.SegundoApellido, to.DestinatarioFactura.Telefono,
                                to.DestinatarioFactura.Correo,
                                to.DestinatarioFactura.CodigoDireccion,
                                to.DestinatarioFactura.DireccionExacta);
                }

                BLSolicitanteCita solicitante = null;

                if (to.SolicitanteCita != null)
                {
                    solicitante = new BLSolicitanteCita(to.SolicitanteCita.Cedula, to.SolicitanteCita.Correo, to.SolicitanteCita.Contrasenna,
                    to.SolicitanteCita.Telefono);
                }

                BLHistoriaClinica historiaClinica = null;

                if (to.HistoriaClinica != null)
                {

                    BLDatosNacimiento datosNacimiento = null;

                    if (to.HistoriaClinica.DatosNacimiento != null)
                    {

                        datosNacimiento = new BLDatosNacimiento(
                            to.HistoriaClinica.DatosNacimiento.TallaNacimiento,
                            to.HistoriaClinica.DatosNacimiento.PesoNacimiento,
                            to.HistoriaClinica.DatosNacimiento.PerimetroCefalico,
                            to.HistoriaClinica.DatosNacimiento.Apgar,
                            to.HistoriaClinica.DatosNacimiento.EdadGestacional,
                            to.HistoriaClinica.DatosNacimiento.ClasificacionUniversal);
                    }

                    historiaClinica = new BLHistoriaClinica(
                        to.HistoriaClinica.Perinatales, to.HistoriaClinica.Patologicos,
                        to.HistoriaClinica.Quirurgicos, to.HistoriaClinica.Traumaticos,
                        to.HistoriaClinica.HeredoFamiliares, to.HistoriaClinica.Alergias,
                        to.HistoriaClinica.Vacunas, datosNacimiento);
                }

                expediente.IDExpediente = to.IDExpediente;
                expediente.Cedula = to.Cedula;
                expediente.Nombre = to.Nombre;
                expediente.PrimerApellido = to.PrimerApellido;
                expediente.SegundoApellido = to.SegundoApellido;
                expediente.FechaNacimiento = to.FechaNacimiento;
                expediente.Sexo = to.Sexo;
                expediente.UrlExpedienteAntiguo = to.UrlExpedienteAntiguo;
                expediente.CodigoDireccion = to.CodigoDireccion;
                expediente.DireccionExacta = to.DireccionExacta;
                expediente.IDMedico = to.IDMedico;
                expediente.FechaCreacion = to.FechaCreacion;

                expediente.HistoriaClinica = historiaClinica;
                expediente.Encargado = encargado;
                expediente.DestinatarioFactura = destinatario;
                expediente.SolicitanteCita = solicitante;

            }

            return confirmacion;

        }

    }
}
