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
        protected readonly IUnitOfWork UnitOfWork;
        protected bool IsMassiveWriter { get; private set; }

        public Manager(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            IsMassiveWriter = false;
        }

        public void BeginTransaction()
        {
            UnitOfWork.BeginTransaction();
            IsMassiveWriter = true;
        }

        public abstract void Dispose();        
    }
}
