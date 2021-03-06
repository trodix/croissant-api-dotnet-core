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
        private readonly IPaymentRecordService _paymentRecordService;

        public UserService(IUserRepository userRepository, IRuleRepository ruleRepository, ITeamRepository teamRepository, IUnitOfWork unitOfWork, IPaymentRecordService paymentRecordService)
        {
            this._userRepository = userRepository;
            this._ruleRepository = ruleRepository;
            this._teamRepository = teamRepository;
            this._unitOfWork = unitOfWork;
            this._paymentRecordService = paymentRecordService;
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

            if (existingUser.TeamId != existingTeam.Id)
            {
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
                            existingUser.nextPaymentDate = null;
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

        public async Task<UserResponse> UpdateNextPaymentDateAsync(int id, DateTime nextPaymentDate)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);

            if (existingUser == null)
                return new UserResponse("User not found.");

            try
            {
                foreach (var userRule in existingUser.UserRules)
                {
                    foreach (var teamRule in existingUser.Team.TeamRules)
                    {
                        if (userRule.CoinsQuantity >= teamRule.Rule.CoinsCapacity)
                        {
                            existingUser.nextPaymentDate = nextPaymentDate;

                            _userRepository.Update(existingUser);
                            await _unitOfWork.CompleteAsync();

                            return new UserResponse(existingUser);
                        }
                    }
                }

                return new UserResponse("None of the coin quantity for this user is full.");
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new UserResponse($"An error occurred when updating the user's rule: {ex.Message}");
            }
        }

        public async Task<UserResponse> ResetCoinQuantityAsync(int id)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);
            var isUpdatedPaymentRecords = false;

            if (existingUser == null)
                return new UserResponse("User not found.");

            try
            {
                foreach (var userRule in existingUser.UserRules)
                {
                    foreach (var teamRule in existingUser.Team.TeamRules)
                    {
                        if (userRule.RuleId == teamRule.RuleId)
                        {
                            if (userRule.CoinsQuantity >= teamRule.Rule.CoinsCapacity)
                            {
                                DateTime payedAt = DateTime.Now;

                                if (existingUser.nextPaymentDate != null && DateTime.Now.CompareTo(existingUser.nextPaymentDate) > 0)
                                {
                                    payedAt = (DateTime)existingUser.nextPaymentDate;
                                }

                                PaymentRecord paymentRecord = new PaymentRecord
                                {
                                    UserRule = userRule,
                                    PayedAt = payedAt
                                };

                                await _paymentRecordService.SaveAsync(paymentRecord);
                                isUpdatedPaymentRecords = true;


                                userRule.CoinsQuantity = 0;
                                existingUser.nextPaymentDate = GetNextBirthdayDate(existingUser);

                                _userRepository.Update(existingUser);
                                await _unitOfWork.CompleteAsync();
                            }
                        }
                    }
                }

                if (isUpdatedPaymentRecords == true)
                {
                    return new UserResponse(existingUser);
                }

                return new UserResponse("None of the coin quantity for this user is full.");
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
                user.UserRules.Add(new UserRule { User = user, Rule = teamRule.Rule });
            }

            return user.UserRules;
        }

        /// <summary>
        /// Get the next birthday date for a user
        /// </summary>
        private DateTime GetNextBirthdayDate(User user)
        {
            DateTime nextBirthDayDate = new DateTime(DateTime.Now.Year, user.BirthDate.Month, user.BirthDate.Day);

            if (nextBirthDayDate.CompareTo(DateTime.Now) < 0)
                nextBirthDayDate = nextBirthDayDate.AddYears(1);

            return nextBirthDayDate;
        }
    }
}