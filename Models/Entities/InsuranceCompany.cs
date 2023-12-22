using Models.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class InsuranceCompany : AuditableEntity
    {
        public int Id { get; set; }
        public string PathLogo { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
    }
}
