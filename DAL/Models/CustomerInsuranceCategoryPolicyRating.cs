using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CustomerInsuranceCategoryPolicyRating:AuditableEntity
    {
        public long Id { get; set; }
        public int CustomerId { get; set; }
        public byte InsuranceCategoryPolicy { get; set; }
        public float Rating { get; set; }
    }
}
