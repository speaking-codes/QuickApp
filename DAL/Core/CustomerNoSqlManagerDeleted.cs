using DAL.ModelsNoSql;
using DAL.ModelsRabbitMQ;
using DAL.Repositories.Interfaces;
using DAL.RepositoryNoSql.Interfaces;
using MongoDB.Driver;

namespace DAL.Core
{
    internal class CustomerNoSqlManagerDeleted : CustomerNoSqlManager
    {
        public CustomerNoSqlManagerDeleted(ICustomerRepository customerRepository,
                                           ICustomerHeaderRepository customerRepositoryNoSql,
                                           ICustomerDetailRepository customerDetailRepositoryNoSql,
                                           CustomerQueue customerQueue) :
            base(customerRepository, customerRepositoryNoSql, customerDetailRepositoryNoSql, customerQueue)
        {

        }

        protected override void Run()
        {
            var filterHeader = Builders<CustomerHeader>.Filter.Eq(x => x.CustomerCode, CustomerHeader.CustomerCode);
            CustomerHeader.IsDeleted = true;
            //_customerHeaderRepositoryNoSql.ReplaceOne(filterHeader, CustomerHeader);

            var filterDetail = Builders<CustomerDetail>.Filter.Eq(x => x.CustomerCode, CustomerDetail.CustomerCode);
            CustomerDetail.IsDeleted = true;
            //_customerDetailRepositoryNoSql.ReplaceOne(filterDetail, CustomerDetail);
        }
    }
}
