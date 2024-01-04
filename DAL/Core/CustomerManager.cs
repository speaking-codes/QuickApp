using DAL.Core.Interfaces;
using DAL.Models;
using System;
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

        public IList<Customer> GetActiveCustomers() => _unitOfWork.Customers.GetActiveCustomers().ToList();

        public int AddCustomer(Customer customer)
        {
            _unitOfWork.Customers.Add(customer);
            return _unitOfWork.SaveChanges();
        }

        public void DeleteCustomer(string taxIdCode)
        {
            var customer = _unitOfWork.Customers.GetCustomer(taxIdCode).FirstOrDefault();
            if (customer == null)
                throw new Exception($"Customer with Customer.taxIdCode: {taxIdCode} not found");

            customer.IsActive = false;
            _unitOfWork.Customers.Update(customer);
            _unitOfWork.SaveChanges();
        }
    }
}
