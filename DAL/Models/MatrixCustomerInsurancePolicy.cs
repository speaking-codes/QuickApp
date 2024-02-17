using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MatrixCustomerInsurancePolicy
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int InsurancePolicyCategoryId { get; set; }
        public float? Rating { get; set; }
    }
}
