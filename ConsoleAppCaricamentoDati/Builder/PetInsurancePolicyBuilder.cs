using ConsoleAppCaricamentoDati.Helpers;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati.Builder
{
    public class PetInsurancePolicyBuilder : InsurancePolicyBuilder
    {
        private readonly IList<BreedPetDetailType> _breedPetDetailTypes;

        public PetInsurancePolicyBuilder(InsurancePolicyCategory insurancePolicyCategory, Customer customer, IUnitOfWork unitOfWork) :
            base(insurancePolicyCategory, customer, unitOfWork)
        {
            _breedPetDetailTypes = UnitOfWork.BreedPetDetailTypes.GetAll();
        }

        protected override InsurancePolicy NewInsurancePolicy() => new PetInsurancePolicy();

        public override InsurancePolicyBuilder SetIsLuxuryPolicy()
        {
            InsurancePolicy.IsLuxuryPolicy = false;
            return this;
        }

        public override InsurancePolicyBuilder SetDetailItem()
        {
            var indexBreedPetDetail = Random.Next(0, _breedPetDetailTypes.Count);
            var age = Random.Next(0, 3);
            var days = Random.Next(1, 365);

            ((PetInsurancePolicy)InsurancePolicy).PetIdentificationCode = Utils.GenerateRandomCode(10, Utils.Digit, Random);
            ((PetInsurancePolicy)InsurancePolicy).PetName = Random.NextDouble().ToString();
            ((PetInsurancePolicy)InsurancePolicy).PetBirthDate = DateTime.Now.AddYears(-age).AddDays(-days);
            ((PetInsurancePolicy)InsurancePolicy).BreedPetDetailType = _breedPetDetailTypes[indexBreedPetDetail];

            return this;
        }
    }
}
