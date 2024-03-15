using DAL.BuilderModel.Interfaces;
using DAL.BuilderModelTemplate;
using DAL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel
{
    public class BykeInsurancePolicyBuilder : InsurancePolicyBuilder, IBykeInsurancePolicyBuilder
    {
        private VehicleInsurancePolicy _vehicleInsurancePolicy;


        public IBykeInsurancePolicyBuilder SetConfigurationModel(IList<ConfigurationModel> bykeConfigurationModels)
        {
            var i = Random.Next(0, bykeConfigurationModels.Count);
            _vehicleInsurancePolicy.ConfigurationModel = bykeConfigurationModels[i];
            return this;
        }

        public IBykeInsurancePolicyBuilder SetLicensePlate()
        {
            _vehicleInsurancePolicy.LicensePlate = Utilities.GenerateLicensePlate(Random);
            return this;
        }

        public IBykeInsurancePolicyBuilder SetCommercialValue()
        {
            _vehicleInsurancePolicy.CommercialValue = 25000 * (1 + Random.NextDouble());
            return this;
        }

        public IBykeInsurancePolicyBuilder SetInsuredValue()
        {
            _vehicleInsurancePolicy.InsuredValue = _vehicleInsurancePolicy.CommercialValue * 1.15;
            return this;
        }

        public IBykeInsurancePolicyBuilder SetRiskCategory()
        {
            _vehicleInsurancePolicy.RiskCategory = (byte)Random.Next(1, 14);
            return this;
        }

        public override IInsurancePolicyBuilder SetInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            base.SetInsurancePolicy(insurancePolicy);
            _vehicleInsurancePolicy =new VehicleInsurancePolicy(InsurancePolicy);
            return this;
        }

        public override IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories)
        {
            InsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.Moto).FirstOrDefault();
            return this;
        }

        public override IInsurancePolicyBuilder SetDetailItem(InsurancePolicyTemplate insurancePolicyTemplate)
        {
            return this.SetConfigurationModel(insurancePolicyTemplate.CarConfigurationModels)
                       .SetLicensePlate()
                       .SetCommercialValue()
                       .SetInsuredValue()
                       .SetRiskCategory();
        }

        public new VehicleInsurancePolicy Build() => _vehicleInsurancePolicy;
    }
}
