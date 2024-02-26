using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class FamilyInsurancePolicy : InsurancePolicy
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public EnumGender Gender { get; set; }
        public bool IsUnderage { get; set; }
        public bool IsDisabled { get; set; }

        public virtual KinshipRelationshipType KinshipRelationshipType { get; set; }
    }
}
