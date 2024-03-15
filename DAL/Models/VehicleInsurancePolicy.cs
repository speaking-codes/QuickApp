using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class VehicleInsurancePolicy : InsurancePolicy
    {
        public string LicensePlate { get; set; }
        public double CommercialValue { get; set; }
        public double InsuredValue { get; set; }
        public byte RiskCategory { get; set; }

        public virtual ConfigurationModel ConfigurationModel { get; set; }

        public VehicleInsurancePolicy()
        {

        }

        public VehicleInsurancePolicy(InsurancePolicy insurancePolicy)
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
