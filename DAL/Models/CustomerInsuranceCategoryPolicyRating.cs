using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CustomerInsuranceCategoryPolicyRating
    {
        public long Id { get; set; }
        public string CustomerCode { get; set; }
        public byte Gender { get; set; }
        public int Age { get; set; }
        public byte MaritalStatus { get; set; }
        public byte ChildrenNumbers { get; set; }
        public string JobTitle { get; set; }
        public byte ContractType { get; set; }
        public double Ral { get; set; }
        public string InsuranceCategoryPolicyCode { get; set; }
        public int Rating { get; set; }
    }
}
