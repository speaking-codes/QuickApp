using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BaggageLossInsurancePolicy: InsurancePolicy
    {
        public int LuggageHeightMetres { get; set; }
        public int LuggageLengthMetres { get; set; }
        public int LuggageWidthMetres { get; set; }
        public int LuggageWeightKg { get; set; }

        public virtual BaggageType BaggageType { get; set; }
    }
}
