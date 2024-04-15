using MongoDB.Bson.Serialization.Attributes;

namespace QuickApp.ViewModels
{
    public class DeliveryDetailViewModel
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DeliveryType { get; set; }
        public bool IsPrimary { get; set; }
    }
}
