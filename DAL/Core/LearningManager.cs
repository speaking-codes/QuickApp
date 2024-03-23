using DAL.Core.Interfaces;
using System;
using System.Threading.Tasks;

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
                //var learningTrainings = UnitOfWork.LearningTrainings.GetAll();
                //MatrixCustomerInsurancePolicy matrixCustomerInsurancePolicy = null;
                //ModelInput modelInput = null;
                //ModelOutput modelOutput = null;

                //await UnitOfWork.BeginTransactionAsync();

                //foreach (var item in learningTrainings)
                //{
                //    modelInput = new ModelInput
                //    {
                //        Gender = item.Gender,
                //        Age = item.Age,
                //        MaritalStatusId = item.MaritalStatusId,
                //        FamilyTypeId = item.FamilyTypeId,
                //        ChildrenNumbers = item.ChildrenNumbers,
                //        IncomeTypeId = item.IncomeTypeId,
                //        ProfessionTypeId = item.ProfessionTypeId,
                //        Income = (float)item.Income,
                //        RegionId = item.RegionId,
                //        InsurancePolicyCategoryId = item.InsurancePolicyCategoryId,                        
                //    };
                //    modelOutput = RegressionPredictionModel.Predict(modelInput);
                //    matrixCustomerInsurancePolicy = new MatrixCustomerInsurancePolicy
                //    {
                //        UserId = item.UserId,
                //        InsurancePolicyCategoryId = item.InsurancePolicyCategoryId,
                //    };
                //    UnitOfWork.MatrixCustomerInsurancePolicies.Add(matrixCustomerInsurancePolicy);
                //}

                //UnitOfWork.SaveChanges();
                //await UnitOfWork.CommitTransactionAsync();
            }
            catch
            {
                //UnitOfWork.RollbackTransaction();
            }
        }

        public void GetPrediction()
        {
            //var result = new RegressionPredictionModel.ModelOutput();
            //return result;
            throw new NotImplementedException();
        }

        public void GetRecommendation(int customerId, byte insurancePolicyCategory)
        {
            ////Load sample data
            //var sampleData = new RecommenderSystemModel.ModelInput()
            //{
            //    UserId = customerId,
            //    InsurancePolicyCategoryId = insurancePolicyCategory,
            //};

            ////Load model and predict output
            //var result = RecommenderSystemModel.Predict(sampleData);
            //return result;
            throw new NotImplementedException();
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
