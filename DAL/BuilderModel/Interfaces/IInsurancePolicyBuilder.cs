using DAL.BuilderModelTemplate;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel.Interfaces
{
    public interface IInsurancePolicyBuilder
    {
        IInsurancePolicyBuilder SetInsurancePolicy(InsurancePolicy insurancePolicy);
        IInsurancePolicyBuilder SetIssueDate();
        IInsurancePolicyBuilder SetExpiryDate();
        IInsurancePolicyBuilder SetInsuredMaximum();
        IInsurancePolicyBuilder SetTotalPrize();
        IInsurancePolicyBuilder SetLuxuryPolicy();
        IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories);
        IInsurancePolicyBuilder SetCustomer(Customer customer);
        IInsurancePolicyBuilder SetDetailItem(InsurancePolicyTemplate insurancePolicyTemplate);
        //IInsurancePolicyBuilder SetBaggageLosses(IList<BaggageType> baggageTypes);
        //IInsurancePolicyBuilder SetTravels(IList<TravelMeansType> travelMeansTypes, IList<TravelClassType> travelClassTypes, IList<ConfigurationModel> configurationModels, IList<Municipality> municipalities);
        //IInsurancePolicyBuilder SetVacations(IList<StructureType> structureTypes, IList<Municipality> municipalities);

        InsurancePolicy Build();
    }
}
