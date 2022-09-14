namespace sprintplanningtoolbackend.Helpers
{
    using Microsoft.EntityFrameworkCore;

    public class SqliteDBContext : DBContext
    {
        public SqliteDBContext(IConfiguration configuration) : base(configuration) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("SPTDatabase"));
        }
    }
}
