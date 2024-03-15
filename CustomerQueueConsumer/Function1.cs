using System;
using DAL;
using DAL.Core.Interfaces;
using DAL.ModelsRabbitMQ;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CustomerQueueConsumer
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly ICustomerServerlessManager _customerServerlessManager;

        public Function1(ILoggerFactory loggerFactory, ICustomerServerlessManager customerServerlessManager)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _customerServerlessManager = customerServerlessManager;
        }

        [Function("Function1")]
        public void Run([RabbitMQTrigger("customers", ConnectionStringSetting = "localhost")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            var customerQueue = JsonConvert.DeserializeObject<CustomerQueue>(myQueueItem);
            _customerServerlessManager.Manage(customerQueue);
        }
    }
}
