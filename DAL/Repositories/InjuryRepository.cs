using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class InjuryRepository:Repository<Injury>, IInjuryRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public InjuryRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<Injury> GetInjuries(int insurancePolicyId) =>
            _appContext.Injuries.Where(x => x.InsurancePolicy.Id == insurancePolicyId);

        public IQueryable<Injury> GetInjuries(string insurancePolicyCode)=>
            _appContext.Injuries.Where(x=>x.InsurancePolicy.InsurancePolicyCode== insurancePolicyCode);
    }
}
