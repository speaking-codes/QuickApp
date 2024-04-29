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
    public class VehicleRepository:Repository<Vehicle>, IVehicleRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public VehicleRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<Vehicle> GetVehicles(int insurancePolicyId) =>
               _appContext.Vehicles
                          .Include(x => x.ConfigurationModel)
                                .ThenInclude(y => y.Model)
                                    .ThenInclude(z => z.Brand)
                                        .ThenInclude(k => k.BrandType)
                          .Where(x => x.InsurancePolicy.Id == insurancePolicyId);

        public IQueryable<Vehicle> GetVehicles(string insurancePolicyCode)=>
            _appContext.Vehicles
                       .Include(x => x.ConfigurationModel)
                                .ThenInclude(y => y.Model)
                                    .ThenInclude(z => z.Brand)
                                        .ThenInclude(k => k.BrandType)
                       .Where(x=>x.InsurancePolicy.InsurancePolicyCode== insurancePolicyCode);
    }
}
