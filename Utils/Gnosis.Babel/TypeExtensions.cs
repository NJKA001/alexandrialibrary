using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public static class TypeExtensions
    {
        public static string AsSchemaName(this Type type)
        {
            var name = type.Name;

            if (name.Length > 1 && name.StartsWith("I"))
            {
                if (name[1].ToString() == name[1].ToString().ToUpper())
                    return name.Substring(1);
            }

            return name;
        }
    }
}
