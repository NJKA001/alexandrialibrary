﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Gnosis.Core.Attributes;
using Gnosis.Core.Commands;

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

        public static TableInfo GetTableInfo(this Type type)
        {
            TableAttribute table = null;
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
            }

            if (table != null)
            {
                var defaultSortExpression = defaultSort != null ? defaultSort.Expression : string.Empty;
                new TableInfo(table.Name, defaultSortExpression, type.GetColumnInfo(), type.GetIndexInfo());
            }

            return null;
        }

        //public static DefaultSortAttribute GetDefaultSortAttribute(this Type type)
        //{
        //    foreach (var attribute in type.GetCustomAttributes(true))
        //    {
        //        if (attribute is DefaultSortAttribute)
        //        {
        //            return attribute as DefaultSortAttribute;
        //        }
        //    }

        //    return null;
        //}

        public static IEnumerable<IndexInfo> GetIndexInfo(this Type type)
        {
            var indexInfo = new List<IndexInfo>();

            foreach (var indexAttribute in type.GetIndexAttributes())
                indexInfo.Add(new IndexInfo(indexAttribute));

            return indexInfo;
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

        public static IEnumerable<OneToManyAttribute> GetOneToManyAttributes(this Type type)
        {
            var oneToManyAttributes = new List<OneToManyAttribute>();

            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is OneToManyAttribute)
                        oneToManyAttributes.Add(attribute as OneToManyAttribute);
                }
            }

            return oneToManyAttributes;
        }

        private static void AddColumnInfo(List<ColumnInfo> columnInfo, Type type)
        {
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                ColumnAttribute columnAttribute = null;
                var ignore = false;

                foreach (var propertyAttribute in property.GetCustomAttributes(true))
                {
                    if (propertyAttribute is ColumnIgnoreAttribute)
                    {
                        ignore = true;
                        break;
                    }

                    if (propertyAttribute is ColumnAttribute)
                        columnAttribute = propertyAttribute as ColumnAttribute;
                }

                if (!ignore)
                {
                    var name = (columnAttribute != null) ? columnAttribute.Name : property.Name;
                    columnInfo.Add(new ColumnInfo(name, property));
                }
            }
        }

        private static IEnumerable<ColumnInfo> GetColumnInfo(this Type type)
        {
            var columnInfo = new List<ColumnInfo>();

            foreach (var typeInterface in type.GetInterfaces())
            {
                AddColumnInfo(columnInfo, typeInterface);
            }

            AddColumnInfo(columnInfo, type);

            return columnInfo;
        }

        public static void AddValueInsertStatement<T>(this T self, CommandBuilder builder)
            where T : IValue
        {
            self.AddValueInsertStatement(builder, typeof(T).GetTableInfo());
        }

        public static void AddValueInsertStatement<T>(this T self, CommandBuilder builder, TableInfo table)
            where T : IValue
        {

        }

        public static void AddValueDeleteStatement<T>(this T self, CommandBuilder builder)
            where T : IValue
        {
            self.AddValueDeleteStatement(builder, typeof(T).GetTableInfo());
        }

        public static void AddValueDeleteStatement<T>(this T self, CommandBuilder builder, TableInfo table)
            where T : IValue
        {
        }

        public static void AddEntitySaveStatement<T>(this T self, CommandBuilder builder)
            where T : IEntity
        {
            if (self.IsNew)
                self.AddEntityInsertStatement(builder);
            else if (self.IsChanged)
                self.AddEntityUpdateStatement(builder);
        }

        public static void AddEntitySaveStatement<T>(this T self, CommandBuilder builder, TableInfo table)
            where T : IEntity
        {
            if (self.IsNew)
                self.AddEntityInsertStatement(builder);
            else if (self.IsChanged)
                self.AddEntityUpdateStatement(builder);
        }

        public static void AddEntityInsertStatement<T>(this T self, CommandBuilder builder)
            where T : IEntity
        {
            self.AddEntityInsertStatement<T>(builder, typeof(T).GetTableInfo());
        }

        public static void AddEntityInsertStatement<T>(this T self, CommandBuilder builder, TableInfo table)
            where T : IEntity
        {
            var idParameterName = builder.GetParameterName();
            var insertStatement = new InsertStatement(table.Name);

            foreach (var column in table.Columns)
            {
                var columnParameterName = builder.GetParameterName();
                insertStatement.Add(column.Name, columnParameterName);
                builder.AddParameter(columnParameterName, column.GetValue(self));
            }
        }

        public static void AddEntityUpdateStatement<T>(this T self, CommandBuilder builder)
            where T : IEntity
        {
            self.AddEntityUpdateStatement<T>(builder, typeof(T).GetTableInfo());
        }

        public static void AddEntityUpdateStatement<T>(this T self, CommandBuilder builder, TableInfo table)
            where T : IEntity
        {
            var idParameterName = builder.GetParameterName();
            var whereClause = string.Format("{0}.Id = {1}", table.Name, idParameterName);
            var updateStatement = new UpdateStatement("Feed", whereClause);
            builder.AddParameter(idParameterName, self.Id);

            foreach (var column in table.Columns.Where(x => x.IsReadOnly == false))
            {
                var columnParameterName = builder.GetParameterName();
                updateStatement.Set(column.Name, columnParameterName);
                builder.AddParameter(columnParameterName, column.GetValue(self));
            }

            builder.AddStatement(updateStatement);
        }

        public static void AddEntityDeleteStatement<T>(this T self, CommandBuilder builder)
            where T : IEntity
        {
            self.AddEntityDeleteStatement(builder, typeof(T).GetTableInfo());
        }

        public static void AddEntityDeleteStatement<T>(this T self, CommandBuilder builder, TableInfo table)
            where T : IEntity
        {
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
