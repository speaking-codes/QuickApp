using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class LearningTraining
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int MaritalStatusId { get; set; }
        public int FamilyTypeId { get; set; }
        public int ChildrenNumbers { get; set; }
        public int IncomeTypeId { get; set; }
        public int ProfessionTypeId { get; set; }
        public double Income { get; set; }
        public int RegionId { get; set; }
        public int InsurancePolicyCategoryId { get; set; }
        public int RenewalNumber { get; set; }
        public double? NormalizedRenewalNumber { get; set; }
    }
}
