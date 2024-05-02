using DAL;
using DAL.Core;
using DAL.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WorkerServiceForRecommenderSystemTraining
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connectionStringDbContext = "Server=FGBAL031929\\MSSQLSERVER_1;Initial Catalog=datSampleDataBase;Persist Security Info=True;Integrated Security=SSPI;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddDbContextFactory<ApplicationDbContext>(options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionStringDbContext), ServiceLifetime.Singleton);
                    services.AddTransient<IUnitOfWork, UnitOfWork>();
                    services.AddTransient<ILearningManager, LearningManager>();
                    services.AddTransient<IStorageManager, StorageManager>();

                    services.AddHostedService<Worker>();
                })
                .Build();

            host.Run();
        }
    }
}