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
                    confirmacion = "Ocurrió un error y no se pudo ingresar la cuenta en el sistema";
                    return confirmacion;

                }
            }
            else
            {
                confirmacion = "Ocurrió un error y no se pudo ingresar la cuenta en el sistema";
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
                    confirmacion = "Ocurrió un error y no se pudo ingresar la cuenta en el sistema";
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
