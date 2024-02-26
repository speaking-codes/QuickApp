using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati.Helpers
{
    public static class Utility
    {
        public static string Chars => "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string Digit => "0123456789";

        public static string GenerateRandomCode(int length, string pattern, Random random)
        {
            return new string(Enumerable.Repeat(pattern, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
