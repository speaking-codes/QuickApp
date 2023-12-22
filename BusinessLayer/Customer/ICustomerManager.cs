using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BusinessLayer.Customer
{
    public interface ICustomerManager
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomer(string taxIdCode);
        IEnumerable<Customer> GetCustomers(string search);
    }
}
