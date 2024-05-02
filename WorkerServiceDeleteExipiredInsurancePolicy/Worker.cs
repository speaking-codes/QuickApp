using DAL.Core.Interfaces;
using DAL.QueueModels;

namespace WorkerServiceDeleteExipiredInsurancePolicy
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICustomerManager _customerManager;
        private readonly IInsurancePolicyManager _insurancePolicyManager;
        private int _monthCount;
        public Worker(ILogger<Worker> logger, ICustomerManager customerManager, IInsurancePolicyManager insurancePolicyManager)
        {
            _logger = logger;
            _customerManager = customerManager;
            _insurancePolicyManager = insurancePolicyManager;
            _monthCount = 0;
        }

        private async Task DeleteInsurancePolicies()
        {
            _monthCount++;
            _monthCount = (_monthCount > 6) ? 0 : _monthCount;

            try
            {
                var insurancePolicies = _insurancePolicyManager.GetExpiredInsurancePolicy(DateTime.Now.AddMonths(_monthCount));
                if (insurancePolicies.Count == 0)
                    return;

                await _insurancePolicyManager.BeginTransactionAsync();

                foreach (var policy in insurancePolicies)
                    _insurancePolicyManager.DeleteInsurancePolicy(policy.InsurancePolicyCode);

                await _insurancePolicyManager.CommitTransactionAsync();

                _insurancePolicyManager.EnqueueDeletedInsurancePolicies(insurancePolicies.Select(x =>
                                        new CustomerInsurancePolicy
                                        {
                                            CustomerCode = x.Customer.CustomerCode,
                                            InsurancePolicyCode = x.InsurancePolicyCode
                                        }));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message: {ex.Message}");
                _logger.LogError($"Error StackTrace: {ex.StackTrace}");
                _logger.LogError($"Error Source: {ex.Source}");

                _insurancePolicyManager.RollbackTransaction();
            }
        }

        private async Task DeleteCustomers()
        {
            try
            {
                var customers = _customerManager.GetActiveCustomersWithoutInsurancePolicies();
                if (customers.Count == 0)
                    return;

                await _customerManager.BeginTransactionAsync();

                foreach (var customer in customers)
                    _customerManager.DeleteCustomer(customer.CustomerCode);

                await _customerManager.CommitTransactionAsync();

                _customerManager.EnqueueDeletedCustomers(customers.Select(x => x.CustomerCode));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message: {ex.Message}");
                _logger.LogError($"Error StackTrace: {ex.StackTrace}");
                _logger.LogError($"Error Source: {ex.Source}");

                _customerManager.RollbackTransaction();
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await DeleteInsurancePolicies();
                await DeleteCustomers();

                await Task.Delay(3600000, stoppingToken);
            }
        }
    }
}