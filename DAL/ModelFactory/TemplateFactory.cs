using DAL.BuilderModel;
using DAL.BuilderModel.Interfaces;
using DAL.BuilderModelTemplate;
using DAL.ModelFactory.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelFactory
{
    public class TemplateFactory : ITemplateFactory
    {
        private const string _basePath = @"C:\Users\mauro.diliddo\source\repos\QuickApp\QuickAppGitHub\QuickApp\ConsoleAppCaricamentoDati\DatiBase\";
        private const string _lastNamePath = $"{_basePath}Cognomi.txt";
        private const string _firstNameMalePath = $"{_basePath}NomiMaschili.txt";
        private const string _petNamePath = $"{_basePath}Pets.txt";
        private const string _firstNameFemalePath = $"{_basePath}NomiFemminili.txt";
        private const string _addressPath = $"{_basePath}Indirizzi.txt";
        private const string _jobPath = $"{_basePath}Professioni.txt";

        private readonly IUnitOfWork _unitOfWork;

        private IList<string> getLastName(string pathFile)
        {
            IList<string> lastNames;

            using (var reader = new StreamReader(pathFile))
                lastNames = reader.ReadToEnd().Split('\n').ToList();

            for (var i = 0; i < lastNames.Count; i++)
                lastNames[i] = lastNames[i].Replace('\r', ' ').Trim();

            return lastNames;
        }
        private IList<FirstNameTemplate> getFirstName(string pathFileMale, string pathFileFemale)
        {
            IList<FirstNameTemplate> firstNameTemplates;
            using (var reader = new StreamReader(pathFileMale))
                firstNameTemplates = reader.ReadToEnd()
                                            .Split('\n').Select(x =>
                                            new FirstNameTemplate
                                            {
                                                FirstName = x.Replace('\r', ' ').Trim(),
                                                IsMale = true
                                            }
                                            ).ToList();

            using (var reader = new StreamReader(pathFileFemale))
                ((List<FirstNameTemplate>)firstNameTemplates).AddRange(
                            reader.ReadToEnd()
                                    .Split('\n').Select(x =>
                                    new FirstNameTemplate
                                    {
                                        FirstName = x.Replace('\r', ' ').Trim(),
                                        IsMale = false
                                    }
                                    ).ToList());


            return firstNameTemplates;
        }
        private IList<string> getPetNames(string pathFile)
        {
            IList<string> petNames;

            using (var reader = new StreamReader(pathFile))
                petNames = reader.ReadToEnd().Split('\n').ToList();

            for (var i = 0; i < petNames.Count; i++)
                petNames[i] = petNames[i].Replace('\r', ' ').Trim();

            return petNames;
        }
        private IList<string> GetStreetNames(string pathFileStreet)
        {
            IList<string> streetNames;
            using (var reader = new StreamReader(pathFileStreet))
                streetNames = reader.ReadToEnd().Split('\n').ToList();

            for (var i = 0; i < streetNames.Count; i++)
                streetNames[i] = streetNames[i].Replace('\r', ' ').Trim();

            return streetNames;
        }
        private DeliveryModelTemplate getDeliveryModel() => new DeliveryModelTemplate();
        private IList<FamilyType> GetFamilyTypes() => _unitOfWork.FamilyTypes.GetAll();
        private IList<MaritalStatusType> GetMaritalStatusTypes() => _unitOfWork.MaritalStatusTypes.GetAll();
        private IList<Municipality> GetMunicipalities() => _unitOfWork.Municipalities.GetAll();
        private IList<ContractType> GetContractTypes() => _unitOfWork.ContractTypes.GetAll();
        private IList<IncomeType> GetIncomeTypes() => _unitOfWork.IncomeTypes.GetAll();
        private IList<ProfessionType> GetProfessionTypes() => _unitOfWork.ProfessionTypes.GetAll();
        private IList<double> GetIncomes()
        {
            var random = new Random();
            var incomes = new List<double>();
            double incomeBase = 9000;

            while (incomeBase < 180000)
            {
                incomes.Add(incomeBase);
                incomeBase = incomeBase + 1500 + 1000 * random.NextDouble();
            }
            return incomes;
        }
        private IList<InsurancePolicyCategory> GetInsurancePolicyCategories() => _unitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategories().ToList();
        private IList<ConfigurationModel> GetCarConfigurationModels() => _unitOfWork.ConfigurationModels.GetCarConfigurationModels().ToList();
        private IList<ConfigurationModel> GetBykeConfigurationModels() => _unitOfWork.ConfigurationModels.GetBykeConfigurationModels().ToList();
        private IList<TravelMeansType> GetTravelMeansTypes() => _unitOfWork.TravelMeansTypes.GetTravelMeansTypes().ToList();
        private IList<TravelClassType> GetTravelClassTypes() => _unitOfWork.TravelClassTypes.GetAll();
        private IList<StructureType> GetStructureTypes() => _unitOfWork.StructureTypes.GetAll();
        private IList<BaggageType> GetBaggageTypes() => _unitOfWork.BaggageTypes.GetAll();
        private IList<KinshipRelationshipType> GetKinshipRelationshipTypes() => _unitOfWork.KinshipRelationshipTypes.GetAll();
        private IList<BreedPetDetailType> GetBreedPetDetailTypes() => _unitOfWork.BreedPetDetailTypes.GetAll();

        public TemplateFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AdressModelTemplate CreateAddressModelTemplate()
        {
            var addressModel = new AdressModelTemplate();
            addressModel.StreetNames = GetStreetNames(_addressPath);
            addressModel.Municipalities = GetMunicipalities();
            return addressModel;
        }

        public CustomerModelTemplate CreateCustomerModelTemplate()
        {
            var customerModel = new CustomerModelTemplate();

            customerModel.LastNames = getLastName(_lastNamePath);
            customerModel.FirstNameTemplates = getFirstName(_firstNameMalePath, _firstNameFemalePath);
            customerModel.FamilyTypes = GetFamilyTypes();
            customerModel.MaritalStatuses = GetMaritalStatusTypes();
            customerModel.BirthMunicipalities = GetMunicipalities();

            customerModel.ContractTypes = GetContractTypes();
            customerModel.IncomeTypes = GetIncomeTypes();
            customerModel.ProfessionTypes = GetProfessionTypes();
            customerModel.Incomes = GetIncomes();

            customerModel.DeliveryModelTemplate = getDeliveryModel();
            customerModel.AddressTemplate = CreateAddressModelTemplate();

            return customerModel;
        }

        public InsurancePolicyTemplate CreateInsurancePolicyTemplate()
        {
            var insurancePolicyModel = new InsurancePolicyTemplate();
            insurancePolicyModel.LastNames = getLastName(_lastNamePath);
            insurancePolicyModel.FirstNameTemplates = getFirstName(_firstNameMalePath, _firstNameFemalePath);
            insurancePolicyModel.PetNames = getPetNames(_petNamePath);
            insurancePolicyModel.InsurancePolicyCategories = GetInsurancePolicyCategories();
            insurancePolicyModel.CarConfigurationModels = GetCarConfigurationModels();
            insurancePolicyModel.BykeConfigurationModels = GetBykeConfigurationModels();
            insurancePolicyModel.Municipalities = GetMunicipalities();
            insurancePolicyModel.TravelMeansTypes = GetTravelMeansTypes();
            insurancePolicyModel.TravelClassTypes = GetTravelClassTypes();
            insurancePolicyModel.StructureTypes = GetStructureTypes();
            insurancePolicyModel.ProfessionTypes = GetProfessionTypes();
            insurancePolicyModel.BaggageTypes = GetBaggageTypes();
            insurancePolicyModel.KinshipRelationshipTypes = GetKinshipRelationshipTypes();
            insurancePolicyModel.BreedPetDetailTypes = GetBreedPetDetailTypes();
            insurancePolicyModel.AddressTemplate = CreateAddressModelTemplate();
            return insurancePolicyModel;
        }

        public IList<IInsurancePolicyBuilder> CreateInsurancePolicyBuilders(int count, IList<InsurancePolicyCategory> insurancePolicyCategories, Random random)
        {
            var insurancePolicyBuilders = new List<IInsurancePolicyBuilder>();
            for (var i = 0; i < count ; i++)
            {
                var j = random.Next(insurancePolicyCategories.Count);
                var enumInsurancePolicyCategory = (EnumInsurancePolicyCategory)insurancePolicyCategories[j].Id;

                switch (enumInsurancePolicyCategory)
                {
                    case EnumInsurancePolicyCategory.None:
                        break;
                    case EnumInsurancePolicyCategory.Auto:
                        insurancePolicyBuilders.Add(new CarInsurancePolicyBuilder());
                        break;
                    case EnumInsurancePolicyCategory.Moto:
                        insurancePolicyBuilders.Add(new BykeInsurancePolicyBuilder());
                        break;
                    case EnumInsurancePolicyCategory.Imbarcazione:
                        break;
                    //case EnumInsurancePolicyCategory.Viaggi:
                    //    insurancePolicyBuilders.Add(new TravelInsurancePolicyBuilder());
                    //    break;
                    //case EnumInsurancePolicyCategory.Vacanza:
                    //    insurancePolicyBuilders.Add(new VacationInsurancePolicyBuilder());
                    //    break;
                    //case EnumInsurancePolicyCategory.PerditaBagaglio:
                    //    insurancePolicyBuilders.Add(new BaggageLossInsurancePolicyBuilder());
                    //    break;
                    //case EnumInsurancePolicyCategory.AttivitàProfessionale:
                    //    insurancePolicyBuilders.Add(new InsurancePolicyBuilder());
                    //    break;
                    //case EnumInsurancePolicyCategory.ImmobileAziendale:
                    //    insurancePolicyBuilders.Add(new InsurancePolicyBuilder());
                    //    break;
                    //case EnumInsurancePolicyCategory.AttivitàCommerciale:
                    //    insurancePolicyBuilders.Add(new InsurancePolicyBuilder());
                    //    break;
                    //case EnumInsurancePolicyCategory.AttivitàAgricola:
                    //    insurancePolicyBuilders.Add(new InsurancePolicyBuilder());
                    //    break;
                    //case EnumInsurancePolicyCategory.AllevamentoBestiame:
                    //    insurancePolicyBuilders.Add(new InsurancePolicyBuilder());
                    //    break;
                    case EnumInsurancePolicyCategory.FamiliareeCongiunto:
                        insurancePolicyBuilders.Add(new FamilyInsurancePolicyBuilder());
                        break;
                    case EnumInsurancePolicyCategory.AnimaleDomestico:
                        insurancePolicyBuilders.Add(new PetInsurancePolicyBuilder());
                        break;
                    case EnumInsurancePolicyCategory.Casa:
                        insurancePolicyBuilders.Add(new HouseInsurancePolicyBuilder());
                        break;
                    //case EnumInsurancePolicyCategory.Infortunio:
                    //    insurancePolicyBuilders.Add(new InsurancePolicyBuilder());
                    //    break;
                    //case EnumInsurancePolicyCategory.Malattia:
                    //    insurancePolicyBuilders.Add(new InsurancePolicyBuilder());
                    //    break;
                    //case EnumInsurancePolicyCategory.VisiteSpecialistiche:
                    //    insurancePolicyBuilders.Add(new SpecialisticExaminationsInsurancePolicyBuilder());
                    //    break;
                    //case EnumInsurancePolicyCategory.GrandiInterventi:
                    //    insurancePolicyBuilders.Add(new GreatInterventionsInsurancePolicyBuilder());
                    //    break;
                    //case EnumInsurancePolicyCategory.CureOdontoiatriche:
                    //    insurancePolicyBuilders.Add(new DentalCareInsurancePolicyBuilder());
                    //    break;
                    default:
                        break;                        
                }
            }

            return insurancePolicyBuilders;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
