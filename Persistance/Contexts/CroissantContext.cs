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
                .HasKey(tr => new { tr.TeamId, tr.RuleId });
            
            modelBuilder
                .Entity<UserRule>()
                .HasKey(ur => new { ur.UserId, ur.RuleId });

            modelBuilder
                .Entity<User>()
                .HasOne<Team>(u => u.Team)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TeamId);
        }
    }
}