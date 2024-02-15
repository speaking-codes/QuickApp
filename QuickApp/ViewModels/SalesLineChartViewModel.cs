using MongoDB.Bson.Serialization.Attributes;

namespace QuickApp.ViewModels
{
    public class SalesLineChartViewModel
    {
        public string SalesLineCode { get; set; }

        public string SalesLineName { get; set; }

        public string BackGroundColor { get; set; }

        public string TotalPrice { get; set; }

        public int TotalCount { get; set; }
    }
}
