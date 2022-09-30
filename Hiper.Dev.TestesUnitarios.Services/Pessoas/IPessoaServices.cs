using Hiper.Dev.TestesUnitarios.Services.Dtos;

namespace Hiper.Dev.TestesUnitarios.Services.Pessoas
{
    public interface IPessoaServices
    {
        Task<PessoaDto> AddAsync(PessoaDto dto);

        double GetIdadeMediaAsync(ICollection<DateOnly> datas);

        Task<int> ReajustarRendaMensal(decimal percentual, string nome, string sexo, int? idadeMinima);

        Task<PessoaDto> UpdateAsync(PessoaDto dto);
    }
}