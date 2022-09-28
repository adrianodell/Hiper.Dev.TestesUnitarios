namespace Hiper.Dev.TestesUnitarios.Services.Dtos
{
    public class PessoaDto
    {
        public DateOnly DataDeNascimento { get; set; }

        public ICollection<string> Erros { get; set; }

        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Sexo { get; set; }
    }
}