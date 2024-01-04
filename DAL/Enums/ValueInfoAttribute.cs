using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enums
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ValueInfoAttribute : Attribute
    {
        public string Definition;

        public ValueInfoAttribute()
        {
        }
    }
}
