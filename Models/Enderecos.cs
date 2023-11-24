using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_POO.Models
{
    public class Enderecos
    {
        public int id { get; set; }
        public int numero { get; set; }
        public string logradouro { get; set; }
        public string bairro { get; set; }

        public Cliente_ cliente { get; set; }



    }
}
