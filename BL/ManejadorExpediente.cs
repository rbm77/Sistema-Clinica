﻿using System;
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
                TOExpediente to = new TOExpediente(expediente.IDExpediente, expediente.Cedula, expediente.Nombre,
                    expediente.PrimerApellido, expediente.SegundoApellido, expediente.FechaNacimiento, expediente.Sexo,
                    expediente.UrlFoto, expediente.UrlExpedienteAntiguo, 
                    new TODireccion(expediente.Direccion.CodigoDireccion,
                    expediente.Direccion.Provincia, expediente.Direccion.Canton, expediente.Direccion.Distrito),
                    expediente.DireccionExacta, expediente.IDMedico, expediente.FechaCreacion, 
                    new TOHistoriaClinica(
                        expediente.HistoriaClinica.Perinatales, expediente.HistoriaClinica.Patologicos,
                        expediente.HistoriaClinica.Quirurgicos, expediente.HistoriaClinica.Traumaticos,
                        expediente.HistoriaClinica.HeredoFamiliares, expediente.HistoriaClinica.Alergias,
                        expediente.HistoriaClinica.Vacunas, 
                        new TODatosNacimiento(
                            expediente.HistoriaClinica.DatosNacimiento.TallaNacimiento,
                            expediente.HistoriaClinica.DatosNacimiento.PesoNacimiento,
                            expediente.HistoriaClinica.DatosNacimiento.PerimetroCefalico,
                            expediente.HistoriaClinica.DatosNacimiento.Apgar,
                            expediente.HistoriaClinica.DatosNacimiento.EdadGestacional,
                            expediente.HistoriaClinica.DatosNacimiento.ClasificacionUniversal)),
                    new TOEncargado(
                                expediente.Encargado.Cedula, expediente.Encargado.Nombre, expediente.Encargado.PrimerApellido,
                                expediente.Encargado.SegundoApellido, expediente.Encargado.Telefono, expediente.Encargado.Correo,
                                expediente.Encargado.Parentesco, new TODireccion(
                                    expediente.Encargado.Direccion.CodigoDireccion, expediente.Encargado.Direccion.Provincia,
                                    expediente.Encargado.Direccion.Canton, expediente.Encargado.Direccion.Distrito)),
                    new TODestinatarioFactura(
                                expediente.DestinatarioFactura.Cedula, expediente.DestinatarioFactura.Nombre, expediente.DestinatarioFactura.PrimerApellido,
                                expediente.DestinatarioFactura.SegundoApellido, expediente.DestinatarioFactura.Telefono, expediente.DestinatarioFactura.Correo,
                                new TODireccion(
                                    expediente.DestinatarioFactura.Direccion.CodigoDireccion, expediente.DestinatarioFactura.Direccion.Provincia,
                                    expediente.DestinatarioFactura.Direccion.Canton, expediente.DestinatarioFactura.Direccion.Distrito)),
                    new TOSolicitanteCita(expediente.SolicitanteCita.Correo, expediente.SolicitanteCita.Contrasenna,
                    expediente.SolicitanteCita.Telefono, expediente.SolicitanteCita.Estado));
            }
            else
            {
                confirmacion = "Error: No se pudo ingresar el expediente en el sistema";
            }
            return confirmacion;
        }
    }
}
