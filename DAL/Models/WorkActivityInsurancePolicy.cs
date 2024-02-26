using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WorkActivityInsurancePolicy : InsurancePolicy
    {
        public string VatNumber { get; set; }

        public virtual Municipality Municipality { get; set; }

        public virtual ProfessionType ProfessionType { get; set; }
    }
}
