using CroissantApi.Models;

namespace CroissantApi
{
    public class TeamRule
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int RuleId { get; set; }
        public Rule Rule { get; set; }
    }
}