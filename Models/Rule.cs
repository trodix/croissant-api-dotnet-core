using System.Collections.Generic;

namespace CroissantApi.Models
{
    public class Rule
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CoinsCapacity { get; set; }

        public ICollection<UserRule> UserRules { get; set; }

        public ICollection<TeamRule> TeamRules { get; set; }
    }
}