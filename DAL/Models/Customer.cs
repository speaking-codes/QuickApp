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
        public DateTime? BirthDate { get; set; }
        public EnumGender Gender { get; set; }
        public EnumMaritalStatus? MaritalStatus { get; set; }
        public byte? ChildrenNumber { get; set; }
        public string JobTitle { get; set; }
        public EnumContractType? ContractType { get; set; }
        public double? Ral { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual Municipality BirthMunicipality { get; set; }

        public virtual IList<Delivery> Deliveries { get; set; }

        public virtual IList<Address> Addresses { get; set; }

        public virtual IList<InsurancePolicy> InsurancePolicies { get; set; } 
    }
}
