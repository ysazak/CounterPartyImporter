using CounterPartyDomain.Data;
using CounterPartyDomain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterPartyDomain.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CountryPartyImporterDbContext context;

        public CompanyRepository(CountryPartyImporterDbContext context)
        {
            this.context = context;
        }
        public async Task Add(Company company)
        {
            await context.Companies.AddAsync(company);
            await context.SaveChangesAsync();
        }

        public async Task AddRange(IList<Company> companyList)
        {
            context.ChangeTracker.AutoDetectChangesEnabled = false;
            for (int i = 0; i < companyList.Count; i++)
            {
                await context.Companies.AddAsync(companyList[i]);
                if(i%100 == 0)
                {
                    await context.SaveChangesAsync();
                }
            }
            await context.SaveChangesAsync();
        }

        public async Task<int> GetCompaniesCount()
        {
            return await context.Companies.CountAsync();
        }

        public async Task<IList<Company>> GetCompanies(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            return await context.Companies.OrderByDescending(c => c.Id).Skip(skip).Take(pageSize).AsNoTracking().ToListAsync();
        }



    }
}
