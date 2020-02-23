using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Repositories;
using CroissantApi.Models;
using CroissantApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CroissantApi.Persistence.Repositories
{
    public class RuleRepository : BaseRepository, IRuleRepository
    {
        public RuleRepository(CroissantContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Rule>> ListAsync()
        {
            return await _context.Rules
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Rule> FindByIdAsync(int id)
        {
            return await _context.Rules.FindAsync(id);
        }

        public void Update(Rule rule)
        {
            _context.Rules.Update(rule);
        }

        public async Task AddAsync(Rule rule)
        {
            await _context.Rules.AddAsync(rule);
        }

        public void Remove(Rule rule)
        {
            _context.Rules.Remove(rule);
        }
    }
}