﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;
using DAO;
using System.Threading;
using System.IO;
using System.Web;

namespace BL
{
    public class ManejadorConsultas
    {
        public string CrearConsulta(BLConsulta consulta)
        {
            string confirmacion = "Error: Indefinido.";

            if (consulta != null)
            {
                DAOConsulta dao = new DAOConsulta();

                TOExamenFisico examenFisico = null;

                if (consulta.ExamenFisico != null)
                {
                    examenFisico = new TOExamenFisico(consulta.ExamenFisico.Peso, consulta.ExamenFisico.Talla,
                        consulta.ExamenFisico.PerimetroCefalico, consulta.ExamenFisico.IMC, consulta.ExamenFisico.SO2,
                        consulta.ExamenFisico.Temperatura, consulta.ExamenFisico.PC_Edad, consulta.ExamenFisico.Peso_Edad,
                        consulta.ExamenFisico.Talla_Edad, consulta.ExamenFisico.Peso_Talla, consulta.ExamenFisico.IMC_Edad,
                        consulta.ExamenFisico.EstadoAlerta, consulta.ExamenFisico.EstadoHidratacion, consulta.ExamenFisico.RuidosCardiacos,
                        consulta.ExamenFisico.CamposPulmonares, consulta.ExamenFisico.Abdomen, consulta.ExamenFisico.Faringe,
                        consulta.ExamenFisico.Nariz, consulta.ExamenFisico.Oidos, consulta.ExamenFisico.SNC, consulta.ExamenFisico.Neurodesarrollo,
                        consulta.ExamenFisico.SistemaOsteomuscular, consulta.ExamenFisico.Piel, consulta.ExamenFisico.OtrosHallazgos);
                }

                TOConsulta to = new TOConsulta(consulta.IDExpediente, consulta.Fecha, consulta.Hora, consulta.PadecimientoActual,
                    consulta.Analisis, consulta.ImpresionDiagnostica, consulta.Plan, consulta.MMFrecuencia, consulta.MMReferidoA,
                    consulta.CPEspecialidad, consulta.CPMotivo, examenFisico, consulta.Enfermedad);

                return dao.CrearConsulta(to);
            }
            else
            {
                confirmacion = "Error: No se pudo ingresar la consulta en el sistema";
            }
            return confirmacion;
        }

