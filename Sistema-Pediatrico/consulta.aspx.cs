using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace Sistema_Pediatrico
{
    public partial class consulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                perimetroCefalicoEdad.Text = "Normal";
                pesoEdad.Text = "Normal";
                tallaEdad.Text = "Normal";
                pesoTalla.Text = "Normal";
                imcEdad.Text = "Normal";
                ruidosCardiacos.Value = "Normal";
                camposPulmonares.Value = "Normal";
                abdomen.Value = "Normal";
                faringe.Value = "Normal";
                nariz.Value = "Normal";
                oidos.Value = "Normal";
                snc.Value = "Normal";
                neurodesarrollo.Value = "Normal";
                sistemaOsteomuscular.Value = "Normal";
                piel.Value = "Normal";
                estadoAlerta.Value = "Normal";
                estadoHidratacion.Value = "Normal";
                otrosHallazgos.Value = "Normal";

                fecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
                Titulo.InnerText = "Nueva Consulta Médica";
                CargarEnfermedades();
            }
        }

        private void CargarEnfermedades()
        {
            ManejadorConsultas manejador = new ManejadorConsultas();
            List<string> listaEnfemedades = new List<string>();
            string confimacion = manejador.CargarEnfermedades(listaEnfemedades);
            enfermedades.Items.Clear();
            enfermedades.Items.Add(new ListItem("Seleccionar...", "ninguna"));
            enfermedades.SelectedValue = "ninguna";
            if (!confimacion.Contains("Error:"))
            {
                if (listaEnfemedades.Count > 0)
                {
                    foreach (string i in listaEnfemedades)
                    {
                        ListItem item = new ListItem(i, i);
                        enfermedades.Items.Add(item);
                    }
                }
            }

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string fechaCreacion = fecha.Text.Trim();
            string horaCreacion = hora.Text.Trim() + "|" + medioDia.Value.Trim();

            if(hora.Text.Trim().Equals(""))
            {
                horaCreacion = "";
            }

            string frecuenciaC = frecuencia.SelectedValue.Trim();
            string referidoC = referidoA.SelectedValue.Trim();
            string especialidadC = especialidad.Value.Trim();
            string motivoC = motivo.Value.Trim();

            bool mediMixta = false;
            bool consulprivada = false;

            if (!frecuenciaC.Equals("nulo") || !referidoC.Equals("nulo"))
            {
                if (!frecuenciaC.Equals("nulo") && !referidoC.Equals("nulo"))
                {
                    mediMixta = true;
                }
            }
            else
            {
                mediMixta = true;
                frecuenciaC = "";
                referidoC = "";
            }

            if (!especialidadC.Equals("") || !motivoC.Equals(""))
            {
                if (!especialidadC.Equals("") && !motivoC.Equals(""))
                {
                    consulprivada = true;
                }
            }
            else
            {
                consulprivada = true;
                especialidadC = "";
                motivoC = "";
            }

            string tempPesoC = peso.Text.Trim();
            string tempTallaC = talla.Text.Trim();
            string tempPerimetroCefalicoC = perimetroCefalico.Text.Trim();
            string tempImc = IMC.Text.Trim();
            string tempTemperaturaC = temperatura.Text.Trim();
            string tempSo2 = SO2.Text.Trim();

            double pesoC = 0.0;
            double tallaC = 0.0;
            double perimetroCefalicoC = 0.0;
            double imc = 0.0;
            double temperaturaC = 0.0;
            double so2 = 0.0;

            bool decimalesValidados = true; 

            try
            {
                if (!tempPesoC.Equals(""))
                {
                    pesoC = double.Parse(tempPesoC);
                }
                if (!tempTallaC.Equals(""))
                {
                    tallaC = double.Parse(tempTallaC);
                }
                if (!tempPerimetroCefalicoC.Equals(""))
                {
                    perimetroCefalicoC = double.Parse(tempPerimetroCefalicoC);
                }
                if (!tempImc.Equals(""))
                {
                    imc = double.Parse(tempImc);
                }
                if (!tempTemperaturaC.Equals(""))
                {
                    temperaturaC = int.Parse(tempTemperaturaC);
                }
                if (!tempSo2.Equals(""))
                {
                    so2 = int.Parse(tempSo2);
                }
            } catch (Exception)
            {
                decimalesValidados = false;
            }

            if (!fechaCreacion.Equals("") && !horaCreacion.Equals("") && mediMixta && consulprivada && decimalesValidados)
            {
               
                string padecimientoActualC = padecimientoActual.Value.Trim();

                string perimetroCefalicoEdadC = perimetroCefalicoEdad.Text.Trim();
                string pesoEdadC = pesoEdad.Text.Trim();
                string tallaEdadC = tallaEdad.Text.Trim();
                string pesoTallaC = pesoTalla.Text.Trim();
                string imcEdadC = imcEdad.Text.Trim();
                string ruidosCardiacosC = ruidosCardiacos.Value.Trim();
                string camposPulmonaresC = camposPulmonares.Value.Trim();
                string abdomenC = abdomen.Value.Trim();
                string faringeC = faringe.Value.Trim();
                string narizC = nariz.Value.Trim();
                string oidosC = oidos.Value.Trim();
                string sncC = snc.Value.Trim();
                string neurodesarrolloC = neurodesarrollo.Value.Trim();
                string sistemaOsteomuscularC = sistemaOsteomuscular.Value.Trim();
                string pielC = piel.Value.Trim();
                string estadoAlertaC = estadoAlerta.Value.Trim();
                string estadoHidratacionC = estadoHidratacion.Value.Trim();
                string otrosHallazgosC = otrosHallazgos.Value.Trim();
                string analisisC = analisis.Value.Trim();
                string impresionDiagnosticaC = impresionDiagnostica.Value.Trim();
                string planC = plan.Value.Trim();

                string enfermedadC = enfermedades.SelectedValue.Trim();


                bool generarRef = generarReferencia.Checked;

                // SE CREA LA CONSULTA Y EL EXAMEN FISICO

                ManejadorConsultas manejador = new ManejadorConsultas();

                BLExamenFisico examenFisico = new BLExamenFisico(pesoC, tallaC, perimetroCefalicoC, imc, so2, temperaturaC,
                    perimetroCefalicoEdadC, pesoEdadC, tallaEdadC, pesoTallaC, imcEdadC, estadoAlertaC, estadoHidratacionC,
                    ruidosCardiacosC, camposPulmonaresC, abdomenC, faringeC, narizC, oidosC, sncC, neurodesarrolloC,
                    sistemaOsteomuscularC, pielC, otrosHallazgosC);

                long idExpediente = 0;
                if (Session["idExpediente"] != null)
                {
                    idExpediente = long.Parse(Session["idExpediente"].ToString());
                }

                BLConsulta consulta = new BLConsulta(idExpediente, fechaCreacion, horaCreacion, padecimientoActualC, analisisC,
                    impresionDiagnosticaC, planC, frecuenciaC, referidoC, especialidadC, motivoC, examenFisico, enfermedadC);

                string confirmacion = manejador.CrearConsulta(consulta);

                if (!confirmacion.Contains("Error:"))
                {
                    MensajeAviso(confirmacion);

                    // SE GENERA LA REFERENCIA EN CASO DE SER NECESARIO

                    if(generarRef)
                    {
                        // SE LLAMA AL METODO QUE GENERA EL PDF PARA DESCARGARLO

                    }
                }
                else
                {
                    MensajeAviso(confirmacion);
                }

            }
            else
            {
                MensajeAviso("Error: Puede que algunos datos se encuentren vacíos o con un formato incorrecto");
            }

        }
        private void MensajeAviso(string mensaje)
        {
            string color = "";

            if (mensaje.Contains("Error:"))
            {
                color = "danger";
            }
            else
            {
                color = "success";
            }
            mensajeConfirmacion.Text = "<div class=\"alert alert-" + color + " alert-dismissible fade show\" " +
            "role=\"alert\"> <strong></strong>" + mensaje + "<button" +
            " type = \"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">" +
            " <span aria-hidden=\"true\">&times;</span> </button> </div>";

            mensajeConfirmacion.Visible = true;
        }
    }
}