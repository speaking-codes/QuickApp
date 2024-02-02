using DAL.ModelsNoSql;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryNoSql.Interfaces
{
    public interface ICustomerDetailRepository : IRepositoryNoSql<CustomerDetail>
    {
        bool ReplaceOne(FilterDefinition<CustomerDetail> filter, CustomerDetail document);
        CustomerDetail GetCustomer(string customerCode);
        bool DeleteCustomer(string customerCode);
  }
}
