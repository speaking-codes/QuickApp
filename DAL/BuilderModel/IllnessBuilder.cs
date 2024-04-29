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
    public class IllnessBuilder : InsurancePolicyBuilder, IIllnessBuilder
    {
        public override IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories)
        {
            InsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.MALATTIA).FirstOrDefault();
            return this;
        }

        public override IInsurancePolicyBuilder SetDetailItems(InsurancePolicyTemplate insurancePolicyTemplate)
        {
            var maxIllness = Random.Next(1, 4);
            InsurancePolicy.Illnesses = new List<Illness>();
            Illness illness;

            for (var i = 0; i < maxIllness; i++)
            {
                illness = new Illness();
                illness = illness.SetFirstNameAndGender(Random, insurancePolicyTemplate.FirstNameTemplates)
                                .SetLastName(Random, insurancePolicyTemplate.LastNames)
                                .SetBirthDate(Random)
                                .SetKinshipRelationshipType(Random, insurancePolicyTemplate.KinshipRelationshipTypes);
                InsurancePolicy.Illnesses.Add(illness);
            }
            return this;
        }
    }
}
