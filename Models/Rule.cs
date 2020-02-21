using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CroissantApi.Models
{
  public class Rule
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int CoinsCapacity { get; set; }

    public ICollection<Team> Teams { get; set; }

    public ICollection<UserRule> UserRules { get; set; }
  }
}