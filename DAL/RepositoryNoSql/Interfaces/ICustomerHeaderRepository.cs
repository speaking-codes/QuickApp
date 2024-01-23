using DAL.ModelsNoSql;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryNoSql.Interfaces
{
    public interface ICustomerHeaderRepository : IRepositoryNoSql<CustomerHeader>
    {
        bool ReplaceOne(FilterDefinition<CustomerHeader> filter, CustomerHeader document);

        CustomerHeader GetCustomer(string customerCode);
    }
}
