using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Customer : AuditableEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string TaxIdCode { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string BirthCounty { get; set; }
        public EnumGender Gender { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual IList<Delivery> Deliveries { get; set; }

        public virtual IList<Address> Addresses { get; set; }
    }
}
