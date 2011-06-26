using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Gnosis.Core.Ietf;
using Gnosis.Core.Iso;
using Gnosis.Core.UN;

namespace Gnosis.Core
{
    public static class TypeExtensions
    {
        #region Private Helper Methods

        public static bool IsTextType(this Type type)
        {
            if (type.IsEntityType())
                return true;
            else if (type == typeof(string))
                return true;
            else if (type == typeof(DateTime))
                return true;
            else if (type == typeof(Guid))
                return true;
            else if (type == typeof(Uri))
                return true;
            else if (type == typeof(ILanguage))
                return true;
            else if (type == typeof(ILanguageTag))
                return true;
            else if (type == typeof(ICountry))
                return true;
            else if (type == typeof(IRegion))
                return true;

            var args = type.GetGenericArguments();
            if (args.Length > 0)
            {
                return args[0].IsTextType();
            }

            return false;
        }

        public static bool IsIntegerType(this Type type)
        {
            if (type.IsEnum)
                return true;
            else if (type == typeof(bool))
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
            else if (type == typeof(TimeSpan))
                return true;

            return false;
        }

        public static bool IsRealType(this Type type)
        {
            if (type == typeof(decimal))
                return true;
            else if (type == typeof(float))
                return true;
            else if (type == typeof(double))
                return true;

            return false;
        }

        public static bool IsBlobType(this Type type)
        {
            if (type == typeof(byte[]))
                return true;

            return false;
        }

        #endregion

        public static string ToPersistentName(this Type type)
        {
            if (type.IsInterface)
            {
                if (type.Name.StartsWith("I") && type.Name.Length > 1)
                    return type.Name.Substring(1);
            }

            return type.Name;
        }

        /// <summary>
        /// Get the SQLite type affinity of the given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>TypeAffinity</returns>
        /// <remarks>http://sqlite.org/datatype3.html#affinity</remarks>
        public static TypeAffinity ToTypeAffinity(this Type type)
        {
            if (type.IsIntegerType())
                return TypeAffinity.Integer;
            else if (type.IsTextType())
                return TypeAffinity.Text;
            else if (type.IsBlobType())
                return TypeAffinity.None;
            else if (type.IsRealType())
                return TypeAffinity.Real;
            else
                return TypeAffinity.Text;
                //if (typeof(IComparable).IsAssignableFrom(type))
                //return TypeAffinity.Numeric;
        }

        public static bool IsSimpleType(this Type type)
        {
            if (type.IsEnum)
                return true;

            if (type == typeof(string))
                return true;

            if (type == typeof(Guid))
                return true;

            if (type == typeof(Uri))
                return true;

            if (type == typeof(DateTime))
                return true;

            if (type == typeof(TimeSpan))
                return true;

            if (type == typeof(byte[]))
                return true;

            if (type == typeof(ILanguage))
                return true;

            if (type == typeof(ILanguageTag))
                return true;

            if (type == typeof(ICountry))
                return true;

            if (type == typeof(IRegion))
                return true;

            var args = type.GetGenericArguments();
            if (args.Length > 0)
            {
                return args[0].IsSimpleType();
            }

            return false;
        }

        public static bool IsEntityType(this Type type)
        {
            return typeof(IEntity).IsAssignableFrom(type);
        }

        public static bool IsChildType(this Type type)
        {
            return typeof(IChild).IsAssignableFrom(type);
        }

        public static bool IsValueType(this Type type)
        {
            return typeof(IValue).IsAssignableFrom(type);
        }
    }
}
