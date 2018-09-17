using CounterPartyDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace CounterPartyDomain.Data
{
    public class CountryPartyImporterDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().ToTable("Company");
        }
    }
}
