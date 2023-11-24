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
                        Console.WriteLine(" ID  |      Lançamentos    |  Status   |  Consumo (M3)  | Total ");
                        foreach (var item in contasLançadas)
                        {
                            Console.WriteLine($"  {item.Id.ToString().PadRight(2)} | " +
                                              $"{item.lançamento} | " +
                                              $"{item.status.ToString().PadRight(9)} | " +
                                              $"{item.consumo.ToString().PadRight(14)} | " +
                                              $"R$ {item.Total}");
                        }

                    }
                    break;

                case 'b':

                    using (var db = new ProjetoDbContext())
                    {
                        Console.WriteLine($"Contas lançadas para o usuario {user.Nome}:");
                        var contasLançadas = db.ContaLuz.Where(c => c.clienteId == user.Id).ToList();
                        Console.WriteLine(" ID  |      Lançamentos    |  Status   |  Consumo (M3)  | Total ");
                        foreach (var item in contasLançadas)
                        {
                            Console.WriteLine($"  {item.Id.ToString().PadRight(2)} | " +
                                              $"{item.lançamento} | " +
                                              $"{item.status.ToString().PadRight(9)} | " +
                                              $"{item.consumo.ToString().PadRight(14)} | " +
                                              $"R$ {item.Total}");
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
                Console.WriteLine("Informe o consumo de agua da conta (M3):");
                double consumoAgua = double.Parse(Console.ReadLine());
                Console.WriteLine("informe a data de vencimento: (dd/mm/yyyy)");
                DateOnly dateOnly = DateOnly.Parse(Console.ReadLine());
                ContaAgua conta = new ContaAgua(consumoAgua, dateOnly);
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
                conta.enderecoId = escolherEnderecos(client).id;
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
                ContaLuz conta = new ContaLuz(consumoLuz, dateOnly);

                conta.tipo = tipo;
                conta.calculaTotal();
                conta.cliente = client;
                conta.enderecoId = escolherEnderecos(client).id;
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
                novoCliente.enderecos.Add(CriarEndereco());

                db.Add(novoCliente);

                db.SaveChanges();
                Console.WriteLine("Cliente criado com sucesso!");
                Principal.Main(novoCliente);
            }

        }

        public static Enderecos escolherEnderecos(Cliente_ cliente)
        {
            using (var db = new ProjetoDbContext())
            {
                Console.WriteLine("Lista de endereços cadastrados: ");
                List<Enderecos> enderecos = db.Clientes.Where(c => c.Equals(cliente)).Select(c => c.enderecos).FirstOrDefault().ToList();
                Console.WriteLine("ID | Logradouro                     | Numero | Bairro");
                foreach (var item in enderecos)
                {
                    Console.WriteLine($"{item.id.ToString().PadRight(2)} | {item.logradouro.PadRight(30)} | {item.numero.ToString().PadRight(6)} | {item.bairro}");
                }
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("[a] Escolher um endereço;");
                Console.WriteLine("[b] Criar um novo endereço;");
                char resp = char.Parse(Console.ReadLine());
                switch (resp)
                {
                    case 'a':
                        Console.WriteLine("Informe o id do endereço desejado:");
                        int idEndereco = int.Parse(Console.ReadLine());
                        return enderecos.Where(e => e.id == idEndereco).FirstOrDefault();
                        break;
                    case 'b':
                        return CriarEndereco(cliente);
                        break;

                    default:
                        return CriarEndereco(cliente);
                        break;
                }
            }

        }

        public static Enderecos CriarEndereco()
        {
            Console.WriteLine("Informe o logradouro:");
            string logradouro = Console.ReadLine();

            Console.WriteLine("Informe o Numero:");
            int numero = int.Parse(Console.ReadLine());

            Console.WriteLine("Informe o bairro:");
            string bairro = Console.ReadLine();
            Enderecos enderecos = new Enderecos { numero = numero, bairro = bairro, logradouro = logradouro };
            return enderecos;

        }
        public static Enderecos CriarEndereco(Cliente_ cliente)
        {
            Console.WriteLine("Informe o logradouro:");
            string logradouro = Console.ReadLine();

            Console.WriteLine("Informe o Numero:");
            int numero = int.Parse(Console.ReadLine());

            Console.WriteLine("Informe o bairro:");
            string bairro = Console.ReadLine();
            Enderecos enderecos = new Enderecos { numero = numero, bairro = bairro, logradouro = logradouro, cliente = cliente };
            using (var db = new ProjetoDbContext())
            {
                db.Add(enderecos);
            }
            return enderecos;

        }
    }
}
