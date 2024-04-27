using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class LargeBuilding:AuditableEntity
    {
        public int Id { get; set; }
        public float ExtensionSquareMeters { get; set; }
        public string Location { get; set; }
        public int BuildingNumbers { get; set; }
        public int RoomNumbers { get; set; }
        public float PercentageResidentialUse { get; set; }
        public bool HasCommercialActivities { get; set; }

        public virtual Municipality Municipality { get; set; }

        public virtual InsurancePolicy InsurancePolicy { get; set; }
    }
}
