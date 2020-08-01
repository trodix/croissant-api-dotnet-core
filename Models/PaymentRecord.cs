using System;

namespace CroissantApi.Models
{
    public class PaymentRecord
    {
        public int UserRuleId { get; set; }
        public UserRule UserRule { get; set; }
        
        public DateTime PayedAt { get; set; }
    }
}