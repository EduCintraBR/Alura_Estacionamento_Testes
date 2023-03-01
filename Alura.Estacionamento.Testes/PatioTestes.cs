using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using Xunit;

namespace Alura.Estacionamento.Testes
{
    public class PatioTestes
    {
        private Veiculo veiculo;
        private Patio patio;
        private Operador operador;

        public PatioTestes()
        {
            veiculo = new Veiculo();
            operador = new Operador("Pedro Malazarte");
            patio = new Patio(operador);
        }

        [Fact]
        public void ValidaFaturamentoDoEstacionamentoComUmVeiculo()
        {
            //Arrange
            veiculo.Placa = "HJX-5353";
            veiculo.Cor = "Chumbo";
            veiculo.Modelo = "Gol G5";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Proprietario = "Eduardo Cintra";

            patio.RegistrarEntradaVeiculo(veiculo);
            patio.RegistrarSaidaVeiculo(veiculo.Placa);
            //Act
            double faturamento = patio.TotalFaturado();
            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("Eduardo Cintra", "HJX-5353", "Cinza", "Gol G5", TipoVeiculo.Automovel)]
        [InlineData("Sandra Barbosa", "FRJ-2374", "Vermelha", "Ka 2007", TipoVeiculo.Automovel)]
        [InlineData("Eduardo Cintra", "GCM-2255", "Azul", "Ka 98", TipoVeiculo.Automovel)]
        public void ValidaFaturamentoDoEstacionamentoComVariosVeiculos(string proprietario, string placa, string cor, string modelo, TipoVeiculo tipoVeiculo)
        {
            //Arrange
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Tipo = tipoVeiculo;
            veiculo.Proprietario = proprietario;

            patio.RegistrarEntradaVeiculo(veiculo);
            patio.RegistrarSaidaVeiculo(veiculo.Placa);
            //Act
            double faturamento = patio.TotalFaturado();
            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory(DisplayName = "Testa se o veículo existe no pátio")]
        [InlineData("Eduardo Cintra", "HJX-5353", "Cinza", "Gol G5", TipoVeiculo.Automovel)]
        public void PesquisaAPlacaDoVeiculoNoPatioComOsDadosDoVeiculoComoParametro(string proprietario, string placa, string cor, string modelo, TipoVeiculo tipoVeiculo)
        {
            //Arrange
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Tipo = tipoVeiculo;
            veiculo.Proprietario = proprietario;

            patio.RegistrarEntradaVeiculo(veiculo);

            //Act
            var veiculoConsultado = patio.PesquisaVeiculo(veiculo.TicketId);
            //Assert
            Assert.Equal(placa, veiculoConsultado.Placa);
        }

        [Fact]
        public void AlteraOsDadosDoProprioVeiculoEstacionado()
        {
            //Arrange
            var veiculoTemp = new Veiculo();

            veiculo.Placa = "HJX-5353";
            veiculo.Cor = "Chumbo";
            veiculo.Modelo = "Gol G5";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Proprietario = "Eduardo Cintra";

            patio.RegistrarEntradaVeiculo(veiculo);

            veiculoTemp.Placa = "HJX-5353";
            veiculoTemp.Cor = "Chumbo";
            veiculoTemp.Modelo = "Gol G6";
            veiculoTemp.Tipo = TipoVeiculo.Automovel;
            veiculoTemp.Proprietario = "Eduardo Cintra";


            //Act
            patio.AlterarDadosDoVeiculo(veiculoTemp);

            //Assert
            Assert.Equal(veiculo.Placa, veiculoTemp.Placa);
            Assert.Equal(veiculo.Cor, veiculoTemp.Cor);
            Assert.Equal(veiculo.Modelo, veiculoTemp.Modelo);
            Assert.Equal(veiculo.Tipo, veiculoTemp.Tipo);
            Assert.Equal(veiculo.Proprietario, veiculoTemp.Proprietario);
        }
    }
}
