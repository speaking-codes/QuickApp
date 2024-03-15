using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.Interfaces
{
    public interface IManager : IDisposable
    {
        void BeginTransaction();
        Task BeginTransactionAsync();

        void CommitTransaction();
        Task CommitTransactionAsync();

        void RollbackTransaction();
    }
}
