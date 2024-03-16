using DAL.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelsNoSql
{
    public class SalesLineChart 
    {
        [BsonElement("SalesLineCode")]
        public string SalesLineCode { get; set; }

        [BsonElement("SalesLineName")]
        public string SalesLineName { get; set; }

        [BsonElement("BackgroundColor")]
        public string BackGroundColor { get; set; }

        [BsonElement("TotalPrice")]
        public double TotalPrice { get; set; }

        [BsonElement("TotalCount")]
        public int TotalCount { get; set; }
    }
}
