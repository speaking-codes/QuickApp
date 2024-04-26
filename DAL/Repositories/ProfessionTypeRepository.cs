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
    public class ProfessionTypeRepository : Repository<ProfessionType>, IProfessionTypeRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public ProfessionTypeRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<ProfessionType> GetProfessionTypes() => _appContext.ProfessionTypes.Include(x => x.IncomeType);
    }
}
