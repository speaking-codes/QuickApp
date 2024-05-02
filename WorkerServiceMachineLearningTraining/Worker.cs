using Amazon.Util.Internal;
using DAL;
using DAL.Core.Interfaces;
using DAL.Models;
using System.Linq;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkerServiceMachineLearningTraining
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILearningManager _learningManager;
        private readonly IStorageManager _storageManager;

        private IList<string> getCopertureString(IList<string> coperture)
        {
            var copertureItem = new List<List<string>>();
            var innerList = new List<string>();

            if (coperture.Count == 2)
            {
                innerList.Add(coperture[0]);
                innerList.Add(coperture[1]);
                copertureItem.Add(innerList);
            }
            else if (coperture.Count == 3)
            {
                innerList.Add(coperture[0]);
                innerList.Add(coperture[1]);
                copertureItem.Add(innerList);

                innerList = new List<string>();
                innerList.Add(coperture[0]);
                innerList.Add(coperture[2]);
                copertureItem.Add(innerList);

                innerList = new List<string>();
                innerList.Add(coperture[1]);
                innerList.Add(coperture[2]);
                copertureItem.Add(innerList);
            }
            else if (coperture.Count == 4)
            {
                innerList.Add(coperture[0]);
                innerList.Add(coperture[1]);
                copertureItem.Add(innerList);

                innerList = new List<string>();
                innerList.Add(coperture[0]);
                innerList.Add(coperture[2]);
                copertureItem.Add(innerList);

                innerList = new List<string>();
                innerList.Add(coperture[0]);
                innerList.Add(coperture[3]);
                copertureItem.Add(innerList);

                innerList = new List<string>();
                innerList.Add(coperture[1]);
                innerList.Add(coperture[2]);
                copertureItem.Add(innerList);

                innerList = new List<string>();
                innerList.Add(coperture[1]);
                innerList.Add(coperture[3]);
                copertureItem.Add(innerList);

                innerList = new List<string>();
                innerList.Add(coperture[3]);
                innerList.Add(coperture[3]);
                copertureItem.Add(innerList);
            }

            var copertureItemReverse = new List<List<string>>();
            for (var i = 0; i < copertureItem.Count; i++)
            {
                copertureItem[i].Reverse();
                copertureItemReverse.Add(copertureItem[i]);
                copertureItem[i].Reverse();
            }

            var copertureItemEquals = new List<List<string>>();
            foreach (var item in coperture)
            {
                innerList = new List<string>();
                innerList.Add(item);
                innerList.Add(item);
                copertureItemEquals.Add(innerList);
            }

            var copertureTotal = new List<List<string>>();
            copertureTotal.AddRange(copertureItem);
            copertureTotal.AddRange(copertureItemReverse);
            copertureTotal.AddRange(copertureItemEquals);

            var copertureOutput = new List<string>();

            foreach (var item in copertureTotal)
                copertureOutput.Add(string.Join(";", item));

            return copertureOutput;
        }

        public Worker(ILogger<Worker> logger, IUnitOfWork unitOfWork, ILearningManager learningManager, IStorageManager storageManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _learningManager = learningManager;
            _storageManager = storageManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    var pathDirectory = @"C:\Users\mauro.diliddo\source\repos\QuickApp\QuickAppGitHub\QuickApp\DataStorage\Training\";
                    var pathInput = $"{pathDirectory}Input\\";
                    var pathProcessed = $"{pathDirectory}Processed\\";
                    var customerLearningFeatures = _storageManager.LoadDataFromStorage(pathInput, pathProcessed);
                    if (customerLearningFeatures.Count > 0)
                        await _storageManager.SaveDataFeaturs(customerLearningFeatures);

                    customerLearningFeatures = _storageManager.LoadDataFeatures();
                    await _storageManager.UpdateDataFeatures(customerLearningFeatures);
                    //_learningManager.TrainingClassifier();
                    Console.WriteLine("Training Complete");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error Message: {ex.Message}");
                    _logger.LogError($"Error StackTrace: {ex.StackTrace}");
                    _logger.LogError($"Error Source: {ex.Source}");
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}