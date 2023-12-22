using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALMongoDb.Models
{
    public class Delivery
    {
        public int IdDelivery { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
