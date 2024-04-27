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
    public class InsurancePolicyRepository : Repository<InsurancePolicy>, IInsurancePolicyRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public InsurancePolicyRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<InsurancePolicy> GetInsurancePolicies(string customerCode, byte salesLineId) =>
            _appContext.InsurancePolicies
                       .Where(x => x.Customer.CustomerCode == customerCode &&
                                  x.InsurancePolicyCategory.SalesLine.Id == salesLineId);

        public IQueryable<InsurancePolicy> GetInsurancePolicies(string customerCode) =>
            _appContext.InsurancePolicies
                       .AsSingleQuery()
                       .Where(x => x.Customer.CustomerCode == customerCode);

        public IQueryable<InsurancePolicy> GetInsurancePolicy(int id) =>
            _appContext.InsurancePolicies
                       .Include(x => x.InsurancePolicyCategory)
                       .Where(x => x.Id == id);

        public IQueryable<InsurancePolicy> GetInsurancePolicy(string insurancePolicyCode) =>
            _appContext.InsurancePolicies
                       .Where(x => x.InsurancePolicyCode == insurancePolicyCode);

        public IQueryable<InsurancePolicy> GetInsurancePolicyForTrainingMachineLearning(string insurancePolicyCode)=>
            _appContext.InsurancePolicies
                       .Include(x => x.InsurancePolicyCategory)
                       .Where(x => x.InsurancePolicyCode == insurancePolicyCode);

        public IQueryable<InsurancePolicy> GetActiveInsurancePolicies(string customerCode) =>
            _appContext.InsurancePolicies
                                .Include(x => x.InsurancePolicyCategory)
                                    .ThenInclude(y => y.SalesLine)
                                .AsSingleQuery()
                                .Where(x => x.Customer.CustomerCode == customerCode && x.IssueDate <= DateTime.Now && x.ExpiryDate > DateTime.Now);

        public IQueryable<InsurancePolicy> GetExiperedInsurancePolicies(DateTime expireDate)=>
            _appContext.InsurancePolicies
                       .Include(x => x.Customer)
                       .Where(x => x.ExpiryDate<= expireDate);

        public int GetInsurancePolicyCategoryCount(string insurancePolicyCategoryCode) =>
            _appContext.InsurancePolicies
                       .Where(x => x.InsurancePolicyCategory.InsurancePolicyCategoryCode == insurancePolicyCategoryCode)
                       .Count();

        public bool IsExistingInsurancePolicyCategory(string customerCode, string insurancePolicyCategoryCode, DateTime issueDate, DateTime expiryDate) =>
            _appContext.InsurancePolicies
                       .Where(x => x.Customer.CustomerCode == customerCode &&
                                   x.InsurancePolicyCategory.InsurancePolicyCategoryCode == insurancePolicyCategoryCode &&
                                   x.IssueDate <= expiryDate &&
                                   x.ExpiryDate <= issueDate)
                       .Any();

        //public IQueryable<VehicleInsurancePolicy> GetVehicleInsurancePolicy(string insurancePolicyCode) =>
        //    _appContext.VehicleInsurancePolicies
        //               .Include(x => x.ConfigurationModel)
        //                    .ThenInclude(y => y.Model)
        //                        .ThenInclude(z => z.Brand)
        //                            .ThenInclude(w => w.BrandType)
        //               .Include(x => x.InsurancePolicyCategory)
        //                    .ThenInclude(y => y.SalesLine)
        //               .Where(x => x.InsurancePolicyCode == insurancePolicyCode);

        //public IQueryable<FamilyInsurancePolicy> GetFamilyInsurancePolicy(string insurancePolicyCode) =>
        //    _appContext.FamilyInsurancePolicies
        //               .Include(x => x.KinshipRelationshipType)
        //               .Include(x => x.InsurancePolicyCategory)
        //                    .ThenInclude(y => y.SalesLine)
        //               .Where(x => x.InsurancePolicyCode == insurancePolicyCode);

        //public IQueryable<HealthInsurancePolicy> GetHealthInsurancePolicy(string insurancePolicyCode) =>
        //    _appContext.HealthInsurancePolicies
        //               .Include(x => x.KinshipRelationshipType)
        //               .Where(x => x.InsurancePolicyCode == insurancePolicyCode);

        //public IQueryable<PetInsurancePolicy> GetPetInsurancePolicy(string insurancePolicyCode) =>
        //    _appContext.PetInsurancePolicies
        //               .Include(x => x.BreedPetDetailType)
        //                    .ThenInclude(y => y.BreedPetType)
        //                        .ThenInclude(z => z.PetType)
        //               .Include(x => x.InsurancePolicyCategory)
        //                    .ThenInclude(y => y.SalesLine)
        //               .Where(x => x.InsurancePolicyCode == insurancePolicyCode);

        //public IQueryable<HouseInsurancePolicy> GetHouseInsurancePolicy(string insurancePolicyCode)=>
        //    _appContext.HouseInsurancePolicies
        //               .Include(x => x.Municipality)
        //                    .ThenInclude(y => y.Province)
        //               .Include(x => x.InsurancePolicyCategory)
        //                    .ThenInclude(y => y.SalesLine)
        //               .Where(x => x.InsurancePolicyCode==insurancePolicyCode);

        //public IQueryable<InsurancePolicy> GetInsurancePolicyBaggageLoss(string insurancePolicyCode) =>
        //    _appContext.InsurancePolicies
        //               .Include(x => x.BaggageLosses)
        //                    .ThenInclude(y => y.BaggageType)
        //               .Where(x => x.InsurancePolicyCode == insurancePolicyCode);

        //public IQueryable<InsurancePolicy> GetInsurancePolicyTravel(string insurancePolicyCode)=>
        //    _appContext.InsurancePolicies
        //               .Include(x => x.Travels)
        //                    .ThenInclude(y => y.TravelMeansType)
        //               .Include(x => x.Travels)
        //                    .ThenInclude(y => y.TravelClassType)
        //               .Include(x => x.Travels)
        //                    .ThenInclude(y => y.DepartureMunicipality)
        //                        .ThenInclude(z => z.Province)
        //               .Include(x => x.Travels)
        //                    .ThenInclude(y => y.ArrivalMunicipality)
        //                        .ThenInclude(z => z.Province)
        //               .Include(x => x.Travels)
        //                    .ThenInclude(y => y.ConfigurationModel)
        //                        .ThenInclude(z => z.Model)
        //                            .ThenInclude(w => w.Brand)
        //               .Where(x => x.InsurancePolicyCode == insurancePolicyCode);

        //public IQueryable<InsurancePolicy> GetInsurancePolicyVacation(string insurancePolicyCode)=>
        //    _appContext.InsurancePolicies
        //               .Include(x => x.Vacations)
        //                   .ThenInclude(y => y.StructureType)
        //               .Include(x => x.Vacations)
        //                    .ThenInclude(y => y.PlaceStructure)
        //                        .ThenInclude(z => z.Province)
        //               .Where(x => x.InsurancePolicyCode == insurancePolicyCode);
    }
}
