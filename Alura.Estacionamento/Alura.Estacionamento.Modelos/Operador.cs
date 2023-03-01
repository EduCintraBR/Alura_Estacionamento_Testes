using System;

namespace Alura.Estacionamento.Alura.Estacionamento.Modelos
{
    public class Operador
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }

        public Operador(string nome)
        {
            this.Matricula = new Guid().ToString().Substring(0, 8);
            this.Nome = nome;
        }

        public override string ToString()
        {
            return $"Operador: {this.Nome}\n" +
                   $"Matricula: {this.Matricula}";
        }
    }
}
