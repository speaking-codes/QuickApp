﻿// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private

        private readonly ApplicationDbContext _context;

        private ICustomerRepository _customers;

        private IRepositoryMunicipality _municipalities;
        private IFamilyTypeRepository _familyTypes;
        private IMaritalStatusTypeRepository _maritalStatusTypes;

        private IContractTypeRepository _contractTypes;
        private IProfessionTypeRepository _professionTypes;

        private IInsurancePolicyRepository _insurancePolicies;
        private IInsurancePolicyCategoryRepository _insurancePolicyCategories;
        private IInsurancePolicyCategoryStaticRepository _insurancePolicyCategoryStatics;

        private bool _disposedValue;
        private IDbContextTransaction _transaction;

        #endregion

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Properties 

        public ICustomerRepository Customers
        {
            get
            {
                _customers ??= new CustomerRepository(_context);

                return _customers;
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

        public IInsurancePolicyCategoryStaticRepository InsurancePolicyCategoryStatics
        {
            get
            {
                _insurancePolicyCategoryStatics ??= new InsurancePolicyCategoryStaticRepository(_context);
                return _insurancePolicyCategoryStatics;
            }
        }

        public bool IsTransactionOpened { get { return _transaction != null; } }

        #endregion

        #region Methods

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void BeginTransaction() => _transaction = _context.Database.BeginTransaction();

        public void CommitTransaction() => _transaction.Commit();

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
