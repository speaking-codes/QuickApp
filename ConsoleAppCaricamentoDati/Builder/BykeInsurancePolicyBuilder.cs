using ConsoleAppCaricamentoDati.Helpers;
using DAL;
using DAL.Models;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati.Builder
{
    public class BykeInsurancePolicyBuilder : InsurancePolicyBuilder
    {
        private readonly IList<ConfigurationModel> _configurationModels;
        private int _index;

        public BykeInsurancePolicyBuilder(InsurancePolicyCategory insurancePolicyCategory, Customer customer, IUnitOfWork unitOfWork) :
            base(insurancePolicyCategory, customer, unitOfWork)
        {
            _configurationModels = UnitOfWork.ConfigurationModels
                                           .GetConfigurationModels()
                                           .Where(x => x.ModelType.IsByke && x.Model.Brand.BrandType.IsByke)
                                           .ToList();
            _index = 0;
        }

        protected override InsurancePolicy NewInsurancePolicy() => new VehicleInsurancePolicy();

        public override InsurancePolicyBuilder SetIsLuxuryPolicy()
        {
            InsurancePolicy.IsLuxuryPolicy = _configurationModels[_index].Model.Brand.BrandType.IsLuxury; ;
            return this;
        }

        public override InsurancePolicyBuilder SetDetailItem()
        {
            _index = Random.Next(0, _configurationModels.Count);
            var commercialValue = 25000 * (1 + Random.NextDouble());

            ((VehicleInsurancePolicy)InsurancePolicy).LicensePlate = Utility.GenerateRandomCode(2,Utility.Chars, Random) + 
                                                                     Utility.GenerateRandomCode(3,Utility.Digit, Random) +
                                                                     Utility.GenerateRandomCode(2, Utility.Chars, Random);

            ((VehicleInsurancePolicy)InsurancePolicy).ConfigurationModel = _configurationModels[_index];
            ((VehicleInsurancePolicy)InsurancePolicy).CommercialValue = commercialValue;
            ((VehicleInsurancePolicy)InsurancePolicy).InsuredValue = commercialValue * 0.95;
            ((VehicleInsurancePolicy)InsurancePolicy).RiskCategory = (byte)Random.Next(1, 14);

            return this;
        }
    }
}
