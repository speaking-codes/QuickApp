using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class HouseInsurancePolicy : InsurancePolicy
    {
        public float ExtensionSquareMeters { get; set; }

        public int NumberBuildingFloors { get; set; }

        public int HomeFloorNumber { get; set; }

        public string Location { get; set; }

        public virtual Municipality Municipality { get; set; }

        public HouseInsurancePolicy()
        {

        }

        public HouseInsurancePolicy(InsurancePolicy insurancePolicy)
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
