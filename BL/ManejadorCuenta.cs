using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;
using DAO;

namespace BL
{
    public class ManejadorCuenta
    {

        public string CrearCuenta(BLCuenta blCuenta, BLUsuario blUsuario, BLMedico blMedico)
        {
            string confirmacion = "Error: Indefinido.";

            if(blCuenta != null && blUsuario != null)
            {
                TOCuenta toCuenta = new TOCuenta(blCuenta.IdCuenta.Trim(), blCuenta.Correo.Trim(), blCuenta.Contrasenna.Trim(),
               blCuenta.Rol.Trim(), blCuenta.Estado.Trim());

                TOUsuario toUsuario = new TOUsuario(blUsuario.Cedula.Trim(), blUsuario.Nombre.Trim(),
                    blUsuario.PrimerApellido.Trim(), blUsuario.SegundoApellido.Trim(),
                    blUsuario.Telefono, blUsuario.CodigoAsistente);

                TOMedico toMedico = null;

                toCuenta = ValidarCuenta(toCuenta);

                if(toCuenta != null)
                {
                    toUsuario = ValidarUsuario(toUsuario, toCuenta.Rol);
                }

                if (toCuenta != null && toUsuario != null)
                {
                    

                    if (toCuenta.Rol.Equals("medico") && blMedico != null)
                    {
                        toMedico = new TOMedico(blMedico.IdMedico.Trim(), blMedico.CodigoMedico.Trim(),
                            blMedico.Especialidad.Trim(), blMedico.DuracionCita.Trim());

                        toMedico = ValidarMedico(toMedico);

                        if (toMedico == null)
                        {
                            return "Error: Puede que algunos datos se encuentren vacíos o con un formato incorrecto.";
                        }

                    }
                    DAOCuenta daoCuenta = new DAOCuenta();
                    return daoCuenta.CrearCuenta(toCuenta, toUsuario, toMedico);
                }
                else
                {
                    return "Error: Puede que algunos datos se encuentren vacíos o con un formato incorrecto.";
                }
            }
            return confirmacion;
        }

        public string ActualizarCuenta(BLCuenta blCuenta, BLUsuario blUsuario, BLMedico blMedico)
        {
            string confirmacion = "Error: Indefinido.";

            if (blCuenta != null && blUsuario != null)
            {
                TOCuenta toCuenta = new TOCuenta(blCuenta.IdCuenta.Trim(), blCuenta.Correo.Trim(), blCuenta.Contrasenna.Trim(),
               blCuenta.Rol.Trim(), blCuenta.Estado.Trim());

                TOUsuario toUsuario = new TOUsuario(blUsuario.Cedula.Trim(), blUsuario.Nombre.Trim(),
                    blUsuario.PrimerApellido.Trim(), blUsuario.SegundoApellido.Trim(),
                    blUsuario.Telefono, blUsuario.CodigoAsistente);

                TOMedico toMedico = null;

                toCuenta = ValidarCuenta(toCuenta);

                if (toCuenta != null)
                {
                    toUsuario = ValidarUsuario(toUsuario, toCuenta.Rol);
                }

                if (toCuenta != null && toUsuario != null)
                {


                    if (toCuenta.Rol.Equals("medico") && blMedico != null)
                    {
                        toMedico = new TOMedico(blMedico.IdMedico.Trim(), blMedico.CodigoMedico.Trim(),
                            blMedico.Especialidad.Trim(), blMedico.DuracionCita.Trim());

                        toMedico = ValidarMedico(toMedico);

                        if (toMedico == null)
                        {
                            return "Error: Puede que algunos datos se encuentren vacíos o con un formato incorrecto.";
                        }

                    }
                    DAOCuenta daoCuenta = new DAOCuenta();
                    return daoCuenta.ActualizarCuenta(toCuenta, toUsuario, toMedico);
                }
                else
                {
                    return "Error: Puede que algunos datos se encuentren vacíos o con un formato incorrecto.";
                }
            }
            return confirmacion;
        }

        private TOCuenta ValidarCuenta(TOCuenta toCuenta)
        {
            if ((!toCuenta.Correo.Contains("@")) || (toCuenta.Correo == null) || (toCuenta.Correo.Equals(""))
                || (toCuenta.Contrasenna == null) || (toCuenta.Contrasenna.Equals(""))
                || (toCuenta.Rol == null) || (toCuenta.Rol.Equals("")) || (toCuenta.Rol.Equals("nulo"))
                || (toCuenta.Estado == null) || (toCuenta.Estado.Equals(""))
                || (toCuenta.IdCuenta == null) ||(toCuenta.IdCuenta.Equals("")))
            {
                return null;
            } 
            return toCuenta;
        }

        private TOUsuario ValidarUsuario(TOUsuario toUsuario, string rol)
        {
            if ((toUsuario.Cedula == null) || (toUsuario.Cedula.Equals(""))
                || (toUsuario.Nombre == null) || (toUsuario.Nombre.Equals(""))
                || (toUsuario.PrimerApellido == null) || (toUsuario.PrimerApellido.Equals(""))
                || (toUsuario.SegundoApellido == null) || (toUsuario.SegundoApellido.Equals(""))
                || (toUsuario.Telefono.Equals("")) || (rol.Equals("asistente") && toUsuario.CodigoAsistente == null)
                || (rol.Equals("asistente") && toUsuario.CodigoAsistente.Equals("")) || (rol.Equals("asistente") && toUsuario.CodigoAsistente.Equals("nulo")))
            {
                return null;
            }
            return toUsuario;
        }

