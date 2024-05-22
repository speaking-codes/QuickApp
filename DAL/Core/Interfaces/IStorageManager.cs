using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.Interfaces
{
    public interface IStorageManager
    {
        IList<CustomerLearningFeature> LoadDataFromStorage(string inputPath, string processedPath);
        Task SaveDataFeaturs(IList<CustomerLearningFeature> customerLearningFeatures);
        IList<CustomerLearningFeature> LoadDataFeatures();
        Task UpdateDataFeatures(IList<CustomerLearningFeature> customerLearningFeatures);
        Task ReduceDataFeaturesDuplicated();
        Task AddToStorage(string inputPath, Customer customer);

        Task<IList<CustomerLearningFeature>> LoadCustomerLearningFeature(long customerId);
        Task<IList<CustomerLearningFeatureCopy>> LoadCustomerLearningFeatureCopyForTest(long customerId);

        Task<long> GetLastUserIdFromMatrixUserItems();
        Task SaveMatrixUsersItems(IEnumerable<MatrixUsersItems> matrixUsersItems);
    }
}
