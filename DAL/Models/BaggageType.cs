using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BaggageType : AuditableEntity
    {
        public byte Id { get; set; }
        public string BaggageTypeName { get; set; }

        public virtual IList<BaggageLoss> BaggageLossInsurancePolicies  { get; set; }

    }
}
