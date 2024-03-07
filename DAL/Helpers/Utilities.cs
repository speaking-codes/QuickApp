using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    public static class Utilities
    {
        private const string charsNumber = "0123456789";

        public static string GetEmail(string provider, string lastName, string firstName)
        {
            lastName = Regex.Replace(lastName.Trim().ToLower(), "^[a-z0-9]$", string.Empty);
            firstName = Regex.Replace(firstName.Trim().ToLower(), "^[a-z0-9]$", string.Empty);
            return $"{provider}{lastName.Substring(0, 2)}{firstName.Substring(0, 2)}";
        }

        public static string GetPhoneNumber(Random random)
        {
            return new string(Enumerable.Repeat(charsNumber, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
