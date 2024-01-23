using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PetType:AuditableEntity
    {
        public byte Id { get; set; }
        public string PetTypeDescription { get; set; }

        public virtual IList<BreedPetType> BreedPetTypes { get; set;}
    }
}
