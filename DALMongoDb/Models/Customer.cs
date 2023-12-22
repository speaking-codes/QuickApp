using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALMongoDb.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string TaxIdCode { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string BirthCounty { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Province { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }

    public enum Gender
    {
        None,
        Female,
        Male
    }
}
