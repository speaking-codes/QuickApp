using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CustomerLearningFeatureCopy
    {
        public long Id { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthMonth { get; set; }
        public string YearBirth { get; set; }
        public string MaritalStatus { get; set; }
        public bool IsDependentSpouse { get; set; }
        public int ChildrenNumbers { get; set; }
        public int DependentChildrenNumber { get; set; }
        public string ProfessionType { get; set; }
        public double Income { get; set; }
        public string IncomeType { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public byte InsurancePolicyId { get; set; }
        public string InsurancePolicyCode { get; set; }
    }
}
