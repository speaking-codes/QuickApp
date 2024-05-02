using DAL.Core.Interfaces;
using DAL.ModelsRabbitMQ;
using DAL.RepositoryNoSql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    public class InsurancePolicyServerlessManager : IInsurancePolicyServerlessManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInsuranceCoverageChartRepository _insuranceCoverageChartRepository;
        private readonly IInsuranceCoverageSummaryRepository _insuranceCoverageSummaryRepository;
        private readonly ILearningManager _learningManager;
        private readonly IStorageManager _storageManager;

        public InsurancePolicyServerlessManager(IUnitOfWork unitOfWork,
                                                IInsuranceCoverageChartRepository insuranceCoverageChartRepository,
                                                IInsuranceCoverageSummaryRepository insuranceCoverageSummaryRepository,
                                                ILearningManager learningManager,
                                                IStorageManager storageManager)
        {
            _unitOfWork = unitOfWork;
            _insuranceCoverageChartRepository = insuranceCoverageChartRepository;
            _insuranceCoverageSummaryRepository = insuranceCoverageSummaryRepository;
            _learningManager = learningManager;
            _storageManager = storageManager;
        }

        public async Task Manage(CustomerInsurancePolicyQueue customerInsurancePolicyQueue)
        {
            try
            {
                var insurancePolicies = _unitOfWork.InsurancePolicies.GetActiveInsurancePolicies(customerInsurancePolicyQueue.CustomerCode).ToList();
                InsurancePolicyNoSqlManager insurancePolicyNoSqlManager;
                switch (customerInsurancePolicyQueue.PublishQueueType)
                {
                    case Enums.EnumPublishQueueType.Added:
                        insurancePolicyNoSqlManager = new InsurancePolicyNoSqlManagerAdded(_unitOfWork,
                                                                                           _insuranceCoverageChartRepository,
                                                                                           _insuranceCoverageSummaryRepository,
                                                                                           customerInsurancePolicyQueue,
                                                                                           _learningManager,
                                                                                           _storageManager);
                        break;
                    case Enums.EnumPublishQueueType.Deleted:
                        insurancePolicyNoSqlManager = new InsurancePolicyNoSqlManagerDeleted(_unitOfWork,
                                                                                             _insuranceCoverageChartRepository,
                                                                                             _insuranceCoverageSummaryRepository,
                                                                                             customerInsurancePolicyQueue);
                        break;
                    default:
                        insurancePolicyNoSqlManager = new InsurancePolicyNoSqlManagerNoObject(_unitOfWork,
                                                                                              _insuranceCoverageChartRepository,
                                                                                              _insuranceCoverageSummaryRepository,
                                                                                              customerInsurancePolicyQueue);
                        break;
                }
                await insurancePolicyNoSqlManager.Execute();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
