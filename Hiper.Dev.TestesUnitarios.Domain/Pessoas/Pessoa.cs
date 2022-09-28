namespace Hiper.Dev.TestesUnitarios.Domain.Pessoas
{
    public class Pessoa
    {
        public const string ErroDataDeNascimentoInvalida = "Data de nascimento inválida.";
        public const string ErroIdInvalido = "Id inválido.";
        public const string ErroNomeNaoInformado = "Nome não informado.";
        public const string ErroSexoInvalido = "Sexo inválido.";
        private readonly ICollection<string> SexosValidos = new List<string>() { "M", "F" };

        private ICollection<string> Erros = new List<string>();

        public Pessoa(DateOnly dataDeNascimento, Guid id, string nome, string sexo)
        {
            SetDataDeNascimento(dataDeNascimento);
            SetId(id);
            SetNome(nome);
            SetSexo(sexo);
        }

        public DateOnly DataDeNascimento { get; private set; }

        public Guid Id { get; private set; }

        public string Nome { get; private set; }

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

            Nome = nome;
        }

        public void SetSexo(string sexo)
        {
            if (!SexosValidos.Contains(sexo))
            {
                AddErro(ErroSexoInvalido);
                return;
            }

            Sexo = sexo;
        }
    }
}