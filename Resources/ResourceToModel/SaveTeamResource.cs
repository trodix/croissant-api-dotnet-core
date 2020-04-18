using System.ComponentModel.DataAnnotations;

namespace CroissantApi.Resources
{
    public class SaveTeamResource
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
    }
}