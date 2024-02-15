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

        public IQueryable<InsurancePolicy> GetInsurancePolicies(string customerCode) =>
            _appContext.InsurancePolicies
                                .Include(x => x.InsurancePolicyCategory)
                                    .ThenInclude(y => y.SalesLine)
                                .AsSingleQuery()
                                .Where(x => x.Customer.CustomerCode == customerCode);

        public IQueryable<InsurancePolicy> GetInsurancePolicy(int id) =>
            _appContext.InsurancePolicies
                       .Include(x => x.InsurancePolicyCategory)
                       .Where(x => x.Id == id);

        public IQueryable<InsurancePolicy> GetActiveInsurancePolicies(string customerCode) =>
            _appContext.InsurancePolicies
                                .Include(x => x.InsurancePolicyCategory)
                                    .ThenInclude(y => y.SalesLine)
                                .AsSingleQuery()
                                .Where(x => x.Customer.CustomerCode == customerCode && x.IssueDate <= DateTime.Now && x.ExpiryDate > DateTime.Now);

        public int GetInsurancePolicyCategoryCount(string insurancePolicyCategoryCode) =>
            _appContext.InsurancePolicies
                       .Where(x => x.InsurancePolicyCategory.InsurancePolicyCategoryCode == insurancePolicyCategoryCode)
                       .Count();

        public bool IsExistingInsurancePolicyCategory(string customerCode, string insurancePolicyCategoryCode, DateTime issueDate, DateTime expiryDate) =>
            _appContext.InsurancePolicies
                       .Where(x => x.Customer.CustomerCode == customerCode &&
                                   x.InsurancePolicyCategory.InsurancePolicyCategoryCode == insurancePolicyCategoryCode &&
                                   x.IssueDate <= expiryDate &&
                                   x.ExpiryDate <= issueDate)
                       .Any();
    }
}
