using CroissantApi.Models;

namespace CroissantApi.Domain.Services.Communication
{
    public class UserRuleResponse : BaseResponse
    {
        public UserRule UserRule { get; private set; }

        private UserRuleResponse(bool success, string message, UserRule userRule) : base(success, message)
        {
            UserRule = userRule;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="userRule">Saved user rule.</param>
        /// <returns>Response.</returns>
        public UserRuleResponse(UserRule userRule) : this(true, string.Empty, userRule)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public UserRuleResponse(string message) : this(false, message, null)
        { }
    }
}