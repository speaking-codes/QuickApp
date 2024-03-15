using DAL;
using DAL.Core;
using DAL.Core.Interfaces;
using DAL.MongoDB;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using DAL.RepositoryNoSql;
using DAL.RepositoryNoSql.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var host = new HostBuilder()
    .ConfigureHostConfiguration(config =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("local.settings.json");
        config.AddJsonFile("host.json");
        config.AddEnvironmentVariables();
    })
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        var config = Environment.GetEnvironmentVariable("DefaultConnection");
        var connectionStringDbContext = "Server=FGBAL031929\\MSSQLSERVER_1;Initial Catalog=datSampleDataBase;Persist Security Info=True;Integrated Security=SSPI;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";//"Server=tcp:dat-sql.database.windows.net,1433;Initial Catalog=datSampleDataBase;Persist Security Info=False;User ID=mauro.diliddo;Password=md.123456;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        var connectionStringNoSql = "mongodb://localhost:27017/";
        var databaseNoSql = "datSampleDataBaseNoSql";

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContextFactory<ApplicationDbContext>(options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionStringDbContext));
        services.AddSingleton((IMongoClient)new MongoClient(connectionStringNoSql));

        services.AddTransient<IMongoDbContext, MongoDbContext>(x => new MongoDbContext(connectionStringNoSql, databaseNoSql));
        services.AddTransient<ICustomerHeaderRepository, CustomerHeaderRepository>(x => new CustomerHeaderRepository(x.GetRequiredService<IMongoDbContext>(), "CustomerHeaderCollection"));
        services.AddTransient<ICustomerDetailRepository, CustomerDetailRepository>(x => new CustomerDetailRepository(x.GetRequiredService<IMongoDbContext>(), "CustomerDetailCollection"));
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ICustomerServerlessManager, CustomerServerlessManager>();
        services.AddTransient<IInsurancePolicyServerlessManager, InsurancePolicyServerlessManager>();
    })
    .Build();

host.Run();
