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
        private readonly ITeamRepository _teamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, ITeamRepository teamRepository, IUnitOfWork unitOfWork)
        {
            this._userRepository = userRepository;
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
    }
}