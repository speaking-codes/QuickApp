using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class TravelInsurancePolicy : InsurancePolicy
    {
        public bool IsLuxuryPolicy { get; set; }
        public int? LuggageHeightMetres { get; set; }
        public int? LuggageLengthMetres { get; set; }
        public int? LuggageWidthMetres { get; set; }
        public int? LuggageWeightKg { get; set; }
        public EnumLuggageType? LuggageType { get; set; }
        public EnumStructureType? StructureType { get; set; }
        public string StructureName { get; set; }
        public EnumMeansTransportType? MeansTransportType { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string ParkingAddress { get; set; }
        public string ParkingCity { get; set; }
        public string ParkingPostalCode { get; set; }
        public string ParkingProvince { get; set; }
        public DateTime? DepartureDate { get; set; }
        public string DepartureCity { get; set; }
        public string DeparturePostalCode { get; set; }
        public string DepartureProvince { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string ArrivalCity { get; set; }
        public string ArrivalPostalCode { get; set; }
        public string ArrivalProvince { get; set; }
    }
}
