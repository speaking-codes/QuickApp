using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IConfigurationModelRepository : IRepository<ConfigurationModel>
    {
        IQueryable<ConfigurationModel> GetConfigurationModels();
        IQueryable<ConfigurationModel> GetConfigurationsByInsurancePolicyVehicle(int idPolicy);
    }
}
