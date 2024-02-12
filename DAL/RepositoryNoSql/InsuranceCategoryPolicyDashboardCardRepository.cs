using DAL.ModelsNoSql;
using DAL.MongoDB;
using DAL.RepositoryNoSql.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryNoSql
{
    public class InsuranceCategoryPolicyDashboardCardRepository : RepositoryNoSql<InsuranceCategoryPolicyDashboardCard>, IInsuranceCategoryPolicyDashboardCardRepository
    {
        public InsuranceCategoryPolicyDashboardCardRepository(IMongoDbContext context, string collectionName)
                : base(context, collectionName)
        {

        }

        public IList<InsuranceCategoryPolicyDashboardCard> GetAll()
        {
            var filter = Builders<InsuranceCategoryPolicyDashboardCard>.Filter.Empty;
            return base.GetCollection().Find(filter).ToList();
        }
    }
}
