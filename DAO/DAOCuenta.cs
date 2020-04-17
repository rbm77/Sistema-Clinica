﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;
using System.Data.SqlClient;
using System.Data;

namespace DAO
{
    public class DAOCuenta
    {
        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexion);
        public string CrearCuenta(TOCuenta toCuenta, TOUsuario toUsuario, TOMedico toMedico)
        {
            string confirmacion = "La cuenta se creó exitosamente.";

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
                    confirmacion = "Error: No se pudo ingresar la cuenta en el sistema";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pudo ingresar la cuenta en el sistema";
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

                comando.CommandText = "INSERT INTO CUENTA_USUARIO (ID_CUENTA, CORREO, CONTRASENNA, ROL, ESTADO)" +
                    "VALUES (@id, @correo, @contrasenna, @rol, @estado);";


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


                comando.CommandText = "INSERT INTO USUARIO (CEDULA, NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO," +
                    " TELEFONO, CODIGO_ASISTENTE) VALUES (@cedula, @nombre, @primer, @segundo, @telefono, @codigoAsistente);";

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

                if(toCuenta.Rol.Equals("medico") && toMedico != null)
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
                    confirmacion = "Error: No se pudo ingresar la cuenta en el sistema";
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


        public string CargarCodigosMedicos(List<string> codigos)
        {
            string confirmacion = "Los códigos de los médicos se cargaron exitosamente.";

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
                    confirmacion = "Error: No se pudieron cargar los códigos de los médicos";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Error: No se pudieron cargar los códigos de los médicos";
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

                comando.CommandText = "SELECT CODIGO_MEDICO FROM MEDICO";


                comando.Transaction = transaccion;


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
                    confirmacion = "Error: No se pudieron cargar los códigos de los médicos";
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
            string confirmacion = "El usuario se cargó exitosamente.";

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

                comando.CommandText = "SELECT CORREO, ROL, ESTADO FROM CUENTA_USUARIO WHERE ID_CUENTA = @idCuenta AND CONTRASENNA = @contrasenna";


                comando.Transaction = transaccion;


                // Se ejecuta el comando y se realiza un commit de la transacción

                comando.Parameters.AddWithValue("@idCuenta", cuenta.IdCuenta);
                comando.Parameters.AddWithValue("@contrasenna", cuenta.Contrasenna);

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        cuenta.Correo = lector["CORREO"].ToString();
                        cuenta.Rol = lector["ROL"].ToString();
                        cuenta.Estado = lector["ESTADO"].ToString();
                    }
                }
                else
                {
                    cuenta.IdCuenta = "";
                }

                lector.Close();

                comando.Parameters.Clear();


                if(!cuenta.IdCuenta.Equals(""))
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


                } else
                {
                    confirmacion = "Error: Las credenciales ingresadas no son correctas.";
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

    }
}












