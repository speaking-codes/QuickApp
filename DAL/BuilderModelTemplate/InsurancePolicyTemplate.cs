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
        public IList<ConfigurationModel> CarConfigurationModels { get; set; }
        public IList<ConfigurationModel> BykeConfigurationModels { get; set; }
        public IList<Municipality> Municipalities { get; set; }
        public IList<TravelMeansType> TravelMeansTypes { get; set; }
        public IList<TravelClassType> TravelClassTypes { get; set; }
        public IList<StructureType> StructureTypes { get; set; }
        public IList<ProfessionType> ProfessionTypes { get; set; }
        public IList<BaggageType> BaggageTypes { get; set;}
        public IList<KinshipRelationshipType> KinshipRelationshipTypes { get;set; }
        public IList<BreedPetDetailType> BreedPetDetailTypes { get; set; }

    }
}
