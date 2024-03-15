using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel.Interfaces
{
    public interface IPetInsurancePolicyBuilder : IInsurancePolicyBuilder
    {
        IPetInsurancePolicyBuilder SetPetIdentificationCode();
        IPetInsurancePolicyBuilder SetPetName(IList<string> petNames);
        IPetInsurancePolicyBuilder SetPetBirthDate();
        IPetInsurancePolicyBuilder SetBreedPetDetailType(IList<BreedPetDetailType> breedPetDetailTypes);
        new PetInsurancePolicy Build();
    }
}
