namespace sprintplanningtoolbackend.Helpers
{
    using Microsoft.EntityFrameworkCore;

    public class SqliteDBContext : DataContext
    {
        public SqliteDBContext(IConfiguration configuration) : base(configuration) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("SPTDatabase"));
        }
    }
}
