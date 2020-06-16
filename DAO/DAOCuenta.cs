using System;
using System.Collections.Generic;
using TO;
using System.Data.SqlClient;
using System.Data;

namespace DAO
{
    public class DAOCuenta
    {
        // Se inicializa el objeto que contiene la conexion a base de datos  

        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexion);

        // Se crea una cuenta de usuario en base de datos
        public string CrearCuenta(TOCuenta toCuenta, TOUsuario toUsuario, TOMedico toMedico)
        {
            string confirmacion = "La cuenta se creó exitosamente";
            string mensajeError = "Error: No se pudo ingresar la cuenta en el sistema";

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
                    confirmacion = mensajeError;
                    return confirmacion;
                }
            }
            else
            {
                confirmacion = mensajeError;
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction("Ingresar nueva cuenta");

                // Se crea un nuevo comando con la secuencia SQL y el objeto de conexión

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;
                comando.Transaction = transaccion;

                comando.CommandText = "INSERT INTO CUENTA_USUARIO (ID_CUENTA, CORREO, CONTRASENNA, ROL, ESTADO)" +
                    "VALUES (@id, @correo, @contrasenna, @rol, @estado);";

                // Se asigna un valor a los parámetros del comando a ejecutar

                comando.Parameters.AddWithValue("@id", toCuenta.IdCuenta);
                comando.Parameters.AddWithValue("@correo", toCuenta.Correo);
                comando.Parameters.AddWithValue("@contrasenna", toCuenta.Contrasenna);
                comando.Parameters.AddWithValue("@rol", toCuenta.Rol);
                comando.Parameters.AddWithValue("@estado", toCuenta.Estado);

                // Se ejecuta el comando y se limpian los parámetros

                comando.ExecuteNonQuery();
                comando.Parameters.Clear();

                // Se modifica la consulta del comando de la transacción

                comando.CommandText = "INSERT INTO USUARIO (CEDULA, NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO," +
                    " TELEFONO, CODIGO_ASISTENTE) VALUES (@cedula, @nombre, @primer, @segundo, @telefono, @codigoAsistente);";

                // Se asigna un valor a los parámetros del comando a ejecutar

                comando.Parameters.AddWithValue("@cedula", toUsuario.Cedula);
                comando.Parameters.AddWithValue("@nombre", toUsuario.Nombre);
                comando.Parameters.AddWithValue("@primer", toUsuario.PrimerApellido);
                comando.Parameters.AddWithValue("@segundo", toUsuario.SegundoApellido);
                comando.Parameters.AddWithValue("@telefono", toUsuario.Telefono);
                comando.Parameters.AddWithValue("@codigoAsistente", toUsuario.CodigoAsistente);

                // Se ejecuta el comando

                comando.ExecuteNonQuery();
                comando.Parameters.Clear();

                // En caso de que el rol de la cuennta se un médico, se procede a guardar en base de datos

                if (toCuenta.Rol.Equals("medico") && toMedico != null)
                {
                    comando.CommandText = "INSERT INTO MEDICO (ID_MEDICO, CODIGO_MEDICO, ESPECIALIDAD, DURACION_CITA) " +
                        "VALUES (@id, @codigo, @especialidad, @duracion);";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@id", toMedico.IdMedico);
                    comando.Parameters.AddWithValue("@codigo", toMedico.CodigoMedico);
                    comando.Parameters.AddWithValue("@especialidad", toMedico.Especialidad);
                    comando.Parameters.AddWithValue("@duracion", toMedico.DuracionCita);

                    // Se ejecuta el comando y se realiza un commit de la transacción

                    comando.ExecuteNonQuery();
                }

                // Una vez que se ejecutaron todos los comandos se realiza el commit de la transacción
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
                    confirmacion = mensajeError;
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

        // Se obtiene los códigos de los médicos de la base de datos
        public string CargarCodigosMedicos(List<string> codigos)
        {
            string confirmacion = "Los códigos de los médicos se cargaron exitosamente";
            string mensajeError = "Error: No se pudieron cargar los códigos de los médicos";

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
                    confirmacion = mensajeError;
                    return confirmacion;
                }
            }
            else
            {
                confirmacion = mensajeError;
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction("Cargar códigos de médicos");

                // Se crea un nuevo comando con la secuencia SQL y el objeto de conexión

                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;
                comando.Transaction = transaccion;

                comando.CommandText = "SELECT CODIGO_MEDICO FROM MEDICO";

                // Se ejecuta el comando y se realiza un commit de la transacción

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        codigos.Add(lector["CODIGO_MEDICO"].ToString());
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
                    confirmacion = mensajeError;
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

        public string CargarUsuario(TOCuenta cuenta, TOUsuario usuario, TOMedico medico)
        {
            string confirmacion = "El usuario se cargó exitosamente";

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
                    confirmacion = "Error: No se puede cargar los datos de usuario";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se puede cargar los datos de usuario";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction("Cargar datos de usuario");

                // Se crea un nuevo comando con la secuencia SQL y el objeto de conexión

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.CommandText = "SELECT CORREO, ROL, ESTADO, CONTRASENNA FROM CUENTA_USUARIO WHERE ID_CUENTA = @idCuenta";


                comando.Transaction = transaccion;


                // Se ejecuta el comando y se realiza un commit de la transacción

                comando.Parameters.AddWithValue("@idCuenta", cuenta.IdCuenta);

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        cuenta.Correo = lector["CORREO"].ToString();
                        cuenta.Rol = lector["ROL"].ToString();
                        cuenta.Estado = lector["ESTADO"].ToString();
                        cuenta.Contrasenna = lector["CONTRASENNA"].ToString();
                    }
                }
                else
                {
                    cuenta.IdCuenta = "";
                }

                lector.Close();

                comando.Parameters.Clear();


                if (!cuenta.IdCuenta.Equals(""))
                {

                    comando.CommandText = "SELECT * FROM USUARIO WHERE CEDULA = @cedula";


                    comando.Parameters.AddWithValue("@cedula", cuenta.IdCuenta);

                    lector = comando.ExecuteReader();

                    if (lector.HasRows)
                    {
                        while (lector.Read())
                        {
                            usuario.Cedula = lector["CEDULA"].ToString();
                            usuario.Nombre = lector["NOMBRE"].ToString();
                            usuario.PrimerApellido = lector["PRIMER_APELLIDO"].ToString();
                            usuario.SegundoApellido = lector["SEGUNDO_APELLIDO"].ToString();
                            usuario.Telefono = lector["TELEFONO"].ToString();
                            usuario.CodigoAsistente = lector["CODIGO_ASISTENTE"].ToString();
                        }
                    }

                    lector.Close();

                    comando.Parameters.Clear();

                    if (cuenta != null)
                    {
                        if (cuenta.Rol.Equals("medico"))
                        {
                            comando.CommandText = "SELECT * FROM MEDICO WHERE ID_MEDICO = @idMedico";

                            comando.Parameters.AddWithValue("@idMedico", cuenta.IdCuenta);

                            lector = comando.ExecuteReader();

                            if (lector.HasRows)
                            {
                                while (lector.Read())
                                {
                                    medico.IdMedico = lector["ID_MEDICO"].ToString();
                                    medico.CodigoMedico = lector["CODIGO_MEDICO"].ToString();
                                    medico.Especialidad = lector["ESPECIALIDAD"].ToString();
                                    medico.DuracionCita = lector["DURACION_CITA"].ToString();
                                }
                            }

                            lector.Close();
                        }
                    }


                }
                else
                {
                    confirmacion = "Error: El usuario no existe";
                }

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
                    confirmacion = "Error: No se puede cargar los datos de usuario";
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

        public string CargarUsuarios(List<TOCuenta> cuentas, List<TOUsuario> usuarios)
        {
            string confirmacion = "Las cuentas de usuario se cargaron exitosamente";

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
                    confirmacion = "Error: No se pueden cargar las cuentas de usuario";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pueden cargar las cuentas de usuario";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction("Cargar cuentas de usuario");

                // Se crea un nuevo comando con la secuencia SQL y el objeto de conexión

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.CommandText = "SELECT ID_CUENTA, CORREO, ESTADO FROM CUENTA_USUARIO";


                comando.Transaction = transaccion;


                // Se ejecuta el comando y se realiza un commit de la transacción


                SqlDataReader lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        TOCuenta temp = new TOCuenta();
                        temp.IdCuenta = lector["ID_CUENTA"].ToString();
                        temp.Correo = lector["CORREO"].ToString();
                        temp.Estado = lector["ESTADO"].ToString();
                        cuentas.Add(temp);
                    }
                }

                lector.Close();


                comando.CommandText = "SELECT CEDULA, NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO, TELEFONO FROM USUARIO";

                lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        TOUsuario temp = new TOUsuario(lector["CEDULA"].ToString(), lector["NOMBRE"].ToString(),
                            lector["PRIMER_APELLIDO"].ToString(), lector["SEGUNDO_APELLIDO"].ToString(),
                            lector["TELEFONO"].ToString(), "");
                        usuarios.Add(temp);
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
                    confirmacion = "Error: No se pueden cargar las cuentas de usuario";
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


        public string IniciarSesion(TOCuenta cuenta, TOUsuario usuario)
        {
            string confirmacion = "La sesión se ha iniciado correctamente.";

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
                    confirmacion = "Error: No se pudo iniciar la sesión de usuario";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pudo iniciar la sesión de usuario";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction("Iniciar sesión de usuario");

                // Se crea un nuevo comando con la secuencia SQL y el objeto de conexión

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.CommandText = "SELECT ROL FROM CUENTA_USUARIO WHERE ID_CUENTA = @idCuenta AND CONTRASENNA = @contrasenna AND ESTADO = 'activa'";


                comando.Transaction = transaccion;


                // Se ejecuta el comando y se realiza un commit de la transacción

                comando.Parameters.AddWithValue("@idCuenta", cuenta.IdCuenta);
                comando.Parameters.AddWithValue("@contrasenna", cuenta.Contrasenna);

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        cuenta.Rol = lector["ROL"].ToString();
                    }
                }
                else
                {
                    cuenta.IdCuenta = "";
                    confirmacion = "Error: El usuario no existe o la cuenta se encuentra inactiva";
                }

                lector.Close();

                comando.Parameters.Clear();

                if (!cuenta.IdCuenta.Equals(""))
                {

                    comando.CommandText = "SELECT NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO, CODIGO_ASISTENTE FROM USUARIO WHERE CEDULA = @cedula";


                    comando.Parameters.AddWithValue("@cedula", cuenta.IdCuenta);

                    lector = comando.ExecuteReader();

                    if (lector.HasRows)
                    {
                        while (lector.Read())
                        {
                            usuario.Nombre = lector["NOMBRE"].ToString();
                            usuario.PrimerApellido = lector["PRIMER_APELLIDO"].ToString();
                            usuario.SegundoApellido = lector["SEGUNDO_APELLIDO"].ToString();
                            if (cuenta.Rol.Equals("asistente"))
                            {
                                usuario.CodigoAsistente = lector["CODIGO_ASISTENTE"].ToString();
                            }
                        }
                    }

                    lector.Close();
                    comando.Parameters.Clear();

                    if (cuenta.Rol.Equals("medico"))
                    {
                        comando.CommandText = "SELECT CODIGO_MEDICO FROM MEDICO WHERE ID_MEDICO = @idMedico";


                        comando.Parameters.AddWithValue("@idMedico", cuenta.IdCuenta);

                        lector = comando.ExecuteReader();

                        if (lector.HasRows)
                        {
                            while (lector.Read())
                            {
                                usuario.CodigoAsistente = lector["CODIGO_MEDICO"].ToString();
                            }
                        }

                        lector.Close();

                    }

                }


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
                    confirmacion = "Error: No se pudo iniciar la sesión de usuario";
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

        public string ActualizarCuenta(TOCuenta toCuenta, TOUsuario toUsuario, TOMedico toMedico)
        {
            string confirmacion = "La cuenta se actualizó exitosamente";

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
                    confirmacion = "Error: No se pudo actualizar la cuenta en el sistema";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pudo actualizar la cuenta en el sistema";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;



            try
            {
                transaccion = conexion.BeginTransaction("Actualizar una cuenta");

                // Se crea un nuevo comando con la secuencia SQL y el objeto de conexión

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.CommandText = "UPDATE CUENTA_USUARIO SET " +
                    "CORREO = @correo, " +
                    "CONTRASENNA = @contrasenna, " +
                    "ROL = @rol, " +
                    "ESTADO = @estado " +
                    "WHERE ID_CUENTA = @id";


                comando.Transaction = transaccion;

                // Se asigna un valor a los parámetros del comando a ejecutar

                comando.Parameters.AddWithValue("@id", toCuenta.IdCuenta);
                comando.Parameters.AddWithValue("@correo", toCuenta.Correo);
                comando.Parameters.AddWithValue("@contrasenna", toCuenta.Contrasenna);
                comando.Parameters.AddWithValue("@rol", toCuenta.Rol);
                comando.Parameters.AddWithValue("@estado", toCuenta.Estado);

                // Se ejecuta el comando y se realiza un commit de la transacción

                comando.ExecuteNonQuery();


                comando.Parameters.Clear();


                comando.CommandText = "UPDATE USUARIO SET " +
                    "NOMBRE = @nombre, " +
                    "PRIMER_APELLIDO = @primer, " +
                    "SEGUNDO_APELLIDO = @segundo, " +
                    "TELEFONO = @telefono, " +
                    "CODIGO_ASISTENTE = @codigoAsistente " +
                    "WHERE CEDULA = @cedula;";

                // Se asigna un valor a los parámetros del comando a ejecutar

                comando.Parameters.AddWithValue("@cedula", toUsuario.Cedula);
                comando.Parameters.AddWithValue("@nombre", toUsuario.Nombre);
                comando.Parameters.AddWithValue("@primer", toUsuario.PrimerApellido);
                comando.Parameters.AddWithValue("@segundo", toUsuario.SegundoApellido);
                comando.Parameters.AddWithValue("@telefono", toUsuario.Telefono);
                comando.Parameters.AddWithValue("@codigoAsistente", toUsuario.CodigoAsistente);

                // Se ejecuta el comando y se realiza un commit de la transacción

                comando.ExecuteNonQuery();

                comando.Parameters.Clear();

                if (toCuenta.Rol.Equals("medico") && toMedico != null)
                {
                    comando.CommandText = "UPDATE MEDICO SET " +
                        "CODIGO_MEDICO = @codigo, " +
                        "ESPECIALIDAD = @especialidad, " +
                        "DURACION_CITA = @duracion " +
                        "WHERE ID_MEDICO = @id;";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@id", toMedico.IdMedico);
                    comando.Parameters.AddWithValue("@codigo", toMedico.CodigoMedico);
                    comando.Parameters.AddWithValue("@especialidad", toMedico.Especialidad);
                    comando.Parameters.AddWithValue("@duracion", toMedico.DuracionCita);

                    // Se ejecuta el comando y se realiza un commit de la transacción

                    comando.ExecuteNonQuery();
                }

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
                    confirmacion = "Error: No se pudo actualizar la cuenta en el sistema";
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

        public string ActualizarEstados(List<TOCuenta> cuentas)
        {
            string confirmacion = "Las cuentas se actualizaron exitosamente";

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
                    confirmacion = "Error: No se pudieron actualizar las cuentas en el sistema";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pudieron actualizar las cuentas en el sistema";
                return confirmacion;
            }

            // Se inicia una nueva transacción

            SqlTransaction transaccion = null;



            try
            {
                transaccion = conexion.BeginTransaction("Actualizar estado de cuentas");

                // Se crea un nuevo comando con la secuencia SQL y el objeto de conexión

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;

                comando.CommandText = "UPDATE CUENTA_USUARIO SET " +
                    "ESTADO = @estado " +
                    "WHERE ID_CUENTA = @id";

                comando.Transaction = transaccion;

                foreach (TOCuenta c in cuentas)
                {
                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@id", c.IdCuenta);
                    comando.Parameters.AddWithValue("@estado", c.Estado);

                    // Se ejecuta el comando y se realiza un commit de la transacción

                    comando.ExecuteNonQuery();

                    comando.Parameters.Clear();

                }

                transaccion.Commit();

            }
            catch (Exception)
            {
                try
                {
                    transaccion.Rollback();
                }
                catch (Exception)
                {
                }
                finally
                {
                    confirmacion = "Error: No se pudieron actualizar las cuentas en el sistema";
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












