using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ArrangementCylinderType:AuditableEntity
    {
        public byte Id { get; set; }
        public string ArrangementCylinderDescription { get; set; }
        public bool IsActive { get; set; }

        public virtual IList<ConfigurationModel> ConfigurationModels { get; set; }
    }
}
