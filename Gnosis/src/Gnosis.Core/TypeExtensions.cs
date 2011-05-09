using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Attributes;

namespace Gnosis.Core
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Get the SQLIte type affinity of the given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>TypeAffinity</returns>
        /// <remarks>http://sqlite.org/datatype3.html#affinity</remarks>
        public static TypeAffinity GetTypeAffinity(this Type type)
        {
            if (type.IsIntegerColumn())
                return TypeAffinity.Integer;
            else if (type.IsTextColumn())
                return TypeAffinity.Text;
            else if (type.IsBlobColumn())
                return TypeAffinity.None;
            else if (type.IsRealColumn())
                return TypeAffinity.Real;
            else
                return TypeAffinity.Numeric;
        }

        public static bool IsDataTypeColumn(this Type type)
        {
            foreach (var attribute in type.GetCustomAttributes(true))
            {
                if (attribute is DataTypeAttribute)
                    return true;
            }

            return false;
        }

        public static bool IsTextColumn(this Type type)
        {
            if (type == typeof(string))
                return true;
            else if (type == typeof(DateTime))
                return true;
            else if (type == typeof(Guid))
                return true;
            else if (type == typeof(Uri))
                return true;

            return false;
        }

        public static bool IsIntegerColumn(this Type type)
        {
            if (type == typeof(bool))
                return true;
            else if (type == typeof(byte))
                return true;
            else if (type == typeof(sbyte))
                return true;
            else if (type == typeof(short))
                return true;
            else if (type == typeof(ushort))
                return true;
            else if (type == typeof(int))
                return true;
            else if (type == typeof(uint))
                return true;
            else if (type == typeof(long))
                return true;
            else if (type == typeof(ulong))
                return true;

            return false;
        }

        public static bool IsRealColumn(this Type type)
        {
            if (type == typeof(decimal))
                return true;
            else if (type == typeof(float))
                return true;
            else if (type == typeof(double))
                return true;

            return false;
        }

        public static bool IsBlobColumn(this Type type)
        {
            if (type == typeof(byte[]))
                return true;

            return false;
        }
    }
}
