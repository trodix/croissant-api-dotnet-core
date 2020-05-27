using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Repositories;
using CroissantApi.Domain.Services;
using CroissantApi.Domain.Services.Communication;
using CroissantApi.Models;

namespace CroissantApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRuleRepository _ruleRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IRuleRepository ruleRepository, ITeamRepository teamRepository, IUnitOfWork unitOfWork)
        {
            this._userRepository = userRepository;
            this._ruleRepository = ruleRepository;
            this._teamRepository = teamRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _userRepository.ListAsync();
        }

        public async Task<User> FindAsync(int id)
        {
            return await _userRepository.FindByIdAsync(id);
        }

        public async Task<UserResponse> SaveAsync(User user)
        {

            var existingTeam = await _teamRepository.FindByIdAsync(user.TeamId);

            if (existingTeam == null)
            {
                return new UserResponse("The requested team was not found.");
            }

            user.UserRules = CreateRulesFromTeam(user, existingTeam);

            try
            {
                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new UserResponse($"An error occurred when saving the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateAsync(int id, User user)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);
            var existingTeam = await _teamRepository.FindByIdAsync(user.TeamId);

            if (existingUser == null)
            {
                return new UserResponse("User not found.");
            }

            if (existingTeam == null)
            {
                return new UserResponse("The requested team was not found.");
            }

            existingUser.Lastname = user.Lastname;
            existingUser.Firstname = user.Firstname;
            existingUser.BirthDate = user.BirthDate;
            existingUser.Team = existingTeam;

            if (existingUser.TeamId != existingTeam.Id) {
                // Update the UserRules if the Team change
                existingUser.UserRules = CreateRulesFromTeam(user, existingTeam);
            }
            

            try
            {
                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new UserResponse($"An error occurred when updating the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> IncrementCoinQuantityAsync(int id, int ruleId)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);
            var existingRule = await _ruleRepository.FindByIdAsync(ruleId);

            if (existingUser == null)
                return new UserResponse("User not found.");

            if (existingRule == null)
                return new UserResponse("Rule not found.");

            try
            {
                foreach (var item in existingUser.UserRules)
                {
                    if (item.RuleId == ruleId) 
                    {
                        // Increment the coin counter
                        item.CoinsQuantity += 1;

                        if (item.CoinsQuantity == existingRule.CoinsCapacity)
                        {
                            // TODO add a new record in database to track the payment status (not payed here)

                            // Note: next counter iteration will reset the coin counter (above)
                        }

                        if (item.CoinsQuantity > existingRule.CoinsCapacity)
                        {
                            // Reset the coin counter
                            item.CoinsQuantity = 0;
                            item.nextPaymentDate = null;
                        }
                        
                        _userRepository.Update(existingUser);
                        await _unitOfWork.CompleteAsync();

                        return new UserResponse(existingUser);
                    }
                }

                // Rule not found in user collection rules
                return new UserResponse("User did not subscribed to this rule.");

            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new UserResponse($"An error occurred when deleting the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateNextPaymentDateAsync(int id, int ruleId, DateTime nextPaymentDate)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);
            var existingRule = await _ruleRepository.FindByIdAsync(ruleId);

            if (existingUser == null)
                return new UserResponse("User not found.");

            if (existingRule == null)
                return new UserResponse("Rule not found.");

            try
            {
                foreach (var item in existingUser.UserRules)
                {
                    if (item.RuleId == ruleId)
                    {

                        if (item.CoinsQuantity < existingRule.CoinsCapacity)
                        {
                            return new UserResponse("The coin quantity for this user's rule is lower than the coin capacity of the global rule.");
                        }

                        // TODO Check if already payed for this round

                        item.nextPaymentDate = nextPaymentDate;
                        _userRepository.Update(existingUser);
                        await _unitOfWork.CompleteAsync();

                        return new UserResponse(existingUser);
                    }
                }

                return new UserResponse("User did not subscribed to this rule.");
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new UserResponse($"An error occurred when updating the user's rule: {ex.Message}");
            }
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);

            if (existingUser == null)
                return new UserResponse("User not found.");

            try
            {
                _userRepository.Remove(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new UserResponse($"An error occurred when deleting the user: {ex.Message}");
            }
        }

        private ICollection<UserRule> CreateRulesFromTeam(User user, Team team)
        {
            user.UserRules = new List<UserRule>();

            foreach (var teamRule in team.TeamRules)
            {
                user.UserRules.Add(new UserRule{User = user, Rule = teamRule.Rule});
            }

            return user.UserRules;
        }
    }
}