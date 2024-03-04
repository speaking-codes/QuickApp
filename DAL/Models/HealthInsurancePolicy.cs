﻿using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class HealthInsurancePolicy : InsurancePolicy
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public EnumGender Gender { get; set; }
        public string TipologiaPrestazione { get; set; }

        public virtual KinshipRelationshipType KinshipRelationshipType { get; set; }

        //public HealthcareProvisionType HealthcareProvisionType { get; set; }
    }
}
