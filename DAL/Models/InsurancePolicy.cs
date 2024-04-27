using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class InsurancePolicy : AuditableEntity
    {
        public int Id { get; set; }
        public string InsurancePolicyCode { get; set; }
        public short Progressive { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double InsuredMaximum { get; set; }
        public double TotalPrize { get; set; }
        public bool IsLuxuryPolicy { get; set; }
        public bool IsTransferForMachineLearning { get; set; }

        public byte InsurancePolicyCategoryId { get; set; }
        public virtual InsurancePolicyCategory InsurancePolicyCategory { get; set; }

        public virtual IList<Vehicle> Vehicles { get; set; }
        public virtual IList<Pet> Pets { get; set; }
        public virtual IList<SportEvent> SportEvents { get; set; }
        public virtual IList<House> Houses { get; set; }
        public virtual IList<LegalProtection> LegalProtections { get; set; }
        public virtual IList<LargeBuilding> LargeBuildings { get; set; }
        public virtual IList<Injury> Injuries { get; set; }
        public virtual IList<Illness> Illnesses { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual IList<WarrantySelected> WarrantySelecteds { get; set; }
    }
}
