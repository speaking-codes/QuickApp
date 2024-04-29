using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SportEvent : AuditableEntity
    {
        public int Id { get; set; }
        public string SportEventTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? NumberTeams { get; set; }
        public int? NumberForTeam { get; set; }
        public int TotalNumberMembers { get; set; }
        public string Location { get; set; }
        public bool IsCompetitive { get; set; }

        public virtual SportEventType SportEventType { get; set; }

        public virtual Municipality Municipality { get; set; }

        public virtual InsurancePolicy InsurancePolicy { get; set; }
    }
}
