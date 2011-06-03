using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Gnosis.Core.Attributes;
using Gnosis.Core.Batches;
using Gnosis.Core.Collections;
using Gnosis.Core.Commands;

namespace Gnosis.Core
{
    public static class TypeExtensions
    {
        #region Private Helpers Methods

        private static void AddChildInfo(Type type, List<ChildInfo> childInfo)
        {
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var hasAttribute = false;
                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    var oneToManyAttribute = attribute as OneToManyAttribute;
                    if (oneToManyAttribute != null)
                    {
                        hasAttribute = true;
                        childInfo.Add(new ChildInfo(oneToManyAttribute, property));
                        break;
                    }
                }

                if (!hasAttribute && property.PropertyType.IsCollectionType())
                {
                    var tableName = string.Format("{0}_{1}", type.GetDefaultTableName(), property.PropertyType.GetItemType().GetDefaultTableName());
                    var foreignKey = ForeignKeyInfo.Default;
                    var sequence = property.PropertyType.IsOrderedCollectionType() ? SequenceInfo.Default : null;
                    childInfo.Add(new ChildInfo(tableName, property, foreignKey, sequence));
                }
            }
        }

        private static void AddCustomDataTypeInfo(Type type, List<CustomDataTypeInfo> customDataTypeInfo)
        {
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.PropertyType.IsValueType())
                {
                    customDataTypeInfo.Add(new CustomDataTypeInfo(property.PropertyType.GetColumnInfo(property.PropertyType.GetDefaultTableName() + "_", false), property));
                }
                else
                {
                    foreach (var attribute in property.PropertyType.GetCustomAttributes(true))
                    {
                        if (attribute is CustomDataTypeAttribute)
                        {
                            customDataTypeInfo.Add(new CustomDataTypeInfo(property.PropertyType.GetColumnInfo(string.Empty, false), property));
                        }
                    }
                }
            }
        }

        private static void AddOneToManyAttributes(Type type, List<Tuple<OneToManyAttribute, PropertyInfo>> oneToManyAttributes)
        {
            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is OneToManyAttribute)
                        oneToManyAttributes.Add(new Tuple<OneToManyAttribute, PropertyInfo>(attribute as OneToManyAttribute, property));
                }
            }
        }

        private static void AddColumnInfo(Type type, List<ColumnInfo> columnInfo, string prefix, bool includePrimaryKey)
        {
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                ColumnAttribute columnAttribute = null;
                PrimaryKeyColumnAttribute primaryKeyColumnAttribute = null;
                var ignore = false;
                var isPrimaryKey = ((type == typeof(IEntity) || type == typeof(IValue)) && property.Name == "Id");

                if (isPrimaryKey && !includePrimaryKey)
                {
                    ignore = true;
                }
                else if (property.PropertyType.IsCustomDataType() || property.PropertyType.IsValueType())
                {
                    ignore = true;
                }
                else
                {
                    foreach (var propertyAttribute in property.GetCustomAttributes(true))
                    {
                        if (propertyAttribute is ColumnIgnoreAttribute)
                        {
                            ignore = true;
                            break;
                        }

                        if (propertyAttribute is OneToManyAttribute)
                        {
                            ignore = true;
                            break;
                        }

                        if (propertyAttribute is ColumnAttribute)
                            columnAttribute = propertyAttribute as ColumnAttribute;

                        if (propertyAttribute is PrimaryKeyColumnAttribute)
                            primaryKeyColumnAttribute = propertyAttribute as PrimaryKeyColumnAttribute;
                    }
                }

                if (!ignore)
                {
                    if (columnAttribute != null)
                    {
                        var name = !string.IsNullOrEmpty(columnAttribute.Name) ? (prefix ?? string.Empty) + columnAttribute.Name : (prefix ?? string.Empty) + property.Name;
                        var defaultValue = columnAttribute.DefaultValue;
                        columnInfo.Add(new ColumnInfo(name, property, defaultValue));
                    }
                    else if (primaryKeyColumnAttribute != null || isPrimaryKey)
                    {
                        var name = (primaryKeyColumnAttribute != null && !string.IsNullOrEmpty(primaryKeyColumnAttribute.Name)) ? primaryKeyColumnAttribute.Name : property.Name;
                        var isAutoIncrement = primaryKeyColumnAttribute != null ? primaryKeyColumnAttribute.IsAutoIncrement : false;
                        columnInfo.Add(new ColumnInfo(name, property, true, isAutoIncrement));
                    }
                    else
                    {
                        var name = (prefix ?? string.Empty) + property.Name;
                        columnInfo.Add(new ColumnInfo(name, property, null));
                    }
                }
            }
        }

        private static IEnumerable<ColumnInfo> GetColumnInfo(this Type type, string prefix, bool includePrimaryKey)
        {
            var columnInfo = new List<ColumnInfo>();

            foreach (var interfaceType in type.GetInterfaces())
            {
                AddColumnInfo(interfaceType, columnInfo, prefix, includePrimaryKey);
            }

            AddColumnInfo(type, columnInfo, prefix, includePrimaryKey);

            return columnInfo;
        }

        private static IEnumerable<IndexAttribute> GetIndexAttributes(this Type type)
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

        private static IEnumerable<ChildInfo> GetChildInfo(this Type type)
        {
            var childInfo = new List<ChildInfo>();

            foreach (var interfaceType in type.GetInterfaces())
            {
                AddChildInfo(interfaceType, childInfo);
            }

            AddChildInfo(type, childInfo);

            return childInfo;
        }

        private static IEnumerable<CustomDataTypeInfo> GetCustomDataTypeInfo(this Type type)
        {
            var customDataTypeInfo = new List<CustomDataTypeInfo>();

            foreach (var interfaceType in type.GetInterfaces())
            {
                AddCustomDataTypeInfo(interfaceType, customDataTypeInfo);
            }

            AddCustomDataTypeInfo(type, customDataTypeInfo);

            return customDataTypeInfo;
        }

        private static string GetDefaultTableName(this Type type)
        {
            if (type.IsInterface)
            {
                if (type.Name.StartsWith("I") && type.Name.Length > 1)
                    return type.Name.Substring(1);
            }

            return type.Name;
        }

        #endregion

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

        public static bool IsCustomDataType(this Type type)
        {
            foreach (var attribute in type.GetCustomAttributes(true))
            {
                if (attribute is CustomDataTypeAttribute)
                    return true;
            }

            return false;
        }

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
