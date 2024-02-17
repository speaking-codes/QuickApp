using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SalesLineRepository : Repository<SalesLineType>, ISalesLineRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public SalesLineRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<SalesLineType> GetSalesLineTypes(string customerCode) =>
                    _appContext.SalesLineTypes
                               .Where(x => x.InsurancePolicyCategories
                                            .Any(y => y.InsurancePolicies
                                                       .Any(z => z.Customer.CustomerCode == customerCode))
                                     );
    }
}
