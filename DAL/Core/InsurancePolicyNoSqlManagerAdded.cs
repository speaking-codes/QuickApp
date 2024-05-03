using DAL.Core.Helpers;
using DAL.Core.Interfaces;
using DAL.Helpers;
using DAL.Mapping;
using DAL.Models;
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
        private readonly IStorageManager _storageManager;

        private CustomerLearningFeature getLearningCustomerPreferencesFromCustomer(Customer customer, CustomerLearningFeature customerLearningFeature) {
            if (customerLearningFeature==null)
                customerLearningFeature=new CustomerLearningFeature();

            return customerLearningFeature;
        }

        public InsurancePolicyNoSqlManagerAdded(IUnitOfWork unitOfWork,
                                                IInsuranceCoverageChartRepository insuranceCoverageChartRepository,
                                                IInsuranceCoverageSummaryRepository insuranceCoverageSummaryRepository,
                                                CustomerInsurancePolicyQueue customerInsurancePolicyQueue,
                                                IStorageManager storageManager) :
            base(unitOfWork, insuranceCoverageChartRepository, insuranceCoverageSummaryRepository, customerInsurancePolicyQueue)
        {
            _storageManager = storageManager;
        }

        public override void UpdateInsuranceCoverageChart(InsuranceCoverageChart insuranceCoverageChart)
        {
            var insuranceCoverageChartByRepository = _insuranceCoverageChartRepository.GetInsuranceCoverageChart(insuranceCoverageChart.CustomerCode);
            if (insuranceCoverageChartByRepository == null)
            {
                _insuranceCoverageChartRepository.InsertOne(insuranceCoverageChart);
                return;
            }

            _insuranceCoverageChartRepository.UpdateInsuranceCoverageChart(insuranceCoverageChart.CustomerCode, insuranceCoverageChart);
        }

        public override void UpdateInsuranceCoverageSummary(InsuranceCoverageSummary insuranceCoverageSummary)
        {
            var insuranceCoverageSummaryRepository = _insuranceCoverageSummaryRepository.GetInsuranceCoverageSummary(insuranceCoverageSummary.CustomerCode);
            if (insuranceCoverageSummaryRepository == null)
            {
                _insuranceCoverageSummaryRepository.InsertOne(insuranceCoverageSummary);
                return;
            }

            _insuranceCoverageSummaryRepository.UpdateInsuranceCoverageSummary(insuranceCoverageSummary.CustomerCode, insuranceCoverageSummary);
        }

        public override async Task UpdateLearningTable(CustomerInsurancePolicyQueue customerInsurancePolicyQueue)
        {
            var customer =new Customer { CustomerCode = customerInsurancePolicyQueue.CustomerCode };
            var pathDirectory = @"C:\Users\mauro.diliddo\source\repos\QuickApp\QuickAppGitHub\QuickApp\DataStorage\Training\";
            var pathInput = $"{pathDirectory}Input\\";
            await _storageManager.AddToStorage(pathInput,customer);
        }

        public override async Task UpdateMatrixUserItem(CustomerInsurancePolicyQueue customerInsurancePolicyQueue)
        {
            Task.Yield();
        }
    }
}
