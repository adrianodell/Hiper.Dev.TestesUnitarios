using Bogus;
using Hiper.Dev.TestesUnitarios.Domain.Pessoas;
using Hiper.Dev.TestesUnitarios.Domain.Pessoas.Builders;

namespace Hiper.Dev.TestesUnitarios.Tests
{
    public class PessoaBuilderTests
    {
        private readonly Faker _faker;

        public PessoaBuilderTests()
        {
            _faker = new Faker("pt_BR");
        }

        private static IEnumerable<object[]> Parametros
        {
            get
            {
                yield return new object[]
                {
                    DateOnly.MinValue,
                    Guid.Empty,
                    string.Empty,
                    (decimal)-1,
                    "X",
                    Pessoa.ErroDataDeNascimentoInvalida,
                    Pessoa.ErroIdInvalido,
                    Pessoa.ErroNomeNaoInformado,
                    Pessoa.ErroRendaMensalInvalida,
                    Pessoa.ErroSexoInvalido
                };

                yield return new object[]
                {
                    DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    Guid.Empty,
                    "MADFKMDAFKDKAFMDKAMFKASDMFKASDMFKDASMFKASDMFKDASMFKASDMFKASDMFKDASMFKASMDFKASMDFKDASMFKASMDFKDASMFKASDMFKDMASFKDMASKFMDKASMFKDMFA",
                    (decimal)-1,
                    "MF",
                    Pessoa.ErroDataDeNascimentoInvalida,
                    Pessoa.ErroIdInvalido,
                    Pessoa.ErroNomeMaxLength,
                    Pessoa.ErroRendaMensalInvalida,
                    Pessoa.ErroSexoMaxLength
                };
            }
        }

        [Theory(DisplayName = "Criar pessoa com dados inválidos retorna erros"), MemberData(nameof(Parametros))]
        private void CriarPessoaComDadosInvalidosRetornaErros(DateOnly dataDeNascimento, Guid id, string nome, decimal rendaMensal, string sexo,
            string erroDataDeNascimento, string erroId, string erroNome, string erroRenda, string erroSexo)
        {
            var pessoa = new PessoaBuilder()
                .WithNome(nome)
                .WithDataDeNascimento(dataDeNascimento)
                .WithId(id)
                .WithRendaMensal(rendaMensal)
                .WithSexo(sexo)
                .Build();

            Assert.NotNull(pessoa);
            Assert.NotEmpty(pessoa.GetErros());
            Assert.Contains(erroDataDeNascimento, pessoa.GetErros());
            Assert.Contains(erroId, pessoa.GetErros());
            Assert.Contains(erroNome, pessoa.GetErros());
            Assert.Contains(erroRenda, pessoa.GetErros());
            Assert.Contains(erroSexo, pessoa.GetErros());
        }

        [Fact(DisplayName = "Criar pessoa com dados válidos retorna pessoa")]
        private void CriarPessoaComDadosValidosRetornaPessoa()
        {
            var dataDeNascimento = _faker.Date.PastDateOnly(100, DateOnly.FromDateTime(DateTime.Now));
            var id = Guid.NewGuid();
            var nome = _faker.Random.AlphaNumeric(100);
            var rendaMensal = _faker.Random.Decimal(0);
            var sexo = _faker.Random.String2(1, "MF");

            var pessoa = new PessoaBuilder()
                .WithNome(nome)
                .WithDataDeNascimento(dataDeNascimento)
                .WithId(id)
                .WithRendaMensal(rendaMensal)
                .WithSexo(sexo)
                .Build();

            Assert.NotNull(pessoa);
            Assert.True(pessoa.IsValid());
            Assert.Equal(dataDeNascimento, pessoa.DataDeNascimento);
            Assert.Equal(id, pessoa.Id);
            Assert.Equal(nome, pessoa.Nome);
            Assert.Equal(rendaMensal, pessoa.RendaMensal);
            Assert.Equal(sexo, pessoa.Sexo);
        }
    }
}