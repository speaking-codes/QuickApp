using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SalesLineType:AuditableEntity
    {
        public byte Id { get; set; }
        public string SalesLineCode { get; set; }
        public string SalesLineName { get; set; }
        public string BackGroundColor { get; set; }
        public string BackGroundColorCssClass { get; set; }
        public bool IsActive { get; set; }

        public virtual IList<InsurancePolicyCategory> InsurancePolicyCategories { get; set; }
    }
}
