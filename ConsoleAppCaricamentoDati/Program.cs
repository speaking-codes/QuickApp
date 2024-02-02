using ConsoleAppCaricamentoDati.Models;
using DAL;
using DAL.Core.Interfaces;
using DAL.Core;
using DAL.MongoDB;
using DAL.QueueService;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DAL.Models;

namespace ConsoleAppCaricamentoDati
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var basePath = @"C:\Users\mauro.diliddo\source\repos\QuickApp\QuickAppGitHub\QuickApp\ConsoleAppCaricamentoDati\DatiBase\";
            var lastNamePath = $"{basePath}Cognomi.txt";
            var firstNameMalePath = $"{basePath}NomiMaschili.txt";
            var firstNameFemalePath = $"{basePath}NomiFemminili.txt";
            var addressPath = $"{basePath}Indirizzi.txt";
            var jobPath = $"{basePath}Professioni.txt";

            var connectionStringDbContext = "Server=FGBAL031929\\MSSQLSERVER_1;Initial Catalog=datSampleDataBase;Persist Security Info=True;Integrated Security=SSPI;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
            var connectionStringNoSql = "mongodb://localhost:27017/";//"mongodb +srv://speakingcodes:<password>@clustermongodb01.oclccsd.mongodb.net/?retryWrites=true&w=majority";
            var databaseNoSql = "datSampleDataBaseNoSql";

            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionStringDbContext));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepositoryMunicipality, RepositoryMunicipality>();
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<ICustomerRatingManager, CustomerRatingManager>();
            services.AddScoped<IInsurancePolicyManager, InsurancePolicyManager>();
            services.AddScoped<IMessageQueueProducer, MessageQueueProducer>();
            var provider = services.BuildServiceProvider();

            var customerTemplateBuilder = new CustomerBaseTemplateBuilder();
            var customerTemplate = customerTemplateBuilder.SetJobContratType()
                                                          .SetLastName(lastNamePath)
                                                          .SetFirstName(firstNameMalePath, firstNameFemalePath)
                                                          .SetAddress(addressPath)
                                                          .SetProviderMail()
                                                          .SetJob(jobPath)
                                                          .Build();
            var customBuilder = new CustomerBuilder(provider, customerTemplate);
            var insurancePolicyBuilder = new InsurancePolicyBuilder(provider);

            IList<Customer> customerList = new List<Customer>();
            var insurancePolicyList = new List<InsurancePolicy>();

            var random = new Random();

            for (var i = 0; i < 2; i++)
            {
                customerList.Add(customBuilder.SetCustomer()
                                            .SetFirstName()
                                            .SetLastName()
                                            .SetMaritalStatus()
                                            .SetChildrenNumber()
                                            .SetBirthDate()
                                            .SetBirthPlace()
                                            .SetAddress()
                                            .SetDelivery()
                                            .SetJob()
                                            .Build());
            }
            using (var manger = provider.GetService<ICustomerManager>())
            {
                manger.BeginTransaction();
                for (var i = 0; i < customerList.Count; i++)
                    manger.AddCustomer(customerList[i]);
            }
            return;
            using (var manger = provider.GetService<ICustomerManager>())
            {
                customerList.Clear();
                customerList = manger.GetActiveCustomers();
            }

            var maxCount = (int)Math.Round(customerList.Count * 1.5);

            for (var i = 0; i < maxCount; i++)
            {
                var j = random.Next(0, customerList.Count);
                if (j < customerList.Count)
                {
                    insurancePolicyList.Add(insurancePolicyBuilder.SetInsurancePolicy()
                                                                  .SetIssueDate()
                                                                  .SetExpireDate()
                                                                  .SetInsurancePolicyCategory()
                                                                  .SetCustomer(customerList[j])
                                                                  .SetInsuredMaximum()
                                                                  .SetTotalPrize()
                                                                  .SetIsLuxuryPolicy()
                                                                  .Build());
                }
            }
            using (var manager = provider.GetService<IInsurancePolicyManager>())
            {
                manager.BeginTransaction();
                for (var i = 0; i < insurancePolicyList.Count; i++)
                    manager.AddInsurancePolicy(insurancePolicyList[i]);
            }

            Console.WriteLine("Hello, World!");
        }
    }
}