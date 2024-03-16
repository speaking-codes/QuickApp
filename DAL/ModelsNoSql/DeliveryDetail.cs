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
    public class DeliveryDetail 
    {
        [BsonElement("Phone")]
        public string Phone { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("DeliveryType")]
        public string DeliveryType { get; set; }

        [BsonElement("IsPrimary")]
        public bool IsPrimary { get; set; }
    }
}
