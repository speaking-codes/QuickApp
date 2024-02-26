using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class TravelMeansType : AuditableEntity
    {
        public byte Id { get; set; }
        public string TravelMeansTypeName { get; set; }

        public virtual IList<TravelClassType> TravelClassTypes { get; set; }

        public virtual IList<Travel> Travels { get; set; }
    }
}
