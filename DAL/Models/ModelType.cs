using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ModelType:AuditableEntity
    {
        public Byte Id { get; set; }
        public string ModelTypeDescription { get; set; }
        public bool IsByke { get; set; }
        public bool IsActive { get; set; }

        public virtual IList<ConfigurationModel> ConfigurationModels { get; set; }
    }
}