        private TOMedico ValidarMedico(TOMedico toMedico)
        {
            if ((toMedico.IdMedico == null) || (toMedico.IdMedico.Equals(""))
                || (toMedico.CodigoMedico == null) || (toMedico.CodigoMedico.Equals(""))
                || (toMedico.Especialidad == null) || (toMedico.Especialidad.Equals("")))
            {
                return null;
            }
            return toMedico;
        }

        public string CargarCodigosMedicos(List<string> codigos)
        {
            string confirmacion = "Error: Indefinido.";
            if(codigos != null)
            {
                DAOCuenta daoCuenta = new DAOCuenta();
                return daoCuenta.CargarCodigosMedicos(codigos);
            }
            return confirmacion;
        }

        public string CargarUsuario(BLCuenta cuenta, BLUsuario usuario, BLMedico medico)
        {
            string confirmacion = "Error: Indefinido.";

            TOCuenta toCuenta = new TOCuenta();
            toCuenta.IdCuenta = cuenta.IdCuenta;

            TOUsuario toUsuario = new TOUsuario();
            TOMedico toMedico = new TOMedico();

            DAOCuenta dao = new DAOCuenta();
            confirmacion =  dao.CargarUsuario(toCuenta, toUsuario, toMedico);
            
            if(!confirmacion.Contains("Error:"))
            {

                cuenta.Correo = toCuenta.Correo;
                cuenta.Rol = toCuenta.Rol;
                cuenta.Estado = toCuenta.Estado;
                cuenta.Contrasenna = toCuenta.Contrasenna;

                usuario.Cedula = toUsuario.Cedula;
                usuario.Nombre = toUsuario.Nombre;
                usuario.PrimerApellido = toUsuario.PrimerApellido;
                usuario.SegundoApellido = toUsuario.SegundoApellido;
                usuario.Telefono = toUsuario.Telefono;
                usuario.CodigoAsistente = toUsuario.CodigoAsistente;

                if(toCuenta.Rol.Equals("medico"))
                {
                    medico.IdMedico = toMedico.IdMedico;
                    medico.CodigoMedico = toMedico.CodigoMedico;
                    medico.Especialidad = toMedico.Especialidad;
                    medico.DuracionCita = toMedico.DuracionCita;
                }
            }

            return confirmacion;
        }

        public string CargarUsuarios(List<BLCuenta> cuentas, List<BLUsuario> usuarios)
        {
            string confirmacion = "Error: Indefinido.";

            List<TOCuenta> toCuentas = new List<TOCuenta>();
            List<TOUsuario> toUsuarios = new List<TOUsuario>();

            DAOCuenta dao = new DAOCuenta();
            confirmacion = dao.CargarUsuarios(toCuentas, toUsuarios);

            if (!confirmacion.Contains("Error:"))
            {
                foreach (TOCuenta i in toCuentas) {
                    BLCuenta c = new BLCuenta();
                    c.IdCuenta = i.IdCuenta;
                    c.Correo = i.Correo;
                    c.Estado = i.Estado;
                    cuentas.Add(c);
                }

                foreach (TOUsuario i in toUsuarios)
                {
                    BLUsuario u = new BLUsuario(i.Cedula, i.Nombre, i.PrimerApellido, i.SegundoApellido, i.Telefono, "");
                    usuarios.Add(u);
                }
            }

            return confirmacion;
        }


        public string IniciarSesion(BLCuenta cuenta, BLUsuario usuario)
        {
            string confirmacion = "Error: Indefinido.";

            TOCuenta toCuenta = new TOCuenta(cuenta.IdCuenta, "", cuenta.Contrasenna, "", "");
            TOUsuario toUsuario = new TOUsuario();

            DAOCuenta dao = new DAOCuenta();
            confirmacion = dao.IniciarSesion(toCuenta, toUsuario);

            if (!confirmacion.Contains("Error:"))
            {
                cuenta.Rol = toCuenta.Rol;

                usuario.Nombre = toUsuario.Nombre;
                usuario.PrimerApellido = toUsuario.PrimerApellido;
                usuario.SegundoApellido = toUsuario.SegundoApellido;
                usuario.CodigoAsistente = toUsuario.CodigoAsistente;

            }

            return confirmacion;
        }

        public string ActualizarEstados(List<BLCuenta> cuentas)
        {
            List<TOCuenta> to = new List<TOCuenta>();

            foreach (BLCuenta c in cuentas)
            {
                TOCuenta nueva = new TOCuenta();
                nueva.IdCuenta = c.IdCuenta;
                nueva.Estado = c.Estado;
                to.Add(nueva);
            }

            DAOCuenta daoCuenta = new DAOCuenta();
            return daoCuenta.ActualizarEstados(to);
        }

    }
}
