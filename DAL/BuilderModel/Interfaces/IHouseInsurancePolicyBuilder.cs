using DAL.BuilderModelTemplate;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel.Interfaces
{
    public interface IHouseInsurancePolicyBuilder : IInsurancePolicyBuilder
    {
        IHouseInsurancePolicyBuilder SetExtensionSquareMeters();
        IHouseInsurancePolicyBuilder SetNumberBuildingFloors();
        IHouseInsurancePolicyBuilder SetHomeFloorNumber();
        IHouseInsurancePolicyBuilder SetLocation(AdressModelTemplate adressModelTemplate);
        IHouseInsurancePolicyBuilder SetMunicipality(IList<Municipality> municipalities);
        new HouseInsurancePolicy Build();
    }
}
