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
        [BsonElement("Code")]
        public string CustomerCode { get; set; }
        [BsonElement("FullName")]
        public string FullName { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; }
        [BsonElement("Phone")]
        public string Phone { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        
        public CustomerHeader() { }
    }
}
