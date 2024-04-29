using DAL.BuilderModel.Interfaces;
using DAL.BuilderModelTemplate;
using DAL.Core.Interfaces;
using DAL.ModelFactory.Interfaces;
using DAL.Models;

namespace WorkerServiceCustomer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITemplateFactory _customerTemplateFactory;
        private readonly ICustomerBuilder _customerBuilder;
        private readonly ICustomerManager _customerManager;

        private CustomerModelTemplate _customerTemplate;

        public Worker(ILogger<Worker> logger, ITemplateFactory customerTemplateFactory, ICustomerBuilder customerBuilder, ICustomerManager customerManager)
        {
            _logger = logger;
            _customerTemplateFactory = customerTemplateFactory;
            _customerBuilder = customerBuilder;
            _customerManager = customerManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    _customerTemplate ??= _customerTemplateFactory.CreateCustomerModelTemplate();
                    Customer customer = null;
                    customer = _customerBuilder.SetCustomer(customer)
                                              .SetLastName(_customerTemplate.LastNames)
                                              .SetFirstNameAndGender(_customerTemplate.FirstNameTemplates)
                                              .SetBirthDate()
                                              .SetBirthMunicipality(_customerTemplate.BirthMunicipalities)

                                               .SetFamilyType(_customerTemplate.FamilyTypes)
                                               .SetMaritalStatus(_customerTemplate.MaritalStatuses)
                                               .SetChildrenNumber()

                                               .SetContractType(_customerTemplate.ContractTypes)
                                               .SetIncomeType(_customerTemplate.IncomeTypes, _customerTemplate.ContractTypes)
                                               .SetProfessionType(_customerTemplate.ProfessionTypes, _customerTemplate.ContractTypes)
                                               .SetIncome()

                                               .SetDeliveries(_customerTemplate.DeliveryModelTemplate)
                                               .SetAddresses(_customerTemplate.AddressTemplate)

                                               .Build();
                    _customerManager.AddCustomer(customer);
        
                    await Task.Delay(5000, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error Message: {ex.Message}");
                    _logger.LogError($"Error StackTrace: {ex.StackTrace}");
                    _logger.LogError($"Error Source: {ex.Source}");
                }
            }
        }
    }
}