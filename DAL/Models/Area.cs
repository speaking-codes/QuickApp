using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Area:AuditableEntity
    {
        public byte Id { get; set; }
        public string AreaName { get; set; }

        public virtual IList<Region> Regions { get; set; }
    }
}
