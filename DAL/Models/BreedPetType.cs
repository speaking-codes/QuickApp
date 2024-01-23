using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BreedPetType:AuditableEntity
    {
        public short Id { get; set; }
        public string BreedPetTypeDescription { get; set; }

        public virtual PetType PetType { get; set; }

        public virtual IList<BreedPetDetailType> BreedPetDetailTypes { get; set; }
    }
}
