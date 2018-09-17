using CounterPartyImporter.Models;
using Microsoft.EntityFrameworkCore;

namespace CounterPartyImporter.Data
{
    public class CountryPartyImporterDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().ToTable("Company");
        }
    }
}
