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
    public class LearningTrainingCopyRepository : Repository<CustomerLearningFeatureCopy>, ILearningTrainingCopyRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public LearningTrainingCopyRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<CustomerLearningFeatureCopy> GetCustomerLearningFeatures() => _appContext.CustomerLearningFeatureCopies;

        public IQueryable<CustomerLearningFeatureCopy> GetCustomerLearningFeature(CustomerLearningFeatureCopy customerLearningFeature) =>
            _appContext.CustomerLearningFeatureCopies.Where(x => x.Gender == customerLearningFeature.Gender &&
                                                            x.BirthMonth == customerLearningFeature.BirthMonth &&
                                                            x.YearBirth == customerLearningFeature.YearBirth &&
                                                            x.MaritalStatus == customerLearningFeature.MaritalStatus &&
                                                            //x.IsSingle == customerLearningFeature.IsSingle &&
                                                            x.ChildrenNumbers == customerLearningFeature.ChildrenNumbers &&
                                                            x.ProfessionType == customerLearningFeature.ProfessionType &&
                                                            x.Country == customerLearningFeature.Country &&
                                                            x.Region == customerLearningFeature.Region);

        public IQueryable<CustomerLearningFeatureCopy> GetCustomerLearningFeaturesForTraining() => _appContext.CustomerLearningFeatureCopies;

        public IQueryable<CustomerLearningFeatureCopy> GetLearningCustomerPreferences(string customerCode, string insurancePolicyCategory) =>
            _appContext.CustomerLearningFeatureCopies;

        public IQueryable<CustomerLearningFeatureCopy> GetLearningCustomerPreferences(string customerCode) =>
            _appContext.CustomerLearningFeatureCopies;

        public bool HasCustomerIds() => _appContext.CustomerLearningFeatureCopies.Any();

        public long GetMaxCustomerId() => int.MaxValue;// _appContext.CustomerLearningFeatureCopies.Max(x => x.CustomerId);

        public int CustomUpdate(CustomerLearningFeatureCopy customerLearningFeature)
        {
            return 0;
            //return _appContext.CustomerLearningFeatureCopies
            //                        .Where(x => x.Gender == customerLearningFeature.Gender &&
            //                                    x.BirthMonth == customerLearningFeature.BirthMonth &&
            //                                    x.YearBirth == customerLearningFeature.YearBirth &&
            //                                    x.MaritalStatus == customerLearningFeature.MaritalStatus &&
            //                                    //x.IsSingle == customerLearningFeature.IsSingle &&
            //                                    x.IsDependentSpouse == customerLearningFeature.IsDependentSpouse &&
            //                                    x.ChildrenNumbers == customerLearningFeature.ChildrenNumbers &&
            //                                    x.DependentChildrenNumber == customerLearningFeature.DependentChildrenNumber &&
            //                                    x.ProfessionType == customerLearningFeature.ProfessionType &&
            //                                    //x.IsFreelancer == customerLearningFeature.IsFreelancer &&
            //                                    x.Income == customerLearningFeature.Income &&
            //                                    x.IncomeType == customerLearningFeature.IncomeType &&
            //                                    x.Country == customerLearningFeature.Country &&
            //                                    x.Region == customerLearningFeature.Region)
            //                        .ExecuteUpdate(setter => setter.SetProperty(p => p.CustomerId, customerLearningFeature.CustomerId));

        }
    }
}
