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
        private int _countError;

        private readonly IMessageQueueProducer _messageQueueProducer;

        public CustomerManager(IUnitOfWork unitOfWork, IMessageQueueProducer messageQueueProducer) : base(unitOfWork)
        {
            _countError = -1;
             _messageQueueProducer = messageQueueProducer;
       }

        public IList<Customer> GetCustomers() => UnitOfWork.Customers.GetAllCustomers().ToList();

        public Customer GetCustomer(string customerCode) => UnitOfWork.Customers.GetCustomer(customerCode).FirstOrDefault();

        public IList<Customer> GetActiveCustomers() => UnitOfWork.Customers.GetActiveCustomers().ToList();

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

                _messageQueueProducer.Send(_queueName, new CustomerQueue(Enums.EnumPublishQueueType.Created, customer.CustomerCode));

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
            var customer = UnitOfWork.Customers.GetCustomer(customerCode).FirstOrDefault();
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
                //customer.BirthPlace = customerToUpdate.BirthPlace;
                //customer.BirthCounty = customerToUpdate.BirthCounty;
                customer.JobTitle = customerToUpdate.JobTitle;
                customer.ContractType = customerToUpdate.ContractType;
                customer.Ral = customerToUpdate.Ral;

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

        public int DeleteCustomer(string customerCode)
        {
            var customer = UnitOfWork.Customers.GetCustomer(customerCode).FirstOrDefault();
            if (customer == null)
                throw new Exception($"Customer with Customer.taxIdCode: {customerCode} not found");

            try
            {
                if (!IsMassiveWriter) UnitOfWork.BeginTransaction();

                customer.IsActive = false;
                UnitOfWork.Customers.Update(customer);
                _messageQueueProducer.Send(_queueName, new CustomerQueue(Enums.EnumPublishQueueType.Deleted, customer.CustomerCode));
                return UnitOfWork.SaveChanges();

                if (!IsMassiveWriter) UnitOfWork.CommitTransaction();
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
            if (IsMassiveWriter)
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
