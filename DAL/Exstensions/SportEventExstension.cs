using DAL.BuilderModelTemplate;
using DAL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exstensions
{
    public static class SportEventExstension
    {
        public string SportEventTitle { get; set; }
        public static SportEvent SetStartDate(this SportEvent sportEvent, Random random)
        {
            var days = random.Next(10, 60);
            sportEvent.StartDate = DateTime.Now.AddDays(days);
            return sportEvent;
        }
        public static SportEvent SetEndDate(this SportEvent sportEvent, Random random)
        {
            var days = random.Next(1, 60);
            sportEvent.EndDate = sportEvent.StartDate.AddDays(days);
            return sportEvent;
        }
        public static SportEvent SetSportEventType(this SportEvent sportEvent, Random random, IList<SportEventType> sportEventTypes)
        {
            var index = random.Next(sportEventTypes.Count);
            sportEvent.SportEventType = sportEventTypes[index];
            return sportEvent;
        }
        public static SportEvent SetMunicipality(this SportEvent sportEvent, Random random, IList<Municipality> municipalities)
        {
            var index = random.Next(municipalities.Count);
            sportEvent.Municipality = municipalities[index];
            return sportEvent;
        }
        public int? NumberTeams { get; set; }
        public int? NumberForTeam { get; set; }
        public int TotalNumberMembers { get; set; }

        public static SportEvent SetNumberMembers(this SportEvent sportEvent, Random random)
        {
            if (sportEvent.SportEventType != null && sportEvent.SportEventType.IsTeamCompetition && sportEvent.SportEventType.MaxNumberMembers.HasValue)
                sportEvent.NumberMembers = random.Next(sportEvent.SportEventType.MaxNumberTeams, sportEvent.SportEventType.MaxNumberMembers.Value);
            else
                sportEvent.NumberMembers = random.Next(maxValue: sportEvent.SportEventType.MaxNumberTeams);
            return sportEvent;
        }
        public static SportEvent SetLocation(this SportEvent sportEvent, Random random, AdressModelTemplate adressModelTemplate)
        {
            sportEvent.Location = Utilities.GenerateFullStreetName(adressModelTemplate.StreetTypes, adressModelTemplate.StreetNames, random);
            return sportEvent;
        }
        public static SportEvent SetIsCompetitive(this SportEvent sportEvent, Random random)
        {
            sportEvent.IsCompetitive = (random.Next() % 2) == 0;
            return sportEvent;
        }
    }
}
