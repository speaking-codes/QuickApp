using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class LargeBuildingRepository : Repository<LargeBuilding>, ILargeBuildingRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public LargeBuildingRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<LargeBuilding> GetLargeBuildings(int insurancePolicyId) =>
            _appContext.LargeBuildings.Where(x => x.InsurancePolicy.Id == insurancePolicyId);

        public IQueryable<LargeBuilding> GetLargeBuildings(string insurancePolicyCode)=>
            _appContext.LargeBuildings.Where(x=>x.InsurancePolicy.InsurancePolicyCode== insurancePolicyCode);
    }
}
