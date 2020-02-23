using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Models;

namespace CroissantApi.Domain.Repositories
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> ListAsync();
        Task<Team> FindByIdAsync(int id);
        Task AddAsync(Team team);
        void Update(Team team);
        void Remove(Team team);
    }
}