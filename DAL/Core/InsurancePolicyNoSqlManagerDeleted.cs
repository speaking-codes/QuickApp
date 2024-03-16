using DAL.ModelsNoSql;
using DAL.ModelsRabbitMQ;
using DAL.RepositoryNoSql.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    internal class InsurancePolicyNoSqlManagerDeleted : InsurancePolicyNoSqlManager
    {
        public InsurancePolicyNoSqlManagerDeleted(IUnitOfWork unitOfWork, 
                                                  IInsuranceCoverageChartRepository insuranceCoverageChartRepository,
                                                  IInsuranceCoverageSummaryRepository insuranceCoverageSummaryRepository,
                                                  CustomerInsurancePolicyQueue customerInsurancePolicyQueue) : 
            base(unitOfWork, insuranceCoverageChartRepository, insuranceCoverageSummaryRepository, customerInsurancePolicyQueue) { }

        public override void UpadateInsuranceCoverageChart(InsuranceCoverageChart insuranceCoverageChart)
        {
            var filterHeader = Builders<InsuranceCoverageChart>.Filter.Eq(x => x.CustomerCode, insuranceCoverageChart.CustomerCode);
            _insuranceCoverageChartRepository.DeleteInsuranceCoverageChart(insuranceCoverageChart.CustomerCode);
        }
    }
}
