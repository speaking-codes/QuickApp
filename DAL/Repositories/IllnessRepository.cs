using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class IllnessRepository:Repository<Illness>, Interfaces.IIllnessRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public IllnessRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<Illness> GetIllnesses(int insurancePolicyId) =>
            _appContext.Illnesses.Where(x => x.InsurancePolicy.Id == insurancePolicyId);

        public IQueryable<Illness> GetIllnesses(string insurancePolicyCode)=>
            _appContext.Illnesses.Where(x=>x.InsurancePolicy.InsurancePolicyCode== insurancePolicyCode);
    }
}
