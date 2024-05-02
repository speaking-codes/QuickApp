// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public CustomerRepository(ApplicationDbContext context) : base(context)
        { }

        public IQueryable<Customer> GetActiveCustomers()
        {
            return _appContext.Customers
                .Include(c => c.Addresses)
                    .ThenInclude(a => a.Municipality)
                .Include(c => c.FamilyType)
                .Include(c => c.MaritalStatus)
                .Include(c => c.ContractType)
                .Include(c => c.ProfessionType)
                .Where(x => x.IsActive)
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName);
        }

        public IQueryable<Customer> GetAllCustomers()
        {
            return _appContext.Customers
                .Include(c => c.Addresses)
                    .ThenInclude(a => a.Municipality)
                        .ThenInclude(m => m.Province)
                .AsSingleQuery()
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName);
        }

        public IQueryable<Customer> GetCustomersWithoutInsurancePolicies() =>
            _appContext.Customers.Where(x => !x.InsurancePolicies.Any());

        public IQueryable<Customer> GetActiveCustomersWithoutInsurancePolicies() =>
            _appContext.Customers.Where(x => !x.InsurancePolicies.Any() && x.IsActive);

        public IQueryable<Customer> GetCustomersForServerLessManager(string customerCode) =>
                 _appContext.Customers
                            .Include(c => c.BirthMunicipality)
                                .ThenInclude(m => m.Province)
                            .Include(c => c.Deliveries)
                            .Include(c => c.Addresses)
                                .ThenInclude(a => a.Municipality)
                                    .ThenInclude(m => m.Province)
                                        .ThenInclude(p => p.Region)
                            .Where(c => c.CustomerCode == customerCode);

        public IQueryable<Customer> GetCustomerForLearningFeature(string customerCode) =>
                _appContext.Customers
                           .Include(c=> c.FamilyType)
                           .Include(c =>c.MaritalStatus)
                           .Include(c=>c.ContractType)
                           .Include(c=>c.IncomeType)
                           .Include(c=>c.ProfessionType)
                           .Include(c => c.Addresses)
                               .ThenInclude(a => a.Municipality)
                                   .ThenInclude(m => m.Province)
                                       .ThenInclude(p => p.Region)
                           .Where(c => c.CustomerCode == customerCode);

        public int MaxId()
        {
            if (!_appContext.Customers.Any())
                return 0;
            return _appContext.Customers.Max(x => x.Id);
        }
    }
}
