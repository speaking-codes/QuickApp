using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.Helpers
{
    public static class Extension
    {
        public static void AddRange<T>(this IList<T> value, IEnumerable<T> items)
        {
            if (items != null)
            {

                if (value == null)
                    value = new List<T>();

                foreach (var item in items)
                    value.Add(item);
            }
        }

        public static string GetItemDescription(this IEnumerable<Travel> travels)
        {
            var itemDescription = new StringBuilder(string.Empty);

            foreach(var item in travels)
            {
                itemDescription.AppendLine($"Partenza da: {item.DepartureMunicipality.MunicipalityName} in data: {item.DepartureDate.ToString("dd/MM/yyyy")} - ");
                itemDescription.AppendLine($"Arrivo a: {item.ArrivalMunicipality.MunicipalityName} in data: {item.ArrivalDate.ToString("dd/MM/yyyy")}");
                itemDescription.AppendLine($"Viaggio con: {item.TravelMeansType.TravelMeansTypeName} in: {item.TravelClassType.TravelClassName}");
                itemDescription.AppendLine();
            }
            return itemDescription.ToString();
        }

        public static string GetItemDescription(this IEnumerable<Vacation> vacations)
        {
            var itemDescription = new StringBuilder(string.Empty);

            foreach (var item in vacations) {
                itemDescription.Append($"Arrivo: {item.CheckInDate.ToString("dd/MM/yyyy")} - Partenza: {item.CheckOutDate.ToString("dd/MM/yyyy")}");
                itemDescription.AppendLine();
            }

            return itemDescription.ToString();
        }
    }
    }
