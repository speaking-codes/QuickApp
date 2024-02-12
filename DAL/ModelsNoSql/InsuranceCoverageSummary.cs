using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelsNoSql
{
    public class InsuranceCoverageSummary
    {
        public string CustomerCode { get; set; }
        public string Code { get; set; }
        public string CategoryType { get; set; }
        public string ItemDescription { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string TotalPrice { get; set; }
    }
}
