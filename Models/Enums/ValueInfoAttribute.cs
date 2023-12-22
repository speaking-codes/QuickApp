using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Enums
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ValueInfoAttribute : Attribute
    {
        public string Name;

        public ValueInfoAttribute()
        {
        }
    }
}
