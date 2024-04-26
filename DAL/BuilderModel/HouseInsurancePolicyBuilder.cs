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
            _houseInsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.MULTIGARANZIA_ABITAZIONE).FirstOrDefault();
            return this;
        }

        public override IInsurancePolicyBuilder SetCustomer(Customer customer)
        {
            _houseInsurancePolicy.Customer = customer;
            return this;
        }

        public override IInsurancePolicyBuilder SetIssueDate()
        {
            base.SetIssueDate();
            _houseInsurancePolicy.IssueDate = InsurancePolicy.IssueDate;
            return this;
        }

        public override IInsurancePolicyBuilder SetExpiryDate()
        {
            base.SetExpiryDate();
            _houseInsurancePolicy.ExpiryDate = InsurancePolicy.ExpiryDate;
            return this;
        }

        public override IInsurancePolicyBuilder SetInsuredMaximum()
        {
            base.SetInsuredMaximum();
            _houseInsurancePolicy.InsuredMaximum = InsurancePolicy.InsuredMaximum;
            return this;
        }

        public override IInsurancePolicyBuilder SetTotalPrize()
        {
            base.SetTotalPrize();
            _houseInsurancePolicy.TotalPrize = InsurancePolicy.TotalPrize;
            return this;
        }

        public override IInsurancePolicyBuilder SetLuxuryPolicy()
        {
            base.SetLuxuryPolicy();
            _houseInsurancePolicy.IsLuxuryPolicy = InsurancePolicy.IsLuxuryPolicy;
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

        public override HouseInsurancePolicy Build() => _houseInsurancePolicy;
    }
}
