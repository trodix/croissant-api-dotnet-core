using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Models;

namespace CroissantApi.Domain.Services
{
    public interface IUserRuleService
    {
         Task<IEnumerable<UserRule>> ListAsync();
    }
}