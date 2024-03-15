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
        string UpdateCustomer(string customerCode, Customer customer);
        int activateCustomer(string customerCode);
        int DeleteCustomer(string customerCode);
    }
}
