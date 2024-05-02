using DAL.ModelsNoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryNoSql.Interfaces
{
    public interface IInsuranceCategoryPolicyRecommendationRepository:IRepositoryNoSql<InsuranceCategoryPolicyRecommendation>
    {
        InsuranceCategoryPolicyRecommendation GetInsuranceCategoryPolicyRecommendation(string customerCode);
    }
}
