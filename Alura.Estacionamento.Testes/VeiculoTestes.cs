using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using Xunit;

namespace Alura.Estacionamento.Testes
{
    public class VeiculoTestes
    {
        private Veiculo veiculo;
        public VeiculoTestes()
        {
            veiculo = new Veiculo();
        }

        [Fact]
        public void TestaVeiculoAcelerarComParametro10()
        {
            //Arrange
            //Act
            veiculo.Acelerar(10);
            //Assert
            Assert.Equal(100, veiculo.VelocidadeAtual);
        }

        [Fact]
        public void TestaVeiculoFrearComParametro10()
        {
            //Arrange
            //Act
            veiculo.Frear(10);
            //Assert
            Assert.Equal(-150, veiculo.VelocidadeAtual);
        }

        [Fact]
        public void TestaEConfirmaOTipoDoVeiculo()
        {
            //Arrange
            //Act
            //Assert
            Assert.Equal(TipoVeiculo.Automovel, veiculo.Tipo);
        }

        [Fact]
        public void ImprimeAFichaDeInformacaoDoVeiculoEstacionado()
        {
            //Arrange
            veiculo.Placa = "HJX-5353";
            veiculo.Cor = "Chumbo";
            veiculo.Modelo = "Gol G5";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Proprietario = "Eduardo Cintra";

            //Act
            var ficha = veiculo.ToString();

            //Assert
            Assert.Contains("Proprietário: Eduardo Cintra", ficha);
        }

        [Fact]
        public void TestaNomeProprietarioVeiculoComMenosDeTresCaracteres()
        {
            //Arrange
            string nomeProprietario = "Ed";
            //Assert
            Assert.Throws<FormatException>(
                //Act
                () => new Veiculo(nomeProprietario)
            );
        }

        [Fact]
        public void TestaTipoDaExcecaoQuandoHaErroAoAdicionarUmVeiculo()
        {
            //Arrange
            string placaVeiculo = "DRE-552";

            //Assert
            Assert.Throws<FormatException>(
                //Act
                () => new Veiculo().Placa = placaVeiculo
            );
        }

        [Fact]
        public void TestaMensagemDeExcecaoQuandoPlacaVeiculoTemMenosDeOitoCaracteres()
        {
            //Arrange
            string placaVeiculo = "DRE-552";

            //Act
            var validacao = Assert.Throws<FormatException>(
                () => new Veiculo().Placa = placaVeiculo
            );

            //Assert
            Assert.Equal("A placa deve possuir 8 caracteres", validacao.Message);
        }

        [Fact]
        public void TestaMensagemDeExcecaoDoQuartoCaractereDaPlaca()
        {
            //Arrange
            string placaVeiculo = "DRE15523";

            //Act
            var validacao = Assert.Throws<FormatException>(
                () => new Veiculo().Placa = placaVeiculo
            );

            //Assert
            Assert.Equal("O 4° caractere deve ser um hífen", validacao.Message);
        }

        [Fact]
        public void TestaMensagemDeExcecaoNaValidacaoDosTresPrimeirosCaracteres()
        {
            //Arrange
            string placaVeiculo = "D12-5523";

            //Act
            var validacao = Assert.Throws<FormatException>(
                () => new Veiculo().Placa = placaVeiculo
            );

            //Assert
            Assert.Equal("Os 3 primeiros caracteres devem ser letras!", validacao.Message);
        }
    }
}