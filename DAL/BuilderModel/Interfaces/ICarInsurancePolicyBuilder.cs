using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel.Interfaces
{
    public interface ICarInsurancePolicyBuilder : IInsurancePolicyBuilder
    {
        ICarInsurancePolicyBuilder SetLicensePlate();
        ICarInsurancePolicyBuilder SetCommercialValue();
        ICarInsurancePolicyBuilder SetInsuredValue();
        ICarInsurancePolicyBuilder SetRiskCategory();
        ICarInsurancePolicyBuilder SetConfigurationModel(IList<ConfigurationModel> carConfigurationModels);
        VehicleInsurancePolicy Build();
    }
}
