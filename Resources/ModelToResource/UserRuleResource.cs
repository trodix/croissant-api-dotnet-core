namespace CroissantApi.Resources
{
    public class UserRuleResource
    {
        public UserResource User { get; set; }
        public RuleResource Rule { get; set; }
        public int CoinsQuantity { get; set; }
    }
}