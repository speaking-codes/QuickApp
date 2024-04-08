using DAL.Core.Interfaces;
using DAL.Enums;
using DAL.Models;
using DAL.ModelsRabbitMQ;
using DAL.QueueModels;
using DAL.QueueService;
using System;
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
            insurancePolicy.Progressive = (short)(UnitOfWork.InsurancePolicies.GetInsurancePolicyCategoryCount(insurancePolicyCategoryCode) + 1);

            return $"{salesLineCode}-{insurancePolicyCategoryCode}-{insurancePolicy.Progressive}";
        }

        public InsurancePolicyManager(IUnitOfWork unitOfWork, IMessageQueueProducer messageQueueProducer) : base(unitOfWork)
        {
            _countError = -1;
            _messageQueueProducer = messageQueueProducer;
        }

        public IList<InsurancePolicy> GetInsurancePolicy(string customerCode) => UnitOfWork.InsurancePolicies.GetInsurancePolicies(customerCode).ToList();

        public IList<InsurancePolicy> GetActiveInsurancePolicy(string customerCode) => UnitOfWork.InsurancePolicies.GetActiveInsurancePolicies(customerCode).ToList();

        public IList<InsurancePolicy> GetExpiredInsurancePolicy(DateTime expireDate) => UnitOfWork.InsurancePolicies.GetExiperedInsurancePolicies(expireDate).ToList();

        public string AddInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            try
            {
                if (!IsMassiveWriter) UnitOfWork.BeginTransaction();

                insurancePolicy.InsurancePolicyCode = getInsurancePolicyCode(insurancePolicy);
                UnitOfWork.InsurancePolicies.Add(insurancePolicy);
                UnitOfWork.SaveChanges();

                if (!IsMassiveWriter)
                {
                    UnitOfWork.CommitTransaction();
                    _messageQueueProducer.Send(_queueName, new CustomerInsurancePolicyQueue(EnumPublishQueueType.Added, insurancePolicy.Customer.CustomerCode, insurancePolicy.InsurancePolicyCode));
                }

                return insurancePolicy.InsurancePolicyCode;
            }
            catch
            {
                if (!IsMassiveWriter) UnitOfWork.RollbackTransaction();
                _countError++;
                throw;
            }
        }

        public void EnqueueAddedInsurancePolicies(IEnumerable<CustomerInsurancePolicy> customerInsurancePolicies)
        {
            foreach (var item in customerInsurancePolicies)
                _messageQueueProducer.Send(_queueName, new CustomerInsurancePolicyQueue(EnumPublishQueueType.Added, item.CustomerCode, item.InsurancePolicyCode));
        }

        public int DeleteInsurancePolicy(string insurancePolicyCode)
        {
            try
            {
                int countRow = 0;
                var insurancePolicy = UnitOfWork.InsurancePolicies.GetInsurancePolicy(insurancePolicyCode).FirstOrDefault();
                if (insurancePolicy == null)
                    return countRow;

                if (!IsMassiveWriter) UnitOfWork.BeginTransaction();

                UnitOfWork.InsurancePolicies.Remove(insurancePolicy);
                countRow = UnitOfWork.SaveChanges();

                if (!IsMassiveWriter)
                {
                    UnitOfWork.CommitTransaction();
                    _messageQueueProducer.Send(_queueName, new CustomerInsurancePolicyQueue(EnumPublishQueueType.Deleted, insurancePolicy.Customer.CustomerCode, insurancePolicy.InsurancePolicyCode));
                }

                return countRow;
            }
            catch
            {
                if (!IsMassiveWriter) UnitOfWork.RollbackTransaction();
                _countError++;
                throw;
            }
        }

        public void EnqueueDeletedInsurancePolicies(IEnumerable<CustomerInsurancePolicy> customerInsurancePolicies)
        {
            foreach (var item in customerInsurancePolicies)
                _messageQueueProducer.Send(_queueName, new CustomerInsurancePolicyQueue(EnumPublishQueueType.Deleted, item.CustomerCode, item.InsurancePolicyCode));
        }

        public override void Dispose()
        {
            if (IsMassiveWriter && UnitOfWork.IsTransactionOpened)
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
