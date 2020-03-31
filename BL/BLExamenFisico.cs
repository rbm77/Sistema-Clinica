using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLExamenFisico
    {
        public double Peso { get; set; }
        public double Talla { get; set; }
        public double PerimetroCefalico { get; set; }
        public double IMC { get; set; }
        public double SO2 { get; set; }
        public double Temperatura { get; set; }
        public string PC_Edad { get; set; }
        public string Peso_Edad { get; set; }
        public string Talla_Edad { get; set; }
        public string Peso_Talla { get; set; }
        public string IMC_Edad { get; set; }
        public string EstadoAlerta { get; set; }
        public string EstadoHidratacion { get; set; }
        public string RuidosCardiacos { get; set; }
        public string CamposPulmonares { get; set; }
        public string Abdomen { get; set; }
        public string Faringe { get; set; }
        public string Nariz { get; set; }
        public string Oidos { get; set; }
        public string SNC { get; set; }
        public string Neurodesarrollo { get; set; }
        public string SistemaOsteomuscular { get; set; }
        public string Piel { get; set; }
        public string OtrosHallazgos { get; set; }

        public BLExamenFisico()
        {

        }
        public BLExamenFisico(double peso, double talla, double perimetroCefalico,
            double imc, double so2, double temperatura, string pc_edad, string peso_edad,
            string talla_edad, string peso_talla, string imc_edad, string estadoAlerta, 
            string estadoHidratacion, string ruidosCardiacos, string camposPulmonares, string abdomen, string faringe,
            string nariz, string oidos, string snc, string neurodesarrollo, string sistemaOsteomuscular,
            string piel, string otrosHallazgos)
        {
            this.Peso = peso;
            this.Talla = talla;
            this.PerimetroCefalico = perimetroCefalico;
            this.IMC = imc;
            this.SO2 = so2;
            this.Temperatura = temperatura;
            this.PC_Edad = pc_edad;
            this.Peso_Edad = peso_edad;
            this.Talla_Edad = talla_edad;
            this.Peso_Talla = peso_talla;
            this.IMC_Edad = imc_edad;
            this.EstadoAlerta = estadoAlerta;
            this.EstadoHidratacion = estadoHidratacion;
            this.RuidosCardiacos = ruidosCardiacos;
            this.CamposPulmonares = camposPulmonares;
            this.Abdomen = abdomen;
            this.Faringe = faringe;
            this.Nariz = nariz;
            this.Oidos = oidos;
            this.SNC = snc;
            this.Neurodesarrollo = neurodesarrollo;
            this.SistemaOsteomuscular = sistemaOsteomuscular;
            this.Piel = piel;
            this.OtrosHallazgos = otrosHallazgos;
        }


    }
}
