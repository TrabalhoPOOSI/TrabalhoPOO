using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_POO.Context;
using Trabalho_POO.Enums;
using Trabalho_POO.Interfaces;

namespace Trabalho_POO.Models
{
    public class ContaAgua : Conta, TarifasDagua
    {
        public double COFINS = 0.03;
        public ContaAgua(double leitura, DateOnly vencimento, string endereco) : base(vencimento, endereco)
        {
            this.leitura = leitura;
            this.status = StatusConta.EmAberto;
            using (var db = new ProjetoDbContext())
            {
                this.leituraAnterior = db.ContaAgua.Where(c => c.lançamento < DateTime.Now).Select(c => c.leitura).FirstOrDefault();
            }
            this.consumo = leituraAnterior != null ? (double)(leitura - leituraAnterior) : leitura;
            this.tipo = Tipo_Consumidor.RESIDENCIAL;
        }

        public double TarifaAgua()
        {
            if (this.tipo == Tipo_Consumidor.SOCIAL && consumo < 6)
            {
                return 10.08;
            }
            double consumoAux = consumo;
            double tarifa = 0;
            if (this.tipo == Tipo_Consumidor.RESIDENCIAL)
            {
                if (consumoAux >= 10)
                {
                    tarifa += 10 * 2.241;
                    consumoAux -= 10;
                }
                else
                {
                    tarifa += consumoAux * 2.241;
                    consumoAux = 0;
                }
                if (consumoAux >= 5)
                {
                    tarifa += 5 * 5.447;
                    consumoAux -= 5;
                }
                else
                {
                    tarifa += consumoAux * 5.447;
                    consumoAux = 0;
                }
                if (consumoAux >= 5)
                {
                    tarifa += 5 * 5.461;
                    consumoAux -= 5;
                }
                else
                {
                    tarifa += consumoAux * 5.461;
                    consumoAux = 0;
                }
                if (consumoAux >= 20)
                {
                    tarifa += 20 * 5.487;
                    consumoAux -= 20;
                }
                else
                {
                    tarifa += consumoAux * 5.4871;
                    consumoAux = 0;
                }
                if (consumoAux > 0)
                {
                    tarifa += consumoAux * 10.066;
                    consumoAux = 0;
                }
                return tarifa;
            }
            else
            {
                if (consumoAux > 6)
                {
                    return 25.79;
                }
                if (consumoAux >= 10)
                {
                    tarifa += 10 * 4.299;
                    consumoAux -= 10;
                }
                else
                {
                    tarifa += consumoAux * 4.299;
                    consumoAux = 0;
                }
                if (consumoAux >= 30)
                {
                    tarifa += 30 * 8.221;
                    consumoAux -= 30;
                }
                else
                {
                    tarifa += consumoAux * 8.221;
                    consumoAux = 0;
                }
                if (consumoAux >= 60)
                {
                    tarifa += 60 * 8.288;
                    consumoAux -= 60;
                }
                else
                {
                    tarifa += consumoAux * 8.288;
                    consumoAux = 0;
                }
                if (consumoAux > 0)
                {
                    tarifa += consumoAux * 8.329;
                    consumoAux = 0;
                }
                return tarifa;
            }
        }

        public double TarifaEsgoto()
        {

            if (this.tipo == Tipo_Consumidor.SOCIAL && consumo < 6)
            {
                return 5.05;
            }
            double consumoAux = consumo;
            double tarifa = 0;
            if (this.tipo == Tipo_Consumidor.RESIDENCIAL)
            {
                if (consumoAux >=10)
                {
                    tarifa += 10 * 1.122;
                    consumoAux -= 10;
                }
                else
                {
                    tarifa += consumoAux * 1.122;
                    consumoAux = 0;

                }
                if (consumoAux >=5)
                {
                    tarifa += 5 * 2.724;
                    consumoAux -= 5;
                }
                else
                {
                    tarifa += consumoAux * 2.724;
                    consumoAux = 0;
                }
                if (consumoAux >= 5)
                {
                    tarifa += 5 * 2.731;
                    consumoAux -= 5;
                }
                else
                {
                    tarifa += consumoAux * 2.731;
                    consumoAux = 0;
                }
                if (consumoAux >= 20)
                {
                    tarifa += 20 * 2.744;
                    consumoAux -= 20;
                }
                else
                {
                    tarifa += consumoAux * 2.744;
                    consumoAux = 0;
                }
                if (consumoAux > 0)
                {
                    tarifa += consumoAux * 5.035;
                    consumoAux = 0;
                }
                return tarifa;
            }
            else
            {
                if (consumoAux < 6)
                {
                    return 12.90;
                }
                if (consumoAux >= 10)
                {
                    tarifa += 10 * 2.149;
                    consumoAux -= 10;
                }
                else
                {
                    tarifa += consumoAux * 2.1499;
                    consumoAux = 0;
                }
                if (consumoAux >= 30)
                {
                    tarifa += 30 * 4.111;
                    consumoAux -= 30;
                }
                else
                {
                    tarifa += consumoAux * 4.111;
                    consumoAux = 0;
                }
                if (consumoAux >= 60)
                {
                    tarifa += 60 * 4.144;
                    consumoAux -= 60;
                }
                else
                {
                    tarifa += consumoAux * 4.144;
                    consumoAux = 0;
                }
                if (consumoAux > 0)
                {
                    tarifa += consumoAux * 4.165;
                    consumoAux = 0;
                }
                return tarifa;
            }
        }

        public void calculaTotal()
        {
            this.Subtotal = (decimal)(TarifaEsgoto() + TarifaAgua());
            this.Total = Subtotal + (Subtotal * (decimal)COFINS);
        }
    }
}
