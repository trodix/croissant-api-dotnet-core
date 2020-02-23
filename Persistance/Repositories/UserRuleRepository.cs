using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Repositories;
using CroissantApi.Models;
using CroissantApi.Persistence.Context;
using CroissantApi.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Supermarket.API.Persistence.Repositories
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
    }
}