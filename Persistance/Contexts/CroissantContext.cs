using CroissantApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CroissantApi.Persistence.Context
{
    public class CroissantContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRule> UserRules { get; set; }
        public DbSet<Rule> Rules { get; set; }

        public CroissantContext(DbContextOptions<CroissantContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamRule>().HasKey(t => new { t.TeamId, t.RuleId });
            modelBuilder.Entity<UserRule>().HasKey(u => new { u.UserId, u.RuleId });

            modelBuilder.Entity<User>().HasOne<Team>(u => u.Team).WithMany(t => t.Users).HasForeignKey(u => u.TeamId);


            modelBuilder.Entity<Rule>().HasData
            (
                new Rule { 
                    Id = 1,
                    Name = "Ton ordi !", 
                    Description = "Fais un Win+L sinon tu paie les croissants, tu as 1 chance !!!",
                    CoinsCapacity = 1
                },
                new Rule { 
                    Id = 2,
                    Name = "La chaise !", 
                    Description = "Quand tu as fini la journée, tu met ta chaise sur ta table, sinon tu paie les croissants. Tu as 3 chances.",
                    CoinsCapacity = 3
                },
                new Rule { 
                    Id = 3,
                    Name = "La porte !", 
                    Description = "Quand tu sors tu ferme la porte derière toi, sinon tu paie les croissants. Tu as 3 chances.",
                    CoinsCapacity = 3
                }
            );

            modelBuilder.Entity<Team>().HasData
            (
                new Team { 
                    Id = 1,
                    Name = "CESI RIL B2 aka Croissanistan"
                }
            );


            modelBuilder.Entity<User>().HasData
            (
                new User { 
                    Id = 1,
                    Lastname = "Vallet", 
                    Firstname = "Sébastien",
                    BirthDate = new System.DateTime(1997, 09, 18),
                    TeamId = 1
                },
                new User { 
                    Id = 2,
                    Lastname = "Bayon", 
                    Firstname = "Sylvain",
                    BirthDate = new System.DateTime(1970, 01, 01),
                    TeamId = 1
                }
            );
            

            modelBuilder.Entity<TeamRule>().HasData
            (
                new TeamRule { 
                    TeamId = 1, 
                    RuleId = 1
                },
                new TeamRule { 
                    TeamId = 1, 
                    RuleId = 2
                },
                new TeamRule { 
                    TeamId = 1, 
                    RuleId = 3
                }
            );


            modelBuilder.Entity<UserRule>().HasData
            (
                new UserRule { 
                    UserId = 1, 
                    RuleId = 1,
                    CoinsQuantity = 1
                },
                 new UserRule { 
                    UserId = 2, 
                    RuleId = 3,
                    CoinsQuantity = 2
                }
            );
        }
    }
}