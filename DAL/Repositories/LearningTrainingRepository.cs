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

        public LearningTrainingRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeatures() => _appContext.CustomerLearningFeatures;

        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeature(CustomerLearningFeature customerLearningFeature) =>
            _appContext.CustomerLearningFeatures.Where(x => x.Gender == customerLearningFeature.Gender &&
                                                            x.BirthMonth == customerLearningFeature.BirthMonth &&
                                                            x.YearBirth == customerLearningFeature.YearBirth &&
                                                            x.MaritalStatus == customerLearningFeature.MaritalStatus &&
                                                            x.IsSingle == customerLearningFeature.IsSingle &&
                                                            x.ChildrenNumbers == customerLearningFeature.ChildrenNumbers &&
                                                            x.ProfessionType == customerLearningFeature.ProfessionType &&
                                                            x.IsFreelancer == customerLearningFeature.IsFreelancer &&
                                                            x.Country == customerLearningFeature.Country &&
                                                            x.Region == customerLearningFeature.Region &&
                                                            x.CustomerId.HasValue);
        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutOne(CustomerLearningFeature customerLearningFeature) =>
                        _appContext.CustomerLearningFeatures.Where(x => x.Gender == customerLearningFeature.Gender &&
                                                            x.BirthMonth == customerLearningFeature.BirthMonth &&
                                                            x.YearBirth == customerLearningFeature.YearBirth &&
                                                            x.MaritalStatus == customerLearningFeature.MaritalStatus &&
                                                            x.IsSingle == customerLearningFeature.IsSingle &&
                                                            x.ChildrenNumbers == customerLearningFeature.ChildrenNumbers &&
                                                            x.IsFreelancer == customerLearningFeature.IsFreelancer &&
                                                            x.Country == customerLearningFeature.Country &&
                                                            x.Region == customerLearningFeature.Region &&
                                                            x.CustomerId.HasValue);
        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutTwo(CustomerLearningFeature customerLearningFeature) =>
                        _appContext.CustomerLearningFeatures.Where(x => x.Gender == customerLearningFeature.Gender &&
                                                            x.BirthMonth == customerLearningFeature.BirthMonth &&
                                                            x.YearBirth == customerLearningFeature.YearBirth &&
                                                            x.MaritalStatus == customerLearningFeature.MaritalStatus &&
                                                            x.IsSingle == customerLearningFeature.IsSingle &&
                                                            x.IsFreelancer == customerLearningFeature.IsFreelancer &&
                                                            x.Country == customerLearningFeature.Country &&
                                                            x.Region == customerLearningFeature.Region &&
                                                            x.CustomerId.HasValue);
        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutThree(CustomerLearningFeature customerLearningFeature) =>
                _appContext.CustomerLearningFeatures.Where(x => x.Gender == customerLearningFeature.Gender &&
                                                    x.BirthMonth == customerLearningFeature.BirthMonth &&
                                                    x.YearBirth == customerLearningFeature.YearBirth &&
                                                    x.IsSingle == customerLearningFeature.IsSingle &&
                                                    x.IsFreelancer == customerLearningFeature.IsFreelancer &&
                                                    x.Country == customerLearningFeature.Country &&
                                                    x.Region == customerLearningFeature.Region &&
                                                    x.CustomerId.HasValue);
        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutFour(CustomerLearningFeature customerLearningFeature) =>
                _appContext.CustomerLearningFeatures.Where(x => x.Gender == customerLearningFeature.Gender &&
                                                    x.BirthMonth == customerLearningFeature.BirthMonth &&
                                                    x.YearBirth == customerLearningFeature.YearBirth &&
                                                    x.IsSingle == customerLearningFeature.IsSingle &&
                                                    x.IsFreelancer == customerLearningFeature.IsFreelancer &&
                                                    x.Region == customerLearningFeature.Region &&
                                                    x.CustomerId.HasValue);
        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutFive(CustomerLearningFeature customerLearningFeature) =>
                _appContext.CustomerLearningFeatures.Where(x => x.Gender == customerLearningFeature.Gender &&
                                                                x.BirthMonth == customerLearningFeature.BirthMonth &&
                                                                x.IsSingle == customerLearningFeature.IsSingle &&
                                                                x.IsFreelancer == customerLearningFeature.IsFreelancer &&
                                                                x.Region == customerLearningFeature.Region &&
                                                                x.CustomerId.HasValue);
        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutSix(CustomerLearningFeature customerLearningFeature) =>
                _appContext.CustomerLearningFeatures.Where(x => x.Gender == customerLearningFeature.Gender &&
                                                                x.BirthMonth == customerLearningFeature.BirthMonth &&
                                                                x.IsSingle == customerLearningFeature.IsSingle &&
                                                                x.IsFreelancer == customerLearningFeature.IsFreelancer &&
                                                                x.CustomerId.HasValue);
        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutSeven(CustomerLearningFeature customerLearningFeature) =>
                _appContext.CustomerLearningFeatures.Where(x => x.Gender == customerLearningFeature.Gender &&
                                                                x.IsSingle == customerLearningFeature.IsSingle &&
                                                                x.IsFreelancer == customerLearningFeature.IsFreelancer &&
                                                                x.CustomerId.HasValue);
        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutEight(CustomerLearningFeature customerLearningFeature) =>
                _appContext.CustomerLearningFeatures.Where(x => x.Gender == customerLearningFeature.Gender &&
                                                                x.IsSingle == customerLearningFeature.IsSingle &&
                                                                x.CustomerId.HasValue);
        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutNine(CustomerLearningFeature customerLearningFeature) =>
                _appContext.CustomerLearningFeatures.Where(x => x.Gender == customerLearningFeature.Gender &&
                                                                x.CustomerId.HasValue);
        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeatureWithoutTen(CustomerLearningFeature customerLearningFeature) =>
                _appContext.CustomerLearningFeatures.Where(x => x.CustomerId.HasValue);

        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeaturesForTraining() => _appContext.CustomerLearningFeatures.Where(x => x.CustomerId.HasValue);

        public IQueryable<CustomerLearningFeature> GetLearningCustomerPreferences(string customerCode, string insurancePolicyCategory) =>
            _appContext.CustomerLearningFeatures;

        public IQueryable<CustomerLearningFeature> GetLearningCustomerPreferences(string customerCode) =>
            _appContext.CustomerLearningFeatures;

        public async Task<IList<CustomerLearningFeature>> GetCustomCustomerLearningFeatures() =>
                  await _appContext.CustomerLearningFeatures
                              .FromSqlRaw($"select CustomerId, InsurancePolicyId from AppCustomerLearningFeatures group by CustomerId, InsurancePolicyId having COUNT(Id) > 1")
                              .Select(x => new CustomerLearningFeature
                              {
                                  CustomerId = x.CustomerId,
                                  InsurancePolicyId = x.InsurancePolicyId
                              })
                              .ToListAsync();

        public async Task<IList<CustomerLearningFeature>> GetCustomerLearningFeatures(long? customerId, byte insurancePolicyId) =>
           await _appContext.CustomerLearningFeatures
                            .Where(x => x.CustomerId == customerId && x.InsurancePolicyId == insurancePolicyId)
                            .Select(x => new CustomerLearningFeature
                            {
                                Id = x.Id,
                                CustomerId = x.CustomerId,
                                InsurancePolicyId = x.InsurancePolicyId
                            }).ToListAsync();

        public IQueryable<CustomerLearningFeature> GetCustomerLearningFeatures(long? customerId) =>
            _appContext.CustomerLearningFeatures.Where(x => x.CustomerId == customerId);

        public int CustomDelete(long id) =>
            _appContext.Database.ExecuteSql($"DELETE FROM AppCustomerLearningFeatures WHERE Id = {id}");

        public bool HasCustomerIds() => _appContext.CustomerLearningFeatures.Where(x => x.CustomerId.HasValue).Any();

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
                                                x.Income == customerLearningFeature.Income &&
                                                x.IncomeType == customerLearningFeature.IncomeType &&
                                                x.Country == customerLearningFeature.Country &&
                                                x.Region == customerLearningFeature.Region)
                                    .ExecuteUpdate(setter => setter.SetProperty(p => p.CustomerId, customerLearningFeature.CustomerId));

        }
    }
}
