using System;
using System.ComponentModel.DataAnnotations;

namespace CroissantApi.Resources
{
    public class SaveUserResource
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Firstname { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}