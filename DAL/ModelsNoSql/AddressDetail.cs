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
    public class AddressDetail
    {
        [BsonElement("Location")]
        public string Location { get; set; }

        [BsonElement("City")]
        public string City { get; set; }

        [BsonElement("PostalCode")]
        public string PostalCode { get; set; }

        [BsonElement("Country")]
        public string Country { get; set; }

        [BsonElement("AddressType")]
        public string AddressType { get; set; }

        [BsonElement("IsPrimary")]
        public bool IsPrimary { get; set; }
    }
}
