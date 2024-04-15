using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class InsurancePolicyCategory:AuditableEntity
    {
        public byte Id { get; set; }
        public string InsurancePolicyCategoryCode { get; set; }
        public string InsurancePolicyCategoryName { get; set; }
        public string InsurancePolicyCategoryDescription { get; set; }
        public string IconCssClass { get; set; }
        public bool IsActive { get; set; }

        public virtual SalesLineType SalesLine { get; set; }
        public virtual IList<InsurancePolicy> InsurancePolicies { get; set; }
        public virtual IList<WarrantyAvaible> WarrantyAvaibles { get; set; }
        public virtual IList<InsurancePolicyCategoryStatic> InsurancePolicyCategoryStatics { get; set; }
    }
}
