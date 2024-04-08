using DAL;
using DAL.Core;
using DAL.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using WorkerServiceMachineLearningTraining;

var connectionStringDbContext = "Server=FGBAL031929\\MSSQLSERVER_1;Initial Catalog=datSampleDataBase;Persist Security Info=True;Integrated Security=SSPI;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
var connectionStringNoSql = "mongodb://localhost:27017/";//"mongodb +srv://speakingcodes:<password>@clustermongodb01.oclccsd.mongodb.net/?retryWrites=true&w=majority";
var databaseNoSql = "datSampleDataBaseNoSql";

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContextFactory<ApplicationDbContext>(options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionStringDbContext), ServiceLifetime.Singleton);
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ILearningManager, LearningManager>();

        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
