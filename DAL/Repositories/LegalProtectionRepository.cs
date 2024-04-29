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
    public class LegalProtectionRepository : Repository<LegalProtection>, ILegalProtectionRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public LegalProtectionRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<LegalProtection> GetLegalProtections(int insurancePolicyId) =>
             _appContext.LegalProtections
                        .Include(x => x.KinshipRelationshipType)
                        .Where(x => x.InsurancePolicy.Id == insurancePolicyId);

        public IQueryable<LegalProtection> GetLegalProtections(string insurancePolicyCode)=>
            _appContext.LegalProtections
                       .Include(x => x.KinshipRelationshipType)
                       .Where(x=>x.InsurancePolicy.InsurancePolicyCode== insurancePolicyCode);
    }
}
