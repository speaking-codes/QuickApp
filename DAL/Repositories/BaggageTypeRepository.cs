using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BaggageTypeRepository : Repository<BaggageType>, IBaggageTypeRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public BaggageTypeRepository(ApplicationDbContext context) : base(context) { }
    }
}
