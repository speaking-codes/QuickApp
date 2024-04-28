using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.BuilderModel.Interfaces;
using DAL.BuilderModelTemplate;
using DAL.Exstensions;
using DAL.Helpers;
using DAL.Models;

namespace DAL.BuilderModel
{
    public class PetBuilder : InsurancePolicyBuilder, IPetBuilder
    {
        public override IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories)
        {
            InsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.ARD).FirstOrDefault();
            return this;
        }

        public override IInsurancePolicyBuilder SetDetailItems(InsurancePolicyTemplate insurancePolicyTemplate)
        {
            var maxPets = Random.Next(1, 4);
            Pet pet;
            InsurancePolicy.Pets = new List<Pet>();

            for (var i = 0; i < maxPets; i++)
            {
                pet = new Pet();
                pet = pet.SetPetIdentificationCode(Random)
                        .SetPetName(Random, insurancePolicyTemplate.PetNames)
                        .SetPetBirthDate(Random)
                        .SetBreedPetDetailType(Random, insurancePolicyTemplate.BreedPetDetailTypes);
                InsurancePolicy.Pets.Add(pet);
            }
            return this;
        }
    }
}
