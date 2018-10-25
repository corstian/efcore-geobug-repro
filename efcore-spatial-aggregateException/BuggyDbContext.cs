using Microsoft.EntityFrameworkCore;

namespace efcore_spatial_aggregateException
{
    public class BuggyDbContext : DbContext
    {
        string _connectionString = "Data Source=(localdb)\\.;Initial Catalog=geobug;Integrated Security=True;";

        public DbSet<Airfield> Airfields { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString, q => q.UseNetTopologySuite());
        }
    }
}
