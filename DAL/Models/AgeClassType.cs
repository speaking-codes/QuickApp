using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AgeClassType
    {
        public byte Id { get; set; }

        public string NameAgeClass { get; set; }

        public string DescriptionAgeClass { get; set; }

        public byte MinAge { get; set; }

        public byte? MaxAge { get; set; }
    }
}
