using Models.Entities;
using System.Collections.Generic;

namespace DAL.Core.Interfaces
{
    public interface ICustomerManager
    {
        IList<Customer> GetCustomers();
        Customer GetCustomer(string taxIdCode);
        IList<Customer> GetActiveCustomers();
        int AddCustomer(Customer customer);
        void DeleteCustomer(string taxIdCode);
    }
}
