using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ConfigurationModelRepository : Repository<ConfigurationModel>, IConfigurationModelRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public ConfigurationModelRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<ConfigurationModel> GetConfigurationModels() =>
                _appContext.ConfigurationModels
                           .Include(x => x.Model)
                                .ThenInclude(x => x.Brand)
                                    .ThenInclude(x => x.BrandType)
                           .Include(x => x.ModelType);

        public IQueryable<ConfigurationModel> GetConfigurationsByInsurancePolicyVehicle(int idPolicy) =>
            _appContext.ConfigurationModels
                       .Include(x => x.Model)
                       .ThenInclude(x => x.Brand)
                       .Where(x => x.VehicleInsurancePolicies.Any(y => y.Id == idPolicy));
    }
}
