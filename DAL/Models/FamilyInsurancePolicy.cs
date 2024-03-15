using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class FamilyInsurancePolicy : InsurancePolicy
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public EnumGender Gender { get; set; }
        public bool IsUnderage { get; set; }
        public bool IsDisabled { get; set; }

        public virtual KinshipRelationshipType KinshipRelationshipType { get; set; }

        public FamilyInsurancePolicy()
        {

        }

        public FamilyInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            this.InsurancePolicyCode = insurancePolicy.InsurancePolicyCode;
            this.Progressive = insurancePolicy.Progressive;
            this.IssueDate = insurancePolicy.IssueDate;
            this.ExpiryDate = insurancePolicy.ExpiryDate;
            this.InsuredMaximum = insurancePolicy.InsuredMaximum;
            this.TotalPrize = insurancePolicy.TotalPrize;
            this.IsLuxuryPolicy = insurancePolicy.IsLuxuryPolicy;

            this.InsurancePolicyCategory = insurancePolicy.InsurancePolicyCategory;
            this.Customer = insurancePolicy.Customer;
        }
    }
}
