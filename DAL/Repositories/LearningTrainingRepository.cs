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
    public class LearningTrainingRepository : Repository<LearningCustomerPreferences>, ILearningTrainingRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public LearningTrainingRepository(DbContext context) : base(context)
        {
        }

        public IQueryable<LearningCustomerPreferences> GetLearningCustomerPreferences(string customerCode, string insurancePolicyCategory) =>
            _appContext.LearningCustomerPreferences
                       .Where(x => x.CustomerCode == customerCode && x.InsurancePolicyCategory == insurancePolicyCategory);

        public IQueryable<LearningCustomerPreferences> GetLearningCustomerPreferences(string customerCode) =>
            _appContext.LearningCustomerPreferences
                       .Where(x => x.CustomerCode == customerCode);

        public IList<int> GetUserId(string customerCode) =>
            _appContext.LearningCustomerPreferences
                       .Where(x => x.CustomerCode == customerCode)
                       .Select(x => x.UserId)
                       .ToList();

        public int GetLastUserId()=>
            _appContext.LearningCustomerPreferences.Max(x => x.UserId);
    }
}
