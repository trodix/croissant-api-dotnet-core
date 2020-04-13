using System;
using System.Collections.Generic;

namespace CroissantApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public Team Team { get; set; }

        // public int TeamId { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public DateTime BirthDate { get; set; }

        public ICollection<UserRule> UserRules { get; set; }

    }
}