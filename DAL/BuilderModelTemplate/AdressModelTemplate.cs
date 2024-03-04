using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModelTemplate
{
    public class AdressModelTemplate
    {
        public readonly IList<string> StreetTypes;
        public IList<string> StreetNames { get; set; }
        public IList<Municipality> Municipalities { get; set; }

        public AdressModelTemplate()
        {
            StreetTypes = new List<string> { "Via", "Viale", "Piazza", "Corso" };
        }
    }
}
