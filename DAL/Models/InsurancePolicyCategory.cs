using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class InsurancePolicyCategory
    {
        public byte Id { get; set; }
        public string InsurancePolicyCategoryCode { get; set; }
        public string InsurancePolicyCategoryName { get; set; }
        public string IconCssClass { get; set; }
        public string BackGroundColorCssClass { get; set; }

        public virtual SalesLineType SalesLine { get; set; }
        public virtual IList<InsurancePolicy> InsurancePolicies { get; set; }
    }
}
