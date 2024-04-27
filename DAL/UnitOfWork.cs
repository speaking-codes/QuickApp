// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private

        protected readonly ApplicationDbContext _context;

        private ICustomerRepository _customers;

        private IProvinceRepository _provinces;
        private IRepositoryMunicipality _municipalities;
        private IFamilyTypeRepository _familyTypes;
        private IMaritalStatusTypeRepository _maritalStatusTypes;

        private IIncomeClassTypeRepository _incomeClassTypes;
        private IContractTypeRepository _contractTypes;
        private IProfessionTypeRepository _professionTypes;

        private IInsurancePolicyRepository _insurancePolicies;
        private IInsurancePolicyCategoryRepository _insurancePolicyCategories;
        private IWarrantyRepository _warranties;
        private IInsurancePolicyCategoryStaticRepository _insurancePolicyCategoryStatics;
        private ISalesLineRepository _salesLines;

        private IConfigurationModelRepository _configurationModels;

        private ILearningTrainingRepository _customerLearningFeatures;
        private IMatrixCustomerInsurancePolicyRepository _matrixUsersItems;
        private IKinshipRelationshipTypeRepository _kinshipRelationshipTypes;

        private IBreedPetDetailTypeRepository _breedPetDetailTypes;
        private IIncomeTypeRepository _incomeTypes;
        private ISportEventTypeRepository _sportEventTypes;
        private IGenderTypeRepository _genderTypes;

        private IVehicleRepository _vehicles;
        private IPetRepository _pets;
        private ISportEventRepository _sportEvents;
        private IHouseRepository _houses;
        private ILegalProtectionRepository _legalProtections;
        private ILargeBuildingRepository _largeBuildings;
        private IInjuryRepository _injuries;
        private IIllnessRepository _illnesses;


        private bool _disposedValue;
        private IDbContextTransaction _transaction;

        #endregion

        #region Ctor

        //public UnitOfWork(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        public UnitOfWork(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _context = contextFactory.CreateDbContext();
        }

        #endregion

        #region Properties 

        public ICustomerRepository Customers
        {
            get
            {
                _customers ??= new CustomerRepository(_context);

                return _customers;
            }
        }

        public IProvinceRepository Provinces
        {
            get
            {
                _provinces ??= new ProvinceRepository(_context);
                return _provinces;
            }
        }

        public IRepositoryMunicipality Municipalities
        {
            get
            {
                _municipalities ??= new RepositoryMunicipality(_context);
                return _municipalities;
            }
        }

        public IFamilyTypeRepository FamilyTypes
        {
            get
            {
                _familyTypes ??= new FamilyTypeRepository(_context);
                return _familyTypes;
            }
        }

        public IMaritalStatusTypeRepository MaritalStatusTypes
        {
            get
            {
                _maritalStatusTypes ??= new MaritalStatusTypeRepository(_context);
                return _maritalStatusTypes;
            }
        }

        public IIncomeClassTypeRepository IncomeClassTypes
        {
            get
            {
                _incomeClassTypes ??= new IncomeClassTypeRepository(_context);
                return _incomeClassTypes;
            }
        }

        public IContractTypeRepository ContractTypes
        {
            get
            {
                _contractTypes ??= new ContractTypeRepository(_context);
                return _contractTypes;
            }
        }

        public IProfessionTypeRepository ProfessionTypes
        {
            get
            {
                _professionTypes ??= new ProfessionTypeRepository(_context);
                return _professionTypes;
            }
        }

        public IInsurancePolicyRepository InsurancePolicies
        {
            get
            {
                _insurancePolicies ??= new InsurancePolicyRepository(_context);
                return _insurancePolicies;
            }
        }

        public IInsurancePolicyCategoryRepository InsurancePolicyCategories
        {
            get
            {
                _insurancePolicyCategories ??= new InsurancePolicyCategoryRepository(_context);
                return _insurancePolicyCategories;
            }
        }

        public IWarrantyRepository Warranties
        {
            get
            {
                _warranties ??= new WarrantyRepository(_context);
                return _warranties;
            }
        }

        public IInsurancePolicyCategoryStaticRepository InsurancePolicyCategoryStatics
        {
            get
            {
                _insurancePolicyCategoryStatics ??= new InsurancePolicyCategoryStaticRepository(_context);
                return _insurancePolicyCategoryStatics;
            }
        }

        public ISalesLineRepository SalesLines
        {
            get
            {
                _salesLines ??= new SalesLineRepository(_context);
                return _salesLines;
            }
        }

        public IConfigurationModelRepository ConfigurationModels
        {
            get
            {
                _configurationModels ??= new ConfigurationModelRepository(_context);
                return _configurationModels;
            }
        }

        public ILearningTrainingRepository CustomerLearningFeatures
        {
            get
            {
                _customerLearningFeatures ??= new LearningTrainingRepository(_context);
                return _customerLearningFeatures;
            }
        }

        public IMatrixCustomerInsurancePolicyRepository MatrixUsersItems
        {
            get
            {
                _matrixUsersItems ??= new MatrixCustomerInsurancePolicyRepository(_context);
                return _matrixUsersItems;
            }
        }

        public IKinshipRelationshipTypeRepository KinshipRelationshipTypes
        {
            get
            {
                _kinshipRelationshipTypes ??= new KinshipRelationshipTypeRepository(_context);
                return _kinshipRelationshipTypes;
            }
        }

        public IBreedPetDetailTypeRepository BreedPetDetailTypes
        {
            get
            {
                _breedPetDetailTypes ??= new BreedPetDetailTypeRepository(_context);
                return _breedPetDetailTypes;
            }
        }

        public IIncomeTypeRepository IncomeTypes
        {
            get
            {
                _incomeTypes ??= new IncomeTypeRepository(_context);
                return _incomeTypes;
            }
        }

        public ISportEventTypeRepository SportEventTypes
        {
            get
            {
                _sportEventTypes ??= new SportEventTypeRepository(_context);
                return _sportEventTypes;
            }
        }

        public IGenderTypeRepository GenderTypes
        {
            get
            {
                _genderTypes ??= new GenderTypeRepository(_context);
                return _genderTypes;
            }
        }

        public IVehicleRepository Vehicles
        {
            get
            {
                _vehicles ??= new VehicleRepository(_context);
                return _vehicles;
            }
        }

        public IPetRepository Pets
        {
            get
            {
                _pets ??= new PetRepository(_context);
                return _pets;
            }
        }

        public ISportEventRepository SportEvents
        {
            get
            {
                _sportEvents ??= new SportEventRepository(_context);
                return _sportEvents;
            }
        }

        public IHouseRepository Houses
        {
            get
            {
                _houses ??= new HouseRepository(_context);
                return _houses;
            }
        }

        public ILegalProtectionRepository LegalProtections
        {
            get
            {
                _legalProtections ??= new LegalProtectionRepository(_context);
                return _legalProtections;
            }
        }

        public ILargeBuildingRepository LargeBuildings
        {
            get
            {
                _largeBuildings ??= new LargeBuildingRepository(_context);
                return _largeBuildings;
            }
        }

        public IInjuryRepository Injuries
        {
            get
            {
                _injuries ??= new InjuryRepository(_context);
                return _injuries;
            }
        }

        public IIllnessRepository Illnesses
        {
            get
            {
                _illnesses ??= new IllnessRepository(_context);
                return _illnesses;
            }
        }

        public bool IsTransactionOpened { get { return _transaction != null; } }

        #endregion

        #region Methods

        public int SaveChanges() => _context.SaveChanges();
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public void BeginTransaction() => _transaction = _context.Database.BeginTransaction();
        public async Task BeginTransactionAsync() => _transaction = await _context.Database.BeginTransactionAsync();

        public void CommitTransaction() => _transaction.Commit();
        public async Task CommitTransactionAsync() { if (_transaction != null) await _transaction.CommitAsync(); }

        public void RollbackTransaction() => _transaction.Rollback();

        #endregion

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
                _context.Dispose();
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
