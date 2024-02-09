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
    public class InsurancePolicyCategoryStaticRepository: Repository<InsurancePolicyCategoryStatic>, IInsurancePolicyCategoryStaticRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public InsurancePolicyCategoryStaticRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<InsurancePolicyCategoryStatic> GetInsurancePolicyCategoryStatics() =>
            _appContext.InsurancePolicyCategoryStatics;

        public IQueryable<InsurancePolicyCategoryStatic> GetInsurancePolicyCategoryStatics(int year) =>
            _appContext.InsurancePolicyCategoryStatics
                       .Include(x => x.InsurancePolicyCategory)
                       .ThenInclude(y => y.SalesLine)
                       .Where(x => x.Year == year);
    }
}
