using System;
using DAL.Core.Interfaces;
using DAL.ModelsRabbitMQ;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace QueueConsumer
{
    public class CustomerFunction
    {
        private readonly ILogger _logger;
        private readonly ICustomerServerlessManager _customerServerlessManager;

        public CustomerFunction(ILoggerFactory loggerFactory, ICustomerServerlessManager customerServerlessManager)
        {
            _logger = loggerFactory.CreateLogger<CustomerFunction>();
            _customerServerlessManager = customerServerlessManager;
        }

        [Function("CustomerFunction")]
        public void Run([RabbitMQTrigger("customers", ConnectionStringSetting = "localhost")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            var customerQueue = JsonConvert.DeserializeObject<CustomerQueue>(myQueueItem);
            _customerServerlessManager.Manage(customerQueue);
        }
    }
}
