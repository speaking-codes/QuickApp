using DAL.Models;
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

        public IList<Municipality> GetMunicipalities()=> _appContext.Municipalities.ToList();

        public async Task<IList<Municipality>> GetMunicipalitiesAsync() => await _appContext.Municipalities.ToListAsync();

        public Municipality GetMunicipality(int municipalityId)=>
            _appContext.Municipalities
                    .Include(m => m.Province)
                    .Where(x => x.Id==municipalityId)
                    .SingleOrDefault();

        public async Task<Municipality> GetMunicipalityAsync(int municipalityId)=>
            await _appContext.Municipalities
                    .Include(m => m.Province)
                    .Where(x => x.Id == municipalityId)
                    .SingleOrDefaultAsync();
    }
}
