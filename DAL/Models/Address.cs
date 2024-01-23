using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Address : AuditableEntity
    {
        public int Id { get; set; }
        public EnumAddressType AddressType { get; set; }    
        public string Location { get; set; }
        
        public virtual Municipality Municipality { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
