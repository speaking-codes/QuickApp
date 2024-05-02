using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Business : AuditableEntity
    {
        public int Id { get; set; }
        public string BusinessTitle { get; set; }
        public string BusinessLocation { get; set; }
        public int EmployeeNumbers { get; set; }
        public float AnnualRevenue { get; set; }

        public virtual BusinessType BusinessType { get; set; }

        public virtual Municipality Municipality { get; set; }

        public virtual InsurancePolicy InsurancePolicy { get; set; }
    }
}
