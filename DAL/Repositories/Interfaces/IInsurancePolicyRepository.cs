using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IInsurancePolicyRepository : IRepository<InsurancePolicy>
    {
        IQueryable<InsurancePolicy> GetInsurancePolicies(string customerCode);
        IQueryable<InsurancePolicy> GetInsurancePolicy(int id);
        IQueryable<InsurancePolicy> GetActiveInsurancePolicies(string customercode);
        int GetInsurancePolicyCategoryCount(string insurancePolicyCategoryCode);
        bool IsExistingInsurancePolicyCategory(string customerCode, string insurancePolicyCategoryCode, DateTime createdDate, DateTime expiryDate);
    }
}
