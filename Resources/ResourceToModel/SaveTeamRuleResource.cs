using System.ComponentModel.DataAnnotations;

namespace CroissantApi.Resources
{
    public class SaveTeamRuleResource
    {
        [Required]
        public int RuleId { get; set; }
    }
}