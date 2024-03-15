using DAL;
using DAL.BuilderModel;
using DAL.BuilderModel.Interfaces;
using DAL.Core;
using DAL.Core.Interfaces;
using DAL.ModelFactory;
using DAL.ModelFactory.Interfaces;
using DAL.QueueService;
using Microsoft.EntityFrameworkCore;

namespace WorkerServiceInsurancePolicyIussue
{
    public class Program
    {
        private const string connectionStringDbContext = "Server=FGBAL031929\\MSSQLSERVER_1;Initial Catalog=datSampleDataBase;Persist Security Info=True;Integrated Security=SSPI;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        private const string connectionStringNoSql = "mongodb://localhost:27017/";//"mongodb +srv://speakingcodes:<password>@clustermongodb01.oclccsd.mongodb.net/?retryWrites=true&w=majority";
        private const string databaseNoSql = "datSampleDataBaseNoSql";

        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddDbContextFactory<ApplicationDbContext>(options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionStringDbContext), ServiceLifetime.Singleton);
                    services.AddSingleton<IUnitOfWork, UnitOfWork>();

                    services.AddTransient<ICustomerManager, CustomerManager>();
                    services.AddTransient<IInsurancePolicyManager, InsurancePolicyManager>();

                    services.AddTransient<IMessageQueueProducer, MessageQueueProducer>();

                    services.AddTransient<IAddressBuilder, AddressBuilder>();
                    services.AddTransient<IDeliveryBuilder, DeliveryBuilder>();
                    services.AddTransient<ICustomerBuilder, CustomerBuilder>();
                    services.AddTransient<ITemplateFactory, TemplateFactory>();

                    services.AddHostedService<Worker>();
                })
                .Build();

            host.Run();
        }
    }
}