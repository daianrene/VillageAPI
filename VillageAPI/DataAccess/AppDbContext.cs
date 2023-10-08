using Microsoft.EntityFrameworkCore;
using VillageAPI.Models;

namespace VillageAPI.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Village> Villages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Village>().HasData(
                new Village()
                {
                    Id = 1,
                    Name = "VillaTest",
                    Description = "Description Test",
                    Population = 999,
                    M2 = 4000
                }
                );
        }
    }
}
