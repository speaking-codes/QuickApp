using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class JobRalRatingCoefficient : AuditableEntity
    {
        public byte Id { get; set; }
        public double MinRal { get; set; }
        public double? MaxRal { get; set; }
        public float RatingCoefficient { get; set; }
    }
}
