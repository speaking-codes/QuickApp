using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Model:AuditableEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public bool IsActive { get; set; }

        public virtual Brand Brand { get; set; }
    
        public virtual IList<ConfigurationModel> ConfigurationModels { get; set; }
    }
}
