using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Vehicle : AuditableEntity
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public double CommercialValue { get; set; }
        public double InsuredValue { get; set; }
        public byte RiskCategory { get; set; }

        public virtual ConfigurationModel ConfigurationModel { get; set; }

        public virtual InsurancePolicy InsurancePolicy { get; set; }
    }
}
