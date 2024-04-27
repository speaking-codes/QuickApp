using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class GenderType : AuditableEntity
    {
        public byte Id { get; set; }
        public string GenderDescription { get; set; }
        public string GenderTitle { get; set; }
    }
}
