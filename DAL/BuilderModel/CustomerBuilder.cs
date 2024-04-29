using DAL.BuilderModel.Interfaces;
using DAL.BuilderModelTemplate;
using DAL.Enums;
using DAL.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel
{
    public class CustomerBuilder : ICustomerBuilder
    {
        private readonly IDeliveryBuilder _deliveryBuilder;
        private readonly IAddressBuilder _addressBuilder;

        private int _age;
        private Random _random;
        private Customer _customer;

        public CustomerBuilder(IDeliveryBuilder deliveryBuilder, IAddressBuilder addressBuilder)
        {
            _deliveryBuilder = deliveryBuilder;
            _addressBuilder = addressBuilder;

            _random = new Random();
        }

        public ICustomerBuilder SetCustomer(Customer customer)
        {
            customer ??= new Customer();
            _customer = customer;
            return this;
        }

        public ICustomerBuilder SetLastName(IList<string> lastNames)
        {
            var i = _random.Next(lastNames.Count);
            _customer.LastName = lastNames[i];
            return this;
        }

        public ICustomerBuilder SetFirstNameAndGender(IList<FirstNameTemplate> firstNameTemplates)
        {
            var i = _random.Next(firstNameTemplates.Count);
            _customer.FirstName = firstNameTemplates[i].FirstName;
            _customer.Gender = firstNameTemplates[i].IsMale ? Enums.EnumGender.Uomo : Enums.EnumGender.Donna;

            return this;
        }

        public ICustomerBuilder SetBirthDate()
        {
            _age = -_random.Next(18, 90);
            var days = -_random.Next(1, 365);
            _customer.BirthDate = DateTime.Now.AddYears(_age).AddDays(days);
            return this;
        }

        public ICustomerBuilder SetBirthMunicipality(IList<Municipality> municipalities)
        {
            var i = _random.Next(municipalities.Count);
            _customer.BirthMunicipalityId = municipalities[i].Id;

            return this;
        }

        public ICustomerBuilder SetFamilyType(IList<FamilyType> familyTypes)
        {
            var i = _random.Next(familyTypes.Count);
            _customer.FamilyTypeId = familyTypes[i].Id;
            return this;
        }

        public ICustomerBuilder SetMaritalStatus(IList<MaritalStatusType> maritalStatusTypes)
        {
            IList<MaritalStatusType> tempMaritalStatusTypes;
            var familyType = (EnumFamilyType)_customer.FamilyTypeId;
            var maritalStatusSingleCollection = new List<byte>
            {
                (byte)EnumMaritalStatus.Libero,
                (byte)EnumMaritalStatus.Celibe,
                (byte)EnumMaritalStatus.Vedovo,
                (byte)EnumMaritalStatus.Divorziato,
            };
            var maritalStatusCoupleCollection = new List<byte>
            {
                (byte)EnumMaritalStatus.Coniugato,
                (byte)EnumMaritalStatus.Vedovo,
                (byte)EnumMaritalStatus.Separato,
                (byte)EnumMaritalStatus.Convivente
            };
            switch (familyType)
            {
                case EnumFamilyType.Single_Senza_Figli:
                case EnumFamilyType.Single_Con_Figli:
                    if (_customer.Gender == EnumGender.Uomo)
                        tempMaritalStatusTypes = maritalStatusTypes.Where(x => maritalStatusSingleCollection.Any(y => y == x.Id))
                                                                   .ToList();
                    else
                        tempMaritalStatusTypes = maritalStatusTypes.Where(x => x.Id == (byte)EnumMaritalStatus.Nubile)
                                                                   .ToList();
                    break;
                case EnumFamilyType.Coppia_Senza_Figli:
                case EnumFamilyType.Coppia_Con_1_Figlio:
                case EnumFamilyType.Coppia_Con_2_Figli:
                case EnumFamilyType.Coppia_Con_3_Figli:
                    tempMaritalStatusTypes = maritalStatusTypes.Where(x => maritalStatusCoupleCollection.Any(y => y == x.Id))
                                                               .ToList();
                    break;
                case EnumFamilyType.Nucleo_Con_Familiari_A_Carico:
                default:
                    if (_customer.Gender == EnumGender.Donna)
                        tempMaritalStatusTypes = maritalStatusTypes.Where(x => x.Id == (byte)EnumMaritalStatus.Nubile)
                                                                   .ToList();
                    else
                        tempMaritalStatusTypes = maritalStatusTypes.Where(x => x.Id != (byte)EnumMaritalStatus.Nubile)
                                                                   .ToList();
                    break;
            }

            var i = _random.Next(tempMaritalStatusTypes.Count);
            _customer.MaritalStatusId = tempMaritalStatusTypes[i].Id;
            return this;
        }

        public ICustomerBuilder SetChildrenNumber()
        {
            var familyType = (EnumFamilyType)_customer.FamilyTypeId;

            switch (familyType)
            {
                case EnumFamilyType.Single_Con_Figli:
                    _customer.ChildrenNumber = (byte)_random.Next(5);
                    break;
                    _customer.ChildrenNumber = 0;
                case EnumFamilyType.Coppia_Con_1_Figlio:
                    _customer.ChildrenNumber = 1;
                    break;
                case EnumFamilyType.Coppia_Con_2_Figli:
                    _customer.ChildrenNumber = 2;
                    break;
                case EnumFamilyType.Coppia_Con_3_Figli:
                    _customer.ChildrenNumber = (byte)_random.Next(5);
                    break;
                case EnumFamilyType.Nucleo_Con_Familiari_A_Carico:
                    _customer.ChildrenNumber = (byte)_random.Next(7);
                    break;
                case EnumFamilyType.Single_Senza_Figli:
                case EnumFamilyType.Coppia_Senza_Figli:
                default:
                    _customer.ChildrenNumber = 0;
                    break;
            }

            return this;
        }

        public ICustomerBuilder SetContractType(IList<ContractType> contractTypes)
        {
            var i = _random.Next(contractTypes.Count);
            _customer.ContractTypeId = contractTypes[i].Id;
            return this;
        }

        public ICustomerBuilder SetIncomeType(IList<IncomeType> incomeTypes, IList<ContractType> contractTypes)
        {
            var contractType = contractTypes.Where(x => x.Id == _customer.ContractTypeId).FirstOrDefault();
            if (contractType == null)
                return this;

            if (contractType.IsSubordinateEmployment)
                _customer.IncomeTypeId = incomeTypes.Where(x => x.Id == (byte)EnumIncomeType.Dipendente).FirstOrDefault()?.Id;
            else
                _customer.IncomeTypeId = incomeTypes.Where(x => x.Id == (byte)EnumIncomeType.Professionista).FirstOrDefault()?.Id;

            return this;
        }

        public ICustomerBuilder SetProfessionType(IList<ProfessionType> professionTypes, IList<ContractType> contractTypes)
        {
            var contractType = contractTypes.Where(x => x.Id == _customer.ContractTypeId).FirstOrDefault();
            if (contractType == null)
                return this;

            IList<ProfessionType> tempProfessionTypes = null;
            if (contractType.IsSubordinateEmployment)
                tempProfessionTypes = professionTypes.Where(x => x.IsFreelancer.HasValue && !x.IsFreelancer.Value).ToList();
            else
                tempProfessionTypes = professionTypes.Where(x => !x.IsFreelancer.HasValue || x.IsFreelancer.Value).ToList();

            var i = _random.Next(tempProfessionTypes.Count);
            _customer.ProfessionTypeId = tempProfessionTypes[i].Id;

            return this;
        }

        public ICustomerBuilder SetIncome()
        {
            double annualIncome;
            if ((_random.Next() % 2) == 0)
                annualIncome = _customer.ProfessionType.MinAnnualGrossIncome * (1 + _random.NextDouble());
            else
                annualIncome = _customer.ProfessionType.MaxAnnualGrossIncome * _random.NextDouble();

            if (annualIncome < _customer.ProfessionType.MinAnnualGrossIncome)
                annualIncome = _customer.ProfessionType.MinAnnualGrossIncome;

            _customer.Income = annualIncome;

            return this;
        }

        public ICustomerBuilder SetDeliveries(DeliveryModelTemplate deliveryTemplate)
        {
            _customer.Deliveries = new List<Delivery>();
            Delivery delivery = null;
            var isPrimary = _random.Next() % 2 == 0;
            _customer.Deliveries.Add(_deliveryBuilder.SetDelivery(delivery)
                                                     .SetIsPrimary(isPrimary)
                                                     .SetDeliveryType(EnumDeliveryType.Privato)
                                                     .SetEmail(deliveryTemplate.ProviderMails, _customer.LastName, _customer.FirstName)
                                                     .SetPhoneNumber()
                                                     .Build());

            delivery = null;
            _customer.Deliveries.Add(_deliveryBuilder.SetDelivery(delivery)
                                                     .SetIsPrimary(!isPrimary)
                                                     .SetDeliveryType(EnumDeliveryType.Lavorativo)
                                                     .SetEmail(deliveryTemplate.ProviderMails, _customer.LastName, _customer.FirstName)
                                                     .SetPhoneNumber()
                                                     .Build());
            return this;
        }

        public ICustomerBuilder SetAddresses(AdressModelTemplate adressTemplate)
        {
            _customer.Addresses = new List<Address>();
            var valueModuleThree = _random.Next() % 3;
            Address address = null;
            _customer.Addresses.Add(_addressBuilder.SetAddress(address)
                                                   .SetIsPrimary(valueModuleThree == 0)
                                                   .SetAddressType(EnumAddressType.Residenza)
                                                   .SetLocation(adressTemplate.StreetNames)
                                                   .SetMunicipality(adressTemplate.Municipalities)
                                                   .Build());

            address = null;
            _customer.Addresses.Add(_addressBuilder.SetAddress(address)
                                                   .SetIsPrimary(valueModuleThree == 1)
                                                   .SetAddressType(EnumAddressType.Lavoro)
                                                   .SetLocation(adressTemplate.StreetNames)
                                                   .SetMunicipality(adressTemplate.Municipalities)
                                                   .Build());

            address = null;
            _customer.Addresses.Add(_addressBuilder.SetAddress(address)
                                                   .SetIsPrimary(valueModuleThree == 2)
                                                   .SetAddressType(EnumAddressType.Domicilio)
                                                   .SetLocation(adressTemplate.StreetNames)
                                                   .SetMunicipality(adressTemplate.Municipalities)
                                                   .Build());

            return this;
        }

        public Customer Build() => _customer;

        public void Dispose()
        {
            _customer = null;
        }
    }
}
