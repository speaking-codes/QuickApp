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
    internal class InsurancePolicyNoSqlManagerAdded : InsurancePolicyNoSqlManager
    {
        public InsurancePolicyNoSqlManagerAdded(IUnitOfWork unitOfWork,
                                                IInsuranceCoverageChartRepository insuranceCoverageChartRepository,
                                                IInsuranceCoverageSummaryRepository insuranceCoverageSummaryRepository,
                                                CustomerInsurancePolicyQueue customerInsurancePolicyQueue) :
            base(unitOfWork, insuranceCoverageChartRepository, insuranceCoverageSummaryRepository, customerInsurancePolicyQueue)
        { }

        public override void UpadateInsuranceCoverageChart(InsuranceCoverageChart insuranceCoverageChart)
        {
            var insuranceCoverageChartByRepository = _insuranceCoverageChartRepository.GetInsuranceCoverageChart(insuranceCoverageChart.CustomerCode);
            if (insuranceCoverageChartByRepository == null)
            {
                _insuranceCoverageChartRepository.InsertOne(insuranceCoverageChart);
                return;
            }

            _insuranceCoverageChartRepository.UpdateInsuranceCoverageChart(insuranceCoverageChart.CustomerCode, insuranceCoverageChart);
        }
    }
}
