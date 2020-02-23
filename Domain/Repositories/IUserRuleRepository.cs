using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Models;

namespace CroissantApi.Domain.Repositories
{
    public interface IUserRuleRepository
    {
        Task<IEnumerable<UserRule>> ListAsync();
        Task<UserRule> FindByIdAsync(int userId, int RuleId);
        Task<IEnumerable<UserRule>> FindByUserIdAsync(int userId);
        Task AddAsync(int userId, UserRule userRule);
        void Update(UserRule userRule);
        void Remove(UserRule userRule);
    }
}