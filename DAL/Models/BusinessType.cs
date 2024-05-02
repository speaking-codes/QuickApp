using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BusinessType : AuditableEntity
    {
        public byte Id { get; set; }
        public string BusinessTypeName { get; set; }
        public bool IsWholesaleBusinessActivity { get; set; }
        public int MinEmployedNumbers { get; set; }
        public int MaxEmployedNumbers { get; set; }
        public float MinGrossAnnualRevenue { get; set; }

        public virtual IList<Business> Businesses { get; set; }
    }
}
