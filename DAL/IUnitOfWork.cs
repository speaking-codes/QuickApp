// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }

        IProvinceRepository Provinces { get; }
        IRepositoryMunicipality Municipalities { get; }
        IFamilyTypeRepository FamilyTypes { get; }
        IMaritalStatusTypeRepository MaritalStatusTypes { get; }

        IIncomeClassTypeRepository IncomeClassTypes { get; }
        IContractTypeRepository ContractTypes { get; }
        IProfessionTypeRepository ProfessionTypes { get; }

        IInsurancePolicyRepository InsurancePolicies { get; }
        IInsurancePolicyCategoryRepository InsurancePolicyCategories { get; }
        IWarrantyRepository Warranties { get; }
        IInsurancePolicyCategoryStaticRepository InsurancePolicyCategoryStatics { get; }
        ISalesLineRepository SalesLines { get; }

        IConfigurationModelRepository ConfigurationModels { get; }

        ILearningTrainingRepository CustomerLearningFeatures { get; }
        IMatrixCustomerInsurancePolicyRepository MatrixUsersItems { get; }

        IKinshipRelationshipTypeRepository KinshipRelationshipTypes { get; }
        IBreedPetDetailTypeRepository BreedPetDetailTypes { get; }
        IIncomeTypeRepository IncomeTypes { get; }
        ISportEventTypeRepository SportEventTypes { get; }
        IGenderTypeRepository GenderTypes { get; }
        IBusinessTypeRepository BusinessTypes { get; }

        IVehicleRepository Vehicles { get;  }
        IPetRepository Pets { get;  }
        ISportEventRepository SportEvents { get;  }
        IHouseRepository Houses { get;  }
        ILegalProtectionRepository LegalProtections { get;  }
        ILargeBuildingRepository LargeBuildings { get;  }
        IInjuryRepository Injuries { get;  }
        IIllnessRepository Illnesses { get;  }
        IBusinessRepository Businesses { get; }

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
