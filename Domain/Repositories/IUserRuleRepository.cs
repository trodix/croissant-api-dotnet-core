using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Models;

namespace CroissantApi.Domain.Repositories
{
    public interface IUserRuleRepository
    {
         Task<IEnumerable<UserRule>> ListAsync();
    }
}