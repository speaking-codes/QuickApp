using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class KinshipRelationshipType:AuditableEntity
    {
        public byte Id { get; set; }
        public string KinshipRelationshipTypeName { get; set; }

        public virtual IList<FamilyInsurancePolicy > FamilyInsurancePolicies { get; set; }

        public virtual IList<HealthInsurancePolicy> HealthInsurancePolicies { get; set; }
    }
}
