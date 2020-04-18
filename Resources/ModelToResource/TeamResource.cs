using System.Collections.Generic;

namespace CroissantApi.Resources
{
    public class TeamResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TeamRuleResource> TeamRules { get; set; }
    }
}