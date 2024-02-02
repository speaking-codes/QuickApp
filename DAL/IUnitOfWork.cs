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
        IMaritalStatusTypeRepository MaritalStatusTypes { get; }
        IJobRalRatingCoefficientRepository JobRalRatingCoefficients { get; }
        IInsurancePolicyRepository InsurancePolicies { get; }
        IInsurancePolicyCategoryRepository InsurancePolicyCategories { get; }
        ICustomerInsuranceCategoryPolicyRatingRepository CustomerInsuranceCategoryPolicyRatings { get; }
        IContractTypeRepository ContractTypes { get; }
        IAgeRatingCoefficientRepository AgeRatingCoefficients { get; }

        bool IsTransactionOpened { get; }

        int SaveChanges();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
