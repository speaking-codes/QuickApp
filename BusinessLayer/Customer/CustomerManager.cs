using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Customer
{
    public class CustomerManager:ICustomerManager
    {

        public CustomerManager() { }
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomer(string taxIdCode);
        IEnumerable<Customer> GetCustomers();
    }
}
