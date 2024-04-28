using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.BuilderModel.Exstensions;
using DAL.BuilderModel.Interfaces;
using DAL.BuilderModelTemplate;
using DAL.Exstensions;
using DAL.Helpers;
using DAL.Models;

namespace DAL.BuilderModel
{
    public class VehicleBuilder : InsurancePolicyBuilder, IVehicleBuilder
    {
        public override IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories)
        {
            InsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.RCA).FirstOrDefault();
            return this;
        }

        public override IInsurancePolicyBuilder SetLuxuryPolicy()
        {
            base.SetLuxuryPolicy();
            InsurancePolicy.IsLuxuryPolicy = InsurancePolicy.IsLuxuryPolicy;
            return this;
        }

        public override IInsurancePolicyBuilder SetDetailItems(InsurancePolicyTemplate insurancePolicyTemplate)
        {
            var maxVehicles = Random.Next(1, 2);
            InsurancePolicy.Vehicles = new List<Vehicle>();
            Vehicle vehicle;

            for (var i = 0; i < maxVehicles; i++)
            {
                vehicle = new Vehicle();
                vehicle = vehicle.SetConfigurationModel(Random, insurancePolicyTemplate.ConfigurationModels)
                                .SetLicensePlate(Random)
                                .SetCommercialValue(Random)
                                .SetInsuredValue()
                                .SetRiskCategory(Random);
                InsurancePolicy.Vehicles.Add(vehicle);
            }
            return this;
        }
    }
}