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
    public static class LargeBuildingExtension
    {
        public static LargeBuilding SetLocation(this LargeBuilding largeBuilding, Random random, AdressModelTemplate adressModelTemplate)
        {
            largeBuilding.Location = Utilities.GenerateFullStreetName(adressModelTemplate.StreetTypes, adressModelTemplate.StreetNames, random);
            return largeBuilding;
        }
        public static LargeBuilding SetBuildingNumbers(this LargeBuilding largeBuilding, Random random)
        {
            largeBuilding.BuildingNumbers = random.Next(5, 20);
            return largeBuilding;
        }
        public static LargeBuilding SetRoomNumbers(this LargeBuilding largeBuilding, Random random)
        {
            var roomsForApartment = random.Next(2, 5);
            var floorsForBuilding = random.Next(2, 7);
            largeBuilding.RoomNumbers = roomsForApartment * floorsForBuilding * largeBuilding.BuildingNumbers;
            return largeBuilding;
        }
        public static LargeBuilding SetExtensionSquareMeters(this LargeBuilding largeBuilding, Random random)
        {
            largeBuilding.ExtensionSquareMeters = random.Next(10, 30) * largeBuilding.RoomNumbers;
            return largeBuilding;
        }
        public static LargeBuilding SetPercentageResidentialUse(this LargeBuilding largeBuilding, Random random)
        {
            largeBuilding.PercentageResidentialUse = random.Next(10, 100);
            return largeBuilding;
        }
        public static LargeBuilding SetHasCommercialActivities(this LargeBuilding largeBuilding, Random random)
        {
            if (largeBuilding.PercentageResidentialUse >= 100)
                largeBuilding.HasCommercialActivities = false;
            else
                largeBuilding.HasCommercialActivities = (random.Next() % 2) == 0;
            return largeBuilding;
        }
        public static LargeBuilding SetCommercialActivityNumbers(this LargeBuilding largeBuilding, Random random)
        {
            if (!largeBuilding.HasCommercialActivities)
            {
                largeBuilding.CommercialActivityNumbers = 0;
                return largeBuilding;
            }

            var percentageCommecialActivities = (100 - largeBuilding.PercentageResidentialUse) * 0.15;
            largeBuilding.CommercialActivityNumbers = (int)Math.Ceiling(largeBuilding.ExtensionSquareMeters * percentageCommecialActivities);
            return largeBuilding;
        }
        public static LargeBuilding SetMunicipality(this LargeBuilding largeBuilding, Random random, IList<Municipality> municipalities)
        {
            var index = random.Next(municipalities.Count);
            largeBuilding.Municipality = municipalities[index];
            return largeBuilding;
        }
    }
}
