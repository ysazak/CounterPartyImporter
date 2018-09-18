using System.Collections.Generic;
using System.Threading.Tasks;
using CounterPartyDomain.Models;

namespace CounterPartyDomain.Repositories
{
    public interface ICompanyRepository
    {
        Task Add(Company company);
        Task AddRange(IList<Company> companyList);

        Task<int> GetCompaniesCount();
        Task<IList<Company>> GetCompanies(int page, int pageSize);
    }
}