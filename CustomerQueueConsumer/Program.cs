using DAL;
using DAL.Core;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var host = new HostBuilder()
    .ConfigureHostConfiguration(config => {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("local.settings.json");
        config.AddJsonFile("host.json");
        config.AddEnvironmentVariables();
    })
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        var config = Environment.GetEnvironmentVariable("DefaultConnection");
        var conn = "Server=tcp:dat-sql.database.windows.net,1433;Initial Catalog=datSampleDataBase;Persist Security Info=False;User ID=mauro.diliddo;Password=md.123456;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<ApplicationDbContext>(options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, conn));

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICustomerServerlessManager, CustomerServerlessManager>();
    })         
    .Build();

host.Run();
