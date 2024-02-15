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
    public class InsuranceCoverageSummaryRepository : RepositoryNoSql<InsuranceCoverageSummary>, IInsuranceCoverageSummaryRepository
    {
        public InsuranceCoverageSummaryRepository(IMongoDbContext context, string collectionName) : base(context, collectionName)
        {
        }

        public InsuranceCoverageSummary GetInsuranceCoverageSummary(string customerCode)
        {
            var filter = Builders<InsuranceCoverageSummary>.Filter.Eq(x => x.CustomerCode, customerCode);
            return FindOne(filter);
        }
    }
}
