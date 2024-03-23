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
        private readonly ILearningManager _learningManager;

        private LearningCustomerPreferences getLearningCustomerPreferencesFromCustomer(Customer customer, LearningCustomerPreferences learningCustomerPreferences) {
            if (learningCustomerPreferences==null)
                learningCustomerPreferences=new LearningCustomerPreferences();

            learningCustomerPreferences.Age = customer.BirthDate.HasValue ? customer.BirthDate.Value.GetAge() : 0;
            learningCustomerPreferences.MaritalStatus = customer.MaritalStatus?.MaritalStatusDescription;
            learningCustomerPreferences.FamilyType = customer.FamilyType?.FamilyTypeDescription;
            learningCustomerPreferences.ChildrenNumbers = customer.ChildrenNumber;
            learningCustomerPreferences.IncomeType = customer.IncomeType?.IncomeTypeDescription;
            learningCustomerPreferences.ProfessionType = customer.ProfessionType?.ProfessionTypeDescription;
            learningCustomerPreferences.Income = customer.Income;
            learningCustomerPreferences.Region = customer.Addresses
                                                .Where(x => x.IsPrimary)
                                                .FirstOrDefault()?
                                                .Municipality?
                                                .Province?
                                                .Region?
                                                .RegionName;

            return learningCustomerPreferences;
        }

        private float getLearningScore(LearningCustomerPreferences learningCustomerPreferences)
        {
            //var modelInput = learningCustomerPreferences.ToRegressionPredictionModel();
            //var modelOutput= _learningManager.GetPrediction(modelInput);
            //return modelOutput.Score;
            return 0.0f;
        }

        public InsurancePolicyNoSqlManagerAdded(IUnitOfWork unitOfWork,
                                                IInsuranceCoverageChartRepository insuranceCoverageChartRepository,
                                                IInsuranceCoverageSummaryRepository insuranceCoverageSummaryRepository,
                                                CustomerInsurancePolicyQueue customerInsurancePolicyQueue,
                                                ILearningManager learningManager) :
            base(unitOfWork, insuranceCoverageChartRepository, insuranceCoverageSummaryRepository, customerInsurancePolicyQueue)
        {
            _learningManager = learningManager;
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
            var customer = _unitOfWork.Customers.GetCustomersForServerLessManager(customerInsurancePolicyQueue.CustomerCode).FirstOrDefault();
            var insurancePolicy = _unitOfWork.InsurancePolicies.GetInsurancePolicyForTrainingMachineLearning(customerInsurancePolicyQueue.InsurancePolicyCode).FirstOrDefault();
            var customerLearning = _unitOfWork.LearningTrainings.GetLearningCustomerPreferences(customer.CustomerCode, insurancePolicy.InsurancePolicyCategory.InsurancePolicyCategoryCode).FirstOrDefault();

            await _unitOfWork.BeginTransactionAsync();

            if (customerLearning != null)
            {
                              _unitOfWork.LearningTrainings.Update(customerLearning);
            }
            else
            {
                var userId = 0;
                var userIds = _unitOfWork.LearningTrainings.GetUserId(customer.CustomerCode);
                if (userIds == null || userIds.Count == 0)
                    userId = _unitOfWork.LearningTrainings.GetLastUserId() + 1;
                else
                    userId = userIds.Max();

                customerLearning = getLearningCustomerPreferencesFromCustomer(customer, customerLearning);
                customerLearning.CustomerCode = customer.CustomerCode;
                customerLearning.UserId = userId;

                _unitOfWork.LearningTrainings.Add(customerLearning);
            }

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }

        public override async Task UpdateMatrixUserItem(CustomerInsurancePolicyQueue customerInsurancePolicyQueue)
        {
            Task.Yield();
        }
    }
}
