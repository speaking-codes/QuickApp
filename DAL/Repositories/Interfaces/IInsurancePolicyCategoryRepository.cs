using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IInsurancePolicyCategoryRepository : IRepository<InsurancePolicyCategoryRepository>
    {
        IQueryable<InsurancePolicyCategory> GetInsurancePolicyCategories();
        IQueryable<InsurancePolicyCategory> GetInsurancePolicyCategory(string insurancePolicyCategoryCode);
        IQueryable<int> GetInsurancePolicyCategoryCount(string insurancePolicyCategoryCode);
    }
}
