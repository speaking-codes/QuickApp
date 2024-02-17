using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PetInsurancePolicy: InsurancePolicy
    {
        public string PetIdentificationCode { get; set; }
        public string PetName { get; set; }
        public DateTime PetBirthDate { get; set; }

        public virtual BreedPetDetailType BreedPetDetailType { get; set; }
    }
}
