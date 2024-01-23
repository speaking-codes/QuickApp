using DAL.Core.Interfaces;
using DAL.ModelsNoSql;
using DAL.RepositoryNoSql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    public class DashboardManager : IDashboardManager
    {
        private readonly ICustomerHeaderRepository _customerHeaderRepository;
        private readonly ICustomerDetailRepository _customerDetailRepository;

        public DashboardManager(ICustomerHeaderRepository customerHeaderRepository, ICustomerDetailRepository customerDetailRepository)
        {
            _customerHeaderRepository = customerHeaderRepository;
            _customerDetailRepository = customerDetailRepository;
        }

        public CustomerHeader GetCustomerHeader(string customerCode) => _customerHeaderRepository.GetCustomer(customerCode);

        public CustomerDetail GetCustomerDetail(string customerCode) => _customerDetailRepository.GetCustomer(customerCode);
    }
}
