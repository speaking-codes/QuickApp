using DAL.BuilderModelTemplate;
using DAL.Core.Interfaces;
using DAL.ModelFactory.Interfaces;
using DAL.Models;

namespace WorkerServiceInsurancePolicyIussue
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITemplateFactory _templateFactory;
        private readonly ICustomerManager _customerManager;
        private readonly IInsurancePolicyManager _insurancePolicyManager;
        
        private Random _random;
        private IList<Customer> _customers;
        private InsurancePolicyTemplate _insurancePolicyTemplate;

        public Worker(ILogger<Worker> logger, ITemplateFactory templateFactory, ICustomerManager customerManager,IInsurancePolicyManager insurancePolicyManager)
        {
            _logger = logger;
            _templateFactory = templateFactory; 
            _customerManager = customerManager;
            _insurancePolicyManager= insurancePolicyManager;

            _random = new Random();            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _insurancePolicyTemplate ??= _templateFactory.CreateInsurancePolicyTemplate();

                if (_customers == null || _customers.Count == 0)
                {
                    _customers = _customerManager.GetCustomersWithoutInsurancePolicies();
                    _random = new Random();
                }

                var i = _random.Next(_customers.Count);
                var count = _random.Next(1, _insurancePolicyTemplate.InsurancePolicyCategories.Count);
                var insurancePolicyBuilders = _templateFactory.CreateInsurancePolicyBuilders(count, _insurancePolicyTemplate.InsurancePolicyCategories, _random);
                IList<InsurancePolicy> insurancePolicies= new List<InsurancePolicy>();
                foreach(var builder in insurancePolicyBuilders)
                {
                    InsurancePolicy insurancePolicy = null;
                    insurancePolicies.Add(builder.SetInsurancePolicy(insurancePolicy)
                                                 .SetInsurancePolicyCategory(_insurancePolicyTemplate.InsurancePolicyCategories)
                                                 .SetCustomer(_customers[i])
                                                 .SetDetailItem(_insurancePolicyTemplate)
                                                 .SetIssueDate()
                                                 .SetExpiryDate()
                                                 .SetInsuredMaximum()
                                                 .SetTotalPrize()
                                                 .SetLuxuryPolicy()
                                                 .Build());
                }
                foreach(var item in insurancePolicies)
                {
                    _insurancePolicyManager.BeginTransaction();
                    _insurancePolicyManager.AddInsurancePolicy(item);
                }

                _customers.RemoveAt(i);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}