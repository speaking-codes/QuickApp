using DAL.ModelsNoSql;
using DAL.MongoDB;
using DAL.RepositoryNoSql.Interfaces;
using MongoDB.Driver;

namespace DAL.RepositoryNoSql
{
    public class CustomerHeaderRepository : RepositoryNoSql<CustomerHeader>, ICustomerHeaderRepository
    {
        public CustomerHeaderRepository(IMongoDbContext context, string collectionName)
            : base(context, collectionName)
        {

        }

        public virtual bool ReplaceOne(FilterDefinition<CustomerHeader> filter, CustomerHeader document)
        {
            var replaceResult = GetCollection().ReplaceOne(filter, document);
            return replaceResult.ModifiedCount == 1;
        }

        public CustomerHeader GetCustomer(string customerCode)
        {
            var filter = Builders<CustomerHeader>.Filter.Eq(c => c.CustomerCode, customerCode);
            return FindOne(filter);
        }

        public bool DeleteCustomer(string customerCode)
        {
            var filter = Builders<CustomerHeader>.Filter.Eq(c => c.CustomerCode, customerCode);
            var result = DeleteOne(filter);
            return result.DeletedCount > 0;
        }
    }
}
