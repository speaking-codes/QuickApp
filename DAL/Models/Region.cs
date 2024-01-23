using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Region:AuditableEntity
    {
        public byte Id { get; set; }
        public string RegionName { get; set; }

        public virtual Area Area { get; set; }
        public virtual IList<Province> Provinces { get; set; }
    }
}
