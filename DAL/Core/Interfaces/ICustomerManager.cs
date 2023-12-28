using Models.Entities;
using System.Collections.Generic;

namespace DAL.Core.Interfaces
{
    public interface ICustomerManager
    {
        IList<Customer> GetCustomers();
        Customer GetCustomer(string taxIdCode);
        IList<Customer> GetCustomersActive(string search);
        int AddCustomer(Customer customer);
    }
}
