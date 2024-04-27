using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class LegalProtection:AuditableEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderType Gender { get; set; }
        public bool IsUnderage { get; set; }
        public bool IsDisabled { get; set; }
        public bool HasLegalProtectionPrivateLife { get; set; }
        public bool HasLegalProtectionProfessionalLife { get; set; }

        public virtual KinshipRelationshipType KinshipRelationshipType { get; set; }

        public virtual InsurancePolicy InsurancePolicy { get; set; }

    }
}
