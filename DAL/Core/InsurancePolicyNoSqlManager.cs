using DAL.BuilderModel;
using DAL.BuilderModel.Interfaces;
using DAL.Exstensions;
using DAL.Mapping;
using DAL.Models;
using DAL.ModelsNoSql;
using DAL.ModelsRabbitMQ;
using DAL.Repositories.Interfaces;
using DAL.RepositoryNoSql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    internal abstract class InsurancePolicyNoSqlManager
    {
        private readonly CustomerInsurancePolicyQueue _customerInsurancePolicyQueue;

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IInsurancePolicyRepository _insurancePolicies;
        protected readonly IInsuranceCoverageChartRepository _insuranceCoverageChartRepository;
        protected readonly IInsuranceCoverageSummaryRepository _insuranceCoverageSummaryRepository;

        public InsurancePolicyNoSqlManager(IUnitOfWork unitOfWork,
                                           IInsuranceCoverageChartRepository insuranceCoverageChartRepository,
                                           IInsuranceCoverageSummaryRepository insuranceCoverageSummaryRepository,
                                           CustomerInsurancePolicyQueue customerInsurancePolicyQueue)
        {
            _unitOfWork = unitOfWork;
            _insurancePolicies = unitOfWork.InsurancePolicies;
            _insuranceCoverageChartRepository = insuranceCoverageChartRepository;
            _insuranceCoverageSummaryRepository = insuranceCoverageSummaryRepository;
            _customerInsurancePolicyQueue = customerInsurancePolicyQueue;
        }

        private InsuranceCoverageChart getInsuranceCoverageChart(string customerCode)
        {
            var customerInsuranceCoverages = new List<Tuple<SalesLineType, IList<InsurancePolicy>>>();
            var salesLines = _unitOfWork.SalesLines.GetSalesLineTypes(customerCode).ToList();
            foreach (var item in salesLines)
            {
                var insurancePolicies = _unitOfWork.InsurancePolicies.GetInsurancePolicies(customerCode, item.Id).ToList();
                customerInsuranceCoverages.Add(new Tuple<SalesLineType, IList<InsurancePolicy>>(item, insurancePolicies));
            }

            return customerInsuranceCoverages.ToInsuranceCoverageChart(customerCode);
        }

        private IList<string> getDescriptionEmpty(InsurancePolicy insurancePolicy) => new List<string>();
        private IList<string> getDescriptionVehicles(InsurancePolicy insurancePolicy)
        {
            var vehicles = _unitOfWork.Vehicles.GetVehicles(insurancePolicy.Id);
            return vehicles.GetVehicleDescriptions();
        }
        private IList<string> getDescriptionPets(InsurancePolicy insurancePolicy)
        {
            var pets = _unitOfWork.Pets.GetPets(insurancePolicy.Id);
            return pets.GetPetDescriptions();
        }
        private IList<string> getDescriptionSportEvents(InsurancePolicy insurancePolicy)
        {
            var sportEvents = _unitOfWork.SportEvents.GetSportEvents(insurancePolicy.Id);
            return new List<string>();
        }
        private IList<string> getDescriptionHouses(InsurancePolicy insurancePolicy)
        {
            var houses = _unitOfWork.Houses.GetHouses(insurancePolicy.Id);
            return houses.GetHouseDescriptions();
        }
        private IList<string> getDescriptionLargeBuildings(InsurancePolicy insurancePolicy)
        {
            var largeBuildings=_unitOfWork.LargeBuildings.GetLargeBuildings(insurancePolicy.Id);
            return new List<string>();
        }
        private IList<string> getDescriptionInjuries(InsurancePolicy insurancePolicy)
        {
            var injuries = _unitOfWork.Injuries.GetInjuries(insurancePolicy.Id);
            return new List<string>();
        }
        private IList<string> getDescriptionIllnesses(InsurancePolicy insurancePolicy)
        {
            var illnesses = _unitOfWork.Illnesses.GetIllnesses(insurancePolicy.Id);
            return new List<string>();
        }
        private IList<string> getDescriptionLegalProtection(InsurancePolicy insurancePolicy)
        {
            var legalProtections = _unitOfWork.LegalProtections.GetLegalProtections(insurancePolicy.Id);
            return legalProtections.GetLegalProtectionDescriptions();
        }
        private InsuranceCoverageGrid getInsuranceCoverageGrid(InsurancePolicy insurancePolicy)
        {
            var enumInsurancePolicyCategory = (EnumInsurancePolicyCategory)insurancePolicy.InsurancePolicyCategoryId;
            var insuranceCoverageGrid = insurancePolicy.ToInsuranceCoverageGrid();

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
                    insuranceCoverageGrid.ItemDescriptions = getDescriptionEmpty(insurancePolicy);
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

            var insurancePolicies = _unitOfWork.InsurancePolicies
                                               .GetInsurancePolicies(customerCode)
                                               .Select(x => new InsurancePolicy
                                               {
                                                   InsurancePolicyCode = x.InsurancePolicyCode,
                                                   InsurancePolicyCategoryId = x.InsurancePolicyCategoryId,
                                               })
                                               .ToList();
            foreach (var item in insurancePolicies)
                insuranceCoverageSummary.InsuranceCoverageGrids.Add(getInsuranceCoverageGrid(item));

            return insuranceCoverageSummary;
        }

        public abstract void UpdateInsuranceCoverageChart(InsuranceCoverageChart insuranceCoverageChart);

        public abstract void UpdateInsuranceCoverageSummary(InsuranceCoverageSummary insuranceCoverageSummary);

        public abstract Task UpdateLearningTable(CustomerInsurancePolicyQueue customerInsurancePolicyQueue);

        public abstract Task UpdateMatrixUserItem(CustomerInsurancePolicyQueue customerInsurancePolicyQueue);

        public async Task Execute()
        {
            var insuranceCoverageChart = getInsuranceCoverageChart(_customerInsurancePolicyQueue.CustomerCode);
            UpdateInsuranceCoverageChart(insuranceCoverageChart);

            var insuranceCoverageSummary = getInsuranceCoverageSummary(_customerInsurancePolicyQueue.CustomerCode);
            UpdateInsuranceCoverageSummary(insuranceCoverageSummary);

            await UpdateLearningTable(_customerInsurancePolicyQueue);

        }
    }
}
