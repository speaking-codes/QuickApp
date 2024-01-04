namespace DAL.Models
{
    public class InsuranceCompany : AuditableEntity
    {
        public int Id { get; set; }
        public string PathLogo { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
    }
}
