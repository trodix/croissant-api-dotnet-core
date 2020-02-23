using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CroissantApi.Domain.Repositories;
using CroissantApi.Models;
using CroissantApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CroissantApi.Persistence.Repositories
{
    public class UserRuleRepository : BaseRepository, IUserRuleRepository
    {
        public UserRuleRepository(CroissantContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserRule>> ListAsync()
        {
            return await _context.UserRules.Include(p => p.User).ToListAsync();
        }

        public async Task<UserRule> FindByIdAsync(int userId, int ruleId)
        {
            return await _context.UserRules.FindAsync(userId, ruleId);
        }

        public async Task<IEnumerable<UserRule>> FindByUserIdAsync(int userId)
        {
            return await _context.UserRules
                .Where(ur => ur.UserId == userId)
                .ToListAsync();
        }

        public async Task AddAsync(int userId, UserRule userRule)
        {
            userRule.UserId = userId;
            await _context.UserRules.AddAsync(userRule);
        }

        public void Update(UserRule userRule)
        {
            _context.UserRules.Update(userRule);
        }

        public void Remove(UserRule userRule)
        {
            _context.UserRules.Remove(userRule);
        }
    }
}