using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SportEventType : AuditableEntity
    {
        public byte Id { get; set; }
        public string SportEventTypeName { get; set; }
        public bool IsTeamCompetition { get; set; }

        public virtual IList<SportEvent> SportEvents { get; set; }
    }
}
