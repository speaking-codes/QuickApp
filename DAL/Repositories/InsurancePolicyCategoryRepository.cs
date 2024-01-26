using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class InsurancePolicyCategoryRepository:Repository<InsurancePolicyCategoryRepository>, IInsurancePolicyCategoryRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public InsurancePolicyCategoryRepository(ApplicationDbContext context):base(context) { }
    }
}
