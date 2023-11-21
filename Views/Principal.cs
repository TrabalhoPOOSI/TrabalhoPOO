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
                Servicos.CriarContaLuz(user);
                break;
            case "3":
                Servicos.ConsultarContas(user);
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


    static void ConsultarClientes(Cliente_ user)
    {
        Main(user); // Volta ao menu principal
    }
}
