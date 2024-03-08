using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati.Helpers
{
    public static class Utils
    {
        public static string Chars => "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string Digit => "0123456789";

        private static string generateRandomCode(string baseDictionary, int length, Random random)
        {
            return new string(Enumerable.Repeat(baseDictionary, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateRandomCode(int length, string pattern, Random random)
        {
            return new string(Enumerable.Repeat(pattern, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateLicensePlate(Random random)
        {
            return $"{generateRandomCode(Chars, 2, random)} {generateRandomCode(Digit, 3, random)} {generateRandomCode(Chars, 2, random)}";
        }
    }
}
