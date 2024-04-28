using DAL.BuilderModel.Interfaces;
using DAL.BuilderModelTemplate;
using DAL.Exstensions;
using DAL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel
{
    public class LargeBuildingBuilder : InsurancePolicyBuilder, ILargeBuildingBuilder
    {
        public override IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories)
        {
            InsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.GLOBALE_FABBRICATI).FirstOrDefault();
            return this;
        }

        public override IInsurancePolicyBuilder SetDetailItems(InsurancePolicyTemplate insurancePolicyTemplate)
        {
            var maxLargeBuildings = Random.Next(1, 5);
            InsurancePolicy.LargeBuildings = new List<LargeBuilding>();
            LargeBuilding largeBuilding;

            for (var i = 0; i < maxLargeBuildings; i++)
            {
                largeBuilding = new LargeBuilding();
                largeBuilding = largeBuilding.SetLocation(Random, insurancePolicyTemplate.AddressTemplate)
                                             .SetBuildingNumbers(Random)
                                             .SetRoomNumbers(Random)
                                             .SetExtensionSquareMeters(Random)
                                             .SetPercentageResidentialUse(Random)
                                             .SetHasCommercialActivities(Random)
                                             .SetCommercialActivityNumbers(Random)
                                             .SetMunicipality(Random, insurancePolicyTemplate.Municipalities);
                InsurancePolicy.LargeBuildings.Add(largeBuilding);
            }
            return this;
        }
    }
}
