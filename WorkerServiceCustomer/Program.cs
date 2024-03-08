using DAL.Core.Interfaces;
using DAL.Core;
using DAL.QueueService;
using DAL;
using WorkerServiceCustomer;
using Microsoft.EntityFrameworkCore;
using DAL.BuilderModel;
using DAL.BuilderModel.Interfaces;
using DAL.ModelFactory.Interfaces;
using DAL.ModelFactory;


var connectionStringDbContext = "Server=FGBAL031929\\MSSQLSERVER_1;Initial Catalog=datSampleDataBase;Persist Security Info=True;Integrated Security=SSPI;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
var connectionStringNoSql = "mongodb://localhost:27017/";//"mongodb +srv://speakingcodes:<password>@clustermongodb01.oclccsd.mongodb.net/?retryWrites=true&w=majority";
var databaseNoSql = "datSampleDataBaseNoSql";

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContextFactory<ApplicationDbContext>(options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionStringDbContext), ServiceLifetime.Singleton);
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ICustomerManager, CustomerManager>();

        services.AddSingleton<IMessageQueueProducer, MessageQueueProducer>();

        services.AddTransient<IAddressBuilder, AddressBuilder>();
        services.AddTransient<IDeliveryBuilder, DeliveryBuilder>();
        services.AddTransient<ICustomerBuilder, CustomerBuilder>();
        services.AddTransient<ITemplateFactory, TemplateFactory>();

        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
