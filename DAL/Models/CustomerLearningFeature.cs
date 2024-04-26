using Amazon.Util.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAL.Models
{
    public class CustomerLearningFeature
    {
        public long Id { get; set; }
        public long? CustomerId { get; set; }
        public string Gender { get; set; }
        public string BirthMonth { get; set; }
        public string YearBirth { get; set; }
        public string MaritalStatus { get; set; }
        public bool IsSingle { get; set; }
        public bool IsDependentSpouse { get; set; }
        public int ChildrenNumbers { get; set; }
        public int DependentChildrenNumber { get; set; }
        public string ProfessionType { get; set; }
        public bool IsFreelancer { get; set; }
        public string IncomeClassType { get; set; }
        public string IncomeType { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public byte InsurancePolicyId { get; set; }
        public string InsurancePolicyCode { get; set; }
        public string InsurancePolicyName { get; set; }
        public string InsurancePolicyDescription { get; set; }
        public string WarrantyAvaibles { get; set; }
    }
}
