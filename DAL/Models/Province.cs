using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Province:AuditableEntity
    {
        public short Id { get; set; }
        public string ProvinceName { get; set; }
        public string ProvinceAbbreviation { get; set; }

        public virtual Region Region { get; set; }
        public virtual IList<Municipality> Municipalities { get; set; }
    }
}
