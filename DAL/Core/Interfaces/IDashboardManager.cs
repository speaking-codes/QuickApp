using DAL.ModelsNoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.Interfaces
{
    public interface IDashboardManager
    {
        CustomerHeader GetCustomerHeader(string customerCode);
        CustomerDetail GetCustomerDetail(string customerCode);
    }
}
