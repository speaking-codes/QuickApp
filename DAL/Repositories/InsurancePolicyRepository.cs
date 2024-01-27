using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class InsurancePolicyRepository : Repository<InsurancePolicy>, IInsurancePolicyRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public InsurancePolicyRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<InsurancePolicy> GetInsurancePolicies(string customerCode)
        {
            return _appContext.InsurancePolicies
                                .Include(x => x.InsurancePolicyCategory)
                                    .ThenInclude(y => y.SalesLine)
                                .AsSingleQuery()
                                .Where(x => x.Customer.CustomerCode == customerCode);
        }

        public IQueryable<InsurancePolicy> GetActiveInsurancePolicies(string customerCode) {
            return _appContext.InsurancePolicies
                                .Include(x => x.InsurancePolicyCategory)
                                    .ThenInclude(y => y.SalesLine)
                                .AsSingleQuery()
                                .Where(x => x.Customer.CustomerCode == customerCode && x.CreatedDate <= DateTime.Now && x.ExpiryDate > DateTime.Now);
        }
    }
}
