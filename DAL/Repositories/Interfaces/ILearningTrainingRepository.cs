using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ILearningTrainingRepository : IRepository<CustomerLearningFeature>
    {
        IQueryable<CustomerLearningFeature> GetCustomerLearningFeatures();
        IQueryable<CustomerLearningFeature> GetCustomerLearningFeature(CustomerLearningFeature customerLearningFeature);
        IQueryable<CustomerLearningFeature> GetCustomerLearningFeaturesForTraining();
        IQueryable<CustomerLearningFeature> GetLearningCustomerPreferences(string customerCode, string insurancePolicyCategory);
        IQueryable<CustomerLearningFeature> GetLearningCustomerPreferences(string customerCode);
        IList<int> GetUserId(string customerCode);
        long GetMaxCustomerId();
        int CustomUpdate(CustomerLearningFeature customerLearningFeature);
    }
}
