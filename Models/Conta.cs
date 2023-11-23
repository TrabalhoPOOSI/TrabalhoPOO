using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_POO.Enums;
using Trabalho_POO.Interfaces;

namespace Trabalho_POO.Models
{

    public abstract class Conta
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Endereco { get; set; }

        public double leitura { get; set; }
        public double leituraAnterior { get; set; }
        [Required]
        public DateTime lançamento { get; set; }
        public DateOnly vencimento { get; set; }

        public Cliente_ cliente { get; set; }

        public string clienteId { get; set; }

        [Required]
        [Column(TypeName = "Double (10,3)")]
        public double consumo { get; set; }
        [Required]
        [Column(TypeName = "Decimal (10,2)")]
        public decimal Subtotal { get; set; }

        [Required]
        [Column(TypeName = "Decimal (10,2)")]
        public decimal Total { get; set; }
        public Tipo_Consumidor tipo { get; set; }
        public StatusConta status { get; set; }
        public Conta(DateOnly venc, string endereco)
        {
            status = StatusConta.EmAberto;
            lançamento = DateTime.Now;
            vencimento = venc;
            Endereco = endereco;
        }
    }
}
