using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ILearningTrainingRepository : IRepository<LearningCustomerPreferences>
    {
        IQueryable<LearningCustomerPreferences> GetLearningCustomerPreferences(string customerCode, string insurancePolicyCategory);
        IQueryable<LearningCustomerPreferences> GetLearningCustomerPreferences(string customerCode);
        IList<int> GetUserId(string customerCode);
        int GetLastUserId();
    }
}
