using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PowerType:AuditableEntity
    {
        public byte Id { get; set; }
        public string PowerTypeDescription { get; set; }
        public bool IsActive { get; set; }

        public virtual IList<ConfigurationModel> ConfigurationModels { get; set; }
    }
}
