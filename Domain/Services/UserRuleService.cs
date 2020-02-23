using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Repositories;
using CroissantApi.Domain.Services;
using CroissantApi.Domain.Services.Communication;
using CroissantApi.Models;

namespace CroissantApi.Services
{
    public class UserRuleService : IUserRuleService
    {
        private readonly IUserRuleRepository _userRuleRepository;
        private readonly IUnitOfWork _unitOfWork;
    
        public UserRuleService(IUserRuleRepository userRuleRepository, IUnitOfWork unitOfWork)
        {
            _userRuleRepository = userRuleRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserRule>> ListAsync()
        {
            return await _userRuleRepository.ListAsync();
        }

        public async Task<UserRule> FindAsync(int userId, int ruleId)
        {
            return await _userRuleRepository.FindByIdAsync(userId, ruleId);
        }

        public async Task<IEnumerable<UserRule>> FindByUserIdAsync(int userId)
        {
            return await _userRuleRepository.FindByUserIdAsync(userId);
        }

        public async Task<UserRuleResponse> SaveAsync(int userId, UserRule userRule)
        {
            try
            {
                await _userRuleRepository.AddAsync(userId, userRule);
                await _unitOfWork.CompleteAsync();

                return new UserRuleResponse(userRule);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new UserRuleResponse($"An error occurred when saving the user rule: {ex.Message}");
            }
        }
    }
}