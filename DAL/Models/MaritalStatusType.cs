using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MaritalStatusType : AuditableEntity
    {
        public byte Id { get; set; }
        public string MaritalStatusDescription { get; set; }

        public virtual IList<Customer> Customers { get; set; }
    }
}
