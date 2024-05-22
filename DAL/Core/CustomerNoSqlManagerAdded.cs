using DAL.Core.Interfaces;
using DAL.Exstensions;
using DAL.ModelsNoSql;
using DAL.ModelsRabbitMQ;
using DAL.Repositories.Interfaces;
using DAL.RepositoryNoSql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    internal class CustomerNoSqlManagerAdded : CustomerNoSqlManager
    {
        private readonly ILearningManager _learningManager;
        private readonly IInsuranceCategoryPolicyRecommendationRepository _insuranceCategoryPolicyRecommendationRepository;

        private void addCustomerRecommendation()
        {
            var insurancePolicyCategory = _learningManager.GetRecommendation(Customer.CustomerCode, 0.65f, 4);
            var insuranceCategoryPolicyRecommendation = new InsuranceCategoryPolicyRecommendation();
            insuranceCategoryPolicyRecommendation.CustomerCode = Customer.CustomerCode;
            insuranceCategoryPolicyRecommendation.InsuranceCategoryPolicies = insurancePolicyCategory.ToInsuranceCategoryPolicyDashboardCards();
            _insuranceCategoryPolicyRecommendationRepository.InsertOne(insuranceCategoryPolicyRecommendation);
        }

        public CustomerNoSqlManagerAdded(ICustomerRepository customerRepository,
                                         ICustomerHeaderRepository customerHeaderRepositoryNoSql,
                                         ICustomerDetailRepository customerDetailRepositoryNoSql,
                                         ILearningManager learningManager,
                                         IInsuranceCategoryPolicyRecommendationRepository insuranceCategoryPolicyRecommendationRepository,
                                         CustomerQueue customerQueue) :
            base(customerRepository, customerHeaderRepositoryNoSql, customerDetailRepositoryNoSql, customerQueue)
        {
            _learningManager = learningManager;
            _insuranceCategoryPolicyRecommendationRepository = insuranceCategoryPolicyRecommendationRepository;
        }

        protected override void Run()
        {
            _customerHeaderRepositoryNoSql.InsertOne(CustomerHeader);
            _customerDetailRepositoryNoSql.InsertOne(CustomerDetail);
            addCustomerRecommendation();
        }
    }
}
