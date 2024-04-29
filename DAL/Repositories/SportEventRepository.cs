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
    public class SportEventRepository : Repository<SportEvent>, ISportEventRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public SportEventRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<SportEvent> GetSportEvents(int insurancePolicyId) =>
            _appContext.SportEvents
                       .Include(x => x.SportEventType)
                       .Where(x => x.InsurancePolicy.Id == insurancePolicyId);

        public IQueryable<SportEvent> GetSportEvents(string insurancePolicyCode) =>
            _appContext.SportEvents
                       .Include(x => x.SportEventType)
                       .Where(x => x.InsurancePolicy.InsurancePolicyCode == insurancePolicyCode);
    }
}
