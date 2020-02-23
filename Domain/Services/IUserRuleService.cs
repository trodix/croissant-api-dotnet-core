using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Services.Communication;
using CroissantApi.Models;

namespace CroissantApi.Domain.Services
{
    public interface IUserRuleService
    {
        Task<IEnumerable<UserRule>> ListAsync();
        Task<UserRule> FindAsync(int userId, int ruleId);
        Task<IEnumerable<UserRule>> FindByUserIdAsync(int userId);
        Task<UserRuleResponse> SaveAsync(int userId, UserRule userRule);
        Task<UserRuleResponse> UpdateAsync(int userId, int ruleId, UserRule userRule);
        Task<UserRuleResponse> DeleteByUserIdAsync(int userId, int ruleId);
    }
}