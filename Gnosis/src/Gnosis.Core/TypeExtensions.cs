using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public static TableAttribute GetTableAttribute(this Type type)
        {
            foreach (var attribute in type.GetCustomAttributes(true))
            {
                if (attribute is TableAttribute)
                {
                    return attribute as TableAttribute;
                }
            }

            return null;
        }

        public static DefaultSortAttribute GetDefaultSortAttribute(this Type type)
        {
            foreach (var attribute in type.GetCustomAttributes(true))
            {
                if (attribute is DefaultSortAttribute)
                {
                    return attribute as DefaultSortAttribute;
                }
            }

            return null;
        }

        public static IEnumerable<IndexAttribute> GetIndexAttributes(this Type type)
        {
            var indexAttributes = new List<IndexAttribute>();

            foreach (var attribute in type.GetCustomAttributes(true))
            {
                if (attribute is IndexAttribute)
                {
                    indexAttributes.Add(attribute as IndexAttribute);
                }
            }

            return indexAttributes;
        }

        public static IEnumerable<Tuple<PropertyInfo, OneToManyAttribute>> GetOneToManyAttributes(this Type type)
        {
            var oneToManyAttributes = new List<Tuple<PropertyInfo, OneToManyAttribute>>();

            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is OneToManyAttribute)
                        oneToManyAttributes.Add(new Tuple<PropertyInfo, OneToManyAttribute>(property, attribute as OneToManyAttribute));
                }
            }

            return oneToManyAttributes;
        }

        public static bool IsCustomDataType(this Type type)
        {
            foreach (var attribute in type.GetCustomAttributes(true))
            {
                if (attribute is CustomDataTypeAttribute)
                    return true;
            }

            return false;
        }

        public static bool IsEntityType(this Type type)
        {
            return typeof(IEntity).IsAssignableFrom(type);
        }

        public static bool IsValueType(this Type type)
        {
            return typeof(IValue).IsAssignableFrom(type);
        }

        public static bool IsTimeStampType(this Type type)
        {
            return type == typeof(ITimeStamp);
        }

        public static bool IsOrderedCollectionType(this Type type)
        {
            return type.Name == "IOrderedSet`1";
        }

        public static bool IsUnorderedCollectionType(this Type type)
        {
            return type.Name == "ISet`1";
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
