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
        private readonly IInsuranceCategoryPolicyRecommendationRepository _insuranceCategoryPolicyRecommendationRepository;

        public DashboardManager(IUnitOfWork unitOfWork,
                                ICustomerHeaderRepository customerHeaderRepository,
                                ICustomerDetailRepository customerDetailRepository,
                                IInsuranceCategoryPolicyDashboardCardRepository insuranceCategoryPolicyDashboardCardRepository,
                                IInsuranceCategoryPolicyTopSellingRepository insuranceCategoryPolicyTopSellingRepository,
                                IInsuranceCoverageSummaryRepository insuranceCoverageSummaryRepository,
                                IInsuranceCoverageChartRepository insuranceCoverageChartRepository,
                                IInsuranceCategoryPolicyRecommendationRepository insuranceCategoryPolicyRecommendationRepository) 
            : base(unitOfWork)
        {
            _customerHeaderRepository = customerHeaderRepository;
            _customerDetailRepository = customerDetailRepository;
            _insuranceCategoryPolicyDashboardCardRepository = insuranceCategoryPolicyDashboardCardRepository;
            _insuranceCategoryPolicyTopSellingRepository = insuranceCategoryPolicyTopSellingRepository;
            _insuranceCoverageSummaryRepository = insuranceCoverageSummaryRepository;
            _insuranceCoverageChartRepository = insuranceCoverageChartRepository;
            _insuranceCategoryPolicyRecommendationRepository= insuranceCategoryPolicyRecommendationRepository;
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
        }

        public IList<InsuranceCategoryPolicyDashboardCard> GetRecommendationInsuranceCategoryPolicyDashboardCards(string customerCode)
        {
            var customer = UnitOfWork.Customers.Find(x => x.CustomerCode == customerCode);
            if (customer == null || customer.Count == 0)
                return new List<InsuranceCategoryPolicyDashboardCard>();

            var insurancePolicyCategoryRecommendation = _insuranceCategoryPolicyRecommendationRepository.GetInsuranceCategoryPolicyRecommendation(customerCode);
            if (insurancePolicyCategoryRecommendation != null)
                return insurancePolicyCategoryRecommendation.InsuranceCategoryPolicies;

            return new List<InsuranceCategoryPolicyDashboardCard>();
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
