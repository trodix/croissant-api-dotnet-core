using System.Collections.Generic;
using CroissantApi.Models;

namespace CroissantApi.Resources
{

    public class RuleResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CoinsCapacity { get; set; }

        public ICollection<Team> Teams { get; set; }

        public ICollection<UserRule> UserRules { get; set; }
    }
}