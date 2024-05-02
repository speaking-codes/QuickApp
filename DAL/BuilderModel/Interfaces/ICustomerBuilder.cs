using DAL.BuilderModelTemplate;
using DAL.Enums;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel.Interfaces
{
    public interface ICustomerBuilder : IDisposable
    {
        ICustomerBuilder SetCustomer(Customer customer);
        ICustomerBuilder SetLastName(IList<string> lastNames);
        ICustomerBuilder SetFirstNameAndGender(IList<FirstNameTemplate> firstNameTemplates);
        ICustomerBuilder SetBirthDate();
        ICustomerBuilder SetBirthMunicipality(IList<Municipality> municipalities);

        ICustomerBuilder SetFamilyType(IList<FamilyType> familyTypes);
        ICustomerBuilder SetMaritalStatus(IList<MaritalStatusType> maritalStatusTypes);
        ICustomerBuilder SetChildrenNumber();
        
        ICustomerBuilder SetContractType(IList<ContractType> contractTypes);
        ICustomerBuilder SetIncomeType(IList<IncomeType> incomeTypes, IList<ContractType> contractTypes);
        ICustomerBuilder SetProfessionType(IList<ProfessionType> professionTypes, IList<ContractType> contractTypes);
        ICustomerBuilder SetIncome(IList<ProfessionType> professionTypes);

        ICustomerBuilder SetDeliveries(DeliveryModelTemplate deliveryTemplate);
        ICustomerBuilder SetAddresses(AdressModelTemplate adressTemplate);

        Customer Build();
    }
}
