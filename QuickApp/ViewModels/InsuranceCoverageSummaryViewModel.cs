using System.Collections.Generic;

namespace QuickApp.ViewModels
{
    public class InsuranceCoverageSummaryViewModel
    {
        public string CustomerCode { get; set; }
        public string Code { get; set; }
        public string CategoryType { get; set; }
        public IList<string> ItemDescriptions { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string TotalPrice { get; set; }
    }
}
