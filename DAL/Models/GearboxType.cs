using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class GearboxType:AuditableEntity
    {
        public byte Id { get; set; }
        public string GearboxTypeDescription { get; set; }
        public bool IsByke { get; set; }
        public bool IsActive { get; set; }

        public virtual IList<ConfigurationModel> ConfigurationModels { get; set; }
    }
}
