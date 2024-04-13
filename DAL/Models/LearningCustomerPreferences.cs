using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class LearningCustomerPreferences
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CustomerCode { get; set; }
        public string Gender { get; set; }
        public int AgeClass { get; set; }
        public string MaritalStatus { get; set; }
        public string FamilyType { get; set; }
        public int? ChildrenNumbers { get; set; }
        public string IncomeTypeClass { get; set; }
        public string ProfessionType { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string InsurancePolicyCategory { get; set; }
    }
}
