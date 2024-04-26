using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class IncomeClassType
    {
        public byte Id { get; set; }

        public string NameIncomeClass { get; set; }

        public string DescriptionIncomeClass { get; set; }

        public double MinAnnualGrossIncome { get; set; }

        public double? MaxAnnualGrossIncome { get; set; }
    }
}
