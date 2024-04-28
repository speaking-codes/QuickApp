using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BusinessTypeRepository : Repository<BusinessType>, IBusinessTypeRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public BusinessTypeRepository(ApplicationDbContext context) : base(context) { }
    }
}
