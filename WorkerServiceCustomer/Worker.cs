using DAL.BuilderModelTemplate;
using DAL.ModelFactory.Interfaces;

namespace WorkerServiceCustomer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITemplateFactory _customerTemplateFactory;

        private CustomerModelTemplate _customerModel;

        public Worker(ILogger<Worker> logger, ITemplateFactory customerTemplateFactory)
        {
            _logger = logger;
            _customerTemplateFactory = customerTemplateFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _customerModel ??= _customerTemplateFactory.CreateCustomerModelTemplate();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}