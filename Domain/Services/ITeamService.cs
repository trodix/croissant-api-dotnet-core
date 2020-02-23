using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Services.Communication;
using CroissantApi.Models;

namespace CroissantApi.Domain.Services
{
    public interface ITeamService
    {
         Task<IEnumerable<Team>> ListAsync();
         Task<Team> FindAsync(int id);
         Task<TeamResponse> SaveAsync(Team team);
         Task<TeamResponse> UpdateAsync(int id, Team team);
         Task<TeamResponse> DeleteAsync(int id);
    }
}