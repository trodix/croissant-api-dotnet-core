using System;
using System.Collections.Generic;

namespace CroissantApi.Resources
{
    public class UserLightResource
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? nextPaymentDate { get; set; }
        public ICollection<UserRuleWithoutUserResource> UserRules { get; set; }
    }
}