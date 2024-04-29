using DAL.BuilderModel.Interfaces;
using DAL.BuilderModelTemplate;
using DAL.Exstensions;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel
{
    public class LegalProtectionBuilder : InsurancePolicyBuilder, ILegalProtectionBuilder
    {
        public override IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories)
        {
            InsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.TUTELA_GIUDIZIARIA).FirstOrDefault();
            return this;
        }

        public override IInsurancePolicyBuilder SetDetailItems(InsurancePolicyTemplate insurancePolicyTemplate)
        {
            var maxLegalProtection = Random.Next(1, 4);
            InsurancePolicy.LegalProtections = new List<LegalProtection>();
            LegalProtection legalProtection;

            for (var i = 0; i < maxLegalProtection; i++)
            {
                legalProtection = new LegalProtection();
                legalProtection = legalProtection.SetFirstNameAndGender(Random, insurancePolicyTemplate.FirstNameTemplates)
                                .SetLastName(Random, insurancePolicyTemplate.LastNames)
                                .SetBirthDate(Random)
                                .SetKinshipRelationshipType(Random, insurancePolicyTemplate.KinshipRelationshipTypes)
                                .SetIsDisabled(Random)
                                .SetHasLegalProtectionPrivateLife(Random)
                                .SetHasLegalProtectionProfessionalLife(Random);
                InsurancePolicy.LegalProtections.Add(legalProtection);
            }
            return this;
        }
    }
}
