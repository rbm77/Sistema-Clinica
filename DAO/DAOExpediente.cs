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
            string confirmacion = "El expediente se creó exitosamente.";
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

                int idExpedienteGuardado = 0;

                if (expediente != null)
                {
                    comando.CommandText = "INSERT INTO EXPEDIENTE (CEDULA, NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO, FECHA_NACIMIENTO," +
                    " SEXO, URL_FOTO, URL_EXPEDIENTE_ANTIGUO, CODIGO_DIRECCION, DIRECCION_EXACTA, ID_ENCARGADO, ID_DESTINATARIO_FACTURA," +
                    " ID_SOLICITANTE_CITA, FECHA_CREACION, ID_MEDICO) output INSERTED.ID_EXPEDIENTE" + " VALUES (@cedula, @nombre, @primerApellido, @segundoApellido, @fechaNacimiento," +
                    "@sexo, @urlFoto, @urlExpediente, @codigoDireccion, @direccionExacta, @idEncargado, @idDestinatario, @idSolicitante, @fechaCreacion," +
                    "@idMedico);";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@cedula", expediente.Cedula);
                    comando.Parameters.AddWithValue("@nombre", expediente.Nombre);
                    comando.Parameters.AddWithValue("@primerApellido", expediente.PrimerApellido);
                    comando.Parameters.AddWithValue("@segundoApellido", expediente.SegundoApellido);
                    comando.Parameters.AddWithValue("@fechaNacimiento", expediente.FechaNacimiento);
                    comando.Parameters.AddWithValue("@sexo", expediente.Sexo);
                    comando.Parameters.AddWithValue("@urlFoto", expediente.UrlFoto);
                    comando.Parameters.AddWithValue("@urlExpediente", expediente.UrlExpedienteAntiguo);
                    comando.Parameters.AddWithValue("@codigoDireccion", expediente.Direccion.CodigoDireccion);
                    comando.Parameters.AddWithValue("@direccionExacta", expediente.DireccionExacta);
                    comando.Parameters.AddWithValue("@idEncargado", expediente.Encargado.Cedula);
                    comando.Parameters.AddWithValue("@idDestinatario", expediente.DestinatarioFactura.Cedula);
                    comando.Parameters.AddWithValue("@idSolicitante", expediente.SolicitanteCita.Correo);
                    comando.Parameters.AddWithValue("@fechaCreacion", expediente.FechaCreacion);
                    comando.Parameters.AddWithValue("@idMedico", expediente.IDMedico);

                    // Se ejecuta el comando y se realiza un commit de la transacción

                    idExpedienteGuardado = (int)comando.ExecuteScalar();

                    comando.Parameters.Clear();
                }

                if (expediente.Encargado != null && idExpedienteGuardado != 0)
                {
                    comando.CommandText = "INSERT INTO ENCARGADO (CEDULA, NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO," +
                    " TELEFONO, CORREO, PARENTESCO, CODIGO_DIRECCION, DIRECCION_EXACTA) VALUES (@cedula, @nombre, @primerApellido, @segundoApellido," +
                    " @telefono, @correo, @parentesco, @codigoDireccion, @direccionExacta);";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@cedula", expediente.Encargado.Cedula);
                    comando.Parameters.AddWithValue("@nombre", expediente.Encargado.Nombre);
                    comando.Parameters.AddWithValue("@primerApellido", expediente.Encargado.PrimerApellido);
                    comando.Parameters.AddWithValue("@segundoApellido", expediente.Encargado.SegundoApellido);
                    comando.Parameters.AddWithValue("@telefono", expediente.Encargado.Telefono);
                    comando.Parameters.AddWithValue("@correo", expediente.Encargado.Correo);
                    comando.Parameters.AddWithValue("@parentesco", expediente.Encargado.Parentesco);
                    comando.Parameters.AddWithValue("@codigoDireccion", expediente.Encargado.Direccion.CodigoDireccion);
                    comando.Parameters.AddWithValue("@direccionExacta", expediente.Encargado.DireccionExacta);

                    // Se ejecuta el comando y se realiza un commit de la transacción

                    comando.ExecuteNonQuery();

                    comando.Parameters.Clear();
                }

                if (expediente.DestinatarioFactura != null && idExpedienteGuardado != 0)
                {
                    comando.CommandText = "INSERT INTO DESTINATARIO_FACTURA (CEDULA, NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO," +
                        " TELEFONO, CORREO, CODIGO_DIRECCION, DIRECCION_EXACTA) VALUES (@cedula, @nombre, @primerApellido, @segundoApellido," +
                        " @telefono, @correo, @codigoDireccion, @direccionExacta);";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@cedula", expediente.DestinatarioFactura.Cedula);
                    comando.Parameters.AddWithValue("@nombre", expediente.DestinatarioFactura.Nombre);
                    comando.Parameters.AddWithValue("@primerApellido", expediente.DestinatarioFactura.PrimerApellido);
                    comando.Parameters.AddWithValue("@segundoApellido", expediente.DestinatarioFactura.SegundoApellido);
                    comando.Parameters.AddWithValue("@telefono", expediente.DestinatarioFactura.Telefono);
                    comando.Parameters.AddWithValue("@correo", expediente.DestinatarioFactura.Correo);
                    comando.Parameters.AddWithValue("@codigoDireccion", expediente.DestinatarioFactura.Direccion.CodigoDireccion);
                    comando.Parameters.AddWithValue("@direccionExacta", expediente.DestinatarioFactura.DireccionExacta);

                    // Se ejecuta el comando y se realiza un commit de la transacción

                    comando.ExecuteNonQuery();

                    comando.Parameters.Clear();
                }

                if (expediente.SolicitanteCita != null && idExpedienteGuardado != 0)
                {
                    comando.CommandText = "INSERT INTO SOLICITANTE_CITA (CORREO, CONTRASENNA, TELEFONO, ESTADO) VALUES (@correo, @contrasenna, @telefono, @estado);";

                    // Se asigna un valor a los parámetros del comando a ejecutar

                    comando.Parameters.AddWithValue("@correo", expediente.SolicitanteCita.Correo);
                    comando.Parameters.AddWithValue("@contrasenna", expediente.SolicitanteCita.Contrasenna);
                    comando.Parameters.AddWithValue("@telefono", expediente.SolicitanteCita.Telefono);
                    comando.Parameters.AddWithValue("@estado", expediente.SolicitanteCita.Estado);

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
    }
}
