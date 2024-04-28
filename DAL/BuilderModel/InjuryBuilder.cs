using DAL.BuilderModel.Interfaces;
using DAL.BuilderModelTemplate;
using DAL.Enums;
using DAL.Exstensions;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel
{
    public class InjuryBuilder : InsurancePolicyBuilder, IInjuryBuilder
    {
        public override IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories)
        {
            InsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.INFORTUNI).FirstOrDefault();
            return this;
        }

        public override IInsurancePolicyBuilder SetDetailItems(InsurancePolicyTemplate insurancePolicyTemplate)
        {
            var maxInjuries = Random.Next(1, 4);
            InsurancePolicy.Injuries = new List<Injury>();
            Injury injury;

            for (var i = 0; i < maxInjuries; i++)
            {
                injury = new Injury();
                injury = injury.SetFirstNameAndGender(Random, insurancePolicyTemplate.FirstNameTemplates)
                               .SetLastName(Random, insurancePolicyTemplate.LastNames)
                               .SetBirthDate(Random)
                               .SetInjuryPrivateLife(Random)
                               .SetInjuryProfessionalLife(Random)
                               .SetKinshipRelationshipType(Random, insurancePolicyTemplate.KinshipRelationshipTypes);
                InsurancePolicy.Injuries.Add(injury);
            }
        }

    }
}