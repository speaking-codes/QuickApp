using DAL.Core.Interfaces;
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
    internal class CustomerNoSqlManagerAdded: CustomerNoSqlManager
    {
        private readonly ILearningManager _learningManager;

        public CustomerNoSqlManagerAdded(ICustomerRepository customerRepository, 
                                         ICustomerHeaderRepository customerHeaderRepositoryNoSql, 
                                         ICustomerDetailRepository customerDetailRepositoryNoSql, 
                                         CustomerQueue customerQueue):
            base(customerRepository, customerHeaderRepositoryNoSql, customerDetailRepositoryNoSql, customerQueue)
        { }

        protected override void Run()
        {
            _customerHeaderRepositoryNoSql.InsertOne(CustomerHeader);
            _customerDetailRepositoryNoSql.InsertOne(CustomerDetail);
        }
    }
}
