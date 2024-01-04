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
        public CustomerRepository(ApplicationDbContext context) : base(context)
        { }

        public IQueryable<Customer> GetActiveCustomers()
        {
            return _appContext.Customers
                .Include(c => c.Addresses)
                .AsSingleQuery()
                .Where(x => x.IsActive)
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName);
        }

        public IQueryable<Customer> GetAllCustomers()
        {
            return _appContext.Customers
                .Include(c => c.Addresses)
                .AsSingleQuery()
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName);
        }

        public IQueryable<Customer> GetCustomer(string taxIdCode)
        {
            return _appContext.Customers
                            .Include(c => c.Deliveries)
                            .Include(c => c.Addresses)
                            .AsSingleQuery()
                            .Where(c => c.TaxIdCode == taxIdCode);
        }

        public void Update(Customer customer) => base.Update(customer);

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    }
}
