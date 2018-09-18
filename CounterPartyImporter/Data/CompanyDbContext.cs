using CounterPartyDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace CounterPartyDomain.Data
{
    public class CompanyDbContext : DbContext
    {

        public DbSet<Company> Companies { get; set; }
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
    : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().ToTable("Company");
        }
    }
}
