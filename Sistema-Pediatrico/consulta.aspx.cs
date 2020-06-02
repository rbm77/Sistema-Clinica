using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

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

                    BLExpediente expediente = (BLExpediente)Session["expediente"];

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

            if (hora.Text.Trim().Equals(""))
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
            }
            catch (Exception)
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
                        Session["accion"] = "consultarConsulta";
                    }
                }
                else if (Session["accion"].Equals("consultarConsulta"))
                {
                    confirmacion = manejador.ActualizarConsulta(consulta);
                    if (confirmacion.Contains("Error:"))
                    {
                        CargarConsulta(long.Parse(Session["idExpediente"].ToString()), Session["fechaConsulta"].ToString());
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

                if (pesoTemp == null || pesoTemp.Equals("0"))
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
                }
                catch (Exception)
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
        private void GenerarPDF(BLReferencia referencia)
        {
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Mi título";
            PdfPage page = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(page);

            XFont fontRegular = new XFont("Verdana", 10, XFontStyle.Regular);
            XFont fontBold = new XFont("Verdana", 10, XFontStyle.Bold);

            XTextFormatter tf = new XTextFormatter(graph);

            tf.Alignment = XParagraphAlignment.Justify;

            graph.DrawString(referencia.NombreClinica, fontRegular, XBrushes.Black, new XRect(20, 10, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("Dr. " + referencia.NombreMedico, fontRegular, XBrushes.Black, new XRect(20, 22, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("Código: " + referencia.CodigoMedico, fontRegular, XBrushes.Black, new XRect(20, 34, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("Tel: " + referencia.TelefonoMedico, fontRegular, XBrushes.Black, new XRect(20, 46, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            graph.DrawString("Correo: " + referencia.CorreoMedico, fontRegular, XBrushes.Black, new XRect(20, 58, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);

            graph.DrawString("Fecha: ", fontRegular, XBrushes.Black, new XRect(340, 10, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            graph.DrawString(referencia.FechaReferencia, fontRegular, XBrushes.Black, new XRect(390, 10, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);

            graph.DrawString("Cédula: ", fontRegular, XBrushes.Black, new XRect(340, 22, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            graph.DrawString(referencia.CedulaPaciente, fontRegular, XBrushes.Black, new XRect(390, 22, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);

            graph.DrawString("Nombre: ", fontRegular, XBrushes.Black, new XRect(340, 34, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            graph.DrawString(referencia.NombrePaciente, fontRegular, XBrushes.Black, new XRect(390, 34, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);

            graph.DrawString("Edad: ", fontRegular, XBrushes.Black, new XRect(340, 46, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            graph.DrawString(referencia.EdadPaciente, fontRegular, XBrushes.Black, new XRect(390, 46, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);

            graph.DrawString("Sexo: ", fontRegular, XBrushes.Black, new XRect(340, 58, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            graph.DrawString(referencia.SexoPaciente, fontRegular, XBrushes.Black, new XRect(390, 58, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);

            tf.DrawString("Análisis: ", fontRegular, XBrushes.Black, new XRect(20, 100, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            tf.DrawString(referencia.Analisis, fontRegular, XBrushes.Black, new XRect(20, 115, page.Width - 40, page.Height.Point), XStringFormats.TopLeft);

            tf.DrawString("Impresión Diagnóstica: ", fontRegular, XBrushes.Black, new XRect(20, 250, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            tf.DrawString(referencia.ImpresionDiagnóstica, fontRegular, XBrushes.Black, new XRect(20, 265, page.Width - 40, page.Height.Point), XStringFormats.TopLeft);

            tf.DrawString("A: (Especialidad) ", fontRegular, XBrushes.Black, new XRect(20, 400, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            tf.DrawString(referencia.Especialidad, fontRegular, XBrushes.Black, new XRect(20, 415, page.Width - 40, page.Height.Point), XStringFormats.TopLeft);

            tf.DrawString("Motivo de la Referencia: ", fontRegular, XBrushes.Black, new XRect(20, 450, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            tf.DrawString(referencia.Motivo, fontRegular, XBrushes.Black, new XRect(20, 465, page.Width - 40, page.Height.Point), XStringFormats.TopLeft);

            tf.DrawString("Observaciones: ", fontRegular, XBrushes.Black, new XRect(20, 600, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            tf.DrawString("", fontRegular, XBrushes.Black, new XRect(20, 615, page.Width - 40, page.Height.Point), XStringFormats.TopLeft);


            tf.Alignment = XParagraphAlignment.Center;
            tf.DrawString("__________________________", fontBold, XBrushes.Black, new XRect(0, 745, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            tf.DrawString("Firma", fontRegular, XBrushes.Black, new XRect(0, 760, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);


            // Send PDF to browser
            MemoryStream stream = new MemoryStream();
            pdf.Save(stream, false);

            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Referencia.pdf");
            Response.BinaryWrite(stream.ToArray());
            Response.Flush();
            stream.Close();
            Response.End();
        }

        protected void btnGenerarReferencia_Click(object sender, EventArgs e)
        {
            BLReferencia referencia = new BLReferencia();
            referencia.NombreClinica = "Clínica Pediátrica Divino Niño";
            referencia.NombreMedico = "Robert Gerardo Moya Vásquez";
            referencia.CodigoMedico = "6127";
            referencia.TelefonoMedico = "89712354";
            referencia.CorreoMedico = "robertmoyav@gmail.com";
            referencia.FechaReferencia = DateTime.Today.ToString("dd/MM/yyyy");
            referencia.CedulaPaciente = "207850434";
            referencia.NombrePaciente = "Richard Gerardo Bolaños Rodríguez";
            referencia.EdadPaciente = "21 años";
            referencia.SexoPaciente = "Masculino";
            referencia.Analisis = "Contrary to popular belief, Lorem Ipsum is not simply random text. ñ ñ ñ á é í ó ú AÁ É IÍ Ó 123 " +
                "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. " +
                "Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the " +
                "interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in" +
                " their exact original form, accompanied by English versions from the 1914 translation by H.Rackham.";

            referencia.ImpresionDiagnóstica = "Contrary to popular belief, Lorem Ipsum is not simply random text. ñ ñ ñ á é í ó ú AÁ É IÍ Ó 123 " +
                "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. " +
                "Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the " +
                "more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of " +
                "interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in" +
                " their exact original form, accompanied by English versions from the 1914 translation by H.Rackham.";

            referencia.Especialidad = "Optometría";

            referencia.Motivo = "Contrary to popular belief, Lorem Ipsum is not simply random text. ñ ñ ñ á é í ó ú AÁ É IÍ Ó 123 " +
    "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. " +
    "Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the " +
    "more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of " +
    "interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in" +
    " their exact original form, accompanied by English versions from the 1914 translation by H.Rackham.";

            GenerarPDF(referencia);
        }
    }
}