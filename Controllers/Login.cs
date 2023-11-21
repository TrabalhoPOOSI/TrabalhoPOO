﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_POO.Context;
using Trabalho_POO.Models;

namespace Trabalho_POO.Controllers
{
    public static class login
    {
        public static void Main()
        {
            using (var db = new ProjetoDbContext())
            {
                Console.WriteLine("Bem-vindo ao Sistema de Login!");

                // Solicitação de entrada do usuário
                Console.Write("Digite o CPF ou CNPJ de usuário: ");
                string userId = Console.ReadLine();
                Cliente_ user = db.Clientes.Where(c => c.Id == userId).FirstOrDefault();
                Console.WriteLine("Bem-vindo, " + user.Nome + ".");
                Console.Write("Digite a senha: ");
                string senhaDigitada = LerSenha();
                // Verificação das credenciais
                if (senhaDigitada == user.password)
                {
                    Principal.Main(user);
                }
                else
                {
                    Console.WriteLine("Login falhou. Verifique suas credenciais.");

                }
            }
        }


        // Método para ler a senha sem exibir caracteres no console
        static string LerSenha()
        {
            string senha = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                // Verifica se a tecla pressionada é uma tecla de caractere
                if (!char.IsControl(key.KeyChar))
                {
                    senha += key.KeyChar;
                    Console.Write("*"); // Exibe um caractere '*' para cada caractere digitado
                }
                // Se a tecla Enter for pressionada, termina o loop
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Pula uma linha após a entrada da senha
            return senha;
        }
    }

}