using DAL.BuilderModelTemplate;
using DAL.Enums;
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
        public static SportEvent SetSportEventTitle(this SportEvent sportEvent, Random random, IList<string> sportEventTitles)
        {
            var index = random.Next(sportEventTitles.Count);
            sportEvent.SportEventTitle= sportEventTitles[index];
            return sportEvent;
        }
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
        public static string GetSportEventDescription(this SportEvent sportEvent)
        {
            var sb = new StringBuilder(string.Empty);
            sb.Append($"Nome evento: {sportEvent.SportEventTitle}");
            sb.Append($" - tipo evento: {sportEvent.SportEventType.SportEventTypeName}");
            sb.Append($" - presso: {sportEvent.Location}");
            sb.Append($" - nel comune: {sportEvent.Municipality.PostalCode} {sportEvent.Municipality.MunicipalityName} ({sportEvent.Municipality.Province.ProvinceAbbreviation})");
            sb.Append($" - nel periodo da: {sportEvent.StartDate.ToString("dd/MM/yyyy")} - a: {sportEvent.EndDate.ToString("dd/MM/yyyy")}");
            if (sportEvent.IsCompetitive)
                sb.Append(" - manifestazione agonistica");
            else
                sb.Append(" - manifestazione amatoriale");
            return sb.ToString();
        }
        public static IList<string> GetSportEventDescriptions(this IEnumerable<SportEvent> sportEvents)
        {
            var sportEventDescriptions = new List<string>();
            foreach (var item in sportEvents)
                sportEventDescriptions.Add(item.GetSportEventDescription());
            return sportEventDescriptions;
        }

    }
}
