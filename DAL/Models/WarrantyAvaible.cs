using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WarrantyAvaible : AuditableEntity
    {
        public int Id { get; set; }
        public string WarrantyCode { get; set; }
        public string WarrantyName { get; set; }
        public bool IsPrimary { get; set; }

        public virtual InsurancePolicyCategory InsurancePolicyCategory { get; set; }

        public virtual IList<WarrantySelected> WarrantySelecteds { get; set; }
    }
}
