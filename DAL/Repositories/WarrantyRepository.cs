using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class WarrantyRepository : Repository<WarrantyAvaible>, IWarrantyRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public WarrantyRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<WarrantyAvaible> GetWarranties(byte insurancePolicyCategoryId) =>
            _appContext.WarrantyAvaibles.Where(x => x.InsurancePolicyCategory.Id == insurancePolicyCategoryId);
    }
}
