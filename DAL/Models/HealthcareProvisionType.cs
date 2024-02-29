using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class HealthcareProvisionType:AuditableEntity
    {
        public byte Id { get; set; }
        public string HealthcareProvisionName { get; set; }

        //public IList<HealthInsurancePolicy> InsurancePolicy { get; set;}
    }
}
