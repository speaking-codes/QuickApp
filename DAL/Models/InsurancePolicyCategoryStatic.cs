using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class InsurancePolicyCategoryStatic : AuditableEntity
    {
        public int Id { get; set; }
        public short Year { get; set; }
        public byte Month { get; set; }
        public int TotalCount { get; set; }

        public virtual InsurancePolicyCategory InsurancePolicyCategory { get; set; }
    }
}
