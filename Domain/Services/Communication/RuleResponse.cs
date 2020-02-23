using CroissantApi.Models;

namespace CroissantApi.Domain.Services.Communication
{
    public class RuleResponse : BaseResponse
    {
        public Rule Rule { get; private set; }

        private RuleResponse(bool success, string message, Rule rule) : base(success, message)
        {
            Rule = rule;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="rule">Saved rule.</param>
        /// <returns>Response.</returns>
        public RuleResponse(Rule rule) : this(true, string.Empty, rule)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public RuleResponse(string message) : this(false, message, null)
        { }
    }
}