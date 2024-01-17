using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BrandType : AuditableEntity
    {
        public byte Id { get; set; }
        public string BrandTypeDescription { get; set; }
        public bool IsLuxury { get; set; }
        public bool IsByke { get; set; }
        public bool IsActive { get; set; }

        public virtual IList<Brand> Brands { get; set; }
    }
}
