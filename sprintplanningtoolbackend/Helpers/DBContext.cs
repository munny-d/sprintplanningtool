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

        public DbSet<User> Users { get; set; }
    }
}
