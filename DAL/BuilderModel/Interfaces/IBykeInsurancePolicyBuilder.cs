using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel.Interfaces
{
    public interface IBykeInsurancePolicyBuilder : IInsurancePolicyBuilder
    {
        IBykeInsurancePolicyBuilder SetLicensePlate();
        IBykeInsurancePolicyBuilder SetCommercialValue();
        IBykeInsurancePolicyBuilder SetInsuredValue();
        IBykeInsurancePolicyBuilder SetRiskCategory();
        IBykeInsurancePolicyBuilder SetConfigurationModel(IList<ConfigurationModel> bykeConfigurationModels);
        new VehicleInsurancePolicy Build();
    }
}
