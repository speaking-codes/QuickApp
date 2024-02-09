using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IInsurancePolicyCategoryStaticRepository: IRepository<InsurancePolicyCategoryStatic>
    {
        IQueryable<InsurancePolicyCategoryStatic> GetInsurancePolicyCategoryStatics();
        IQueryable<InsurancePolicyCategoryStatic> GetInsurancePolicyCategoryStatics(int year);
    }
}
