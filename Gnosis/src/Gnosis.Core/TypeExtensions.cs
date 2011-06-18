using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

//using Gnosis.Core.Batches;
//using Gnosis.Core.Commands;
using Gnosis.Core.Iso;

namespace Gnosis.Core
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Get the SQLite type affinity of the given type
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

        public static string GetNormalizedName(this Type type)
        {
            if (type.IsInterface)
            {
                if (type.Name.StartsWith("I") && type.Name.Length > 1)
                    return type.Name.Substring(1);
            }

            return type.Name;
        }

        /*
        public static TableInfo GetTableInfo(this Type type)
        {
            TableAttribute table = null;
            TableIgnoreAttribute tableIgnore = null;
            DefaultSortAttribute defaultSort = null;

            foreach (var attribute in type.GetCustomAttributes(true))
            {
                if (attribute is TableAttribute)
                {
                    table = attribute as TableAttribute;
                }
                else if (attribute is DefaultSortAttribute)
                {
                    defaultSort = attribute as DefaultSortAttribute;
                }
                else if (attribute is TableIgnoreAttribute)
                {
                    tableIgnore = attribute as TableIgnoreAttribute;
                }
            }

            if (tableIgnore == null)
            {
                var tableName = table != null ? table.Name : type.GetDefaultTableName();
                var defaultSortExpression = defaultSort != null ? defaultSort.Expression : string.Empty;
                return new TableInfo(tableName, defaultSortExpression, type.GetColumnInfo(string.Empty, true), type.GetChildInfo(), type.GetCustomDataTypeInfo());
            }

            return null;
        }

        //public static bool IsCustomDataType(this Type type)
        //{
        //    foreach (var attribute in type.GetCustomAttributes(true))
        //    {
        //        if (attribute is CustomDataTypeAttribute)
        //            return true;
        //    }

        //    return false;
        //}

        public static Type GetItemType(this Type type)
        {
            var args = type.GetGenericArguments();
            if (args != null && args.Length > 0)
                return args[0];

            return null;
        }

        public static bool IsEntityCollectionType(this Type type)
        {
            var itemType = type.GetItemType();
            if (itemType != null)
                return itemType.IsEntityType();

            return false;
        }

        public static bool IsEntityType(this Type type)
        {
            return typeof(IEntity).IsAssignableFrom(type);
        }

        public static bool IsValueCollectionType(this Type type)
        {
            var itemType = type.GetItemType();
            if (itemType != null)
                return itemType.IsValueType();

            return false;
        }

        public static bool IsValueType(this Type type)
        {
            return typeof(IValue).IsAssignableFrom(type);
        }

        public static bool IsCollectionType(this Type type)
        {
            return type.IsOrderedCollectionType() || type.IsUnorderedCollectionType();
        }

        public static bool IsOrderedCollectionType(this Type type)
        {
            return type.Name == "IOrderedSet`1";
        }

        public static bool IsUnorderedCollectionType(this Type type)
        {
            return type.Name == "ISet`1";
        }
        */

        public static bool IsSimple(this Type type)
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

            if (type == typeof(IIso639Language))
                return true;

            var args = type.GetGenericArguments();
            if (args.Length > 0)
            {
                return args[0].IsSimple();
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

        public static bool IsTextColumn(this Type type)
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
            else if (type == typeof(IIso639Language))
                return true;

            var args = type.GetGenericArguments();
            if (args.Length > 0)
            {
                return args[0].IsTextColumn();
            }

            return false;
        }

        public static bool IsIntegerColumn(this Type type)
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
