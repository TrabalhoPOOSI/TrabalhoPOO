using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Trabalho_POO.Context;
using Trabalho_POO.Models;

public static class Principal
{
    public static void Main()
    {
        Console.WriteLine("Bem-vindo à nossa Landing Page!");
        Console.WriteLine("-------------------------------");
        Console.WriteLine("O que você gostaria de fazer?");
        Console.WriteLine("1. Criar um cliente");
        Console.WriteLine("2. Criar uma conta (luz ou água)");
        Console.WriteLine("3. Consultar contas");
        Console.WriteLine("4. Consultar clientes");
        Console.WriteLine("5. Sair");

        string escolha = Console.ReadLine();

        switch (escolha)
        {
            case "1":
                CriarCliente();
                break;
            case "2":
                CriarContaAgua();
                break;
            case "3":
                ConsultarContas();
                break;
            case "4":
                ConsultarClientes();
                break;
            case "5":
                Console.WriteLine("Obrigado por visitar nosso app. Até mais!");
                break;
            default:
                Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                break;
        }
    }

    static void CriarCliente()
    {
        using (var db = new ProjetoDbContext())
        {
            Console.WriteLine("Digite o nome do cliente:");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o CPF/CNPJ do cliente:");
            string ID = Console.ReadLine();
            Console.WriteLine("informe a Senha:");
            string ps = Console.ReadLine();

            Cliente_ novoCliente = new Cliente_(nome, ID,ps);

            db.Add(novoCliente);
            db.SaveChanges();
        }

        Console.WriteLine("Cliente criado com sucesso!");

        Main(); // Volta ao menu principal
    }

    static void CriarContaAgua()
    {
        using (var db = new ProjetoDbContext())
        {
            
            Console.WriteLine("Digite o CPF/CNPJ do cliente:");
            string CPFCNPJ = Console.ReadLine();
            Cliente_ novoCliente = db.Clientes.Where(c => c.Id == CPFCNPJ).FirstOrDefault();
            if(novoCliente == null)
            {
                Console.WriteLine("Cliente não encontrado");
                Main();
            }

            var client = db.Clientes.Where(c => c.Id == CPFCNPJ).FirstOrDefault();

           // 
            Console.WriteLine("Informe o consumo de agua da conta:");
            double consumoAgua = double.Parse(Console.ReadLine());
            Console.WriteLine("Informe o consumo de esgoto da conta:");
            double consumoEsgoto = double.Parse(Console.ReadLine());
            Console.WriteLine("informe a data de vencimento: (dd/mm/yyyy)");
            DateOnly dateOnly = DateOnly.Parse(Console.ReadLine());

            ContaAgua conta = new ContaAgua(consumoAgua, consumoEsgoto,dateOnly);
            conta.cliente= novoCliente;
            
           client.contas.Add(conta);


            
            db.SaveChanges();
        }
        Main(); // Volta ao menu principal
    }

    static void ConsultarContas()
    {
        Main(); // Volta ao menu principal
    }

    static void ConsultarClientes()
    {
        Main(); // Volta ao menu principal
    }
}

