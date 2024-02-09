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
    public class InsuranceCategoryPolicyTopSellingRepository : RepositoryNoSql<InsuranceCategoryPolicyTopSelling>, IInsuranceCategoryPolicyTopSellingRepository
    {
        public InsuranceCategoryPolicyTopSellingRepository(IMongoDbContext context, string collectionName)
            : base(context, collectionName)
        {

        }

        public InsuranceCategoryPolicyTopSelling GetInsuranceCategoryPolicyTopSelling(int year)
        {
            var filter = Builders<InsuranceCategoryPolicyTopSelling>.Filter.Eq(c => c.Year, year);
            return FindOne(filter);
        }
    }
}
