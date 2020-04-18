using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CroissantApi.Models
{
    public class SaveRuleResource
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Range(1, 10)]
        public int CoinsCapacity { get; set; }
    }
}