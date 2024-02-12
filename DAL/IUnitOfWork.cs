// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using DAL.Repositories.Interfaces;
using System;

namespace DAL
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }

        IRepositoryMunicipality Municipalities { get; }
        IFamilyTypeRepository FamilyTypes { get; }
        IMaritalStatusTypeRepository MaritalStatusTypes { get; }

        IContractTypeRepository ContractTypes { get; }
        IProfessionTypeRepository ProfessionTypes { get; }

        IInsurancePolicyRepository InsurancePolicies { get; }
        IInsurancePolicyCategoryRepository InsurancePolicyCategories { get; }
        IInsurancePolicyCategoryStaticRepository InsurancePolicyCategoryStatics { get; }

        IConfigurationModelRepository ConfigurationModels { get; }

        bool IsTransactionOpened { get; }

        int SaveChanges();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
