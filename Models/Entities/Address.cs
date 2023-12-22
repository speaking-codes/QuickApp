using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Address : AuditableEntity
    {
        public int Id { get; set; }
        public EnumAddressType AddressType { get; set; }
        public string Location { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
