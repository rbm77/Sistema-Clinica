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
                if (Session["accion"] != null)
                {

                    if (Request.QueryString["accion"] != null)
                    {
                        if (Request.QueryString["accion"].Equals("dia"))
                        {
                            Session["esDia"] = true;
                        }
                    }
                    else
                    {
                        Session["esDia"] = false;
                    }

                    CargarEnfermedades();

                    BLExpediente expediente = (BLExpediente) Session["expediente"];

                    expedienteEncabezado.InnerText = expediente.IDExpediente + "";
                    cedulaEncabezado.InnerText = expediente.Cedula;
                    nombreEncabezado.InnerText = expediente.Nombre + " " + expediente.PrimerApellido + " " + expediente.SegundoApellido;

                    string accion = Session["accion"].ToString();

                    if (accion.Equals("crearConsulta"))
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
                    }
                    else if (accion.Equals("consultarConsulta"))
                    {
                        if (Session["fechaConsulta"] != null)
                        {
                            long idExpediente = long.Parse(Session["idExpediente"].ToString());
                            CargarConsulta(idExpediente, Session["fechaConsulta"].ToString());
                        }
                    }
                    
                }
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

                string confirmacion = "";

                if (Session["accion"].Equals("crearConsulta"))
                {
                    confirmacion = manejador.CrearConsulta(consulta);

                    if (!confirmacion.Contains("Error:"))
                    {
                        Titulo.InnerText = "Consulta Médica";
                        fecha.ReadOnly = true;
                        hora.ReadOnly = true;
                        medioDia.Disabled = true;
                        generarReferencia.Checked = false;
                        Session["accion"] = "consultarConsulta";
                        if (generarRef)
                        {
                            // SE LLAMA AL METODO QUE GENERA EL PDF PARA DESCARGARLO

                        }
                    }
                }
                else if (Session["accion"].Equals("consultarConsulta"))
                {
                    confirmacion = manejador.ActualizarConsulta(consulta);
                    if (confirmacion.Contains("Error:"))
                    {
                        CargarConsulta(long.Parse(Session["idExpediente"].ToString()), Session["fechaConsulta"].ToString());
                    }
                    else
                    {
                        generarReferencia.Checked = false;
                        if (generarRef)
                        {
                            // SE LLAMA AL METODO QUE GENERA EL PDF PARA DESCARGARLO

                        }
                    }
                }
                MensajeAviso(confirmacion);
            }
            else
            {
                MensajeAviso("Error: Puede que algunos datos se encuentren vacíos o con un formato incorrecto");
                if (Session["accion"] != null && Session["accion"].Equals("consultarConsulta"))
                {
                    CargarConsulta(long.Parse(Session["idExpediente"].ToString()), Session["fechaConsulta"].ToString());
                }
            }
        }
        private bool GenerarReferencia()
        {
            return true;
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

        private void CargarConsulta(long idExpediente, string fechaConsulta)
        {
            ManejadorConsultas manejador = new ManejadorConsultas();
            BLConsulta consulta = new BLConsulta();
            BLExamenFisico examenFisico = new BLExamenFisico();
            consulta.ExamenFisico = examenFisico;
            consulta.IDExpediente = idExpediente;
            consulta.Fecha = fechaConsulta;
            string confirmacion = manejador.CargarConsulta(consulta);

            if (!confirmacion.Contains("Error:"))
            {
                // Limpiar datos

                fecha.Text = "";
                hora.Text = "";
                medioDia.Value = "am";
                frecuencia.SelectedValue = "nulo";
                referidoA.SelectedValue = "nulo";
                especialidad.Value = "";
                motivo.Value = "";

                peso.Text = "";
                talla.Text = "";
                perimetroCefalico.Text = "";
                IMC.Text = "";
                temperatura.Text = "";
                SO2.Text = "";
                padecimientoActual.Value = "";

                perimetroCefalicoEdad.Text = "";
                pesoEdad.Text = "";
                tallaEdad.Text = "";
                pesoTalla.Text = "";
                imcEdad.Text = "";
                ruidosCardiacos.Value = "";
                camposPulmonares.Value = "";
                abdomen.Value = "";
                faringe.Value = "";
                nariz.Value = "";
                oidos.Value = "";
                snc.Value = "";
                neurodesarrollo.Value = "";
                sistemaOsteomuscular.Value = "";
                piel.Value = "";
                estadoAlerta.Value = "";
                estadoHidratacion.Value = "";
                otrosHallazgos.Value = "";
                analisis.Value = "";
                impresionDiagnostica.Value = "";
                plan.Value = "";
                enfermedades.SelectedValue = "ninguna";
                generarReferencia.Checked = false;

                // Cargar datos

                fecha.Text = consulta.Fecha;
                fecha.ReadOnly = true;
                string[] entradaHora = consulta.Hora.Split('|');
                hora.Text = entradaHora[0];
                medioDia.Value = entradaHora[1];
                hora.ReadOnly = true;
                medioDia.Disabled = true;
                string mmFrecuencia = consulta.MMFrecuencia;
                if (mmFrecuencia == null || mmFrecuencia.Equals(""))
                {
                    mmFrecuencia = "nulo";
                }
                string mmReferido = consulta.MMReferidoA;
                if (mmReferido == null || mmReferido.Equals(""))
                {
                    mmReferido = "nulo";
                }
                string cpEspecialidad = consulta.CPEspecialidad;
                if (cpEspecialidad == null)
                {
                    cpEspecialidad = "";
                }
                string cpMotivo = consulta.CPMotivo;
                if (cpMotivo == null)
                {
                    cpMotivo = "";
                }
                frecuencia.SelectedValue = mmFrecuencia;
                referidoA.SelectedValue = mmReferido;
                especialidad.Value = cpEspecialidad;
                motivo.Value = cpMotivo;

                string pesoTemp = consulta.ExamenFisico.Peso + "";
                string tallaTemp = consulta.ExamenFisico.Talla + "";
                string pcTemp = consulta.ExamenFisico.PerimetroCefalico + "";
                string imcTemp = consulta.ExamenFisico.IMC + "";
                string tempTemp = consulta.ExamenFisico.Temperatura + "";
                string so2Temp = consulta.ExamenFisico.SO2 + "";

                if(pesoTemp == null || pesoTemp.Equals("0"))
                {
                    pesoTemp = "";
                }
                if (tallaTemp == null || tallaTemp.Equals("0"))
                {
                    tallaTemp = "";
                }
                if (pcTemp == null || pcTemp.Equals("0"))
                {
                    pcTemp = "";
                }
                if (imcTemp == null || imcTemp.Equals("0"))
                {
                    imcTemp = "";
                }
                if (tempTemp == null || tempTemp.Equals("0"))
                {
                    tempTemp = "";
                }
                if (so2Temp == null || so2Temp.Equals("0"))
                {
                    so2Temp = "";
                }

                peso.Text = pesoTemp;
                talla.Text = tallaTemp;
                perimetroCefalico.Text = pcTemp;
                IMC.Text = imcTemp;
                temperatura.Text = tempTemp;
                SO2.Text = so2Temp;

                padecimientoActual.Value = consulta.PadecimientoActual;

                string pcEdadTemp = consulta.ExamenFisico.PC_Edad;
                string pesoEdadTemp = consulta.ExamenFisico.Peso_Edad;
                string tallaEdadTemp = consulta.ExamenFisico.Talla_Edad;
                string pesoTallaTemp = consulta.ExamenFisico.Peso_Talla;
                string imcEdadTemp = consulta.ExamenFisico.IMC_Edad;

                if (pcEdadTemp.Equals("nulo"))
                {
                    pcEdadTemp = "";
                }
                if (pesoEdadTemp.Equals("nulo"))
                {
                    pesoEdadTemp = "";
                }
                if (tallaEdadTemp.Equals("nulo"))
                {
                    tallaEdadTemp = "";
                }
                if (pesoTallaTemp.Equals("nulo"))
                {
                    pesoTallaTemp = "";
                }
                if (imcEdadTemp.Equals("nulo"))
                {
                    imcEdadTemp = "";
                }

                perimetroCefalicoEdad.Text = pcEdadTemp;
                pesoEdad.Text = pesoEdadTemp;
                tallaEdad.Text = tallaEdadTemp;
                pesoTalla.Text = pesoTallaTemp;
                imcEdad.Text = imcEdadTemp;

                ruidosCardiacos.Value = consulta.ExamenFisico.RuidosCardiacos;
                camposPulmonares.Value = consulta.ExamenFisico.CamposPulmonares;
                abdomen.Value = consulta.ExamenFisico.Abdomen;
                faringe.Value = consulta.ExamenFisico.Faringe;
                nariz.Value = consulta.ExamenFisico.Nariz;
                oidos.Value = consulta.ExamenFisico.Oidos;
                snc.Value = consulta.ExamenFisico.SNC;
                neurodesarrollo.Value = consulta.ExamenFisico.Neurodesarrollo;
                sistemaOsteomuscular.Value = consulta.ExamenFisico.SistemaOsteomuscular;
                piel.Value = consulta.ExamenFisico.Piel;
                estadoAlerta.Value = consulta.ExamenFisico.EstadoAlerta;
                estadoHidratacion.Value = consulta.ExamenFisico.EstadoHidratacion;
                otrosHallazgos.Value = consulta.ExamenFisico.OtrosHallazgos;
                analisis.Value = consulta.Analisis;
                impresionDiagnostica.Value = consulta.ImpresionDiagnostica;
                plan.Value = consulta.Plan;
                try
                {
                    enfermedades.SelectedValue = consulta.Enfermedad;
                } catch (Exception)
                {
                    enfermedades.SelectedValue = "ninguna";
                }
                Titulo.InnerText = "Consulta Médica";
            }
            else
            {
                MensajeAviso(confirmacion);
                contenedorDatos.Visible = false;
            }

        }

    }
}