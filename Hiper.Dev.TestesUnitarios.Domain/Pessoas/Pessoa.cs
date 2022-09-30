namespace Hiper.Dev.TestesUnitarios.Domain.Pessoas
{
    public class Pessoa
    {
        public const string ErroDataDeNascimentoInvalida = "Data de nascimento inválida.";
        public const string ErroIdInvalido = "Id inválido.";
        public const string ErroNomeMaxLength = "Número de carácteres do nome maior que ";
        public const string ErroNomeNaoInformado = "Nome não informado.";
        public const string ErroRendaMensalInvalida = "Renda mensal inválida.";
        public const string ErroSexoInvalido = "Sexo inválido.";
        public const string ErroSexoMaxLength = "Número de carácteres do sexo maior que ";
        public const int NomeMaxLength = 100;
        public const int SexoMaxLength = 1;
        private readonly ICollection<string> SexosValidos = new List<string>() { "M", "F" };

        private ICollection<string> Erros = new List<string>();

        public Pessoa(DateOnly dataDeNascimento, Guid id, string nome, string sexo, decimal rendaMensal)
        {
            SetDataDeNascimento(dataDeNascimento);
            SetId(id);
            SetNome(nome);
            SetSexo(sexo);
            SetRendaMensal(rendaMensal);
        }

        public DateOnly DataDeNascimento { get; private set; }

        public Guid Id { get; private set; }

        public string Nome { get; private set; }

        public decimal RendaMensal { get; private set; }

        public string Sexo { get; private set; }

        public void AddErro(string erro)
        {
            Erros ??= new List<string>();
            Erros.Add(erro);
        }

        public ICollection<string> GetErros() => Erros;

        public bool IsValid() => !Erros.Any();

        public void SetDataDeNascimento(DateOnly dataDeNascimento)
        {
            if (dataDeNascimento == DateOnly.MinValue || dataDeNascimento > DateOnly.FromDateTime(DateTime.Now))
            {
                AddErro(ErroDataDeNascimentoInvalida);
                return;
            }

            DataDeNascimento = dataDeNascimento;
        }

        public void SetId(Guid id)
        {
            if (id == Guid.Empty)
            {
                AddErro(ErroIdInvalido);
                return;
            }

            Id = id;
        }

        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                AddErro(ErroNomeNaoInformado);
                return;
            }

            if (nome.Length > NomeMaxLength)
            {
                AddErro($"{ErroNomeMaxLength}{NomeMaxLength}");
                return;
            }

            Nome = nome;
        }

        public void SetRendaMensal(decimal rendaMensal)
        {
            if (rendaMensal < 0)
            {
                AddErro(ErroRendaMensalInvalida);
                return;
            }

            RendaMensal = rendaMensal;
        }

        public void SetSexo(string sexo)
        {
            if (sexo.Length > SexoMaxLength)
            {
                AddErro($"{ErroSexoMaxLength}{SexoMaxLength}");
                return;
            }

            if (!SexosValidos.Contains(sexo))
            {
                AddErro(ErroSexoInvalido);
                return;
            }

            Sexo = sexo;
        }
    }
}