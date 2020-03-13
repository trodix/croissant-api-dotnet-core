using System.Collections.Generic;

namespace CroissantApi.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<TeamRule> TeamRules { get; set; }
    }
}