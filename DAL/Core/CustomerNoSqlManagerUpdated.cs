using DAL.ModelsNoSql;
using DAL.ModelsRabbitMQ;
using DAL.Repositories.Interfaces;
using DAL.RepositoryNoSql.Interfaces;
using MongoDB.Driver;

namespace DAL.Core
{
    internal class CustomerNoSqlManagerUpdated: CustomerNoSqlManager
    {
        public CustomerNoSqlManagerUpdated(ICustomerRepository customerRepository, 
                                           ICustomerHeaderRepository customerRepositoryNoSql, 
                                           ICustomerDetailRepository customerDetailRepositoryNoSql, 
                                           CustomerQueue customerQueue) : 
            base(customerRepository, customerRepositoryNoSql, customerDetailRepositoryNoSql, customerQueue)
        {

        }

        protected override void Run()
        {
            _customerHeaderRepositoryNoSql.DeleteCustomer(CustomerHeader.CustomerCode);
            _customerDetailRepositoryNoSql.DeleteCustomer(CustomerDetail.CustomerCode);

            _customerHeaderRepositoryNoSql.InsertOne(CustomerHeader);
            _customerDetailRepositoryNoSql.InsertOne(CustomerDetail);
        }
    }
}
