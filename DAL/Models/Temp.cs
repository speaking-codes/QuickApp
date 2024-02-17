using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Temp
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        
        public string MaritalStatusCode { get; set; }
        public int? MaritalStatusId { get; set; }
        
        public string FamilyTypeCode { get; set; }
        public int? FamilyTypeId { get; set; }
        public int? ChildrenNumbers { get; set; }
        
        public string IncomeTypeCode { get; set; }
        public int? IncomeTypeId { get; set; }

        public string ProfessioneTypeCode { get; set; }
        public int? ProfessionTypeId { get; set; }
        public double? Income { get; set; }

        public string RegionCode { get; set; }
        public int? RegionId { get; set; }
    }
}
