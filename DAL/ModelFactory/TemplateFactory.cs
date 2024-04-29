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
        private const string _basePath = @"C:\Users\mauro.diliddo\source\repos\QuickApp\QuickAppGitHub\QuickApp\DataStorage\Input\";
        private const string _lastNamePath = $"{_basePath}Cognomi.txt";
        private const string _firstNameMalePath = $"{_basePath}NomiMaschili.txt";
        private const string _petNamePath = $"{_basePath}Pets.txt";
        private const string _firstNameFemalePath = $"{_basePath}NomiFemminili.txt";
        private const string _addressPath = $"{_basePath}Indirizzi.txt";

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
        private IList<IncomeClassType> GetIncomeClassTypes() => _unitOfWork.IncomeClassTypes.GetAll();
        private IList<ProfessionType> GetProfessionTypes() => _unitOfWork.ProfessionTypes.GetAll();
        private IList<InsurancePolicyCategory> GetInsurancePolicyCategories() => _unitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategories().ToList();
        private IList<ConfigurationModel> GetConfigurationModels() => _unitOfWork.ConfigurationModels.GetConfigurationModels().ToList();
        //private IList<GenderType> GetGenderTypes() => _unitOfWork.GenderTypes.GetAll();
        private IList<SportEventType> GetSportEventTypes() => _unitOfWork.SportEventTypes.GetAll();
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
            //customerModel.GenderTypes= GetGenderTypes();
            customerModel.FamilyTypes = GetFamilyTypes();
            customerModel.MaritalStatuses = GetMaritalStatusTypes();
            customerModel.BirthMunicipalities = GetMunicipalities();

            customerModel.ContractTypes = GetContractTypes();
            customerModel.IncomeTypes = GetIncomeTypes();
            customerModel.IncomeClassTypes = GetIncomeClassTypes();
            customerModel.ProfessionTypes = GetProfessionTypes();            

            customerModel.DeliveryModelTemplate = getDeliveryModel();
            customerModel.AddressTemplate = CreateAddressModelTemplate();

            return customerModel;
        }

        public InsurancePolicyTemplate CreateInsurancePolicyTemplate()
        {
            var insurancePolicyModel = new InsurancePolicyTemplate();
            insurancePolicyModel.LastNames = getLastName(_lastNamePath);
            insurancePolicyModel.FirstNameTemplates = getFirstName(_firstNameMalePath, _firstNameFemalePath);
            //insurancePolicyModel.GenderTypes= GetGenderTypes();
            insurancePolicyModel.PetNames = getPetNames(_petNamePath);
            insurancePolicyModel.InsurancePolicyCategories = GetInsurancePolicyCategories();
            insurancePolicyModel.ConfigurationModels = GetConfigurationModels();
            insurancePolicyModel.Municipalities = GetMunicipalities();
            insurancePolicyModel.IncomeTypes = GetIncomeTypes();
            insurancePolicyModel.IncomeClassType = GetIncomeClassTypes();
            insurancePolicyModel.SportEventTypes =GetSportEventTypes();
            insurancePolicyModel.ProfessionTypes = GetProfessionTypes();
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
                    case EnumInsurancePolicyCategory.RCA:
                        insurancePolicyBuilders.Add(new VehicleBuilder());
                        break;
                    case EnumInsurancePolicyCategory.ARD:
                        insurancePolicyBuilders.Add(new PetBuilder());
                        break;
                    case EnumInsurancePolicyCategory.RC_DIVERSI:
                        insurancePolicyBuilders.Add(new SportEventBuilder());
                        break;
                    case EnumInsurancePolicyCategory.MULTIGARANZIA_ABITAZIONE:
                        insurancePolicyBuilders.Add(new HouseBuilder());
                        break;
                    case EnumInsurancePolicyCategory.GLOBALE_FABBRICATI:
                        insurancePolicyBuilders.Add(new LargeBuildingBuilder());
                        break;
                    case EnumInsurancePolicyCategory.INFORTUNI:
                        insurancePolicyBuilders.Add(new InjuryBuilder());
                        break;
                    case EnumInsurancePolicyCategory.MALATTIA:
                        insurancePolicyBuilders.Add(new IllnessBuilder());
                        break;
                    case EnumInsurancePolicyCategory.INCENDIO_FURTO:
                        //insurancePolicyBuilders.Add(new InsurancePolicyBuilder());
                        break;
                    case EnumInsurancePolicyCategory.TUTELA_GIUDIZIARIA:
                        insurancePolicyBuilders.Add(new LegalProtectionBuilder());
                        break;
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
