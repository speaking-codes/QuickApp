using DAL.Core.Interfaces;
using DAL.Mapping;
using DAL.ModelsRabbitMQ;
using DAL.Repositories.Interfaces;
using DAL.RepositoryNoSql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    public class CustomerServerlessManager : ICustomerServerlessManager
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerHeaderRepository _customerHeaderRepositoryNoSql;
        private readonly ICustomerDetailRepository _customerDetailRepositoryNoSql;

        public CustomerServerlessManager(ICustomerRepository customerRepository,
                                         ICustomerHeaderRepository customerRepositoryNoSql,
                                         ICustomerDetailRepository customerDetailRepositoryNoSql)
        {
            _customerRepository = customerRepository;
            _customerHeaderRepositoryNoSql = customerRepositoryNoSql;
            _customerDetailRepositoryNoSql = customerDetailRepositoryNoSql;
        }

        public void ManageCustomer(CustomerQueue customerQueue)
        {
            CustomerNoSqlManager customerNoSqlManager = null;
            switch (customerQueue.PublishQueueType)
            {
                case Enums.EnumPublishQueueType.Created:
                    customerNoSqlManager = new CustomerNoSqlManagerAdded(_customerRepository, _customerHeaderRepositoryNoSql, _customerDetailRepositoryNoSql, customerQueue);
                    break;
                case Enums.EnumPublishQueueType.Updated:
                    customerNoSqlManager = new CustomerNoSqlManagerUpdated(_customerRepository, _customerHeaderRepositoryNoSql, _customerDetailRepositoryNoSql, customerQueue);
                    break;
                case Enums.EnumPublishQueueType.Deleted:
                    customerNoSqlManager = new CustomerNoSqlManagerDeleted(_customerRepository, _customerHeaderRepositoryNoSql, _customerDetailRepositoryNoSql, customerQueue);
                    break;
                default:
                    customerNoSqlManager = new CustomerNoSqlManagerNoObject(_customerRepository, _customerHeaderRepositoryNoSql, _customerDetailRepositoryNoSql, customerQueue);
                    break;
            }
            customerNoSqlManager.Execute();
        }
    }
}
