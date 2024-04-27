using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ProfessionType:AuditableEntity
    {
        public short Id { get; set; }
        public string ProfessionTypeDescription { get; set; }
        public double MinAnnualGrossIncome { get; set; }
        public double MaxAnnualGrossIncome { get; set; }
        public bool? IsFreelancer { get; set; }

        public virtual IncomeType IncomeType { get; set; }

        public virtual IList<Customer> Customers { get; set; }
    }
}
