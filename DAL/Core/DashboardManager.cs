using DAL.Core.Interfaces;
using DAL.Exstensions;
using DAL.Mapping;
using DAL.Models;
using DAL.ModelsNoSql;
using DAL.ModelsRabbitMQ;
using DAL.RepositoryNoSql.Interfaces;
using System;
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
        private readonly ILearningManager _learningManager;

        private IList<string> getDescriptionEmpty(InsurancePolicy insurancePolicy) => new List<string>();
        private IList<string> getDescriptionVehicles(InsurancePolicy insurancePolicy)
        {
            var vehicles = UnitOfWork.Vehicles.GetVehicles(insurancePolicy.Id);
            return vehicles.GetVehicleDescriptions();
        }
        private IList<string> getDescriptionPets(InsurancePolicy insurancePolicy)
        {
            var pets = UnitOfWork.Pets.GetPets(insurancePolicy.Id);
            return pets.GetPetDescriptions();
        }
        private IList<string> getDescriptionSportEvents(InsurancePolicy insurancePolicy)
        {
            var sportEvents = UnitOfWork.SportEvents.GetSportEvents(insurancePolicy.Id);
            return sportEvents.GetSportEventDescriptions();
        }
        private IList<string> getDescriptionHouses(InsurancePolicy insurancePolicy)
        {
            var houses = UnitOfWork.Houses.GetHouses(insurancePolicy.Id);
            return houses.GetHouseDescriptions();
        }
        private IList<string> getDescriptionLargeBuildings(InsurancePolicy insurancePolicy)
        {
            var largeBuildings = UnitOfWork.LargeBuildings.GetLargeBuildings(insurancePolicy.Id);
            return largeBuildings.GetLargeBuildingDescription();
        }
        private IList<string> getDescriptionInjuries(InsurancePolicy insurancePolicy)
        {
            var injuries = UnitOfWork.Injuries.GetInjuries(insurancePolicy.Id);
            return injuries.GetInjuryDescriptions();
        }
        private IList<string> getDescriptionIllnesses(InsurancePolicy insurancePolicy)
        {
            var illnesses = UnitOfWork.Illnesses.GetIllnesses(insurancePolicy.Id);
            return illnesses.GetIllnessDescriptions();
        }
        private IList<string> getDescriptionBusinesses(InsurancePolicy insurancePolicy)
        {
            var businesses = UnitOfWork.Businesses.GetBusinesses(insurancePolicy.Id);
            return businesses.GetBusinessDescriptions();
        }
        private IList<string> getDescriptionLegalProtection(InsurancePolicy insurancePolicy)
        {
            var legalProtections = UnitOfWork.LegalProtections.GetLegalProtections(insurancePolicy.Id);
            return legalProtections.GetLegalProtectionDescriptions();
        }
        private IList<string> getWarrantySelecteds(InsurancePolicy insurancePolicy)
        {
            var warrantySelecteds = new List<string>();
            var primaryWarranty = string.Empty;
            foreach (var item in insurancePolicy.WarrantySelecteds)
            {
                if (!item.WarrantyAvaible.IsPrimary)
                    warrantySelecteds.Add($"{item.WarrantyAvaible.WarrantyCode} - {item.WarrantyAvaible.WarrantyName}");
                else
                    primaryWarranty = $"{item.WarrantyAvaible.WarrantyCode} - {item.WarrantyAvaible.WarrantyName} - garanzia obbligatoria";
            }
            warrantySelecteds.Insert(0, primaryWarranty);
            return warrantySelecteds;
        }
        private InsuranceCoverageGrid getInsuranceCoverageGrid(InsurancePolicy insurancePolicy)
        {
            var enumInsurancePolicyCategory = (EnumInsurancePolicyCategory)insurancePolicy.InsurancePolicyCategoryId;
            var insuranceCoverageGrid = insurancePolicy.ToInsuranceCoverageGrid();
            insuranceCoverageGrid.WarrantySelecteds = getWarrantySelecteds(insurancePolicy);

            switch (enumInsurancePolicyCategory)
            {
                case EnumInsurancePolicyCategory.None:
                    insuranceCoverageGrid.ItemDescriptions = getDescriptionEmpty(insurancePolicy);
                    break;
                case EnumInsurancePolicyCategory.RCA:
                    insuranceCoverageGrid.ItemDescriptions = getDescriptionVehicles(insurancePolicy);
                    break;
                case EnumInsurancePolicyCategory.ARD:
                    insuranceCoverageGrid.ItemDescriptions = getDescriptionPets(insurancePolicy);
                    break;
                case EnumInsurancePolicyCategory.RC_DIVERSI:
                    insuranceCoverageGrid.ItemDescriptions = getDescriptionSportEvents(insurancePolicy);
                    break;
                case EnumInsurancePolicyCategory.MULTIGARANZIA_ABITAZIONE:
                    insuranceCoverageGrid.ItemDescriptions = getDescriptionHouses(insurancePolicy);
                    break;
                case EnumInsurancePolicyCategory.GLOBALE_FABBRICATI:
                    insuranceCoverageGrid.ItemDescriptions = getDescriptionLargeBuildings(insurancePolicy);
                    break;
                case EnumInsurancePolicyCategory.INFORTUNI:
                    insuranceCoverageGrid.ItemDescriptions = getDescriptionInjuries(insurancePolicy);
                    break;
                case EnumInsurancePolicyCategory.MALATTIA:
                    insuranceCoverageGrid.ItemDescriptions = getDescriptionIllnesses(insurancePolicy);
                    break;
                case EnumInsurancePolicyCategory.INCENDIO_FURTO:
                    insuranceCoverageGrid.ItemDescriptions = getDescriptionBusinesses(insurancePolicy);
                    break;
                case EnumInsurancePolicyCategory.TUTELA_GIUDIZIARIA:
                    insuranceCoverageGrid.ItemDescriptions = getDescriptionLegalProtection(insurancePolicy);
                    break;
                default:
                    insuranceCoverageGrid.ItemDescriptions = getDescriptionEmpty(insurancePolicy);
                    break;
            }
            return insuranceCoverageGrid;
        }
        private InsuranceCoverageSummary getInsuranceCoverageSummary(string customerCode)
        {
            var insuranceCoverageSummary = new InsuranceCoverageSummary();
            insuranceCoverageSummary.CustomerCode = customerCode;
            insuranceCoverageSummary.InsuranceCoverageGrids = new List<InsuranceCoverageGrid>();

            var insurancePolicies = UnitOfWork.InsurancePolicies
                                              .GetInsurancePolicies(customerCode)
                                              .ToList();
            foreach (var item in insurancePolicies)
                insuranceCoverageSummary.InsuranceCoverageGrids.Add(getInsuranceCoverageGrid(item));

            return insuranceCoverageSummary;
        }

        public DashboardManager(IUnitOfWork unitOfWork,
                                ICustomerHeaderRepository customerHeaderRepository,
                                ICustomerDetailRepository customerDetailRepository,
                                IInsuranceCategoryPolicyDashboardCardRepository insuranceCategoryPolicyDashboardCardRepository,
                                IInsuranceCategoryPolicyTopSellingRepository insuranceCategoryPolicyTopSellingRepository,
                                IInsuranceCoverageSummaryRepository insuranceCoverageSummaryRepository,
                                IInsuranceCoverageChartRepository insuranceCoverageChartRepository,
                                IInsuranceCategoryPolicyRecommendationRepository insuranceCategoryPolicyRecommendationRepository,
                                ILearningManager learningManager)
            : base(unitOfWork)
        {
            _customerHeaderRepository = customerHeaderRepository;
            _customerDetailRepository = customerDetailRepository;
            _insuranceCategoryPolicyDashboardCardRepository = insuranceCategoryPolicyDashboardCardRepository;
            _insuranceCategoryPolicyTopSellingRepository = insuranceCategoryPolicyTopSellingRepository;
            _insuranceCoverageSummaryRepository = insuranceCoverageSummaryRepository;
            _insuranceCoverageChartRepository = insuranceCoverageChartRepository;
            _insuranceCategoryPolicyRecommendationRepository = insuranceCategoryPolicyRecommendationRepository;
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

            var customerInsuranceCoverages = new List<Tuple<SalesLineType, IList<InsurancePolicy>>>();
            var salesLines = UnitOfWork.SalesLines.GetSalesLineTypes(customerCode).ToList();
            foreach (var item in salesLines)
            {
                var insurancePolicies = UnitOfWork.InsurancePolicies.GetInsurancePolicies(customerCode, item.Id).ToList();
                customerInsuranceCoverages.Add(new Tuple<SalesLineType, IList<InsurancePolicy>>(item, insurancePolicies));
            }

            insuranceCoverageChart = customerInsuranceCoverages.ToInsuranceCoverageChart(customerCode);
            _insuranceCoverageChartRepository.InsertOne(insuranceCoverageChart);
            return insuranceCoverageChart.SalesLineCharts;
        }

        public IList<InsuranceCoverageGrid> GetInsuranceCoverageGridSummaries(string customerCode)
        {
            var insuranceCoverageSummary = _insuranceCoverageSummaryRepository.GetInsuranceCoverageSummary(customerCode);
            if (insuranceCoverageSummary != null)
                return insuranceCoverageSummary.InsuranceCoverageGrids;

            insuranceCoverageSummary = getInsuranceCoverageSummary(customerCode);
            var insuranceCoverageSummaryRepository = _insuranceCoverageSummaryRepository.GetInsuranceCoverageSummary(insuranceCoverageSummary.CustomerCode);
            if (insuranceCoverageSummaryRepository == null)
            {
                _insuranceCoverageSummaryRepository.InsertOne(insuranceCoverageSummary);
                return insuranceCoverageSummary.InsuranceCoverageGrids;
            }

            _insuranceCoverageSummaryRepository.UpdateInsuranceCoverageSummary(insuranceCoverageSummary.CustomerCode, insuranceCoverageSummary);
            return insuranceCoverageSummary.InsuranceCoverageGrids;
        }

        public IList<InsuranceCategoryPolicyDashboardCard> GetRecommendationInsuranceCategoryPolicyDashboardCards(string customerCode)
        {
            var customer = UnitOfWork.Customers.Find(x => x.CustomerCode == customerCode);
            if (customer == null || customer.Count == 0)
                return new List<InsuranceCategoryPolicyDashboardCard>();

            var insuranceCategoryPolicyTopSelling = _insuranceCategoryPolicyTopSellingRepository.GetInsuranceCategoryPolicyTopSelling(DateTime.Now.Year - 1);
            IList<InsuranceCategoryPolicyDashboardCard> insuranceCategoryPolicyDashboardCards = new List<InsuranceCategoryPolicyDashboardCard>();
            if (insuranceCategoryPolicyTopSelling != null)
                insuranceCategoryPolicyDashboardCards = insuranceCategoryPolicyTopSelling.InsuranceCategoryPolicies
                                                                                         .OrderByDescending(x => x.Total)
                                                                                         .Take(2)
                                                                                         .ToList();

            var insurancePolicyCategoryRecommendation = _insuranceCategoryPolicyRecommendationRepository.GetInsuranceCategoryPolicyRecommendation(customerCode);
            if (insurancePolicyCategoryRecommendation != null)
            {
                foreach (var item in insurancePolicyCategoryRecommendation.InsuranceCategoryPolicies)
                {
                    if (insuranceCategoryPolicyDashboardCards.Any(x => x.Code == item.Code))
                        item.IsTopSelling = true;
                }
                return insurancePolicyCategoryRecommendation.InsuranceCategoryPolicies;
            }
            var insurancePolicyCategory = _learningManager.GetRecommendation(customerCode, 0.80f, 2);
            var insuranceCategoryPolicyRecommendation = new InsuranceCategoryPolicyRecommendation();
            insuranceCategoryPolicyRecommendation.CustomerCode = customerCode;
            insuranceCategoryPolicyRecommendation.InsuranceCategoryPolicies = insurancePolicyCategory.ToInsuranceCategoryPolicyDashboardCards();
            _insuranceCategoryPolicyRecommendationRepository.InsertOne(insuranceCategoryPolicyRecommendation);

            foreach (var item in insurancePolicyCategoryRecommendation.InsuranceCategoryPolicies)
            {
                if (insuranceCategoryPolicyDashboardCards.Any(x => x.Code == item.Code))
                    item.IsTopSelling = true;
            }
            return insuranceCategoryPolicyRecommendation.InsuranceCategoryPolicies;
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
                insuranceCategoryPolicyTopSelling.InsuranceCategoryPolicies.Add(subItem.ToInsuranceCategoryPolicyDashboardCard());
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
