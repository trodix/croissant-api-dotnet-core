using System;
using System.ComponentModel.DataAnnotations;
using CroissantApi.Models;

namespace CroissantApi.Resources
{
    public class UpdateNextPaymentDateResource
    {   
        [Required]
        [DataType(DataType.Date)]
        public DateTime nextPaymentDate { get; set; }
    }
}