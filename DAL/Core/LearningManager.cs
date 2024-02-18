using DAL.Core.Interfaces;
using DAL.Models;
using MachineLearningModel;
using System.Threading.Tasks;
using static MachineLearningModel.RegressionPredictionModel;

namespace DAL.Core
{
    public class LearningManager : Manager, ILearningManager
    {
        public LearningManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task LoadMatrixUserItems()
        {
            try
            {
                var learningTrainings = UnitOfWork.LearningTrainings.GetAll();
                MatrixCustomerInsurancePolicy matrixCustomerInsurancePolicy = null;
                ModelInput modelInput = null;
                ModelOutput modelOutput = null;

                await UnitOfWork.BeginTransactionAsync();

                foreach (var item in learningTrainings)
                {
                    modelInput = new ModelInput
                    {
                        Id = item.Id,
                        CustomerId = item.CustomerId,
                        Gender = item.Gender,
                        Age = item.Age,
                        MaritalStatusId = item.MaritalStatusId,
                        FamilyTypeId = item.FamilyTypeId,
                        ChildrenNumbers = item.ChildrenNumbers,
                        IncomeTypeId = item.IncomeTypeId,
                        ProfessionTypeId = item.ProfessionTypeId,
                        Income = (float)item.Income,
                        RegionId = item.RegionId,
                        InsurancePolicyCategoryId = item.InsurancePolicyCategoryId,
                        RenewalNumber = item.RenewalNumber,
                    };
                    modelOutput = RegressionPredictionModel.Predict(modelInput);
                    matrixCustomerInsurancePolicy = new MatrixCustomerInsurancePolicy
                    {
                        CustomerId = item.CustomerId,
                        InsurancePolicyCategoryId = item.InsurancePolicyCategoryId,
                        Rating = modelOutput.Score
                    };
                    UnitOfWork.MatrixCustomerInsurancePolicies.Add(matrixCustomerInsurancePolicy);
                }

                UnitOfWork.SaveChanges();
                await UnitOfWork.CommitTransactionAsync();
            }
            catch
            {
                UnitOfWork.RollbackTransaction();
            }
        }

        public RecommenderSystemModel.ModelOutput GetRecommendation(int customerId, byte insurancePolicyCategory)
        {
            //Load sample data
            var sampleData = new RecommenderSystemModel.ModelInput()
            {
                Id = customerId,
                CustomerId = customerId,
                InsurancePolicyCategoryId = insurancePolicyCategory,
            };

            //Load model and predict output
            var result = RecommenderSystemModel.Predict(sampleData);
            return result;
        }

        public override void Dispose()
        {
            if (IsMassiveWriter && UnitOfWork.IsTransactionOpened)
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
