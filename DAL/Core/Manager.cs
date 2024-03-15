using DAL.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    public abstract class Manager : IManager
    {
        protected bool IsMassiveWriter { get; private set; }
        protected readonly IUnitOfWork UnitOfWork;
        protected int _countError;

        public Manager(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _countError = -1;
            IsMassiveWriter = false;
        }

        public void BeginTransaction()
        {
            UnitOfWork.BeginTransaction();
            IsMassiveWriter = true;
        }
        public async Task BeginTransactionAsync()
        {
            await UnitOfWork.BeginTransactionAsync();
            IsMassiveWriter = true;
        }
        public void CommitTransaction() => UnitOfWork.CommitTransaction();
        public async Task CommitTransactionAsync() => await UnitOfWork.CommitTransactionAsync();
        public void RollbackTransaction() => UnitOfWork.RollbackTransaction();

        public abstract void Dispose();
    }
}
