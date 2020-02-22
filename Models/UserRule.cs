using System.ComponentModel.DataAnnotations;

namespace CroissantApi.Models
{
    public class UserRule
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int RuleId { get; set; }

        public Rule Rule { get; set; }

        [Range(1, 10)]
        public int CoinsQuantity { get; set; }
    }
}