using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BreedPetDetailType:AuditableEntity
    {
        public short Id { get; set; }
        public string BreedPetDetailTypeDescription { get; set; }

        public virtual BreedPetType BreedPetType { get; set; }
    }
}
