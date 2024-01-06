using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.Helpers
{
    public static class Utility
    {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private static string generateRandomCode(int length)
        {
            var rnd = new Random(42);
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        public static string GenerateCustomerCode(string firstName, string lastName, int progressive)
        {
            var preCode = firstName.Substring(0, 1) + lastName.Substring(0,1);
            var length = 16 - preCode.Length - progressive.ToString().Length - 2;
            return $"{preCode}-{generateRandomCode(length)}-{progressive}".ToUpper();
        }
    }
}
