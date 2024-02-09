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
    public class InsuranceCategoryPolicyTopSelling:IDocument
    {
        [BsonId]
        public ObjectId Id { get; internal set; }

        [BsonElement("Year")]
        public int Year { get; set; }

        [BsonElement("InsuranceCategoryPolicies")]
        public IList<InsuranceCategoryPolicyDashboardCard> InsuranceCategoryPolicies { get; set; }
    }
}
