namespace Hiper.Dev.TestesUnitarios.Domain.Pessoas.Builders
{
    public class PessoaBuilder
    {
        private DateOnly DataDeNascimento;
        private Guid Id;
        private string Nome;
        private decimal RendaMensal;
        private string Sexo;

        public Pessoa Build() => new Pessoa(DataDeNascimento, Id, Nome, Sexo, RendaMensal);

        public PessoaBuilder WithDataDeNascimento(DateOnly dataDeNascimento)
        {
            DataDeNascimento = dataDeNascimento;
            return this;
        }

        public PessoaBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }

        public PessoaBuilder WithNome(string nome)
        {
            Nome = nome;
            return this;
        }

        public PessoaBuilder WithRendaMensal(decimal rendaMensal)
        {
            RendaMensal = rendaMensal;
            return this;
        }

        public PessoaBuilder WithSexo(string sexo)
        {
            Sexo = sexo;
            return this;
        }
    }
}