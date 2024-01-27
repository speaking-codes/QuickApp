using DAL.Core.Interfaces;
using DAL.Models;
using DAL.QueueService;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Core
{
    public class InsurancePolicyManager : Manager, IInsurancePolicyManager
    {
        private const string _queueName = "insurancepolicies";
        private int _countError;

        private readonly IMessageQueueProducer _messageQueueProducer;

        private string getInsurancePolicyCode(InsurancePolicy insurancePolicy)
        {
            var insurancePolicyCategory = UnitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategory(insurancePolicy.InsurancePolicyCategory.InsurancePolicyCategoryCode).FirstOrDefault();
            var salesLineCode = insurancePolicyCategory.SalesLine.SalesLineCode;
            var insurancePolicyCategoryCode = insurancePolicyCategory.InsurancePolicyCategoryCode;
            var policyCount = UnitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategoryCount(insurancePolicyCategoryCode).FirstOrDefault() + 1;

            return $"{salesLineCode}-{insurancePolicyCategoryCode}-{policyCount}";
        }

        public InsurancePolicyManager(IUnitOfWork unitOfWork, IMessageQueueProducer messageQueueProducer) : base(unitOfWork)
        {
            _countError = -1;
            _messageQueueProducer = messageQueueProducer;
        }

        public IList<InsurancePolicy> GetInsurancePolicy(string customerCode) => UnitOfWork.InsurancePolicies.GetInsurancePolicies(customerCode).ToList();

        public IList<InsurancePolicy> GetActiveInsurancePolicy(string customerCode) => UnitOfWork.InsurancePolicies.GetActiveInsurancePolicies(customerCode).ToList();

        public string AddInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            try
            {
                if (!IsMassiveWriter) UnitOfWork.BeginTransaction();

                insurancePolicy.InsurancePolicyCode = getInsurancePolicyCode(insurancePolicy);
                UnitOfWork.InsurancePolicies.Add(insurancePolicy);
                UnitOfWork.SaveChanges();

                if (!IsMassiveWriter) UnitOfWork.CommitTransaction();

                return insurancePolicy.InsurancePolicyCode;
            }
            catch
            {
                if (!IsMassiveWriter) UnitOfWork.RollbackTransaction();
                _countError++;
                throw;
            }
        }

        public override void Dispose()
        {
            if (IsMassiveWriter)
            {
                if (_countError > 0)
                    UnitOfWork.RollbackTransaction();
                else
                    UnitOfWork.CommitTransaction();
            }

            UnitOfWork.Dispose();
        }
    }
}
