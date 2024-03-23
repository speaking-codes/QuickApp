using DAL.BuilderModel;
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

        private VehicleInsurancePolicy getVehicleInsurancePolicy(string insurancePolicyCode) =>
            _unitOfWork.InsurancePolicies.GetVehicleInsurancePolicy(insurancePolicyCode).FirstOrDefault();
        private FamilyInsurancePolicy getFamilyInsurancePolicy(string insurancePolicyCode) =>
            _unitOfWork.InsurancePolicies.GetFamilyInsurancePolicy(insurancePolicyCode).FirstOrDefault();
        private PetInsurancePolicy getPetInsurancePolicy(string insurancePolicyCode) =>
            _unitOfWork.InsurancePolicies.GetPetInsurancePolicy(insurancePolicyCode).FirstOrDefault();
        private HouseInsurancePolicy GetHouseInsurancePolicy(string insurancePolicyCode) =>
            _unitOfWork.InsurancePolicies.GetHouseInsurancePolicy(insurancePolicyCode).FirstOrDefault();

        private InsuranceCoverageGrid getInsuranceCoverageGrid(InsurancePolicy insurancePolicy)
        {
            var enumInsurancePolicyCategory = (EnumInsurancePolicyCategory)insurancePolicy.InsurancePolicyCategoryId;

            switch (enumInsurancePolicyCategory)
            {
                case EnumInsurancePolicyCategory.None:
                    break;
                case EnumInsurancePolicyCategory.Auto:
                case EnumInsurancePolicyCategory.Moto:
                    return getVehicleInsurancePolicy(insurancePolicy.InsurancePolicyCode).ToInsuranceCoverageGrid();
                case EnumInsurancePolicyCategory.Imbarcazione:
                    break;
                case EnumInsurancePolicyCategory.Viaggi:
                    break;
                case EnumInsurancePolicyCategory.Vacanza:
                    break;
                case EnumInsurancePolicyCategory.PerditaBagaglio:
                    break;
                case EnumInsurancePolicyCategory.AttivitàProfessionale:
                    break;
                case EnumInsurancePolicyCategory.ImmobileAziendale:
                    break;
                case EnumInsurancePolicyCategory.AttivitàCommerciale:
                    break;
                case EnumInsurancePolicyCategory.AttivitàAgricola:
                    break;
                case EnumInsurancePolicyCategory.AllevamentoBestiame:
                    break;
                case EnumInsurancePolicyCategory.FamiliareeCongiunto:
                    return getFamilyInsurancePolicy(insurancePolicy.InsurancePolicyCode).ToInsuranceCoverageGrid();
                case EnumInsurancePolicyCategory.AnimaleDomestico:
                    return getPetInsurancePolicy(insurancePolicy.InsurancePolicyCode).ToInsuranceCoverageGrid();
                case EnumInsurancePolicyCategory.Casa:
                    return GetHouseInsurancePolicy(insurancePolicy.InsurancePolicyCode).ToInsuranceCoverageGrid();
                case EnumInsurancePolicyCategory.Infortunio:
                    break;
                case EnumInsurancePolicyCategory.Malattia:
                    break;
                case EnumInsurancePolicyCategory.VisiteSpecialistiche:
                    break;
                case EnumInsurancePolicyCategory.GrandiInterventi:
                    break;
                case EnumInsurancePolicyCategory.CureOdontoiatriche:
                    break;
                default:
                    break;
            }
            return new InsuranceCoverageGrid();
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

          await  UpdateLearningTable(_customerInsurancePolicyQueue);

        }
    }
}
