using Hiper.Dev.TestesUnitarios.Domain.Pessoas;
using Hiper.Dev.TestesUnitarios.Domain.Pessoas.Builders;

namespace Hiper.Dev.TestesUnitarios.Repository.Pessoas
{
    public class PessoaRepository : IPessoaRepository
    {
        public async Task AddAsync(Pessoa pessoa)
        {
        }

        public async Task<List<Pessoa>> GetAllAsync(Func<Pessoa, bool>? where)
        {
            var pessoas = new List<Pessoa>();

            return where is null ? pessoas : pessoas.Where(where).ToList();
        }

        public async Task<Pessoa> GetByIdAsync(Guid id)
        {
            return new PessoaBuilder().Build();
        }

        public async Task RemoveAsync(Pessoa pessoa)
        {
        }

        public async Task UpdateAsync(Pessoa pessoa)
        {
        }
    }
}