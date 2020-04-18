using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Repositories;
using CroissantApi.Models;
using CroissantApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CroissantApi.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(CroissantContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Team)
                .Include(u => u.UserRules)
                .ThenInclude(ur => ur.Rule)
                .ToListAsync();
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.Team)
                .Include(u => u.UserRules)
                .ThenInclude(ur => ur.Rule)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(User user)
        {
            _context.Attach(user).State = EntityState.Added;
            await _context.Users.AddAsync(user);
        }

        public void Update(User user)
        {
            _context.Attach(user).State = EntityState.Modified;
            _context.Entry(user).Reference(u => u.Team).IsModified = true;

            _context.Users.Update(user);
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }
    }
}