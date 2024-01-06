using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.Helpers
{
    public static class Extension
    {
        public static void AddRange<T>(this IList<T> value, IEnumerable<T> items)
        {
            if (items != null)
            {

                if (value == null)
                    value = new List<T>();

                foreach (var item in items)
                    value.Add(item);
            }
        }
    }
}
