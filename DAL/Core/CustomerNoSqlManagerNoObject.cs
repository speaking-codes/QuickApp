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
    internal class CustomerNoSqlManagerNoObject : CustomerNoSqlManager
    {
        public CustomerNoSqlManagerNoObject(ICustomerRepository customerRepository, 
                                            ICustomerHeaderRepository customerRepositoryNoSql,
                                            ICustomerDetailRepository customerDetailRepositoryNoSql, 
                                            CustomerQueue customerQueue) : 
            base(customerRepository, customerRepositoryNoSql, customerDetailRepositoryNoSql, customerQueue) { }

        protected override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
