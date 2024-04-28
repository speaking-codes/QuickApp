using DAL.BuilderModel.Interfaces;
using DAL.BuilderModelTemplate;
using DAL.Enums;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exstensions
{
    public static class InjuryExstension
    {
        public static Injury SetFirstNameAndGender(this Injury injury, Random random, IList<FirstNameTemplate> firstNameTemplates)
        {
            var i = random.Next(firstNameTemplates.Count);
            injury.FirstName = firstNameTemplates[i].FirstName;
            injury.Gender = firstNameTemplates[i].IsMale ? EnumGender.Uomo : EnumGender.Donna;
            return injury;
        }
        public static Injury SetLastName(this Injury injury, Random random, IList<string> lastNames)
        {
            var i = random.Next(lastNames.Count);
            injury.LastName = lastNames[i];
            return injury;
        }
        public static Injury SetBirthDate(this Injury injury, Random random)
        {
            var age = random.Next(0, 90);
            var days = random.Next(1, 365);
            injury.BirthDate = DateTime.Now.AddYears(-age).AddDays(-days);
            return injury;
        }
        public static Injury SetInjuryPrivateLife(this Injury injury, Random random)
        {
            injury.IsInjuryPrivateLife = random.Next() % 2 == 0;
            return injury;
        }
        public static Injury SetInjuryProfessionalLife(this Injury injury, Random random)
        {
            injury.IsInjuryProfessionalLife = random.Next() % 2 == 0;
            return injury;
        }
        public static Injury SetKinshipRelationshipType(this Injury injury, Random random, IList<KinshipRelationshipType> kinshipRelationshipTypes)
        {
            var i = random.Next(kinshipRelationshipTypes.Count);
            injury.KinshipRelationshipType = kinshipRelationshipTypes[i];
            return injury;
        }
    }
}
