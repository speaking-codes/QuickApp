using DAL.BuilderModel.Interfaces;
using DAL.BuilderModelTemplate;
using DAL.Enums;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel
{
    public class FamilyInsurancePolicyBuilder : InsurancePolicyBuilder, IFamilyInsurancePolicyBuilder
    {
        private FamilyInsurancePolicy _familyInsurancePolicy;

        public IFamilyInsurancePolicyBuilder SetFirstNameAndGender(IList<FirstNameTemplate> firstNameTemplates)
        {
            var i = Random.Next(firstNameTemplates.Count);
            _familyInsurancePolicy.FirstName = firstNameTemplates[i].FirstName;
            _familyInsurancePolicy.Gender = firstNameTemplates[i].IsMale ? EnumGender.Uomo : EnumGender.Donna;
            return this;
        }
        public IFamilyInsurancePolicyBuilder SetLastName(IList<string> lastNames)
        {
            var i = Random.Next(lastNames.Count);
            _familyInsurancePolicy.LastName = lastNames[i];
            return this;
        }
        public IFamilyInsurancePolicyBuilder SetBirthDate()
        {
            var age = Random.Next(0, 90);
            var days = Random.Next(1, 365);
            _familyInsurancePolicy.BirthDate = DateTime.Now.AddYears(-age).AddDays(-days);
            return this;
        }
        public IFamilyInsurancePolicyBuilder SetUnderage()
        {
            var age = Math.Round(DateTime.Now.Subtract(_familyInsurancePolicy.BirthDate).TotalDays / 360, 0);
            _familyInsurancePolicy.IsUnderage = age < 18;
            return this;
        }
        public IFamilyInsurancePolicyBuilder SetDisabled()
        {
            _familyInsurancePolicy.IsDisabled = Random.Next() % 2 == 0;
            return this;
        }
        public IFamilyInsurancePolicyBuilder SetKinshipRelationshipType(IList<KinshipRelationshipType> kinshipRelationshipTypes)
        {
            var i = Random.Next(kinshipRelationshipTypes.Count);
            _familyInsurancePolicy.KinshipRelationshipType = kinshipRelationshipTypes[i];
            return this;
        }

         public override IInsurancePolicyBuilder SetInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            base.SetInsurancePolicy(insurancePolicy);
            _familyInsurancePolicy = new FamilyInsurancePolicy(InsurancePolicy);
            return this;
        }

        public override IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories)
        {
            InsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.FamiliareeCongiunto).FirstOrDefault();
            return this;
        }

        public override IInsurancePolicyBuilder SetDetailItem(InsurancePolicyTemplate insurancePolicyTemplate)
        {
            return this.SetFirstNameAndGender(insurancePolicyTemplate.FirstNameTemplates)
                       .SetLastName(insurancePolicyTemplate.LastNames)
                       .SetBirthDate()
                       .SetUnderage()
                       .SetUnderage()
                       .SetDisabled()
                       .SetKinshipRelationshipType(insurancePolicyTemplate.KinshipRelationshipTypes);
        }
    }
}
