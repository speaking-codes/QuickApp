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
    public class BusinessRepository:Repository<Business>, IBusinessRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public BusinessRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<Business> GetBusinesses(int insurancePolicyId) =>
            _appContext.Businesss
                       .Include(x => x.BusinessType)
                       .Include(x => x.Municipality)
                            .ThenInclude(y => y.Province)
                       .Where(x => x.InsurancePolicy.Id == insurancePolicyId);

        public IQueryable<Business> GetBusinesses(string insurancePolicyCode)=>
            _appContext.Businesss
                       .Include(x => x.BusinessType)
                       .Include(x => x.Municipality)
                            .ThenInclude(y => y.Province)
                       .Where(x=>x.InsurancePolicy.InsurancePolicyCode== insurancePolicyCode);
    }
}
