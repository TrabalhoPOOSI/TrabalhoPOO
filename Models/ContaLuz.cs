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
        [Required]
        public string Endereco { get; set; }

        public double? ConsumoMesAnterior { get; set; }

        [Required]
        public TipoContaEnergia TipoConta { get; set; }

        public ContaLuz(double consumo, DateOnly vencimento, string endereco) : base(consumo, vencimento)
        {
            Endereco = endereco;
            using (var db = new ProjetoDbContext())
            {
                this.ConsumoMesAnterior = db.ContaLuz.Where(c => c.lançamento < DateTime.Now).Select(c => c.Consumo).FirstOrDefault();
            }
            if (ConsumoMesAnterior != null)
                consumo = (double)(consumo - ConsumoMesAnterior);
            tarifa = TarifaLuz().ToString("F2");

            // sem imposto
            Subtotal = (decimal)(consumo * TarifaLuz() + ContribuiçãoPublica());

            //com imposto

            Total = Subtotal * ((decimal)Imposto());
        }

        public double TarifaLuz()
        {
            if (TipoConta.ToString() == "RESIDENCIAL")
            {
                return 0.46;
            }
            else
            {
                return 0.41;
            }
        }

        public double ContribuiçãoPublica()
        {
            return 13.25;
        }

        public double Imposto()
        {
            if (Consumo >= 90)
            {
                if (TipoConta.ToString() == "RESIDENCIAL")
                {
                    return 1.4285;
                }
                else
                {
                    return 1.2195;
                }
            }
            else
            {
                // isento
                return 0;
            }
        }
    }
}
