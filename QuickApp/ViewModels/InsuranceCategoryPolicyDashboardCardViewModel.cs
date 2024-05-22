using MongoDB.Bson.Serialization.Attributes;

namespace QuickApp.ViewModels
{
    public class InsuranceCategoryPolicyDashboardCardViewModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Abstract { get; set; }

        public string IconCssClass { get; set; }
   
        public string SalesLineCode { get; set; }

        public string SalesLineName { get; set; }

        public string SalesLineBackgroundColor { get; set; }

        public string SalesLineBackgroundCssClass { get; set; }

        public bool IsTopSelling { get; set; }
    }
}
