using System;
using System.Collections.Generic;
using CroissantApi.Models;

namespace CroissantApi.Resources
{

    public class TeamWithUsersResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}