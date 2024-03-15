// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using DAL.Models;
using System.Linq;

namespace DAL.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IQueryable<Customer> GetActiveCustomers();
        IQueryable<Customer> GetAllCustomers();
        IQueryable<Customer> GetCustomersWithoutInsurancePolicies();
        IQueryable<Customer> GetActiveCustomersWithoutInsurancePolicies();
        IQueryable<Customer> GetCustomerForServerLessManager(string customerCode);
        int MaxId();
    }
}
