using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AgeRatingCoefficient : AuditableEntity
    {
        public byte Id { get; set; }
        public int MinAge { get; set; }
        public int? MaxAge { get; set; }
        public float RatingCoefficient { get; set; }
        public float ChildrenNumberRatingCoefficient { get; set; }
    }
}
