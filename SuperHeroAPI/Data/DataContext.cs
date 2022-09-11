using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Controllers.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SuperHero> SuperHeroes { get; set; } // This is table name in the DB
    }
}
