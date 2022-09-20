namespace sprintplanningtoolbackend.Helpers
{
    using Microsoft.EntityFrameworkCore;
    using sprintplanningtoolbackend.Models;

    public class DBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("SPTDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SprintReport>()
                .HasMany(t => t.TeamMembers)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<SprintReport> SprintReports { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
    }
}
