using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ILearningTrainingCopyRepository : IRepository<CustomerLearningFeatureCopy>
    {
        IQueryable<CustomerLearningFeatureCopy> GetCustomerLearningFeatures();
        IQueryable<CustomerLearningFeatureCopy> GetCustomerLearningFeature(CustomerLearningFeatureCopy customerLearningFeature);
        IQueryable<CustomerLearningFeatureCopy> GetCustomerLearningFeaturesForTraining();
        IQueryable<CustomerLearningFeatureCopy> GetLearningCustomerPreferences(string customerCode, string insurancePolicyCategory);
        IQueryable<CustomerLearningFeatureCopy> GetLearningCustomerPreferences(string customerCode);
        bool HasCustomerIds();
        long GetMaxCustomerId();
        int CustomUpdate(CustomerLearningFeatureCopy customerLearningFeature);
    }
}
