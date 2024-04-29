using DAL.BuilderModelTemplate;
using DAL.Enums;
using DAL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exstensions
{
    public static class LegalProtectionExstension
    {
        public static LegalProtection SetFirstNameAndGender(this LegalProtection legalProtection, Random random, IList<FirstNameTemplate> firstNameTemplates)
        {
            var i = random.Next(firstNameTemplates.Count);
            legalProtection.FirstName = firstNameTemplates[i].FirstName;
            legalProtection.Gender = firstNameTemplates[i].IsMale ? EnumGender.Uomo : EnumGender.Donna;
            return legalProtection;
        }
        public static LegalProtection SetLastName(this LegalProtection legalProtection, Random random, IList<string> lastNames)
        {
            var i = random.Next(lastNames.Count);
            legalProtection.LastName = lastNames[i];
            return legalProtection;
        }
        public static LegalProtection SetBirthDate(this LegalProtection legalProtection, Random random)
        {
            var age = random.Next(0, 90);
            var days = random.Next(1, 365);
            legalProtection.BirthDate = DateTime.Now.AddYears(-age).AddDays(-days);
            return legalProtection;
        }
        public static LegalProtection SetKinshipRelationshipType(this LegalProtection legalProtection, Random random, IList<KinshipRelationshipType> kinshipRelationshipTypes)
        {
            var i = random.Next(kinshipRelationshipTypes.Count);
            legalProtection.KinshipRelationshipType = kinshipRelationshipTypes[i];
            return legalProtection;
        }
        public static LegalProtection SetIsDisabled(this LegalProtection legalProtection, Random random)
        {
            legalProtection.IsDisabled = random.Next() % 2 == 0;
            return legalProtection;
        }
        public static LegalProtection SetHasLegalProtectionPrivateLife(this LegalProtection legalProtection, Random random)
        {
            legalProtection.HasLegalProtectionPrivateLife = random.Next() % 2 == 0;
            return legalProtection;
        }
        public static LegalProtection SetHasLegalProtectionProfessionalLife(this LegalProtection legalProtection, Random random)
        {
            legalProtection.HasLegalProtectionProfessionalLife = random.Next() % 2 == 0;
            return legalProtection;
        }
        public static string GetLegalProtectionDescription(this LegalProtection legalProtection)
        {
            var sb = new StringBuilder(string.Empty);
            sb.Append($"Nominativo: {legalProtection.LastName} {legalProtection.FirstName}");

            if (legalProtection.Gender == EnumGender.Uomo)
                sb.Append($" - Nato il: {legalProtection.BirthDate.ToString("dd/MM/yyyy")}");
            else
                sb.Append($" - Nata il: {legalProtection.BirthDate.ToString("dd/MM/yyyy")}");
            sb.Append($" - Relazione: {legalProtection.KinshipRelationshipType.KinshipRelationshipTypeName}");

            if (Utilities.GetAge(legalProtection.BirthDate) < 18)
                sb.Append(" - Tutela giuridica per minore");

            if (legalProtection.IsDisabled)
                sb.Append(" - Tutela giuridica per disabile");

            if (legalProtection.HasLegalProtectionPrivateLife)
                sb.Append(" - Tutela giuridica nella vita privata");

            if (legalProtection.HasLegalProtectionProfessionalLife)
                sb.Append(" - Tutela giuridica nella sfera professionale");

            return sb.ToString();
        }
        public static IList<string> GetLegalProtectionDescriptions(this IEnumerable< LegalProtection> legalProtections)
        {
            var legalProtectionDescriptions=new List<string>();
            foreach (var item in legalProtections)
                legalProtectionDescriptions.Add(item.GetLegalProtectionDescription());
            return legalProtectionDescriptions;
        }
    }
}
