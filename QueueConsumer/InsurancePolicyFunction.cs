using System;
using DAL.Core.Interfaces;
using DAL.ModelsRabbitMQ;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace QueueConsumer
{
    public class InsurancePolicyFunction
    {
        private readonly ILogger _logger;
        private readonly IInsurancePolicyServerlessManager _insurancePolicyServerlessManager;

        public InsurancePolicyFunction(ILoggerFactory loggerFactory, IInsurancePolicyServerlessManager insurancePolicyServerlessManager)
        {
            _logger = loggerFactory.CreateLogger<InsurancePolicyFunction>();
            _insurancePolicyServerlessManager = insurancePolicyServerlessManager;
        }

        [Function("InsurancePolicyFunction")]
        public void Run([RabbitMQTrigger("insurancepolicies", ConnectionStringSetting = "localhost")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            var customerInsurancePolicyQueue = JsonConvert.DeserializeObject<CustomerInsurancePolicyQueue>(myQueueItem);
            _insurancePolicyServerlessManager.Manage(customerInsurancePolicyQueue);
        }
    }
}
