using DAL.BuilderModel.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel
{
    public class PetInsurancePolicyBuilder:InsurancePolicyBuilder, IPetInsurancePolicyBuilder
    {
        public IPetInsurancePolicyBuilder SetBreedPetDetailType(IList<BreedPetDetailType> breedPetDetailTypes)=>this;
        public new PetInsurancePolicy Build() => new PetInsurancePolicy();
    }
}
