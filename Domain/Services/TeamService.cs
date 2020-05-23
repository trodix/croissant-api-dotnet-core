using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CroissantApi.Domain.Repositories;
using CroissantApi.Domain.Services;
using CroissantApi.Domain.Services.Communication;
using CroissantApi.Models;

namespace CroissantApi.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IRuleRepository _ruleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TeamService(ITeamRepository teamRepository, IRuleRepository ruleRepository, IUnitOfWork unitOfWork)
        {
            this._teamRepository = teamRepository;
            this._ruleRepository = ruleRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Team>> ListAsync()
        {
            return await _teamRepository.ListAsync();
        }

        public async Task<Team> FindAsync(int id)
        {
            return await _teamRepository.FindByIdAsync(id);
        }

        public async Task<TeamResponse> SaveAsync(Team team)
        {
            try
            {
                await _teamRepository.AddAsync(team);
                await _unitOfWork.CompleteAsync();

                return new TeamResponse(team);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new TeamResponse($"An error occurred when saving the team: {ex.Message}");
            }
        }

        public async Task<TeamResponse> UpdateAsync(int id, Team team)
        {
            var existingTeam = await _teamRepository.FindByIdAsync(id);

            if (existingTeam == null)
            {
                return new TeamResponse("Team not found.");
            }

            existingTeam.Name = team.Name;

            List<TeamRule> teamRules = new List<TeamRule>();

            foreach(TeamRule tr in team.TeamRules)
            {
                var existingRule = await _ruleRepository.FindByIdAsync(tr.RuleId);

                if (existingRule == null)
                {
                    return new TeamResponse("Rule not found.");
                }

                teamRules.Add(new TeamRule{Team = existingTeam, Rule = existingRule});
            }

            existingTeam.TeamRules = teamRules;

            try
            {
                _teamRepository.Update(existingTeam);
                await _unitOfWork.CompleteAsync();

                return new TeamResponse(existingTeam);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new TeamResponse($"An error occurred when updating the team: {ex.Message}");
            }
        }

        public async Task<TeamResponse> DeleteAsync(int id)
        {
            var existingTeam = await _teamRepository.FindByIdAsync(id);

            if (existingTeam == null)
                return new TeamResponse("Team not found.");

            try
            {
                _teamRepository.Remove(existingTeam);
                await _unitOfWork.CompleteAsync();

                return new TeamResponse(existingTeam);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new TeamResponse($"An error occurred when deleting the team: {ex.Message}");
            }
        }
    }
}