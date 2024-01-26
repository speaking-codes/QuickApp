using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MaritalStatusTypeRepository : Repository<MaritalStatusType>, IMaritalStatusTypeRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public MaritalStatusTypeRepository(ApplicationDbContext context) : base(context) { }
    }
}
