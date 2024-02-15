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
    public class InsuranceCoverageChart: IDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("CustomerCode")]
        public string CustomerCode { get; set; }

        [BsonElement("SalesLines")]
        public IList<SalesLineChart> SalesLineCharts { get; set; }
    }
}
