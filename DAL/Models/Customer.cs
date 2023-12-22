// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using DAL.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Models
{
    public class Customer : AuditableEntity
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
}
