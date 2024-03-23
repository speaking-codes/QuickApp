using DAL.ModelsNoSql;
using DAL.ModelsRabbitMQ;
using DAL.RepositoryNoSql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    internal class InsurancePolicyNoSqlManagerNoObject : InsurancePolicyNoSqlManager
    {
        public InsurancePolicyNoSqlManagerNoObject(IUnitOfWork unitOfWork, 
                                                   IInsuranceCoverageChartRepository insuranceCoverageChartRepository,
                                                   IInsuranceCoverageSummaryRepository insuranceCoverageSummaryRepository,
                                                   CustomerInsurancePolicyQueue customerInsurancePolicyQueue) : 
            base(unitOfWork, insuranceCoverageChartRepository, insuranceCoverageSummaryRepository, customerInsurancePolicyQueue) { }

        public override void UpdateInsuranceCoverageChart(InsuranceCoverageChart insuranceCoverageChart)
        {
            throw new NotImplementedException();
        }

        public override void UpdateInsuranceCoverageSummary(InsuranceCoverageSummary insuranceCoverageSummary)
        {
            throw new NotImplementedException();
        }

        public override async Task UpdateLearningTable(CustomerInsurancePolicyQueue customerInsurancePolicyQueue)
        {
            throw new NotImplementedException();
        }

        public override async Task UpdateMatrixUserItem(CustomerInsurancePolicyQueue customerInsurancePolicyQueue)
        {
            throw new NotImplementedException();
        }
    }
}
