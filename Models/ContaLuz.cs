using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_POO.Context;
using Trabalho_POO.Enums;
using Trabalho_POO.Enums;
using Trabalho_POO.Interfaces;

namespace Trabalho_POO.Models
{
    public class ContaLuz : Conta, TarifasLuz
    {
        public double imposto = 0.03;


        public ContaLuz(double leitura, DateOnly vencimento) : base(vencimento)
        {
            this.leitura = leitura;
        }

        public double TarifaLuz()
        {
            if (tipo.ToString() == "RESIDENCIAL")
            {
                return 0.46;
            }
            else if (tipo.ToString() == "COMERCIAL")
            {
                return 0.41;
            }
            else { return 0; }
        }

        public double ContribuiçãoPublica()
        {
            return 13.25;
        }

        public decimal Imposto()
        {
            if (consumo >= 90)
            {
                if (tipo == Tipo_Consumidor.RESIDENCIAL)
                {
                    return (decimal)1.4285;
                }
                else
                {
                    return (decimal)1.2195;
                }
            }
            else
            {
                // isento
                return 0;
            }
        }

        public void calculaTotal()
        {

            if (leituraAnterior == null || leituraAnterior == 0)
            {
                consumo = (double)(leitura);
            }
            else
            {
                if (leitura < leituraAnterior)
                {
                    consumo = leitura;
                }
                else
                {
                    consumo = (double)(leitura - leituraAnterior);
                }
            }

            this.Subtotal = (decimal)(consumo * TarifaLuz() + ContribuiçãoPublica());

            this.Total = Imposto() == 0 ? Subtotal : Imposto() * Subtotal;
        }
    }
}
