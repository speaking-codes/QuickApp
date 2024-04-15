using MongoDB.Bson.Serialization.Attributes;

namespace QuickApp.ViewModels
{
    public class AddressDetailViewModel
    {
        public string Location { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string AddressType { get; set; }
        public bool IsPrimary { get; set; }
    }
}
