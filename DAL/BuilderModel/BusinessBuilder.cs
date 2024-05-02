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
    public class BusinessBuilder : InsurancePolicyBuilder, IBusinessBuilder
    {
        public override IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories)
        {
            InsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.INCENDIO_FURTO).FirstOrDefault();
            return this;
        }

        public override IInsurancePolicyBuilder SetDetailItems(InsurancePolicyTemplate insurancePolicyTemplate)
        {
            var maxHouses = Random.Next(1, 1);
            InsurancePolicy.Businesss = new List<Business>();
            Business business;

            for (var i = 0; i < maxHouses; i++)
            {
                business = new Business();
                business = business.SetBusinessTitle(Random, insurancePolicyTemplate.BusinessTitles)
                            .SetBusinessLocation(Random,insurancePolicyTemplate.AddressTemplate)
                            .SetBusinessType(Random,insurancePolicyTemplate.BusinessTypes)
                            .SetEmployeeNumbers(Random)
                            .SetAnnualRevenue(Random)
                            .SetMunicipality(Random, insurancePolicyTemplate.AddressTemplate.Municipalities);
                InsurancePolicy.Businesss.Add(business);
            }
            return this;
        }
    }
}