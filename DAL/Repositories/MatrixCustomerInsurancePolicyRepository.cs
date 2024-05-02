using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MatrixCustomerInsurancePolicyRepository : Repository<MatrixUsersItems>, IMatrixCustomerInsurancePolicyRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public MatrixCustomerInsurancePolicyRepository(DbContext context) : base(context)
        {
        }

        public async Task<bool> HasItems() => await _appContext.MatrixUsersItems.AnyAsync();

        public async Task<long> GetLastUserId() => await _appContext.MatrixUsersItems.MaxAsync(x => x.UserId);
    }
}
