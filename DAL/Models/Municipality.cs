﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Municipality : AuditableEntity
    {
        public short Id { get; set; }
        public string MunicipalityName { get; set; }
        public string PostalCode { get; set; }
        public bool HasAirport { get; set; }

        public virtual Province Province { get; set; }

        public virtual IList<Customer> Customers { get; set; }

        public virtual IList<Address> Addresses { get; set; }

        public virtual IList<SportEvent> SportEvents { get; set; }

        public virtual IList<LargeBuilding> LargeBuildings { get; set; }

        public virtual IList<House> Houses { get; set; }
    }
}
