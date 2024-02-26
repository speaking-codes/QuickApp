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
    public class TravelMeansTypeRepository : Repository<TravelMeansType>, ITravelMeansTypeRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public TravelMeansTypeRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<TravelMeansType> GetTravelMeansTypes() =>
            _appContext.TravelMeansTypes
                       .Include(x => x.TravelClassTypes);
    }
}
