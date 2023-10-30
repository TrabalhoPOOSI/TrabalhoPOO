using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ContaLuz( double consumo) : base(consumo)
        {

        }

        public double TarifaLuz()
        {
            if (TipoConta.ToString() == "RESIDENCIAL")
            {
                return Consumo * 0.46;
            }
            else
            {
                return Consumo* 0.41;
            }
        }
    }
}
