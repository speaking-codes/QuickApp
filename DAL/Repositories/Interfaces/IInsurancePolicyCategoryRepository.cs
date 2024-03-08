using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IInsurancePolicyCategoryRepository : IRepository<InsurancePolicyCategory>
    {
        IQueryable<InsurancePolicyCategory> GetInsurancePolicyCategories();
        IQueryable<InsurancePolicyCategory> GetInsurancePolicyCategories(string customerCode);
        IQueryable<InsurancePolicyCategory> GetInsurancePolicyCategory(string insurancePolicyCategoryCode);
        IQueryable<InsurancePolicyCategory> GetInsurancePolicyCategory(int insurancePolicyCategoryId);
        IQueryable<InsurancePolicyCategory> GetInsurancePolicyCategoryStatistics(int year);
        IQueryable<InsurancePolicyCategory> GetSalesLineTypes(string customerCode);
    }
}
