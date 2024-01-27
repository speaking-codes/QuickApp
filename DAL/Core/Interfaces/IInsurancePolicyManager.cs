using DAL.Models;
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
        string AddInsurancePolicy(InsurancePolicy insurancePolicy);
    }
}
