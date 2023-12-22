// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using Models.Entities;
using System.Linq;

namespace DAL.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IQueryable<Customer> GetTopActiveCustomers(int count);
        IQueryable<Customer> GetAllCustomers();
        IQueryable<Customer> GetCustomer(string taxIdCode);   
    }
}
