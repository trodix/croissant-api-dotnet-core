using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CroissantApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public Team Team { get; set; }

        public int TeamId { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Firstname { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public ICollection<UserRule> UserRules { get; set; }

    }
}