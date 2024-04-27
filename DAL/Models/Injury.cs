using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Injury:AuditableEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderType Gender { get; set; }
        public bool IsInjuryPrivateLife { get; set; }
        public bool IsInjuryProfessionalLine { get; set; }

        public virtual KinshipRelationshipType KinshipRelationshipType { get; set; }

        public virtual InsurancePolicy InsurancePolicy { get; set; }
    }
}
