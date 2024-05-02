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
    public class InsuranceCategoryPolicyRecommendationRepository : RepositoryNoSql<InsuranceCategoryPolicyRecommendation>, IInsuranceCategoryPolicyRecommendationRepository
    {
        public InsuranceCategoryPolicyRecommendationRepository(IMongoDbContext context, string collectionName)
             : base(context, collectionName)
        {

        }

        public InsuranceCategoryPolicyRecommendation GetInsuranceCategoryPolicyRecommendation(string customerCode)
        {
            var filter = Builders<InsuranceCategoryPolicyRecommendation>.Filter.Eq(c => c.CustomerCode, customerCode);
            return FindOne(filter);
        }
    }
}
