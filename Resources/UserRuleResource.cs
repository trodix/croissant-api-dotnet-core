using System.ComponentModel.DataAnnotations;
using CroissantApi.Models;

namespace CroissantApi.Resources
{
    public class UserRuleResource
    {
        public User User { get; set; }

        public Rule Rule { get; set; }

        [Range(1, 10)]
        public int CoinsQuantity { get; set; }
    }
}