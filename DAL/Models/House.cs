using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class House:AuditableEntity
    {
        public int Id { get; set; }

        public float ExtensionSquareMeters { get; set; }

        public int NumberBuildingFloors { get; set; }

        public int HomeFloorNumber { get; set; }

        public string Location { get; set; }

        public bool IsFirstHouse { get; set; }

        public virtual Municipality Municipality { get; set; }

        public virtual InsurancePolicy InsurancePolicy { get; set; }
    }
}
