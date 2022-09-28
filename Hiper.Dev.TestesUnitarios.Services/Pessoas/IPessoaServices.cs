using Hiper.Dev.TestesUnitarios.Domain.Pessoas;
using Hiper.Dev.TestesUnitarios.Services.Dtos;

namespace Hiper.Dev.TestesUnitarios.Services.Pessoas
{
    public interface IPessoaServices
    {
        Task<PessoaDto> AddAsync(PessoaDto dto);

        double GetIdadeMediaAsync(ICollection<Pessoa> pessoas);

        Task<PessoaDto> UpdateAsync(PessoaDto dto);
    }
}