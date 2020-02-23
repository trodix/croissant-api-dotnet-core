using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Models;

namespace CroissantApi.Domain.Repositories
{
    public interface IRuleRepository
    {
        Task<IEnumerable<Rule>> ListAsync();
        Task<Rule> FindByIdAsync(int id);
        Task AddAsync(Rule rule);
        void Update(Rule rule);
        void Remove(Rule rule);
    }
}