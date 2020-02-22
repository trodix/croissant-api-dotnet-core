using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CroissantApi.Models
{
    public class Rule
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Range(1, 10)]
        public int CoinsCapacity { get; set; }

        public ICollection<Team> Teams { get; set; }

        public ICollection<UserRule> UserRules { get; set; }
    }
}