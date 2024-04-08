using DAL;
using DAL.Core.Interfaces;
using DAL.Mapping;
using MachineLearningModel;
using Microsoft.ML;

namespace WorkerServiceMachineLearningTraining
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILearningManager _learningManager;

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
                    var learningCustomerPreferences = _unitOfWork.LearningTrainings.GetAll();
                    var classificationModelInputs = learningCustomerPreferences.ToClassificationModelInputCollection();
                    var mlContext = new MLContext(seed: 0);
                    var dataInput = mlContext.Data.LoadFromEnumerable<ClassificationLearningModel.ModelInput>(classificationModelInputs);
                    DataOperationsCatalog.TrainTestData dataSplit = mlContext.Data.TrainTestSplit(dataInput, testFraction: 0.2);
                    IDataView trainData = dataSplit.TrainSet;
                    IDataView testData = dataSplit.TestSet;
                    var trainPipeline = ClassificationLearningModel.RetrainPipeline(mlContext, trainData);
                    mlContext.Model.Save(trainPipeline, dataInput.Schema, "ClassificationLearningModel.zip");

                    await _unitOfWork.BeginTransactionAsync();

                    foreach (var item in learningCustomerPreferences)
                    {
                        var modelInput = item.ToClassificationModelInput();
                        var modelOutput = ClassificationLearningModel.Predict(modelInput);
                        item.PredictionInsurancePolicyCategory = modelOutput.PredictedLabel;
                    }

                    _unitOfWork.LearningTrainings.UpdateRange(learningCustomerPreferences);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitTransactionAsync();

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