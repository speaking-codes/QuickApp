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

        public Worker(ILogger<Worker> logger, IUnitOfWork unitOfWork, ILearningManager learningManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _learningManager = learningManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    var pathDirectory = @"C:\Users\mauro.diliddo\source\repos\QuickApp\QuickAppGitHub\QuickApp\ExportPolizzeMauroDiLiddo\";
                    var pathFileCustomer = $"{pathDirectory}ExportPolizzeMauroDiLiddo.csv";

                    //var customerLearningFeatures = _learningManager.LoadDataFromStorage(pathFileCustomer);
                    //await _learningManager.SaveDataFeaturs(customerLearningFeatures);
                    //var  customerLearningFeatures = _learningManager.LoadDataFeatures();
                    //  await _learningManager.UpdateDataFeatures(customerLearningFeatures);
                    //_learningManager.CreateClassifierModel();
                    //_learningManager.PredictionClassification();
                    //var matrixUsersItems = _learningManager.LoadMatrixUsersItems();
                    //await _learningManager.SaveDataMatrixUsersItems(matrixUsersItems);
                    //_learningManager.TrainingRecommendation();
                    //var matrixUsersItems = _learningManager.GetMatrixUsersItems();
                    //await _learningManager.SaveDataMatrixUsersItems(matrixUsersItems);
                    //}
                    var customerCodes = new List<string>() {
                        "MR-YFESGJ0SG1I-1",
                        "SC-YFESGJ0SG1I-2",
                        "MB-YFESGJ0SG1I-3",
                        "MM-YFESGJ0SG1I-4",
                        "EC-YFESGJ0SG1I-5",
                        "FB-YFESGJ0SG1I-6",
                        "VR-YFESGJ0SG1I-7",
                        "EG-YFESGJ0SG1I-8",
                        "EM-YFESGJ0SG1I-9",
                        "MB-YFESGJ0SG1-10",
                        "MM-YFESGJ0SG1-11",
                        "AP-YFESGJ0SG1-12",
                        "MF-YFESGJ0SG1-13",
                        "CP-YFESGJ0SG1-14"
                    };
                    foreach (var item in customerCodes)
                        _learningManager.GetRecommendation(item, 0.85f, 4);
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