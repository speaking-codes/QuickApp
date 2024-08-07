﻿using DAL.BuilderModel.Interfaces;
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
    public static class HouseExstension
    {
        public static House SetExtensionSquareMeters(this House house, Random random)
        {
            house.ExtensionSquareMeters = random.Next(65, 120);
            return house;
        }
        public static House SetNumberBuildingFloors(this House house, Random random)
        {
            house.NumberBuildingFloors = random.Next(4, 7);
            return house;
        }
        public static House SetHomeFloorNumber(this House house, Random random)
        {
            house.HomeFloorNumber = random.Next(1, house.NumberBuildingFloors);
            return house;
        }
        public static House SetLocation(this House house, Random random, AdressModelTemplate adressModelTemplate)
        {
            house.Location = Utilities.GenerateFullStreetName(adressModelTemplate.StreetTypes, adressModelTemplate.StreetNames, random);
            return house;
        }
        public static House SetIsFirstHouse(this House house, Random random)
        {
            house.IsFirstHouse = (random.Next() % 2) == 0;
            return house;
        }
        public static House SetMunicipality(this House house, Random random, IList<Municipality> municipalities)
        {
            var i = random.Next(municipalities.Count);
            house.Municipality = municipalities[i];
            return house;
        }
        public static string GetHouseDescription(this House house)
        {
            var sb = new StringBuilder(string.Empty);
            sb.Append($"Immobile presso: {house.Location}");
            sb.Append($" - Nel comune: {house.Municipality.PostalCode} {house.Municipality.MunicipalityName} ({house.Municipality.Province.ProvinceAbbreviation})");
            sb.Append($" - Estensione in mq: {house.ExtensionSquareMeters}");
            sb.Append($" - Numero piani immobile: {house.NumberBuildingFloors}");
            sb.Append($" - Piano appartamento: {house.HomeFloorNumber}");

            if (house.IsFirstHouse)
                sb.Append(" - Abitazione principale");
            else
                sb.Append(" - Abitazione secondaria");

            return sb.ToString();
        }
        public static IList<string> GetHouseDescriptions(this IEnumerable<House> houses)
        {
            var houseDescriptions = new List<string>();
            foreach (var item in houses)
                houseDescriptions.Add(item.GetHouseDescription());
            return houseDescriptions;
        }
    }
}
