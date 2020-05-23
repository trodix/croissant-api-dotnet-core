using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CroissantApi.Resources
{
    public class UpdateTeamResource
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<SaveTeamRuleResource> TeamRules { get; set; }
    }
}