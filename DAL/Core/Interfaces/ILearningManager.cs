using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Core.Interfaces
{
    public interface ILearningManager : IManager
    {
        IList<CustomerLearningFeature> LoadDataFromStorage(string pathFile);
        Task SaveDataFeaturs(IList<CustomerLearningFeature> customerLearningFeatures);
        IList<CustomerLearningFeature> LoadDataFeatures();
        Task UpdateDataFeatures(IList<CustomerLearningFeature> customerLearningFeatures);

        void CreateClassifierModel();
        void PredictionClassification();

        IList<MatrixUsersItems> LoadMatrixUsersItems();
        Task SaveDataMatrixUsersItems(IList<MatrixUsersItems> matrixUsersItems);

        IList<InsurancePolicyCategory> GetRecommendation(string customerCode, float minScore, int maxItems);
    }
}
