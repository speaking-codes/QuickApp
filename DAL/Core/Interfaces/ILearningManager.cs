using MachineLearningModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MachineLearningModel.RecommenderSystemModel;

namespace DAL.Core.Interfaces
{
    public interface ILearningManager : IManager
    {
        Task LoadMatrixUserItems();

        RecommenderSystemModel.ModelOutput GetRecommendation(int customerId, byte insurancePolicyCategory);
    }
}
