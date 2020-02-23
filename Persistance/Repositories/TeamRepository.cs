using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Repositories;
using CroissantApi.Models;
using CroissantApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CroissantApi.Persistence.Repositories
{
    public class TeamRepository : BaseRepository, ITeamRepository
    {
        public TeamRepository(CroissantContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Team>> ListAsync()
        {
            return await _context.Teams
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Team> FindByIdAsync(int id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public void Update(Team team)
        {
            _context.Teams.Update(team);
        }

        public async Task AddAsync(Team team)
        {
            await _context.Teams.AddAsync(team);
        }

        public void Remove(Team team)
        {
            _context.Teams.Remove(team);
        }
    }
}