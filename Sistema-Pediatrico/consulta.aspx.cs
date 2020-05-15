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
            enfermedades.Items.Add(new ListItem("Seleccionar...", "nulo"));
            enfermedades.SelectedValue = "nulo";
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

            if (!fechaCreacion.Equals("") && !horaCreacion.Equals(""))
            {

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