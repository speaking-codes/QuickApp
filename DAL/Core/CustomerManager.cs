using DAL.Core.Interfaces;
using Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Core
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<Customer> GetCustomers() => _unitOfWork.Customers.GetAllCustomers().ToList();

        public Customer GetCustomer(string taxIdCode) => _unitOfWork.Customers.GetCustomer(taxIdCode).FirstOrDefault();

        public IList<Customer> GetCustomersActive(string search) => new List<Customer>();

        public int AddCustomer(Customer customer)
        {
            _unitOfWork.Customers.Add(customer);
            return _unitOfWork.SaveChanges();
        }
    }
}
