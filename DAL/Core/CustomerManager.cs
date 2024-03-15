using DAL.Core.Helpers;
using DAL.Core.Interfaces;
using DAL.Models;
using DAL.ModelsRabbitMQ;
using DAL.QueueService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Core
{
    public class CustomerManager : Manager, ICustomerManager
    {
        private const string _queueName = "customers";

        private readonly IMessageQueueProducer _messageQueueProducer;

        public CustomerManager(IUnitOfWork unitOfWork, IMessageQueueProducer messageQueueProducer) : base(unitOfWork)
        {
            _messageQueueProducer = messageQueueProducer;
        }

        public IList<Customer> GetCustomers() => UnitOfWork.Customers.GetAllCustomers().ToList();

        public Customer GetCustomer(string customerCode) => UnitOfWork.Customers.GetCustomerForServerLessManager(customerCode).FirstOrDefault();

        public IList<Customer> GetActiveCustomers() => UnitOfWork.Customers.GetActiveCustomers().ToList();

        public IList<Customer> GetCustomersWithoutInsurancePolicies() => UnitOfWork.Customers.GetCustomersWithoutInsurancePolicies().ToList();

        public IList<Customer> GetActiveCustomersWithoutInsurancePolicies()=> UnitOfWork.Customers.GetActiveCustomersWithoutInsurancePolicies().ToList();

        public string AddCustomer(Customer customer)
        {
            try
            {
                if (!IsMassiveWriter) UnitOfWork.BeginTransaction();

                customer.CustomerCode = Utility.GenerateCustomerCode(customer.FirstName, customer.LastName, UnitOfWork.Customers.MaxId() + 1);
                customer.IsActive = true;
                UnitOfWork.Customers.Add(customer);
                UnitOfWork.SaveChanges();

                if (!IsMassiveWriter) UnitOfWork.CommitTransaction();

                _messageQueueProducer.Send(_queueName, new CustomerQueue(Enums.EnumPublishQueueType.Added, customer.CustomerCode));

                return customer.CustomerCode;
            }
            catch
            {
                if (!IsMassiveWriter) UnitOfWork.RollbackTransaction();
                _countError++;
                throw;
            }
        }

        public string UpdateCustomer(string customerCode, Customer customerToUpdate)
        {
            var customer = UnitOfWork.Customers.GetCustomerForServerLessManager(customerCode).FirstOrDefault();
            if (customer == null)
                throw new Exception($"Customer with Customer.taxIdCode: {customerCode} not found");

            try
            {
                if (!IsMassiveWriter) UnitOfWork.BeginTransaction();

                #region Customer

                customer.FirstName = customerToUpdate.FirstName;
                customer.LastName = customerToUpdate.LastName;
                customer.Gender = customerToUpdate.Gender;
                customer.BirthDate = customerToUpdate.BirthDate;
                customer.FamilyTypeId = customerToUpdate.FamilyTypeId;
                customer.MaritalStatusId = customerToUpdate.MaritalStatusId;
                customer.BirthMunicipalityId = customerToUpdate.BirthMunicipalityId;

                customer.ContractTypeId = customerToUpdate.ContractTypeId;
                customer.IncomeTypeId = customerToUpdate.IncomeTypeId;
                customer.Income = customerToUpdate.Income;

                customer.IsActive = customer.IsActive;

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

                UnitOfWork.Customers.Update(customer);
                UnitOfWork.SaveChanges();
                if (!IsMassiveWriter) UnitOfWork.CommitTransaction();

                _messageQueueProducer.Send(_queueName, new CustomerQueue(Enums.EnumPublishQueueType.Updated, customer.CustomerCode));
                return customer.CustomerCode;
            }
            catch
            {
                if (!IsMassiveWriter) UnitOfWork.RollbackTransaction();
                _countError++;
                throw;
            }
        }

        public int activateCustomer(string customerCode)
        {
            var customer = UnitOfWork.Customers.GetCustomerForServerLessManager(customerCode).FirstOrDefault();
            if (customer == null)
                throw new Exception($"Customer with Customer.taxIdCode: {customerCode} not found");

            try
            {
                if (!IsMassiveWriter) UnitOfWork.BeginTransaction();

                customer.IsActive = true;
                UnitOfWork.Customers.Update(customer);
                _messageQueueProducer.Send(_queueName, new CustomerQueue(Enums.EnumPublishQueueType.Added, customer.CustomerCode));
                var countRow = UnitOfWork.SaveChanges();

                if (!IsMassiveWriter) UnitOfWork.CommitTransaction();

                return countRow;
            }
            catch
            {
                if (!IsMassiveWriter) UnitOfWork.RollbackTransaction();
                _countError++;
                throw;
            }
        }

        public int DeleteCustomer(string customerCode)
        {
            var customer = UnitOfWork.Customers.GetCustomerForServerLessManager(customerCode).FirstOrDefault();
            if (customer == null)
                throw new Exception($"Customer with Customer.taxIdCode: {customerCode} not found");

            try
            {
                if (!IsMassiveWriter) UnitOfWork.BeginTransaction();

                customer.IsActive = false;
                UnitOfWork.Customers.Update(customer);
                _messageQueueProducer.Send(_queueName, new CustomerQueue(Enums.EnumPublishQueueType.Deleted, customer.CustomerCode));
                var countRow= UnitOfWork.SaveChanges();

                if (!IsMassiveWriter) UnitOfWork.CommitTransaction();

                return countRow;
            }
            catch
            {
                if (!IsMassiveWriter) UnitOfWork.RollbackTransaction();
                _countError++;
                throw;
            }
        }

        public override void Dispose()
        {
            if (IsMassiveWriter && UnitOfWork.IsTransactionOpened)
            {
                if (_countError > 0)
                    UnitOfWork.RollbackTransaction();
                else
                    UnitOfWork.CommitTransaction();
            }

            UnitOfWork.Dispose();
        }
    }
}
