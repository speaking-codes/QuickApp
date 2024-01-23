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
    public class CustomerDetail : IDocument
    {
        [BsonId]
        public ObjectId Id { get; internal set; }

        [BsonElement("CustomerCode")]
        public string CustomerCode { get; set; }
        
        [BsonElement("CustomerName")]
        public string CustomerName { get; set; }

        [BsonElement("CustomerGender")]
        public string CustomerGender { get; set; }

        [BsonElement("CustomerBirthDate")]
        public string CustomerBirthDate { get; set; }

        [BsonElement("CustomerBirthPlace")]
        public string CustomerBirthPlace { get; set; }

        [BsonElement("CustomerAddressLocation")]
        public string CustomerAddressLocation { get; set; }

        [BsonElement("CustomerAddressCity")]
        public string CustomerAddressCity { get; set; }

        [BsonElement("CustomerPhone")]
        public string CustomerPhone { get; set; }
        
        [BsonElement("CustomerEmail")]
        public string CustomerEmail { get; set; }
        
        [BsonElement("CustomerJobTitle")]
        public string CustomerJobTitle { get; set; }
        
        [BsonElement("CustomerRal")]
        public string CustomerRal { get; set; }
        
        [BsonElement("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}
