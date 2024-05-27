using DAL.Models;
using DAL.ModelsNoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exstensions
{
    public static class InsurancePolicyCategoryExstension
    {
        public static InsuranceCategoryPolicyDashboardCard ToInsuranceCategoryPolicyDashboardCard(this InsurancePolicyCategory insurancePolicyCategory) =>
                    new InsuranceCategoryPolicyDashboardCard
                    {
                        Code = insurancePolicyCategory.InsurancePolicyCategoryCode,
                        Name = insurancePolicyCategory.InsurancePolicyCategoryName,
                        Abstract = insurancePolicyCategory?.InsurancePolicyCategoryDescription.Length > 170 ? 
                                        insurancePolicyCategory.InsurancePolicyCategoryDescription.Substring(0, 170) + "..." : 
                                        insurancePolicyCategory?.InsurancePolicyCategoryDescription,
                        IconCssClass = insurancePolicyCategory?.IconCssClass,
                        SalesLineBackgroundColor = insurancePolicyCategory?.SalesLine?.BackGroundColor,
                        SalesLineBackgroundCssClass = insurancePolicyCategory?.SalesLine?.BackGroundColorCssClass,
                        SalesLineCode = insurancePolicyCategory?.SalesLine?.SalesLineCode,
                        SalesLineName = insurancePolicyCategory?.SalesLine?.SalesLineName,
                        Total = (insurancePolicyCategory != null &&
                                 insurancePolicyCategory.InsurancePolicyCategoryStatics != null &&
                                 insurancePolicyCategory.InsurancePolicyCategoryStatics.Count > 0) ?
                                        insurancePolicyCategory.InsurancePolicyCategoryStatics.Sum(x => x.TotalCount) : 0
                    };

        public static IList<InsuranceCategoryPolicyDashboardCard> ToInsuranceCategoryPolicyDashboardCards(this IEnumerable<InsurancePolicyCategory> insurancePolicyCategories)
        {
            var insuranceCategoryPolicyDashboardCards = new List<InsuranceCategoryPolicyDashboardCard>();

            foreach (var item in insurancePolicyCategories)
                insuranceCategoryPolicyDashboardCards.Add(item.ToInsuranceCategoryPolicyDashboardCard());

            return insuranceCategoryPolicyDashboardCards;
        }
    }
}
