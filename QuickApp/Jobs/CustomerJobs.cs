using DAL.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace QuickApp.Jobs
{
    public class CustomerJobs : IJob
    {
        private readonly ILogger _logger;
        private readonly ICustomerManager _customerManager;

        public CustomerJobs(ILogger logger, ICustomerManager customerManager)
        {
            _logger = logger;
            _customerManager = customerManager;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
