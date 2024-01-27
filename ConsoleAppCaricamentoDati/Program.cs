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
            var customerList = new List<Customer>();

            for (var i = 0; i < 4; i++)
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
                for(var i=0;i<customerList.Count;i++)
                    manger.AddCustomer(customerList[i]);
            }
            Console.WriteLine("Hello, World!");
        }
    }
}