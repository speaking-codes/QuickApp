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
    public class InsuranceCoverageChartRepository : RepositoryNoSql<InsuranceCoverageChart>, IInsuranceCoverageChartRepository
    {
        public InsuranceCoverageChartRepository(IMongoDbContext context, string collectionName)
            : base(context, collectionName)
        {

        }

        public InsuranceCoverageChart GetInsuranceCoverageChart(string customerCode)
        {
            var filter = Builders<InsuranceCoverageChart>.Filter.Eq(x => x.CustomerCode, customerCode);
            return FindOne(filter);
        }
    }
}