        public string IngresarEnfermedad(string enfermedad)
        {
            string confirmacion = "Error: Indefinido.";

            if (enfermedad != null && !enfermedad.Equals(""))
            {
                DAOConsulta dao = new DAOConsulta();
                return dao.IngresarEnfermedad(enfermedad);
            }
            else
            {
                confirmacion = "Error: No se pudo ingresar la enfermedad";
            }
            return confirmacion;
        }
        public string EliminarEnfermedad(string enfermedad)
        {
            string confirmacion = "Error: Indefinido.";

            if (enfermedad != null && !enfermedad.Equals(""))
            {
                DAOConsulta dao = new DAOConsulta();
                return dao.EliminarEnfermedad(enfermedad);
            }
            else
            {
                confirmacion = "Error: No se pudo eliminar la enfermedad";
            }
            return confirmacion;
        }
        public string CargarEnfermedades(List<string> enfermedades)
        {
            string confirmacion = "Error: Indefinido.";

            if (enfermedades != null)
            {
                DAOConsulta dao = new DAOConsulta();
                return dao.CargarEnfermedades(enfermedades);
            }
            else
            {
                confirmacion = "Error: No se pudieron cargar las enfermedades";
            }
            return confirmacion;
        }
        public string CargarConsultas(List<BLConsulta> consultas, BLExpediente expediente)
        {
            List<TOConsulta> to = new List<TOConsulta>();
            DAOConsulta dao = new DAOConsulta();

            TOExpediente toExp = new TOExpediente();
            toExp.IDExpediente = expediente.IDExpediente;

            string confirmacion = "Error: Indefinido.";

            confirmacion = dao.CargarConsultas(to, toExp);

            if (!confirmacion.Contains("Error:"))
            {
                foreach (TOConsulta t in to)
                {
                    BLConsulta c = new BLConsulta();
                    c.IDExpediente = t.IDExpediente;
                    c.Fecha = t.Fecha;
                    c.Hora = t.Hora;
                    consultas.Add(c);
                }
                expediente.Cedula = toExp.Cedula;
                expediente.Nombre = toExp.Nombre;
                expediente.PrimerApellido = toExp.PrimerApellido;
                expediente.SegundoApellido = toExp.SegundoApellido;
            }

            return confirmacion;

        }
        public string CargarConsulta(BLConsulta consulta)
        {
            TOConsulta to = new TOConsulta();
            to.ExamenFisico = new TOExamenFisico();
            DAOConsulta dao = new DAOConsulta();

            to.IDExpediente = consulta.IDExpediente;
            to.Fecha = consulta.Fecha;

            string confirmacion = "Error: Indefinido.";

            confirmacion = dao.CargarConsulta(to);

            if (!confirmacion.Contains("Error:"))
            {
                consulta.Hora = to.Hora;
                consulta.PadecimientoActual = to.PadecimientoActual;
                consulta.Analisis = to.Analisis;
                consulta.ImpresionDiagnostica = to.ImpresionDiagnostica;
                consulta.Plan = to.Plan;
                consulta.MMFrecuencia = to.MMFrecuencia;
                consulta.MMReferidoA = to.MMReferidoA;
                consulta.CPEspecialidad = to.CPEspecialidad;
                consulta.CPMotivo = to.CPMotivo;
                consulta.Enfermedad = to.Enfermedad;
                consulta.ExamenFisico.Peso = to.ExamenFisico.Peso;
                consulta.ExamenFisico.Talla = to.ExamenFisico.Talla;
                consulta.ExamenFisico.IMC = to.ExamenFisico.IMC;
                consulta.ExamenFisico.Temperatura = to.ExamenFisico.Temperatura;
                consulta.ExamenFisico.PC_Edad = to.ExamenFisico.PC_Edad;
                consulta.ExamenFisico.Peso_Edad = to.ExamenFisico.Peso_Edad;
                consulta.ExamenFisico.Talla_Edad = to.ExamenFisico.Talla_Edad;
                consulta.ExamenFisico.Peso_Talla = to.ExamenFisico.Peso_Talla;
                consulta.ExamenFisico.IMC_Edad = to.ExamenFisico.IMC_Edad;
                consulta.ExamenFisico.PerimetroCefalico = to.ExamenFisico.PerimetroCefalico;
                consulta.ExamenFisico.SO2 = to.ExamenFisico.SO2;
                consulta.ExamenFisico.EstadoAlerta = to.ExamenFisico.EstadoAlerta;
                consulta.ExamenFisico.EstadoHidratacion = to.ExamenFisico.EstadoHidratacion;
                consulta.ExamenFisico.RuidosCardiacos = to.ExamenFisico.RuidosCardiacos;
                consulta.ExamenFisico.CamposPulmonares = to.ExamenFisico.CamposPulmonares;
                consulta.ExamenFisico.Abdomen = to.ExamenFisico.Abdomen;
                consulta.ExamenFisico.Faringe = to.ExamenFisico.Faringe;
                consulta.ExamenFisico.Nariz = to.ExamenFisico.Nariz;
                consulta.ExamenFisico.Oidos = to.ExamenFisico.Oidos;
                consulta.ExamenFisico.SNC = to.ExamenFisico.SNC;
                consulta.ExamenFisico.Neurodesarrollo = to.ExamenFisico.Neurodesarrollo;
                consulta.ExamenFisico.SistemaOsteomuscular = to.ExamenFisico.SistemaOsteomuscular;
                consulta.ExamenFisico.Piel = to.ExamenFisico.Piel;
                consulta.ExamenFisico.OtrosHallazgos = to.ExamenFisico.OtrosHallazgos;
            }

            return confirmacion;

        }

