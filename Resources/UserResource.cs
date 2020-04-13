using System;
using System.Collections.Generic;
using CroissantApi.Models;

namespace CroissantApi.Resources
{

    public class UserResource
    {
        public int Id { get; set; }

        public TeamResource Team { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public DateTime BirthDate { get; set; }

        public ICollection<UserRule> UserRules { get; set; }
    }
}