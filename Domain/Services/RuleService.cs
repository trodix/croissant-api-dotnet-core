using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Repositories;
using CroissantApi.Domain.Services;
using CroissantApi.Domain.Services.Communication;
using CroissantApi.Models;

namespace CroissantApi.Services
{
    public class RuleService : IRuleService
    {
        private readonly IRuleRepository _ruleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RuleService(IRuleRepository ruleRepository, IUnitOfWork unitOfWork)
        {
            this._ruleRepository = ruleRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Rule>> ListAsync()
        {
            return await _ruleRepository.ListAsync();
        }

        public async Task<Rule> FindAsync(int id)
        {
            return await _ruleRepository.FindByIdAsync(id);
        }

        public async Task<RuleResponse> SaveAsync(Rule rule)
        {
            try
            {
                await _ruleRepository.AddAsync(rule);
                await _unitOfWork.CompleteAsync();

                return new RuleResponse(rule);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new RuleResponse($"An error occurred when saving the rule: {ex.Message}");
            }
        }

        public async Task<RuleResponse> UpdateAsync(int id, Rule rule)
        {
            var existingRule = await _ruleRepository.FindByIdAsync(id);

            if (existingRule == null)
            {
                return new RuleResponse("Rule not found.");
            }

            existingRule.Name = rule.Name;
            existingRule.Description = rule.Description;
            existingRule.CoinsCapacity = rule.CoinsCapacity;

            try
            {
                _ruleRepository.Update(existingRule);
                await _unitOfWork.CompleteAsync();

                return new RuleResponse(existingRule);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new RuleResponse($"An error occurred when updating the rule: {ex.Message}");
            }
        }

        public async Task<RuleResponse> DeleteAsync(int id)
        {
            var existingRule = await _ruleRepository.FindByIdAsync(id);

            if (existingRule == null)
                return new RuleResponse("Rule not found.");

            try
            {
                _ruleRepository.Remove(existingRule);
                await _unitOfWork.CompleteAsync();

                return new RuleResponse(existingRule);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new RuleResponse($"An error occurred when deleting the rule: {ex.Message}");
            }
        }
    }
}