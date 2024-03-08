using DAL.BuilderModelTemplate;
using DAL.Enums;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel.Interfaces
{
    public interface IFamilyInsurancePolicyBuilder:IInsurancePolicyBuilder
    {
        IFamilyInsurancePolicyBuilder SetFirstNameAndGender(IList<FirstNameTemplate> firstNameTemplates);
        IFamilyInsurancePolicyBuilder SetLastName(IList<string> lastNames);
        IFamilyInsurancePolicyBuilder SetBirthDate();
        IFamilyInsurancePolicyBuilder SetUnderage();
        IFamilyInsurancePolicyBuilder SetDisabled();
        IFamilyInsurancePolicyBuilder SetKinshipRelationshipType(IList<KinshipRelationshipType> kinshipRelationshipTypes);
    }
}
