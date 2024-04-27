using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModelTemplate
{
    public class InsurancePolicyTemplate
    {
        public IList<string> LastNames { get; set; }
        public IList<FirstNameTemplate> FirstNameTemplates { get; set; }
        public IList<string> PetNames { get; set; }
        public AdressModelTemplate AddressTemplate { get; set; }
        public IList<InsurancePolicyCategory> InsurancePolicyCategories { get; set; }
        public IList<ConfigurationModel> ConfigurationModels { get; set; }
        public IList<Municipality> Municipalities { get; set; }
        public IList<SportEventType> SportEventTypes { get; set; }
        public IList<GenderType> GenderTypes { get; set; }
        public IList<IncomeType> IncomeTypes { get; set; }
        public IList<IncomeClassType> IncomeClassType { get; set; }
        public IList<ProfessionType> ProfessionTypes { get; set; }
        public IList<KinshipRelationshipType> KinshipRelationshipTypes { get;set; }
        public IList<BreedPetDetailType> BreedPetDetailTypes { get; set; }

    }
}
