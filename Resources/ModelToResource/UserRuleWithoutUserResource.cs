using System;

namespace CroissantApi.Resources
{
    public class UserRuleWithoutUserResource
    {
        public RuleResource Rule { get; set; }
        public int CoinsQuantity { get; set; }
        public DateTime? nextPaymentDate { get; set; }
    }
}