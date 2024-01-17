using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Customer : AuditableEntity
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string BirthCounty { get; set; }
        public EnumGender Gender { get; set; }
        public string Profession { get; set; }
        public ContractType ContractType { get; set; }
        public double? RAL { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual IList<Delivery> Deliveries { get; set; }

        public virtual IList<Address> Addresses { get; set; }

        public virtual IList<InsurancePolicy> InsurancePolicies { get; set; } 
    }
}
