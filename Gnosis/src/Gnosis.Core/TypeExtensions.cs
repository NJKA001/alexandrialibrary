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
                var tableName = table != null ? table.Name : type.GetTableName();
                var defaultSortExpression = defaultSort != null ? defaultSort.Expression : string.Empty;
                return new TableInfo(tableName, defaultSortExpression, type.GetColumnInfo(string.Empty), type.GetChildInfo(), type.GetCustomDataTypeInfo());
            }

            return null;
        }

        private static string GetTableName(this Type type)
        {
            if (type.IsInterface)
            {
                if (type.Name.StartsWith("I") && type.Name.Length > 1)
                    return type.Name.Substring(1);
            }

            return type.Name;
        }

        //public static IEnumerable<IndexInfo> GetIndexInfo(this Type type)
        //{
        //    var indexInfo = new List<IndexInfo>();

        //    foreach (var indexAttribute in type.GetIndexAttributes())
        //        indexInfo.Add(new IndexInfo(indexAttribute));

        //    return indexInfo;
        //}

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

        private static void AddChildInfo(this Type type, List<ChildInfo> childInfo)
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
                    var tableName = string.Format("{0}_{1}", type.GetTableName(), property.PropertyType.GetItemType().GetTableName());
                    var primaryKey = property.PropertyType.IsValueCollectionType() ? PrimaryKeyInfo.Default : null;
                    var foreignKey = ForeignKeyInfo.Default;
                    var sequence = property.PropertyType.IsOrderedCollectionType() ? SequenceInfo.Default : null;
                    childInfo.Add(new ChildInfo(tableName, property, primaryKey, foreignKey, sequence));
                }
            }
        }

        public static IEnumerable<ChildInfo> GetChildInfo(this Type type)
        {
            var childInfo = new List<ChildInfo>();

            foreach (var interfaceType in type.GetInterfaces())
            {
                interfaceType.AddChildInfo(childInfo);
            }

            type.AddChildInfo(childInfo);

            return childInfo;
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
                if (property.PropertyType.IsValueType())
                {
                    customDataTypeInfo.Add(new CustomDataTypeInfo(property.PropertyType.GetColumnInfo(property.PropertyType.GetTableName() + "_"), property));
                }
                else
                {
                    foreach (var attribute in property.PropertyType.GetCustomAttributes(true))
                    {
                        if (attribute is CustomDataTypeAttribute)
                        {
                            customDataTypeInfo.Add(new CustomDataTypeInfo(property.PropertyType.GetColumnInfo(string.Empty), property));
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

        //public static IEnumerable<Tuple<OneToManyAttribute, PropertyInfo>> GetOneToManyAttributes(this Type type)
        //{
        //    var oneToManyAttributes = new List<Tuple<OneToManyAttribute, PropertyInfo>>();

        //    AddOneToManyAttributes(type, oneToManyAttributes);

        //    foreach (var interfaceType in type.GetInterfaces())
        //    {
        //        AddOneToManyAttributes(interfaceType, oneToManyAttributes);
        //    }

        //    return oneToManyAttributes;
        //}

        private static void AddColumnInfo(this Type type, List<ColumnInfo> columnInfo, string prefix)
        {
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                ColumnAttribute columnAttribute = null;
                PrimaryKeyColumnAttribute primaryKeyColumnAttribute = null;
                var ignore = false;
                var isPrimaryKey = (type == typeof(IEntity) && property.Name == "Id");

                if (property.PropertyType.IsCustomDataType() || property.PropertyType.IsValueType())
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

        private static IEnumerable<ColumnInfo> GetColumnInfo(this Type type, string prefix)
        {
            var columnInfo = new List<ColumnInfo>();

            foreach (var interfaceType in type.GetInterfaces())
            {
                interfaceType.AddColumnInfo(columnInfo, prefix);
            }

            type.AddColumnInfo(columnInfo, prefix);

            return columnInfo;
        }

        public static void AddValueCreateStatement(this Type self, IBatch unitOfWork)
        {
        }

        public static void AddEntityCreateStatement(this Type self, IBatch unitOfWork)
        {
            var builder = new CommandBuilder();

            var table = self.GetTableInfo();

            //var createTable = new CreateTableStatement(builder, table);

            //builder.AddStatement(createTable);
            unitOfWork.Add(builder);
        }

        public static void AddEntityCreateStatement(this Type self, ChildInfo childInfo, IBatch unitOfWork)
        {

        }

        //public static void AddValueInsertStatement<T>(this T self, IUnitOfWork unitOfWork)
        //    where T : IValue
        //{
        //    self.AddValueInsertStatement(unitOfWork, typeof(T).GetTableInfo());
        //}

        //public static void AddValueInsertStatement<T>(this T self, IUnitOfWork unitOfWork, TableInfo table)
        //    where T : IValue
        //{
        //}

        public static void AddValueInsertStatement<T>(this T self, IBatch unitOfWork, ChildInfo childInfo, CollectionItemInfo itemInfo, IEntity parent)
            where T : IValue
        {
            var builder = new CommandBuilder();
            var statement = new InsertStatement(childInfo.TableName);

            if (childInfo.PrimaryKey != null)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(childInfo.PrimaryKey.Name, parameterName);
                builder.AddParameter(parameterName, itemInfo.Id);
            }

            if (childInfo.ForeignKey != null)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(childInfo.ForeignKey.Name, parameterName);
                builder.AddParameter(parameterName, parent.Id);
            }

            if (childInfo.Sequence != null)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(childInfo.Sequence.Name, parameterName);
                builder.AddParameter(parameterName, itemInfo.Sequence);
            }

            builder.AddStatement(statement);
            unitOfWork.Add(builder);
        }

        //public static void AddValueDeleteStatement<T>(this T self, IUnitOfWork unitOfWork)
        //    where T : IValue
        //{
        //    self.AddValueDeleteStatement(unitOfWork, typeof(T).GetTableInfo());
        //}

        //public static void AddValueDeleteStatement<T>(this T self, IUnitOfWork unitOfWork, TableInfo table)
        //    where T : IValue
        //{
        //}

        public static void AddValueDeleteStatement<T>(this T self, IBatch unitOfWork, ChildInfo childInfo, CollectionItemInfo itemInfo, IEntity parent)
            where T : IValue
        {
            var builder = new CommandBuilder();

            var parentParameterName = builder.GetParameterName();
            builder.AddParameter(parentParameterName, parent.Id);
            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, itemInfo.Id);
            var whereClause = string.Format("{0}.{1} = {2} and {0}.{3} = {4}", childInfo.TableName, childInfo.ForeignKey.Name, parent.Id, childInfo.PrimaryKey.Name, itemInfo.Id);
            var statement = new DeleteStatement(childInfo.TableName, whereClause);

            builder.AddStatement(statement);
            unitOfWork.Add(builder);
        }

        public static void AddValueMoveStatement<T>(this T self, IBatch unitOfWork, ChildInfo childInfo, CollectionItemInfo itemInfo, IEntity parent)
            where T : IValue
        {
            var builder = new CommandBuilder();

            var parentParameterName = builder.GetParameterName();
            builder.AddParameter(parentParameterName, parent.Id);
            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, itemInfo.Id);
            var whereClause = string.Format("{0}.{1} = {2} and {0}.{3} = {4}", childInfo.TableName, childInfo.ForeignKey.Name, parent.Id, childInfo.PrimaryKey.Name, itemInfo.Id);
            var statement = new UpdateStatement(childInfo.TableName, whereClause);

            var sequenceParameterName = builder.GetParameterName();
            statement.Set(childInfo.Sequence.Name, sequenceParameterName);
            builder.AddParameter(sequenceParameterName, itemInfo.Sequence);

            builder.AddStatement(statement);
            unitOfWork.Add(builder);
        }

        public static void AddEntitySaveStatement<T>(this T self, IBatch unitOfWork)
            where T : IEntity
        {
            self.AddEntitySaveStatement(unitOfWork, typeof(T).GetTableInfo());
        }

        public static void AddEntitySaveStatement<T>(this T self, IBatch unitOfWork, TableInfo table)
            where T : IEntity
        {
            if (self.IsNew())
                self.AddEntityInsertStatement(unitOfWork, table);
            else if (self.IsChanged())
                self.AddEntityUpdateStatement(unitOfWork, table);
        }

        public static void AddChildDeleteStatements(this IEnumerable<ChildInfo> self, IBatch unitOfWork, IEntity parent)
        {
            foreach (var childInfo in self)
            {
                var itemInfos = childInfo.GetItemInfo(parent);
                if (childInfo.BaseType.IsEntityType())
                {
                    IEntity entity = null;
                    foreach (var itemInfo in itemInfos)
                    {
                        entity = itemInfo.Item as IEntity;
                        entity.AddEntityDeleteStatement(unitOfWork, childInfo, itemInfo, parent);
                    }
                }
                else
                {
                    IValue value = null;
                    foreach (var itemInfo in itemInfos)
                    {
                        value = itemInfo.Item as IValue;
                        value.AddValueDeleteStatement(unitOfWork, childInfo, itemInfo, parent);
                    }
                }
            }
        }

        public static void AddChildSaveStatements(this IEnumerable<ChildInfo> self, IBatch unitOfWork, IEntity parent)
        {
            foreach (var child in self)
            {
                var itemInfos = child.GetItemInfo(parent);
                if (child.BaseType.IsEntityType())
                {
                    IEntity entity = null;
                    foreach (var itemInfo in itemInfos)
                    {
                        entity = itemInfo.Item as IEntity;

                        switch (itemInfo.State)
                        {
                            case Collections.CollectionItemState.Added:
                                entity.AddEntityInsertStatement(unitOfWork, child, itemInfo, parent);
                                break;
                            case Collections.CollectionItemState.Removed:
                                entity.AddEntityDeleteStatement(unitOfWork, child, itemInfo, parent);
                                break;
                            case Collections.CollectionItemState.Existing:
                                if (entity.IsChanged())
                                {
                                    entity.AddEntityUpdateStatement(unitOfWork, child, itemInfo, parent);
                                }
                                break;
                            case Collections.CollectionItemState.Moved:
                                entity.AddEntityUpdateStatement(unitOfWork, child, itemInfo, parent);
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    IValue value = null;

                    foreach (var itemInfo in itemInfos)
                    {
                        value = itemInfo.Item as IValue;

                        switch (itemInfo.State)
                        {
                            case Collections.CollectionItemState.Added:
                                value.AddValueInsertStatement(unitOfWork, child, itemInfo, parent);
                                break;
                            case Collections.CollectionItemState.Removed:
                                value.AddValueDeleteStatement(unitOfWork, child, itemInfo, parent);
                                break;
                            case Collections.CollectionItemState.Existing:
                                break;
                            case Collections.CollectionItemState.Moved:
                                value.AddValueMoveStatement(unitOfWork, child, itemInfo, parent);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        public static void AddEntityInsertStatement<T>(this T self, IBatch unitOfWork)
            where T : IEntity
        {
            self.AddEntityInsertStatement<T>(unitOfWork, typeof(T).GetTableInfo());
        }

        public static void AddEntityInsertStatement<T>(this T self, IBatch unitOfWork, TableInfo table)
            where T : IEntity
        {
            var builder = new CommandBuilder();
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

            table.Children.AddChildSaveStatements(unitOfWork, self);
        }

        public static void AddEntityInsertStatement<T>(this T self, IBatch unitOfWork, ChildInfo childInfo, CollectionItemInfo itemInfo, IEntity parent)
            where T : IEntity
        {
            var builder = new CommandBuilder();
            var statement = new InsertStatement(childInfo.TableName);

            if (childInfo.PrimaryKey != null)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(childInfo.PrimaryKey.Name, parameterName);
                builder.AddParameter(parameterName, itemInfo.Id);
            }

            if (childInfo.ForeignKey != null)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(childInfo.ForeignKey.Name, parameterName);
                builder.AddParameter(parameterName, parent.Id);
            }

            if (childInfo.Sequence != null)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(childInfo.Sequence.Name, parameterName);
                builder.AddParameter(parameterName, itemInfo.Sequence);
            }

            foreach (var column in childInfo.BaseTable.Columns)
            {
                var parameterName = builder.GetParameterName();
                statement.Add(column.Name, parameterName);
                builder.AddParameter(parameterName, column.GetValue(itemInfo.Item));
            }

            foreach (var customDataType in childInfo.BaseTable.CustomDataTypes)
            {
                foreach (var column in customDataType.Columns)
                {
                    var parameterName = builder.GetParameterName();
                    statement.Add(column.Name, parameterName);
                    builder.AddParameter(parameterName, column.GetValue(itemInfo.Item));
                }
            }

            builder.AddStatement(statement);
            unitOfWork.Add(builder);

            childInfo.BaseTable.Children.AddChildSaveStatements(unitOfWork, self);
        }

        public static void AddEntityUpdateStatement<T>(this T self, IBatch unitOfWork)
            where T : IEntity
        {
            self.AddEntityUpdateStatement<T>(unitOfWork, typeof(T).GetTableInfo());
        }

        public static void AddEntityUpdateStatement<T>(this T self, IBatch unitOfWork, TableInfo table)
            where T : IEntity
        {
            var builder = new CommandBuilder();
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

        public static void AddEntityUpdateStatement<T>(this T self, IBatch unitOfWork, ChildInfo childInfo, CollectionItemInfo itemInfo, IEntity parent)
            where T : IEntity
        {
            var builder = new CommandBuilder();

            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, self.Id);
            var whereClause = string.Format("{0}.Id = {1}", childInfo.TableName, idParameterName);
            var statement = new UpdateStatement(childInfo.TableName, whereClause);

            if (childInfo.Sequence != null)
            {
                var parameterName = builder.GetParameterName();
                statement.Set(childInfo.Sequence.Name, parameterName);
                builder.AddParameter(parameterName, itemInfo.Sequence);
            }

            foreach (var column in childInfo.BaseTable.Columns)
            {
                var parameterName = builder.GetParameterName();
                statement.Set(column.Name, parameterName);
                builder.AddParameter(parameterName, column.GetValue(itemInfo.Item));
            }

            foreach (var customDataType in childInfo.BaseTable.CustomDataTypes)
            {
                foreach (var column in customDataType.Columns)
                {
                    var parameterName = builder.GetParameterName();
                    statement.Set(column.Name, parameterName);
                    builder.AddParameter(parameterName, column.GetValue(itemInfo.Item));
                }
            }

            builder.AddStatement(statement);
            unitOfWork.Add(builder);

            childInfo.BaseTable.Children.AddChildSaveStatements(unitOfWork, self);
        }

        public static void AddEntityDeleteStatement<T>(this T self, IBatch unitOfWork)
            where T : IEntity
        {
            self.AddEntityDeleteStatement(unitOfWork, typeof(T).GetTableInfo());
        }

        public static void AddEntityDeleteStatement<T>(this T self, IBatch unitOfWork, TableInfo table)
            where T : IEntity
        {
            var builder = new CommandBuilder();

            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, self.Id);
            var whereClause = string.Format("{0}.Id = {1}", table.Name, idParameterName);
            var statement = new DeleteStatement(table.Name, whereClause);

            builder.AddStatement(statement);
            unitOfWork.Add(builder);
        }

        public static void AddEntityDeleteStatement<T>(this T self, IBatch unitOfWork, ChildInfo childInfo, CollectionItemInfo itemInfo, IEntity parent)
            where T : IEntity
        {
            var builder = new CommandBuilder();

            var idParameterName = builder.GetParameterName();
            builder.AddParameter(idParameterName, self.Id);
            var whereClause = string.Format("{0}.Id = {1}", childInfo.TableName, idParameterName);
            var statement = new DeleteStatement(childInfo.TableName, whereClause);

            builder.AddStatement(statement);
            unitOfWork.Add(builder);

            childInfo.BaseTable.Children.AddChildDeleteStatements(unitOfWork, self);
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

        //public static bool IsTimeStampType(this Type type)
        //{
        //    return typeof(ITimeStamp).IsAssignableFrom(type);
        //}

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
