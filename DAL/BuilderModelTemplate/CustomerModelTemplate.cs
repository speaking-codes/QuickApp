using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModelTemplate
{
    public class CustomerModelTemplate
    {
        public IList<string> LastNames { get; set; }
        public IList<FirstNameTemplate> FirstNameTemplates { get; set; }
        public AdressModelTemplate AddressTemplate { get; set; }
        public DeliveryModelTemplate DeliveryModelTemplate { get; set; }
        public IList<double> Incomes { get; set; }
        public virtual IList<FamilyType> FamilyTypes { get; set; }
        public virtual IList<MaritalStatusType> MaritalStatuses { get; set; }
        public virtual IList<Municipality> BirthMunicipalities { get; set; }
        public virtual IList<ContractType> ContractTypes { get; set; }
        public virtual IList<IncomeType> IncomeTypes { get; set; }
        public virtual IList<ProfessionType> ProfessionTypes { get; set; }
    }
}
