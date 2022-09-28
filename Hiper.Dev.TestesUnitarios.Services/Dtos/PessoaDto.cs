namespace Hiper.Dev.TestesUnitarios.Services.Dtos
{
    public class PessoaDto
    {
        public ICollection<string> Erros = new List<string>();

        public DateOnly DataDeNascimento { get; set; }

        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Sexo { get; set; }
    }
}