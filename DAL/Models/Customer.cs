﻿using DAL.Enums;
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
        public byte? ChildrenNumber { get; set; }
        public double? Income { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public byte? FamilyTypeId { get; set; }
        public virtual FamilyType FamilyType { get; set; }

        public byte? MaritalStatusId { get; set; }
        public virtual MaritalStatusType MaritalStatus { get; set; }

        public short? BirthMunicipalityId { get; set; }
        public virtual Municipality BirthMunicipality { get; set; }

        public byte? ContractTypeId { get; set; }
        public virtual ContractType ContractType { get; set; }

        public byte? IncomeTypeId { get; set; }
        public virtual IncomeType IncomeType { get; set; }

        public bool IsFreelancer { get; set; }
        public short? ProfessionTypeId { get; set; }
        public virtual ProfessionType ProfessionType { get; set; }

        public virtual IList<Delivery> Deliveries { get; set; }

        public virtual IList<Address> Addresses { get; set; }

        public virtual IList<InsurancePolicy> InsurancePolicies { get; set; } 
    }
}
