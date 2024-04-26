using DAL.BuilderModel.Interfaces;
using DAL.BuilderModelTemplate;
using DAL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel
{
    public class PetInsurancePolicyBuilder : InsurancePolicyBuilder, IPetInsurancePolicyBuilder
    {
        private PetInsurancePolicy _petInsurancePolicy;

        public IPetInsurancePolicyBuilder SetPetIdentificationCode()
        {
            _petInsurancePolicy.PetIdentificationCode = Utilities.GeneratePetIdentificationCode(Random);
            return this;
        }
        public IPetInsurancePolicyBuilder SetPetName(IList<string> petNames)
        {
            var i = Random.Next(petNames.Count);
            _petInsurancePolicy.PetName = petNames[i];
            return this;
        }
        public IPetInsurancePolicyBuilder SetPetBirthDate()
        {
            var age = Random.Next(0, 3);
            var days = Random.Next(1, 365);
            _petInsurancePolicy.PetBirthDate = DateTime.Now.AddYears(-age).AddDays(-days);
            return this;
        }
        public IPetInsurancePolicyBuilder SetBreedPetDetailType(IList<BreedPetDetailType> breedPetDetailTypes)
        {
            var i = Random.Next(breedPetDetailTypes.Count);
            _petInsurancePolicy.BreedPetDetailType = breedPetDetailTypes[i];
            return this;
        }

        public override IInsurancePolicyBuilder SetInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            base.SetInsurancePolicy(insurancePolicy);
            _petInsurancePolicy = new PetInsurancePolicy(InsurancePolicy);
            return this;
        }

        public override IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories)
        {
            _petInsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.ARD).FirstOrDefault();
            return this;
        }

        public override IInsurancePolicyBuilder SetCustomer(Customer customer)
        {
            _petInsurancePolicy.Customer = customer;
            return this;
        }

        public override IInsurancePolicyBuilder SetIssueDate()
        {
            base.SetIssueDate();
            _petInsurancePolicy.IssueDate = InsurancePolicy.IssueDate;
            return this;
        }

        public override IInsurancePolicyBuilder SetExpiryDate()
        {
            base.SetExpiryDate();
            _petInsurancePolicy.ExpiryDate = InsurancePolicy.ExpiryDate;
            return this;
        }

        public override IInsurancePolicyBuilder SetInsuredMaximum()
        {
            base.SetInsuredMaximum();
            _petInsurancePolicy.InsuredMaximum = InsurancePolicy.InsuredMaximum;
            return this;
        }

        public override IInsurancePolicyBuilder SetTotalPrize()
        {
            base.SetTotalPrize();
            _petInsurancePolicy.TotalPrize = InsurancePolicy.TotalPrize;
            return this;
        }

        public override IInsurancePolicyBuilder SetLuxuryPolicy()
        {
            base.SetLuxuryPolicy();
            _petInsurancePolicy.IsLuxuryPolicy = InsurancePolicy.IsLuxuryPolicy;
            return this;
        }

        public override IInsurancePolicyBuilder SetWarranties()
        {
            _petInsurancePolicy.WarrantySelecteds = new List<WarrantySelected>();
            _petInsurancePolicy.WarrantySelecteds.Add(new WarrantySelected { WarrantyAvaible = _petInsurancePolicy.InsurancePolicyCategory.WarrantyAvaibles.Where(x => x.IsPrimary).FirstOrDefault() });
            var warranties = _petInsurancePolicy.InsurancePolicyCategory.WarrantyAvaibles.Where(x => !x.IsPrimary).ToList();

            while (_petInsurancePolicy.WarrantySelecteds.Count < 3)
            {
                var index = Random.Next(warranties.Count);
                if (!_petInsurancePolicy.WarrantySelecteds.Any(x => x.WarrantyAvaible.Id == warranties[index].Id))
                    _petInsurancePolicy.WarrantySelecteds.Add(new WarrantySelected { WarrantyAvaible = warranties[index] });
            }

            return this;
        }
        
        public override IInsurancePolicyBuilder SetDetailItem(InsurancePolicyTemplate insurancePolicyTemplate)
        {
            return this.SetPetIdentificationCode()
                       .SetPetName(insurancePolicyTemplate.PetNames)
                       .SetPetBirthDate()
                       .SetBreedPetDetailType(insurancePolicyTemplate.BreedPetDetailTypes);

        }
        public override PetInsurancePolicy Build() => _petInsurancePolicy;
    }
}
