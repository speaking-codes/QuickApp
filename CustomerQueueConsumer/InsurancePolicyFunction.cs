using System;
using DAL.Core.Interfaces;
using DAL.ModelsRabbitMQ;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CustomerQueueConsumer
{
    public class InsurancePolicyFunction
    {
        private readonly ILogger _logger;
        private readonly ICustomerServerlessManager _customerServerlessManager;

        public InsurancePolicyFunction(ILoggerFactory loggerFactory, ICustomerServerlessManager customerServerlessManager)
        {
            _logger = loggerFactory.CreateLogger<InsurancePolicyFunction>();
            _customerServerlessManager= customerServerlessManager;
        }

        [Function("InsurancePolicyFunction")]
        public void Run([RabbitMQTrigger("insurancepolicies", ConnectionStringSetting = "localhost")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            var customerInsurancePolicyQueue = JsonConvert.DeserializeObject<CustomerInsurancePolicyQueue>(myQueueItem);
            _customerServerlessManager.ManageInsurancePolicy(customerInsurancePolicyQueue);
        }
    }
}
