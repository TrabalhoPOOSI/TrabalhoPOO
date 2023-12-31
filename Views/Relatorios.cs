﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_POO.Context;
using Trabalho_POO.Models;

namespace Trabalho_POO.Views
{
    public static class Relatorios
    {

        public static void MenuRelatorios(Cliente_ cliente)
        {
            Console.WriteLine("              ===========================================================                   ");
            Console.WriteLine($"              |           Escolha a opção de realatorio:                |                   ");
            Console.WriteLine("              |           [a] Valor medio pago em contas.               |                   ");
            Console.WriteLine("              |           [b] Maior valor pago.                         |                   ");
            Console.WriteLine("              |           [c] Variação de consumo.                      |                   ");
            Console.WriteLine("              ===========================================================                   ");

            char resp = char.Parse(Console.ReadLine());

            switch (resp)
            {
                case 'a':
                    valorMedio(cliente);
                    break;
                case 'b':
                    maiorConsumoMes(cliente);
                    break;
                case 'c':
                    variacaoConta(cliente);
                    break;
            }
        }
        public static void valorMedio(Cliente_ user)
        {
            using (var db = new ProjetoDbContext())
            {
                decimal valorMedioLuz = 0;
                decimal valorMedioAgua = 0;
                if (db.ContaAgua.Count() > 0)
                {
                    valorMedioAgua = db.ContaAgua.Where(c => c.clienteId == user.Id).Select(c => c.Total).Average();
                }
                if (db.ContaLuz.Count() > 0)
                {
                    valorMedioLuz = db.ContaLuz.Where(c => c.clienteId == user.Id).Where(c => c != null).Select(c => c.Total).Average();
                }

                Console.WriteLine("              ===========================================================                   ");
                Console.WriteLine($"               O valor medio pago nas contas de agua é R$ {valorMedioAgua.ToString("F2")}.");
                Console.WriteLine($"               O valor medio pago nas contas de Luz é R$ {valorMedioLuz.ToString("F2")}.  ");
                Console.WriteLine("              ===========================================================                   ");

                Principal.Main(user);
            }
        }
        public static void maiorConsumoMes(Cliente_ user)
        {
            using (var db = new ProjetoDbContext())
            {
                try
                {
                    ContaAgua contaAgua = db.ContaAgua.OrderByDescending(c => c.consumo).First();
                    ContaLuz contaLuz = db.ContaLuz.OrderByDescending(c => c.consumo).First();
                    Console.WriteLine("              ===========================================================                    ");
                    Console.WriteLine($"               Maior Consomo de agua foi em {contaAgua.lançamento.ToString("dd/MM/yyyy")}.");
                    Console.WriteLine($"               Consomo de agua foi {contaAgua.consumo}.                                   ");
                    Console.WriteLine("              ===========================================================                    ");
                    Console.WriteLine($"               Maior Consomo de luz foi em {contaLuz.lançamento.ToString("dd/MM/yyyy")}.  ");
                    Console.WriteLine($"               Consomo de luz foi {contaLuz.consumo}.                                     ");
                    Console.WriteLine("              ===========================================================                   ");
                }
                catch (Exception ex)
                {
       
                    Console.WriteLine("                      Erro ao procurar o maior valor, certifique-se de ter ao menos uma conta de agua e luz  ");

                }

                finally
                {
                    Principal.Main(user);
                }
            }
        }

        public static void variacaoConta(Cliente_ user)
        {
            Console.WriteLine("              ===========================================================                   ");
            Console.WriteLine("              |              Escolha o tipo de conta:                   |                   ");
            Console.WriteLine("              |              [a] Conta de Agua.                         |                   ");
            Console.WriteLine("              |              [b] Conta de Luz.                          |                   ");
            Console.WriteLine("              ===========================================================                   ");


            char tipoConta = char.Parse(Console.ReadLine());
            Console.WriteLine("              ===========================================================                   ");
            Console.WriteLine("              |Informe dois meses para calcular a variação: (dd/mm/yyyy)|                   ");
            Console.WriteLine("              ===========================================================                   ");

            DateTime data1 = DateTime.Parse(Console.ReadLine());
            DateTime data2 = DateTime.Parse(Console.ReadLine());
            double consumo = 0;
            double valor = 0;
            switch (tipoConta)
            {
                case 'a':
                    using (var db = new ProjetoDbContext())
                    {
                        consumo = db.ContaAgua.Where(c => c.clienteId == user.Id).Where(c => c.lançamento > data1 && c.lançamento < data2).Select(c => c.consumo).Average();
                        valor = (double)db.ContaAgua.Where(c => c.clienteId == user.Id).Where(c => c.lançamento > data1 && c.lançamento < data2).Select(c => c.Total).Average();
                    }
                    break;
                case 'b':
                    using (var db = new ProjetoDbContext())
                    {
                        consumo = db.ContaLuz.Where(c => c.clienteId == user.Id).Where(c => c.lançamento > data1 && c.lançamento < data2).Select(c => c.consumo).Average();
                        valor = (double)db.ContaLuz.Where(c => c.clienteId == user.Id).Where(c => c.lançamento > data1 && c.lançamento < data2).Select(c => c.Total).Average();
                    }
                    break;
            }
            Console.WriteLine("              ===========================================================                   ");
            Console.WriteLine($"                  O consumo medio é de {consumo.ToString("F3")}.                         ");
            Console.WriteLine($"                  Variou em reais R$ {valor.ToString("F2")}.                              ");
            Console.WriteLine("              ===========================================================                   ");


            Principal.Main(user);
        }

    }
}
