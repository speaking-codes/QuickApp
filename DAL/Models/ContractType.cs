using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ContractType : AuditableEntity
    {
        public byte Id { get; set; }
        public string ContractTypeDescription { get; set; }
        public string ContractTypeTitle { get; set; }
        public float RatingCoefficient { get; set; }
    }
}
