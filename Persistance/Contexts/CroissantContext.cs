using CroissantApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CroissantApi.Persistence.Context
{
    public class CroissantContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRule> UserRules { get; set; }

        public DbSet<TeamRule> TeamRules { get; set; }

        public DbSet<Rule> Rules { get; set; }

        public CroissantContext(DbContextOptions<CroissantContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<TeamRule>()
                .HasKey(t => new { t.TeamId, t.RuleId });
            
            modelBuilder
                .Entity<UserRule>()
                .HasKey(u => new { u.UserId, u.RuleId });

            modelBuilder
                .Entity<User>()
                .HasOne<Team>(u => u.Team)
                .WithMany(t => t.Users);
                //.HasForeignKey(u => u.TeamId);
        }
    }
}