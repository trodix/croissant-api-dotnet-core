using System.Collections.Generic;

namespace CroissantApi.Resources
{
    public class TeamWithUsersResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserLightResource> Users { get; set; }
        public ICollection<TeamRuleResource> TeamRules { get; set; }
    }
}