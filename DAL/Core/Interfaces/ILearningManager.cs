using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Core.Interfaces
{
    public interface ILearningManager : IManager
    {
        void TrainingClassifier();
        void PredictionClassification();

        Task<IList<MatrixUsersItems>> LoadMatrixUsersItems(IEnumerable<CustomerLearningFeature> customerLearningFeatures);
        IList<InsurancePolicyCategory> GetRecommendation(string customerCode, float minScore, int maxItems);
    }
}
