using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Repositories;
using CroissantApi.Domain.Services;
using CroissantApi.Models;

namespace CroissantApi.Services
{
    public class UserRuleService : IUserRuleService
    {
        private readonly IUserRuleRepository _userRuleRepository;
    
        public UserRuleService(IUserRuleRepository userRuleRepository)
        {
            _userRuleRepository = userRuleRepository;
        }

        public async Task<IEnumerable<UserRule>> ListAsync()
        {
            return await _userRuleRepository.ListAsync();
        }
    }
}