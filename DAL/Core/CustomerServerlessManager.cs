using DAL.ModelsRabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    public class CustomerServerlessManager: ICustomerServerlessManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerServerlessManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void ManageCustomer(CustomerQueue customerQueue)
        {
            var customer = _unitOfWork.Customers.GetCustomer(customerQueue.CustomerCode).FirstOrDefault();
        }
    }
}
