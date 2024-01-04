using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enums
{
    public static class ValueInfoAttributeExtension
    {
        public static string GetDefinition(this object enumValue)
        {
            if (enumValue != null)
            {
                MemberInfo[] mi = enumValue.GetType().GetMember(enumValue.ToString());
                if (mi != null && mi.Length > 0)
                {
                    ValueInfoAttribute attr = Attribute.GetCustomAttribute(mi[0], typeof(ValueInfoAttribute)) as ValueInfoAttribute;
                    if (attr != null)
                    {
                        return attr.Definition;
                    }
                }
            }
            return string.Empty;
        }
    }
}
