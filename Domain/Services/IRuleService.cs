using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Services.Communication;
using CroissantApi.Models;

namespace CroissantApi.Domain.Services
{
    public interface IRuleService
    {
         Task<IEnumerable<Rule>> ListAsync();
         Task<Rule> FindAsync(int id);
         Task<RuleResponse> SaveAsync(Rule rule);
         Task<RuleResponse> UpdateAsync(int id, Rule rule);
         Task<RuleResponse> DeleteAsync(int id);
    }
}