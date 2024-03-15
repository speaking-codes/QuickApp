using DAL.Mapping;
using DAL.Models;
using DAL.ModelsNoSql;
using DAL.ModelsRabbitMQ;
using DAL.Repositories.Interfaces;
using DAL.RepositoryNoSql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    internal abstract class CustomerNoSqlManager
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerQueue _customerQueue;

        protected readonly ICustomerHeaderRepository _customerHeaderRepositoryNoSql;
        protected readonly ICustomerDetailRepository _customerDetailRepositoryNoSql;

        protected Customer Customer { get; private set; }
        protected CustomerHeader CustomerHeader { get; private set; }
        protected CustomerDetail CustomerDetail { get; private set; }

        public CustomerNoSqlManager(ICustomerRepository customerRepository, ICustomerHeaderRepository customerRepositoryNoSql, ICustomerDetailRepository customerDetailRepositoryNoSql, CustomerQueue customerQueue)
        {
            _customerRepository = customerRepository;
            _customerHeaderRepositoryNoSql = customerRepositoryNoSql;
            _customerDetailRepositoryNoSql=customerDetailRepositoryNoSql;
            _customerQueue = customerQueue;
        }

        private void GetCustomerFromDb()
        {
            Customer = _customerRepository.GetCustomerForServerLessManager (_customerQueue.CustomerCode).FirstOrDefault();
        }

        protected abstract void Run();

        public void Execute()
        {
            GetCustomerFromDb();
            if (Customer == null)
                return;
            
            CustomerHeader = Customer.ToNoSqlHeaderEntity();
            CustomerDetail = Customer.ToNoSqlDetailEntity();
            Run();
        }
    }
}
