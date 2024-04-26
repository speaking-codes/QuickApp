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
    public class RepositoryMunicipality : Repository<Municipality>, IRepositoryMunicipality
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public RepositoryMunicipality(ApplicationDbContext context) : base(context) { }

        public IQueryable<Municipality> GetMunicipalities() => _appContext.Municipalities;

        public IQueryable<Municipality> GetMunicipality(int municipalityId) =>
            _appContext.Municipalities
                    .Include(m => m.Province)
                    .Where(x => x.Id == municipalityId);        
    }
}
