using ConsoleAppCaricamentoDati.Builder;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati.BuilderManager
{
    public class StateInsurancePolicyBuildManager : IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IList<InsurancePolicyCategory> _categoryList;
        private readonly Random _random;

        public StateInsurancePolicyBuildManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryList = _unitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategories().ToList();
            _random = new Random();
        }

        public IList<Customer> GetCustomers() =>
            _unitOfWork.Customers.GetAllCustomers().ToList();

        public IList<InsurancePolicyBuilder> GetInsurancePolicyBuilderManagers(Customer customer)
        {
            var insurancePolicyBuilders = new List<InsurancePolicyBuilder>();
            var index = _random.Next(0, _categoryList.Count);
            var enumInsurancePolicyCategory = (EnumInsurancePolicyCategory)_categoryList[index].Id;

            switch (enumInsurancePolicyCategory)
            {
                case EnumInsurancePolicyCategory.None:
                    break;
                case EnumInsurancePolicyCategory.Auto:
                    insurancePolicyBuilders.Add(new CarInsurancePolicyBuilder(_categoryList[index], customer, _unitOfWork));
                    break;
                case EnumInsurancePolicyCategory.Moto:
                    insurancePolicyBuilders.Add(new BykeInsurancePolicyBuilder(_categoryList[index], customer, _unitOfWork));
                    break;
                case EnumInsurancePolicyCategory.Viaggi:
                    insurancePolicyBuilders.Add(new TravelInsurancePolicyBuilder(_categoryList[index], customer, _unitOfWork));
                    break;
                case EnumInsurancePolicyCategory.Vacanza:
                    insurancePolicyBuilders.Add(new VacationInsurancePolicyBuilder(_categoryList[index], customer, _unitOfWork));
                    break;
                case EnumInsurancePolicyCategory.PerditaBagaglio:
                    insurancePolicyBuilders.Add(new BaggageLossInsurancePolicyBuilder(_categoryList[index], customer, _unitOfWork));
                    break;
                case EnumInsurancePolicyCategory.FamiliareeCongiunto:
                    insurancePolicyBuilders.Add(new FamilyInsurancePolicyBuilder(_categoryList[index], customer, _unitOfWork));
                    break;
                case EnumInsurancePolicyCategory.AnimaleDomestico:
                    insurancePolicyBuilders.Add(new PetInsurancePolicyBuilder(_categoryList[index], customer, _unitOfWork));
                    break;
                case EnumInsurancePolicyCategory.VisiteSpecialistiche:
                    insurancePolicyBuilders.Add(new SpecialisticExaminationsInsurancePolicyBuilder(_categoryList[index], customer, _unitOfWork));
                    break;
                case EnumInsurancePolicyCategory.GrandiInterventi:
                    insurancePolicyBuilders.Add(new GreatInterventionsInsurancePolicyBuilder(_categoryList[index], customer, _unitOfWork));
                    break;
                case EnumInsurancePolicyCategory.CureOdontoiatriche:
                    insurancePolicyBuilders.Add(new DentalCareInsurancePolicyBuilder(_categoryList[index], customer, _unitOfWork));
                    break;
                default:
                    break;
            }

            return insurancePolicyBuilders;
        }

        public IList<InsurancePolicy> GetInsurancePolicies(IList<InsurancePolicyBuilder> insurancePolicyBuilders)
        {
            var insurancePolicies = new List<InsurancePolicy>();
            foreach (var itemBuilder in insurancePolicyBuilders)
            {
                insurancePolicies.Add(itemBuilder.SetIssueDate()
                                                 .SetDetailItem()
                                                 .SetIsLuxuryPolicy()
                                                 .SetInsuredMaximum()
                                                 .SetTotalPrize()
                                                 .SetExpireDate()
                                                 .Build());
            }
            return insurancePolicies;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
