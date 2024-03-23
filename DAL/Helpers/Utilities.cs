using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAL.Helpers
{
    public static class Utilities
    {
        private static IList<string> separators => new List<string> { "", ".", "-", "_" };
        private static string CharsAndDigits => "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string Chars => "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string Digits => "0123456789";

        private static string generateRandomCode(string baseDictionary, int length, Random random) =>
            new string(Enumerable.Repeat(baseDictionary, length).Select(s => s[random.Next(s.Length)]).ToArray());

        public static string GetEmail(string provider, string lastName, string firstName, Random random)
        {
            lastName = Regex.Replace(lastName.Trim().ToLower(), "[^a-z0-9]", string.Empty);
            firstName = Regex.Replace(firstName.Trim().ToLower(), "[^a-z0-9]", string.Empty);
            var i = random.Next(separators.Count);
            var substringLength = random.Next(1, firstName.Length);
            return $"{firstName.Substring(0, substringLength)}{separators[i]}{lastName}{provider}";
        }

        public static string GetPhoneNumber(Random random) =>
            new string(Enumerable.Repeat(Digits, 10).Select(s => s[random.Next(s.Length)]).ToArray());

        public static string GenerateLicensePlate(Random random) =>
            $"{generateRandomCode(Chars, 2, random)} {generateRandomCode(Digits, 3, random)} {generateRandomCode(Chars, 2, random)}";

        public static string GeneratePetIdentificationCode(Random random) =>
            new string(Enumerable.Repeat(CharsAndDigits, 12).Select(s => s[random.Next(s.Length)]).ToArray());

        public static string GenerateFullStreetName(IList<string> streetTypeNames, IList<string> streetNames, Random random)
        {
            var i = random.Next(streetTypeNames.Count);
            var j = random.Next(streetNames.Count);
            var houseNumber = random.Next(1, 599);
            return $"{streetTypeNames[i]} {streetNames[j]}, {houseNumber}";
        }

        public static int GetAge(this DateTime birthDate) =>
            (int)(Math.Ceiling(DateTime.Now.Subtract(birthDate).TotalDays/365));
    }
}
