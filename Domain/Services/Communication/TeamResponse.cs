using CroissantApi.Models;

namespace CroissantApi.Domain.Services.Communication
{
    public class TeamResponse : BaseResponse
    {
        public Team Team { get; private set; }

        private TeamResponse(bool success, string message, Team team) : base(success, message)
        {
            Team = team;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="team">Saved team.</param>
        /// <returns>Response.</returns>
        public TeamResponse(Team team) : this(true, string.Empty, team)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public TeamResponse(string message) : this(false, message, null)
        { }
    }
}