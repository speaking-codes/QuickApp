using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Brand : AuditableEntity
    {
        public short Id { get; set; }
        public string BrandName { get; set; }
        public bool IsActive { get; set; }

        public virtual BrandType BrandType { get; set; }

        public virtual IList<Model> Models { get; set; }
    }
}
