using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PetInsurancePolicy: InsurancePolicy
    {
        public string PetIdentificationCode { get; set; }
        public string PetName { get; set; }
        public DateTime PetBirthDate { get; set; }

        public virtual BreedPetDetailType BreedPetDetailType { get; set; }

        public PetInsurancePolicy()
        {

        }

        public PetInsurancePolicy(InsurancePolicy insurancePolicy)
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
