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
        IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutOne(CustomerLearningFeature customerLearningFeature);
        IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutTwo(CustomerLearningFeature customerLearningFeature);
        IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutThree(CustomerLearningFeature customerLearningFeature);
        IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutFour(CustomerLearningFeature customerLearningFeature);
        IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutFive(CustomerLearningFeature customerLearningFeature);
        IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutSix(CustomerLearningFeature customerLearningFeature);
        IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutSeven(CustomerLearningFeature customerLearningFeature);
        IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutEight(CustomerLearningFeature customerLearningFeature);
        IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutNine(CustomerLearningFeature customerLearningFeature);
        IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutTen(CustomerLearningFeature customerLearningFeature);

        IQueryable <CustomerLearningFeature> GetCustomerLearningFeaturesForTraining();
        IQueryable<CustomerLearningFeature> GetLearningCustomerPreferences(string customerCode, string insurancePolicyCategory);
        IQueryable<CustomerLearningFeature> GetLearningCustomerPreferences(string customerCode);
        Task<IList<CustomerLearningFeature>> GetCustomCustomerLearningFeatures();
        Task<IList<CustomerLearningFeature>> GetCustomerLearningFeatures(long? customerId, byte insurancePolicyId);
        IQueryable<CustomerLearningFeature> GetCustomerLearningFeatures(long? customerId);
        int CustomDelete(long id);
        bool HasCustomerIds();
        long GetMaxCustomerId();
        int CustomUpdate(CustomerLearningFeature customerLearningFeature);
    }
}
