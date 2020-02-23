using CroissantApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CroissantApi.Persistence.Context
{
    public class CroissantContext : DbContext
    {
        public CroissantContext(DbContextOptions<CroissantContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<Team>(u => u.Team)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TeamId);

            // modelBuilder.Entity<User>().HasData
            // (
            //     new User { 
            //         Id = 1,
            //         Lastname = "Vallet", 
            //         Firstname = "Sébastien",
            //         BirthDate = new System.DateTime(1997, 09, 18)
            //     }
            // );

            modelBuilder.Entity<UserRule>().HasKey(ur => new { ur.UserId, ur.RuleId });
        
            // modelBuilder.Entity<UserRule>().HasData
            // (
            //     new UserRule { 
            //         UserId = 1, 
            //         RuleId = 1,
            //         CoinsQuantity = 1
            //     }
            // );
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRule> UserRules { get; set; }
        public DbSet<Rule> Rules { get; set; }
    }
}