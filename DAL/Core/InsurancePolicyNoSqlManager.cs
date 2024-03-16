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
        private readonly IUnitOfWork _unitOfWork;
        private readonly CustomerInsurancePolicyQueue _customerInsurancePolicyQueue;

        protected readonly IInsurancePolicyRepository _insurancePolicies;
        protected readonly IInsuranceCoverageChartRepository _insuranceCoverageChartRepository;
        private readonly IInsuranceCoverageSummaryRepository _insuranceCoverageSummaryRepository;

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

        private InsuranceCoverageGrid getInsuranceCoverageGrid(InsurancePolicy insurancePolicy)
        {
            var enumInsurancePolicyCategory = (EnumInsurancePolicyCategory)insurancePolicy.Id;

            switch (enumInsurancePolicyCategory)
            {
                case EnumInsurancePolicyCategory.None:
                    break;
                case EnumInsurancePolicyCategory.Auto:
                    break;
                case EnumInsurancePolicyCategory.Moto:
                    break;
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
                    break;
                case EnumInsurancePolicyCategory.AnimaleDomestico:
                    break;
                case EnumInsurancePolicyCategory.Casa:
                    break;
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

            var insurancePolicies = _unitOfWork.InsurancePolicies.GetInsurancePolicies(customerCode).ToList();
            foreach (var item in insurancePolicies)
                insuranceCoverageSummary.InsuranceCoverageGrids.Add(getInsuranceCoverageGrid(item));

            return insuranceCoverageSummary;
        }

        public abstract void UpadateInsuranceCoverageChart(InsuranceCoverageChart insuranceCoverageChart);

        public void Execute()
        {
            var insuranceCoverageChart = getInsuranceCoverageChart(_customerInsurancePolicyQueue.CustomerCode);
            UpadateInsuranceCoverageChart(insuranceCoverageChart);

            var insuranceCoverageSummary = getInsuranceCoverageSummary(_customerInsurancePolicyQueue.CustomerCode);
            //_insuranceCoverageSummaryRepository.InsertOne(insuranceCoverageSummary);

        }
    }
}
