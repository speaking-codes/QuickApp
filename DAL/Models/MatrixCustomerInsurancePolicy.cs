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
        public int UserId { get; set; }
        public string InsurancePolicyCategory { get; set; }
        public bool IsLiked { get; set; }
    }
}
