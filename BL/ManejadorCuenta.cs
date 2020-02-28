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

                ValidarCuenta(toCuenta);

                ValidarUsuario(toUsuario);

                if (toCuenta != null && toUsuario != null)
                {
                    DAOCuenta daoCuenta = new DAOCuenta();

                    if (toCuenta.Rol.Equals("medico") && blMedico != null)
                    {
                        toMedico = new TOMedico(blMedico.IdMedico.Trim(), blMedico.CodigoMedico.Trim(),
                            blMedico.Especialidad.Trim(), blMedico.DuracionCita.Trim());

                        ValidarMedico(toMedico);

                        if (toMedico == null)
                        {
                            return "Error: Puede que algunos datos se encuentren vacíos o con un formato incorrecto.";
                        }

                    }
                    
                    return daoCuenta.CrearCuenta(toCuenta, toUsuario, toMedico);
                }
                else
                {
                    return "Error: Puede que algunos datos se encuentren vacíos o con un formato incorrecto.";
                }
            }
            return confirmacion;
        }

        private void ValidarCuenta(TOCuenta toCuenta)
        {
            if ((!toCuenta.Correo.Contains("@")) || (toCuenta.Correo == null) || (toCuenta.Correo.Equals(""))
                || (toCuenta.Contrasenna == null) || (toCuenta.Contrasenna.Equals(""))
                || (toCuenta.Rol == null) || (toCuenta.Rol.Equals(""))
                || (toCuenta.Estado == null) || (toCuenta.Estado.Equals(""))
                || (toCuenta.IdCuenta == null) ||(toCuenta.IdCuenta.Equals("")))
            {
                toCuenta = null;
            }
        }

        private void ValidarUsuario(TOUsuario toUsuario)
        {
            if ((toUsuario.Cedula == null) || (toUsuario.Cedula.Equals(""))
                || (toUsuario.Nombre == null) || (toUsuario.Nombre.Equals(""))
                || (toUsuario.PrimerApellido == null) || (toUsuario.PrimerApellido.Equals(""))
                || (toUsuario.SegundoApellido == null) || (toUsuario.SegundoApellido.Equals(""))
                || (toUsuario.Telefono == 0))
            {
                toUsuario = null;
            }
        }

        private void ValidarMedico(TOMedico toMedico)
        {
            if ((toMedico.IdMedico == null) || (toMedico.IdMedico.Equals(""))
                || (toMedico.CodigoMedico == null) || (toMedico.CodigoMedico.Equals(""))
                || (toMedico.Especialidad == null) || (toMedico.Especialidad.Equals(""))
                || (toMedico.DuracionCita == null) || (toMedico.DuracionCita.Equals("")))
            {
                toMedico = null;
            }
        }
    }
}
