using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TravelClassTypeRepository : Repository<TravelClassType>, ITravelClassTypeRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public TravelClassTypeRepository(ApplicationDbContext context) : base(context) { }
    }
}
