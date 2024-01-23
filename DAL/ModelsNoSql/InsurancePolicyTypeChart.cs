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
    public class InsurancePolicyTypeChart : IDocument
    {
        [BsonId]
        public ObjectId Id { get; internal set; }
        [BsonElement("InsurancePolicyTypeId")]
        public byte InsurancePolicyTypeId { get; set; }
        [BsonElement("InsurancePolicyTypeName")]
        public string InsurancePolicyTypeName { get; set;}
        [BsonElement("BackGroundColor")]
        public string BackGroundColor { get; set; }
        [BsonElement("InsurancePolicyTypeCount")]
        public int InsurancePolicyTypeCount { get; set; }
    }
}
