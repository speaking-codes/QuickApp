using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class TravelClassType : AuditableEntity
    {
        public byte Id { get; set; }
        public string TravelClassName { get; set; }
        public bool IsTravelClassPremium { get; set; }

        public virtual TravelMeansType TravelMeansType { get; set; }

        public IList<Travel> Travels { get; set; }
    }
}
//Autobus:

//Classe economica
//Classe premium
//Treno:

//Prima classe
//Seconda classe (o classe standard)
//Classe business
//Aereo:

//Prima classe
//Classe business
//Classe economica (o classe turistica)
//Premium economy