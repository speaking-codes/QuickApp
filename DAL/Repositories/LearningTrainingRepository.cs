using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class LearningTrainingRepository : Repository<CustomerLearningFeature>, ILearningTrainingRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public LearningTrainingRepository(DbContext context) : base(context)
        {
        }

        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeatures() => _appContext.CustomerLearningFeatures;

        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeature(CustomerLearningFeature customerLearningFeature) =>
            _appContext.CustomerLearningFeatures.Where(x => x.Gender == customerLearningFeature.Gender &&
                                                            x.BirthMonth == customerLearningFeature.BirthMonth &&
                                                            x.YearBirth == customerLearningFeature.YearBirth &&
                                                            x.MaritalStatus == customerLearningFeature.MaritalStatus &&
                                                            x.IsSingle == customerLearningFeature.IsSingle &&
                                                            x.ChildrenNumbers == customerLearningFeature.ChildrenNumbers &&
                                                            x.ProfessionType == customerLearningFeature.ProfessionType &&
                                                            x.Country == customerLearningFeature.Country &&
                                                            x.Region == customerLearningFeature.Region &&
                                                            x.CustomerId.HasValue);
            
        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeaturesForTraining() => _appContext.CustomerLearningFeatures.Where(x => x.CustomerId.HasValue);

        public IQueryable<CustomerLearningFeature> GetLearningCustomerPreferences(string customerCode, string insurancePolicyCategory) =>
            _appContext.CustomerLearningFeatures;

        public IQueryable<CustomerLearningFeature> GetLearningCustomerPreferences(string customerCode) =>
            _appContext.CustomerLearningFeatures;

        public bool HasCustomerIds() => _appContext.CustomerLearningFeatures.Where(x=> x.CustomerId.HasValue).Any();

        public long GetMaxCustomerId() => _appContext.CustomerLearningFeatures.Where(x => x.CustomerId.HasValue).Max(x => x.CustomerId.Value);

        public int CustomUpdate(CustomerLearningFeature customerLearningFeature)
        {
            return _appContext.CustomerLearningFeatures
                                    .Where(x => x.Gender == customerLearningFeature.Gender &&
                                                x.BirthMonth == customerLearningFeature.BirthMonth &&
                                                x.YearBirth == customerLearningFeature.YearBirth &&
                                                x.MaritalStatus == customerLearningFeature.MaritalStatus &&
                                                x.IsSingle == customerLearningFeature.IsSingle &&
                                                x.IsDependentSpouse == customerLearningFeature.IsDependentSpouse &&
                                                x.ChildrenNumbers == customerLearningFeature.ChildrenNumbers &&
                                                x.DependentChildrenNumber == customerLearningFeature.DependentChildrenNumber &&
                                                x.ProfessionType == customerLearningFeature.ProfessionType &&
                                                x.IsFreelancer == customerLearningFeature.IsFreelancer &&
                                                x.IncomeClassType == customerLearningFeature.IncomeClassType &&
                                                x.IncomeType == customerLearningFeature.IncomeType &&
                                                x.Country == customerLearningFeature.Country &&
                                                x.Region == customerLearningFeature.Region)
                                    .ExecuteUpdate(setter => setter.SetProperty(p => p.CustomerId, customerLearningFeature.CustomerId));

        }
    }
}
