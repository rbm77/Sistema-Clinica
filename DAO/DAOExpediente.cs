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
                        " VALUES(@cedula, @correo, @contrasenna, @telefono) " +
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
                    TODatosNacimiento dn = expediente.HistoriaClinica.DatosNacimiento;

                    if (dn.ClasificacionUniversal == null)
                    {
                        dn.ClasificacionUniversal = "";
                    }

                    if (dn.TallaNacimiento != 0.0 || dn.PesoNacimiento != 0.0 || dn.PerimetroCefalico != 0.0 ||
                        dn.Apgar != 0 || dn.EdadGestacional != 0.0 || !dn.ClasificacionUniversal.Equals(""))
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

                comando.CommandText = "SELECT * FROM EXPEDIENTE WHERE ID_EXPEDIENTE = @idExpediente;";

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
                        comando.CommandText = "SELECT * FROM ENCARGADO WHERE CEDULA = @cedula;";
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
                        comando.CommandText = "SELECT * FROM DESTINATARIO_FACTURA WHERE CEDULA = @cedula;";
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
                        comando.CommandText = "SELECT * FROM SOLICITANTE_CITA WHERE CEDULA = @cedula;";
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


                    comando.CommandText = "SELECT * FROM HISTORIA_CLINICA WHERE ID_EXPEDIENTE = @idExpediente;";
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

                    comando.CommandText = "SELECT * FROM DATOS_NACIMIENTO WHERE ID_EXPEDIENTE = @idExpediente;";
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
                    " FROM EXPEDIENTE WHERE ID_MEDICO = @idMedico;";

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

        public string ActualizarExpediente(TOExpediente expediente)
        {
            string confirmacion = "El expediente se actualizó exitosamente";
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
                    confirmacion = "Error: No se pudo actualizar el expediente en el sistema";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pudo actualizar el expediente en el sistema";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;



            try
            {
                transaccion = conexion.BeginTransaction("Actualizar un expediente");

                // Se crea un nuevo comando con la secuencia SQL y el objeto de conexión

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.Transaction = transaccion;

                if (expediente != null)
                {
                    comando.CommandText = "UPDATE EXPEDIENTE SET " +
                        "CEDULA = @cedula, " +
                        "NOMBRE = @nombre, " +
                        "PRIMER_APELLIDO = @primerApellido, " +
                        "SEGUNDO_APELLIDO = @segundoApellido, " +
                        "FECHA_NACIMIENTO = @fechaNacimiento, " +
                        "SEXO = @sexo, " +
                        "URL_EXPEDIENTE_ANTIGUO = @urlExpediente, " +
                        "CODIGO_DIRECCION = @codigoDireccion, " +
                        "DIRECCION_EXACTA = @direccionExacta, " +
                        "ID_ENCARGADO = @idEncargado, " +
                        "ID_DESTINATARIO_FACTURA = @idDestinatario, " +
                        "ID_SOLICITANTE_CITA = @idSolicitante, " +
                        "FECHA_CREACION = @fechaCreacion, " +
                        "ID_MEDICO = @idMedico WHERE ID_EXPEDIENTE = @idExpediente;";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@idExpediente", expediente.IDExpediente);
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

                    comando.ExecuteNonQuery();

                    comando.Parameters.Clear();
                }

                if (expediente.Encargado != null)
                {
                    comando.CommandText = "IF NOT EXISTS(SELECT CEDULA FROM ENCARGADO WHERE CEDULA = @cedBuscar) " +
                            "BEGIN" +
                            " INSERT INTO ENCARGADO (CEDULA, NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO," +
                            " TELEFONO, CORREO, PARENTESCO, CODIGO_DIRECCION, DIRECCION_EXACTA) VALUES (@cedula, @nombre," +
                            " @primerApellido, @segundoApellido," +
                            " @telefono, @correo, @parentesco, @codigoDireccion, @direccionExacta) " +
                            "END " +
                        "ELSE" +
                        " BEGIN " + 
                            "UPDATE ENCARGADO SET " +
                            "NOMBRE = @nombre, " +
                            "PRIMER_APELLIDO = @primerApellido, " +
                            "SEGUNDO_APELLIDO = @segundoApellido, " +
                            "TELEFONO = @telefono, " +
                            "CORREO = @correo, " +
                            "PARENTESCO = @parentesco, " +
                            "CODIGO_DIRECCION = @codigoDireccion, " +
                            "DIRECCION_EXACTA = @direccionExacta WHERE CEDULA = @cedula" +
                        " END;";

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

                if (expediente.DestinatarioFactura != null)
                {
                    comando.CommandText = "IF NOT EXISTS(SELECT CEDULA FROM DESTINATARIO_FACTURA WHERE CEDULA = @cedBuscar) " +
                            "BEGIN" +
                            " INSERT INTO DESTINATARIO_FACTURA (CEDULA, NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO," +
                            " TELEFONO, CORREO, CODIGO_DIRECCION, DIRECCION_EXACTA) VALUES (@cedula, @nombre," +
                            " @primerApellido, @segundoApellido," +
                            " @telefono, @correo, @codigoDireccion, @direccionExacta) " +
                            "END " +
                        "ELSE" +
                        " BEGIN " +
                            "UPDATE DESTINATARIO_FACTURA SET " +
                            "NOMBRE = @nombre, " +
                            "PRIMER_APELLIDO = @primerApellido, " +
                            "SEGUNDO_APELLIDO = @segundoApellido, " +
                            "TELEFONO = @telefono, " +
                            "CORREO = @correo, " +
                            "CODIGO_DIRECCION = @codigoDireccion, " +
                            "DIRECCION_EXACTA = @direccionExacta WHERE CEDULA = @cedula" +
                        " END;";

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

                if (expediente.SolicitanteCita != null)
                {
                    comando.CommandText = "IF NOT EXISTS(SELECT CEDULA FROM SOLICITANTE_CITA WHERE CEDULA = @cedulaBuscar) " +
                           "BEGIN" +
                           " INSERT INTO SOLICITANTE_CITA (CEDULA, CORREO, CONTRASENNA, TELEFONO) VALUES (@cedula, @correo," +
                           " @contrasenna, @telefono) " +
                           "END " +
                       "ELSE" +
                       " BEGIN " +
                           "UPDATE SOLICITANTE_CITA SET " +
                           "CORREO = @correo, " +
                           "TELEFONO = @telefono WHERE CEDULA = @cedula" +
                       " END;";

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

                if (expediente.HistoriaClinica != null)
                {
                    comando.CommandText = "UPDATE HISTORIA_CLINICA SET " +
                        "PERINATALES = @perinatales, " +
                        "PATOLOGICOS = @patologicos, " +
                        "QUIRURGICOS = @quirurgicos, " +
                        "TRAUMATICOS = @traumaticos, " +
                        "HEREDO_FAMILIARES = @heredoFamiliares, " +
                        "ALERGIAS = @alergias, " +
                        "VACUNAS = @vacunas WHERE ID_EXPEDIENTE = @idExpediente;";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@idExpediente", expediente.IDExpediente);
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


                if (expediente.HistoriaClinica.DatosNacimiento != null)
                {
                    TODatosNacimiento dn = expediente.HistoriaClinica.DatosNacimiento;

                    if (dn.ClasificacionUniversal == null)
                    {
                        dn.ClasificacionUniversal = "";
                    }

                    if (dn.TallaNacimiento != 0.0 || dn.PesoNacimiento != 0.0 || dn.PerimetroCefalico != 0.0 ||
                        dn.Apgar != 0 || dn.EdadGestacional != 0.0 || !dn.ClasificacionUniversal.Equals(""))
                    {
                        // NO ESTA VACIO

                        comando.CommandText = "IF EXISTS(SELECT ID_EXPEDIENTE FROM DATOS_NACIMIENTO WHERE ID_EXPEDIENTE = @idExpediente) " +
                               "BEGIN " +
                                "UPDATE DATOS_NACIMIENTO SET " +
                                "TALLA_NACIMIENTO = @tallaNacimiento, " +
                                "PESO_NACIMIENTO = @pesoNacimiento, " +
                                "PERIMETRO_CEFALICO = @perimetroCefalico, " +
                                "APGAR = @apgar, " +
                                "EDAD_GESTACIONAL = @edadGestacional, " +
                                "CLASIFICACION_UNIVERSAL = @clasificacionUniversal WHERE ID_EXPEDIENTE = @idExpediente " +
                               "END " +
                               "ELSE " +
                               "BEGIN " +
                               "INSERT INTO DATOS_NACIMIENTO(ID_EXPEDIENTE, TALLA_NACIMIENTO, PESO_NACIMIENTO, PERIMETRO_CEFALICO," +
                                " APGAR, EDAD_GESTACIONAL, CLASIFICACION_UNIVERSAL) VALUES (@idExpediente, @tallaNacimiento, @pesoNacimiento, @perimetroCefalico," +
                                " @apgar, @edadGestacional, @clasificacionUniversal) " +
                               "END";
                    }
                    else
                    {
                        // SI ESTA VACIO

                        comando.CommandText = "IF EXISTS(SELECT ID_EXPEDIENTE FROM DATOS_NACIMIENTO WHERE ID_EXPEDIENTE = @idExpediente) " +
                           "BEGIN " +
                            "UPDATE DATOS_NACIMIENTO SET " +
                            "TALLA_NACIMIENTO = @tallaNacimiento, " +
                            "PESO_NACIMIENTO = @pesoNacimiento, " +
                            "PERIMETRO_CEFALICO = @perimetroCefalico, " +
                            "APGAR = @apgar, " +
                            "EDAD_GESTACIONAL = @edadGestacional, " +
                            "CLASIFICACION_UNIVERSAL = @clasificacionUniversal WHERE ID_EXPEDIENTE = @idExpediente " +
                           "END ";
                    }

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@idExpediente", expediente.IDExpediente);
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
                    confirmacion = "Error: No se pudo actualizar el expediente en el sistema";
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
