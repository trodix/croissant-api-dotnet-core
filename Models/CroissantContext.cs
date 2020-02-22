using Microsoft.EntityFrameworkCore;

namespace CroissantApi.Models
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

            modelBuilder.Entity<UserRule>().HasKey(ur => new { ur.UserId, ur.RuleId });
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRule> UserRules { get; set; }
        public DbSet<Rule> Rule { get; set; }
    }
}