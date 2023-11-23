using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_POO.Context;
using Trabalho_POO.Enums;
using Trabalho_POO.Models;
using Trabalho_POO.Views;

namespace Trabalho_POO.Controllers
{
    public static class Servicos
    {

        public static void ConsultarContas(Cliente_ user)
        {
            Console.WriteLine("Escolha a opção desejada:");
            Console.WriteLine("[a] Contas de agua lançadas;");
            Console.WriteLine("[b] Contas de luz lançadas;");
            Console.WriteLine("[c] Todas as contas lançadas;");
            char resp = char.Parse(Console.ReadLine());
            switch (resp)
            {
                case 'a': 
                    using (var db = new ProjetoDbContext())
                    {
                        var contasLançadas = db.ContaAgua.Where(c => c.clienteId == user.Id).ToList();
                        Console.WriteLine($"Contas lançadas para o usuario {user.Nome}:");
                        Console.WriteLine(" ID |      Lançamentos    |  Status  |  consumo (L) | Total ");
                        foreach (var item in contasLançadas)
                        {

                            Console.WriteLine($"  {item.Id} | {item.lançamento} | {item.status.ToString()} |  " +
                                $"   {item.consumo}   | R$ {item.Total} ");
                        }
                    }
                    break;

                case 'b':

                    using (var db = new ProjetoDbContext())
                    {
                        Console.WriteLine($"Contas lançadas para o usuario {user.Nome}:");
                        var contasLançadas = db.ContaLuz.Where(c => c.clienteId == user.Id).ToList();
                        Console.WriteLine(" ID |      Lançamentos    |  Status  |  consumo (L) | Total ");
                        foreach (var item in contasLançadas)
                        {

                            Console.WriteLine($"  {item.Id} | {item.lançamento} | {item.status.ToString()} |  " +
                                $"   {item.consumo}   | R$ {item.Total} ");
                        }

                    }
                    break;
                case 'c':
                    break;
            }

            Principal.Main(user); // Volta ao menu principal
        }
        public static void CriarContaAgua(Cliente_ user)
        {
            using (var db = new ProjetoDbContext())
            {
                var client = db.Clientes.Where(c => c.Id == user.Id).FirstOrDefault();
                Console.WriteLine("Informe o consumo de agua da conta:");
                double consumoAgua = double.Parse(Console.ReadLine());
                Console.WriteLine("Informe o endereço da conta:");
                string endereco = Console.ReadLine();
                Console.WriteLine("informe a data de vencimento: (dd/mm/yyyy)");
                DateOnly dateOnly = DateOnly.Parse(Console.ReadLine());
                ContaAgua conta = new ContaAgua(consumoAgua, dateOnly, endereco);
                Console.WriteLine("Qual o tipo de estabelecimento?");
                Console.WriteLine("[a] Residencial. ");
                Console.WriteLine("[b] Comercial. ");
                char resp = char.Parse(Console.ReadLine());
                Tipo_Consumidor tipo;
                switch (resp)
                {
                    case 'a':
                        tipo = Tipo_Consumidor.RESIDENCIAL;

                        break;
                    case 'b':
                        tipo = Tipo_Consumidor.COMERCIAL;
                        break;
                    case 'c':
                        tipo = Tipo_Consumidor.SOCIAL;
                        break;

                    default:
                        tipo = Tipo_Consumidor.RESIDENCIAL;
                        break;
                }
                conta.tipo = tipo;
                conta.calculaTotal();
                conta.cliente = client;
                client.contas.Add(conta);
                db.SaveChanges();
            }
            Principal.Main(user); // Volta ao menu principal
        }
        public static void CriarContaLuz(Cliente_ user)
        {
            using (var db = new ProjetoDbContext())
            {
                var client = db.Clientes.Where(c => c.Id == user.Id).FirstOrDefault();
                Console.WriteLine("Informe o consumo de Luz da conta:");
                double consumoLuz = double.Parse(Console.ReadLine());
                Console.WriteLine("Informe o endereço a qual a conta se destina:");
                string endereco = Console.ReadLine();
                Console.WriteLine("Qual o tipo de estabelecimento?");
                Console.WriteLine("[a] Residencial. ");
                Console.WriteLine("[b] Comercial. ");
                char resp = char.Parse(Console.ReadLine());
                Tipo_Consumidor tipo;
                switch (resp)
                {
                    case 'a':
                        tipo = Tipo_Consumidor.RESIDENCIAL;

                        break;
                    case 'b':
                        tipo = Tipo_Consumidor.COMERCIAL;
                        break;

                    default:
                        tipo = Tipo_Consumidor.RESIDENCIAL;
                        break;
                }

                Console.WriteLine("informe a data de vencimento: (dd/mm/yyyy)");
                DateOnly dateOnly = DateOnly.Parse(Console.ReadLine());
                ContaLuz conta = new ContaLuz(consumoLuz, dateOnly, endereco);
                conta.tipo = tipo;
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
