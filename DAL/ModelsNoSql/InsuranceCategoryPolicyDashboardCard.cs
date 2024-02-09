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
    public class InsuranceCategoryPolicyDashboardCard : IDocument
    {
        [BsonId]
        public ObjectId Id { get; internal set; }

        [BsonElement("Code")]
        public string Code { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Total")]
        public int Total { get; set; }

        [BsonElement("Abstract")]
        public string Abstract { get; set; }

        [BsonElement("IconCssClass")]
        public string IconCssClass { get; set; }

        [BsonElement("SalesLineCode")]
        public string SalesLineCode { get; set; }

        [BsonElement("SalesLineName")]
        public string SalesLineName { get; set; }

        [BsonElement("SalesLineBackgroundColor")]
        public string SalesLineBackgroundColor { get; set; }

        [BsonElement("SalesLineBackgroundCssClass")]
        public string SalesLineBackgroundCssClass { get; set; }

    }
}
