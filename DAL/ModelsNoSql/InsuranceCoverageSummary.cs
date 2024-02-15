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
    public class InsuranceCoverageSummary: IDocument
    {
        [BsonId]
        public ObjectId Id { get; internal set; }

        [BsonElement("CustomerCode")]
        public string CustomerCode { get; set; }

        [BsonElement("InsuranceCoverageGrids")]
        public IList<InsuranceCoverageGrid> InsuranceCoverageGrids { get; set; }

        public InsuranceCoverageSummary()
        {
            this.InsuranceCoverageGrids = new List<InsuranceCoverageGrid>();
        }
    }
}
