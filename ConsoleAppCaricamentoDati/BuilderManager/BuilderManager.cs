using DAL.Core.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati.BuilderManager
{
    public abstract class BuilderManager: IBuilderManager
    {
        protected IUnitOfWork UnitOfWork { get; private set; }
        protected ICustomerManager CustomerManager { get; private set; }

        public BuilderManager(IUnitOfWork unitOfWork, ICustomerManager customerManager)
        {
            UnitOfWork = unitOfWork;
            customerManager = customerManager;
        }

        public void Dispose()
        {
            CustomerManager.Dispose();
            UnitOfWork.Dispose();
        }
        
        public abstract Task Run();
    }
}
