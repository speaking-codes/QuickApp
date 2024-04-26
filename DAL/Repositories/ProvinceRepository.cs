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
    public class ProvinceRepository : Repository<Province>, IProvinceRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public ProvinceRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<Province> GetProvinces() => _appContext.Provinces.Include(x => x.Region);
    }
}
