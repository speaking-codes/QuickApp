using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BusinessType:AuditableEntity
    {
        public byte Id { get; set; }
        public string BusinessTypeName { get; set; }

        public virtual IList<Business> Businesses { get; set; }
    }
}
