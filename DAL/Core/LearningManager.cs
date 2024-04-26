using DAL.Core.Interfaces;
using DAL.Enums;
using DAL.Mapping;
using DAL.ModelML;
using DAL.Models;
using MachineLearningModel;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Core
{
    public class LearningManager : Manager, ILearningManager
    {
        #region Private 

        private IList<ProfessionType> _professionTypeList;
        private IList<MaritalStatusType> _maritalStatusList;
        private IList<Province> _provinceList;
        private IList<IncomeClassType> _incomeClassTypeList;
        private IList<int> _yearList;
        private IList<InsurancePolicyCategory> _insurancePolicyCategoryList;

        private IList<int> getYearList()
        {
            var yearList = new List<int>();
            for (var year = 1915; year <= 2006; year++)
                yearList.Add(year);

            return yearList;
        }

        //public async Task LoadMatrixUserItems()
        //{
        //    try
        //    {
        //        //var learningTrainings = UnitOfWork.LearningTrainings.GetAll();
        //        //MatrixCustomerInsurancePolicy matrixCustomerInsurancePolicy = null;
        //        //ModelInput modelInput = null;
        //        //ModelOutput modelOutput = null;

        //        //await UnitOfWork.BeginTransactionAsync();

        //        //foreach (var item in learningTrainings)
        //        //{
        //        //    modelInput = new ModelInput
        //        //    {
        //        //        Gender = item.Gender,
        //        //        Age = item.Age,
        //        //        MaritalStatusId = item.MaritalStatusId,
        //        //        FamilyTypeId = item.FamilyTypeId,
        //        //        ChildrenNumbers = item.ChildrenNumbers,
        //        //        IncomeTypeId = item.IncomeTypeId,
        //        //        ProfessionTypeId = item.ProfessionTypeId,
        //        //        Income = (float)item.Income,
        //        //        RegionId = item.RegionId,
        //        //        InsurancePolicyCategoryId = item.InsurancePolicyCategoryId,                        
        //        //    };
        //        //    modelOutput = RegressionPredictionModel.Predict(modelInput);
        //        //    matrixCustomerInsurancePolicy = new MatrixCustomerInsurancePolicy
        //        //    {
        //        //        UserId = item.UserId,
        //        //        InsurancePolicyCategoryId = item.InsurancePolicyCategoryId,
        //        //    };
        //        //    UnitOfWork.MatrixCustomerInsurancePolicies.Add(matrixCustomerInsurancePolicy);
        //        //}

        //        //UnitOfWork.SaveChanges();
        //        //await UnitOfWork.CommitTransactionAsync();
        //    }
        //    catch
        //    {
        //        //UnitOfWork.RollbackTransaction();
        //    }
        //}

        //public void GetPrediction()
        //{
        //    //var result = new RegressionPredictionModel.ModelOutput();
        //    //return result;
        //    throw new NotImplementedException();
        //}

        //public void GetRecommendation(int customerId, byte insurancePolicyCategory)
        //{
        //    ////Load sample data
        //    //var sampleData = new RecommenderSystemModel.ModelInput()
        //    //{
        //    //    UserId = customerId,
        //    //    InsurancePolicyCategoryId = insurancePolicyCategory,
        //    //};

        //    ////Load model and predict output
        //    //var result = RecommenderSystemModel.Predict(sampleData);
        //    //return result;
        //    throw new NotImplementedException();
        //}

        private IList<string> fillEmptyValue(IList<string> lineSplit)
        {
            var rnd = new Random();
            var indexRnd = 0;
            var annualGrossIncomeBase = 0.0;

            #region Sesso

            if (string.IsNullOrEmpty(lineSplit[0].Trim()))
            {
                if (string.IsNullOrEmpty(lineSplit[4]))
                    lineSplit[0] = "S";
                else
                    lineSplit[0] = (rnd.Next() % 2) == 0 ? "M" : "F";
            }

            #endregion

            #region Data di Nascita

            if (string.IsNullOrEmpty(lineSplit[1]))
            {
                indexRnd = rnd.Next(_yearList.Count);
                var year = _yearList[indexRnd];
                var month = rnd.Next(1, 13);
                var day = 0;

                if (month == 2)
                    day = rnd.Next(1, 29);
                else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                    day = rnd.Next(1, 32);
                else
                    day = rnd.Next(1, 31);

                var dataNascita = new DateTime(year, month, day);
                lineSplit[1] = dataNascita.ToString("dd/MM/yyyy");
            }

            #endregion

            #region Stato Civile

            if (string.IsNullOrEmpty(lineSplit[2]))
            {
                indexRnd = rnd.Next(_maritalStatusList.Count);
                lineSplit[2] = _maritalStatusList[indexRnd].MaritalStatusDescription;
            }

            #endregion

            #region Coniuge a Carico

            if (string.IsNullOrEmpty(lineSplit[3]))
            {
                if (_maritalStatusList.FirstOrDefault(x => x.MaritalStatusDescription == lineSplit[2]).IsSingle)
                    lineSplit[3] = System.Boolean.FalseString;
                else
                    lineSplit[3] = (rnd.Next() % 2) == 0 ? System.Boolean.TrueString : System.Boolean.FalseString;
            }

            #endregion

            #region Numero Figli

            if (string.IsNullOrEmpty(lineSplit[4]))
            {
                if (lineSplit[0] == "0" || lineSplit[0] == "X" || lineSplit[0] == "S")
                    lineSplit[4] = "0";
                else
                    lineSplit[4] = (rnd.Next() % 4).ToString();
            }

            #endregion

            #region Numero Figli a Carico

            if (string.IsNullOrEmpty(lineSplit[5]))
            {
                var dataNascita = DateTime.Parse(lineSplit[1]);
                var age = (int)DateTime.Now.Subtract(dataNascita).TotalDays / 365;
                var childrenNumbers = int.Parse(lineSplit[4]);

                if (childrenNumbers == 0 || age < 18)
                    lineSplit[5] = "0";
                else
                    lineSplit[5] = rnd.Next(0, childrenNumbers + 1).ToString();
            }

            #endregion

            #region Professione

            if (lineSplit[6] == "Società" || lineSplit[6] == @"Societ�")
                lineSplit[6] = "Societa'";

            #endregion

            #region RAL

            ProfessionType professionType = null;
            if (string.IsNullOrEmpty(lineSplit[7]))
            {
                professionType = _professionTypeList.FirstOrDefault(x => x.ProfessionTypeDescription == lineSplit[6]);

                if ((rnd.Next() % 2) == 0)
                    annualGrossIncomeBase = professionType.MinAnnualGrossIncome * (1 + rnd.NextSingle());
                else
                    annualGrossIncomeBase = professionType.MaxAnnualGrossIncome * rnd.NextSingle();

                if (annualGrossIncomeBase < professionType.MinAnnualGrossIncome)
                    annualGrossIncomeBase = professionType.MinAnnualGrossIncome;

                lineSplit[7] = annualGrossIncomeBase.ToString();
            }

            #endregion

            #region Tipo Reddito

            if (string.IsNullOrEmpty(lineSplit[8]))
                lineSplit[8] = professionType.IncomeType.IncomeTypeDescription;

            #endregion

            #region Provincia

            if (string.IsNullOrEmpty(lineSplit[10]))
            {
                indexRnd = rnd.Next(0, _provinceList.Count);
                lineSplit[10] = _provinceList[indexRnd].ProvinceAbbreviation;
            }

            #endregion

            #region Region

            lineSplit[9] = lineSplit[10];
            lineSplit[10] = _provinceList[indexRnd].Region.RegionName;

            #endregion

            return lineSplit;
        }

        private CustomerLearningFeature getCustomerLearningFeature(IList<string> lineSplit)
        {
            var customerLearningFeature = new CustomerLearningFeature();

            customerLearningFeature.Gender = lineSplit[0].Trim();

            var birthDate = DateTime.Parse(lineSplit[1]);
            customerLearningFeature.BirthMonth = birthDate.Month.ToString();
            customerLearningFeature.YearBirth = birthDate.Year.ToString();

            customerLearningFeature.MaritalStatus = lineSplit[2].Trim();
            customerLearningFeature.IsSingle = _maritalStatusList.First(x => x.MaritalStatusDescription == customerLearningFeature.MaritalStatus).IsSingle;
            customerLearningFeature.IsDependentSpouse = bool.Parse(lineSplit[3]);
            customerLearningFeature.ChildrenNumbers = int.Parse(lineSplit[4]);
            customerLearningFeature.DependentChildrenNumber = int.Parse(lineSplit[5]);

            customerLearningFeature.ProfessionType = lineSplit[6].Trim();

            var profession = _professionTypeList.FirstOrDefault(x => x.ProfessionTypeDescription == customerLearningFeature.ProfessionType);
            customerLearningFeature.IsFreelancer = profession.IsFreelancer;

            var annualGrossIncome = double.Parse(lineSplit[7]);
            var incomeClassType = _incomeClassTypeList.FirstOrDefault(x => x.MinAnnualGrossIncome <= annualGrossIncome && (!x.MaxAnnualGrossIncome.HasValue || x.MaxAnnualGrossIncome >= annualGrossIncome));
            customerLearningFeature.IncomeClassType = (incomeClassType != null) ? incomeClassType.DescriptionIncomeClass : string.Empty;

            customerLearningFeature.IncomeType = lineSplit[8].Trim();
            customerLearningFeature.Country = lineSplit[9].Trim();
            customerLearningFeature.Region = lineSplit[10].Trim();
            customerLearningFeature.InsurancePolicyName = lineSplit[11].Trim();

            return customerLearningFeature;
        }

        private CustomerLearningFeature getInsurancePolicyData(CustomerLearningFeature customerLearningFeature)
        {
            var insurancePolicyCategory = _insurancePolicyCategoryList.FirstOrDefault(x => x.InsurancePolicyCategoryName.ToLower() == customerLearningFeature.InsurancePolicyName.ToLower());

            customerLearningFeature.InsurancePolicyId = insurancePolicyCategory.Id;
            customerLearningFeature.InsurancePolicyCode = insurancePolicyCategory.InsurancePolicyCategoryCode;
            customerLearningFeature.InsurancePolicyDescription = insurancePolicyCategory.InsurancePolicyCategoryDescription;
            customerLearningFeature.WarrantyAvaibles = string.Join(",", insurancePolicyCategory.WarrantyAvaibles.Select(x => x.WarrantyName.Trim()).ToList());

            if (customerLearningFeature.WarrantyAvaibles.Length > 8000)
                customerLearningFeature.WarrantyAvaibles = customerLearningFeature.WarrantyAvaibles.Substring(0, 7990);
            return customerLearningFeature;
        }

        private CustomerLearningFeature fillCustomerLearningFeature(string line)
        {
            IList<string> lineSplit = line.Split(new char[] { ';' }).ToList();
            lineSplit = fillEmptyValue(lineSplit);
            var customerLearningFeature = getCustomerLearningFeature(lineSplit);
            customerLearningFeature = getInsurancePolicyData(customerLearningFeature);
            //customerLearningFeature.Rating = getRating(customerLearningFeature);
            return customerLearningFeature;
        }

        private MLContext getMLContext() => new MLContext();

        private IDataView loadFromDataSource(MLContext mlContext)
        {
            var customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeaturesForTraining().Take(1000).ToList();
            return mlContext.Data.LoadFromEnumerable(customerLearningFeatures.ToDataViewModels());
        }

        private IEstimator<ITransformer> buildPipeline(MLContext mlContext)
        {
            var x = mlContext.Transforms.Categorical.OneHotEncoding(new[]
                                        {
                                            new InputOutputColumnPair(outputColumnName:"GenderEncoded", inputColumnName:"Gender"),
                                            new InputOutputColumnPair(outputColumnName:"BirthMonthEncoded", inputColumnName:"BirthMonth"),
                                            new InputOutputColumnPair(outputColumnName:"YearBirthEncoded",inputColumnName:"YearBirth"),
                                            new InputOutputColumnPair(outputColumnName:"MaritalStatusEncoded", inputColumnName:"MaritalStatus"),
                                            new InputOutputColumnPair(outputColumnName:"IsSingleEncoded", inputColumnName:"IsSingle"),
                                            new InputOutputColumnPair(outputColumnName:"IsDependentSpouseEncoded", inputColumnName:"IsDependentSpouse"),
                                            new InputOutputColumnPair(outputColumnName:"IsFreelancerEncoded", inputColumnName:"IsFreelancer")
                                        })
                                        .Append(mlContext.Transforms.Concatenate(outputColumnName: "Features", nameof(CustomerLearningFeatureDataView.CustomerId),
                                                                                   nameof(CustomerLearningFeatureDataView.ChildrenNumbers),
                                                                                   nameof(CustomerLearningFeatureDataView.DependentChildrenNumber),
                                                                                   "GenderEncoded",
                                                                                   "BirthMonthEncoded",
                                                                                   "YearBirthEncoded",
                                                                                   "MaritalStatusEncoded",
                                                                                   "IsSingleEncoded",
                                                                                   "IsDependentSpouseEncoded",
                                                                                   "IsFreelancerEncoded"
                                                                                   ))
                                        .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "PredictedLabel", inputColumnName: nameof(CustomerLearningFeatureDataView.InsurancePolicyId)));
            return x;
        }

        private EstimatorChain<KeyToValueMappingTransformer> getTrainer(MLContext mlContext)
        {
            var trainer = mlContext.MulticlassClassification.Trainers.SdcaNonCalibrated(labelColumnName: "PredictedLabel", featureColumnName: "Features")
                                                            .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: nameof(InsurancePrediction.PredictedInsurancePolicyId), inputColumnName: "PredictedLabel"));
            return trainer;
        }

        private CustomerLearningFeature getCustomerLearningFeature(string customerCode)
        {
            var customer = UnitOfWork.Customers.GetCustomersForRecommendations(customerCode).FirstOrDefault();
            var customerLearningFeature = new CustomerLearningFeature
            {
                Gender = customer.Gender.GetCode(),
                BirthMonth = customer.BirthDate.Value.Month.ToString(),
                YearBirth = customer.BirthDate.Value.Year.ToString(),
                MaritalStatus = customer.MaritalStatus.MaritalStatusDescription,
                IsSingle = customer.MaritalStatus.IsSingle,
                ChildrenNumbers = customer.ChildrenNumber ?? 0,
                ProfessionType = customer.ProfessionType.ProfessionTypeDescription,
                IsFreelancer = customer.ProfessionType.IsFreelancer,
                Country = customer.Addresses.Where(x => x.IsPrimary).FirstOrDefault()?.Municipality.Province.ProvinceAbbreviation,
                Region = customer.Addresses.Where(x => x.IsPrimary).FirstOrDefault()?.Municipality.Province.Region.RegionName,
            };
            return customerLearningFeature;
        }

        public long getCustomerId(CustomerLearningFeature customerLearningFeature)
        {
            var customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeature(customerLearningFeature).OrderByDescending(x => x.CustomerId).ToList();
            if (customerLearningFeatures.Count > 0)
                return customerLearningFeatures.First().CustomerId ?? long.MinValue;

            return long.MinValue;
        }

        private IList<MLRecommenderSystemCustomer.ModelOutput> getItemRecommendations(long customerId, float minScore, int maxItems)
        {
            var insurancePolicyCategories = UnitOfWork.InsurancePolicyCategories.GetAll();

            var descendingComparer = Comparer<float>.Create((x, y) => y.CompareTo(x));
            var predictions = new SortedList<float, MLRecommenderSystemCustomer.ModelOutput>(descendingComparer);

            foreach (var item in insurancePolicyCategories)
            {
                var modelInput = new MLRecommenderSystemCustomer.ModelInput { UserId = customerId, ItemId = item.Id };
                var modelOutput = MLRecommenderSystemCustomer.Predict(modelInput);
                predictions.Add(modelOutput.Score, modelOutput);
            }

            return predictions.Where(x => x.Key >= minScore)
                              .Select(x => x.Value)
                              .Take(maxItems)
                              .ToList();
        }

        private IList<InsurancePolicyCategory> getRecommendedInsurancePolicyCategories(IEnumerable<MLRecommenderSystemCustomer.ModelOutput> predictions)
        {
            var insurancePolicyCategories = new List<InsurancePolicyCategory>();

            foreach (var item in predictions)
            {
                var insurancePolicyCategory = UnitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategory((byte)item.ItemId).Single();
                insurancePolicyCategories.Add(insurancePolicyCategory);
            }

            return insurancePolicyCategories;
        }

        #endregion

        public LearningManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _professionTypeList = UnitOfWork.ProfessionTypes.GetProfessionTypes().ToList();
            _maritalStatusList = UnitOfWork.MaritalStatusTypes.GetAll();
            _provinceList = UnitOfWork.Provinces.GetProvinces().ToList();
            _incomeClassTypeList = UnitOfWork.IncomeClassTypes.GetAll();
            _yearList = getYearList();
            _insurancePolicyCategoryList = unitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategories().ToList();
        }

        public IList<CustomerLearningFeature> LoadDataFromStorage(string pathFile)
        {
            var customerLearningFeatures = new List<CustomerLearningFeature>();

            using (var sr = new StreamReader(pathFile))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                        customerLearningFeatures.Add(fillCustomerLearningFeature(line));
                }
            }

            return customerLearningFeatures;
        }

        public async Task SaveDataFeaturs(IList<CustomerLearningFeature> customerLearningFeatures)
        {
            try
            {
                await UnitOfWork.BeginTransactionAsync();

                foreach (var item in customerLearningFeatures)
                    UnitOfWork.CustomerLearningFeatures.Add(item);

                await UnitOfWork.SaveChangesAsync();
                await UnitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                UnitOfWork.RollbackTransaction();

                throw;
            }
        }

        public IList<CustomerLearningFeature> LoadDataFeatures()
        {
            var customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeatures()
                                                           .Where(x => !x.CustomerId.HasValue)
                                                           .Select(x => new CustomerLearningFeature
                                                           {
                                                               Gender = x.Gender,
                                                               BirthMonth = x.BirthMonth,
                                                               YearBirth = x.YearBirth,
                                                               MaritalStatus = x.MaritalStatus,
                                                               IsSingle = x.IsSingle,
                                                               IsDependentSpouse = x.IsDependentSpouse,
                                                               ChildrenNumbers = x.ChildrenNumbers,
                                                               DependentChildrenNumber = x.DependentChildrenNumber,
                                                               ProfessionType = x.ProfessionType,
                                                               IsFreelancer = x.IsFreelancer,
                                                               IncomeClassType = x.IncomeClassType,
                                                               IncomeType = x.IncomeType,
                                                               Country = x.Country,
                                                               Region = x.Region
                                                           })
                                                           .Distinct()
                                                           .ToList();

            long customerId = UnitOfWork.CustomerLearningFeatures.GetMaxCustomerId() + 1;
            foreach (var item in customerLearningFeatures)
            {
                item.CustomerId = customerId;
                customerId++;
            }

            return customerLearningFeatures;
        }

        public async Task UpdateDataFeatures(IList<CustomerLearningFeature> customerLearningFeatures)
        {
            try
            {
                long rowCount = 0;
                await UnitOfWork.BeginTransactionAsync();

                foreach (var item in customerLearningFeatures)
                {
                    rowCount += UnitOfWork.CustomerLearningFeatures.CustomUpdate(item);

                    if (rowCount > 60000)
                        break;
                }

                await UnitOfWork.CommitTransactionAsync();
            }
            catch
            {
                UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public void CreateClassifierModel()
        {
            var mlContext = getMLContext();
            var data = loadFromDataSource(mlContext);
            var modelPath = @"C:\Users\mauro.diliddo\source\repos\QuickApp\DataStorage\Output\model.zip";
            // Dividere i dati in set di training e test
            var trainTestSplit = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);
            var pipeline = buildPipeline(mlContext);
            var trainer = getTrainer(mlContext);
            pipeline.Append(trainer);
            var model = pipeline.Fit(trainTestSplit.TrainSet);
            mlContext.Model.Save(model, data.Schema, modelPath);
        }

        public void PredictionClassification()
        {
            DataViewSchema columns;
            var mlContext = getMLContext();
            var modelPath = @"C:\Users\mauro.diliddo\source\repos\QuickApp\DataStorage\Output\model.zip";
            var mlModel = mlContext.Model.Load(modelPath, out columns);
            var predictionEngine = mlContext.Model.CreatePredictionEngine<CustomerLearningFeatureDataView, InsurancePrediction>(mlModel);

            var dataTest = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeaturesForTraining().Skip(1000).Take(10).ToList().ToDataViewModels();
            foreach (var item in dataTest)
            {
                var prediction = predictionEngine.Predict(item);
                var labelBuffer = new VBuffer<ReadOnlyMemory<char>>();
                var annotations = predictionEngine.OutputSchema["PredictedLabel"].Annotations;//.GetValue("KeyValues", ref labelBuffer);

                //var labels = labelBuffer.DenseValues().Select(l => l.ToString()).ToArray();
            }
        }

        public IList<MatrixUsersItems> LoadMatrixUsersItems()
        {
            var customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeaturesForTraining()
                                                                              .OrderBy(x => x.CustomerId)
                                                                              .Take(1200)
                                                                              .ToList();
            var insurancePolicyCategories = UnitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategories().ToList();
            var matrixUsersItems = new List<MatrixUsersItems>();

            foreach (var item in customerLearningFeatures)
            {
                foreach (var subItem in insurancePolicyCategories)
                {
                    var modelInput = new MLClassifierCustomerFeature.ModelInput()
                    {
                        //Id = item.Id,
                        CustomerId = item.CustomerId ?? 0f,
                        Gender = item.Gender,
                        BirthMonth = item.BirthMonth,
                        YearBirth = item.YearBirth,
                        MaritalStatus = item.MaritalStatus,
                        IsSingle = item.IsSingle,
                        IsDependentSpouse = item.IsDependentSpouse,
                        ChildrenNumbers = item.ChildrenNumbers,
                        DependentChildrenNumber = item.DependentChildrenNumber,
                        ProfessionType = item.ProfessionType,
                        IsFreelancer = item.IsFreelancer,
                        IncomeClassType = item.IncomeClassType,
                        IncomeType = item.IncomeType,
                        Country = item.Country,
                        Region = item.Region,
                        InsurancePolicyCode = subItem.InsurancePolicyCategoryCode,
                        InsurancePolicyName = subItem.InsurancePolicyCategoryName,
                        //InsurancePolicyDescription = subItem.InsurancePolicyCategoryDescription,
                        //WarrantyAvaibles = string.Join(",", subItem.WarrantyAvaibles.Select(x => x.WarrantyName).ToArray())
                    };

                    var prediction = MLClassifierCustomerFeature.Predict(modelInput);

                    MatrixUsersItems userItem;
                    if (subItem.Id == (byte)prediction.PredictedLabel)
                        userItem = new MatrixUsersItems
                        {
                            UserId = (long)prediction.CustomerId,
                            ItemId = (byte)prediction.PredictedLabel,
                            Rating = prediction.Score.Max()
                        };
                    else
                        userItem = new MatrixUsersItems
                        {
                            UserId = item.CustomerId ?? 0,
                            ItemId = subItem.Id,
                            Rating = 0f
                        };

                    matrixUsersItems.Add(userItem);
                }
            }
            return matrixUsersItems;
        }

        public async Task SaveDataMatrixUsersItems(IList<MatrixUsersItems> matrixUsersItems)
        {
            try
            {
                await UnitOfWork.BeginTransactionAsync();

                foreach (var item in matrixUsersItems)
                    UnitOfWork.MatrixUsersItems.Add(item);

                await UnitOfWork.SaveChangesAsync();
                await UnitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                UnitOfWork.RollbackTransaction();

                throw;
            }
        }

        public IList<InsurancePolicyCategory> GetRecommendation(string customerCode, float minScore, int maxItems)
        {
            var customerLearningFeature = getCustomerLearningFeature(customerCode);
            var customerId = getCustomerId(customerLearningFeature);
            customerId = 833;
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
