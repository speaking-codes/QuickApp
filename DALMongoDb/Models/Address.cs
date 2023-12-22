using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALMongoDb.Models
{
    public class Address
    {
        public int IdAddress { get; set; }
        public string Location { get; set; }
        public string HouseNumbers { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Province { get; set; }
    }
}
