using System;

namespace CroissantApi.Models
{
    public class UserRule
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int RuleId { get; set; }
        public Rule Rule { get; set; }
        
        public int CoinsQuantity { get; set; }
    }
}