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

        public double GetIdadeMediaAsync(ICollection<Pessoa> pessoas)
        {
            var datas = pessoas.Select(x => x.DataDeNascimento).ToList();
            var idades = new List<int>();

            datas.ForEach(data =>
            {
                idades.Add(CalcularIdade(data));
            });

            return idades.Average();
        }

        public async Task<PessoaDto> UpdateAsync(PessoaDto dto)
        {
            try
            {
                var pessoa = await _pessoasRepository.GetByIdAsync(dto.Id);

                pessoa.SetDataDeNascimento(dto.DataDeNascimento);
                pessoa.SetNome(dto.Nome);
                pessoa.SetSexo(dto.Sexo);

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

        private int CalcularIdade(DateOnly data)
        {
            var hoje = DateOnly.FromDateTime(DateTime.Now);
            var anos = hoje.Year - data.Year;

            if (hoje.Month > data.Month) anos--;

            return anos;
        }
    }
}