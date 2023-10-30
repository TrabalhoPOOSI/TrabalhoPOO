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

        public Cliente_ cliente { get; set; }

        public string clienteId { get; set; }

        [StringLength(50)]
        public string tarifa { get; set; }
        [Required]
        [Column(TypeName = "Double (10,3)")]
        public double Consumo { get; set; }
        [Required]
        [Column(TypeName = "Decimal (10,2)")]
        public decimal Subtotal { get; set; }

        [Required]
        [Column(TypeName = "Decimal (10,2)")]
        public decimal Total { get; set; }
        public Tipo_Consumidor tipo { get; set; }
        public StatusConta status { get; set; }
        public Conta(double consumo)
        {
            Consumo = consumo;
            status = StatusConta.EmAberto;
        }
    }
}
