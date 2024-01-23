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
    public class CustomerHeader:IDocument
    {
        [BsonId]
        public ObjectId Id { get; internal set; }
        [BsonElement("CustomerCode")]
        public string CustomerCode { get; set; }
        [BsonElement("CustomerName")]
        public string CustomerName { get; set; }
        [BsonElement("CustomerAddress")]
        public string CustomerAddress { get; set; }
        [BsonElement("CustomerPhone")]
        public string CustomerPhone { get; set; }
        [BsonElement("CustomerEmail")]
        public string CustomerEmail { get; set; }
        [BsonElement("IsDeleted")]
        public bool IsDeleted { get; set; }
        [BsonElement("InsurancePolicies")]
        public IList<InsurancePolicyTypeChart> InsurancePolicies { get; set; }

        public CustomerHeader()
        {
            InsurancePolicies=new List<InsurancePolicyTypeChart>();
        }
    }
}
