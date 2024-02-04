﻿using DAL.Core.Interfaces;
using DAL.Models;
using DAL.ModelsRabbitMQ;
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

        public string AddInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            try
            {
                if (!IsMassiveWriter) UnitOfWork.BeginTransaction();

                if (UnitOfWork.InsurancePolicies.IsExistingInsurancePolicyCategory(insurancePolicy.Customer.CustomerCode,
                                                                                   insurancePolicy.InsurancePolicyCategory.InsurancePolicyCategoryCode, 
                                                                                   insurancePolicy.IssueDate, 
                                                                                   insurancePolicy.ExpiryDate))
                    return string.Empty;

                insurancePolicy.InsurancePolicyCode = getInsurancePolicyCode(insurancePolicy);
                UnitOfWork.InsurancePolicies.Add(insurancePolicy);
                UnitOfWork.SaveChanges();

                if (!IsMassiveWriter) UnitOfWork.CommitTransaction();

                _messageQueueProducer.Send(_queueName, new CustomerInsurancePolicyQueue(Enums.EnumPublishQueueType.Created, insurancePolicy.Customer.CustomerCode, insurancePolicy.InsurancePolicyCategory.InsurancePolicyCategoryCode));

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