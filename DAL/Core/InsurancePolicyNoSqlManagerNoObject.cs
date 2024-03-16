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

        public override void UpadateInsuranceCoverageChart(InsuranceCoverageChart insuranceCoverageChart)
        {
            throw new NotImplementedException();
        }
    }
}
