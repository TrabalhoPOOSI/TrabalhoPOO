using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_POO.Enums;
using System.ComponentModel.DataAnnotations;

namespace Trabalho_POO.Models
{
    public class Cliente_
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(14)]
        [Key]
        public string Id { get; set; }
        public string password { get; set; }

        public ICollection<Conta>? contas { get; set; }
        public ICollection<Enderecos>? enderecos { get; set; }
        public Cliente_(string nome, string id, string password)
        {
            Nome = nome;
            Id = id;
            this.password = password;
            contas = new List<Conta>();
            enderecos = new List<Enderecos>();
        }

    }
}
