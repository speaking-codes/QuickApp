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
    public class CarInsurancePolicyBuilder : InsurancePolicyBuilder, ICarInsurancePolicyBuilder
    {
        private VehicleInsurancePolicy _vehicleInsurancePolicy;

        public override IInsurancePolicyBuilder SetInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            base.SetInsurancePolicy(insurancePolicy);
            _vehicleInsurancePolicy = (VehicleInsurancePolicy)InsurancePolicy;
            return this;
        }

        public ICarInsurancePolicyBuilder SetConfigurationModel(IList<ConfigurationModel> carConfigurationModels)
        {
            var i = Random.Next(0, carConfigurationModels.Count);
            _vehicleInsurancePolicy.ConfigurationModel = carConfigurationModels[i];
            return this;
        }
      
        public ICarInsurancePolicyBuilder SetLicensePlate()
        {
            _vehicleInsurancePolicy.LicensePlate = Utilities.GenerateLicensePlate(Random);
            return this;
        }

        public ICarInsurancePolicyBuilder SetCommercialValue()
        {
            switch (_vehicleInsurancePolicy.ConfigurationModel.Model.Brand.BrandType.Id)
            {
                case 1://   BASE
                    _vehicleInsurancePolicy.CommercialValue = 26000 * (1 + Random.NextDouble());
                    break;
                case 2://   LUSSO
                    _vehicleInsurancePolicy.CommercialValue = 100000 * (1 + Random.NextDouble());
                    break;
                case 3:// PLUS
                    _vehicleInsurancePolicy.CommercialValue = 45000 * (1 + Random.NextDouble());
                    break;
                case 4: //  SUPER LUSSO
                    _vehicleInsurancePolicy.CommercialValue = 150000 * (1 + Random.NextDouble());
                    break;
            }
            return this;
        }

        public ICarInsurancePolicyBuilder SetInsuredValue()
        {
            _vehicleInsurancePolicy.InsuredValue = _vehicleInsurancePolicy.CommercialValue * 1.15;
            return this;
        }

        public ICarInsurancePolicyBuilder SetRiskCategory()
        {
            _vehicleInsurancePolicy.RiskCategory = (byte)Random.Next(1, 14);
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
