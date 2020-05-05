using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;
using DAO;

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
                    examenFisico);

                return dao.CrearConsulta(to);
            }
            else
            {
                confirmacion = "Error: No se pudo ingresar la consulta en el sistema";
            }
            return confirmacion;
        }
    }
}
