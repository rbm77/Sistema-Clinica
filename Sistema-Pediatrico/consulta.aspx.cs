using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            }
        }
    }
}