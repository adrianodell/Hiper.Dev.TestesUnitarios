using Hiper.Dev.TestesUnitarios.Domain.Pessoas;
using Hiper.Dev.TestesUnitarios.Domain.Pessoas.Builders;
using Hiper.Dev.TestesUnitarios.Repository.Pessoas;
using Hiper.Dev.TestesUnitarios.Services.Dtos;

namespace Hiper.Dev.TestesUnitarios.Services.Pessoas
{
    public class PessoaServices : IPessoaServices
    {
        private readonly IPessoaRepository _pessoasRepository;

        public PessoaServices(IPessoaRepository pessoasRepository)
        {
            _pessoasRepository = pessoasRepository;
        }

        public async Task<PessoaDto> AddAsync(PessoaDto dto)
        {
            try
            {
                var pessoa = new PessoaBuilder()
                    .WithDataDeNascimento(dto.DataDeNascimento)
                    .WithSexo(dto.Sexo)
                    .WithId(dto.Id)
                    .WithNome(dto.Nome)
                    .WithRendaMensal(dto.RendaMensal)
                    .Build();

                if (!pessoa.IsValid())
                {
                    dto.Erros = pessoa.GetErros();
                    return dto;
                }

                await _pessoasRepository.AddAsync(pessoa);
            }
            catch (Exception ex)
            {
                dto.Erros = new List<string> { ex.InnerException?.Message ?? ex.Message };
            }

            return dto;
        }

        public double GetIdadeMediaAsync(ICollection<DateOnly> datas)
        {
            var idades = new List<int>();

            datas.ToList().ForEach(data =>
            {
                idades.Add(CalcularIdade(data));
            });

            return idades.Average();
        }

        public async Task<int> ReajustarRendaMensal(decimal percentual, string nome, string sexo, int? idadeMinima)
        {
            var pessoas = await _pessoasRepository.GetAllAsync(null);

            pessoas = AplicarFiltros(pessoas, nome, sexo, idadeMinima);

            pessoas.ForEach(pessoa =>
            {
                pessoa.SetRendaMensal(pessoa.RendaMensal * ((percentual / 100) + 1));
            });

            return pessoas.Count();
        }

        public async Task<PessoaDto> UpdateAsync(PessoaDto dto)
        {
            try
            {
                var pessoa = await _pessoasRepository.GetByIdAsync(dto.Id);

                pessoa.SetDataDeNascimento(dto.DataDeNascimento);
                pessoa.SetNome(dto.Nome);
                pessoa.SetSexo(dto.Sexo);
                pessoa.SetRendaMensal(dto.RendaMensal);

                if (!pessoa.IsValid())
                {
                    dto.Erros = pessoa.GetErros();
                    return dto;
                }

                await _pessoasRepository.UpdateAsync(pessoa);
            }
            catch (Exception ex)
            {
                dto.Erros = new List<string> { ex.InnerException?.Message ?? ex.Message };
            }

            return dto;
        }

        private List<Pessoa> AplicarFiltros(List<Pessoa> pessoas, string nome, string sexo, int? idadeMinima)
        {
            var dataDeNascimento = DateOnly.FromDateTime(DateTime.Now.AddYears(idadeMinima.Value * -1));

            return pessoas.Where(x =>
                x.Nome == nome
             && x.Sexo == sexo
             && x.DataDeNascimento <= dataDeNascimento).ToList();
        }

        private int CalcularIdade(DateOnly data)
        {
            var hoje = DateOnly.FromDateTime(DateTime.Now);
            var anos = hoje.Year - data.Year;

            if (hoje.Month > data.Month) anos--;

            return anos;
        }
    }
}