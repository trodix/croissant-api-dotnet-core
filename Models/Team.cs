using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CroissantApi.Models
{
  public class Team
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<User> Users { get; set; }
  }
}