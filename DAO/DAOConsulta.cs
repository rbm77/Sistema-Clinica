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

                    consulta.CPEspecialidad.Replace("|", "/");
                    consulta.CPMotivo.Replace("|", "/");

                    string medicMixta = "";
                    string consultPrivada = "";

                    if (!consulta.MMFrecuencia.Equals("") && !consulta.MMReferidoA.Equals(""))
                    {
                        medicMixta = consulta.MMFrecuencia + "|" + consulta.MMReferidoA;
                    }

                    if (!consulta.CPEspecialidad.Equals("") && !consulta.CPMotivo.Equals(""))
                    {
                        consultPrivada = consulta.CPEspecialidad + "|" + consulta.CPMotivo;
                    }

                    comando.Parameters.AddWithValue("@medicinaMixta", medicMixta);
                    comando.Parameters.AddWithValue("@consultaPrivada", consultPrivada);
                    comando.Parameters.AddWithValue("@enfermedad", consulta.Enfermedad);
                    comando.ExecuteNonQuery();

                    comando.Parameters.Clear();

                    if (consulta.ExamenFisico != null)
                    {
                        comando.CommandText = "INSERT INTO EXAMEN_FISICO (ID_EXPEDIENTE, FECHA, PESO, TALLA, " +
                            "IMC, TEMPERATURA, GRAFICAS_CRECIMIENTO_ADICIONALES, ESTADO_ALERTA, " +
                            "ESTADO_HIDRATACION, RUIDOS_CARDIACOS, CAMPOS_PULMONARES, ABDOMEN, FARINGE, " +
                            "NARIZ, OIDOS, SNC, NEURODESARROLLO, SISTEMA_OSTEOMUSCULAR, PIEL, OTROS_HALLAZGOS) VALUES(" +
                            "@idExpediente, @fecha, @peso, @talla, @imc, @temperatura, @graficasYadicionales, @estadoAlerta, " +
                            "@estadoHidratacion, @ruidosCardiacos, @camposPulmonares, @abdomen, @faringe, @nariz, @oidos, @snc, " +
                            "@neurodesarrollo, @sistemaOsteomuscular, @piel, @otrosHallazgos);";

                        // Se asigna un valor a los parámetros del comando a ejecutar

                        comando.Parameters.AddWithValue("@idExpediente", consulta.IDExpediente);
                        comando.Parameters.AddWithValue("@fecha", consulta.Fecha);
                        comando.Parameters.AddWithValue("@peso", consulta.ExamenFisico.Peso);
                        comando.Parameters.AddWithValue("@talla", consulta.ExamenFisico.Talla);
                        comando.Parameters.AddWithValue("@imc", consulta.ExamenFisico.IMC);
                        comando.Parameters.AddWithValue("@temperatura", consulta.ExamenFisico.Temperatura);

                        string pcEdad = consulta.ExamenFisico.PC_Edad.Replace("|", "/").Replace("^", "'");
                        string pesoEdad = consulta.ExamenFisico.Peso_Edad.Replace("|", "/").Replace("^", "'");
                        string tallEdad = consulta.ExamenFisico.Talla_Edad.Replace("|", "/").Replace("^", "'");
                        string pesoTalla = consulta.ExamenFisico.Peso_Talla.Replace("|", "/").Replace("^", "'");
                        string imcEdad = consulta.ExamenFisico.IMC_Edad.Replace("|", "/").Replace("^", "'");

                        if (pcEdad.Equals(""))
                        {
                            pcEdad = "nulo";
                        }
                        if (pesoEdad.Equals(""))
                        {
                            pesoEdad = "nulo";
                        }
                        if (tallEdad.Equals(""))
                        {
                            tallEdad = "nulo";
                        }
                        if (pesoTalla.Equals(""))
                        {
                            pesoTalla = "nulo";
                        }
                        if (imcEdad.Equals(""))
                        {
                            imcEdad = "nulo";
                        }

                        comando.Parameters.AddWithValue("@graficasYadicionales", pcEdad + "|" +
                            pesoEdad + "|" + tallEdad + "|" + pesoTalla + "|" + imcEdad + "^" +
                            consulta.ExamenFisico.PerimetroCefalico + "^" + consulta.ExamenFisico.SO2);

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
                        comando.Parameters.Clear();

                    }

                    comando.CommandText = "INSERT INTO CONSULTAS_DIA (ID_EXPEDIENTE, FECHA_CONSULTA) VALUES(" +
                       "@idExpediente, @fecha);";
                    comando.Parameters.AddWithValue("@idExpediente", consulta.IDExpediente);
                    comando.Parameters.AddWithValue("@fecha", consulta.Fecha);
                    comando.ExecuteNonQuery();
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
        public string CargarConsulta(TOConsulta consulta)
        {
            string confirmacion = "Las consulta se cargó exitosamente";

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
                    confirmacion = "Error: No se puede cargar la consulta";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se puede cargar la consulta";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction("Cargar consulta");

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.CommandText = "SELECT * FROM CONSULTA WHERE ID_EXPEDIENTE = @idExpediente AND FECHA = @fecha";

                comando.Transaction = transaccion;

                comando.Parameters.AddWithValue("@idExpediente", consulta.IDExpediente);
                comando.Parameters.AddWithValue("@fecha", consulta.Fecha);

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        consulta.Hora = lector["HORA"].ToString();
                        consulta.PadecimientoActual = lector["PADECIMIENTO_ACTUAL"].ToString();
                        consulta.Analisis = lector["ANALISIS"].ToString();
                        consulta.ImpresionDiagnostica = lector["IMPRESION_DIAGNOSTICA"].ToString();
                        consulta.Plan = lector["DESCRIPCION_PLAN"].ToString();

                        string medicinaMixta = lector["MEDICINA_MIXTA_FRECUENCIA_REFERIDO_A"].ToString();

                        if (medicinaMixta != null && !medicinaMixta.Equals(""))
                        {
                            string[] divisiones = medicinaMixta.Split('|');
                            consulta.MMFrecuencia = divisiones[0];
                            consulta.MMReferidoA = divisiones[1];
                        }

                        string consultaPrivada = lector["CONSULTA_PRIVADA_ESPECIALIDAD_MOTIVO"].ToString();

                        if (consultaPrivada != null && !consultaPrivada.Equals(""))
                        {
                            string[] divisiones = consultaPrivada.Split('|');
                            consulta.CPEspecialidad = divisiones[0];
                            consulta.CPMotivo = divisiones[1];
                        }
                        consulta.Enfermedad = lector["ENFERMEDAD"].ToString();
                    }
                }

                lector.Close();
                comando.Parameters.Clear();

                comando.CommandText = "SELECT * FROM EXAMEN_FISICO WHERE ID_EXPEDIENTE = @idExpediente AND FECHA = @fecha";

                comando.Parameters.AddWithValue("@idExpediente", consulta.IDExpediente);
                comando.Parameters.AddWithValue("@fecha", consulta.Fecha);

                lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        consulta.ExamenFisico.Peso = double.Parse(lector["PESO"].ToString());
                        consulta.ExamenFisico.Talla = double.Parse(lector["TALLA"].ToString());
                        consulta.ExamenFisico.IMC = double.Parse(lector["IMC"].ToString());
                        consulta.ExamenFisico.Temperatura = double.Parse(lector["TEMPERATURA"].ToString());

                        string graficasAdicionales = lector["GRAFICAS_CRECIMIENTO_ADICIONALES"].ToString();

                        if (graficasAdicionales != null && !graficasAdicionales.Equals(""))
                        {
                            string[] graficas = graficasAdicionales.Split('|');
                            string[] adicionales = graficas[graficas.Length - 1].Split('^');

                            consulta.ExamenFisico.PC_Edad = graficas[0];
                            consulta.ExamenFisico.Peso_Edad = graficas[1];
                            consulta.ExamenFisico.Talla_Edad = graficas[2];
                            consulta.ExamenFisico.Peso_Talla = graficas[3];
                            consulta.ExamenFisico.IMC_Edad = adicionales[0];
                            consulta.ExamenFisico.PerimetroCefalico = double.Parse(adicionales[1]);
                            consulta.ExamenFisico.SO2 = double.Parse(adicionales[2]);
                        }

                        consulta.ExamenFisico.EstadoAlerta = lector["ESTADO_ALERTA"].ToString();
                        consulta.ExamenFisico.EstadoHidratacion = lector["ESTADO_HIDRATACION"].ToString();
                        consulta.ExamenFisico.RuidosCardiacos = lector["RUIDOS_CARDIACOS"].ToString();
                        consulta.ExamenFisico.CamposPulmonares = lector["CAMPOS_PULMONARES"].ToString();
                        consulta.ExamenFisico.Abdomen = lector["ABDOMEN"].ToString();
                        consulta.ExamenFisico.Faringe = lector["FARINGE"].ToString();
                        consulta.ExamenFisico.Nariz = lector["NARIZ"].ToString();
                        consulta.ExamenFisico.Oidos = lector["OIDOS"].ToString();
                        consulta.ExamenFisico.SNC = lector["SNC"].ToString();
                        consulta.ExamenFisico.Neurodesarrollo = lector["NEURODESARROLLO"].ToString();
                        consulta.ExamenFisico.SistemaOsteomuscular = lector["SISTEMA_OSTEOMUSCULAR"].ToString();
                        consulta.ExamenFisico.Piel = lector["PIEL"].ToString();
                        consulta.ExamenFisico.OtrosHallazgos = lector["OTROS_HALLAZGOS"].ToString();
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
                    confirmacion = "Error: No se puede carga la consulta";
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
        public string ActualizarConsulta(TOConsulta consulta)
        {
            string confirmacion = "La consulta se actualizó exitosamente";
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
                    confirmacion = "Error: No se pudo actualizar la consulta en el sistema";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pudo actualizar la consulta en el sistema";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction("Actualizar nueva consulta");

                // Se crea un nuevo comando con la secuencia SQL y el objeto de conexión

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.Transaction = transaccion;

                if (consulta != null)
                {
                    comando.CommandText = "UPDATE CONSULTA SET PADECIMIENTO_ACTUAL = @padecimiento, " +
                        "ANALISIS = @analisis, " +
                        "IMPRESION_DIAGNOSTICA = @impresion, " +
                        "DESCRIPCION_PLAN = @plan, " +
                        "MEDICINA_MIXTA_FRECUENCIA_REFERIDO_A = @medicinaMixta, " +
                        "CONSULTA_PRIVADA_ESPECIALIDAD_MOTIVO = @consultaPrivada, " +
                        "ENFERMEDAD = @enfermedad WHERE ID_EXPEDIENTE = @idExpediente AND FECHA = @fecha;";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@idExpediente", consulta.IDExpediente);
                    comando.Parameters.AddWithValue("@fecha", consulta.Fecha);
                    comando.Parameters.AddWithValue("@padecimiento", consulta.PadecimientoActual);
                    comando.Parameters.AddWithValue("@analisis", consulta.Analisis);
                    comando.Parameters.AddWithValue("@impresion", consulta.ImpresionDiagnostica);
                    comando.Parameters.AddWithValue("@plan", consulta.Plan);

                    // Validamos que no se lleguen a producir problemas de caracteres

                    consulta.CPEspecialidad.Replace("|", "/");
                    consulta.CPMotivo.Replace("|", "/");

                    string medicMixta = "";
                    string consultPrivada = "";

                    if (!consulta.MMFrecuencia.Equals("") && !consulta.MMReferidoA.Equals(""))
                    {
                        medicMixta = consulta.MMFrecuencia + "|" + consulta.MMReferidoA;
                    }

                    if (!consulta.CPEspecialidad.Equals("") && !consulta.CPMotivo.Equals(""))
                    {
                        consultPrivada = consulta.CPEspecialidad + "|" + consulta.CPMotivo;
                    }

                    comando.Parameters.AddWithValue("@medicinaMixta", medicMixta);
                    comando.Parameters.AddWithValue("@consultaPrivada", consultPrivada);
                    comando.Parameters.AddWithValue("@enfermedad", consulta.Enfermedad);
                    comando.ExecuteNonQuery();

                    comando.Parameters.Clear();

                    if (consulta.ExamenFisico != null)
                    {
                        comando.CommandText = "UPDATE EXAMEN_FISICO SET PESO = @peso, " +
                            "TALLA = @talla, " +
                            "IMC = @imc, " +
                            "TEMPERATURA = @temperatura, " +
                            "GRAFICAS_CRECIMIENTO_ADICIONALES = @graficasYadicionales, " +
                            "ESTADO_ALERTA = @estadoAlerta, " +
                            "ESTADO_HIDRATACION = @estadoHidratacion, " +
                            "RUIDOS_CARDIACOS = @ruidosCardiacos, " +
                            "CAMPOS_PULMONARES = @camposPulmonares, " +
                            "ABDOMEN = @abdomen, " +
                            "FARINGE = @faringe, " +
                            "NARIZ = @nariz, " +
                            "OIDOS = @oidos, " +
                            "SNC = @snc, " +
                            "NEURODESARROLLO = @neurodesarrollo, " +
                            "SISTEMA_OSTEOMUSCULAR = @sistemaOsteomuscular, " +
                            "PIEL = @piel, " +
                            "OTROS_HALLAZGOS = @otrosHallazgos WHERE ID_EXPEDIENTE = @idExpediente AND FECHA = @fecha;";

                        // Se asigna un valor a los parámetros del comando a ejecutar

                        comando.Parameters.AddWithValue("@idExpediente", consulta.IDExpediente);
                        comando.Parameters.AddWithValue("@fecha", consulta.Fecha);
                        comando.Parameters.AddWithValue("@peso", consulta.ExamenFisico.Peso);
                        comando.Parameters.AddWithValue("@talla", consulta.ExamenFisico.Talla);
                        comando.Parameters.AddWithValue("@imc", consulta.ExamenFisico.IMC);
                        comando.Parameters.AddWithValue("@temperatura", consulta.ExamenFisico.Temperatura);

                        string pcEdad = consulta.ExamenFisico.PC_Edad.Replace("|", "/").Replace("^", "'");
                        string pesoEdad = consulta.ExamenFisico.Peso_Edad.Replace("|", "/").Replace("^", "'");
                        string tallEdad = consulta.ExamenFisico.Talla_Edad.Replace("|", "/").Replace("^", "'");
                        string pesoTalla = consulta.ExamenFisico.Peso_Talla.Replace("|", "/").Replace("^", "'");
                        string imcEdad = consulta.ExamenFisico.IMC_Edad.Replace("|", "/").Replace("^", "'");

                        if (pcEdad.Equals(""))
                        {
                            pcEdad = "nulo";
                        }
                        if (pesoEdad.Equals(""))
                        {
                            pesoEdad = "nulo";
                        }
                        if (tallEdad.Equals(""))
                        {
                            tallEdad = "nulo";
                        }
                        if (pesoTalla.Equals(""))
                        {
                            pesoTalla = "nulo";
                        }
                        if (imcEdad.Equals(""))
                        {
                            imcEdad = "nulo";
                        }

                        comando.Parameters.AddWithValue("@graficasYadicionales", pcEdad + "|" +
                            pesoEdad + "|" + tallEdad + "|" + pesoTalla + "|" + imcEdad + "^" +
                            consulta.ExamenFisico.PerimetroCefalico + "^" + consulta.ExamenFisico.SO2);

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
                    confirmacion = "Error: No se pudo actualizar la consulta en el sistema";
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
        public string CargarConsultasDia(List<TOConsulta> consultas, List<TOExpediente> expedientes,
            string idMedico, string fechaActual)
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

                comando.CommandText = "SELECT E.ID_EXPEDIENTE, E.CEDULA, E.NOMBRE, E.PRIMER_APELLIDO, " +
                    "E.SEGUNDO_APELLIDO , C.HORA FROM EXPEDIENTE AS E, CONSULTA AS C, CONSULTAS_DIA AS D " +
                    "WHERE E.ID_MEDICO = @idMedico AND E.ID_EXPEDIENTE = C.ID_EXPEDIENTE AND " +
                    "C.FECHA = D.FECHA_CONSULTA AND " +
                    "E.ID_EXPEDIENTE = D.ID_EXPEDIENTE AND D.FECHA_CONSULTA = @fechaActual;";

                comando.Transaction = transaccion;

                comando.Parameters.AddWithValue("@idMedico", idMedico);
                comando.Parameters.AddWithValue("@fechaActual", fechaActual);

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        TOExpediente expediente = new TOExpediente();
                        expediente.IDExpediente = (long)lector["ID_EXPEDIENTE"];
                        expediente.Cedula = lector["CEDULA"].ToString();
                        expediente.Nombre = lector["NOMBRE"].ToString();
                        expediente.PrimerApellido = lector["PRIMER_APELLIDO"].ToString();
                        expediente.SegundoApellido = lector["SEGUNDO_APELLIDO"].ToString();
                        expedientes.Add(expediente);
                        TOConsulta consulta = new TOConsulta();
                        consulta.Hora = lector["HORA"].ToString();
                        consultas.Add(consulta);
                    }
                }

                lector.Close();

                transaccion.Commit();

            }
            catch (Exception e)
            {
                try
                {
                    string mensaje = e.Message;
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
        public string EliminarConsultasDia(string fechaActual)
        {
            string confirmacion = "Las consultas se eliminaron exitosamente";
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
                    confirmacion = "Error: No se pudieron eliminar las consultas";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pudieron eliminar las consultas";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction("Eliminar consultas");

                // Se crea un nuevo comando con la secuencia SQL y el objeto de conexión

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.Transaction = transaccion;

                if (fechaActual != null && !fechaActual.Equals(""))
                {

                    comando.CommandText = "DELETE FROM CONSULTAS_DIA WHERE FECHA_CONSULTA != @fechaActual;";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@fechaActual", fechaActual);

                    comando.ExecuteNonQuery();
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
                    confirmacion = "Error: No se pudieron eliminar las consultas";
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
