using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Gnosis.Core.Attributes;
using Gnosis.Core.Commands;
using System.Collections;

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
                new TableInfo(table.Name, defaultSortExpression, type.GetColumnInfo(), type.GetIndexInfo(), type.GetOneToManyInfo(), type.GetCustomDataTypeInfo());
            }

            return null;
        }

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

        public static IEnumerable<OneToManyInfo> GetOneToManyInfo(this Type type)
        {
            var oneToManyInfo = new List<OneToManyInfo>();

            foreach (var oneToMany in type.GetOneToManyAttributes())
            {

                oneToManyInfo.Add(new OneToManyInfo(oneToMany.Item1, oneToMany.Item2));
            }

            return oneToManyInfo;
        }

        public static IEnumerable<CustomDataTypeInfo> GetCustomDataTypeInfo(this Type type)
        {
            var customDataTypeInfo = new List<CustomDataTypeInfo>();

            foreach (var interfaceType in type.GetInterfaces())
            {
                interfaceType.AddCustomDataTypeInfo(customDataTypeInfo);
            }

            type.AddCustomDataTypeInfo(customDataTypeInfo);

            return customDataTypeInfo;
        }

        public static void AddCustomDataTypeInfo(this Type type, List<CustomDataTypeInfo> customDataTypeInfo)
        {
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                foreach (var attribute in type.GetCustomAttributes(true))
                {
                    if (attribute is CustomDataTypeAttribute)
                    {
                         customDataTypeInfo.Add(new CustomDataTypeInfo(property.PropertyType.GetColumnInfo(), property));
                    }
                }
            }
        }

        public static IEnumerable<Tuple<OneToManyAttribute, PropertyInfo>> GetOneToManyAttributes(this Type type)
        {
            var oneToManyAttributes = new List<Tuple<OneToManyAttribute, PropertyInfo>>();

            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is OneToManyAttribute)
                        oneToManyAttributes.Add(new Tuple<OneToManyAttribute, PropertyInfo>(attribute as OneToManyAttribute, property));
                }
            }

            return oneToManyAttributes;
        }

        private static void AddColumnInfo(this Type type, List<ColumnInfo> columnInfo)
        {
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                ColumnAttribute columnAttribute = null;
                PrimaryKeyColumnAttribute primaryKeyColumnAttribute = null;
                var ignore = false;

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

                    if (property.PropertyType.IsCustomDataType())
                    {
                        ignore = true;
                        break;
                    }

                    if (propertyAttribute is ColumnAttribute)
                        columnAttribute = propertyAttribute as ColumnAttribute;

                    if (propertyAttribute is PrimaryKeyColumnAttribute)
                        primaryKeyColumnAttribute = propertyAttribute as PrimaryKeyColumnAttribute;
                }

                if (!ignore)
                {
                    if (columnAttribute != null)
                    {
                        var name = !string.IsNullOrEmpty(columnAttribute.Name) ? columnAttribute.Name : property.Name;
                        columnInfo.Add(new ColumnInfo(name, property));
                    }
                    else if (primaryKeyColumnAttribute != null)
                    {
                        var name = !string.IsNullOrEmpty(primaryKeyColumnAttribute.Name) ? primaryKeyColumnAttribute.Name : property.Name;
                        columnInfo.Add(new ColumnInfo(name, property, true, primaryKeyColumnAttribute.AutoIncrement));
                    }
                    else
                    {
                        columnInfo.Add(new ColumnInfo(property.Name, property));
                    }
                }
            }
        }

        private static IEnumerable<ColumnInfo> GetColumnInfo(this Type type)
        {
            var columnInfo = new List<ColumnInfo>();

            foreach (var interfaceType in type.GetInterfaces())
            {
                interfaceType.AddColumnInfo(columnInfo);
            }

            type.AddColumnInfo(columnInfo);

            return columnInfo;
        }

        public static void AddValueInsertStatement<T>(this T self, IUnitOfWork unitOfWork)
            where T : IValue
        {
            self.AddValueInsertStatement(unitOfWork, typeof(T).GetTableInfo());
        }

        public static void AddValueInsertStatement<T>(this T self, IUnitOfWork unitOfWork, TableInfo table)
            where T : IValue
        {

        }

        public static void AddValueDeleteStatement<T>(this T self, IUnitOfWork unitOfWork)
            where T : IValue
        {
            self.AddValueDeleteStatement(unitOfWork, typeof(T).GetTableInfo());
        }

        public static void AddValueDeleteStatement<T>(this T self, IUnitOfWork unitOfWork, TableInfo table)
            where T : IValue
        {
        }

        public static void AddEntitySaveStatement<T>(this T self, IUnitOfWork unitOfWork)
            where T : IEntity
        {
            self.AddEntitySaveStatement(unitOfWork, typeof(T).GetTableInfo());
        }

        public static void AddEntitySaveStatement<T>(this T self, IUnitOfWork unitOfWork, TableInfo table)
            where T : IEntity
        {
            if (self.IsNew)
                self.AddEntityInsertStatement(unitOfWork, table);
            else if (self.IsChanged)
                self.AddEntityUpdateStatement(unitOfWork, table);
        }

        public static void AddEntitySaveStatements(this IEnumerable self, IUnitOfWork unitOfWork, OneToManyInfo child)
        {
        }

        public static void AddEntityInsertStatement<T>(this T self, IUnitOfWork unitOfWork)
            where T : IEntity
        {
            self.AddEntityInsertStatement<T>(unitOfWork, typeof(T).GetTableInfo());
        }

        public static void AddEntityInsertStatement<T>(this T self, IUnitOfWork unitOfWork, TableInfo table)
            where T : IEntity
        {
            var builder = new SaveCommandBuilder();
            var statement = new InsertStatement(table.Name);

            foreach (var column in table.Columns)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(column.Name, parameterName);
                builder.AddParameter(parameterName, column.GetValue(self));
            }

            foreach (var customDataType in table.CustomDataTypes)
            {
                var dataTypeValue = customDataType.GetValue(self);

                foreach (var column in customDataType.Columns)
                {
                    var parameterName = builder.GetParameterName();
                    statement.Add(column.Name, parameterName);
                    builder.AddParameter(parameterName, column.GetValue(dataTypeValue));
                }
            }

            builder.AddStatement(statement);
            unitOfWork.Add(builder);

            foreach (var childInfo in table.Children)
            {
                var childrenValues = childInfo.GetValues(self);
                childrenValues.AddEntitySaveStatements(unitOfWork, childInfo);
            }
        }

        public static void AddEntityUpdateStatement<T>(this T self, IUnitOfWork unitOfWork)
            where T : IEntity
        {
            self.AddEntityUpdateStatement<T>(unitOfWork, typeof(T).GetTableInfo());
        }

        public static void AddEntityUpdateStatement<T>(this T self, IUnitOfWork unitOfWork, TableInfo table)
            where T : IEntity
        {
            var builder = new SaveCommandBuilder();
            var idParameterName = builder.GetParameterName();
            var whereClause = string.Format("{0}.Id = {1}", table.Name, idParameterName);
            var statement = new UpdateStatement(table.Name, whereClause);
            builder.AddParameter(idParameterName, self.Id);

            foreach (var column in table.Columns.Where(x => x.IsReadOnly == false))
            {
                var parameterName = builder.GetParameterName();
                statement.Set(column.Name, parameterName);
                builder.AddParameter(parameterName, column.GetValue(self));
            }

            foreach (var customDataType in table.CustomDataTypes)
            {
                var dataTypeValue = customDataType.GetValue(self);

                foreach (var column in customDataType.Columns)
                {
                    var parameterName = builder.GetParameterName();
                    statement.Set(column.Name, parameterName);
                    builder.AddParameter(parameterName, column.GetValue(dataTypeValue));
                }
            }

            builder.AddStatement(statement);
            unitOfWork.Add(builder);
        }

        public static void AddEntityDeleteStatement<T>(this T self, IUnitOfWork unitOfWork)
            where T : IEntity
        {
            self.AddEntityDeleteStatement(unitOfWork, typeof(T).GetTableInfo());
        }

        public static void AddEntityDeleteStatement<T>(this T self, IUnitOfWork unitOfWork, TableInfo table)
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
