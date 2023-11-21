using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Trabalho_POO.Context;
using Trabalho_POO.Controllers;
using Trabalho_POO.Models;
using Trabalho_POO.Views;


public static class Principal
{
    public static void Main(Cliente_ user)
    {
        Console.WriteLine($"Bem-vindo, {user.Nome} ao nosso Menu!");
        Console.WriteLine("-------------------------------");
        Console.WriteLine("O que você gostaria de fazer?");
        Console.WriteLine("1. Cadastra uma conta água");
        Console.WriteLine("2. Cadastra uma conta luz");
        Console.WriteLine("3. Consultar contas");
        Console.WriteLine("4. Relatorios personalizados");
        Console.WriteLine("9. Sair");

        string escolha = Console.ReadLine();

        switch (escolha)
        {
            case "1":
                Servicos.CriarContaAgua(user);
                break;
            case "2":
                break;
            case "3":
                ConsultarContas(user);
                break;
            case "4":
                ConsultarClientes(user);
                break;
            case "9":
                Console.WriteLine("Obrigado por visitar nosso app. Até mais!");
                break;
            default:
                Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                break;
        }
    }

    static void ConsultarContas(Cliente_ user)
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
                    var contasLançadas = db.ContaAgua.Where(c=>c.clienteId == user.Id).ToList();
                    Console.WriteLine($"Contas lançadas para o usuario {user.Nome}:");
                    Console.WriteLine(" ID |      Lançamentos    |  Status  | Tarifa |  Consumo (L) | Total ");
                    foreach (var item in contasLançadas)
                    {
        
                        Console.WriteLine($"  {item.Id} | {item.lançamento} | { item.status.ToString()} |  {item.tarifa} | " +
                            $"   {item.Consumo}   | R$ {item.Total} ");
                    }

                }

        break;
            case 'b':
            break;
        case 'c':
            break;
        }

        Main(user); // Volta ao menu principal
    }

    static void ConsultarClientes(Cliente_ user)
    {
        Main(user); // Volta ao menu principal
    }
}
