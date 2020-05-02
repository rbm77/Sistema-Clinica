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
    public class DAOExpediente
    {
        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexion);
        public string CrearExpediente(TOExpediente expediente)
        {
            string confirmacion = "El expediente se creó exitosamente";
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
                    confirmacion = "Error: No se pudo ingresar el expediente en el sistema";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pudo ingresar el expediente en el sistema";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;



            try
            {
                transaccion = conexion.BeginTransaction("Ingresar nuevo expediente");

                // Se crea un nuevo comando con la secuencia SQL y el objeto de conexión

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.Transaction = transaccion;

                long idExpedienteGuardado = 0;

                if (expediente != null)
                {
                    comando.CommandText = "INSERT INTO EXPEDIENTE (CEDULA, NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO, FECHA_NACIMIENTO," +
                    " SEXO, URL_EXPEDIENTE_ANTIGUO, CODIGO_DIRECCION, DIRECCION_EXACTA, ID_ENCARGADO, ID_DESTINATARIO_FACTURA," +
                    " ID_SOLICITANTE_CITA, FECHA_CREACION, ID_MEDICO) OUTPUT INSERTED.ID_EXPEDIENTE" + " VALUES (@cedula, @nombre, @primerApellido, @segundoApellido, @fechaNacimiento," +
                    "@sexo, @urlExpediente, @codigoDireccion, @direccionExacta, @idEncargado, @idDestinatario, @idSolicitante, @fechaCreacion," +
                    "@idMedico);";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@cedula", expediente.Cedula);
                    comando.Parameters.AddWithValue("@nombre", expediente.Nombre);
                    comando.Parameters.AddWithValue("@primerApellido", expediente.PrimerApellido);
                    comando.Parameters.AddWithValue("@segundoApellido", expediente.SegundoApellido);
                    comando.Parameters.AddWithValue("@fechaNacimiento", expediente.FechaNacimiento);
                    comando.Parameters.AddWithValue("@sexo", expediente.Sexo);
                    comando.Parameters.AddWithValue("@urlExpediente", expediente.UrlExpedienteAntiguo);
                    comando.Parameters.AddWithValue("@codigoDireccion", expediente.CodigoDireccion);
                    comando.Parameters.AddWithValue("@direccionExacta", expediente.DireccionExacta);
                    comando.Parameters.AddWithValue("@fechaCreacion", expediente.FechaCreacion);
                    comando.Parameters.AddWithValue("@idMedico", expediente.IDMedico);


                    // validaciones de los asociados al expediente


                    if (expediente.Encargado != null)
                    {
                        comando.Parameters.AddWithValue("@idEncargado", expediente.Encargado.Cedula);

                        if (expediente.DestinatarioFactura == null)
                        {
                            comando.Parameters.AddWithValue("@idDestinatario", expediente.Encargado.Cedula);
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("@idDestinatario", expediente.DestinatarioFactura.Cedula);
                        }
                        if (expediente.SolicitanteCita == null)
                        {
                            comando.Parameters.AddWithValue("@idSolicitante", expediente.Encargado.Cedula);
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("@idSolicitante", expediente.SolicitanteCita.Cedula);
                        }
                    }
                    else
                    {
                        comando.Parameters.AddWithValue("@idEncargado", "");

                        if (expediente.DestinatarioFactura == null)
                        {
                            comando.Parameters.AddWithValue("@idDestinatario", "");
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("@idDestinatario", expediente.DestinatarioFactura.Cedula);
                        }
                        if (expediente.SolicitanteCita == null)
                        {
                            comando.Parameters.AddWithValue("@idSolicitante", "");
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("@idSolicitante", expediente.SolicitanteCita.Cedula);
                        }
                    }
                    

                    // Se ejecuta el comando y se realiza un commit de la transacción

                    idExpedienteGuardado = (long) comando.ExecuteScalar();

                    confirmacion += "*" + idExpedienteGuardado;

                    comando.Parameters.Clear();
                }

                if (expediente.Encargado != null && idExpedienteGuardado != 0)
                {
                    comando.CommandText = "IF NOT EXISTS(SELECT CEDULA FROM ENCARGADO WHERE CEDULA = @cedBuscar) " +
                        "BEGIN" +
                        " INSERT INTO ENCARGADO (CEDULA, NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO," +
                        " TELEFONO, CORREO, PARENTESCO, CODIGO_DIRECCION, DIRECCION_EXACTA) VALUES (@cedula, @nombre," +
                        " @primerApellido, @segundoApellido," +
                        " @telefono, @correo, @parentesco, @codigoDireccion, @direccionExacta)" +
                        "END;";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@cedBuscar", expediente.Encargado.Cedula);
                    comando.Parameters.AddWithValue("@cedula", expediente.Encargado.Cedula);
                    comando.Parameters.AddWithValue("@nombre", expediente.Encargado.Nombre);
                    comando.Parameters.AddWithValue("@primerApellido", expediente.Encargado.PrimerApellido);
                    comando.Parameters.AddWithValue("@segundoApellido", expediente.Encargado.SegundoApellido);
                    comando.Parameters.AddWithValue("@telefono", expediente.Encargado.Telefono);
                    comando.Parameters.AddWithValue("@correo", expediente.Encargado.Correo);
                    comando.Parameters.AddWithValue("@parentesco", expediente.Encargado.Parentesco);
                    comando.Parameters.AddWithValue("@codigoDireccion", expediente.Encargado.CodigoDireccion);
                    comando.Parameters.AddWithValue("@direccionExacta", expediente.Encargado.DireccionExacta);

                    // Se ejecuta el comando y se realiza un commit de la transacción

                    comando.ExecuteNonQuery();

                    comando.Parameters.Clear();
                }

                if (expediente.DestinatarioFactura != null && idExpedienteGuardado != 0)
                {
                    comando.CommandText = "IF NOT EXISTS(SELECT CEDULA FROM DESTINATARIO_FACTURA WHERE CEDULA = @cedBuscar) " +
                        "BEGIN" +
                        " INSERT INTO DESTINATARIO_FACTURA (CEDULA, NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO," +
                        " TELEFONO, CORREO, CODIGO_DIRECCION, DIRECCION_EXACTA) VALUES (@cedula, @nombre," +
                        " @primerApellido, @segundoApellido," +
                        " @telefono, @correo, @codigoDireccion, @direccionExacta)" +
                        "END;";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@cedBuscar", expediente.DestinatarioFactura.Cedula);
                    comando.Parameters.AddWithValue("@cedula", expediente.DestinatarioFactura.Cedula);
                    comando.Parameters.AddWithValue("@nombre", expediente.DestinatarioFactura.Nombre);
                    comando.Parameters.AddWithValue("@primerApellido", expediente.DestinatarioFactura.PrimerApellido);
                    comando.Parameters.AddWithValue("@segundoApellido", expediente.DestinatarioFactura.SegundoApellido);
                    comando.Parameters.AddWithValue("@telefono", expediente.DestinatarioFactura.Telefono);
                    comando.Parameters.AddWithValue("@correo", expediente.DestinatarioFactura.Correo);
                    comando.Parameters.AddWithValue("@codigoDireccion", expediente.DestinatarioFactura.CodigoDireccion);
                    comando.Parameters.AddWithValue("@direccionExacta", expediente.DestinatarioFactura.DireccionExacta);

                    // Se ejecuta el comando y se realiza un commit de la transacción

                    comando.ExecuteNonQuery();

                    comando.Parameters.Clear();
                }

                if (expediente.SolicitanteCita != null && idExpedienteGuardado != 0)
                {

                    comando.CommandText = "IF NOT EXISTS(SELECT CEDULA FROM SOLICITANTE_CITA WHERE CEDULA = @cedulaBuscar) " +
                        "BEGIN" +
                        " INSERT INTO SOLICITANTE_CITA(CEDULA, CORREO, CONTRASENNA, TELEFONO)" +
                        " VALUES(@cedula, @correo, @contrasenna, @telefono,) " +
                        "END;";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@cedulaBuscar", expediente.SolicitanteCita.Cedula);
                    comando.Parameters.AddWithValue("@cedula", expediente.SolicitanteCita.Cedula);
                    comando.Parameters.AddWithValue("@correo", expediente.SolicitanteCita.Correo);
                    comando.Parameters.AddWithValue("@contrasenna", expediente.SolicitanteCita.Contrasenna);
                    comando.Parameters.AddWithValue("@telefono", expediente.SolicitanteCita.Telefono);
                    // Se ejecuta el comando y se realiza un commit de la transacción

                    comando.ExecuteNonQuery();

                    comando.Parameters.Clear();
                }

                if(expediente.HistoriaClinica != null && idExpedienteGuardado != 0)
                {
                    comando.CommandText = "INSERT INTO HISTORIA_CLINICA (ID_EXPEDIENTE, PERINATALES, PATOLOGICOS, QUIRURGICOS," +
                        " TRAUMATICOS, HEREDO_FAMILIARES, ALERGIAS, VACUNAS) VALUES (@idExpediente, @perinatales, @patologicos, @quirurgicos," +
                        " @traumaticos, @heredoFamiliares, @alergias, @vacunas);";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@idExpediente", idExpedienteGuardado);
                    comando.Parameters.AddWithValue("@perinatales", expediente.HistoriaClinica.Perinatales);
                    comando.Parameters.AddWithValue("@patologicos", expediente.HistoriaClinica.Patologicos);
                    comando.Parameters.AddWithValue("@quirurgicos", expediente.HistoriaClinica.Quirurgicos);
                    comando.Parameters.AddWithValue("@traumaticos", expediente.HistoriaClinica.Traumaticos);
                    comando.Parameters.AddWithValue("@heredoFamiliares", expediente.HistoriaClinica.HeredoFamiliares);
                    comando.Parameters.AddWithValue("@alergias", expediente.HistoriaClinica.Alergias);
                    comando.Parameters.AddWithValue("@vacunas", expediente.HistoriaClinica.Vacunas);

                    // Se ejecuta el comando y se realiza un commit de la transacción

                    comando.ExecuteNonQuery();

                    comando.Parameters.Clear();
                }


                if (expediente.HistoriaClinica.DatosNacimiento != null && idExpedienteGuardado != 0)
                {
                    comando.CommandText = "INSERT INTO DATOS_NACIMIENTO (ID_EXPEDIENTE, TALLA_NACIMIENTO, PESO_NACIMIENTO, PERIMETRO_CEFALICO," +
                    " APGAR, EDAD_GESTACIONAL, CLASIFICACION_UNIVERSAL) VALUES (@idExpediente, @tallaNacimiento, @pesoNacimiento, @perimetroCefalico," +
                    " @apgar, @edadGestacional, @clasificacionUniversal);";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@idExpediente", idExpedienteGuardado);
                    comando.Parameters.AddWithValue("@tallaNacimiento", expediente.HistoriaClinica.DatosNacimiento.TallaNacimiento);
                    comando.Parameters.AddWithValue("@pesoNacimiento", expediente.HistoriaClinica.DatosNacimiento.PesoNacimiento);
                    comando.Parameters.AddWithValue("@perimetroCefalico", expediente.HistoriaClinica.DatosNacimiento.PerimetroCefalico);
                    comando.Parameters.AddWithValue("@apgar", expediente.HistoriaClinica.DatosNacimiento.Apgar);
                    comando.Parameters.AddWithValue("@edadGestacional", expediente.HistoriaClinica.DatosNacimiento.EdadGestacional);
                    comando.Parameters.AddWithValue("@clasificacionUniversal", expediente.HistoriaClinica.DatosNacimiento.ClasificacionUniversal);

                    // Se ejecuta el comando y se realiza un commit de la transacción

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
                    confirmacion = "Error: No se pudo ingresar el expediente en el sistema";
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

        public string CargarExpediente(TOExpediente expediente)
        {
            string confirmacion = "El expediente se cargó exitosamente";

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
                    confirmacion = "Error: No se pueden cargar los datos del expediente";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pueden cargar los datos del expediente";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction("Cargar datos de expediente");

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.CommandText = "SELECT * FROM EXPEDIENTE WHERE ID_EXPEDIENTE = @idExpediente";

                comando.Transaction = transaccion;

                comando.Parameters.AddWithValue("@idExpediente", expediente.IDExpediente);

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        expediente.Cedula = lector["CEDULA"].ToString();
                        expediente.Nombre = lector["NOMBRE"].ToString();
                        expediente.PrimerApellido = lector["PRIMER_APELLIDO"].ToString();
                        expediente.SegundoApellido = lector["SEGUNDO_APELLIDO"].ToString();
                        expediente.FechaNacimiento = lector["FECHA_NACIMIENTO"].ToString();
                        expediente.Sexo = lector["SEXO"].ToString();
                        expediente.UrlExpedienteAntiguo = lector["URL_EXPEDIENTE_ANTIGUO"].ToString();
                        expediente.CodigoDireccion = lector["CODIGO_DIRECCION"].ToString();
                        expediente.DireccionExacta = lector["DIRECCION_EXACTA"].ToString();
                        expediente.Encargado.Cedula = lector["ID_ENCARGADO"].ToString();
                        expediente.DestinatarioFactura.Cedula = lector["ID_DESTINATARIO_FACTURA"].ToString();
                        expediente.SolicitanteCita.Cedula = lector["ID_SOLICITANTE_CITA"].ToString();
                        expediente.FechaCreacion = lector["FECHA_CREACION"].ToString();
                        expediente.IDMedico = lector["ID_MEDICO"].ToString();
                    }
                }
                else
                {
                    expediente.IDExpediente = 0;
                }

                lector.Close();

                comando.Parameters.Clear();

                if (expediente.IDExpediente != 0)
                {

                    if (!expediente.Encargado.Cedula.Equals(""))
                    {
                        comando.CommandText = "SELECT * FROM ENCARGADO WHERE CEDULA = @cedula";
                        comando.Parameters.AddWithValue("@cedula", expediente.Encargado.Cedula);

                        lector = comando.ExecuteReader();

                        if (lector.HasRows)
                        {
                            while (lector.Read())
                            {
                                expediente.Encargado.Cedula = lector["CEDULA"].ToString();
                                expediente.Encargado.Nombre = lector["NOMBRE"].ToString();
                                expediente.Encargado.PrimerApellido = lector["PRIMER_APELLIDO"].ToString();
                                expediente.Encargado.SegundoApellido = lector["SEGUNDO_APELLIDO"].ToString();
                                expediente.Encargado.Telefono = lector["TELEFONO"].ToString();
                                expediente.Encargado.Correo = lector["CORREO"].ToString();
                                expediente.Encargado.Parentesco = lector["PARENTESCO"].ToString();
                                expediente.Encargado.CodigoDireccion = lector["CODIGO_DIRECCION"].ToString();
                                expediente.Encargado.DireccionExacta = lector["DIRECCION_EXACTA"].ToString();
                            }
                        }

                        lector.Close();
                        comando.Parameters.Clear();
                    }

                    if ((!expediente.Encargado.Cedula.Equals(expediente.DestinatarioFactura.Cedula)) &&
                        (!expediente.DestinatarioFactura.Cedula.Equals("")))
                    {
                        comando.CommandText = "SELECT * FROM DESTINATARIO_FACTURA WHERE CEDULA = @cedula";
                        comando.Parameters.AddWithValue("@cedula", expediente.DestinatarioFactura.Cedula);

                        lector = comando.ExecuteReader();

                        if (lector.HasRows)
                        {
                            while (lector.Read())
                            {
                                expediente.DestinatarioFactura.Cedula = lector["CEDULA"].ToString();
                                expediente.DestinatarioFactura.Nombre = lector["NOMBRE"].ToString();
                                expediente.DestinatarioFactura.PrimerApellido = lector["PRIMER_APELLIDO"].ToString();
                                expediente.DestinatarioFactura.SegundoApellido = lector["SEGUNDO_APELLIDO"].ToString();
                                expediente.DestinatarioFactura.Telefono = lector["TELEFONO"].ToString();
                                expediente.DestinatarioFactura.Correo = lector["CORREO"].ToString();
                                expediente.DestinatarioFactura.CodigoDireccion = lector["CODIGO_DIRECCION"].ToString();
                                expediente.DestinatarioFactura.DireccionExacta = lector["DIRECCION_EXACTA"].ToString();
                            }
                        }

                        lector.Close();
                        comando.Parameters.Clear();
                    }

                    if ((!expediente.Encargado.Cedula.Equals(expediente.SolicitanteCita.Cedula)) &&
                        (!expediente.SolicitanteCita.Cedula.Equals("")))
                    {
                        comando.CommandText = "SELECT * FROM SOLICITANTE_CITA WHERE CEDULA = @cedula";
                        comando.Parameters.AddWithValue("@cedula", expediente.SolicitanteCita.Cedula);

                        lector = comando.ExecuteReader();

                        if (lector.HasRows)
                        {
                            while (lector.Read())
                            {
                                expediente.SolicitanteCita.Cedula = lector["CEDULA"].ToString();
                                expediente.SolicitanteCita.Telefono = lector["TELEFONO"].ToString();
                                expediente.SolicitanteCita.Correo = lector["CORREO"].ToString();
                            }
                        }

                        lector.Close();
                        comando.Parameters.Clear();
                    }


                    comando.CommandText = "SELECT * FROM HISTORIA_CLINICA WHERE ID_EXPEDIENTE = @idExpediente";
                    comando.Parameters.AddWithValue("@idExpediente", expediente.IDExpediente);

                    lector = comando.ExecuteReader();

                    if (lector.HasRows)
                    {
                        while (lector.Read())
                        {
                            expediente.HistoriaClinica.Perinatales = lector["PERINATALES"].ToString();
                            expediente.HistoriaClinica.Patologicos = lector["PATOLOGICOS"].ToString();
                            expediente.HistoriaClinica.Quirurgicos = lector["QUIRURGICOS"].ToString();
                            expediente.HistoriaClinica.Traumaticos = lector["TRAUMATICOS"].ToString();
                            expediente.HistoriaClinica.HeredoFamiliares = lector["HEREDO_FAMILIARES"].ToString();
                            expediente.HistoriaClinica.Alergias = lector["ALERGIAS"].ToString();
                            expediente.HistoriaClinica.Vacunas = lector["VACUNAS"].ToString();
                        }
                    }

                    lector.Close();
                    comando.Parameters.Clear();

                    comando.CommandText = "SELECT * FROM DATOS_NACIMIENTO WHERE ID_EXPEDIENTE = @idExpediente";
                    comando.Parameters.AddWithValue("@idExpediente", expediente.IDExpediente);

                    lector = comando.ExecuteReader();

                    if (lector.HasRows)
                    {
                        while (lector.Read())
                        {
                            expediente.HistoriaClinica.DatosNacimiento.TallaNacimiento = (double) lector["TALLA_NACIMIENTO"];
                            expediente.HistoriaClinica.DatosNacimiento.PesoNacimiento = (double) lector["PESO_NACIMIENTO"];
                            expediente.HistoriaClinica.DatosNacimiento.PerimetroCefalico =  (double) lector["PERIMETRO_CEFALICO"];
                            expediente.HistoriaClinica.DatosNacimiento.Apgar = (int) lector["APGAR"];
                            expediente.HistoriaClinica.DatosNacimiento.EdadGestacional = (double) lector["EDAD_GESTACIONAL"];
                            expediente.HistoriaClinica.DatosNacimiento.ClasificacionUniversal = lector["CLASIFICACION_UNIVERSAL"].ToString();
                        }
                    }

                    lector.Close();
                    comando.Parameters.Clear();

                }
                else
                {
                    confirmacion = "Error: El expediente no existe";
                }

                transaccion.Commit();

            }
            catch (Exception e)
            {
                try
                {
                    string error = e.Message;
                    // En caso de un error se realiza un rollback a la transacción

                    transaccion.Rollback();
                }
                catch (Exception)
                {
                }
                finally
                {
                    confirmacion = "Error: No se pueden cargar los datos del expediente";
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

        public string CargarExpedientes(List<TOExpediente> expedientes, string idMedico)
        {
            string confirmacion = "Los expedientes se cargaron exitosamente";

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
                    confirmacion = "Error: No se pueden cargar los expedientes";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pueden cargar los expedientes";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction("Cargar expedientes");

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.CommandText = "SELECT ID_EXPEDIENTE, CEDULA, NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO" +
                    " FROM EXPEDIENTE WHERE ID_MEDICO = @idMedico";

                comando.Transaction = transaccion;

                comando.Parameters.AddWithValue("@idMedico", idMedico);

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        TOExpediente expediente = new TOExpediente();
                        expediente.IDExpediente = (long) lector["ID_EXPEDIENTE"];
                        expediente.Cedula = lector["CEDULA"].ToString();
                        expediente.Nombre = lector["NOMBRE"].ToString();
                        expediente.PrimerApellido = lector["PRIMER_APELLIDO"].ToString();
                        expediente.SegundoApellido = lector["SEGUNDO_APELLIDO"].ToString();

                        expedientes.Add(expediente);
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
                    confirmacion = "Error: No se pueden cargar los expedientes";
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
