﻿using DAL.BuilderModel.Interfaces;
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

        public override IInsurancePolicyBuilder SetInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            base.SetInsurancePolicy(insurancePolicy);
            _vehicleInsurancePolicy = new VehicleInsurancePolicy(InsurancePolicy);
            return this;
        }

        public override IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories)
        {
            _vehicleInsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.Auto).FirstOrDefault();
            return this;
        }

        public override IInsurancePolicyBuilder SetCustomer(Customer customer)
        {
            _vehicleInsurancePolicy.Customer = customer;
            return this;
        }

        public override IInsurancePolicyBuilder SetIssueDate()
        {
            base.SetIssueDate();
            _vehicleInsurancePolicy.IssueDate = InsurancePolicy.IssueDate;
            return this;
        }

        public override IInsurancePolicyBuilder SetExpiryDate()
        {
            base.SetExpiryDate();
            _vehicleInsurancePolicy.ExpiryDate = InsurancePolicy.ExpiryDate;
            return this;
        }

        public override IInsurancePolicyBuilder SetInsuredMaximum()
        {
            base.SetInsuredMaximum();
            _vehicleInsurancePolicy.InsuredMaximum = InsurancePolicy.InsuredMaximum;
            return this;
        }

        public override IInsurancePolicyBuilder SetTotalPrize()
        {
            base.SetTotalPrize();
            _vehicleInsurancePolicy.TotalPrize = InsurancePolicy.TotalPrize;
            return this;
        }

        public override IInsurancePolicyBuilder SetLuxuryPolicy()
        {
            if (_vehicleInsurancePolicy.ConfigurationModel == null)
            {
                base.SetLuxuryPolicy();
                _vehicleInsurancePolicy.IsLuxuryPolicy = InsurancePolicy.IsLuxuryPolicy;
                return this;
            }

            _vehicleInsurancePolicy.IsLuxuryPolicy = (_vehicleInsurancePolicy.ConfigurationModel.Model.Brand.BrandType.Id == 2 || _vehicleInsurancePolicy.ConfigurationModel.Model.Brand.BrandType.Id == 4);
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

        public override VehicleInsurancePolicy Build() => _vehicleInsurancePolicy;
    }
}
