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
    public class SportEventBuilder : InsurancePolicyBuilder, ISportEventBuilder
    {
        public override IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories)
        {
            InsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.RC_DIVERSI).FirstOrDefault();
            return this;
        }

        public override IInsurancePolicyBuilder SetDetailItems(InsurancePolicyTemplate insurancePolicyTemplate)
        {
            var maxSportEvents = Random.Next(1, 3);
            InsurancePolicy.SportEvents=new List<SportEvent>();
            SportEvent sportEvent;

            for (var i = 0; i < maxSportEvents; i++)
            {
                sportEvent= new SportEvent();
                sportEvent = sportEvent.SetStartDate(Random)
                                       .SetEndDate(Random)
                                       .SetSportEventType(Random, insurancePolicyTemplate.SportEventTypes)
                                       .SetMunicipality(Random, insurancePolicyTemplate.Municipalities)
                                       .SetNumberMembers(Random)
                                       .SetLocation(Random, insurancePolicyTemplate.AddressTemplate)
                                       .SetIsCompetitive(Random);
                InsurancePolicy.SportEvents.Add(sportEvent);
            }
            return this;
        }

    }
}
