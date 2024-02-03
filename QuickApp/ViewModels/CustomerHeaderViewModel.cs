using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace QuickApp.ViewModels
{
    public class CustomerHeaderViewModel
    {
        public string CustomerCode { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
