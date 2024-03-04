using DAL.BuilderModelTemplate;
using DAL.ModelFactory.Interfaces;
using DAL.Models;
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
            var _customerModel = new CustomerModelTemplate();
            _customerModel.LastNames = getLastName(_lastNamePath);
            _customerModel.FirstNameTemplates = getFirstName(_firstNameMalePath, _firstNameFemalePath);
            _customerModel.FamilyTypes = GetFamilyTypes();
            _customerModel.MaritalStatuses = GetMaritalStatusTypes();
            _customerModel.BirthMunicipalities = GetMunicipalities();
            _customerModel.ContractTypes = GetContractTypes();
            _customerModel.IncomeTypes = GetIncomeTypes();
            _customerModel.ProfessionTypes = GetProfessionTypes();
            _customerModel.DeliveryModelTemplate = getDeliveryModel();
            _customerModel.AddressTemplate = CreateAddressModelTemplate();

            return _customerModel;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
