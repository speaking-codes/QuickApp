using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ConfigurationModel : AuditableEntity
    {
        public int Id { get; set; }
        public string ConfigurationDescription { get; set; }
        public int PowerTax { get; set; }
        public int PowerCV { get; set; }
        public int PowerKW { get; set; }
        public int Displacement { get; set; }
        public int PowerRpmMin { get; set; }
        public int CylindersNumber { get; set; }
        public int ValvesCylinderNumber { get; set; }
        public bool Catalyzed { get; set; }
        public int GearsNumber { get; set; }
        public int FullSpeed { get; set; }
        public double Acceleration_0_100 { get; set; }
        public int DoorsNumber { get; set; }
        public int SeatsNumber { get; set; }
        public double LengthMeters { get; set; }
        public double WidthMeters { get; set; }
        public double HeightMeters { get; set; }
        public double StepMeters { get; set; }
        public double CurbMassKg { get; set; }
        public double PowerToMassRatio { get; set; }
        public double Consumption { get; set; }
        public double TankCapacity { get; set; }
        public double LuggageCapacity { get; set; }

        public virtual ModelType ModelType { get; set; }
        public virtual Model Model { get; set; }//Modello
        public virtual PowerType PowerType { get; set; }//Tipo Combustibile
        //public virtual CarArrangementCylinderType ArrangementCylinderType { get; set; }//Disposizione Cilindri
        //public virtual CarTractionType TractionType { get; set; }//Trazione
        //public virtual GearboxType GearboxType { get; set; }//Tipo Cambio

        public virtual IList<VehicleInsurancePolicy> VehicleInsurancePolicies { get; set; }
    }
}
