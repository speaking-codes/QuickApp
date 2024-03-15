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

        [BsonElement("Code")]
        public string CustomerCode { get; set; }

        [BsonElement("FullName")]
        public string FullName { get; set; }

        [BsonElement("Gender")]
        public string Gender { get; set; }

        [BsonElement("BirthDate")]
        public string BirthDate { get; set; }

        [BsonElement("BirthPlace")]
        public string BirthPlace { get; set; }

        [BsonElement("FamilyDescription")]
        public string FamilyDescription { get; set; }

        [BsonElement("MaritalStatus")]
        public string MaritalStatus { get; set; }

        [BsonElement("ChildrenNumber")]
        public string ChildrenNumber { get; set; }

        [BsonElement("AddressDetails")]
        public IList<AddressDetail> AddressDetails { get; set; }

        [BsonElement("DeliveryDetails")]
        public IList<DeliveryDetail> DeliveryDetails { get; set; }

        [BsonElement("JobTitle")]
        public string JobTitle { get; set; }

        [BsonElement("IsFrelancer")]
        public bool IsFrelancer { get; set; }

        [BsonElement("ContractType")]
        public string ContractTitle { get; set; }

        [BsonElement("Income")]
        public string Income { get; set; }
    }
}
