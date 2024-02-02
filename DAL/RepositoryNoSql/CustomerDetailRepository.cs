using DAL.ModelsNoSql;
using DAL.MongoDB;
using DAL.RepositoryNoSql.Interfaces;
using MongoDB.Driver;

namespace DAL.RepositoryNoSql
{
    public class CustomerDetailRepository : RepositoryNoSql<CustomerDetail>, ICustomerDetailRepository
    {
        public CustomerDetailRepository(IMongoDbContext context, string collectionName)
            : base(context, collectionName)
        {

        }

        public virtual bool ReplaceOne(FilterDefinition<CustomerDetail> filter, CustomerDetail document)
        {
            var replaceResult = GetCollection().ReplaceOne(filter, document);
            return replaceResult.ModifiedCount == 1;
        }

        public CustomerDetail GetCustomer(string customerCode)
        {
            var filter = Builders<CustomerDetail>.Filter.Eq(c => c.CustomerCode, customerCode);
            return FindOne(filter);
        }      

        public bool DeleteCustomer(string customerCode)
        {
            var filter = Builders<CustomerDetail>.Filter.Eq(c => c.CustomerCode, customerCode);
            var result = DeleteOne(filter);
            return result.DeletedCount > 0;
        }
    }
}
