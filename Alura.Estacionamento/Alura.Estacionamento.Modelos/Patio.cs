using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Estacionamento.Modelos
{
    public class Patio
    {
        private List<Veiculo> _veiculos;
        private Operador _operadorPatio;
        private double _faturado;

        public Operador OperadorPatio { get => _operadorPatio; set => _operadorPatio = value; }
        public double Faturado { get => _faturado; set => _faturado = value; }
        public List<Veiculo> Veiculos { get => _veiculos; set => _veiculos = value; }

        public Patio()
        {
            Faturado = 0;
            _veiculos = new List<Veiculo>();
        }
        public Patio(Operador operador)
        {
            _faturado = 0;
            _veiculos = new List<Veiculo>();
            this.OperadorPatio = operador;
        }
             
        public double TotalFaturado()
        {
            return this.Faturado;
        }

        public string MostrarFaturamento()
        {
            string totalfaturado = String.Format("Total faturado até o momento :::::::::::::::::::::::::::: {0:c}", this.TotalFaturado());
            return totalfaturado;
        }

        public void RegistrarEntradaVeiculo(Veiculo veiculo)
        {
            veiculo.HoraEntrada = DateTime.Now;   
            GerarTicket(veiculo);
            this.Veiculos.Add(veiculo);            
        }

        public string RegistrarSaidaVeiculo(String placa)
        {
            Veiculo procurado = null;
            string informacao=string.Empty;

            foreach (Veiculo v in this.Veiculos)
            {
                if (v.Placa == placa)
                {
                    v.HoraSaida = DateTime.Now;
                    TimeSpan tempoPermanencia = v.HoraSaida - v.HoraEntrada;
                    double valorASerCobrado = 0;
                    if (v.Tipo == TipoVeiculo.Automovel)
                    {
                        /// o método Math.Ceiling(), aplica o conceito de teto da matemática onde o valor máximo é o inteiro imediatamente posterior a ele.
                        /// Ex.: 0,9999 ou 0,0001 teto = 1
                        /// Obs.: o conceito de chão é inverso e podemos utilizar Math.Floor();
                        valorASerCobrado = Math.Ceiling(tempoPermanencia.TotalHours) * 2;

                    }
                    if (v.Tipo == TipoVeiculo.Motocicleta)
                    {
                        valorASerCobrado = Math.Ceiling(tempoPermanencia.TotalHours) * 1;
                    }
                    informacao = string.Format(" Hora de entrada: {0: HH: mm: ss}\n " +
                                             "Hora de saída: {1: HH:mm:ss}\n "      +
                                             "Permanência: {2: HH:mm:ss} \n "       +
                                             "Valor a pagar: {3:c}", v.HoraEntrada, v.HoraSaida, new DateTime().Add(tempoPermanencia), valorASerCobrado);
                    procurado = v;
                    this.Faturado = this.Faturado + valorASerCobrado;
                    break;
                }

            }
            if (procurado != null)
            {
                this.Veiculos.Remove(procurado);
            }
            else
            {
                return "Não encontrado veículo com a placa informada.";
            }

            return informacao;
        }

        public Veiculo PesquisaVeiculo(string ticketId)
        {
            var consulta = (from veiculo in this.Veiculos
                            where veiculo.TicketId == ticketId
                            select veiculo).FirstOrDefault();
            return consulta;
        }

        public Veiculo AlterarDadosDoVeiculo(Veiculo veiculoAlterado)
        {
            var veiculoTemporario = (from veiculo in this.Veiculos
                                 where veiculo.Placa == veiculoAlterado.Placa
                                 select veiculo).FirstOrDefault();

            veiculoTemporario.AlteraDados(veiculoAlterado);

            return veiculoTemporario;
        }

        private void GerarTicket(Veiculo veiculo)
        {
            veiculo.TicketId = Guid.NewGuid().ToString().Substring(0, 5);
            veiculo.Ticket = $"*** Estacionamento Alura ***\n" + 
                             $"Ticket ID: {veiculo.TicketId}\n" + 
                             $"Placa do Veículo: {veiculo.Placa}\n" +
                             $"Hora da Entrada: {veiculo.HoraEntrada}\n" +
                             $"Operador Pátio: {this.OperadorPatio.Nome}\n";
        }
    
    }
}
