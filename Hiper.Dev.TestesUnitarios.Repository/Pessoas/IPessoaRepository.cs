using Hiper.Dev.TestesUnitarios.Domain.Pessoas;

namespace Hiper.Dev.TestesUnitarios.Repository.Pessoas
{
    public interface IPessoaRepository
    {
        Task AddAsync(Pessoa pessoa);

        Task<List<Pessoa>> GetAllAsync(Func<Pessoa, bool> where);

        Task<Pessoa> GetByIdAsync(Guid id);

        Task RemoveAsync(Pessoa pessoa);

        Task UpdateAsync(Pessoa pessoa);
    }
}