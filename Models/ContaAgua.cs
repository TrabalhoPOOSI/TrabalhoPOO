using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_POO.Enums;
using Trabalho_POO.Interfaces;

namespace Trabalho_POO.Models
{
    public class ContaAgua : Conta, TarifasDagua
    {
        public string tarifaEsgoto { get; set; }
        public double consumoEsgoto { get; set; }
        public ContaAgua(double consumo, double consumoEsgoto, DateOnly vencimento) : base(consumo, vencimento)
        {
            this.tarifa = TarifaAgua().ToString("F2");
            this.tarifaEsgoto = TarifaEsgoto().ToString("F2");
            this.Consumo = consumo;
            this.consumoEsgoto = consumoEsgoto;
            this.status = StatusConta.EmAberto;
            // criar sobrecarga para tipo consumidor diferrentes
            this.tipo = Tipo_Consumidor.RESIDENCIAL;
            this.Subtotal = (decimal)(TarifaEsgoto() + TarifaAgua());
            this.Total = (decimal)(TarifaEsgoto() * consumoEsgoto + TarifaAgua() * Consumo);
        }

        public double TarifaEsgoto()
        {
            if (tipo.ToString() == "COMERCIAL")
            {
                if (consumoEsgoto < 6) { return 12.90; }
                else if (consumoEsgoto >= 6 && consumoEsgoto < 10) { return 2.149; }
                else if (consumoEsgoto >= 10 && consumoEsgoto < 40) { return 4.111; }
                else if (consumoEsgoto >= 40 && consumoEsgoto < 100) { return 4.144; }
                else if (consumoEsgoto >= 100) { return 4.165; }

            }
            if (consumoEsgoto < 6) { tipo = Tipo_Consumidor.SOCIAL; return 5.05; }
            else if (consumoEsgoto >= 6 && consumoEsgoto < 10) { tipo = Tipo_Consumidor.SOCIAL; return 1.122; }
            else if (consumoEsgoto >= 10 && consumoEsgoto < 15) { return 2.724; }
            else if (consumoEsgoto >= 15 && consumoEsgoto < 20) { return 2.731; }
            else if (consumoEsgoto >= 20 && consumoEsgoto < 40) { return 2.744; }
            else { return 5.035; }
        }

        public double TarifaAgua()
        {

            if (tipo.ToString() == "COMERCIAL")
            {
                if (Consumo < 6) { return 25.79; }
                else if (Consumo >= 6 && Consumo < 10) { return 4.299; }
                else if (Consumo >= 10 && Consumo < 40) { return 8.221; }
                else if (Consumo >= 40 && Consumo < 100) { return 8.288; }
                else if (Consumo >= 100) { return 8.329; }

            }
            if (Consumo < 6) { tipo = Tipo_Consumidor.SOCIAL; return 10.08; }
            else if (Consumo >= 6 && Consumo < 10) { tipo = Tipo_Consumidor.SOCIAL; return 2.241; }
            else if (Consumo >= 10 && Consumo < 15) { return 5.447; }
            else if (Consumo >= 15 && Consumo < 20) { return 5.461; }
            else if (Consumo >= 20 && Consumo < 40) { return 5.487; }
            else { return 10.066; }

        }
    }
}
