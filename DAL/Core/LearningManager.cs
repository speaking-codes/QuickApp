using DAL.Core.Interfaces;
using DAL.Enums;
using DAL.Models;
using MachineLearningModel;
using Microsoft.EntityFrameworkCore;
using QuickApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Core
{
    public class LearningManager : Manager, ILearningManager
    {
        #region Private 

        private CustomerLearningFeature getCustomerLearningFeature(string customerCode)
        {
            var customer = UnitOfWork.Customers.GetCustomerForLearningFeature(customerCode).FirstOrDefault();
            var customerLearningFeature = new CustomerLearningFeature
            {
                Gender = customer.Gender.GetCode(),
                BirthMonth = customer.BirthDate.Month.ToString(),
                YearBirth = customer.BirthDate.Year.ToString(),
                MaritalStatus = customer.MaritalStatus.MaritalStatusDescription,
                IsSingle = customer.MaritalStatus.IsSingle,
                ChildrenNumbers = customer.ChildrenNumber ?? 0,
                ProfessionType = customer.ProfessionType.ProfessionTypeDescription,
                IsFreelancer = customer.ProfessionType.IsFreelancer ?? false,
                Country = customer.Addresses.Where(x => x.IsPrimary).FirstOrDefault()?.Municipality.Province.ProvinceAbbreviation,
                Region = customer.Addresses.Where(x => x.IsPrimary).FirstOrDefault()?.Municipality.Province.Region.RegionName,
            };
            return customerLearningFeature;
        }

        private long getCustomerId(CustomerLearningFeature customerLearningFeature)
        {
            var index = 0;
            var rnd = new Random();

            var customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeature(customerLearningFeature).OrderByDescending(x => x.CustomerId).ToList();
            if (customerLearningFeatures.Count > 0)
            {
                index = rnd.Next(0, customerLearningFeatures.Count);
                return customerLearningFeatures[index].CustomerId ?? long.MinValue;
            }
            customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeatureWithoutOne(customerLearningFeature).OrderByDescending(x => x.CustomerId).ToList();
            if (customerLearningFeatures.Count > 0)
            {
                index = rnd.Next(0, customerLearningFeatures.Count);
                return customerLearningFeatures[index].CustomerId ?? long.MinValue;
            }
            customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeatureWithoutTwo(customerLearningFeature).OrderByDescending(x => x.CustomerId).ToList();
            if (customerLearningFeatures.Count > 0)
            {
                index = rnd.Next(0, customerLearningFeatures.Count);
                return customerLearningFeatures[index].CustomerId ?? long.MinValue;
            }
            customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeatureWithoutThree(customerLearningFeature).OrderByDescending(x => x.CustomerId).ToList();
            if (customerLearningFeatures.Count > 0)
            {
                index = rnd.Next(0, customerLearningFeatures.Count);
                return customerLearningFeatures[index].CustomerId ?? long.MinValue;
            }
            customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeatureWithoutFour(customerLearningFeature).OrderByDescending(x => x.CustomerId).ToList();
            if (customerLearningFeatures.Count > 0)
            {
                index = rnd.Next(0, customerLearningFeatures.Count);
                return customerLearningFeatures[index].CustomerId ?? long.MinValue;
            }
            customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeatureWithoutFive(customerLearningFeature).OrderByDescending(x => x.CustomerId).ToList();
            if (customerLearningFeatures.Count > 0)
            {
                index = rnd.Next(0, customerLearningFeatures.Count);
                return customerLearningFeatures[index].CustomerId ?? long.MinValue;
            }
            customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeatureWithoutSix(customerLearningFeature).OrderByDescending(x => x.CustomerId).ToList();
            if (customerLearningFeatures.Count > 0)
            {
                index = rnd.Next(0, customerLearningFeatures.Count);
                return customerLearningFeatures[index].CustomerId ?? long.MinValue;
            }
            customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeatureWithoutSeven(customerLearningFeature).OrderByDescending(x => x.CustomerId).ToList();
            if (customerLearningFeatures.Count > 0)
            {
                index = rnd.Next(0, customerLearningFeatures.Count);
                return customerLearningFeatures[index].CustomerId ?? long.MinValue;
            }
            customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeatureWithoutEight(customerLearningFeature).OrderByDescending(x => x.CustomerId).ToList();
            if (customerLearningFeatures.Count > 0)
            {
                index = rnd.Next(0, customerLearningFeatures.Count);
                return customerLearningFeatures[index].CustomerId ?? long.MinValue;
            }
            customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeatureWithoutNine(customerLearningFeature).OrderByDescending(x => x.CustomerId).ToList();
            if (customerLearningFeatures.Count > 0)
            {
                index = rnd.Next(0, customerLearningFeatures.Count);
                return customerLearningFeatures[index].CustomerId ?? long.MinValue;
            }
            customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeatureWithoutTen(customerLearningFeature).OrderByDescending(x => x.CustomerId).ToList();
            index = rnd.Next(0, customerLearningFeatures.Count);
            return customerLearningFeatures[index].CustomerId ?? long.MinValue;
        }

        private IList<MLRecommenderSystem.ModelOutput> getItemRecommendations(long customerId, float minScore, int maxItems)
        {
            var insurancePolicyCategories = UnitOfWork.InsurancePolicyCategories.GetAll();

            var descendingComparer = Comparer<float>.Create((x, y) => y.CompareTo(x));
            var predictions = new SortedList<float, MLRecommenderSystem.ModelOutput>(descendingComparer);

            foreach (var item in insurancePolicyCategories)
            {
                var modelInput = new MLRecommenderSystem.ModelInput { UserId = customerId, ItemId = item.Id };
                var modelOutput = MLRecommenderSystem.Predict(modelInput);
                predictions.Add(modelOutput.Score, modelOutput);
            }

            return predictions.Where(x => x.Key >= minScore)
                              .Select(x => x.Value)
                              .Take(maxItems)
                              .ToList();
        }

        private IList<InsurancePolicyCategory> getRecommendedInsurancePolicyCategories(IEnumerable<MLRecommenderSystem.ModelOutput> predictions)
        {
            var insurancePolicyCategories = new List<InsurancePolicyCategory>();

            foreach (var item in predictions)
            {
                var insurancePolicyCategory = UnitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategory((byte)item.ItemId).Single();
                insurancePolicyCategories.Add(insurancePolicyCategory);
            }

            return insurancePolicyCategories;
        }

        private readonly Random _random;

        #endregion

        public LearningManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _random = new Random();
        }

        public void TrainingClassifier()
        {
            //var mlContext = new MLContext();
            //var customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeaturesForTraining().Take(1200).ToList();
            //var dataView = mlContext.Data.LoadFromEnumerable(customerLearningFeatures);
            //var transformer = MLClassifierCustomerFeature.RetrainPipeline(mlContext, dataView);
            //mlContext.Model.Save(transformer, dataView.Schema, @"C:\Users\mauro.diliddo\source\repos\QuickApp\QuickAppGitHub\QuickApp\QueueConsumer\mlClassifierCustomerFeature.zip");
        }

        public void PredictionClassification()
        {
            //DataViewSchema columns;
            //var mlContext = getMLContext();
            //var modelPath = @"C:\Users\mauro.diliddo\source\repos\QuickApp\DataStorage\Output\model.zip";
            //var mlModel = mlContext.Model.Load(modelPath, out columns);
            //var predictionEngine = mlContext.Model.CreatePredictionEngine<CustomerLearningFeatureDataView, InsurancePrediction>(mlModel);

            //var dataTest = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeaturesForTraining().Skip(1000).Take(10).ToList().ToDataViewModels();
            //foreach (var item in dataTest)
            //{
            //    var prediction = predictionEngine.Predict(item);
            //    var labelBuffer = new VBuffer<ReadOnlyMemory<char>>();
            //    var annotations = predictionEngine.OutputSchema["PredictedLabel"].Annotations;//.GetValue("KeyValues", ref labelBuffer);

            //    //var labels = labelBuffer.DenseValues().Select(l => l.ToString()).ToArray();
            //}
        }

        public async Task<IList<MatrixUsersItems>> LoadMatrixUsersItems(IEnumerable<CustomerLearningFeature> customerLearningFeatures, double biasMultiplier)
        {
            var insurancePolicyCategories = await UnitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategories().ToListAsync();
            var matrixUsersItems = new List<MatrixUsersItems>();

            foreach (var item in customerLearningFeatures)
            {
                var customerLearningFeatureInsurancePolicies = UnitOfWork.CustomerLearningFeatures
                                                                         .GetCustomerLearningFeatures(item.CustomerId)
                                                                         .Select(x => new CustomerLearningFeature
                                                                         {
                                                                             CustomerId = x.CustomerId,
                                                                             InsurancePolicyId = x.InsurancePolicyId,
                                                                         }).ToList();
                foreach (var subItem in insurancePolicyCategories)
                {
                    MatrixUsersItems userItem;
                    if (customerLearningFeatureInsurancePolicies.Any(x => x.InsurancePolicyId == subItem.Id))
                    {
                        userItem = new MatrixUsersItems
                        {
                            UserId = item.CustomerId ?? 0,
                            ItemId = subItem.Id,
                            Rating = 100 + (biasMultiplier * _random.NextDouble())
                        };
                        matrixUsersItems.Add(userItem);
                        continue;
                    }

                    var modelInput = new MLClassifierCustomerFeature.ModelInput()
                    {
                        Gender = item.Gender,
                        BirthMonth = item.BirthMonth,
                        YearBirth = item.YearBirth,
                        MaritalStatus = item.MaritalStatus,
                        ChildrenNumbers = item.ChildrenNumbers,
                        ProfessionType = item.ProfessionType,
                        Country = item.Country,
                        Region = item.Region,
                        InsurancePolicyCode = subItem.InsurancePolicyCategoryCode,
                    };

                    var prediction = MLClassifierCustomerFeature.Predict(modelInput);

                    if (subItem.Id == (byte)prediction.PredictedLabel)
                        userItem = new MatrixUsersItems
                        {
                            UserId = item.CustomerId ?? 0,
                            ItemId = (byte)prediction.PredictedLabel,
                            Rating = (prediction.Score.Max() * 100) + (biasMultiplier * _random.NextDouble())
                        };
                    else
                        userItem = new MatrixUsersItems
                        {
                            UserId = item.CustomerId ?? 0,
                            ItemId = subItem.Id,
                            Rating = (biasMultiplier * _random.NextDouble())
                        };

                    matrixUsersItems.Add(userItem);
                }

                if (matrixUsersItems.Count > 60000)
                    break;
            }
            return matrixUsersItems;
        }
        public async Task<IList<MatrixUsersItems>> LoadMatrixUsersItems(IEnumerable<CustomerLearningFeatureCopy> customerLearningFeatures)
        {
            var insurancePolicyCategories = await UnitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategories().ToListAsync();
            var matrixUsersItems = new List<MatrixUsersItems>();

            foreach (var item in customerLearningFeatures)
            {
                foreach (var subItem in insurancePolicyCategories)
                {
                    //var modelInput = new MLClassifierCustomerFeature.ModelInput()
                    //{
                    //    Gender = item.Gender,
                    //    BirthMonth = item.BirthMonth,
                    //    YearBirth = item.YearBirth,
                    //    MaritalStatus = item.MaritalStatus,
                    //    ChildrenNumbers = item.ChildrenNumbers,
                    //    ProfessionType = item.ProfessionType,
                    //    Income = item.Income,
                    //    Country = item.Country,
                    //    Region = item.Region,
                    //    InsurancePolicyCode = subItem.InsurancePolicyCategoryCode,
                    //};

                    //var prediction = MLClassifierCustomerFeature.Predict(modelInput);

                    MatrixUsersItems userItem = new MatrixUsersItems();
                    //if (subItem.Id == (byte)prediction.PredictedLabel)
                    //    userItem = new MatrixUsersItems
                    //    {
                    //        UserId = item.CustomerId,
                    //        ItemId = (byte)prediction.PredictedLabel,
                    //        Rating = prediction.Score.Max()
                    //    };
                    //else
                    //    userItem = new MatrixUsersItems
                    //    {
                    //        UserId = item.CustomerId,
                    //        ItemId = subItem.Id,
                    //        Rating = 0f
                    //    };

                    matrixUsersItems.Add(userItem);
                }
            }
            return matrixUsersItems;
        }

        public IList<InsurancePolicyCategory> GetRecommendation(string customerCode, float minScore, int maxItems)
        {
            var customerLearningFeature = getCustomerLearningFeature(customerCode);
            var customerId = getCustomerId(customerLearningFeature);
            var recommendedItems = getItemRecommendations(customerId, minScore, maxItems);
            var recommdendedInsurancePolicyCategories = getRecommendedInsurancePolicyCategories(recommendedItems);
            return recommdendedInsurancePolicyCategories;
        }

        public override void Dispose()
        {
            if (UnitOfWork.IsTransactionOpened)
            {
                if (_countError > 0)
                    UnitOfWork.RollbackTransaction();
                else
                    UnitOfWork.CommitTransaction();
            }

            UnitOfWork.Dispose();
        }
    }
}
