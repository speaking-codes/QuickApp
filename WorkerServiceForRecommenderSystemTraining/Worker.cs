using DAL.Core.Interfaces;

namespace WorkerServiceForRecommenderSystemTraining
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ILearningManager _learningManager;
        private readonly IStorageManager _storageManager;

        public Worker(ILogger<Worker> logger, ILearningManager learningManager, IStorageManager storageManager)
        {
            _logger = logger;
            _learningManager = learningManager;
            _storageManager = storageManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var userId = await _storageManager.GetLastUserIdFromMatrixUserItems();

                var customerLearningFeature = await _storageManager.LoadCustomerLearningFeature(userId);
                var matrixUserItems = await _learningManager.LoadMatrixUsersItems(customerLearningFeature);
                //var temp = matrixUserItems.Where(x => x.Rating == 0).Select(x => x.ItemId).Distinct().ToList();
                //var temp2 = matrixUserItems.Where(x => x.Rating != 0).Select(x => x.ItemId).Distinct().ToList();
                await _storageManager.SaveMatrixUsersItems(matrixUserItems);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}