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
                        "ANALISIS, IMPRESION_DIAGNOSTICA, DESCRIPCION_PLAN, MEDICINA_MIXTA_FRECUENCIA_REFERIDO_A, " +
                        "CONSULTA_PRIVADA_ESPECIALIDAD_MOTIVO, ENFERMEDAD) VALUES(" +
                        "@idExpediente, @fecha, @hora, @padecimiento, @analisis, @impresion, @plan, @medicinaMixta, @consultaPrivada, @enfermedad);";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@idExpediente", consulta.IDExpediente);
                    comando.Parameters.AddWithValue("@fecha", consulta.Fecha);
                    comando.Parameters.AddWithValue("@hora", consulta.Hora);
                    comando.Parameters.AddWithValue("@padecimiento", consulta.PadecimientoActual);
                    comando.Parameters.AddWithValue("@analisis", consulta.Analisis);
                    comando.Parameters.AddWithValue("@impresion", consulta.ImpresionDiagnostica);
                    comando.Parameters.AddWithValue("@plan", consulta.Plan);

                    // Validamos que no se lleguen a producir problemas de caracteres

                    consulta.MMFrecuencia.Replace("|","/");
                    consulta.MMReferidoA.Replace("|", "/");
                    consulta.CPEspecialidad.Replace("|", "/");
                    consulta.CPMotivo.Replace("|", "/");

                    comando.Parameters.AddWithValue("@medicinaMixta", consulta.MMFrecuencia + "|" + consulta.MMReferidoA);
                    comando.Parameters.AddWithValue("@consultaPrivada", consulta.CPEspecialidad + "|" + consulta.CPMotivo);
                    comando.Parameters.AddWithValue("@enfermedad", consulta.Enfermedad);
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
                            consulta.ExamenFisico.Peso_Talla + "|" + consulta.ExamenFisico.IMC_Edad + "^" + 
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

        public string IngresarEnfermedad(string enfermedad)
        {
            string confirmacion = "La enfermedad se ingresó exitosamente";
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
                    confirmacion = "Error: No se pudo ingresar la enfermedad";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pudo ingresar la enfermedad";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction("Ingresar nueva enfermedad");

                // Se crea un nuevo comando con la secuencia SQL y el objeto de conexión

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.Transaction = transaccion;

                if (enfermedad != null)
                {
                    if (!enfermedad.Equals(""))
                    {
                        comando.CommandText = "INSERT INTO ENFERMEDADES (ENFERMEDAD) VALUES(@enfermedad);";

                        // Se asigna un valor a los parámetros del comando a ejecutar

                        comando.Parameters.AddWithValue("@enfermedad", enfermedad);

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
                    confirmacion = "Error: No se pudo ingresar la enfermedad";
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

        public string EliminarEnfermedad(string enfermedad)
        {
            string confirmacion = "La enfermedad se eliminó exitosamente";
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
                    confirmacion = "Error: No se pudo eliminar la enfermedad";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pudo eliminar la enfermedad";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction("Eliminar enfermedad");

                // Se crea un nuevo comando con la secuencia SQL y el objeto de conexión

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.Transaction = transaccion;

                if (enfermedad != null)
                {
                    if (!enfermedad.Equals(""))
                    {
                        comando.CommandText = "DELETE FROM ENFERMEDADES WHERE ENFERMEDAD = @enfermedad;";

                        // Se asigna un valor a los parámetros del comando a ejecutar

                        comando.Parameters.AddWithValue("@enfermedad", enfermedad);

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
                    confirmacion = "Error: No se pudo eliminar la enfermedad";
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

        public string CargarEnfermedades(List<string> enfermedades)
        {
            string confirmacion = "Las enfermedades se cargaron exitosamente";

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
                    confirmacion = "Error: No se pudieron cargar las enfermedades";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pudieron cargar las enfermedades";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction("Cargar enfermedades");

                // Se crea un nuevo comando con la secuencia SQL y el objeto de conexión

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.CommandText = "SELECT ENFERMEDAD FROM ENFERMEDADES";

                comando.Transaction = transaccion;


                // Se ejecuta el comando y se realiza un commit de la transacción


                SqlDataReader lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        enfermedades.Add(lector["ENFERMEDAD"].ToString());
                    }
                }

                lector.Close();

                transaccion.Commit();

            }
            catch (Exception)
            {
                try
                {

                    // En caso de un error se realiza un rollback a la transacción

                    transaccion.Rollback();
                }
                catch (Exception)
                {
                }
                finally
                {
                    confirmacion = "Error: No se pudieron cargar las enfermedades";
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
        public string CargarConsultas(List<TOConsulta> consultas, TOExpediente expediente)
        {
            string confirmacion = "Las consultas se cargaron exitosamente";

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
                    confirmacion = "Error: No se pueden cargar las consultas";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pueden cargar las consultas";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction("Cargar consultas");

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.CommandText = "SELECT FECHA, HORA FROM CONSULTA WHERE ID_EXPEDIENTE = @idExpediente";

                comando.Transaction = transaccion;

                comando.Parameters.AddWithValue("@idExpediente", expediente.IDExpediente);

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        TOConsulta consulta = new TOConsulta();
                        consulta.IDExpediente = expediente.IDExpediente;
                        consulta.Fecha = lector["FECHA"].ToString();
                        consulta.Hora = lector["HORA"].ToString();

                        consultas.Add(consulta);
                    }
                }

                lector.Close();

                comando.Parameters.Clear();

                // AHORA SE EXTRAEN DATOS DEL EXPEDIENTE PARA MOSTRAR COMO ENCABEZADO

                comando.CommandText = "SELECT CEDULA, NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO" +
                    " FROM EXPEDIENTE WHERE ID_EXPEDIENTE = @idExpediente";

                comando.Parameters.AddWithValue("@idExpediente", expediente.IDExpediente);

                lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        expediente.Cedula = lector["CEDULA"].ToString();
                        expediente.Nombre = lector["NOMBRE"].ToString();
                        expediente.PrimerApellido = lector["PRIMER_APELLIDO"].ToString();
                        expediente.SegundoApellido = lector["SEGUNDO_APELLIDO"].ToString();
                    }
                }

                lector.Close();

                transaccion.Commit();

            }
            catch (Exception)
            {
                try
                {

                    // En caso de un error se realiza un rollback a la transacción

                    transaccion.Rollback();
                }
                catch (Exception)
                {
                }
                finally
                {
                    confirmacion = "Error: No se pueden cargar las consultas";
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
