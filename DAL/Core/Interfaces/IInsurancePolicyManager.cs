using DAL.Models;
using DAL.QueueModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.Interfaces
{
    public interface IInsurancePolicyManager : IManager
    {
        IList<InsurancePolicy> GetInsurancePolicy(string customerCode);
        IList<InsurancePolicy> GetActiveInsurancePolicy(string customerCode);
        IList<InsurancePolicy> GetExpiredInsurancePolicy(DateTime expireDate);

        string AddInsurancePolicy(InsurancePolicy insurancePolicy);
        void EnqueueAddedInsurancePolicies(IEnumerable<CustomerInsurancePolicy> customerInsurancePolicies);

        int DeleteInsurancePolicy(string insurancePolicyCode);
        void EnqueueDeletedInsurancePolicies(IEnumerable<CustomerInsurancePolicy> customerInsurancePolicies);
    }
}
