using MongoDB.Bson.Serialization.Attributes;

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
        public string AddressLocation { get; set; }
        public string AddressCity { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }    
        public string JobTitle { get; set; }
        public string ContractType { get; set; }
        public string Income { get; set; }
    }
}
