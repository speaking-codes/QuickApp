using ConsoleAppCaricamentoDati.Helpers;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleAppCaricamentoDati.Builder
{
    public class CarInsurancePolicyBuilder : InsurancePolicyBuilder
    {
        private readonly IList<ConfigurationModel> _configurationModels;
        private int _index;

        public CarInsurancePolicyBuilder(InsurancePolicyCategory insurancePolicyCategory, Customer customer, IUnitOfWork unitOfWork) :
            base(insurancePolicyCategory, customer, unitOfWork)
        {
            _configurationModels = UnitOfWork.ConfigurationModels
                                   .GetConfigurationModels()
                                   .Where(x => !x.ModelType.IsByke && !x.Model.Brand.BrandType.IsByke)
                                   .ToList();
            _index = 0;
        }

        protected override InsurancePolicy NewInsurancePolicy() => new VehicleInsurancePolicy();

        public override InsurancePolicyBuilder SetIsLuxuryPolicy()
        {
            InsurancePolicy.IsLuxuryPolicy = _configurationModels[_index].Model.Brand.BrandType.IsLuxury;
            return this;
        }

        public override InsurancePolicyBuilder SetDetailItem()
        {
            _index = Random.Next(0, _configurationModels.Count);
            
            double commercialValue = 0;
            switch (_configurationModels[_index].Model.Brand.BrandType.Id)
            {
                case 1://   BASE
                    commercialValue = 26000 * (1 + Random.NextDouble());
                    break;
                case 2://   LUSSO
                    commercialValue = 100000 * (1 + Random.NextDouble());
                    break;
                case 3:// PLUS
                    commercialValue = 44000 * (1 + Random.NextDouble());
                    break;
                case 4: //  SUPER LUSSO
                    commercialValue = 150000 * (1 + Random.NextDouble());
                    break;               
            }

           ((VehicleInsurancePolicy)InsurancePolicy).LicensePlate = Utils.GenerateRandomCode(2, Utils.Chars, Random) +
                                                                     Utils.GenerateRandomCode(3, Utils.Chars, Random) + 
                                                                     Utils.GenerateRandomCode(2, Utils.Chars, Random);

            ((VehicleInsurancePolicy)InsurancePolicy).ConfigurationModel = _configurationModels[_index];
            ((VehicleInsurancePolicy)InsurancePolicy).CommercialValue = commercialValue;
            ((VehicleInsurancePolicy)InsurancePolicy).InsuredValue = commercialValue * 0.95;
            ((VehicleInsurancePolicy)InsurancePolicy).RiskCategory = (byte)Random.Next(1, 14);

            return this;
        }
    }
}
