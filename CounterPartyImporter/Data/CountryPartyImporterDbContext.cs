using CounterPartyDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace CounterPartyDomain.Data
{
    public class CountryPartyImporterDbContext : DbContext
    {

        public DbSet<Company> Companies { get; set; }
        public CountryPartyImporterDbContext(DbContextOptions<CountryPartyImporterDbContext> options)
    : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().ToTable("Company");
        }
    }
}
