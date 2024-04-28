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
    public class HouseBuilder:InsurancePolicyBuilder,IHouseBuilder
    {
        public override IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories)
        {
            InsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.MULTIGARANZIA_ABITAZIONE).FirstOrDefault();
            return this;
        }

        public override IInsurancePolicyBuilder SetDetailItems(InsurancePolicyTemplate insurancePolicyTemplate)
        {
            var maxHouses = Random.Next(1, 3);
            InsurancePolicy.Houses=new List<House>();
            House house;

            for (var i = 0; i < maxHouses; i++)
            {
                house = new House();
                house = house.SetExtensionSquareMeters(Random)
                            .SetNumberBuildingFloors(Random)
                            .SetHomeFloorNumber(Random)
                            .SetLocation(Random, insurancePolicyTemplate.AddressTemplate)
                            .SetIsFirstHouse(Random)
                            .SetMunicipality(Random, insurancePolicyTemplate.AddressTemplate.Municipalities);
                InsurancePolicy.Houses.Add(house);
            }
            if (InsurancePolicy.Houses.Count(x => x.IsFirstHouse)>1)
            {
                foreach(var item in InsurancePolicy.Houses)
                    item.IsFirstHouse = false;

                InsurancePolicy.Houses[0].IsFirstHouse= true;
            }
            return this;
        }
    }
}
