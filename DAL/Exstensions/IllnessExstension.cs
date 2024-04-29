﻿using DAL.BuilderModelTemplate;
using DAL.Enums;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exstensions
{
    public static class IllnessExstension
    {
        public static Illness SetFirstNameAndGender(this Illness illness, Random random, IList<FirstNameTemplate> firstNameTemplates)
        {
            var i = random.Next(firstNameTemplates.Count);
            illness.FirstName = firstNameTemplates[i].FirstName;
            illness.Gender = firstNameTemplates[i].IsMale ? EnumGender.Uomo : EnumGender.Donna;
            return illness;
        }
        public static Illness SetLastName(this Illness illness, Random random, IList<string> lastNames)
        {
            var i = random.Next(lastNames.Count);
            illness.LastName = lastNames[i];
            return illness;
        }
        public static Illness SetBirthDate(this Illness illness, Random random)
        {
            var age = random.Next(0, 90);
            var days = random.Next(1, 365);
            illness.BirthDate = DateTime.Now.AddYears(-age).AddDays(-days);
            return illness;
        }
        public static Illness SetKinshipRelationshipType(this Illness illness, Random random, IList<KinshipRelationshipType> kinshipRelationshipTypes)
        {
            var i = random.Next(kinshipRelationshipTypes.Count);
            illness.KinshipRelationshipType = kinshipRelationshipTypes[i];
            return illness;
        }
    }
}
