using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_POO.Context;
using Trabalho_POO.Models;
using Trabalho_POO.Views;

namespace Trabalho_POO.Controllers
{
    public static class Servicos
    {
        public static void CriarContaAgua(Cliente_ user)
        {
            using (var db = new ProjetoDbContext())
            {
                var client = db.Clientes.Where(c => c.Id == user.Id).FirstOrDefault();
                Console.WriteLine("Informe o consumo de agua da conta:");
                double consumoAgua = double.Parse(Console.ReadLine());
                Console.WriteLine("Informe o consumo de esgoto da conta:");
                double consumoEsgoto = double.Parse(Console.ReadLine());
                Console.WriteLine("informe a data de vencimento: (dd/mm/yyyy)");
                DateOnly dateOnly = DateOnly.Parse(Console.ReadLine());
                ContaAgua conta = new ContaAgua(consumoAgua, consumoEsgoto, dateOnly);
                conta.cliente = client;
                client.contas.Add(conta);
                db.SaveChanges();
            }
           Principal.Main(user); // Volta ao menu principal
        }
       public static void CriarCliente()
        {
            using (var db = new ProjetoDbContext())
            {
                Console.WriteLine("Digite o nome do cliente:");
                string nome = Console.ReadLine();
                Console.WriteLine("Digite o CPF/CNPJ do cliente:");
                string ID = Console.ReadLine();
                Console.WriteLine("informe a Senha:");
                string ps = Console.ReadLine();

                Cliente_ novoCliente = new Cliente_(nome, ID, ps);

                db.Add(novoCliente);
                db.SaveChanges();
            Console.WriteLine("Cliente criado com sucesso!");
            Principal.Main(novoCliente);
            }

        }

    }
}
