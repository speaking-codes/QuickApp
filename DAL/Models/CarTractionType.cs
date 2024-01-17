using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CarTractionType:AuditableEntity
    {
        public byte Id { get; set; }
        public string TractionTypeDescription { get; set; }

        public virtual IList<ConfigurationModel> ConfigurationModels { get; set; }
    }
}
