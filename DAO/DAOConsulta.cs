using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;
using System.Data.SqlClient;
using System.Data;

namespace DAO
{
    public class DAOConsulta
    {
        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexion);

        public string CrearConsulta(TOConsulta consulta)
        {
            string confirmacion = "La consulta se creó exitosamente";
            // Se abre la conexión

            if (conexion != null)
            {
                try
                {
                    if (conexion.State != ConnectionState.Open)
                    {
                        conexion.Open();
                    }
                }
                catch (Exception)
                {
                    confirmacion = "Error: No se pudo ingresar la consulta en el sistema";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pudo ingresar la consulta en el sistema";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction("Ingresar nueva consulta");

                // Se crea un nuevo comando con la secuencia SQL y el objeto de conexión

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.Transaction = transaccion;

                if (consulta != null)
                {
                    comando.CommandText = "INSERT INTO CONSULTA (ID_EXPEDIENTE, FECHA, HORA, PADECIMIENTO_ACTUAL, " +
                        "ANALISIS, IMPRESION_DIAGNOSTICA, DESCRIPCION_PLAN, MEDICINA_MIXTA_FRECUENCIA_REFERIDO_A) VALUES(" +
                        "@idExpediente, @fecha, @hora, @padecimiento, @analisis, @impresion, @plan, @medicinaMixta);";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@idExpediente", consulta.IDExpediente);
                    comando.Parameters.AddWithValue("@fecha", consulta.Fecha);
                    comando.Parameters.AddWithValue("@hora", consulta.Hora);
                    comando.Parameters.AddWithValue("@padecimiento", consulta.PadecimientoActual);
                    comando.Parameters.AddWithValue("@analisis", consulta.Analisis);
                    comando.Parameters.AddWithValue("@impresion", consulta.ImpresionDiagnostica);
                    comando.Parameters.AddWithValue("@plan", consulta.Plan);
                    comando.Parameters.AddWithValue("@medicinaMixta", consulta.MMFrecuencia + "|" + consulta.MMReferidoA);

                    comando.ExecuteNonQuery();

                    comando.Parameters.Clear();

                    if (consulta.ExamenFisico != null)
                    {
                        comando.CommandText = "INSERT INTO EXAMEN_FISICO (ID_EXPEDIENTE, FECHA, PESO, TALLA, " +
                            "IMC, S02, TEMPERATURA, GRAFICAS_CRECIMIENTO_AND_PERIMETRO_CEFALICO, ESTADO_ALERTA, " +
                            "ESTADO_HIDRATACION, RUIDOS_CARDIACOS, CAMPOS_PULMONARES, ABDOMEN, FARINGE, " +
                            "NARIZ, OIDOS, SNC, NEURODESARROLLO, SISTEMA_OSTEOMUSCULAR, PIEL, OTROS_HALLAZGOS) VALUES(" +
                            "@idExpediente, @fecha, @peso, @talla, @imc, @so2, @temperatura, @graficasYperimetro, @estadoAlerta, " +
                            "@estadoHidratacion, @ruidosCardiacos, @camposPulmonares, @abdomen, @faringe, @nariz, @oidos, @snc, " +
                            "@neurodesarrollo, @sistemaOsteomuscular, @piel, @otrosHallazgos);";

                        // Se asigna un valor a los parámetros del comando a ejecutar

                        comando.Parameters.AddWithValue("@idExpediente", consulta.IDExpediente);
                        comando.Parameters.AddWithValue("@fecha", consulta.Fecha);
                        comando.Parameters.AddWithValue("@peso", consulta.ExamenFisico.Peso);
                        comando.Parameters.AddWithValue("@talla", consulta.ExamenFisico.Talla);
                        comando.Parameters.AddWithValue("@imc", consulta.ExamenFisico.IMC);
                        comando.Parameters.AddWithValue("@so2", consulta.ExamenFisico.SO2);
                        comando.Parameters.AddWithValue("@temperatura", consulta.ExamenFisico.Temperatura);
                        comando.Parameters.AddWithValue("@graficasYperimetro", consulta.ExamenFisico.PC_Edad + "|" +
                            consulta.ExamenFisico.Peso_Edad + "|" + consulta.ExamenFisico.Talla_Edad + "|" + 
                            consulta.ExamenFisico.Peso_Talla + "|" + consulta.ExamenFisico.IMC_Edad + "*" + 
                            consulta.ExamenFisico.PerimetroCefalico);
                        comando.Parameters.AddWithValue("@estadoAlerta", consulta.ExamenFisico.EstadoAlerta);
                        comando.Parameters.AddWithValue("@estadoHidratacion", consulta.ExamenFisico.EstadoHidratacion);
                        comando.Parameters.AddWithValue("@ruidosCardiacos", consulta.ExamenFisico.RuidosCardiacos);
                        comando.Parameters.AddWithValue("@camposPulmonares", consulta.ExamenFisico.CamposPulmonares);
                        comando.Parameters.AddWithValue("@abdomen", consulta.ExamenFisico.Abdomen);
                        comando.Parameters.AddWithValue("@faringe", consulta.ExamenFisico.Faringe);
                        comando.Parameters.AddWithValue("@nariz", consulta.ExamenFisico.Nariz);
                        comando.Parameters.AddWithValue("@oidos", consulta.ExamenFisico.Oidos);
                        comando.Parameters.AddWithValue("@snc", consulta.ExamenFisico.SNC);
                        comando.Parameters.AddWithValue("@neurodesarrollo", consulta.ExamenFisico.Neurodesarrollo);
                        comando.Parameters.AddWithValue("@sistemaOsteomuscular", consulta.ExamenFisico.SistemaOsteomuscular);
                        comando.Parameters.AddWithValue("@piel", consulta.ExamenFisico.Piel);
                        comando.Parameters.AddWithValue("@otrosHallazgos", consulta.ExamenFisico.OtrosHallazgos);

                        comando.ExecuteNonQuery();

                    }
                }

                transaccion.Commit();

            }
            catch (Exception e)
            {
                try
                {
                    string prueba = e.Message;
                    // En caso de un error se realiza un rollback a la transacción

                    transaccion.Rollback();
                }
                catch (Exception)
                {
                }
                finally
                {
                    confirmacion = "Error: No se pudo ingresar la consulta en el sistema";
                }
            }
            finally
            {
                if (conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
            return confirmacion;
        }

    }
}
