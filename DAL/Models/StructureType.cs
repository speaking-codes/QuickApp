using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class StructureType : AuditableEntity
    {
        public byte Id { get; set; }
        public string StructureTypeName { get; set; }

        public virtual IList<VacationInsurancePolicy> VacationInsurancePolicies { get; set; }
    }
}
