using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class IncomeType : AuditableEntity
    {
        public byte Id { get; set; }
        public string IncomeTypeDescription { get; set; }

        public virtual IList<Customer> Customers { get; set; }
    }
}
