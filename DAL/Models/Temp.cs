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
        public string FamilyTypeCode { get; set; }
        public int? ChildrenNumbers { get; set; }

        public string IncomeTypeCode { get; set; }
        public string ProfessioneTypeCode { get; set; }
        public string ProfessionTypeCode_1 { get; set; }
        public string ProfessionTypeCode_2 { get; set; }
        public string IncomeBracket { get; set; }

        public string CountryAbbreviation { get; set; }
        public string RegionCode { get; set; }
    }
}