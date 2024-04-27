using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class HouseRepository:Repository<House>, IHouseRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public HouseRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<House> GetHouses(int insurancePolicyId) =>
            _appContext.Houses.Where(x => x.InsurancePolicy.Id == insurancePolicyId);

        public IQueryable<House> GetHouses(string insurancePolicyCode) =>
            _appContext.Houses.Where(x => x.InsurancePolicy.InsurancePolicyCode == insurancePolicyCode);
    }
}
