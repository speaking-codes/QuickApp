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
    public class HouseInsurancePolicyBuilder : InsurancePolicyBuilder, IHouseInsurancePolicyBuilder
    {
        private HouseInsurancePolicy _houseInsurancePolicy;

        public IHouseInsurancePolicyBuilder SetExtensionSquareMeters()
        {
            _houseInsurancePolicy.ExtensionSquareMeters = Random.Next(65, 120);
            return this;
        }
        public IHouseInsurancePolicyBuilder SetNumberBuildingFloors()
        {
            _houseInsurancePolicy.NumberBuildingFloors = Random.Next(4, 7);
            return this;
        }
        public IHouseInsurancePolicyBuilder SetHomeFloorNumber()
        {
            _houseInsurancePolicy.HomeFloorNumber = Random.Next(1, _houseInsurancePolicy.NumberBuildingFloors);
            return this;
        }
        public IHouseInsurancePolicyBuilder SetLocation(AdressModelTemplate adressModelTemplate)
        {
            _houseInsurancePolicy.Location = Utilities.GenerateFullStreetName(adressModelTemplate.StreetTypes, adressModelTemplate.StreetNames, Random);
            return this;
        }
        public IHouseInsurancePolicyBuilder SetMunicipality(IList<Municipality> municipalities)
        {
            var i = Random.Next(municipalities.Count);
            _houseInsurancePolicy.Municipality = municipalities[i];
            return this;
        }

        public override IInsurancePolicyBuilder SetInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            base.SetInsurancePolicy(insurancePolicy);
            _houseInsurancePolicy = new HouseInsurancePolicy(InsurancePolicy);
            return this;
        }

        public override IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories)
        {
            InsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.Casa).FirstOrDefault();
            return this;
        }

        public override IInsurancePolicyBuilder SetDetailItem(InsurancePolicyTemplate insurancePolicyTemplate)
        {
            return this.SetExtensionSquareMeters()
                       .SetNumberBuildingFloors()
                       .SetHomeFloorNumber()
                       .SetLocation(insurancePolicyTemplate.AddressTemplate)
                       .SetMunicipality(insurancePolicyTemplate.AddressTemplate.Municipalities);
        }
        public new HouseInsurancePolicy Build() => _houseInsurancePolicy;
    }
}
