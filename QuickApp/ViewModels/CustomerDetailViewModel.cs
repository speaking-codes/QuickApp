using MongoDB.Bson.Serialization.Attributes;
using System.Collections;
using System.Collections.Generic;

namespace QuickApp.ViewModels
{
    public class CustomerDetailViewModel
    {
        public string CustomerCode { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string MaritalStatus { get; set; }
        public string ChildrenNumber { get; set; }

        public IList<AddressDetailViewModel> Addresses { get; set; }
        public IList<DeliveryDetailViewModel> Deliveries { get; set; }
        
        public string JobTitle { get; set; }
        public string ContractType { get; set; }
        public string Income { get; set; }
    }
}
