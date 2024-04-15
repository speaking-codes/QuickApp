using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WarrantySelected:AuditableEntity
    {
        public int Id { get; set; }
        
        public virtual WarrantyAvaible WarrantyAvaible { get; set; }

        public virtual InsurancePolicy InsurancePolicy { get; set; }
    }
}
