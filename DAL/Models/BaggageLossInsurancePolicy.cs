using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BaggageLoss
    {
        public int Id { get; set; }
        public double HeightMetres { get; set; }
        public double LengthMetres { get; set; }
        public double WidthMetres { get; set; }
        public double WeightKg { get; set; }

        public virtual BaggageType BaggageType { get; set; }

        public virtual InsurancePolicy InsurancePolicy { get; set; }
    }
}
