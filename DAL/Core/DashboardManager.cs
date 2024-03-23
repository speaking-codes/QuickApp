using DAL.Core.Interfaces;
using DAL.ModelsNoSql;
using DAL.RepositoryNoSql.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Core
{
    public class DashboardManager : Manager, IDashboardManager
    {
        private readonly ICustomerHeaderRepository _customerHeaderRepository;
        private readonly ICustomerDetailRepository _customerDetailRepository;
        private readonly IInsuranceCategoryPolicyDashboardCardRepository _insuranceCategoryPolicyDashboardCardRepository;
        private readonly IInsuranceCategoryPolicyTopSellingRepository _insuranceCategoryPolicyTopSellingRepository;
        private readonly IInsuranceCoverageSummaryRepository _insuranceCoverageSummaryRepository;
        private readonly IInsuranceCoverageChartRepository _insuranceCoverageChartRepository;
        private readonly ILearningManager _learningManager;

        public DashboardManager(IUnitOfWork unitOfWork,
                                ICustomerHeaderRepository customerHeaderRepository,
                                ICustomerDetailRepository customerDetailRepository,
                                IInsuranceCategoryPolicyDashboardCardRepository insuranceCategoryPolicyDashboardCardRepository,
                                IInsuranceCategoryPolicyTopSellingRepository insuranceCategoryPolicyTopSellingRepository,
                                IInsuranceCoverageSummaryRepository insuranceCoverageSummaryRepository,
                                IInsuranceCoverageChartRepository insuranceCoverageChartRepository,
                                ILearningManager learningManager) : base(unitOfWork)
        {
            _customerHeaderRepository = customerHeaderRepository;
            _customerDetailRepository = customerDetailRepository;
            _insuranceCategoryPolicyDashboardCardRepository = insuranceCategoryPolicyDashboardCardRepository;
            _insuranceCategoryPolicyTopSellingRepository = insuranceCategoryPolicyTopSellingRepository;
            _insuranceCoverageSummaryRepository = insuranceCoverageSummaryRepository;
            _insuranceCoverageChartRepository = insuranceCoverageChartRepository;
            _learningManager = learningManager;
        }

        public CustomerHeader GetCustomerHeader(string customerCode)
        {
            var customerHeader = _customerHeaderRepository.GetCustomer(customerCode);
            if (customerHeader != null)
                return customerHeader;

            return new CustomerHeader();
        }

        public CustomerDetail GetCustomerDetail(string customerCode)
        {
            var customerDetail = _customerDetailRepository.GetCustomer(customerCode);
            if (customerDetail != null)
                return customerDetail;

            return new CustomerDetail();
        }

        public IList<SalesLineChart> GetSalesLineChart(string customerCode)
        {
            var insuranceCoverageChart = _insuranceCoverageChartRepository.GetInsuranceCoverageChart(customerCode);
            if (insuranceCoverageChart != null)
                return insuranceCoverageChart.SalesLineCharts;

            return new List<SalesLineChart>();
        }

        public IList<InsuranceCoverageGrid> GetInsuranceCoverageGridSummaries(string customerCode)
        {
            var insuranceCoverageSummary = _insuranceCoverageSummaryRepository.GetInsuranceCoverageSummary(customerCode);
            if (insuranceCoverageSummary != null)
                return insuranceCoverageSummary.InsuranceCoverageGrids;

            return new List<InsuranceCoverageGrid>();
            //var insurancePolicies = UnitOfWork.InsurancePolicies.GetInsurancePolicies(customerCode).ToList();
            //insuranceCoverageSummary = new InsuranceCoverageSummary();
            //insuranceCoverageSummary.CustomerCode = customerCode;
            //InsuranceCoverageGrid insuranceCoverageGrid = null;

            //foreach (var item in insurancePolicies)
            //{
            //    switch ((EnumInsurancePolicyCategory)item.InsurancePolicyCategory.Id)
            //    {
            //        case EnumInsurancePolicyCategory.None:
            //            break;
            //        case EnumInsurancePolicyCategory.Auto:
            //        case EnumInsurancePolicyCategory.Moto:
            //            var vehicleInsurancePolicy = UnitOfWork.InsurancePolicies.GetVehicleInsurancePolicy(item.InsurancePolicyCode).FirstOrDefault();
            //            insuranceCoverageGrid = new InsuranceCoverageGrid
            //            {
            //                Code = vehicleInsurancePolicy.InsurancePolicyCode,
            //                CategoryType = item.InsurancePolicyCategory.InsurancePolicyCategoryName,
            //                ItemDescription = $"Targa: {vehicleInsurancePolicy.LicensePlate} - {vehicleInsurancePolicy.ConfigurationModel.Model.Brand.BrandName} - {vehicleInsurancePolicy.ConfigurationModel.Model.ModelName} - {vehicleInsurancePolicy.ConfigurationModel.ConfigurationDescription}",
            //                IssueDate = vehicleInsurancePolicy.IssueDate.ToString("dd/MM/yyyy"),
            //                ExpiryDate = vehicleInsurancePolicy.ExpiryDate.ToString("dd/MM/yyyy"),
            //                TotalPrice = $"{vehicleInsurancePolicy.TotalPrize.ToString("#,##0.00")} €"
            //            };
            //            insuranceCoverageSummary.InsuranceCoverageGrids.Add(insuranceCoverageGrid);
            //            break;
            //        case EnumInsurancePolicyCategory.Imbarcazione:
            //            break;
            //        case EnumInsurancePolicyCategory.Viaggi:
            //            var travelInsurancePolicy = UnitOfWork.InsurancePolicies.GetInsurancePolicyTravel(item.InsurancePolicyCode).FirstOrDefault();
            //            insuranceCoverageGrid = new InsuranceCoverageGrid
            //            {
            //                Code = travelInsurancePolicy.InsurancePolicyCode,
            //                CategoryType = item.InsurancePolicyCategory.InsurancePolicyCategoryName,
            //                ItemDescription = travelInsurancePolicy.Travels.GetItemDescription(),
            //                IssueDate = travelInsurancePolicy.IssueDate.ToString("dd/MM/yyyy"),
            //                ExpiryDate = travelInsurancePolicy.ExpiryDate.ToString("dd/MM/yyyy"),
            //                TotalPrice = $"{travelInsurancePolicy.TotalPrize.ToString("#,##0.00")} €"
            //            };
            //            insuranceCoverageSummary.InsuranceCoverageGrids.Add(insuranceCoverageGrid);
            //            break;
            //        case EnumInsurancePolicyCategory.Vacanza:
            //            var vacationInsurancePolicy = UnitOfWork.InsurancePolicies.GetInsurancePolicyVacation(item.InsurancePolicyCode).FirstOrDefault();
            //            insuranceCoverageGrid = new InsuranceCoverageGrid
            //            {
            //                Code = vacationInsurancePolicy.InsurancePolicyCode,
            //                CategoryType = item.InsurancePolicyCategory.InsurancePolicyCategoryName,
            //                ItemDescription = vacationInsurancePolicy.Vacations.GetItemDescription(),
            //                IssueDate = vacationInsurancePolicy.IssueDate.ToString("dd/MM/yyyy"),
            //                ExpiryDate = vacationInsurancePolicy.ExpiryDate.ToString("dd/MM/yyyy"),
            //                TotalPrice = $"{vacationInsurancePolicy.TotalPrize.ToString("#,##0.00")} €"
            //            };
            //            insuranceCoverageSummary.InsuranceCoverageGrids.Add(insuranceCoverageGrid);
            //            break;
            //        case EnumInsurancePolicyCategory.PerditaBagaglio:
            //            var insurancePolicyBaggageLoss = UnitOfWork.InsurancePolicies.GetInsurancePolicyBaggageLoss(item.InsurancePolicyCode);
            //            break;
            //        case EnumInsurancePolicyCategory.FamiliareeCongiunto:
            //            var familyInsurancePolicy = UnitOfWork.InsurancePolicies.GetFamilyInsurancePolicy(item.InsurancePolicyCode);
            //            break;
            //        case EnumInsurancePolicyCategory.AnimaleDomestico:
            //            var petInsurancePolicy = UnitOfWork.InsurancePolicies.GetPetInsurancePolicy(item.InsurancePolicyCode);
            //            break;
            //        case EnumInsurancePolicyCategory.VisiteSpecialistiche:
            //        case EnumInsurancePolicyCategory.GrandiInterventi:
            //        case EnumInsurancePolicyCategory.CureOdontoiatriche:
            //            var healthInsurancePolicy = UnitOfWork.InsurancePolicies.GetHealthInsurancePolicy(item.InsurancePolicyCode);
            //            break;
            //    }
            //}

            //_insuranceCoverageSummaryRepository.InsertOne(insuranceCoverageSummary);
            //return insuranceCoverageSummary.InsuranceCoverageGrids;
        }

        public IList<InsuranceCategoryPolicyDashboardCard> GetRecommendationInsuranceCategoryPolicyDashboardCards(string customerCode)
        {
            var customer = UnitOfWork.Customers.Find(x => x.CustomerCode == customerCode);
            if (customer == null || customer.Count == 0)
                return new List<InsuranceCategoryPolicyDashboardCard>();

            var insurancePolicyCategoryIds = UnitOfWork.InsurancePolicyCategories
                                                       .GetInsurancePolicyCategories()
                                                       .Select(x => x.Id)
                                                       .ToList();

            //var modelOutputRecommendationCollection = new List<ModelOutput>();
            //ModelOutput modelOutput = null;
            //foreach (var itemId in insurancePolicyCategoryIds)
            //{
            //    modelOutput = _learningManager.GetRecommendation(customer[0].Id, itemId);
            //    modelOutputRecommendationCollection.Add(modelOutput);
            //}

            var insuranceCategoryPolicyDashboardCards = new List<InsuranceCategoryPolicyDashboardCard>();
            //foreach (var itemOutput in modelOutputRecommendationCollection)
            //{
            //    var categoryPolicyCard = UnitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategory((int)itemOutput.InsurancePolicyCategoryId).SingleOrDefault();
            //    insuranceCategoryPolicyDashboardCards.Add(new InsuranceCategoryPolicyDashboardCard
            //    {
            //        Code = categoryPolicyCard.InsurancePolicyCategoryCode,
            //        Name = categoryPolicyCard.InsurancePolicyCategoryName,
            //        Abstract = categoryPolicyCard.InsurancePolicyCategoryDescription.Length > 170 ?
            //                                     categoryPolicyCard.InsurancePolicyCategoryDescription.Substring(0, 170) :
            //                                     categoryPolicyCard.InsurancePolicyCategoryDescription,
            //        IconCssClass = categoryPolicyCard.IconCssClass,
            //        SalesLineBackgroundColor = categoryPolicyCard.SalesLine.BackGroundColor,
            //        SalesLineBackgroundCssClass = categoryPolicyCard.SalesLine.BackGroundColorCssClass,
            //        SalesLineCode = categoryPolicyCard.SalesLine.SalesLineCode,
            //        SalesLineName = categoryPolicyCard.SalesLine.SalesLineName
            //    });
            //    if (insuranceCategoryPolicyDashboardCards.Count >= 6)
            //        break;
            //}
            return insuranceCategoryPolicyDashboardCards;
        }

        public IList<InsuranceCategoryPolicyDashboardCard> GetTopSellingInsuranceCategoryPolicyDashboardCards(int year, int top, IEnumerable<string> incuranceCoverageCodes)
        {
            var insuranceCategoryPolicyTopSelling = _insuranceCategoryPolicyTopSellingRepository.GetInsuranceCategoryPolicyTopSelling(year);
            if (insuranceCategoryPolicyTopSelling != null)
                return insuranceCategoryPolicyTopSelling.InsuranceCategoryPolicies
                                                        .Where(x => !incuranceCoverageCodes.Any(y => y == x.Code))
                                                        .OrderByDescending(x => x.Total)
                                                        .Take(top)
                                                        .ToList();

            var insurancePolicyCategories = UnitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategoryStatistics(year)
                                                                                     .GroupBy(x => x.Id)
                                                                                     .ToList();
            insuranceCategoryPolicyTopSelling = new InsuranceCategoryPolicyTopSelling();
            insuranceCategoryPolicyTopSelling.Year = year;
            insuranceCategoryPolicyTopSelling.InsuranceCategoryPolicies = new List<InsuranceCategoryPolicyDashboardCard>();

            foreach (var item in insurancePolicyCategories)
            {
                var subItem = item.FirstOrDefault();
                insuranceCategoryPolicyTopSelling.InsuranceCategoryPolicies.Add(
                    new InsuranceCategoryPolicyDashboardCard
                    {
                        Code = subItem.InsurancePolicyCategoryCode,
                        Name = subItem.InsurancePolicyCategoryName,
                        Abstract = subItem.InsurancePolicyCategoryDescription.Length > 170 ? subItem.InsurancePolicyCategoryDescription.Substring(0, 170) + "..." : subItem.InsurancePolicyCategoryDescription,
                        IconCssClass = subItem.IconCssClass,
                        SalesLineBackgroundColor = subItem.SalesLine.BackGroundColor,
                        SalesLineBackgroundCssClass = subItem.SalesLine.BackGroundColorCssClass,
                        SalesLineCode = subItem.SalesLine.SalesLineCode,
                        SalesLineName = subItem.SalesLine.SalesLineName,
                        Total = subItem.InsurancePolicyCategoryStatics.Sum(x => x.TotalCount)
                    });
            }

            if (insuranceCategoryPolicyTopSelling == null)
                return new List<InsuranceCategoryPolicyDashboardCard>();

            if (insuranceCategoryPolicyTopSelling.InsuranceCategoryPolicies.Count == 0)
                return insuranceCategoryPolicyTopSelling.InsuranceCategoryPolicies;

            _insuranceCategoryPolicyTopSellingRepository.InsertOne(insuranceCategoryPolicyTopSelling, false);

            return insuranceCategoryPolicyTopSelling.InsuranceCategoryPolicies
                                                    .Where(x => !incuranceCoverageCodes.Any(y => y == x.Code))
                                                    .OrderByDescending(x => x.Total)
                                                    .Take(top)
                                                    .ToList();
        }

        public IList<InsuranceCategoryPolicyDashboardCard> GetOtherInsuranceCategoryPolicyDashboardCards(IEnumerable<string> insuranceCategoryPolicyCodes)
        {
            var insuranceCategoryPolicyDashboardCards = _insuranceCategoryPolicyDashboardCardRepository.GetAll();
            if (insuranceCategoryPolicyDashboardCards != null && insuranceCategoryPolicyDashboardCards.Count != 0)
                return insuranceCategoryPolicyDashboardCards
                       .Where(x => !insuranceCategoryPolicyCodes.Any(y => y == x.Code))
                       .ToList();

            insuranceCategoryPolicyDashboardCards = UnitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategories()
                                                                  .Select(x => new InsuranceCategoryPolicyDashboardCard
                                                                  {
                                                                      Code = x.InsurancePolicyCategoryCode,
                                                                      Name = x.InsurancePolicyCategoryName,
                                                                      Abstract = x.InsurancePolicyCategoryDescription.Length > 170 ? x.InsurancePolicyCategoryDescription.Substring(0, 170) : x.InsurancePolicyCategoryDescription,
                                                                      IconCssClass = x.IconCssClass,
                                                                      SalesLineBackgroundColor = x.SalesLine.BackGroundColor,
                                                                      SalesLineBackgroundCssClass = x.SalesLine.BackGroundColorCssClass,
                                                                      SalesLineCode = x.SalesLine.SalesLineCode,
                                                                      SalesLineName = x.SalesLine.SalesLineName
                                                                  })
                                                                  .ToList();

            _insuranceCategoryPolicyDashboardCardRepository.InsertMany(insuranceCategoryPolicyDashboardCards);

            return insuranceCategoryPolicyDashboardCards
                  .Where(x => !insuranceCategoryPolicyCodes.Any(y => y == x.Code))
                  .ToList();
        }

        public override void Dispose()
        {
            if (UnitOfWork.IsTransactionOpened)
            {
                if (_countError > 0)
                    UnitOfWork.RollbackTransaction();
                else
                    UnitOfWork.CommitTransaction();
            }

            UnitOfWork.Dispose();
        }
    }
}
