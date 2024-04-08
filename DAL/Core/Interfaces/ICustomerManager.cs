using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL.Core.Interfaces
{
    public interface ICustomerManager : IManager
    {
        IList<Customer> GetCustomers();
        Customer GetCustomer(string customerCode);
        IList<Customer> GetActiveCustomers();
        IList<Customer> GetCustomersWithoutInsurancePolicies();
        IList<Customer> GetActiveCustomersWithoutInsurancePolicies();

        string AddCustomer(Customer customer);
        int activateCustomer(string customerCode);
        void EnqueueAddedCustomers(IEnumerable<string> customerCodes);

        string UpdateCustomer(string customerCode, Customer customer);
        void EnqueueUpdatedCustomers(IEnumerable<string> customerCodes);


        int DeleteCustomer(string customerCode);
        void EnqueueDeletedCustomers(IEnumerable<string> customerCodes);
    }
}
