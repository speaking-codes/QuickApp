// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using DAL.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

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
        ISalesLineRepository SalesLines { get; }

        IConfigurationModelRepository ConfigurationModels { get; }

        ILearningTrainingRepository LearningTrainings { get; }
        IMatrixCustomerInsurancePolicyRepository MatrixCustomerInsurancePolicies { get; }

        IBaggageTypeRepository BaggageTypes { get; }
        ITravelMeansTypeRepository TravelMeansTypes { get; }
        IKinshipRelationshipTypeRepository KinshipRelationshipTypes { get; }
        IStructureTypeRepository StructureTypes { get; }
        IBreedPetDetailTypeRepository BreedPetDetailTypes { get; }
        IIncomeTypeRepository IncomeTypes { get; }

        bool IsTransactionOpened { get; }

        int SaveChanges();
        Task SaveChangesAsync();

        void BeginTransaction();
        Task BeginTransactionAsync();

        void CommitTransaction();
        Task CommitTransactionAsync();

        void RollbackTransaction();
    }
}
