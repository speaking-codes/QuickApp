using DAL.Models;
using DAL.ModelsNoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryNoSql.Interfaces
{
    public interface IInsuranceCategoryPolicyDashboardCardRepository : IRepositoryNoSql<InsuranceCategoryPolicyDashboardCard>
    {
        IList<InsuranceCategoryPolicyDashboardCard> GetAll();
    }
}
