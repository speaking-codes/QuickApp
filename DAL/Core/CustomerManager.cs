using DAL.Core.Helpers;
using DAL.Core.Interfaces;
using DAL.Enums;
using DAL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Core
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public CustomerManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<Customer> GetCustomers() => _unitOfWork.Customers.GetAllCustomers().ToList();

        public Customer GetCustomer(string customerCode) => _unitOfWork.Customers.GetCustomer(customerCode).FirstOrDefault();

        public IList<Customer> GetActiveCustomers() => _unitOfWork.Customers.GetActiveCustomers().ToList();

        public string AddCustomer(Customer customer)
        {
            customer.CustomerCode = Utility.GenerateCustomerCode(customer.FirstName, customer.LastName, _unitOfWork.Customers.MaxId() + 1);
            customer.IsActive = true;
            _unitOfWork.Customers.Add(customer);
            _unitOfWork.SaveChanges();
            return customer.CustomerCode;
        }

        public string UpdateCustomer(string customerCode, Customer customerToUpdate)
        {
            var customer = _unitOfWork.Customers.GetCustomer(customerCode).FirstOrDefault();
            if (customer == null)
                throw new Exception($"Customer with Customer.taxIdCode: {customerCode} not found");

            #region Customer

            customer.FirstName = customerToUpdate.FirstName;
            customer.LastName = customerToUpdate.LastName;
            customer.Gender = customerToUpdate.Gender;
            customer.BirthDate = customerToUpdate.BirthDate;
            customer.BirthPlace = customerToUpdate.BirthPlace;
            customer.BirthCounty = customerToUpdate.BirthCounty;
            customer.Profession = customerToUpdate.Profession;
            customer.ContractType = customerToUpdate.ContractType;
            customer.RAL = customerToUpdate.RAL;

            #region Address 

            if (customer.Addresses == null)
                customer.Addresses = new List<Address>();
            else
                customer.Addresses.Clear();

            customer.Addresses.AddRange(customerToUpdate.Addresses);

            #endregion

            #region Delivery

            if (customer.Deliveries == null)
                customer.Deliveries = new List<Delivery>();
            else
                customer.Deliveries.Clear();

            customer.Deliveries.AddRange(customerToUpdate.Deliveries);

            #endregion

            #endregion

            _unitOfWork.Customers.Update(customer);
             _unitOfWork.SaveChanges();
            return customer.CustomerCode;
        }

        public int DeleteCustomer(string customerCode)
        {
            var customer = _unitOfWork.Customers.GetCustomer(customerCode).FirstOrDefault();
            if (customer == null)
                throw new Exception($"Customer with Customer.taxIdCode: {customerCode} not found");

            customer.IsActive = false;
            _unitOfWork.Customers.Update(customer);
            return _unitOfWork.SaveChanges();
        }
    }
}
