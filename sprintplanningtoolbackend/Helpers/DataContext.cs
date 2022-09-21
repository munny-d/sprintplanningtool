namespace sprintplanningtoolbackend.Helpers
{
    using Microsoft.EntityFrameworkCore;
    using sprintplanningtoolbackend.Models;

    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
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

        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<SprintReport>? SprintReports { get; set; }
        public virtual DbSet<TeamMember>? TeamMembers { get; set; }
    }
}
