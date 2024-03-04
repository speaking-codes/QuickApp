using ConsoleAppCaricamentoDati.BuilderManager;
using DAL;
using DAL.Core;
using DAL.Core.Helpers;
using DAL.Core.Interfaces;
using DAL.Models;
using DAL.QueueService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppCaricamentoDati
{
    internal class Program
    {
        static async Task Main(string[] args)
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
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<ICustomerRatingManager, CustomerRatingManager>();
            services.AddScoped<IInsurancePolicyManager, InsurancePolicyManager>();
            services.AddScoped<IMessageQueueProducer, MessageQueueProducer>();
            services.AddScoped<ILearningManager, LearningManager>();
            var provider = services.BuildServiceProvider();

            using (var customerBuilder = new CustomerBuilderManager(provider.GetRequiredService<IUnitOfWork>(), provider.GetRequiredService<ICustomerManager>(), 1))
                customerBuilder.Run();

            IList<InsurancePolicy> insurancePolicies = new List<InsurancePolicy>();

            using (var builder = new StateInsurancePolicyBuildManager(provider.GetRequiredService<IUnitOfWork>()))
            {
                var customers = builder.GetCustomers();

                foreach (var item in customers)
                {
                    var insurancePolicyBuilders = builder.GetInsurancePolicyBuilderManagers(item);
                    insurancePolicies.AddRange(builder.GetInsurancePolicies(insurancePolicyBuilders));
                }
            }
            
            using (var insurancePolicyManager = provider.GetRequiredService<IInsurancePolicyManager>())
            {
                insurancePolicyManager.BeginTransaction();
                foreach (var item in insurancePolicies)
                    insurancePolicyManager.AddInsurancePolicy(item);
            }

            //using (var manager = provider.GetRequiredService<ILearningManager>())
            //{
            //    manager.LoadMatrixUserItems();
            //}


            Console.WriteLine("Hello, World!");
        }
    }
}