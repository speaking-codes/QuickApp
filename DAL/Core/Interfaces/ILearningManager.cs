using System.Threading.Tasks;

namespace DAL.Core.Interfaces
{
    public interface ILearningManager : IManager
    {
        Task LoadMatrixUserItems();

        void GetPrediction();
        void GetRecommendation(int customerId, byte insurancePolicyCategory);
    }
}
