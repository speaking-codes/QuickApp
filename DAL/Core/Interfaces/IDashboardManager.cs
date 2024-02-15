using DAL.ModelsNoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.Interfaces
{
    public interface IDashboardManager : IManager
    {
        CustomerHeader GetCustomerHeader(string customerCode);
        CustomerDetail GetCustomerDetail(string customerCode);
        IList<SalesLineChart> GetSalesLineChart(string customerCode);
        IList<InsuranceCoverageGrid> GetInsuranceCoverageGridSummaries(string customerCode);
        IList<InsuranceCategoryPolicyDashboardCard> GetTopSellingInsuranceCategoryPolicyDashboardCards(int year, int top, IEnumerable<string> insuranceCategoryPolicyCodes);
        IList<InsuranceCategoryPolicyDashboardCard> GetOtherInsuranceCategoryPolicyDashboardCards(IEnumerable<string> insuranceCategoryPolicyCodes);
    }
}
