using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati.Models
{
    public class VehicleInsurancePolicyBuilder : InsurancePolicyBuilder
    {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string digit = "0123456789";

        private IList<ConfigurationModel> _carConfigurationModels;
        private IList<ConfigurationModel> _bykeConfigurationModels;

        private string generateRandomCode(int length, string pattern)
        {
            return new string(Enumerable.Repeat(pattern, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private VehicleInsurancePolicy _vehicleInsurancePolicy;

        public VehicleInsurancePolicyBuilder(ServiceProvider provider, InsurancePolicy insurancePolicy) : base(provider)
        {
            _vehicleInsurancePolicy = new VehicleInsurancePolicy(insurancePolicy);

            _carConfigurationModels = UnitOfWork.ConfigurationModels.GetConfigurationModels()
                                                           .Where(x => !x.ModelType.IsByke && x.ModelType.IsActive)
                                                           .ToList();
            _bykeConfigurationModels = UnitOfWork.ConfigurationModels.GetConfigurationModels()
                                                                   .Where(x => x.ModelType.IsByke && x.ModelType.IsActive)
                                                                   .ToList();
        }

        public VehicleInsurancePolicyBuilder SetConfigurationModel()
        {
            if (_vehicleInsurancePolicy.InsurancePolicyCategory.Id == 1)//Auto
            {
                var index = _random.Next(0, _carConfigurationModels.Count);
                _vehicleInsurancePolicy.ConfigurationModel = _carConfigurationModels[index];
            }
            else if (_vehicleInsurancePolicy.InsurancePolicyCategory.Id == 2)//Moto
            {
                var index = _random.Next(0, _bykeConfigurationModels.Count);
                _vehicleInsurancePolicy.ConfigurationModel = _bykeConfigurationModels[index];
            }
            return this;
        }

        public VehicleInsurancePolicyBuilder SetLicensePlate()
        {
            _vehicleInsurancePolicy.LicensePlate = generateRandomCode(2, chars) + generateRandomCode(3, digit) + generateRandomCode(2, chars);
            return this;
        }

        public VehicleInsurancePolicyBuilder SetCommercialValue()
        {
            if (_vehicleInsurancePolicy.ConfigurationModel == null) return this;

            switch (_vehicleInsurancePolicy.ConfigurationModel.Model.Brand.BrandType.Id)
            {
                case 1://   BASE
                    _vehicleInsurancePolicy.IsLuxuryPolicy = false;
                    _vehicleInsurancePolicy.CommercialValue = 21000 * (1 + _random.NextDouble());
                    break;
                case 2://   LUSSO
                    _vehicleInsurancePolicy.IsLuxuryPolicy = true;
                    _vehicleInsurancePolicy.CommercialValue = 100000 * (1 + _random.NextDouble());
                    break;
                case 3:// PLUS
                    _vehicleInsurancePolicy.IsLuxuryPolicy = false;
                    _vehicleInsurancePolicy.CommercialValue = 42000 * (1 + _random.NextDouble());
                    break;
                case 4: //  SUPER LUSSO
                    _vehicleInsurancePolicy.IsLuxuryPolicy = true;
                    _vehicleInsurancePolicy.CommercialValue = 150000 * (1 + _random.NextDouble());
                    break;
                case 5://   CICLO MOTORE
                    _vehicleInsurancePolicy.IsLuxuryPolicy = false;
                    _vehicleInsurancePolicy.CommercialValue = 15000 * (1 + _random.NextDouble());
                    break;
                default:
                    break;
            }
            return this;
        }

        public VehicleInsurancePolicyBuilder SetInsuredValue()
        {
            _vehicleInsurancePolicy.InsuredValue = _vehicleInsurancePolicy.CommercialValue * 0.85;
            return this;
        }

        public VehicleInsurancePolicyBuilder SetRiskCategory()
        {
            _vehicleInsurancePolicy.RiskCategory = (byte)_random.Next(1, 14);
            return this;
        }

        public override VehicleInsurancePolicy Build() => _vehicleInsurancePolicy;
    }
}
