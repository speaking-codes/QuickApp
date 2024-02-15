using DAL.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelsNoSql
{
    public class InsuranceCoverageGrid : IDocument
    {
        [BsonId]
        public ObjectId Id { get; internal set; }

        [BsonElement("Code")]
        public string Code { get; set; }

        [BsonElement("CategoryType")]
        public string CategoryType { get; set; }

        [BsonElement("ItemDescription")]
        public string ItemDescription { get; set; }

        [BsonElement("IssueDate")]
        public string IssueDate { get; set; }

        [BsonElement("ExpiryDate")]
        public string ExpiryDate { get; set; }

        [BsonElement("TotalPrice")]
        public string TotalPrice { get; set; }
    }
}
