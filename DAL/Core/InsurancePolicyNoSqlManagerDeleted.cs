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
            base(unitOfWork, insuranceCoverageChartRepository, insuranceCoverageSummaryRepository, customerInsurancePolicyQueue)
        { }

        public override void UpdateInsuranceCoverageChart(InsuranceCoverageChart insuranceCoverageChart)
        {
            var filterHeader = Builders<InsuranceCoverageChart>.Filter.Eq(x => x.CustomerCode, insuranceCoverageChart.CustomerCode);
            _insuranceCoverageChartRepository.DeleteInsuranceCoverageChart(insuranceCoverageChart.CustomerCode);
        }

        public override void UpdateInsuranceCoverageSummary(InsuranceCoverageSummary insuranceCoverageSummary)
        {
            var filterInsuranceCoverageSummary = Builders<InsuranceCoverageSummary>.Filter.Eq(x => x.CustomerCode, insuranceCoverageSummary.CustomerCode);
            _insuranceCoverageSummaryRepository.DeleteInsuranceCoverageSummary(insuranceCoverageSummary.CustomerCode);
        }

        public override async Task UpdateLearningTable(CustomerInsurancePolicyQueue customerInsurancePolicyQueue) => Task.Yield();

        public override async Task UpdateMatrixUserItem(CustomerInsurancePolicyQueue customerInsurancePolicyQueue) => Task.Yield();
    }
}
