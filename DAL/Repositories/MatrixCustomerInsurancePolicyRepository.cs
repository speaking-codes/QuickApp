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
    public class MatrixCustomerInsurancePolicyRepository : Repository<MatrixCustomerInsurancePolicy>, IMatrixCustomerInsurancePolicyRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public MatrixCustomerInsurancePolicyRepository(DbContext context) : base(context)
        {
        }
    }
}