        public string ActualizarConsulta(BLConsulta consulta)
        {
            string confirmacion = "Error: Indefinido.";

            if (consulta != null)
            {
                DAOConsulta dao = new DAOConsulta();

                TOExamenFisico examenFisico = null;

                if (consulta.ExamenFisico != null)
                {
                    examenFisico = new TOExamenFisico(consulta.ExamenFisico.Peso, consulta.ExamenFisico.Talla,
                        consulta.ExamenFisico.PerimetroCefalico, consulta.ExamenFisico.IMC, consulta.ExamenFisico.SO2,
                        consulta.ExamenFisico.Temperatura, consulta.ExamenFisico.PC_Edad, consulta.ExamenFisico.Peso_Edad,
                        consulta.ExamenFisico.Talla_Edad, consulta.ExamenFisico.Peso_Talla, consulta.ExamenFisico.IMC_Edad,
                        consulta.ExamenFisico.EstadoAlerta, consulta.ExamenFisico.EstadoHidratacion, consulta.ExamenFisico.RuidosCardiacos,
                        consulta.ExamenFisico.CamposPulmonares, consulta.ExamenFisico.Abdomen, consulta.ExamenFisico.Faringe,
                        consulta.ExamenFisico.Nariz, consulta.ExamenFisico.Oidos, consulta.ExamenFisico.SNC, consulta.ExamenFisico.Neurodesarrollo,
                        consulta.ExamenFisico.SistemaOsteomuscular, consulta.ExamenFisico.Piel, consulta.ExamenFisico.OtrosHallazgos);
                }

                TOConsulta to = new TOConsulta(consulta.IDExpediente, consulta.Fecha, consulta.Hora, consulta.PadecimientoActual,
                    consulta.Analisis, consulta.ImpresionDiagnostica, consulta.Plan, consulta.MMFrecuencia, consulta.MMReferidoA,
                    consulta.CPEspecialidad, consulta.CPMotivo, examenFisico, consulta.Enfermedad);

                return dao.ActualizarConsulta(to);
            }
            else
            {
                confirmacion = "Error: No se pudo actualizar la consulta en el sistema";
            }
            return confirmacion;
        }

        public string CargarConsultasDia(List<BLConsulta> consultas, List<BLExpediente> expedientes, string idMedico, string fechaActual)
        {
            List<TOExpediente> toExp = new List<TOExpediente>();
            List<TOConsulta> toCon = new List<TOConsulta>();
            DAOConsulta dao = new DAOConsulta();

            string confirmacion = "Error: Indefinido.";

            confirmacion = dao.CargarConsultasDia(toCon, toExp, idMedico, fechaActual);

            if (!confirmacion.Contains("Error:"))
            {
                foreach (TOExpediente t in toExp)
                {
                    BLExpediente e = new BLExpediente();
                    e.IDExpediente = t.IDExpediente;
                    e.Cedula = t.Cedula;
                    e.Nombre = t.Nombre;
                    e.PrimerApellido = t.PrimerApellido;
                    e.SegundoApellido = t.SegundoApellido;
                    expedientes.Add(e);
                }
                foreach (TOConsulta t in toCon)
                {
                    BLConsulta c = new BLConsulta();
                    c.Hora = t.Hora;
                    consultas.Add(c);
                }
            }
            return confirmacion;
        }

        public void IniciarHiloEliminacion(string fechaActual)
        {
            Thread proceso = new Thread(new ThreadStart(new ContenedorFecha(fechaActual).EliminarConsultasDia));
            proceso.Start();

        }

        public string CargarReferencia(BLReferencia referencia)
        {
            TOReferencia to = new TOReferencia();
            DAOConsulta dao = new DAOConsulta();

            to.IdCuenta = referencia.IdCuenta;
            to.IdExpediente = referencia.IdExpediente;

            string confirmacion = "Error: Indefinido.";

            confirmacion = dao.CargarReferencia(to);

            if (!confirmacion.Contains("Error:"))
            {
                referencia.NombreMedico = to.NombreMedico;
                referencia.CodigoMedico = to.CodigoMedico;
                referencia.TelefonoMedico = to.TelefonoMedico;
                referencia.CorreoMedico = to.CorreoMedico;
                referencia.CedulaPaciente = to.CedulaPaciente;
                referencia.NombrePaciente = to.NombrePaciente;
                referencia.SexoPaciente = to.SexoPaciente;
                referencia.EdadPaciente = to.EdadPaciente;
            }
            return confirmacion;
        }
        private class ContenedorFecha
        {
            public string FechaActual { get; set; }

            public ContenedorFecha(string fechaActual)
            {
                this.FechaActual = fechaActual;
            }

            public void EliminarConsultasDia()
            {
                if (this.FechaActual != null && !this.FechaActual.Equals(""))
                {
                    DAOConsulta dao = new DAOConsulta();
                    string confirmacion = dao.EliminarConsultasDia(this.FechaActual);
                }
            }
        }
    }
}
